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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
*/


namespace SchoolAssessment.KG
{
    public partial class SummaryReport : System.Web.UI.Page
    {
        string strTextTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string changedTextTotal = TextTotal.Text;
           

            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }

            if ((Page.IsPostBack == false))
            {
                TextTotal.Attributes.Add("readonly", "readonly");
                dynamic id = Session["K_Assessment_id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string sql = "SELECT S.*, C.CoName, A.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode WHERE S.id = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";


                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        // if report is already completed, send to view report page
                        if ((reader["isComplete"].ToString() == "Y"))
                        {
                            // Added check so that if user is revising the submitted report, you will not be redirected by A.T. on 09/16/2014
                            if ((reader["ReviseSubmittedRPT"].ToString() != "Y"))
                            {
                                Response.Redirect("ViewReportInfo.aspx", true);
                            }
                        }

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

                        /*
                        txtStaffPrsn.Text = (String)reader["ReportedPerson"].ToString();
                        txtStaffPhNo.Text = (String)reader["ReportedPhone"].ToString();
                        txtStaffPhNoExt.Text = (String)reader["ReportedPhoneExt"].ToString();
                        txtmail.Text = (String)reader["ReportedEmail"].ToString();
                        txtconfirmmail.Text = (String)reader["ReportedEmail"].ToString();
                        */

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
                        if ( (String)reader["StudentYesNo"].ToString()  == "No" )
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

                            // Added by AT on 06/26/2016
                            TextIEPServices.Text = "0";
                            TextIndependentStudy.Text = "0";
                            TextHomeBasedPrivate.Text = "0";
                            TextMedExmption.Text = "0";
                            TextHib.Text = "0";

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
                            TextIndependentStudy.Enabled = false;
                            TextHomeBasedPrivate.Enabled = false;
                            TextMedExmption.Enabled = false;
                            TextHib.Enabled = false;




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
                            //txtPerBelExmp.Text = Trim(reader["BeleExmp"].ToString();
                            txtNoimm.Text = (String)reader["NoImm"].ToString();
                            txtPolio.Text = (String)reader["Polio"].ToString();
                            txtDtp.Text = (String)reader["DTP_DTAP_DT"].ToString();
                            txtMMR2.Text = (String)reader["MMRDose2"].ToString();
                            txtHepb.Text = (String)reader["HepB"].ToString();
                            txtVZV.Text = (String)reader["VZV"].ToString();

                            // Added by AT on 06/26/2016
                            TextIEPServices.Text = (String)reader["IEPService"].ToString();
                            TextIndependentStudy.Text = (String)reader["IndependentStudy"].ToString();
                            TextHomeBasedPrivate.Text = (String)reader["HomeBasedPrivate"].ToString();

                            //Added below by A.T. on 08/26/2014
                            //TxtPreJanuaryExmpt.Text = Trim(reader["PBE_PreJanuaryExmpt"].ToString())      'Commented out by AT on 06/01/2015 per Teressa Lee's request
                            //TxtHealthCareExmpt.Text = (String)reader["PBE_HealthCareExmpt"].ToString();   Commented out by AT on 06/26/2016
                            //TxtReligiousExmpt.Text = (String)reader["PBE_ReligiousExmpt"].ToString();     Commented out by AT on 06/26/2016

                            //Added below by A.T. on 08/26/2015
                            TextEnrolledButNotAttending.Text = (String)reader["EnrolledButNotAttending"].ToString();

                            // work on this later 06/28/2016 
                            //Int32 total = (Convert.ToInt32(txtAllimm.Text) + Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text));
                            //TextTotal.Text = total.ToString();


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

            //  Work on this later 06/23/2016 AT 
            // Addded by AT on 10/22/2014 to maintain tab order after PostBack 
            
            if (Page.IsPostBack) {


                //TextTotal.Text = "92";
                //TextTotal.Text = changedTextTotal;

                TextTotal.Text = Request.Form["TextTotal"].ToString();
                /*
                WebControl wcICausedPostBack = (WebControl)GetControlThatCausedPostBack(sender as Page);
	            int indx = wcICausedPostBack.TabIndex;
	            dynamic ctrl = from control in wcICausedPostBack.Parent.Controls.OfType<WebControl>()where control.TabIndex > indxcontrol;
	            ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus();
                */
            }



            //Added below by A.T. on 08/26/2014
            // Work on this later 06/23/2016
            //txtPerBelExmp.Text = (Val(TxtPreJanuaryExmpt.Text) + Val(TxtHealthCareExmpt.Text) + Val(TxtReligiousExmpt.Text)).ToString
            //txtPerBelExmp.Text = (Conversion.Val(TxtHealthCareExmpt.Text) + Conversion.Val(TxtReligiousExmpt.Text)).ToString;
            //txtPerBelExmp.Text = (Int32.Parse(TxtHealthCareExmpt.Text) + Int32.Parse(TxtReligiousExmpt.Text)).ToString();

            //TextTotal.Text = (Convert.ToInt32(txtAllimm.Text) + Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)).ToString();
            //TextTotal.Text = "0";
            //TextTotal.Text = changedTextTotal;
            //TextTotal.Text = strTextTotal;

            //TextTotal.Text = strTextTotal;

        }

        public void KindergartenSum(object source, ServerValidateEventArgs args)
        {
            // Added TextEnrolledButNotAttending for addition to validate total number of kindergartens.
            if (((Convert.ToInt32(txtAllimm.Text) + Convert.ToInt32(txtPermMedExmp.Text) + Convert.ToInt32(txtBelExmp.Text) + Convert.ToInt32(TextIEPServices.Text) + Convert.ToInt32(TextIndependentStudy.Text) + Convert.ToInt32(TextHomeBasedPrivate.Text) + Convert.ToInt32(txtNoimm.Text) + Convert.ToInt32(TextMedExmption.Text) + Convert.ToInt32(TextEnrolledButNotAttending.Text)) != Convert.ToInt32(txttotno.Text)))
            //if (Convert.ToInt32(TextTotal.Text) != Convert.ToInt32(txttotno.Text))
            {
                    args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }

        public void NoImmZeroesValidate(object source, ServerValidateEventArgs args)
        {
            if ((Convert.ToInt32(txtNoimm.Text) > 0 & Convert.ToInt32(txtPolio.Text) == 0 & Convert.ToInt32(txtDtp.Text) == 0 & Convert.ToInt32(txtMMR2.Text) == 0 & Convert.ToInt32(txtHepb.Text) == 0 & Convert.ToInt32(txtVZV.Text) == 0))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        public void NoImmMinimum(object source, ServerValidateEventArgs args)
        {
            if ((Convert.ToInt32(txtNoimm.Text) > (Convert.ToInt32(txtPolio.Text) + Convert.ToInt32(txtDtp.Text) + Convert.ToInt32(txtMMR2.Text) + Convert.ToInt32(txtHepb.Text) + Convert.ToInt32(txtVZV.Text))))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("LoginConfirmed.aspx", false);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                Response.Redirect("ReportContactInfo.aspx", false);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //string strTextTotal;
            //TextTotal.Text = HiddenTextTotal.Value;
            //strTextTotal = HiddenTextTotal.Value;
            Response.Write(Request.Form[TextTotal.UniqueID]);
            //TextTotal.Text = Request.Form[TextTotal.UniqueID];
            //strTextTotal = Request.Form[TextTotal.UniqueID];

            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                //Response.Redirect("ReviewAndSubmit.aspx", false);

                    Page.Validate();
                    if ((Page.IsValid))
                    {
                        Save();
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
                //string connectString = System.Configuration.ConfigurationManager.AppSettings("kSchoolDBConnectString");
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string sql = null;
                SqlCommand cmd = default(SqlCommand);
                int ret = 0;
                //string reviseDate = System.DateTime.Now();

                try
                {
                    /* work later on 06/28/2016
                    if ((string.IsNullOrEmpty(Session["AdminUserType"])))
                    {
                        // Removed PBE_PreJanuaryExmpt by AT on 06/01/2015 
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, ReviseDate = @ReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_PreJanuaryExmpt = @PBE_PreJanuaryExmpt, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt  WHERE id = @id"
                        sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, ReviseDate = @ReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt, EnrolledButNotAttending = @EnrolledButNotAttending  WHERE id = @id";
                    }
                    else {
                        //sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, LhdReviseDate = @LhdReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_PreJanuaryExmpt = @PBE_PreJanuaryExmpt, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt WHERE id = @id"
                        sql = "UPDATE K_Assessment SET isComplete = 'Y', SubmitDate = @SubmitDate, LhdReviseDate = @LhdReviseDate, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, PBE_HealthCareExmpt = @PBE_HealthCareExmpt, PBE_ReligiousExmpt = @PBE_ReligiousExmpt, EnrolledButNotAttending = @EnrolledButNotAttending WHERE id = @id";
                    }

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.Add(new SqlParameter("@SubmitDate", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@ReviseDate", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@LhdReviseDate", SqlDbType.VarChar));
                    // Commented out by A.T. on 08/27/2014
                    //cmd.Parameters.Add(New SqlParameter("@Password", SqlDbType.VarChar, 50))  
                    cmd.Parameters.Add(new SqlParameter("@FormPerson", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@FormPhone", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@FormPhoneExt", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@FormEmail", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@ContactPhone", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@ContactPhoneExt", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@ContactEmail", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@AllImm", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MedExmp", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BeleExmp", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@NoImm", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TotNo", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Polio", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@DTP", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MMRDose2", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@HepB", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@VZV", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    // Added below by A.T. on 08/27/2014
                    //cmd.Parameters.Add(New SqlParameter("@PBE_PreJanuaryExmpt", SqlDbType.Int)) 'Commented out by AT on 06/01/2015 per Teressa Lee's request
                    cmd.Parameters.Add(new SqlParameter("@PBE_HealthCareExmpt", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PBE_ReligiousExmpt", SqlDbType.Int));

                    // Added EnrolledButNotAttending by A.T. on 08/18/2015
                    cmd.Parameters.Add(new SqlParameter("@EnrolledButNotAttending", SqlDbType.Int));

                    cmd.Parameters("@id").Value = Session["K_Assessment_id"];
                    if ((string.IsNullOrEmpty(hdnSubmitDate.Value)))
                    {
                        cmd.Parameters("@SubmitDate").Value = reviseDate;
                    }
                    else {
                        cmd.Parameters("@SubmitDate").Value = hdnSubmitDate.Value;
                    }
                    // Commented out by A.T. on 08/27/2014
                    //cmd.Parameters("@Password").Value = txtpin.Text 
                    cmd.Parameters("@FormPerson").Value = Strings.UCase(txtStaffPrsn.Text);
                    cmd.Parameters("@FormPhone").Value = txtStaffPhNo.Text;
                    cmd.Parameters("@FormPhoneExt").Value = txtStaffPhNoExt.Text;
                    cmd.Parameters("@FormEmail").Value = Strings.UCase(txtmail.Text);
                    cmd.Parameters("@ContactPerson").Value = Strings.UCase(txtfcperson.Text);
                    cmd.Parameters("@ContactPhone").Value = txtfcNumber.Text;
                    cmd.Parameters("@ContactPhoneExt").Value = txtfcNumberExt.Text;
                    cmd.Parameters("@ContactEmail").Value = Strings.UCase(txtfcemail.Text);
                    cmd.Parameters("@AllImm").Value = Convert.ToInt32(txtAllimm.Text);
                    cmd.Parameters("@MedExmp").Value = Convert.ToInt32(txtPermMedExmp.Text);
                    cmd.Parameters("@BeleExmp").Value = Convert.ToInt32(txtPerBelExmp.Text);
                    cmd.Parameters("@NoImm").Value = Convert.ToInt32(txtNoimm.Text);
                    cmd.Parameters("@TotNo").Value = Convert.ToInt32(txttotno.Text);
                    cmd.Parameters("@Polio").Value = Convert.ToInt32(txtPolio.Text);
                    cmd.Parameters("@DTP").Value = Convert.ToInt32(txtDtp.Text);
                    cmd.Parameters("@MMRDose2").Value = Convert.ToInt32(txtMMR2.Text);
                    cmd.Parameters("@HepB").Value = Convert.ToInt32(txtHepb.Text);
                    cmd.Parameters("@VZV").Value = Convert.ToInt32(txtVZV.Text);

                    // Added below by A.T. on 08/27/2014
                    //cmd.Parameters("@PBE_PreJanuaryExmpt").Value = CInt(TxtPreJanuaryExmpt.Text) 'Commented out by AT on 06/01/2015 per Teressa Lee's request
                    cmd.Parameters("@PBE_HealthCareExmpt").Value = Convert.ToInt32(TxtHealthCareExmpt.Text);
                    cmd.Parameters("@PBE_ReligiousExmpt").Value = Convert.ToInt32(TxtReligiousExmpt.Text);

                    // Added below by A.T. on 08/18/2015
                    cmd.Parameters("@EnrolledButNotAttending").Value = Convert.ToInt32(TextboxEnrolledButNotAttending.Text);


                    cmd.Parameters("@ReviseDate").Value = hdnReviseDate.Value;
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

                    Response.Redirect("ReviewAndSubmit.aspx", true);


                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    if ((ret == 1))
                    {
                        //logaudittrail("isComplete", hdnIsComplete.Value, "Y");
                        /* work later 06/28/2016 AT
                        if ((hdnSubmitDate.Value != txtDate.Text))
                        {
                            //logaudittrail("SubmitDate", hdnSubmitDate.Value, txtDate.Text);
                        }
                        */
                        if ((hdnTotNo.Value != txttotno.Text))
                        {
                            //logaudittrail("TotNo", hdnTotNo.Value, txttotno.Text);
                        }
                        if ((hdnAllImm.Value != txtAllimm.Text))
                        {
                            //logaudittrail("AllImm", hdnAllImm.Value, txtAllimm.Text);
                        }
                        if ((hdnMedExmp.Value != txtPermMedExmp.Text))
                        {
                            //logaudittrail("MedExmp", hdnMedExmp.Value, txtPermMedExmp.Text);
                        }
                        if ((hdnPerExmp.Value != txtBelExmp.Text))
                        {
                            //logaudittrail("BeleExmp", hdnPerExmp.Value, txtPerBelExmp.Text);
                        }
                        if ((hdnNoImm.Value != txtNoimm.Text))
                        {
                            //logaudittrail("NoImm", hdnNoImm.Value, txtNoimm.Text);
                        }
                        if ((hdnPolio.Value != txtPolio.Text))
                        {
                            //logaudittrail("Polio", hdnPolio.Value, txtPolio.Text);
                        }
                        if ((hdnDTP.Value != txtDtp.Text))
                        {
                            //logaudittrail("DTP-DTAP-DT", hdnDTP.Value, txtDtp.Text);
                        }
                        if ((hdnMMR2.Value != txtMMR2.Text))
                        {
                            //logaudittrail("MMRDose2", hdnMMR2.Value, txtMMR2.Text);
                        }
                        if ((hdnHepB.Value != txtHepb.Text))
                        {
                            //logaudittrail("HepB", hdnHepB.Value, txtHepb.Text);
                        }
                        if ((hdnVZV.Value != txtVZV.Text))
                        {
                            //logaudittrail("VZV", hdnVZV.Value, txtVZV.Text);
                        }
                       

                        //Added below by A.T. on 08/22/2014
                        //If (hdnTxtPreJanuaryExmpt.Value <> TxtPreJanuaryExmpt.Text) Then
                        // logaudittrail("PBE_PreJanuaryExmpt", hdnTxtPreJanuaryExmpt.Value, TxtPreJanuaryExmpt.Text)
                        //End If

                        /* work later 06/28/2016 AT
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

                        Response.Redirect("ReviewAndSubmit.aspx", true);
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








    }
}
