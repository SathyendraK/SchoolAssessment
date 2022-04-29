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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            AddSchoolBtn.Visible = false;

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            //else if ((Session["AdminUserType"].ToString() != "ADMIN"))
            else if ((Session["AdminUserType"].ToString() == "FIELDREP"))
            {
                Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
            }

            if ((Request.QueryString["Saved"] == "1"))
            {
                lblMsg.Text = "<p><span class=\"greenbold\">Updated</span></p>";
                lblMsg.Visible = true;
            }


            if (Page.IsPostBack == false)
            {

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sql = "SELECT * FROM Schools WHERE Cohort = 'S' AND SchCode = '" + TxtSchCode.Text + "'";
           

            try
            {
                /*
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    lblMsg.Text = "<p><span class=\"redbold\">School already exist. </span></p>";
                    lblMsg.Visible = true;
                }
                */

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblMsg.Text = "<p><span class=\"redbold\">School does not exist. Would you like to add this school? </span></p>";
                    lblMsg.Visible = true;
                    AddSchoolBtn.Visible = true;

                } else
                {
                    lblMsg.Text = "<p><span class=\"redbold\">School already exist. </span></p>";
                    lblMsg.Visible = true;

                }


                DataView dv = new DataView(ds.Tables[0]);
                GridSchoolsToSearch.DataSource = dv;
                GridSchoolsToSearch.DataBind();


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

        protected void AddSchoolBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAddSchool7th.aspx?SchCode=" + TxtSchCode.Text, true);
        }
    }
}