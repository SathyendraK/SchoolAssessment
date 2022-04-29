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

namespace SchoolAssessment.CC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                dynamic id = Session["CC_Assessment_id"];
                string sql = "SELECT S.*, C.CoName, ISNULL(D.DistName, '') AS 'DistName', A.StudentYesNo, A.Reason, A.HomeSchl, A.VirtualSchl, A.isComplete FROM Schools S INNER JOIN Counties C on S.CoCode = C.CoCode LEFT OUTER JOIN Districts D ON S.DistCode = D.DistCode INNER JOIN Assessments A ON A.ID = S.id WHERE A.Assmntid = '" + id + "'";



                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        lblSchName.Text = (String)reader["SchName"].ToString();
                        //lblSchtype.Text = (String)reader["SchType"].ToString();
                        lblSchCode.Text = (String)reader["SchCode"].ToString();
                        lblCounty.Text = (String)reader["CoName"].ToString();
                        //lblDistrict.Text = (String)reader["DistName"].ToString();
                        txtPhyAddress.Text = (String)reader["PhysStreet"].ToString();
                        txtMailAddress.Text = (String)reader["MailStreet"].ToString();
                        txtPhyCity.Text = (String)reader["PhysCity"].ToString();
                        txtMailCity.Text = (String)reader["MailCity"].ToString();
                        txtPhyZip.Text = (String)reader["PhysZip"].ToString();
                        txtMailZip.Text = (String)reader["MailZip"].ToString();
                        hdnKinderYesNo.Value = (String)reader["StudentYesNo"].ToString();
                        hdnReason.Value = reader["Reason"].ToString();
                        hdnHome.Value = (String)reader["HomeSchl"].ToString();
                        hdnVirtual.Value = (String)reader["VirtualSchl"].ToString();
                        hdnIsComplete.Value = (String)reader["isComplete"].ToString();
                        //lblSchEmail.Text = (String)reader["schEmail"].ToString();
                        //txtSchEmail.Text = (String)reader["schEmail"].ToString();
                        hdnSchEmail.Value = (String)reader["schEmail"].ToString();
                        //lblSchAdmin.Text = (String)reader["schAdmin"].ToString();
                        hdndropdwnFType.Value = (String)reader["SchType"].ToString();


                        // Let only admin user be able to go next on reporting.
                        // Commented out on 08/10/2017, 06/19/2018, 06/27/2019
                        // Uncommented on 02/14/2018, 11/30/2018, 12/05/2019, 03/1/2021, 09/20/2021, 03/10/22
                        
                        if (Session["AdminUserType"].ToString() != "ADMIN" && Session["AdminUserType"].ToString() != "LHD") { 
                            ImgBtnNext.Enabled = false;
                            ImgBtnNext.Visible = false;
                        }
                        
                        //ImgBtnNext.Visible = false;
                        //ImgBtnNext.CssClass = "disabledImageButton";

                        if ((Page.IsPostBack == false))
                        {
                            drpdwnStudentYesNo.SelectedValue = (String)reader["StudentYesNo"].ToString();
                            drpReason.SelectedValue = reader["Reason"].ToString();
                            dropdwnFType.SelectedValue = (String)reader["SchType"].ToString();
                            txtSchEmail.Text = (String)reader["schEmail"].ToString();
                            //drphome.SelectedValue = (String)reader["HomeSchl"].ToString();
                            //drpvirtual.SelectedValue = (String)reader["VirtualSchl"].ToString();

                        }


                        if ((drpdwnStudentYesNo.SelectedValue == "No"))
                        {
                            lblConfirm.Visible = true;
                            drpReason.Enabled = true;
                            valReason.Enabled = true;
                            //drphome.Enabled = false;
                            //drpvirtual.Enabled = false;
                        }
                        else {
                            lblConfirm.Visible = false;
                            drpReason.SelectedValue = "";
                            drpReason.Enabled = false;
                            valReason.Enabled = false;
                            //drphome.Enabled = true;
                            //drpvirtual.Enabled = true;
                        }
                    }


                    if ((Page.IsPostBack == true & hdnIsComplete.Value != "Y"))
                    {
                        drpReason.Focus();
                    }


                    if ((hdnIsComplete.Value == "Y"))
                    {
                        // Below used to not be editable once reported, but now always editable with Revise your Submitted Report
                        // Commented out by A.T. on 09/16/2014
                        // drpdwnStudentYesNo.Enabled = False
                        // drpReason.Enabled = False
                        // drphome.Enabled = False
                        // drpvirtual.Enabled = False
                        // valReason.Enabled = False
                        // lblConfirm.Visible = False
                        btnPrintView.Enabled = true;
                        //btnConfirm.Enabled = false; Commented out by AT on 07/06/2016 
                        // Added below by A.T. on 09/04/2014
                        //btnReset.Enabled = true; --> btnReset moved to ViewAndPrint.aspx

                    }
                    else {
                        btnExit.Attributes.Add("onclick", "return confirm('You have not completed this report yet. \\nClick CANCEL or NO to stay logged in.\\n\\nTo complete this report, you must CONFIRM AND CONTINUE and then SUBMIT the Assessment Report page.')");
                        btnPrintView.Enabled = false;
                        // Added below by A.T. on 09/04/2014
                        //btnReset.Enabled = false; --> btnReset moved to ViewAndPrint.aspx
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
                    }
                    //reader = null;
                }
            }
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session.Remove("CC_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Save();
                Response.Redirect("EditDetails.aspx", true);
            }
        }

        // Save data to DB
        // logaudittrail not working yet
        private void Save()
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            dynamic id = Session["CC_Assessment_id"];
            string sql = "", sql2 = "";
            SqlCommand cmd = new SqlCommand(sql, con);
            DateTime curDateTime = DateTime.Now;

            int ret = 0, ret2 = 0;


            try
            {

                if ((drpdwnStudentYesNo.SelectedValue == "Yes"))
                {

                    sql = "UPDATE Assessments SET StudentYesNo = 'Yes', Reason = NULL, EditDate = GetDate()  WHERE Assmntid = '" + id + "'";
                    cmd = new SqlCommand(sql, con);
                    //cmd.Parameters.AddWithValue("@VirtualSchl", drpvirtual.SelectedValue);
                    //cmd.Parameters.AddWithValue("@HomeSchl", drphome.SelectedValue);
                    //cmd.Parameters.AddWithValue("@SchType", dropdwnFType.SelectedValue);
                }
                else {

                    sql = "UPDATE Assessments SET StudentYesNo = 'No', Reason = @Reason, HomeSchl = 'No', VirtualSchl = 'No', EditDate = GetDate() WHERE Assmntid = '" + id + "'";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Reason", drpReason.SelectedValue);
                    //cmd.Parameters.AddWithValue("@SchType", dropdwnFType.SelectedValue);
                }

                sql2 = "UPDATE Schools SET SchType = @schType, SchEmail = @SchEmail, EditDate = @EditDate WHERE id IN (SELECT id FROM Assessments WHERE Assmntid = '" + id + "')";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.Parameters.AddWithValue("@schType", dropdwnFType.SelectedValue);
                cmd2.Parameters.AddWithValue("@SchEmail", txtSchEmail.Text);
                cmd2.Parameters.AddWithValue("@EditDate", curDateTime);


                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                ret = cmd.ExecuteNonQuery();
                ret2 = cmd2.ExecuteNonQuery();

                if (ret == 1 && ret2 == 1)
                {

                    if ((hdndropdwnFType.Value != dropdwnFType.SelectedValue))
                    {
                        logaudittrail("SchType", hdndropdwnFType.Value, dropdwnFType.SelectedValue);
                    }

                    if (hdnSchEmail.Value != txtSchEmail.Text)
                    {
                        logaudittrail("SchEmail", hdnSchEmail.Value, txtSchEmail.Text);
                    }

                    if ((drpdwnStudentYesNo.SelectedValue == "Yes"))
                    {

                        if ((hdnKinderYesNo.Value != drpdwnStudentYesNo.SelectedValue))
                        {
                            logaudittrail("StudentYesNo", hdnKinderYesNo.Value, drpdwnStudentYesNo.SelectedValue);
                        }
                        if ((!string.IsNullOrEmpty(hdnReason.Value)))
                        {
                            logaudittrail("Reason", hdnReason.Value, "");
                        }
                        
                    }
                    else {

                        if ((hdnKinderYesNo.Value != drpdwnStudentYesNo.SelectedValue))
                        {
                            logaudittrail("StudentYesNo", hdnKinderYesNo.Value, drpdwnStudentYesNo.SelectedValue);
                        }
                        if ((hdnReason.Value != drpReason.SelectedValue))
                        {
                            logaudittrail("Reason", hdnReason.Value, drpReason.SelectedValue);
                        }
                        if ((hdnHome.Value != "No"))
                        {
                            logaudittrail("HomeSchl", hdnHome.Value, "No");
                        }
                        if ((hdnVirtual.Value != "No"))
                        {
                            logaudittrail("VirtualSchl", hdnVirtual.Value, "No");
                        }
                    }
                }
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



        private void logaudittrail(string fieldname, string fromval, string toval)
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            dynamic id = Session["CC_Assessment_id"];
            string sql = null;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {

                sql = "INSERT INTO AuditTrail(SchCode, ScreenName, FieldName, FromValue, ToValue, IP, TimeStamp) values (@SchoolCode, @ScreenName, @FieldName, @FromValue, @ToValue, @IP, GetDate())";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@SchoolCode", lblSchCode.Text);
                cmd.Parameters.AddWithValue("@ScreenName", "LoginConfirmed");
                cmd.Parameters.AddWithValue("@FieldName", fieldname);
                cmd.Parameters.AddWithValue("@FromValue", fromval);
                cmd.Parameters.AddWithValue("@ToValue", toval);
                cmd.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);

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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        protected void btnPrintView_Click(object sender, EventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Save();
                Response.Redirect("ViewAndPrint.aspx", true);
            }
        }

        protected void hdrLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CC_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void ImgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Save();
                Response.Redirect("ReportContactInfo.aspx", true);
            }
        }



    }
}