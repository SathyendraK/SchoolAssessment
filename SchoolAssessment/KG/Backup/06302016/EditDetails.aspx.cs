/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
*/

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;

namespace SchoolAssessment.KG
{
    public partial class EditDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                filldata();
            }
        }

        private void filldata()
        {
            dynamic id = Session["K_Assessment_id"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string sql = "SELECT S.*, C.CoName, ISNULL(D.DistName,'') AS 'DistName' FROM Schools S INNER JOIN Counties C on S.CoCode = C.CoCode LEFT OUTER JOIN Districts D ON S.DistCode = D.DistCode WHERE id = '" + id + "'";

            if (Page.IsPostBack == false)
            {
                SqlCommand cmd = new SqlCommand(sql, con);


                try
                {
                    //cmd.Connection = conn;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        /*
                        hdnSchCode.Value = (String)reader[2].ToString(); //SchCode
                        //txtSchoolCode.Text = (String)reader[2].ToString(); //SchCode
                        txtSchoolCode.Text = (String)reader["SchCode"].ToString(); //SchCode
                        txtSchoolName.Text = (String)reader[5].ToString(); //SchName
                        hdnschname.Value = (String)reader[5].ToString(); //SchName
                        txtSchoolType.Text = (String)reader[13].ToString(); //SchType
                        txtCounty.Text = (String)reader[23].ToString(); //Counties.CoName
                        txtDistrict.Text = (String)reader[24].ToString(); //Districts.DistName
                        txtPhyAddress.Text = (String)reader[6].ToString(); //PhysStreet
                        hdnphystreet.Value = (String)reader[6].ToString(); //PhysStreet
                        txtPhyCity.Text = (String)reader[7].ToString(); //PhysCity
                        hdnphycity.Value = (String)reader[7].ToString(); //PhysCity
                        txtPhyZip.Text = (String)reader[8].ToString(); //PhysZip
                        hdnphyzip.Value = (String)reader[8].ToString(); //PhysZip
                        txtMailAddress.Text = (String)reader[9].ToString(); //MailStreet
                        hdnmailstreet.Value = (String)reader[9].ToString(); //MailStreet
                        txtMailCity.Text = (String)reader[10].ToString(); //MailCity
                        hdnmailcity.Value = (String)reader[10].ToString(); //MailCity
                        txtMailZip.Text = (String)reader[11].ToString(); //MailZip
                        hdnmailzip.Value = (String)reader[11].ToString(); //MailZip
                        */

                        hdnSchCode.Value = (String)reader["SchCode"].ToString(); 
                        txtSchoolCode.Text = (String)reader["SchCode"].ToString(); //SchCode
                        txtSchoolName.Text = (String)reader["SchName"].ToString(); //SchName
                        hdnschname.Value = (String)reader["SchName"].ToString(); //SchName
                        txtSchoolType.Text = (String)reader["SchType"].ToString(); //SchType
                        txtCounty.Text = (String)reader["CoName"].ToString(); //Counties.CoName
                        txtDistrict.Text = (String)reader["DistName"].ToString(); //Districts.DistName
                        txtPhyAddress.Text = (String)reader["PhysStreet"].ToString(); //PhysStreet
                        hdnphystreet.Value = (String)reader["PhysStreet"].ToString(); //PhysStreet
                        txtPhyCity.Text = (String)reader["PhysCity"].ToString(); //PhysCity
                        hdnphycity.Value = (String)reader["PhysCity"].ToString(); //PhysCity
                        txtPhyZip.Text = (String)reader["PhysZip"].ToString(); //PhysZip
                        hdnphyzip.Value = (String)reader["PhysZip"].ToString(); //PhysZip
                        txtMailAddress.Text = (String)reader["MailStreet"].ToString(); //MailStreet
                        hdnmailstreet.Value = (String)reader["MailStreet"].ToString(); //MailStreet
                        txtMailCity.Text = (String)reader["MailCity"].ToString(); //MailCity
                        hdnmailcity.Value = (String)reader["MailCity"].ToString(); //MailCity
                        txtMailZip.Text = (String)reader["MailZip"].ToString(); //MailZip
                        hdnmailzip.Value = (String)reader["MailZip"].ToString(); //MailZip
                        TextSchEmail.Text = (String)reader["SchEmail"].ToString();
                        TextSchAdmin.Text = (String)reader["SchAdmin"].ToString();

                        // Add this later once Assessment table is created. -- 06/16/2016 AT
                        //hdnReviseDate.Value = reader("ReviseDate").ToString();
                        //hdnLhdReviseDate.Value = reader("LhdReviseDate").ToString();
                    }

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
                    //reader = null;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("LoginConfirmed.aspx", true);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                dynamic id = Session["K_Assessment_id"];
                string sql = null;
                //string curDate = System.DateTime.Now();

                try
                {
                    /*
                    if ((string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        sql = "UPDATE K_Assessment SET SchName = @FacilityName, PhysStreet = @FacilityPhysicalStreet, PhysCity = @FacilityPhysicalCity, PhysZip = @FacilityPhysicalZip, MailStreet = @FacilityMailingStreet, MailCity = @FacilityMailingCity, MailZip = @FacilityMailingZip, ReviseDate = GETDATE() where id = " + id;
                    }
                    else {
                        sql = "UPDATE K_Assessment SET SchName = @FacilityName, PhysStreet = @FacilityPhysicalStreet, PhysCity = @FacilityPhysicalCity, PhysZip = @FacilityPhysicalZip, MailStreet = @FacilityMailingStreet, MailCity = @FacilityMailingCity, MailZip = @FacilityMailingZip, LhdReviseDate = GETDATE() where id = " + id;
                    }
                    */

                    sql = "UPDATE Schools SET PhysStreet = @FacilityPhysicalStreet, PhysCity = @FacilityPhysicalCity, PhysZip = @FacilityPhysicalZip, MailStreet = @FacilityMailingStreet, MailCity = @FacilityMailingCity, MailZip = @FacilityMailingZip, EditDate = GETDATE(), schEmail = @SchEmail, SchAdmin = @SchAdmin where id = '" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@FacilityPhysicalStreet", txtPhyAddress.Text);
                    cmd.Parameters.AddWithValue("@FacilityPhysicalCity", txtPhyCity.Text);
                    cmd.Parameters.AddWithValue("@FacilityPhysicalZip", txtPhyZip.Text);
                    cmd.Parameters.AddWithValue("@FacilityMailingStreet", txtMailAddress.Text);
                    cmd.Parameters.AddWithValue("@FacilityMailingCity", txtMailCity.Text);
                    cmd.Parameters.AddWithValue("@FacilityMailingZip", txtMailZip.Text);
                    cmd.Parameters.AddWithValue("@SchEmail", TextSchEmail.Text);
                    cmd.Parameters.AddWithValue("@SchAdmin", TextSchAdmin.Text);
                    //cmd.Parameters.Add(new SqlParameter("@ReviseDate", SqlDbType.VarChar));
                    //cmd.Parameters.Add(new SqlParameter("@ReviseDate", SqlDbType.VarChar));
                    //cmd.Parameters.Add(new SqlParameter("@LhdReviseDate", SqlDbType.VarChar));

                    /* Commented out by AT on06/16/2016
                    // Under KG module, No admin is editing this page.
                    if ((string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        cmd.Parameters("@ReviseDate").Value = curDate;
                    }
                    cmd.Parameters("@LhdReviseDate").Value = hdnLhdReviseDate.Value;
                    if ((!string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        cmd.Parameters("@LhdReviseDate").Value = curDate;
                    }
                    */
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if ((hdnschname.Value != txtSchoolName.Text))
                    {
                        //logaudittrail("SchName", hdnschname.Value, txtSchoolName.Text);
                    }
                    //If (hdndistname.Value <> txtdist.Text) Then
                    //    logaudittrail("DistName", hdndistname.Value, txtdist.Text)
                    //End If
                    //If (hdncounty.Value <> txtCounty.Text) Then
                    //  logaudittrail("CountyName", hdncounty.Value, txtCounty.Text)
                    //End If
                    if ((hdnphystreet.Value != txtPhyAddress.Text))
                    {
                        //logaudittrail("PhysStreet", hdnphystreet.Value, txtPhyAddress.Text);
                    }
                    if ((hdnphycity.Value != txtPhyCity.Text))
                    {
                        //logaudittrail("PhysCity", hdnphycity.Value, txtPhyCity.Text);
                    }
                    if ((hdnphyzip.Value != txtPhyZip.Text))
                    {
                        //logaudittrail("PhysZip", hdnphyzip.Value, txtPhyZip.Text);
                    }
                    if ((hdnmailstreet.Value != txtMailAddress.Text))
                    {
                        //logaudittrail("MailStreet", hdnmailstreet.Value, txtMailAddress.Text);
                    }
                    if ((hdnmailcity.Value != txtMailCity.Text))
                    {
                        //logaudittrail("MailCity", hdnmailcity.Value, txtMailCity.Text);
                    }
                    if ((hdnmailzip.Value != txtMailZip.Text))
                    {
                        //logaudittrail("MailZip", hdnmailzip.Value, txtMailZip.Text);
                    }
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
                        Response.Redirect("LoginConfirmed.aspx", false);
                    }
                }
            }
        }
    }
}