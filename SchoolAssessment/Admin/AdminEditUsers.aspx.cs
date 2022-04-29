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

            if ((Request.QueryString["reason"] == "TimedOut"))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Session timed out. Please log in.";
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

            // Issue: Edit Admin Sorting function not working. 
            // Fixing on 05/21/2020
            Session["EditAdminUserSort"] = dv;

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
                string sql = null, sql2 = null;
                SqlCommand cmd = default(SqlCommand);
                SqlCommand cmd2 = default(SqlCommand);
                int ret = 0, ret2 = 0;



                try
                {
                    //CountyList.SelectedItem.Value = "04";
                    con.Open();
                    if ((!string.IsNullOrEmpty(CountyList.SelectedValue)))
                    {
                        //sql = "UPDATE AdminUsers set UserName = @UserName where CoCode = @CoCode";
                        //sql = "UPDATE AdminUsers SET UserName = @UserName FROM Counties C WHERE AdminUsers.CoCode = C.CoCode AND C.CoName = @CoName";
                        sql = "UPDATE AdminUsers SET UserName = @UserNameTo FROM Counties C WHERE C.CoName = @CoName AND UserName = @UserNameFrom";
                        cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@UserNameFrom", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@UserNameTo", txtEmailTo.Text);
                        //cmd.Parameters.AddWithValue("@CoName", CountyList.SelectedItem.Value); SelectedValue
                        cmd.Parameters.AddWithValue("@CoName", CountyList.SelectedValue); 
                         ret = cmd.ExecuteNonQuery();
                    }
                    
                    if ((!string.IsNullOrEmpty(RegionList.SelectedValue)))
                    {
                        //sql = "UPDATE AdminUsers set UserName = @UserName FROM Counties C where C.RegionCode = @RegionCode";
                        //sql = "UPDATE AdminUsers SET AdminUsers.RegCode = R.RegionCode FROM Regions R WHERE  R.RegionCode = @RegionCode AND UserName = @UserName";
                        sql2 = "UPDATE AdminUsers SET UserName = @UserNameTo FROM Regions R WHERE  R.RegCode = @RegionCode AND UserName = @UserNameFrom";
                        cmd2 = new SqlCommand(sql2, con);
                        cmd2.Parameters.AddWithValue("@UserNameFrom", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@UserNameTo", txtEmailTo.Text);
                        //cmd2.Parameters.AddWithValue("@CoName", CountyList.SelectedItem.Value);
                        //cmd2.Parameters.AddWithValue("@RegionCode", RegionList.SelectedItem.Value);
                        cmd2.Parameters.AddWithValue("@RegionCode", RegionList.SelectedValue);
                        ret2 = cmd2.ExecuteNonQuery();
                    }

                    if ((ret == 1 && !string.IsNullOrEmpty(CountyList.SelectedValue)) || 
                        (ret2 == 1 && !string.IsNullOrEmpty(RegionList.SelectedValue)) )
                    {
                        Response.Redirect("AdminEditUsers.aspx?Saved=1", true);
                    } else
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">" + "Not Updated - UserName From does not exist." + "</span></p>";
                        lblMsg.Visible = true;
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
                    cmd2 = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        protected void grdUsers_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            DataView dv = Session["EditAdminUserSort"] as DataView;

            if (dv.Sort == e.SortExpression)
            {
                dv.Sort = e.SortExpression + " DESC";
            }
            else if ((dv.Sort) == (e.SortExpression + " DESC"))
            {
                dv.Sort = e.SortExpression;
            }
            else
            {
                dv.Sort = e.SortExpression;
            }

            // trying these
            //grdVSchools.DataSource = dv;
            //grdVSchools.DataBind();



            //Session["7thRptSchoolsSort"] = dv.Sort;
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            //Session["7thRptSchools"] = dv;
            grdUsers.DataSource = dv;
            grdUsers.DataBind();

            Session["EditAdminUser_Sorted"] = "Y";
        }
    }
}