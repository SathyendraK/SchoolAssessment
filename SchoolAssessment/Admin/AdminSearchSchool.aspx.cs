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


            if (Page.IsPostBack == true)
            {
                lblMsg1.Visible = false;
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
                string sql2 = "SELECT * FROM Schools WHERE Cohort = 'S' AND SchCode = '" + TxtSchCode.Text + "'";
                string sql3 = "SELECT * FROM Schools WHERE Cohort = 'K' AND SchCode = '" + TxtSchCode.Text + "'";
                string sql1 = "SELECT * FROM Schools s where NOT EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.SchCode = '" + TxtSchCode.Text + "'";
                string sql_exist = "SELECT * FROM Schools s where EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.SchCode = '" + TxtSchCode.Text + "'";



                try
                {
                  
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    SqlCommand cmd_exist = new SqlCommand(sql_exist, con);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);

                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School does not exist. Would you like to add this school? </span></p>";
                        lblMsg.Visible = true;
                        AddSchoolBtn.Visible = true;

                    }
                    //else if (ds.Tables[0].Rows.Count == 1)
                    //else if (ds.Tables[0].Rows.Count > 0)
                    else if (ds2.Tables[0].Rows.Count > 0 || ds3.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School already exist. </span></p>";
                        lblMsg.Visible = true;

                        //if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "S")
                        //if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "S" || ds.Tables[0].Rows[0]["Cohort"].ToString() == "E")
                        if (ds2.Tables[0].Rows.Count == 0)
                        {
                            /* 7th Grade exists *//*
                            lblMsg.Text = "<p><span class=\"redbold\">7th/8th Grade School already exist. </span></p>";
                            lblMsg.Visible = true;
                            AddKSchoolBtn.Visible = true; */

                            /* 7th Grade does not exists */
                            lblMsg.Text = "<p><span class=\"redbold\">7th/8th Grade School does not exist. </span></p>";
                            lblMsg.Visible = true;
                            Add7thSchoolBtn.Visible = true;

                        }
                        //else if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "K")
                        //else if (ds.Tables[0].Rows[0]["Cohort"].ToString() == "K" || ds.Tables[0].Rows[0]["Cohort"].ToString() == "F")
                        else if (ds3.Tables[0].Rows.Count == 0)
                        {
                            /* Kindergarten exists *//*
                            lblMsg.Text = "<p><span class=\"redbold\">Kindergarten/First Grade School already exist. </span></p>";
                            lblMsg.Visible = true;
                            Add7thSchoolBtn.Visible = true; */

                            /* Kindergarten exists */
                            lblMsg.Text = "<p><span class=\"redbold\">Kindergarten/First Grade School does not exist. </span></p>";
                            lblMsg.Visible = true;
                            AddKSchoolBtn.Visible = true;

                        }
                    }
                    //else if (ds.Tables[0].Rows.Count > 1)
                    //else if (ds.Tables[0].Rows.Count > 2)
                    else if (ds2.Tables[0].Rows.Count == 1 && ds3.Tables[0].Rows.Count == 1)
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School already exist. </span></p>";
                        lblMsg.Visible = true;
                    }


                    if (ds.Tables[0].Rows.Count != 0) { 
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            // School exist, but Non-Active.
                            lblMsg.Text = "<p><span class=\"redbold\">School is Active.</span></p>";
                            lblMsg.Visible = true;

                            // School is Active.  Do not show Reactivate button
                            Session["AdminSchoolIsActive"] = "Yes";

                            SqlDataAdapter da_exist = new SqlDataAdapter(cmd_exist);
                            DataSet ds_exist = new DataSet();
                            da_exist.Fill(ds_exist);
                            DataView dv_exist = new DataView(ds_exist.Tables[0]);
                            GridSchoolsToSearch.DataSource = dv_exist;
                            GridSchoolsToSearch.DataBind();
                        }
                        else
                        {
                            // School exist, And Active.
                            DataView dv = new DataView(ds1.Tables[0]);
                            GridSchoolsToSearch.DataSource = dv;
                            GridSchoolsToSearch.DataBind();
                        }
                    }

                    /*
                    DataView dv = new DataView(ds.Tables[0]);
                    GridSchoolsToSearch.DataSource = dv;
                    GridSchoolsToSearch.DataBind();
                    */

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

        protected void GridSchoolsToSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reactivate")
            {
                int reactivate_id = Convert.ToInt32(e.CommandArgument);
                string sql = "", sql2 = "";
                SqlCommand cmd = default(SqlCommand);
                SqlCommand cmd2 = default(SqlCommand);
                int ret = 0, ret2 = 0;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];

                try
                {
                    sql = "UPDATE schools SET EditDate = getdate() WHERE id = '" + reactivate_id + "'";
                    sql2 = "INSERT INTO Assessments SELECT S.id, S.SchCode, '" + SchoolYear + "', 'N', '' , '', NULL, NULL, '', '', '', '', '', '', '', '','', 0, 0, 0, 0, 0, 0, '', NULL,NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, Getdate()  FROM Schools S  WHERE NOT EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.EditDate > DATEADD(yy, DATEDIFF(yy,0,getdate()), 0) and id = '" + reactivate_id + "'";

                    cmd = new SqlCommand(sql, con);
                    cmd2 = new SqlCommand(sql2, con);
                    con.Open();

                    ret = cmd.ExecuteNonQuery();
                    ret2 = cmd2.ExecuteNonQuery();

                    if (ret > 0 && ret2 > 0)
                    {
                        lblMsg1.Text = "<p><span class=\"greenbold\">School successfuly reactivated</a></span></p>";
                        lblMsg1.Visible = true;
                    }
                    else
                    {
                        lblMsg1.Text = "<p><span class=\"redbold\">Error: School was not reactivated</a></span></p>";
                        lblMsg1.Visible = true;

                    }

                    btnSearch_Click(sender, e);

                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    Response.Write(ex.Message);
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

        protected void GridSchoolsToSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Show link to Original Report only for those reported. 
                if (Session["AdminSchoolIsActive"].ToString() == "Yes")
                {

                    System.Web.UI.WebControls.Button btn = e.Row.FindControl("BtnReactivate") as System.Web.UI.WebControls.Button;
                    btn.Visible = false;

                    // Resetting Value
                    //Session["AdminReactivateSchoolExist"] = "";

                }
            }
        }
    }
}