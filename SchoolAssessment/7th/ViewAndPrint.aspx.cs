using System;
using System.Collections.Generic;
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

namespace SchoolAssessment._7th
{
    public partial class ViewAndReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["7th_Assessment_id"] == null)))
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

            dynamic id = Session["7th_Assessment_id"];
            dynamic id_e = Session["E_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.id = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";
            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = '" + id + "'";
            string sql_e = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = '" + id_e + "'";


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
                    lbsStaffPhone.Text = reader["ReportedPhone"].ToString().Substring(0, 3) + "-" + reader["ReportedPhone"].ToString().Substring(3, 3) + "-" + reader["ReportedPhone"].ToString().Substring(6);
                    lblStaffPhoneExt.Text = reader["ReportedPhoneExt"].ToString();
                    LblStaffEmail.Text = reader["ReportedEmail"].ToString();
                    lblDesContactName.Text = reader["ContactPerson"].ToString();
                    //lblDesContactPhone.Text = reader["ContactPhone"].ToString();
                    lblDesContactPhone.Text = reader["ContactPhone"].ToString().Substring(0, 3) + "-" + reader["ContactPhone"].ToString().Substring(3, 3) + "-" + reader["ContactPhone"].ToString().Substring(6);
                    lblDesPhoneExt.Text = reader["ContactPhoneExt"].ToString();
                    lblDesContactEmail.Text = reader["ContactEmail"].ToString();
                    txtAllimm.Text = reader["AllImm"].ToString();
                    txtPermMedExmp.Text = reader["MedExmp"].ToString();
                    lblSchAdmin.Text = reader["SchAdmin"].ToString();
                    lblSchEmail.Text = reader["SchEmail"].ToString();
                    TextIEPServices.Text = reader["IEPService"].ToString();
                    TextIndependentStudy.Text = reader["IndependentStudy"].ToString();
                    TextHomeBasedPrivate.Text = reader["HomeBasedPrivate"].ToString();
                    TextMedExmption.Text = reader["TempMedExemption"].ToString();
                    txttotno.Text = reader["TotNo"].ToString();
                    TextTotal.Text = txttotno.Text;
                    hdnIsComplete.Value = reader["isComplete"].ToString();
                    lblSubmittedDate.Text = reader["SubmitDate"].ToString();
                    lblRevisedDate.Text = reader["ReviseDate"].ToString();

                    V_txtAllimm.Text = (String)reader["V_AllImm"].ToString();
                    V_txtPermMedExmp.Text = (String)reader["V_MedExmp"].ToString();
                    V_MDMO_PermMedExmp.Text = (String)reader["V_MDMO_PermMedExmp"].ToString();
                    V_TextIEPServices.Text = (String)reader["V_IEPService"].ToString();
                    V_TextIndependentStudy.Text = (String)reader["V_IndependentStudy"].ToString();
                    V_TextHomeBasedPrivate.Text = (String)reader["V_HomeBasedPrivate"].ToString();
                    V_ConditionalNotDue.Text = (String)reader["V_ConditionalNotDue"].ToString();
                    V_TextMedExmption.Text = (String)reader["V_TempMedExemption"].ToString();
                    V_TextEnrolledButNotAttending.Text = (String)reader["V_EnrolledButNotAttending"].ToString();
                    V_TextTotal.Text = txttotno.Text;

                    // Let only Admin can Revise the Report. 
                    // Commented out on 08/31/2017, 06/19/2018, 06/27/2019, 05/21/2020, 08/17/2021
                    // Uncommented on 02/14/2018, 11/30/2018, 03/16/2021, 03/10/2022
                    
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
                    if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }
                    if (TextMedExmption.Text == "") { TextMedExmption.Text = "0"; }
                    if (TextEnrolledButNotAttending.Text == "") { TextEnrolledButNotAttending.Text = "0"; }
                    if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }
                    if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                    if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }

                    if (V_txtAllimm.Text == "") { V_txtAllimm.Text = "0"; }
                    if (V_txtPermMedExmp.Text == "") { V_txtPermMedExmp.Text = "0"; }
                    if (V_MDMO_PermMedExmp.Text == "") { V_MDMO_PermMedExmp.Text = "0"; }
                    if (V_TextIEPServices.Text == "") { V_TextIEPServices.Text = "0"; }
                    if (V_TextIndependentStudy.Text == "") { V_TextIndependentStudy.Text = "0"; }
                    if (V_TextHomeBasedPrivate.Text == "") { V_TextHomeBasedPrivate.Text = "0"; }
                    if (V_ConditionalNotDue.Text == "") { V_ConditionalNotDue.Text = "0"; }
                    if (V_TextMedExmption.Text == "") { V_TextMedExmption.Text = "0"; }
                    if (V_TextEnrolledButNotAttending.Text == "") { V_TextEnrolledButNotAttending.Text = "0"; }


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
                    //TextMissingDosesTotal.Text = (Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)).ToString();
                    //HdnTextMissingDosesTotal.Value = TextMissingDosesTotal.Text;
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
                    txttotno.Enabled = false;
                    TextIEPServices.Enabled = false;
                    TextIndependentStudy.Enabled = false;
                    TextHomeBasedPrivate.Enabled = false;
                    TextMedExmption.Enabled = false;
                    TextTotal.Enabled = false;
                    lblSubmittedDate.Enabled = false;
                    lblRevisedDate.Enabled = false;
                    V_txtAllimm.Enabled = false;
                    V_txtPermMedExmp.Enabled = false;
                    V_MDMO_PermMedExmp.Enabled = false;
                    V_TextIEPServices.Enabled = false;
                    V_TextIndependentStudy.Enabled = false;
                    V_TextHomeBasedPrivate.Enabled = false;
                    V_ConditionalNotDue.Enabled = false;
                    V_TextMedExmption.Enabled = false;
                    V_TextEnrolledButNotAttending.Enabled = false;


                    //TextMissingDosesTotal.Enabled = false;
                    //TxtOthersTotal.Enabled = false;


                    // Added below by A.T. on 08/27/2014
                    //TxtPreJanuaryExmpt.Enabled = False 'Commented out by AT on 06/01/2015
                    //TxtHealthCareExmpt.Enabled = false;
                    //TxtReligiousExmpt.Enabled = false;

                    // Added below by A.T. on 08/18/2015
                    TextEnrolledButNotAttending.Enabled = false;

                    /*if ((int)reader["SubmittedYear"] < 2016)
                    {
                        ImgBtnReviseYourRPT.Enabled = false;
                        ImgBtnReviseYourRPT.Visible = false;
                    }*/

                    if (reader["Schoolyear"].ToString() != SchoolYear)
                    {
                        ImgBtnReviseYourRPT.Enabled = false;
                        ImgBtnReviseYourRPT.Visible = false;
                    }

                }


                // Get StudentYesNo for 8th Grader
                reader.Close();
                SqlCommand cmd_e = new SqlCommand(sql_e, con);
                SqlDataReader reader_e = cmd_e.ExecuteReader();

                while (reader_e.Read())
                {
                    txtAllimm_8th.Text = reader_e["AllImm"].ToString();
                    txtPermMedExmp_8th.Text = reader_e["MedExmp"].ToString();
                    lblSchAdmin.Text = reader_e["SchAdmin"].ToString();
                    lblSchEmail.Text = reader_e["SchEmail"].ToString();
                    TextIEPServices_8th.Text = reader_e["IEPService"].ToString();
                    TextIndependentStudy_8th.Text = reader_e["IndependentStudy"].ToString();
                    TextHomeBasedPrivate_8th.Text = reader_e["HomeBasedPrivate"].ToString();
                    TextMedExmption_8th.Text = reader_e["TempMedExemption"].ToString();
                    txttotno_8th.Text = reader_e["TotNo"].ToString();
                    TextTotal_8th.Text = txttotno_8th.Text;
                    hdnIsComplete.Value = reader_e["isComplete"].ToString();
                    lblSubmittedDate.Text = reader_e["SubmitDate"].ToString();
                    lblRevisedDate.Text = reader_e["ReviseDate"].ToString();
                    TextEnrolledButNotAttending_8th.Text = reader_e["EnrolledButNotAttending"].ToString();

                    V_txtAllimm_8th.Text = (String)reader_e["V_AllImm"].ToString();
                    V_txtPermMedExmp_8th.Text = (String)reader_e["V_MedExmp"].ToString();
                    V_MDMO_PermMedExmp_8th.Text = (String)reader_e["V_MDMO_PermMedExmp"].ToString();
                    V_TextIEPServices_8th.Text = (String)reader_e["V_IEPService"].ToString();
                    V_TextIndependentStudy_8th.Text = (String)reader_e["V_IndependentStudy"].ToString();
                    V_TextHomeBasedPrivate_8th.Text = (String)reader_e["V_HomeBasedPrivate"].ToString();
                    V_ConditionalNotDue_8th.Text = (String)reader_e["V_ConditionalNotDue"].ToString();
                    V_TextMedExmption_8th.Text = (String)reader_e["V_TempMedExemption"].ToString();
                    V_TextEnrolledButNotAttending_8th.Text = (String)reader_e["V_EnrolledButNotAttending"].ToString();
                    V_TextTotal_8th.Text = txttotno_8th.Text;


                    if (reader_e["StudentYesNo"].ToString() == "Yes")
                    {
                        lblStatus_8th.Text = "Active";

                    }
                    else if (reader_e["StudentYesNo"].ToString() == "No")
                    {
                        lblStatus_8th.Text = reader_e["Reason"].ToString();
                    }

                    if (txtPermMedExmp_8th.Text == "") { txtPermMedExmp_8th.Text = "0"; }
                    if (TextIEPServices_8th.Text == "") { TextIEPServices_8th.Text = "0"; }
                    if (TextIndependentStudy_8th.Text == "") { TextIndependentStudy_8th.Text = "0"; }
                    if (TextHomeBasedPrivate_8th.Text == "") { TextHomeBasedPrivate_8th.Text = "0"; }
                    if (TextIndependentStudy_8th.Text == "") { TextIndependentStudy_8th.Text = "0"; }
                    if (TextHomeBasedPrivate_8th.Text == "") { TextHomeBasedPrivate_8th.Text = "0"; }
                    if (TextMedExmption_8th.Text == "") { TextMedExmption_8th.Text = "0"; }
                    if (TextEnrolledButNotAttending_8th.Text == "") { TextEnrolledButNotAttending_8th.Text = "0"; }
                    if (TextIEPServices_8th.Text == "") { TextIEPServices_8th.Text = "0"; }
                    if (TextIndependentStudy_8th.Text == "") { TextIndependentStudy_8th.Text = "0"; }
                    if (TextHomeBasedPrivate_8th.Text == "") { TextHomeBasedPrivate_8th.Text = "0"; }

                    if (V_txtAllimm_8th.Text == "") { V_txtAllimm_8th.Text = "0"; }
                    if (V_txtPermMedExmp_8th.Text == "") { V_txtPermMedExmp_8th.Text = "0"; }
                    if (V_MDMO_PermMedExmp_8th.Text == "") { V_MDMO_PermMedExmp_8th.Text = "0"; }
                    if (V_TextIEPServices_8th.Text == "") { V_TextIEPServices_8th.Text = "0"; }
                    if (V_TextIndependentStudy_8th.Text == "") { V_TextIndependentStudy_8th.Text = "0"; }
                    if (V_TextHomeBasedPrivate_8th.Text == "") { V_TextHomeBasedPrivate_8th.Text = "0"; }
                    if (V_ConditionalNotDue_8th.Text == "") { V_ConditionalNotDue_8th.Text = "0"; }
                    if (V_TextMedExmption_8th.Text == "") { V_TextMedExmption_8th.Text = "0"; }
                    if (V_TextEnrolledButNotAttending_8th.Text == "") { V_TextEnrolledButNotAttending_8th.Text = "0"; }


                    txtAllimm_8th.Enabled = false;
                    txtPermMedExmp_8th.Enabled = false;
                    txttotno_8th.Enabled = false;
                    TextIEPServices_8th.Enabled = false;
                    TextIndependentStudy_8th.Enabled = false;
                    TextHomeBasedPrivate_8th.Enabled = false;
                    TextMedExmption_8th.Enabled = false;
                    TextTotal_8th.Enabled = false;
                    V_txtAllimm_8th.Enabled = false;
                    V_txtPermMedExmp_8th.Enabled = false;
                    V_MDMO_PermMedExmp_8th.Enabled = false;
                    V_TextIEPServices_8th.Enabled = false;
                    V_TextIndependentStudy_8th.Enabled = false;
                    V_TextHomeBasedPrivate_8th.Enabled = false;
                    V_ConditionalNotDue_8th.Enabled = false;
                    V_TextMedExmption_8th.Enabled = false;
                    V_TextEnrolledButNotAttending_8th.Enabled = false;
                    TextEnrolledButNotAttending_8th.Enabled = false;

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
            Session.Remove("7th_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void btnprevious_Click(object sender, EventArgs e)
        {
            if (((Session["7th_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("SummaryReport.aspx", false);
            }
        }


        protected void hdrLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("7th_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void ImgBtnReviseYourRPT_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["7th_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                dynamic id = Session["7th_Assessment_id"];
                dynamic id_e = Session["E_Assessment_id"];
                string sql = null, sql_e = null;
                int ret = 0, ret_e=0;
                DateTime LastResetDate = DateTime.Now;


                //string LastResetDate = System.DateTime.Now();
                // Added by A.T. so that 4 questions are editable at Revise Report. 

                // ReviseReport button moved from original LoginConfirmed.aspx to ViewAndPrint.aspx, so not calling Save()
                //Save();
                sql = "UPDATE Assessments SET ReviseSubmittedRPT = 'Y', LhdReviseDate = GetDate(), ReviseDate = GetDate()  WHERE Assmntid = '" + id + "'";
                sql_e = "UPDATE Assessments SET ReviseSubmittedRPT = 'Y', LhdReviseDate = GetDate(), ReviseDate = GetDate()  WHERE Assmntid = '" + id_e + "'";


                try
                {

                    //con.Open();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    // Steve and Teresa decided not to set 
                    // sql = "UPDATE K_Assessment SET isComplete = 'N', LhdReviseDate = '" & LastResetDate & "', LastResetDate = '" & LastResetDate & "' WHERE id = " & id
                    //cmd = new SqlCommand(sql, con);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlCommand cmd_e = new SqlCommand(sql_e, con);
                    
                    con.Open();
                    
                    ret = cmd.ExecuteNonQuery();
                    ret_e = cmd_e.ExecuteNonQuery();

                    if ((ret == 1) && (ret_e == 1))
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
            if (((Session["7th_Assessment_id"] == null)))
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
            dynamic id = Session["7th_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "SELECT  A.Schcode FROM[dbo].[Assessments] WHERE A.Assmntid = '" + id + "'";
            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.Cohort = 'S' AND A.Assmntid = '" + id + "'";
            string sql2 = "SELECT S.SchCode FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id WHERE S.Cohort = 'S' AND A.Assmntid = '" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            string schoolcode = null;
            try
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                initialFileName = string.Format("Reports/{0}.pdf", id);
                dir = Server.MapPath("Reports");
                pdfTemplate = Server.MapPath("ReportTemplates/SA_7th_1.pdf");

                SchoolAssessment.Controllers.ReportController.GenerateSchoolSummaryPDF(Server.MapPath(initialFileName), reader, pdfTemplate, dir);
                con.Close();

                con.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    schoolcode = reader2["SchCode"].ToString();
                }
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=7thGradeSummary[" + schoolcode + "].pdf");
                Response.TransmitFile(Server.MapPath(initialFileName));
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

        private void CreatePDF8th()
        {
            string pdfTemplate = null;
            string dir = null;
            string initialFileName = null;
            dynamic id = Session["E_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "SELECT  A.Schcode FROM[dbo].[Assessments] WHERE A.Assmntid = '" + id + "'";
            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.Cohort = 'E' AND A.Assmntid = '" + id + "'";
            string sql2 = "SELECT S.SchCode FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id WHERE S.Cohort = 'E' AND A.Assmntid = '" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = new SqlCommand(sql2, con);

            string schoolcode = null;
            try
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                initialFileName = string.Format("Reports/{0}.pdf", id);
                dir = Server.MapPath("Reports");
                pdfTemplate = Server.MapPath("ReportTemplates/SA_8th_1.pdf");

                SchoolAssessment.Controllers.ReportController.GenerateSchoolSummaryPDF(Server.MapPath(initialFileName), reader, pdfTemplate, dir);
                con.Close();

                con.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    schoolcode = reader2["SchCode"].ToString();
                }
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=8thGradeSummary[" + schoolcode + "].pdf");
                Response.TransmitFile(Server.MapPath(initialFileName));
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

        protected void ImgBtnPrintRPT8th_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["E_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else
            {
                try
                {
                    CreatePDF8th();
                }
                catch (Exception ex)
                {
                    dynamic appError = ex.Message;
                    Response.Write(appError);
                }
            }
        }
    }
}