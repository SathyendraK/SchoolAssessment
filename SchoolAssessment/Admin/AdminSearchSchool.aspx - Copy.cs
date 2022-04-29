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

/*
 *  06/29/2018 AT Created the AdminSearchSchool.aspx.cs  to be able to add schools cross cohort.  
 *                The user should search school first prior to add schools.
 * 
 * */


namespace SchoolAssessment.Admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddSchoolBtn.Visible = false;
            AddKSchoolBtn.Visible = false;
            Add7thSchoolBtn.Visible = false;
            Session["AdminAddSchoolCohort"] = "";            
            Session["AdminSchoolIsActive"] = ""; // Set to Yes if the school is active.


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
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            Page.Validate();
            if ((Page.IsValid))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                //string cohort = DropDownCohort.SelectedValue;
                string sql = "SELECT * FROM Schools WHERE SchCode = '" + TxtSchCode.Text + "'";
                string sql1 = "SELECT * FROM Schools s where NOT EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.SchCode = '" + TxtSchCode.Text + "'";



                try
                {
                  
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlCommand cmd1 = new SqlCommand(sql1, con);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School does not exist. Would you like to add this school? </span></p>";
                        lblMsg.Visible = true;
                        AddSchoolBtn.Visible = true;

                    }
                    else if (ds.Tables[0].Rows.Count == 1)
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School already exist. </span></p>";
                        lblMsg.Visible = true;

                        if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "S")
                        {
                            /* 7th Grade exists */
                            lblMsg.Text = "<p><span class=\"redbold\">7th Grade School already exist. </span></p>";
                            lblMsg.Visible = true;
                            AddKSchoolBtn.Visible = true;

                        }
                        else if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "K")
                        {
                            /* Kindergarten exists */
                            lblMsg.Text = "<p><span class=\"redbold\">Kindergarten School already exist. </span></p>";
                            lblMsg.Visible = true;
                            Add7thSchoolBtn.Visible = true;

                        }


                    }
                    else if (ds.Tables[0].Rows.Count > 1)
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
        }

        protected void AddSchoolBtn_Click(object sender, EventArgs e)
        {
            // No Cohort selected for new school
            Response.Redirect("AdminAddSchool.aspx?SchCode=" + TxtSchCode.Text, true);
            Session["AdminAddSchoolCohort"] = "";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Add7thSchoolBtn_Click(object sender, EventArgs e)
        {
            //Add 7th grade school
            Session["AdminAddSchoolCohort"] = "S";
            Response.Redirect("AdminAddSchool.aspx?SchCode=" + TxtSchCode.Text, true);
            
        }

        protected void AddKSchoolBtn_Click(object sender, EventArgs e)
        {
            //Add Kindergarten School
            Session["AdminAddSchoolCohort"] = "K";
            Response.Redirect("AdminAddSchool.aspx?SchCode=" + TxtSchCode.Text, true);
            
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            if (TxtSchCode.Text.Length == 7 || TxtSchCode.Text.Length == 9) { 
                args.IsValid = true;
            } else
            {
                args.IsValid = false;
            }
           
        }

        private bool DoesSchoolExist()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);


            dynamic schCode = TxtSchCode.Text;
            string sql = "SELECT COUNT(*) AS sch_num FROM Schools where SchCode ='" + schCode + "'";
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);

            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                return Convert.ToInt32(reader["sch_num"].ToString()) > 0;
                //return true;

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
                return false;
            }
        }


        
    }
}