using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using System.Web;

namespace SchoolAssessment.Admin
{
    public partial class AdminEditUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            else if ((Session["AdminUserType"].ToString() != "ADMIN"))
            {
                Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
            }

            if ((Request.QueryString["Saved"] == "1"))
            {
                lblMsg.Text = "<p><span class=\"greenbold\">Updated</span></p>";
                lblMsg.Visible = true;
            }


            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sql = "SELECT t1.*, t2.CoName FROM AdminUsers t1 LEFT JOIN Counties t2 on t1.CoCode = t2.CoCode";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (Page.IsPostBack == false)
            {
                FillCounty();
            }

            //command = new SqlCommand("SELECT t1.*, t2.CountyName FROM AdminUsers t1 LEFT JOIN Counties t2 on t1.CoCode = t2.CountyCode", conn);


            DataView dv = new DataView(ds.Tables[0]);
            grdUsers.DataSource = dv;
            grdUsers.DataBind();

        }

        private void FillCounty()
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            //string sql = "SELECT CountyCode, CountyName from Counties where CountyCode != '00' order by CountyName asc";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT CoCode, CoName from Counties where CoCode != '00' order by CoName asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            

            try
            {
                da.Fill(ds);


                CountyList.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                CountyList.DataValueField = ds.Tables[0].Columns["CoName"].ToString();

                CountyList.DataSource = ds.Tables[0];
                CountyList.DataBind();

                CountyList.Items.Insert(0, ("--Select---"));


            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            Page.Validate();
            if ((Page.IsValid))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string sql = null;
                SqlCommand cmd = default(SqlCommand);
                int ret = 0;



                try
                {
                    //CountyList.SelectedItem.Value = "04";

                    if ((!string.IsNullOrEmpty(CountyList.SelectedValue)))
                    {
                        //sql = "UPDATE AdminUsers set UserName = @UserName where CoCode = @CoCode";
                        sql = "UPDATE AdminUsers SET UserName = @UserName FROM Counties C WHERE AdminUsers.CoCode = C.CoCode AND C.CoName = @CoCode";
                    }
                    else if ((!string.IsNullOrEmpty(RegionList.SelectedValue)))
                    {
                        sql = "UPDATE AdminUsers set UserName = @UserName where RegionCode = @RegionCode";
                    }



                    if ((!string.IsNullOrEmpty(sql)))
                    {
                        cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@UserName", txtEmail.Text);
                        //cmd.Parameters.AddWithValue("@CoCode", CountyList.SelectedValue);
                        cmd.Parameters.AddWithValue("@CoCode", CountyList.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@RegionCode", RegionList.SelectedValue);



                        con.Open();
                        ret = cmd.ExecuteNonQuery();
                        if ((ret == 1))
                        {
                            Response.Redirect("AdminEditUsers.aspx?Saved=1", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    lblMsg.Text = "<p><span class=\"redbold\">" + appError + "</span></p>";
                    lblMsg.Visible = true;
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
}