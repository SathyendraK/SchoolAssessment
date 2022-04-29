using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI.WebControls;  //ServerValidateEventArgs

/* 
 * 08/27/2014 Ayumi Taniguchi Adding TxtPreJanuaryExmpt
 * 08/27/2014 Ayumi Taniguchi Adding TxtHealthCareExmpt
 * 08/27/2014 Ayumi Taniguchi Adding TxtReligiousExmpt
 * 08/18/2015 Ayumi Taniguchi Adding TextEnrolledButNotAttending
 * 08/2016    Ayumi Taniguchi upgraded from Visual Basic to C#
 * 05/03/2018 Ayumi Taniguchi Adding Facility Status in ViewAndPrint page  
 * 
 */



namespace SchoolAssessment.KG
{
    public partial class ViewAndPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                //btnprint.Attributes.Add("onclick", "window.print()");
                //ImgBtnPrintRPT.Attributes.Add("onclick", "window.print()");
                FillInData();
            }

        }


        private void FillInData()
        {

            dynamic id = Session["K_Assessment_id"];
            dynamic id_Fgrade = Session["F_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            string sql = "SELECT S.*, C.CoName, A.*, D.*  FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = '" + id + "'";
            string sql_f = "SELECT S.*, C.CoName, A.*, D.*  FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = '" + id_Fgrade + "'";


            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    System.DateTime theDate = (Convert.ToDateTime(reader["SubmitDate"]));

                    // If report not submitted yet, redirect to report submission page
                    if ((Page.IsPostBack == false))
                    {
                        string isComplete = reader["isComplete"].ToString();
                        if ((isComplete != "Y"))
                        {
                            Response.Redirect("ReportInfoNew.aspx", true);
                            // Adding logic for Revise your Submitted Report. IsComplete == Y
                            //ElseIf (IsNothing(theDate) <> "") Then
                            //Response.Redirect("ReportInfoNew.aspx", True)
                        }
                    }

                    lblSchName.Text = reader["SchName"].ToString();
                    lblSchCode.Text = reader["SchCode"].ToString();
                    lblcounty.Text = reader["CoName"].ToString();
                    lbldistrict.Text = reader["DistName"].ToString();
                    lblpin.Text = reader["Password"].ToString();
                    lblSchCode.Text = reader["SchCode"].ToString();
                    lblstype.Text = reader["SchType"].ToString();
                    lblmaddress.Text = reader["MailStreet"].ToString() + ", " + reader["MailCity"].ToString() + ", " + reader["MailZip"].ToString();
                    lblpaddress.Text = reader["PhysStreet"].ToString() + ", " + reader["PhysCity"].ToString() + ", " + reader["PhysZip"].ToString();
                    //Dim theDate As Date = Trim(reader["SubmitDate"])
                    //txtDate.Text = theDate.ToString();// "d"];
                    lblstaffPrsn.Text = reader["ReportedPerson"].ToString();
                    //lbsStaffPhone.Text = reader["ReportedPhone"].ToString();
                    lbsStaffPhone.Text = reader["ReportedPhone"].ToString().Substring(0,3) + "-" + reader["ReportedPhone"].ToString().Substring(3, 3) + "-" + reader["ReportedPhone"].ToString().Substring(6);
                    lblStaffPhoneExt.Text = reader["ReportedPhoneExt"].ToString();
                    LblStaffEmail.Text = reader["ReportedEmail"].ToString();
                    lblDesContactName.Text = reader["ContactPerson"].ToString();
                    //lblDesContactPhone.Text = reader["ContactPhone"].ToString();
                    lblDesContactPhone.Text = reader["ContactPhone"].ToString().Substring(0,3) + "-" + reader["ContactPhone"].ToString().Substring(3, 3) + "-" + reader["ContactPhone"].ToString().Substring(6);
                    lblDesPhoneExt.Text = reader["ContactPhoneExt"].ToString();
                    lblDesContactEmail.Text = reader["ContactEmail"].ToString();
                    txtAllimm.Text = reader["AllImm"].ToString();
                    txtPermMedExmp.Text = reader["MedExmp"].ToString();
                    //txtBelExmp.Text = reader["BeleExmp"].ToString();
                    txtNoimm.Text = reader["NoImm"].ToString();
                    txttotno.Text = reader["TotNo"].ToString();
                    txtPolio.Text = reader["Polio"].ToString();
                    txtDtp.Text = reader["DTP_DTAP_DT"].ToString();
                    txtMMR2.Text = reader["MMRDose2"].ToString();
                    txtHepb.Text = reader["HepB"].ToString();
                    txtVZV.Text = reader["VZV"].ToString();
                    lblSchAdmin.Text = reader["SchAdmin"].ToString();
                    lblSchEmail.Text = reader["SchEmail"].ToString();
                    TextIEPServices.Text = reader["IEPService"].ToString();
                    TextIndependentStudy.Text = reader["IndependentStudy"].ToString();
                    TextHomeBasedPrivate.Text = reader["HomeBasedPrivate"].ToString();
                    TextMedExmption.Text = reader["TempMedExemption"].ToString();
                    TextTotal.Text = txttotno.Text;
                    hdnIsComplete.Value = reader["isComplete"].ToString();
                    lblSubmittedDate.Text = reader["SubmitDate"].ToString();
                    lblRevisedDate.Text = reader["ReviseDate"].ToString();

                    // Let only Admin can Revise the Report. 
                    // Commented out on 08/10/2017, 06/19/2018, 06/27/2019, 05/21/2020, 08/03/2021
                    // Uncommented on 02/14/2018, 11/30/2018, 12/05/2019, 03/16/2021, 03/10/22
                    
                    if (Session["AdminUserType"].ToString() != "ADMIN")
                    {
                        ImgBtnReviseYourRPT.Enabled = false;
                        ImgBtnReviseYourRPT.Visible = false;
                    }
                    
                    

                    // Added below by A.T. on 08/27/2014
                    //TxtPreJanuaryExmpt.Text = Trim(reader["PBE_PreJanuaryExmpt"].ToString()) 'Commented out by AT on 06/01/2015
                    //TxtHealthCareExmpt.Text = reader["PBE_HealthCareExmpt"].ToString());
                    //TxtReligiousExmpt.Text = reader["PBE_ReligiousExmpt"].ToString());

                    if (txtPermMedExmp.Text == "") { txtPermMedExmp.Text = "0"; }
                    //if (txtBelExmp.Text == "") { txtBelExmp.Text = "0"; }
                    if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }
                    if (txtNoimm.Text == "") { txtNoimm.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }
                    if (TextMedExmption.Text == "") { TextMedExmption.Text = "0"; }
                    if (TextEnrolledButNotAttending.Text == "") { TextEnrolledButNotAttending.Text = "0"; }
                    if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }

                    if (reader["StudentYesNo"].ToString() == "Yes")
                    {
                        lblStatus.Text = "Active";

                    }
                    else if (reader["StudentYesNo"].ToString() == "No")
                    {
                        lblStatus.Text = reader["Reason"].ToString();
                    }

                    // Added below by A.T. on 08/18/2015
                    TextEnrolledButNotAttending.Text = reader["EnrolledButNotAttending"].ToString();
                    TextMissingDosesTotal.Text = (Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)).ToString();
                    HdnTextMissingDosesTotal.Value = TextMissingDosesTotal.Text;
                    //TxtOthersTotal.Text = (Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text)).ToString();


                    //txtDate.Enabled = false;
                    lblstaffPrsn.Enabled = false;
                    lbsStaffPhone.Enabled = false;
                    lblStaffPhoneExt.Enabled = false;
                    LblStaffEmail.Enabled = false;
                    lblDesContactName.Enabled = false;
                    lblDesContactPhone.Enabled = false;
                    lblDesPhoneExt.Enabled = false;
                    lblDesContactEmail.Enabled = false;
                    txtAllimm.Enabled = false;
                    txtPermMedExmp.Enabled = false;
                    //txtBelExmp.Enabled = false;
                    txtNoimm.Enabled = false;
                    txttotno.Enabled = false;
                    txtPolio.Enabled = false;
                    txtDtp.Enabled = false;
                    txtMMR2.Enabled = false;
                    txtHepb.Enabled = false;
                    txtVZV.Enabled = false;
                    TextIEPServices.Enabled = false;
                    TextIndependentStudy.Enabled = false;
                    TextHomeBasedPrivate.Enabled = false;
                    TextMedExmption.Enabled = false;
                    TextTotal.Enabled = false;
                    lblSubmittedDate.Enabled = false;
                    lblRevisedDate.Enabled = false;
                    TextMissingDosesTotal.Enabled = false;
                    //TxtOthersTotal.Enabled = false;


                    // Added below by A.T. on 08/27/2014
                    //TxtPreJanuaryExmpt.Enabled = False 'Commented out by AT on 06/01/2015
                    //TxtHealthCareExmpt.Enabled = false;
                    //TxtReligiousExmpt.Enabled = false;

                    // Added below by A.T. on 08/18/2015
                    TextEnrolledButNotAttending.Enabled = false;

                    //if ((int)reader["SubmittedYear"] < 2016)
                    if (reader["Schoolyear"].ToString() != SchoolYear)
                    {
                        ImgBtnReviseYourRPT.Enabled = false;
                        ImgBtnReviseYourRPT.Visible = false;
                    }

                }


                // Get StudentYesNo for 1st Grader
                reader.Close();
                SqlCommand cmd_f = new SqlCommand(sql_f, con);
                SqlDataReader reader_f = cmd_f.ExecuteReader();

                while (reader_f.Read())
                {
                    if (reader_f["StudentYesNo"].ToString() == "Yes")
                    {
                        lblStatus_1st.Text = "Active";

                    }
                    else if (reader_f["StudentYesNo"].ToString() == "No")
                    {
                        lblStatus_1st.Text = reader_f["Reason"].ToString();
                    }

                    txtAllimm_1st.Text = reader_f["AllImm"].ToString();
                    txtPermMedExmp_1st.Text = reader_f["MedExmp"].ToString();
                    txtNoimm_1st.Text = reader_f["NoImm"].ToString();
                    txttotno_1st.Text = reader_f["TotNo"].ToString();
                    txtPolio_1st.Text = reader_f["Polio"].ToString();
                    txtDtp_1st.Text = reader_f["DTP_DTAP_DT"].ToString();
                    txtMMR2_1st.Text = reader_f["MMRDose2"].ToString();
                    txtHepb_1st.Text = reader_f["HepB"].ToString();
                    txtVZV_1st.Text = reader_f["VZV"].ToString();
                    TextIEPServices_1st.Text = reader_f["IEPService"].ToString();
                    TextIndependentStudy_1st.Text = reader_f["IndependentStudy"].ToString();
                    TextHomeBasedPrivate_1st.Text = reader_f["HomeBasedPrivate"].ToString();
                    TextMedExmption_1st.Text = reader_f["TempMedExemption"].ToString();
                    TextTotal_1st.Text = txttotno_1st.Text;

                    if (txtPermMedExmp_1st.Text == "") { txtPermMedExmp_1st.Text = "0"; }
                    if (TextIEPServices_1st.Text == "") { TextIEPServices_1st.Text = "0"; }
                    if (TextIndependentStudy_1st.Text == "") { TextIndependentStudy_1st.Text = "0"; }
                    if (TextHomeBasedPrivate_1st.Text == "") { TextHomeBasedPrivate_1st.Text = "0"; }
                    if (txtNoimm_1st.Text == "") { txtNoimm_1st.Text = "0"; }
                    if (TextIndependentStudy_1st.Text == "") { TextIndependentStudy_1st.Text = "0"; }
                    if (TextHomeBasedPrivate_1st.Text == "") { TextHomeBasedPrivate_1st.Text = "0"; }
                    if (TextMedExmption_1st.Text == "") { TextMedExmption_1st.Text = "0"; }
                    if (TextEnrolledButNotAttending_1st.Text == "") { TextEnrolledButNotAttending_1st.Text = "0"; }
                    if (TextIEPServices_1st.Text == "") { TextIEPServices_1st.Text = "0"; }
                    if (TextIndependentStudy_1st.Text == "") { TextIndependentStudy_1st.Text = "0"; }
                    if (TextHomeBasedPrivate_1st.Text == "") { TextHomeBasedPrivate_1st.Text = "0"; }

                    TextEnrolledButNotAttending_1st.Text = reader_f["EnrolledButNotAttending"].ToString();
                    TextMissingDosesTotal_1st.Text = (Convert.ToInt32(txtPermMedExmp_1st.Text) + Convert.ToInt32(TextIEPServices_1st.Text) + Convert.ToInt32(TextIndependentStudy_1st.Text) + Convert.ToInt32(TextHomeBasedPrivate_1st.Text) + Convert.ToInt32(txtNoimm_1st.Text) + Convert.ToInt32(TextMedExmption_1st.Text) + Convert.ToInt32(TextEnrolledButNotAttending_1st.Text)).ToString();

                    txtAllimm_1st.Enabled = false;
                    txtPermMedExmp_1st.Enabled = false;
                    txtNoimm_1st.Enabled = false;
                    txttotno_1st.Enabled = false;
                    txtPolio_1st.Enabled = false;
                    txtDtp_1st.Enabled = false;
                    txtMMR2_1st.Enabled = false;
                    txtHepb_1st.Enabled = false;
                    txtVZV_1st.Enabled = false;
                    TextIEPServices_1st.Enabled = false;
                    TextIndependentStudy_1st.Enabled = false;
                    TextHomeBasedPrivate_1st.Enabled = false;
                    TextMedExmption_1st.Enabled = false;
                    TextTotal_1st.Enabled = false;
                    TextMissingDosesTotal_1st.Enabled = false;
                    TextEnrolledButNotAttending_1st.Enabled = false;

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

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Remove("K_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void btnprevious_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("SummaryReport.aspx", false);
            }
        }

        
        protected void hdrLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("K_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void ImgBtnReviseYourRPT_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                dynamic id = Session["K_Assessment_id"];
                dynamic id_1st = Session["F_Assessment_id"];
                string sql = null, sql1="";
                int ret = 0, ret1=0;
                DateTime LastResetDate = DateTime.Now;


                //string LastResetDate = System.DateTime.Now();
                // Added by A.T. so that 4 questions are editable at Revise Report. 

                // ReviseReport button moved from original LoginConfirmed.aspx to ViewAndPrint.aspx, so not calling Save()
                //Save();
                sql = "UPDATE Assessments SET ReviseSubmittedRPT = 'Y', LhdReviseDate = GetDate(), ReviseDate = GetDate() WHERE Assmntid = '" + id + "'";
                sql1 = "UPDATE Assessments SET ReviseSubmittedRPT = 'Y', LhdReviseDate = GetDate(), ReviseDate = GetDate() WHERE Assmntid = '" + id_1st + "'";


                try
                {

                    //con.Open();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    // Steve and Teresa decided not to set 
                    // sql = "UPDATE K_Assessment SET isComplete = 'N', LhdReviseDate = '" & LastResetDate & "', LastResetDate = '" & LastResetDate & "' WHERE id = " & id
                    //cmd = new SqlCommand(sql, con);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    ret1 = cmd1.ExecuteNonQuery();

                    if ((ret == 1) && (ret1 == 1))
                    {
                        if ((hdnIsComplete.Value != "N"))
                        {

                            //logaudittrail("LhdReviseDate", hdnLhdReviseDate.Value, LastResetDate);
                            //logaudittrail("LastResetDate", hdnLastResetDate.Value, LastResetDate);
                            //logaudittrail("ReviseSubmittedRPT", hdnReviseSubmittedRPT.Value, "Y");
                        }
                        //lblMsg.Text = "<p><span class=""greenbold"">School 'report completed flag' reset</span></p>"
                        //lblMsg.Visible = True
                        //lblStatus.Text = "Not Completed"
                    }
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    //lblMsg.Text = "<p><span class=""redbold"">" & appError & "</span></p>"
                    //lblMsg.Visible = True
                }
                finally
                {
                    //cmd = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                //Response.Redirect("ReportContactInfo.aspx", true);
                // Update: Direct the Revise Report to School Information instead 08/03/2021
                Response.Redirect("LoginConfirmed.aspx", true);
            }
        }

        protected void ImgBtnPrintRPT_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                try
                {
                    CreatePDF();
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    Response.Write(appError);
                }
            }
        }

        private void CreatePDF()
        {
            string pdfTemplate = null;
            string dir = null;
            string initialFileName = null;
            dynamic id = Session["K_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "SELECT  A.Schcode FROM[dbo].[Assessments] WHERE A.Assmntid = '" + id + "'";
            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.Cohort = 'K' AND A.Assmntid = '" + id + "'";
            string sql2 = "SELECT S.SchCode FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id WHERE S.Cohort = 'K' AND A.Assmntid = '" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            string schoolcode = null;
            try
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                initialFileName = string.Format("Reports/{0}.pdf", id);
                dir = Server.MapPath("Reports");
                //pdfTemplate = Server.MapPath("ReportTemplates/SA_Kinder_2.pdf");
                pdfTemplate = Server.MapPath("ReportTemplates/SA_Kinder_4.pdf");

                SchoolAssessment.Controllers.ReportController.GenerateSchoolSummaryPDF(Server.MapPath(initialFileName), reader, pdfTemplate, dir);
                con.Close();

                con.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    schoolcode = reader2["SchCode"].ToString();
                }
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=KindergartenSummary[" + schoolcode + "].pdf");
                Response.TransmitFile(Server.MapPath(initialFileName));
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                con.Close();
            }
        }

        protected void ImgBtnPrintRPT1st_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["F_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else
            {
                try
                {
                    CreatePDF1st();
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    Response.Write(appError);
                }
            }
        }

        private void CreatePDF1st()
        {
            string pdfTemplate = null;
            string dir = null;
            string initialFileName = null;
            dynamic id_fst = Session["F_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "SELECT  A.Schcode FROM[dbo].[Assessments] WHERE A.Assmntid = '" + id + "'";
            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.Cohort = 'F' AND A.Assmntid = '" + id_fst + "'";
            string sql2 = "SELECT S.SchCode FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id WHERE S.Cohort = 'F' AND A.Assmntid = '" + id_fst + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            string schoolcode = null;
            try
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                initialFileName = string.Format("Reports/{0}.pdf", id_fst);
                dir = Server.MapPath("Reports");
                pdfTemplate = Server.MapPath("ReportTemplates/SA_1stGrade_1.pdf");

                SchoolAssessment.Controllers.ReportController.GenerateSchoolSummaryPDF(Server.MapPath(initialFileName), reader, pdfTemplate, dir);
                con.Close();

                con.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    schoolcode = reader2["SchCode"].ToString();
                }
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=1stGradeSummary[" + schoolcode + "].pdf");
                Response.TransmitFile(Server.MapPath(initialFileName));
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            finally
            {
                con.Close();
            }
        }
    }
}