
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



namespace SchoolAssessment.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["reason"] == "Logout"))
            {
                lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">Logged out</span></p>";
                lblMsg.Visible = true;
            }
            else if ((Request.QueryString["reason"] == "TimedOut"))
            {
                lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">Timed out</span></p>";
                lblMsg.Visible = true;
            }
            else if ((Request.QueryString["reason"] == "InsufficientPrivileges"))
            {
                lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">Insufficient Privileges</span></p>";
                lblMsg.Visible = true;
            }



        }




        private void CheckPassword(ref bool Pwdstatus)
        {
            string Inpassword = txtPassword.Text.ToString();
            string Inusername = txtUsername.Text.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string connectString = System.Configuration.ConfigurationManager.AppSettings("kSchoolDBConnectString");
            //string allowLHDToLogin = System.Configuration.ConfigurationManager.AppSettings("allowLHDToLogin");
            string allowLHDToLogin = System.Configuration.ConfigurationManager.AppSettings["allowLHDToLogin"];
            string sql = "SELECT top 1 * FROM AdminUsers where UserName = @UserName";
            

            SqlDataReader reader = default(SqlDataReader);
            string Err = null;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserName", Inusername.ToUpper().ToString());
                con.Open();
                reader = cmd.ExecuteReader();

                if ((reader.HasRows == true))
                {
                    reader.Read();
                    if (Inpassword.ToUpper().ToString() == (String)reader["Password"].ToString().ToUpper() )
                    {
                        if (allowLHDToLogin != "True" && (String)reader["UserType"].ToString() == "LHD" )
                        {
                            lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">LHD login is currently unavailable</span></p>";
                            lblMsg.Visible = true;
                            Pwdstatus = false;
                        }
                        else {
                            Pwdstatus = true;
                            Session["AdminUserType"] = (String)reader["UserType"].ToString();
                            Session["AdminCoCode"] = (String)reader["CoCode"].ToString();
                            Session["AdminRegionCode"] = (String)reader["RegCode"].ToString();
                        }
                    }
                    else {
                        lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">Password incorrect</span></p>";
                        lblMsg.Visible = true;
                        Pwdstatus = false;
                    }
                }
                else {
                    lblMsg.Text = "<p align=\"center\"><span class=\"redbold\">Username not found</span></p>";
                    lblMsg.Visible = true;
                    Pwdstatus = false;
                }

                // work later 06/29/2016 AT
                //AdminLoginAuditTrail(Pwdstatus);
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
                reader = null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool Pwdstatus = false;
            //string Pwdstatus = "";

            CheckPassword(ref Pwdstatus);

            if (Pwdstatus == true)
            {
                Response.Redirect("AdminLoginConfirmed.aspx", true);
            }
            
        }
    }
}