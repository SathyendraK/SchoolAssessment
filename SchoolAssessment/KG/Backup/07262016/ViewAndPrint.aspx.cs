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
                btnprint.Attributes.Add("onclick", "window.print()");
                FillInData();
            }

        }


        private void FillInData()
        {

            dynamic id = Session["K_Assessment_id"];
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE S.id = '" + id + "' AND A.SchoolYear = '" + SchoolYear + "'";



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
                    lbsStaffPhone.Text = reader["ReportedPhone"].ToString();
                    lblStaffPhoneExt.Text = reader["ReportedPhoneExt"].ToString();
                    LblStaffEmail.Text = reader["ReportedEmail"].ToString();
                    lblDesContactName.Text = reader["ContactPerson"].ToString();
                    lblDesContactPhone.Text = reader["ContactPhone"].ToString();
                    lblDesPhoneExt.Text = reader["ContactPhoneExt"].ToString();
                    lblDesContactEmail.Text = reader["ContactEmail"].ToString();
                    txtAllimm.Text = reader["AllImm"].ToString();
                    txtPermMedExmp.Text = reader["MedExmp"].ToString();
                    txtBelExmp.Text = reader["BeleExmp"].ToString();
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

                    // Added below by A.T. on 08/27/2014
                    //TxtPreJanuaryExmpt.Text = Trim(reader["PBE_PreJanuaryExmpt"].ToString()) 'Commented out by AT on 06/01/2015
                    //TxtHealthCareExmpt.Text = reader["PBE_HealthCareExmpt"].ToString());
                    //TxtReligiousExmpt.Text = reader["PBE_ReligiousExmpt"].ToString());

                    // Added below by A.T. on 08/18/2015
                    TextEnrolledButNotAttending.Text = reader["EnrolledButNotAttending"].ToString();


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
                    txtBelExmp.Enabled = false;
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

                    // Added below by A.T. on 08/27/2014
                    //TxtPreJanuaryExmpt.Enabled = False 'Commented out by AT on 06/01/2015
                    //TxtHealthCareExmpt.Enabled = false;
                    //TxtReligiousExmpt.Enabled = false;

                    // Added below by A.T. on 08/18/2015
                    TextEnrolledButNotAttending.Enabled = false;

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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (((Session["K_Assessment_id"] == null)))
            {
                Response.Redirect("Login.aspx?reason=TimedOut", true);
            }
            else {
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                dynamic id = Session["K_Assessment_id"];
                string sql = null;
                int ret = 0;
                DateTime LastResetDate = DateTime.Now;


                //string LastResetDate = System.DateTime.Now();
                // Added by A.T. so that 4 questions are editable at Revise Report. 

                // ReviseReport button moved from original LoginConfirmed.aspx to ViewAndPrint.aspx, so not calling Save()
                //Save();
                sql = "UPDATE Assessments SET ReviseSubmittedRPT = 'Y', LhdReviseDate = GetDate() WHERE id = '" + id + "' AND SchoolYear = '" + SchoolYear + "'";


                try
                {
                    
                    //con.Open();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    // Steve and Teresa decided not to set 
                    // sql = "UPDATE K_Assessment SET isComplete = 'N', LhdReviseDate = '" & LastResetDate & "', LastResetDate = '" & LastResetDate & "' WHERE id = " & id
                    //cmd = new SqlCommand(sql, con);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    if ((ret == 1))
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
                
                Response.Redirect("ReportContactInfo.aspx", true);
            }
        }

        protected void hdrLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("K_Assessment_id");
            Response.Redirect("Login.aspx", true);
        }
    }
}