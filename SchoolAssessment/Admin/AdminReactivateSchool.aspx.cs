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
using System.Web.UI.WebControls;
using System.Windows.Forms;


namespace SchoolAssessment.Admin
{
    public partial class AdminReactivateSchool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set to Yes if the school is active. 
            Session["AdminReactivateSchoolExist"] = "";

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
            
            lblMsg.Text = "<p><span class=\"redbold\"></span></p>";

            Page.Validate();
            if ((Page.IsValid))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                string sql = "SELECT * FROM Schools s where NOT EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.SchCode = '" + TxtSchCode.Text + "'";
                string sql_exist = "SELECT * FROM Schools s where EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.SchCode = '" + TxtSchCode.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlCommand cmd_exist = new SqlCommand(sql_exist, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (DoesSchoolExist() == false)
                {
                    // School does not eixt.
                    lblMsg.Text = "<p><span class=\"redbold\">School does not exist.  Please add school first. </span></p>";
                    lblMsg.Visible = true;
                    GridSchoolsToReactivate.Visible = false;

                }
                else
                {

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // School exist, but Non-Active.
                        lblMsg.Text = "<p><span class=\"redbold\">School is Active.</span></p>";
                        lblMsg.Visible = true;

                        // School is Active.  Do not show Reactivate button
                        Session["AdminReactivateSchoolExist"] = "Yes";

                        SqlDataAdapter da_exist = new SqlDataAdapter(cmd_exist);
                        DataSet ds_exist = new DataSet();
                        da_exist.Fill(ds_exist);
                        DataView dv_exist = new DataView(ds_exist.Tables[0]);
                        GridSchoolsToReactivate.DataSource = dv_exist;
                        GridSchoolsToReactivate.DataBind();
                    }
                    else
                    {
                        // School exist, And Active.
                        DataView dv = new DataView(ds.Tables[0]);
                        GridSchoolsToReactivate.DataSource = dv;
                        GridSchoolsToReactivate.DataBind();
                    }
                }
            }
        }

        protected void GridSchoolsToReactivate_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
                    sql2 = "INSERT INTO Assessments SELECT S.id, S.SchCode, '" + SchoolYear + "', 'N', '' , '', NULL, NULL, '', '', '', '', '', '', '', '','', 0, 0, 0, 0, 0, 0, '', NULL,NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, Getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0  FROM Schools S  WHERE NOT EXISTS (SELECT * FROM Assessments WHERE ID = s.id  and SchoolYear = '" + SchoolYear + "' ) AND S.EditDate > DATEADD(yy, DATEDIFF(yy,0,getdate()), 0) and id = '" + reactivate_id + "'";

                    cmd = new SqlCommand(sql, con);
                    cmd2 = new SqlCommand(sql2, con);
                    con.Open();

                    ret = cmd.ExecuteNonQuery();
                    ret2 = cmd2.ExecuteNonQuery();

                    if (ret > 0 && ret2 > 0)
                    {
                        lblMsg.Text = "<p><span class=\"greenbold\">School successfuly reactivated</a></span></p>";
                        lblMsg.Visible = true;
                    } else
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">Error: School was not reactivated</a></span></p>";
                        lblMsg.Visible = true;

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

        protected void GridSchoolsToReactivate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridSchoolsToReactivate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Show link to Original Report only for those reported. 
                if (Session["AdminReactivateSchoolExist"].ToString() == "Yes")
                {
                   
                    System.Web.UI.WebControls.Button btn = e.Row.FindControl("BtnReactivate") as System.Web.UI.WebControls.Button;
                    btn.Visible = false;

                    // Resetting Value
                    //Session["AdminReactivateSchoolExist"] = "";

                }
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (TxtSchCode.Text.Length == 7 || TxtSchCode.Text.Length == 9)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }
    }
}