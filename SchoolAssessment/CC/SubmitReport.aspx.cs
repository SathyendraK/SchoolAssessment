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
using System.Windows.Forms;
using System.Web;

namespace SchoolAssessment.CC
{
    public partial class SubmitReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }

            if ((Page.IsPostBack == false))
            {
                TextTotal.Attributes.Add("readonly", "readonly");
                TextMissingDosesTotal.Attributes.Add("readonly", "readonly");
                //TxtOthersTotal.Attributes.Add("readonly", "readonly");

                dynamic id = Session["CC_Assessment_id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                //string sql = "SELECT S.*, C.CoName, A.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode WHERE A.Assmntid = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";
                string sql = "SELECT S.*, C.CoName, A.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode WHERE A.Assmntid = '" + id + "'";

                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        // if report is already completed, send to view report page
                        /*
                        if ((reader["isComplete"].ToString() == "Y"))
                        {
                            // Added check so that if user is revising the submitted report, you will not be redirected by A.T. on 09/16/2014
                            if ((reader["ReviseSubmittedRPT"].ToString() != "Y"))
                            {
                                Response.Redirect("ViewAndPrint.aspx", true);
                            }
                        }
                        */

                        // if somehow user selected no students and did not select a reason
                        // but still got to this page, send back to first page
                        if ((reader["StudentYesNo"].ToString() == "No" & string.IsNullOrEmpty(reader["Reason"].ToString())))
                        {
                            Response.Redirect("LoginConfirmed.aspx", true);
                        }

                        // fill hidden fields
                        hdnSchCode.Value = reader["SchCode"].ToString();
                        hdnTotNo.Value = reader["TotNo"].ToString();
                        hdnAllImm.Value = reader["AllImm"].ToString();
                        hdnMedExmp.Value = reader["MedExmp"].ToString();
                        hdnPerExmp.Value = reader["BeleExmp"].ToString();
                        hdnNoImm.Value = reader["NoImm"].ToString();
                        hdnPolio.Value = reader["Polio"].ToString();
                        hdnDTP.Value = reader["DTP_DTAP_DT"].ToString();
                        hdnMMR1.Value = reader["MMRDose1"].ToString();
                        hdnMMR2.Value = reader["MMRDose2"].ToString();
                        hdnHepB.Value = reader["HepB"].ToString();
                        hdnVZV.Value = reader["VZV"].ToString();
                        hdnFormPerson.Value = reader["ReportedPerson"].ToString();
                        hdnFormEmail.Value = reader["ReportedEmail"].ToString();
                        hdnFormPhone.Value = reader["ReportedPhone"].ToString();
                        hdnFormPhoneExt.Value = reader["ReportedPhoneExt"].ToString();
                        hdnContactPerson.Value = reader["ContactPerson"].ToString();
                        hdnContactEmail.Value = reader["ContactEmail"].ToString();
                        hdnContactPhone.Value = reader["ContactPhone"].ToString();
                        hdnContactPhoneExt.Value = reader["ContactPhoneExt"].ToString();
                        hdnPassword.Value = reader["Password"].ToString();
                        hdnSubmitDate.Value = reader["SubmitDate"].ToString();
                        hdnIsComplete.Value = reader["isComplete"].ToString();
                        hdnReviseDate.Value = reader["ReviseDate"].ToString();
                        hdnLhdReviseDate.Value = reader["LhdReviseDate"].ToString();


                        //Added below by A.T. on 08/26/2014
                        //hdnTxtPreJanuaryExmpt.Value = reader("PBE_PreJanuaryExmpt").ToString()
                        hdnTxtHealthCareExmpt.Value = reader["PBE_HealthCareExmpt"].ToString();
                        hdnTxtReligiousExmpt.Value = reader["PBE_ReligiousExmpt"].ToString();

                        //Added by A.T. on 08/17/2015 
                        hdnEnrolledButNotAttending.Value = reader["EnrolledButNotAttending"].ToString();

                        // Pre-fill form values
                        lblSchName.Text = (String)reader["SchName"].ToString();
                        lblSchCode.Text = (String)reader["SchCode"].ToString();

                        // Commented out by A.T. on 08/27/2014
                        //txtpin.Text = Trim(reader("Password").ToString())
                        //txtcpin.Text = Trim(reader("Password").ToString())


                        // Existing SubmitDate, or today's date
                        /*  Get this in the SQL statement instead
                        if ((string.IsNullOrEmpty(reader["SubmitDate"].ToString())))
                        {
                            txtDate.Text = System.DateTime.Now().ToString("d");
                        }
                        else {
                            System.DateTime theDate = Strings.Trim(reader("SubmitDate"));
                            txtDate.Text = theDate.ToString("d");
                        }
                        */

                        // If form has been reset before, show Form Submitter values
                        // Not sure if I need to keep it here 06/23/2016 AT
                        /*
                        if ((!string.IsNullOrEmpty(reader["LastResetDate"].ToString())))
                        {
                            txtfcperson.Text = (String)reader["ContactPerson"].ToString();
                            txtfcNumber.Text = (String)reader["ContactPhone"].ToString();
                            txtfcNumberExt.Text = (String)reader["ContactPhoneExt"].ToString();
                            txtfcemail.Text = (String)reader["ContactEmail"].ToString();
                            txtfccemail.Text = (String)reader["ContactEmail"].ToString();
                        }
                        */


                        // If no kindergarten students, prefill with 0s
                        if ((String)reader["StudentYesNo"].ToString() == "No")
                        {
                            ActiveStudentsValidator.Enabled = false;
                            txttotno.Text = "0";
                            txtAllimm.Text = "0";
                            txtPermMedExmp.Text = "0";
                            txtBelExmp.Text = "0";
                            txtNoimm.Text = "0";
                            txtPolio.Text = "0";
                            txtDtp.Text = "0";
                            txtMMR2.Text = "0";
                            txtHepb.Text = "0";
                            txtVZV.Text = "0";
                            TextTotal.Text = "0";
                            TextMissingDosesTotal.Text = "0";
                            //TxtOthersTotal.Text = "0";
                            TxtHib.Text = "0";

                            // Added by AT on 06/26/2016
                            TextIEPServices.Text = "0";
                            //TextIndependentStudy.Text = "0";
                            //TextHomeBasedPrivate.Text = "0";
                            TextMedExmption.Text = "0";
                            //TextHib.Text = "0";

                            //Added below by A.T. on 08/26/2014
                            //TxtPreJanuaryExmpt.Text = "0" '  Commented out by AT on 06/01/2015 per Teressa Lee's request
                            //TxtHealthCareExmpt.Text = "0";    Commented out by AT on 06/27/2016
                            //TxtReligiousExmpt.Text = "0";

                            //Added below by A.T. on 08/25/2015
                            TextEnrolledButNotAttending.Text = "0";

                            txttotno.Enabled = false;
                            txtAllimm.Enabled = false;
                            txtPermMedExmp.Enabled = false;
                            txtBelExmp.Enabled = false;
                            txtNoimm.Enabled = false;
                            txtPolio.Enabled = false;
                            txtDtp.Enabled = false;
                            txtMMR2.Enabled = false;
                            txtHepb.Enabled = false;
                            txtVZV.Enabled = false;
                            //TextTotal.Enabled = false;
                            TextIEPServices.Enabled = false;
                            //TextIndependentStudy.Enabled = false;
                            //TextHomeBasedPrivate.Enabled = false;
                            TextMedExmption.Enabled = false;
                            TxtHib.Enabled = false;
                            //TextHib.Enabled = false;
                            //Added below by A.T. on 09/16/2014
                            //TxtPreJanuaryExmpt.Enabled = False 'Commented out by AT on 06/01/2015 per Teressa Lee's request
                            //TxtHealthCareExmpt.Enabled = false; Commented out by AT on 06/26/2016
                            //TxtReligiousExmpt.Enabled = false;

                            //Added below by A.T. on 08/26/2015
                            TextEnrolledButNotAttending.Enabled = false;


                            //txtStaffPrsn.Focus();
                        }
                        else {

                            //TextTotal.Enabled = false;
                            txttotno.Text = (String)reader["TotNo"].ToString();
                            txtAllimm.Text = (String)reader["AllImm"].ToString();
                            txtPermMedExmp.Text = (String)reader["MedExmp"].ToString();
                            txtBelExmp.Text = (String)reader["BeleExmp"].ToString();
                            txtNoimm.Text = (String)reader["NoImm"].ToString();
                            txtPolio.Text = (String)reader["Polio"].ToString();
                            txtDtp.Text = (String)reader["DTP_DTAP_DT"].ToString();
                            txtMMR2.Text = (String)reader["MMRDose2"].ToString();
                            txtHepb.Text = (String)reader["HepB"].ToString();
                            txtVZV.Text = (String)reader["VZV"].ToString();
                            TxtHib.Text = (String)reader["HIB"].ToString();


                            // Added by AT on 06/26/2016
                            TextIEPServices.Text = (String)reader["IEPService"].ToString();
                            //TextIndependentStudy.Text = (String)reader["IndependentStudy"].ToString();
                            //TextHomeBasedPrivate.Text = (String)reader["HomeBasedPrivate"].ToString();
                            TextMedExmption.Text = (String)reader["TempMedExemption"].ToString();
                            TextTotal.Text = txttotno.Text;
                            //TextMissingDosesTotal.Text = ??

                            //Added below by A.T. on 08/26/2014
                            //TxtPreJanuaryExmpt.Text = Trim(reader["PBE_PreJanuaryExmpt"].ToString())      'Commented out by AT on 06/01/2015 per Teressa Lee's request
                            //TxtHealthCareExmpt.Text = (String)reader["PBE_HealthCareExmpt"].ToString();   Commented out by AT on 06/26/2016
                            //TxtReligiousExmpt.Text = (String)reader["PBE_ReligiousExmpt"].ToString();     Commented out by AT on 06/26/2016

                            //Added below by A.T. on 08/26/2015
                            TextEnrolledButNotAttending.Text = (String)reader["EnrolledButNotAttending"].ToString();

                            if (txtPermMedExmp.Text == "") { txtPermMedExmp.Text = "0"; }
                            if (txtBelExmp.Text == "") { txtBelExmp.Text = "0"; }
                            if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }
                            //if (TextIndependentStudy.Text == "") { TextIndependentStudy.Text = "0"; }
                            //if (TextHomeBasedPrivate.Text == "") { TextHomeBasedPrivate.Text = "0"; }
                            if (txtNoimm.Text == "") { txtNoimm.Text = "0"; }
                            if (TextMedExmption.Text == "") { TextMedExmption.Text = "0"; }
                            if (TextEnrolledButNotAttending.Text == "") { TextEnrolledButNotAttending.Text = "0"; }
                            if (TextIEPServices.Text == "") { TextIEPServices.Text = "0"; }


                            if (txtPolio.Text == "") { txtPolio.Text = "0"; }
                            if (txtDtp.Text == "") { txtDtp.Text = "0"; }
                            if (txtMMR2.Text == "") { txtMMR2.Text = "0"; }
                            if (txtHepb.Text == "") { txtHepb.Text = "0"; }
                            if (txtVZV.Text == "") { txtVZV.Text = "0"; }
                            if (TxtHib.Text == "") { TxtHib.Text = "0"; }

                            TextMissingDosesTotal.Text = (Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text)  + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)).ToString();
                            //TxtOthersTotal.Text = (Convert.ToInt32(TextIEPServices.Text)).ToString();

                        }

                        //if ((int)reader["SubmittedYear"] < 2016)
                        if (reader["Schoolyear"].ToString() != SchoolYear)
                        {
                            ImgBtnSubmit.Enabled = false;
                            ImgBtnSubmit.Visible = false;
                        }

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


            if (Page.IsPostBack)
            {

                //if (HdnConfirmSubmit.Value == "N") { 
                // Get the most recent value of TextTotal modified by client-side Java Script
                TextTotal.Text = Request.Form["TextTotal"].ToString();
                TextMissingDosesTotal.Text = Request.Form["TextMissingDosesTotal"].ToString();
                //TxtOthersTotal.Text = Request.Form["TxtOthersTotal"].ToString();

            }

        }


        public void KindergartenSum(object source, ServerValidateEventArgs args)
        {
            // Added TextEnrolledButNotAttending for addition to validate total number of kindergartens.
            if (((Convert.ToInt32(txtAllimm.Text) + Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text)  + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)) != Convert.ToInt32(txttotno.Text)))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        public void NoImmZeroesValidate(object source, ServerValidateEventArgs args)
        {
            if ((Convert.ToInt32(txtNoimm.Text) > 0 & Convert.ToInt32(txtPolio.Text) == 0 & Convert.ToInt32(txtDtp.Text) == 0 & Convert.ToInt32(txtMMR2.Text) == 0 & Convert.ToInt32(txtHepb.Text) == 0 & Convert.ToInt32(txtVZV.Text) == 0 & Convert.ToInt32(TxtHib.Text) == 0))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        public void NoImmMinimum(object source, ServerValidateEventArgs args)
        {
            if ((Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)) > (Convert.ToInt32(txtPolio.Text) + Convert.ToInt32(txtDtp.Text) + Convert.ToInt32(txtMMR2.Text) + Convert.ToInt32(txtHepb.Text) + Convert.ToInt32(txtVZV.Text) + Convert.ToInt32(TxtHib.Text)))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("LoginConfirmed.aspx", false);
            }
        }





        private void Save()
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                dynamic id = Session["CC_Assessment_id"];
                dynamic SesAdminUserType = Session["AdminUserType"];
                string sql = null;
                SqlCommand cmd = default(SqlCommand);
                int ret = 0;
                //string reviseDate = System.DateTime.Now();
                DateTime reviseDate = DateTime.Now;
                string SubmitDate;
                string AdminReviseDate;
                string LHDReviseDate;

                try
                {
                    //if ((IsNullOrEmpty(Session["AdminUserType"].ToString())))
                    if (Object.ReferenceEquals(null, SesAdminUserType))
                    {
                        // Removed PBE_PreJanuaryExmpt by AT on 06/01/2015 
                        sql = "UPDATE Assessments SET isComplete = 'Y', SubmitDate = @SubmitDate, ReviseDate = @ReviseDate, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, DTP_DTAP_DT = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, HIB = @Hib, EnrolledButNotAttending = @EnrolledButNotAttending, IEPService = @IEP,  TempMedExemption = @TMPMedExmp WHERE Assmntid = '" + id + "' ";
                        AdminReviseDate = reviseDate.ToString();
                        LHDReviseDate = hdnLhdReviseDate.Value;
                    }
                    else {
                        sql = "UPDATE Assessments SET isComplete = 'Y', SubmitDate = @SubmitDate, LhdReviseDate = @LhdReviseDate, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, DTP_DTAP_DT = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, HIB = @Hib, EnrolledButNotAttending = @EnrolledButNotAttending, IEPService = @IEP, TempMedExemption = @TMPMedExmp WHERE Assmntid = '" + id + "'";
                        AdminReviseDate = hdnReviseDate.Value;
                        LHDReviseDate = reviseDate.ToString();
                    }


                    if ((string.IsNullOrEmpty(hdnSubmitDate.Value)))
                    {
                        //cmd.Parameters.Add("@SubmitDate", reviseDate);
                        SubmitDate = reviseDate.ToString();
                    }
                    else {
                        //cmd.Parameters.AddWithValue("@SubmitDate", hdnSubmitDate.Value);
                        SubmitDate = hdnSubmitDate.Value;
                    }




                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@AllImm", Convert.ToInt32(txtAllimm.Text));
                    cmd.Parameters.AddWithValue("@MedExmp", Convert.ToInt32(txtPermMedExmp.Text));
                    cmd.Parameters.AddWithValue("@BeleExmp", Convert.ToInt32(txtBelExmp.Text));
                    cmd.Parameters.AddWithValue("@NoImm", Convert.ToInt32(txtNoimm.Text));
                    cmd.Parameters.AddWithValue("@TotNo", Convert.ToInt32(txttotno.Text));
                    cmd.Parameters.AddWithValue("@Polio", Convert.ToInt32(txtPolio.Text));
                    cmd.Parameters.AddWithValue("@DTP", Convert.ToInt32(txtDtp.Text));
                    cmd.Parameters.AddWithValue("@MMRDose2", Convert.ToInt32(txtMMR2.Text));
                    cmd.Parameters.AddWithValue("@HepB", Convert.ToInt32(txtHepb.Text));
                    cmd.Parameters.AddWithValue("@VZV", Convert.ToInt32(txtVZV.Text));
                    cmd.Parameters.AddWithValue("@EnrolledButNotAttending", Convert.ToInt32(TextEnrolledButNotAttending.Text));  // Added EnrolledButNotAttending by A.T. on 08/18/2015
                    cmd.Parameters.AddWithValue("@IEP", Convert.ToInt32(TextIEPServices.Text));
                    //cmd.Parameters.AddWithValue("@IndependentStudy", Convert.ToInt32(TextIndependentStudy.Text));
                    //cmd.Parameters.AddWithValue("@HomeBased", Convert.ToInt32(TextHomeBasedPrivate.Text));
                    cmd.Parameters.AddWithValue("@TMPMedExmp", Convert.ToInt32(TextMedExmption.Text));
                    cmd.Parameters.AddWithValue("@ReviseDate", reviseDate);
                    cmd.Parameters.AddWithValue("@LhdReviseDate", hdnLhdReviseDate.Value);
                    cmd.Parameters.AddWithValue("@SubmitDate", SubmitDate);
                    cmd.Parameters.AddWithValue("@Hib", TxtHib.Text);

                    // Added below by A.T. on 08/27/2014
                    //cmd.Parameters.Add(New SqlParameter("@PBE_PreJanuaryExmpt", SqlDbType.Int)) 'Commented out by AT on 06/01/2015 per Teressa Lee's request
                    //cmd.Parameters.AddWithValue(new SqlParameter("@PBE_HealthCareExmpt", Convert.ToInt32(TxtHealthCareExmpt.Text));
                    //cmd.Parameters.AddWithValue(new SqlParameter("@PBE_ReligiousExmpt", Convert.ToInt32(TxtReligiousExmpt.Text);

                    //cmd.Parameters("@id").Value = Session["CC_Assessment_id"];


                    // Commented out by A.T. on 08/27/2014
                    //cmd.Parameters("@Password").Value = txtpin.Text 

                    // Added below by A.T. on 08/27/2014
                    //cmd.Parameters("@PBE_PreJanuaryExmpt").Value = CInt(TxtPreJanuaryExmpt.Text) 'Commented out by AT on 06/01/2015 per Teressa Lee's request
                    //cmd.Parameters("@PBE_HealthCareExmpt").Value = Convert.ToInt32(TxtHealthCareExmpt.Text);
                    //cmd.Parameters("@PBE_ReligiousExmpt").Value = Convert.ToInt32(TxtReligiousExmpt.Text);





                    //Response.Redirect("ViewAndPrint.aspx", true);


                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    if ((ret == 1))
                    {
                        //logaudittrail("isComplete", hdnIsComplete.Value, "Y");
                        // work later 06/28/2016 AT
                        /*
                        if ((hdnSubmitDate.Value != txtDate.Text))
                        {
                            logaudittrail("SubmitDate", hdnSubmitDate.Value, txtDate.Text);
                        }
                        */
                        
                        if ((hdnTotNo.Value != txttotno.Text))
                        {
                            logaudittrail("TotNo", hdnTotNo.Value, txttotno.Text);
                        }
                        if ((hdnAllImm.Value != txtAllimm.Text))
                        {
                            logaudittrail("AllImm", hdnAllImm.Value, txtAllimm.Text);
                        }
                        if ((hdnMedExmp.Value != txtPermMedExmp.Text))
                        {
                            logaudittrail("MedExmp", hdnMedExmp.Value, txtPermMedExmp.Text);
                        }
                        if ((hdnPerExmp.Value != txtBelExmp.Text))
                        {
                            logaudittrail("BeleExmp", hdnPerExmp.Value, txtBelExmp.Text);
                        }
                        if ((hdnNoImm.Value != txtNoimm.Text))
                        {
                            logaudittrail("NoImm", hdnNoImm.Value, txtNoimm.Text);
                        }
                        if ((hdnPolio.Value != txtPolio.Text))
                        {
                            logaudittrail("Polio", hdnPolio.Value, txtPolio.Text);
                        }
                        if ((hdnDTP.Value != txtDtp.Text))
                        {
                            logaudittrail("DTP-DTAP-DT", hdnDTP.Value, txtDtp.Text);
                        }
                        if ((hdnMMR2.Value != txtMMR2.Text))
                        {
                            logaudittrail("MMRDose2", hdnMMR2.Value, txtMMR2.Text);
                        }
                        if ((hdnHepB.Value != txtHepb.Text))
                        {
                            logaudittrail("HepB", hdnHepB.Value, txtHepb.Text);
                        }
                        if ((hdnVZV.Value != txtVZV.Text))
                        {
                            logaudittrail("VZV", hdnVZV.Value, txtVZV.Text);
                        }


                        //Added below by A.T. on 08/22/2014
                        //If (hdnTxtPreJanuaryExmpt.Value <> TxtPreJanuaryExmpt.Text) Then
                        // logaudittrail("PBE_PreJanuaryExmpt", hdnTxtPreJanuaryExmpt.Value, TxtPreJanuaryExmpt.Text)
                        //End If

                        /* work later 06/28/2016 AT */
                        /*
                        if ((hdnTxtHealthCareExmpt.Value != TxtHealthCareExmpt.Text))
                        {
                            logaudittrail("PBE_HealthCareExmpt", hdnTxtHealthCareExmpt.Value, TxtHealthCareExmpt.Text);
                        }
                        if ((hdnTxtReligiousExmpt.Value != TxtReligiousExmpt.Text))
                        {
                            logaudittrail("PBE_ReligiousExmpt", hdnTxtReligiousExmpt.Value, TxtReligiousExmpt.Text);
                        }

                        // Added below by A.T. on 08/18/2015
                        if ((hdnEnrolledButNotAttending.Value != TextboxEnrolledButNotAttending.Text))
                        {
                            logaudittrail("hdnEnrolledButNotAttending", hdnEnrolledButNotAttending.Value, TextboxEnrolledButNotAttending.Text);
                        }
                        */
    
                        // Commented out by A.T. on 08/27/2014
                        //If (hdnPassword.Value <> txtpin.Text) Then
                        //logaudittrail("Password", hdnPassword.Value, txtpin.Text)
                        //End If

                        Response.Redirect("ViewAndPrint.aspx", true);
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
                    cmd = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        private void logaudittrail(string fieldname, string fromval, string toval)
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            dynamic id = Session["CC_Assessment_id"];
            string sql = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {

                sql = "INSERT INTO AuditTrail(SchCode, ScreenName, FieldName, FromValue, ToValue, IP, TimeStamp) values (@SchoolCode, @ScreenName, @FieldName, @FromValue, @ToValue, @IP, GetDate())";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@SchoolCode", lblSchCode.Text);
                cmd.Parameters.AddWithValue("@ScreenName", "SubmitReport");
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


        protected void hdrLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CC_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }

        protected void ImgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("ReportContactInfo.aspx", false);
            }
        }

        protected void ImgBtnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (((Session["CC_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else
            {
                Page.Validate();
                if ((Page.IsValid))
                {

                    //string confirmValue = Request.Form["confirm_value"];
                    //if (confirmValue == "Yes")
                    //{
                        Save();
                    //}

                }
            }
        }



    }
}