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
using System.Web;



namespace SchoolAssessment.KG
{
    public partial class ReportContactInfo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }

            if ((Page.IsPostBack == false))
            {
                dynamic id = Session["K_Assessment_id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string sql = "SELECT S.*, C.CoName, A.StudentYesNo, A.Reason, A.ReportedPerson, A.ReportedPhone, A.ReportedPhoneExt, A.ReportedEmail, A.IsComplete FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode WHERE S.id = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";

                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        // if report is already completed, send to view report page
                        if ((String)reader["isComplete"].ToString() == "Y")
                        {
                            /*
                            // Added check so that if user is revising the submitted report, you will not be redirected by A.T. on 09/16/2014
                            if ((reader["ReviseSubmittedRPT"].ToString() != "Y"))
                            {
                                Response.Redirect("ViewReportInfo.aspx", true);
                            }
                            */
                        }
                        


                        // if somehow user selected no students and did not select a reason
                        // but still got to this page, send back to first page
                        if ((reader["StudentYesNo"].ToString() == "No" & string.IsNullOrEmpty(reader["Reason"].ToString())))
                        {
                            Response.Redirect("LoginConfirmed.aspx", true);
                        }

                        // fill hidden fields
                        hdnSchCode.Value = reader["SchCode"].ToString();

                        // Pre-fill form values
                        lblSchName.Text = (String)reader["SchName"].ToString();
                        lblSchCode.Text = (String)reader["SchCode"].ToString();

                        // Reporting person
                        txtStaffPrsn.Text = (String)reader["ReportedPerson"].ToString();
                        txtStaffPhNo.Text = (String)reader["ReportedPhone"].ToString();
                        txtStaffPhNoExt.Text = (String)reader["ReportedPhoneExt"].ToString();
                        txtmail.Text = (String)reader["ReportedEmail"].ToString();
                        txtconfirmmail.Text = (String)reader["ReportedEmail"].ToString();
                        


                        // Commented out by A.T. on 08/27/2014
                        //txtpin.Text = Trim(reader("Password").ToString())
                        //txtcpin.Text = Trim(reader("Password").ToString())


                        // Stamp EditDate, or today's date
                        //txtDate.Text = System.DateTime.Now().ToString("d");
                        
                        // If form has been reset before, show Form Submitter values
                        //if ((!string.IsNullOrEmpty(reader["LastResetDate"].ToString())))
                        //{
                            txtfcperson.Text = (String)reader["ContactPerson"].ToString();
                            txtfcNumber.Text = (String)reader["ContactPhone"].ToString();
                            txtfcNumberExt.Text = (String)reader["ContactPhoneExt"].ToString();
                            txtfcemail.Text = (String)reader["ContactEmail"].ToString();
                            txtfccemail.Text = (String)reader["ContactEmail"].ToString();
                        //}

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

            
            // Addded by AT on 10/22/2014 to maintain tab order after PostBack 
            /*
            if (Page.IsPostBack)
            {
                WebControl wcICausedPostBack = (WebControl)GetControlThatCausedPostBack(sender as Page);
                int indx = wcICausedPostBack.TabIndex;
                dynamic ctrl = from control in wcICausedPostBack.Parent.Controls.OfType<WebControl>() where control.TabIndex > indxcontrol;
                ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus();
            }

            //Added below by A.T. on 08/26/2014
            //txtPerBelExmp.Text = (Val(TxtPreJanuaryExmpt.Text) + Val(TxtHealthCareExmpt.Text) + Val(TxtReligiousExmpt.Text)).ToString
            txtPerBelExmp.Text = (Conversion.Val(TxtHealthCareExmpt.Text) + Conversion.Val(TxtReligiousExmpt.Text)).ToString;
            */

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else
            {
                // Enable this later 06/22/2016 AT
                Save();
                Response.Redirect("LoginConfirmed.aspx", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {

                Page.Validate();
                if ((Page.IsValid))
                {
                    Save();
                    Response.Redirect("SubmitReport.aspx", true); // Added 06/29/2016
                }
            }

        }

        private void Save()
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {

                dynamic id = Session["K_Assessment_id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                //string sql = "SELECT S.*, C.CoName, A.FormPerson, A.FormPhone, A.FormPhoneExt, A.FormEmail, A.IsComplete FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode WHERE S.id = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";
                int ret = 0, ret2 = 0;
                string sql = "", sql2 = "";

                //string reviseDate = System.DateTime.Now();

                try
                {
                    /*
                    if ((string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        // Removed PBE_PreJanuaryExmpt by AT on 06/01/2015 
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, ReviseDate = @ReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_PreJanuaryExmpt = @PBE_PreJanuaryExmpt, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt  WHERE id = @id"
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, ReviseDate = @ReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt, EnrolledButNotAttending = @EnrolledButNotAttending  WHERE id = @id";
                        sql = "UPDATE Assessments SET EditDate = GetDate(), ReviseDate = @ReviseDate, ReportedPerson = @FormPerson, ReportedPhone = @FormPhone, ReportedPhoneExt = @FormPhoneExt, ReportedEmail = @FormEmail WHERE id = '" + id + "' AND SchoolYear = '" + SchoolYear + "' ";
                    }
                    else {
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, LhdReviseDate = @LhdReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_PreJanuaryExmpt = @PBE_PreJanuaryExmpt, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt WHERE id = @id"
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, LhdReviseDate = @LhdReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt, EnrolledButNotAttending = @EnrolledButNotAttending WHERE id = @id";
                        sql = "UPDATE Assessments SET EditDate = GetDate(), LhdReviseDate = @LhdReviseDate, ReportedPerson = @FormPerson, ReportedPhone = @FormPhone, ReportedPhoneExt = @FormPhoneExt, ReportedEmail = @FormEmail WHERE id = '" + id + "' AND SchoolYear = '" + SchoolYear + "' ";
                    }
                    */

                    sql = "UPDATE Assessments SET EditDate = GetDate(), ReportedPerson = @FormPerson, ReportedPhone = @FormPhone, ReportedPhoneExt = @FormPhoneExt, ReportedEmail = @FormEmail WHERE id = '" + id + "' AND SchoolYear = '" + SchoolYear + "' ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@FormPerson", txtStaffPrsn.Text);
                    cmd.Parameters.AddWithValue("@FormPhone", txtStaffPhNo.Text);
                    cmd.Parameters.AddWithValue("@FormPhoneExt", txtStaffPhNoExt.Text);
                    cmd.Parameters.AddWithValue("@FormEmail", txtmail.Text);

                    sql2 = "UPDATE Schools SET EditDate = GetDate(), ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail WHERE id = '" + id + "'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@ContactPerson", txtfcperson.Text);
                    cmd2.Parameters.AddWithValue("@ContactPhone", txtfcNumber.Text);
                    cmd2.Parameters.AddWithValue("@ContactPhoneExt", txtfcNumberExt.Text);
                    cmd2.Parameters.AddWithValue("@ContactEmail", txtfcemail.Text);
                    
                    /*
                    cmd.Parameters("@id").Value = Session["K_Assessment_id"];
                    if ((string.IsNullOrEmpty(hdnSubmitDate.Value)))
                    {
                        cmd.Parameters("@SubmitDate").Value = reviseDate;
                    }
                    else {
                        cmd.Parameters("@SubmitDate").Value = hdnSubmitDate.Value;
                    }
                    */

                    //cmd.Parameters("@ReviseDate").Value = hdnReviseDate.Value;

                    /*  Looks like I need to take care later 06/22/2016 AT
                    if ((string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        cmd.Parameters("@ReviseDate").Value = reviseDate;
                    }

                    cmd.Parameters("@LhdReviseDate").Value = hdnLhdReviseDate.Value;
                    if ((!string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        cmd.Parameters("@LhdReviseDate").Value = reviseDate;
                    }
                    */

                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    ret2 = cmd2.ExecuteNonQuery();
                    if ((ret == 1) && (ret2 == 1))
                    {
                        
                        logaudittrail("isComplete", hdnIsComplete.Value, "Y");

                        
                        if ((hdnFormPerson.Value != txtStaffPrsn.Text))
                        {
                            logaudittrail("FormPerson", hdnFormPerson.Value, txtStaffPrsn.Text);
                        }
                        if ((hdnFormEmail.Value != txtmail.Text))
                        {
                            logaudittrail("FormEmail", hdnFormEmail.Value, txtmail.Text);
                        }
                        if ((hdnFormPhone.Value != txtStaffPhNo.Text))
                        {
                            logaudittrail("FormPhone", hdnFormPhone.Value, txtStaffPhNo.Text);
                        }
                        if ((hdnFormPhoneExt.Value != txtStaffPhNoExt.Text))
                        {
                            logaudittrail("FormPhoneExt", hdnFormPhoneExt.Value, txtStaffPhNoExt.Text);
                        }
                        if ((hdnContactPerson.Value != txtfcperson.Text))
                        {
                            logaudittrail("ContactPerson", hdnContactPerson.Value, txtfcperson.Text);
                        }
                        if ((hdnContactEmail.Value != txtfcemail.Text))
                        {
                            logaudittrail("ContactEmail", hdnContactEmail.Value, txtfcemail.Text);
                        }
                        if ((hdnContactPhone.Value != txtfcNumber.Text))
                        {
                            logaudittrail("ContactPhone", hdnContactPhone.Value, txtfcNumber.Text);
                        }
                        if ((hdnContactPhoneExt.Value != txtfcNumberExt.Text))
                        {
                            logaudittrail("ContactPhoneExt", hdnContactPhoneExt.Value, txtfcNumberExt.Text);
                        }
                        
                        
                        // Commented out by A.T. on 08/27/2014
                        //If (hdnPassword.Value <> txtpin.Text) Then
                        //logaudittrail("Password", hdnPassword.Value, txtpin.Text)
                        //End If

                        //Response.Redirect("SubmitReport.aspx", true);
                    }
                    else {
                        ErrorMsg.Text = "<p><span class=\"redbold\">Oops! There was a database problem. Try again later or report the error to reporting-help@shotsforschool.org</span></p>";
                    }
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    Response.Write(ex.Message);
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

        protected void chkcontact_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcontact.Checked == true)
            {
                txtfcperson.Text = txtStaffPrsn.Text;
                txtfcNumber.Text = txtStaffPhNo.Text;
                txtfcemail.Text = txtmail.Text;
                txtfccemail.Text = txtconfirmmail.Text;
                txtfcNumberExt.Text = txtStaffPhNoExt.Text;
                chkcontact.Focus();
            }
            if (chkcontact.Checked == false)
            {
                txtfcperson.Text = "";
                txtfcNumber.Text = "";
                txtfcNumberExt.Text = "";
                txtfcemail.Text = "";
                txtfccemail.Text = "";
                txtfcperson.Focus();
            }
        }

        private void logaudittrail(string fieldname, string fromval, string toval)
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            dynamic id = Session["K_Assessment_id"];
            string sql = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {

                sql = "INSERT INTO AuditTrail(SchCode, ScreenName, FieldName, FromValue, ToValue, IP, TimeStamp) values (@SchoolCode, @ScreenName, @FieldName, @FromValue, @ToValue, @IP, GetDate())";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@SchoolCode", lblSchCode.Text);
                cmd.Parameters.AddWithValue("@ScreenName", "ReportContactInfo");
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
    }
}