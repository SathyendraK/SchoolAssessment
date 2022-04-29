using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SchoolAssessment.Admin
{
    public partial class AdminAddDistricts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Page.IsPostBack == false))
            {
                Session["AdminAddDistrictCoCode"] = "";
                FillCounty();
                txtCounty.Items.Add("--Select--");
                
            }

        }



        protected void txtCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCountyCode();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            
            dynamic county = txtCounty.SelectedItem.Value.Replace("'", "''");
            //string sql = "SELECT DistCode, DistName, C.CoName FROM Districts d INNER JOIN Counties c on d.CoCode = c.CoCode WHERE d.DistCode = '" + TexDistCode.Text + "' AND d.DistName like '" + TextDistName.Text + "' AND c.CoName = '" + county + "'";
            string sql = "SELECT DistCode, DistName, C.CoName FROM Districts d INNER JOIN Counties c on d.CoCode = c.CoCode WHERE d.DistCode = '" + TexDistCode.Text + "'";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {

                    // Insert School District 
                    InsertSchoolDistrict();
                    

                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = "<p><span class=\"redbold\">School district already exist. </span></p>";
                    lblMsg.Visible = true;
                }


                DataView dv = new DataView(ds.Tables[0]);
                GridAddDistrict.DataSource = dv;
                GridAddDistrict.DataBind();


            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                //cmd = null;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        protected void GridAddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InsertSchoolDistrict()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string sql = "INSERT INTO [dbo].[Districts] (DistCode, DistName, CoCode) VALUES (@DistCode, @DistName, @CoCode);"; 
            SqlCommand cmd = default(SqlCommand);
            int ret = 0;

          
            try
            {
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DistCode", TexDistCode.Text);
                cmd.Parameters.AddWithValue("@DistName", TextDistName.Text);
                cmd.Parameters.AddWithValue("@CoCode", Session["AdminAddDistrictCoCode"].ToString());

                con.Open();
                ret = (int)cmd.ExecuteNonQuery();

                if (ret > 0)
                {
                    if ((!string.IsNullOrEmpty(TexDistCode.Text)))
                    {
                        logaudittrail("DistCode", "", TexDistCode.Text);
                    }
                    if ((!string.IsNullOrEmpty(TextDistName.Text)))
                    {
                        logaudittrail("DistName", "", TextDistName.Text);
                    }


                    lblMsg.Text = "<p><span class=\"greenbold\">New school district added: </span></p>";
                    lblMsg.Visible = true;


                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void FillCountyCode()
        {

            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            dynamic county = txtCounty.SelectedItem.Value.Replace("'", "''");
            string sql = "SELECT distinct CoCode FROM Counties where CoName ='" + county + "' ";
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);

            //Dim ds As New DataSet
            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                
                Session["AdminAddDistrictCoCode"] = reader["CoCode"].ToString();


            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void FillCounty()
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            dynamic userCoCode = Session["AdminCoCode"];
            SqlCommand cmd;

            if ((userCoCode == "00"))
            {
                string sql_admin = "SELECT DISTINCT CoName FROM Counties";
                cmd = new SqlCommand(sql_admin, con);
            }
            else
            {
                string sql = "SELECT DISTINCT CoName FROM Counties WHERE CoCode = '" + userCoCode + "' ";
                cmd = new SqlCommand(sql, con);

            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);

                txtCounty.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                txtCounty.DataValueField = ds.Tables[0].Columns["CoName"].ToString();

                txtCounty.DataSource = ds.Tables[0];
                txtCounty.DataBind();

                txtCounty.Items.Insert(0, ("--Select---"));
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void TextDistName_TextChanged(object sender, EventArgs e)
        {

        }

        private void logaudittrail(string fieldname, string fromval, string toval)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string sql = null;
            SqlCommand cmd = default(SqlCommand);


            try
            {
                sql = "insert into AuditTrail(SchCode, ScreenName, FieldName, FromValue, ToValue, IP, TimeStamp) values (@DistCode, @ScreenName, @FieldName, @FromValue, @ToValue, @IP, @TimeStamp)";
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@DistCode", TexDistCode.Text);
                cmd.Parameters.AddWithValue("@ScreenName", "AdminAddDistricts");
                cmd.Parameters.AddWithValue("@FieldName", fieldname);
                cmd.Parameters.AddWithValue("@FromValue", fromval);
                cmd.Parameters.AddWithValue("@ToValue", toval);
                cmd.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                cmd = null;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
    }
}