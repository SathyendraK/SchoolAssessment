using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

/*
 *  06/25/2018 AT Page_Load Copy MailAddress = PhysAddress is MailAddress is empty 
 * 
 * */

namespace SchoolAssessment.Admin
{
    public partial class AdminEditSchoolKG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this school?')");

            if ((Page.IsPostBack == false & !string.IsNullOrEmpty(Request.QueryString["id"])))
            {
                dynamic id = Request.QueryString["id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string sql = "SELECT A.*, C.*, S.*, D.* FROM Assessments A INNER JOIN Schools S ON S.id = A.id LEFT JOIN COUNTIES C ON C.CoCode = S.CoCode LEFT JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = " + id + "";
                SqlCommand cmd = new SqlCommand(sql, con);

                string region = null;
                string status = null;
                try
                {
                    
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        hdnSchoolName.Value = reader["SchName"].ToString().ToUpper();
                        hdnSchCode.Value = reader["SchCode"].ToString().ToUpper();
                        hdnCoName.Value = reader["CoName"].ToString().ToUpper();
                        hdnCoCode.Value = reader["CoCode"].ToString().ToUpper();
                        hdnDistCode.Value = reader["DistCode"].ToString().ToUpper();
                        hdnDistName.Value = reader["DistName"].ToString().ToUpper();
                        //hdnSPACode.Value = reader["SPACode"].ToString().ToUpper();
                        hdnSchoolPhone.Value = reader["SchPhone"].ToString().ToUpper();
                        hdnSchAdmin.Value = reader["SchAdmin"].ToString().ToUpper();
                        hdnSchEmail.Value = reader["SchEmail"].ToString().ToUpper();
                        hdnPhysStreet.Value = reader["PhysStreet"].ToString().ToUpper();
                        hdnPhysCity.Value = reader["PhysCity"].ToString().ToUpper();
                        hdnPhysZip.Value = reader["PhysZip"].ToString().ToUpper();
                        hdnMailStreet.Value = reader["MailStreet"].ToString().ToUpper();
                        hdnMailCity.Value = reader["MailCity"].ToString().ToUpper();
                        hdnMailZip.Value = reader["MailZip"].ToString().ToUpper();
                        hdnContactEmail.Value = reader["ContactEmail"].ToString().ToUpper();
                        hdnContactPerson.Value = reader["ContactPerson"].ToString().ToUpper();
                        hdnContactPhone.Value = reader["ContactPhone"].ToString().ToUpper();
                        hdnContactPhoneExt.Value = reader["ContactPhoneExt"].ToString().ToUpper();
                        hdnFormEmail.Value = reader["ReportedEmail"].ToString().ToUpper();
                        hdnFormPerson.Value = reader["ReportedPerson"].ToString().ToUpper();
                        hdnFormPhone.Value = reader["ReportedPhone"].ToString().ToUpper();
                        hdnFormPhoneExt.Value = reader["ReportedPhoneExt"].ToString().ToUpper();
                        hdnKinderYesNo.Value = reader["StudentYesNo"].ToString().ToUpper();
                        hdnReason.Value = reader["Reason"].ToString().ToUpper();
                        hdnMemo.Value = reader["Memo"].ToString();
                        hdnSuperintendentName.Value = reader["SuperintendentName"].ToString().ToUpper();
                        hdnSuperintendentEmail.Value = reader["SuperintendentEmail"].ToString().ToUpper();
                        hdnSuperintendentPhone.Value = reader["SuperintendentPhone"].ToString().ToUpper();

                        hdnIsCharter.Value = reader["isCharter"].ToString().ToUpper();
                        hdnSchType.Value = reader["SchType"].ToString().ToUpper();
                        hdnSchoolYear.Value = reader["SchoolYear"].ToString().ToUpper();

                        hdnSubmitDate.Value = reader["SubmitDate"].ToString().ToUpper();
                        hdnIsComplete.Value = reader["isComplete"].ToString().ToUpper();
                        hdnLastResetDate.Value = reader["EditDate"].ToString().ToUpper(); // LastResetDate --> EditDate on 08/17/2016
                        hdnReviseDate.Value = reader["ReviseDate"].ToString().ToUpper();
                        hdnLhdReviseDate.Value = reader["LhdReviseDate"].ToString().ToUpper();

                        status = reader["isComplete"].ToString().ToUpper();

                        lblpin.Text = reader["Password"].ToString().ToUpper();
                        region = reader["RegionCode"].ToString().ToUpper();
                        txtSchoolName.Text = reader["SchName"].ToString().ToUpper();
                        txtSchoolCode.Text = reader["SchCode"].ToString().ToUpper();
                        txtCounty.Text = reader["CoName"].ToString().ToUpper();
                        txtCountyCode.Text = reader["CoCode"].ToString().ToUpper();
                        txtDistrictCode.Text = reader["DistCode"].ToString().ToUpper();
                        txtDistrict.Text = reader["DistName"].ToString().ToUpper();
                        //txtSPACode.Text = reader["SPACode"].ToString().ToUpper();
                        txtSchoolPhone.Text = reader["SchPhone"].ToString().ToUpper();
                        txtPhyAddress.Text = reader["PhysStreet"].ToString().ToUpper();
                        txtPhyCity.Text = reader["PhysCity"].ToString().ToUpper();
                        txtPhyZip.Text = reader["PhysZip"].ToString().ToUpper();
                        txtMailAddress.Text = reader["MailStreet"].ToString().ToUpper();
                        txtMailCity.Text = reader["MailCity"].ToString().ToUpper();
                        txtMailZip.Text = reader["MailZip"].ToString().ToUpper();
                        txtContactEmail.Text = reader["ContactEmail"].ToString().ToUpper();
                        txtContactPerson.Text = reader["ContactPerson"].ToString().ToUpper();

                        string txtContactPhone_str = (String)reader["ContactPhone"].ToString();
                        if (txtContactPhone_str.Length == 10)
                        {
                            txtContactPhone.Text = (String)(reader["ContactPhone"].ToString().Substring(0, 3));
                            txtContactPhone_1.Text = (String)(reader["ContactPhone"].ToString().Substring(3, 3));
                            txtContactPhone_2.Text = (String)reader["ContactPhone"].ToString().Substring(6);

                        }
                        else
                        {
                            txtContactPhone.Text = "";
                            txtContactPhone_1.Text = "";
                            txtContactPhone_2.Text = "";
                        }

                        //txtContactPhone.Text = reader["ContactPhone"].ToString().ToUpper();

                        txtContactPhoneExt.Text = reader["ContactPhoneExt"].ToString().ToUpper();
                        txtFormEmail.Text = reader["ReportedEmail"].ToString().ToUpper();
                        txtFormPerson.Text = reader["ReportedPerson"].ToString().ToUpper();
                        //txtFormPhone.Text = reader["ReportedPhone"].ToString().ToUpper();

                        string txtFormPhone_str = (String)reader["ReportedPhone"].ToString();
                        if (txtFormPhone_str.Length == 10)
                        {
                            txtFormPhone.Text = (String)(reader["ReportedPhone"].ToString().Substring(0, 3));
                            txtFormPhone_1.Text = (String)(reader["ReportedPhone"].ToString().Substring(3, 3));
                            txtFormPhone_2.Text = (String)reader["ReportedPhone"].ToString().Substring(6);

                        }
                        else
                        {
                            txtFormPhone.Text = "";
                            txtFormPhone_1.Text = "";
                            txtFormPhone_2.Text = "";
                        }

                        txtFormPhoneExt.Text = reader["ReportedPhoneExt"].ToString().ToUpper();
                        txtMemo.Text = reader["Memo"].ToString();
                        txtSchAdmin.Text = reader["SchAdmin"].ToString().ToUpper();
                        txtSchEmail.Text = reader["SchEmail"].ToString().ToUpper();
                        txtSuperintendentName.Text = reader["SuperintendentName"].ToString().ToUpper();
                        txtSuperintendentEmail.Text = reader["SuperintendentEmail"].ToString().ToUpper();
                        txtSuperintendentPhone.Text = reader["SuperintendentPhone"].ToString().ToUpper();

                        cmbCharter.SelectedValue = reader["isCharter"].ToString().ToUpper();
                        cmbSchoolType.SelectedValue = reader["SchType"].ToString().ToUpper();
                        litSchoolYear.Text = reader["SchoolYear"].ToString().ToUpper();

                        drpdwnKndrYesNo.SelectedValue = reader["StudentYesNo"].ToString();
                        drpReason.SelectedValue = reader["Reason"].ToString().ToUpper();
                    }

                    if ((drpdwnKndrYesNo.SelectedValue == "No"))
                    {
                        drpReason.Enabled = true;
                        reqReason.Enabled = true;
                        reqFormPerson.Enabled = true;
                        reqFormEmail.Enabled = true;
                        //reqFormPhone.Enabled = true;
                        txtFormPhone.Enabled = true;
                        txtFormPhone_1.Enabled = true;
                        txtFormPhone_2.Enabled = true;
                        reqContactPerson.Enabled = true;
                        reqContactEmail.Enabled = true;
                        txtContactPhone.Enabled = true;
                    }
                    else {
                        drpReason.Enabled = false;
                        reqReason.Enabled = false;
                    }


                    if ((status == "Y"))
                    {
                        lblStatus.Text = "Completed";
                        drpReason.Enabled = false;
                        reqReason.Enabled = false;
                        drpdwnKndrYesNo.Enabled = false;

                        txtFormPerson.Enabled = false; // added below on 9/20/2017
                        txtFormEmail.Enabled = false;
                        txtFormPhoneExt.Enabled = false;
                        txtFormPhone.Enabled = false;
                        txtFormPhone_1.Enabled = false;
                        txtFormPhone_2.Enabled = false;
                        drpdwnKndrYesNo.Enabled = false;
                        drpReason.Enabled = false;

                    }
                    else {
                        lblStatus.Text = "Not Completed";
                        txtFormPerson.Enabled = false;
                        txtFormEmail.Enabled = false;
                        txtFormPhone.Enabled = false;
                        txtFormPhoneExt.Enabled = false;
                        reqFormPerson.Enabled = false;
                        reqFormEmail.Enabled = false;
                        //reqFormPhone.Enabled = false;
                        txtFormPhone.Enabled = false;
                        txtFormPhone_1.Enabled = false;
                        txtFormPhone_2.Enabled = false;
                        reqContactPerson.Enabled = false;
                        reqContactEmail.Enabled = false;
                        drpdwnKndrYesNo.Enabled = false; // 11/01/2021
                        drpReason.Enabled = false; // 11/01/2021 
                        //txtContactPhone.Enabled = false;
                    }

                    /* Automatically copy Physical Address = Mailing Address if Mailing Address is empty.  Kristen Sy's request*/
                    if (txtMailAddress.Text == "" || txtMailCity.Text == "" || txtMailZip.Text == "")
                    {
                        txtMailAddress.Text = txtPhyAddress.Text;
                        txtMailCity.Text = txtPhyCity.Text;
                        txtMailZip.Text = txtPhyZip.Text;
                    }



                    if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "59" && txtCountyCode.Text != "01" && txtPhyCity.Text != "BERKELEY"))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "59" && txtCountyCode.Text == "01" && txtPhyCity.Text == "BERKELEY"))
                    {
                        // do nothing, this is okay
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "01" && txtCountyCode.Text == "01" && txtPhyCity.Text == "BERKELEY"))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "60" && txtCountyCode.Text != "19" && txtPhyCity.Text != "LONG BEACH"))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "60" && txtCountyCode.Text == "19" && txtPhyCity.Text == "LONG BEACH"))
                    {
                        // do nothing, this case is okay
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && Session["AdminCoCode"].ToString() == "19" && txtCountyCode.Text == "19" && txtPhyCity.Text == "LONG BEACH"))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }
                    else if ((Session["AdminUserType"].ToString() == "LHD" && txtCountyCode.Text != Session["AdminCoCode"].ToString()))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }
                    else if ((Session["AdminUserType"].ToString() == "FIELDREP" && region != Session["AdminRegionCode"].ToString()))
                    {
                        Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
                    }

                    if ((Session["AdminUserType"].ToString() != "ADMIN"))
                    {
                        btnDelete.Visible = false;
                        // Commented out to allow LHDs to reset submission
                        // btnReset.Visible = False
                        txtSchoolCode.ReadOnly = true;

                        if ((cmbSchoolType.SelectedValue == "PUBLIC"))
                        {
                            cmbSchoolType.Items.FindByValue("PRIVATE").Enabled = false;
                        }
                        else {
                            cmbSchoolType.Items.FindByValue("PUBLIC").Enabled = false;
                        }

                        if ((cmbCharter.SelectedValue == "Y"))
                        {
                            cmbCharter.Items.FindByValue("N").Enabled = false;
                        }
                        else {
                            cmbCharter.Items.FindByValue("Y").Enabled = false;
                        }
                        txtCounty.ReadOnly = true;
                        txtCounty.CssClass = "aspNetDisabled";
                        txtCountyCode.ReadOnly = true;
                        txtCountyCode.CssClass = "aspNetDisabled";
                        txtDistrict.ReadOnly = true;
                        txtDistrict.CssClass = "aspNetDisabled";
                        txtDistrictCode.ReadOnly = true;
                        txtDistrictCode.CssClass = "aspNetDisabled";
                        txtSchoolCode.ReadOnly = true;
                        txtSchoolCode.CssClass = "aspNetDisabled";

                        //litSchAdmin.Text = txtSchAdmin.Text
                        //litSchEmail.Text = txtSchEmail.Text
                        //txtSchAdmin.Visible = False
                        //txtSchEmail.Visible = False

                        litSuperintendentName.Text = txtSuperintendentName.Text;
                        litSuperintendentEmail.Text = txtSuperintendentEmail.Text;
                        litSuperintendentPhone.Text = txtSuperintendentPhone.Text;
                        txtSuperintendentName.Visible = false;
                        txtSuperintendentEmail.Visible = false;
                        txtSuperintendentPhone.Visible = false;

                        if ((System.Configuration.ConfigurationManager.AppSettings["allowLHDToEdit"] == "False"))
                        {
                            btnSubmit.Enabled = false;
                            // btnReset.Enabled = False 'commented out by AT on 10/07/2014
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
                    cmd = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    //reader = null;
                }
            }
            
            if ((Page.IsPostBack == true & hdnIsComplete.Value != "Y"))
            {
                drpReason.Focus();
                if ((drpdwnKndrYesNo.SelectedValue == "No"))
                {
                    drpReason.Enabled = true;
                    reqReason.Enabled = true;
                    reqFormPerson.Enabled = true;
                    reqFormEmail.Enabled = true;
                    //reqFormPhone.Enabled = true;
                    txtFormPerson.Enabled = true;
                    txtFormEmail.Enabled = true;
                    txtFormPhone.Enabled = true;
                    txtFormPhone_1.Enabled = true;
                    txtFormPhone_2.Enabled = true;
                    txtFormPhoneExt.Enabled = true;
                    reqContactPerson.Enabled = true;
                    reqContactEmail.Enabled = true;
                    txtContactPhone.Enabled = true;
                }
                else {
                    drpReason.Enabled = false;
                    reqReason.Enabled = false;
                    txtFormPerson.Enabled = false;
                    txtFormEmail.Enabled = false;
                    txtFormPhone.Enabled = false;
                    txtFormPhone_1.Enabled = false;
                    txtFormPhone_2.Enabled = false;
                    txtFormPhoneExt.Enabled = false;
                    reqContactPerson.Enabled = false;
                    reqContactEmail.Enabled = false;
                    txtContactPhone.Enabled = false;
                    reqFormPerson.Enabled = false;
                    reqFormEmail.Enabled = false;
                    txtFormPerson.Text = "";
                    txtFormEmail.Text = "";
                    txtFormPhone.Text = "";
                    txtFormPhone_1.Text = "";
                    txtFormPhone_2.Text = "";
                    txtFormPhoneExt.Text = "";
                    drpReason.SelectedValue = "";
                }
            }

        }


        public void schoolDistrictValidate(object source, ServerValidateEventArgs args)
        {
            if ((cmbSchoolType.SelectedValue == "PUBLIC" & string.IsNullOrEmpty(txtDistrict.Text)))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        // Added by AT on 09/17/2015
        public void districtCodeValide(object source, ServerValidateEventArgs args)
        {
            if ((cmbSchoolType.SelectedValue == "PUBLIC" & string.IsNullOrEmpty(txtDistrictCode.Text)))
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["K_Sorted"] = "Y";
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            Response.Redirect("AdminReportKG.aspx", true);
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
                dynamic id = Request.QueryString["id"];
                dynamic f_id = Session["F_Assessment_id"];
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
               

                int ret = 0, ret2 = 0, ret_f =0, ret2_f=0;
                string sql1 = "", sql2 = "", sql1_f="", sql2_f=""; 

                string newSubmit = "False";
                DateTime curDateTime = DateTime.Now; 
                
                try
                {
                    if ((hdnIsComplete.Value == "N" & drpdwnKndrYesNo.SelectedValue == "No"))
                    {
                        //sql = "UPDATE K_Assessment SET SchCode = @SchCode, CoCode =  @CoCode, DistCode = @DistCode, SPACode = @SPACode, CoName = @CoName, DistName = @DistName, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, KinderYesNo = @KinderYesNo, Reason = @Reason, isComplete = 'Y', SubmitDate = @SubmitDate, Password = @Password, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, [DTP-DTAP-DT] = @DTP, MMRDose1 = @MMRDose1, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, Memo = @Memo, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, LhdReviseDate = @LhdReviseDate WHERE id = " + id;
                        sql1 = "UPDATE Schools SET CoCode = @CoCode, DistCode = @DistCode, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, Password = @Password, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, Editdate = Getdate() WHERE id IN (SELECT id FROM Assessments WHERE Assmntid = " + id + ")";
                        sql2 = "UPDATE Assessments SET StudentYesNo = @StudentYesNo, Reason = @Reason, isComplete = 'Y', SubmitDate = @SubmitDate, ReportedPerson = @ReportedPerson, ReportedPhone = @ReportedPhone, ReportedPhoneExt = @ReportedPhoneExt, ReportedEmail = @ReportedEmail,  AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, DTP_DTAP_DT = @DTP, MMRDose1 = @MMRDose1, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, Memo = @Memo,  LhdReviseDate = @LhdReviseDate, Editdate = Getdate() WHERE Assmntid = " + id + " AND SchoolYear = " + SchoolYear;
                        sql1_f = "UPDATE Schools SET CoCode = @CoCode, DistCode = @DistCode, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, Password = @Password, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, Editdate = Getdate() WHERE id IN (SELECT id FROM Assessments WHERE Assmntid = " + f_id + ")";
                        sql2_f = "UPDATE Assessments SET StudentYesNo = @StudentYesNo, Reason = @Reason, isComplete = 'Y', SubmitDate = @SubmitDate, ReportedPerson = @ReportedPerson, ReportedPhone = @ReportedPhone, ReportedPhoneExt = @ReportedPhoneExt, ReportedEmail = @ReportedEmail,  AllImm = @AllImm, MedExmp = @MedExmp, BeleExmp = @BeleExmp, NoImm = @NoImm, TotNo = @TotNo, Polio = @Polio, DTP_DTAP_DT = @DTP, MMRDose1 = @MMRDose1, MMRDose2 = @MMRDose2, HepB = @HepB, VZV = @VZV, Memo = @Memo,  LhdReviseDate = @LhdReviseDate, Editdate = Getdate() WHERE Assmntid = " + f_id + " AND SchoolYear = " + SchoolYear;


                        newSubmit = "True";
                    }
                    else {
                        //sql = "UPDATE K_Assessment SET SchCode = @SchCode, CoCode =  @CoCode, DistCode = @DistCode, SPACode = @SPACode, CoName = @CoName, DistName = @DistName, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, KinderYesNo = @KinderYesNo, Reason = @Reason, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, FormPerson = @FormPerson, FormPhone = @FormPhone, FormPhoneExt = @FormPhoneExt, FormEmail = @FormEmail, Memo = @Memo, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, LhdReviseDate = @LhdReviseDate WHERE id = " + id;
                        sql1 = "UPDATE Schools SET CoCode = @CoCode, DistCode = @DistCode, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, Editdate = Getdate()  WHERE id IN (SELECT id FROM Assessments WHERE Assmntid = " + id + ")";
                        sql2 = "UPDATE Assessments SET StudentYesNo = @StudentYesNo, Reason = @Reason, ReportedPerson = @ReportedPerson, ReportedPhone = @ReportedPhone, ReportedPhoneExt = @ReportedPhoneExt, ReportedEmail = @ReportedEmail, Memo = @Memo,  LhdReviseDate = @LhdReviseDate, Editdate = Getdate() WHERE Assmntid = " + id + " AND SchoolYear = " + SchoolYear;
                        sql1_f = "UPDATE Schools SET CoCode = @CoCode, DistCode = @DistCode, SchName = @SchName, PhysStreet = @PhysStreet, PhysCity = @PhysCity, PhysZip = @PhysZip, MailStreet = @MailStreet, MailCity = @MailCity, MailZip = @MailZip, SchPhone = @SchPhone, SchType = @SchType, isCharter = @isCharter, ContactPerson = @ContactPerson, ContactPhone = @ContactPhone, ContactPhoneExt = @ContactPhoneExt, ContactEmail = @ContactEmail, SchAdmin = @SchAdmin, SchEmail = @SchEmail, SuperintendentName = @SuperintendentName, SuperintendentEmail = @SuperintendentEmail, SuperintendentPhone = @SuperintendentPhone, Editdate = Getdate()  WHERE id IN (SELECT id FROM Assessments WHERE Assmntid = " + f_id + ")";
                        sql2_f = "UPDATE Assessments SET StudentYesNo = @StudentYesNo, Reason = @Reason, ReportedPerson = @ReportedPerson, ReportedPhone = @ReportedPhone, ReportedPhoneExt = @ReportedPhoneExt, ReportedEmail = @ReportedEmail, Memo = @Memo,  LhdReviseDate = @LhdReviseDate, Editdate = Getdate() WHERE Assmntid = " + f_id + " AND SchoolYear = " + SchoolYear;

                    }

                    //cmd = new SqlCommand(sql, con);
                    SqlCommand cmd = new SqlCommand(sql1, con);
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    SqlCommand cmd_f = new SqlCommand(sql1_f, con);
                    SqlCommand cmd2_f = new SqlCommand(sql2_f, con);

                    //cmd.Parameters.AddWithValue("@SchCode", txtSchoolCode.Text);  //right function Strings.Right("0000000" + txtSchoolCode.Text, 7);
                    cmd.Parameters.AddWithValue("@CoCode", txtCountyCode.Text);
                    cmd.Parameters.AddWithValue("@DistCode", txtDistrictCode.Text);
                    cmd.Parameters.AddWithValue("@CoName", txtCounty.Text);
                    cmd.Parameters.AddWithValue("@DistName", txtDistrict.Text);
                    cmd.Parameters.AddWithValue("@SchName", txtSchoolName.Text);
                    cmd.Parameters.AddWithValue("@PhysStreet", txtPhyAddress.Text);
                    cmd.Parameters.AddWithValue("@PhysCity", txtPhyCity.Text);
                    cmd.Parameters.AddWithValue("@PhysZip", txtPhyZip.Text);
                    cmd.Parameters.AddWithValue("@MailStreet", txtMailAddress.Text);
                    cmd.Parameters.AddWithValue("@MailCity", txtMailCity.Text);
                    cmd.Parameters.AddWithValue("@MailZip", txtMailZip.Text);
                    cmd.Parameters.AddWithValue("@SchPhone", txtSchoolPhone.Text);
                    cmd.Parameters.AddWithValue("@SchAdmin", txtSchAdmin.Text);
                    cmd.Parameters.AddWithValue("@SchEmail", txtSchEmail.Text);
                    cmd.Parameters.AddWithValue("@SchType", cmbSchoolType.SelectedValue);
                    cmd.Parameters.AddWithValue("@isCharter", cmbCharter.SelectedValue);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                    cmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text + txtContactPhone_1.Text + txtContactPhone_2.Text);
                    cmd.Parameters.AddWithValue("@ContactPhoneExt", txtContactPhoneExt.Text);
                    cmd.Parameters.AddWithValue("@ContactEmail", txtContactEmail.Text);
                    cmd2.Parameters.AddWithValue("@ReportedPerson", txtFormPerson.Text);
                    cmd2.Parameters.AddWithValue("@ReportedPhone", txtFormPhone.Text + txtFormPhone_1.Text + txtFormPhone_2.Text);
                    cmd2.Parameters.AddWithValue("@ReportedPhoneExt", txtFormPhoneExt.Text);
                    cmd2.Parameters.AddWithValue("@ReportedEmail", txtFormEmail.Text);
                    cmd2.Parameters.AddWithValue("@StudentYesNo", drpdwnKndrYesNo.SelectedValue);
                    cmd2.Parameters.AddWithValue("@Reason", drpReason.SelectedValue);
                    cmd2.Parameters.AddWithValue("@Memo", txtMemo.Text);
                    cmd.Parameters.AddWithValue("@SuperintendentName", txtSuperintendentName.Text);
                    cmd.Parameters.AddWithValue("@SuperintendentEmail", txtSuperintendentEmail.Text);
                    cmd.Parameters.AddWithValue("@SuperintendentPhone", txtSuperintendentPhone.Text);
                    cmd2.Parameters.AddWithValue("@LhdReviseDate", curDateTime);
                    cmd2.Parameters.AddWithValue("@SubmitDate", curDateTime);

                    //cmd.Parameters.AddWithValue("@SubmitDate", curDateTime);
                    cmd.Parameters.AddWithValue("@Password", "0000");

                    cmd2.Parameters.AddWithValue("@AllImm", 0);
                    cmd2.Parameters.AddWithValue("@MedExmp", 0);
                    cmd2.Parameters.AddWithValue("@BeleExmp", 0);
                    cmd2.Parameters.AddWithValue("@NoImm", 0);
                    cmd2.Parameters.AddWithValue("@TotNo", 0);
                    cmd2.Parameters.AddWithValue("@Polio", 0);
                    cmd2.Parameters.AddWithValue("@DTP", 0);
                    cmd2.Parameters.AddWithValue("@MMRDose1", 0);
                    cmd2.Parameters.AddWithValue("@MMRDose2", 0);
                    cmd2.Parameters.AddWithValue("@HepB", 0);
                    cmd2.Parameters.AddWithValue("@VZV", 0);

                    //--------
                    cmd_f.Parameters.AddWithValue("@CoCode", txtCountyCode.Text);
                    cmd_f.Parameters.AddWithValue("@DistCode", txtDistrictCode.Text);
                    cmd_f.Parameters.AddWithValue("@CoName", txtCounty.Text);
                    cmd_f.Parameters.AddWithValue("@DistName", txtDistrict.Text);
                    cmd_f.Parameters.AddWithValue("@SchName", txtSchoolName.Text);
                    cmd_f.Parameters.AddWithValue("@PhysStreet", txtPhyAddress.Text);
                    cmd_f.Parameters.AddWithValue("@PhysCity", txtPhyCity.Text);
                    cmd_f.Parameters.AddWithValue("@PhysZip", txtPhyZip.Text);
                    cmd_f.Parameters.AddWithValue("@MailStreet", txtMailAddress.Text);
                    cmd_f.Parameters.AddWithValue("@MailCity", txtMailCity.Text);
                    cmd_f.Parameters.AddWithValue("@MailZip", txtMailZip.Text);
                    cmd_f.Parameters.AddWithValue("@SchPhone", txtSchoolPhone.Text);
                    cmd_f.Parameters.AddWithValue("@SchAdmin", txtSchAdmin.Text);
                    cmd_f.Parameters.AddWithValue("@SchEmail", txtSchEmail.Text);
                    cmd_f.Parameters.AddWithValue("@SchType", cmbSchoolType.SelectedValue);
                    cmd_f.Parameters.AddWithValue("@isCharter", cmbCharter.SelectedValue);
                    cmd_f.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                    cmd_f.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text + txtContactPhone_1.Text + txtContactPhone_2.Text);
                    cmd_f.Parameters.AddWithValue("@ContactPhoneExt", txtContactPhoneExt.Text);
                    cmd_f.Parameters.AddWithValue("@ContactEmail", txtContactEmail.Text);
                    cmd2_f.Parameters.AddWithValue("@ReportedPerson", txtFormPerson.Text);
                    cmd2_f.Parameters.AddWithValue("@ReportedPhone", txtFormPhone.Text + txtFormPhone_1.Text + txtFormPhone_2.Text);
                    cmd2_f.Parameters.AddWithValue("@ReportedPhoneExt", txtFormPhoneExt.Text);
                    cmd2_f.Parameters.AddWithValue("@ReportedEmail", txtFormEmail.Text);
                    cmd2_f.Parameters.AddWithValue("@StudentYesNo", drpdwnKndrYesNo.SelectedValue);
                    cmd2_f.Parameters.AddWithValue("@Reason", drpReason.SelectedValue);
                    cmd2_f.Parameters.AddWithValue("@Memo", txtMemo.Text);
                    cmd_f.Parameters.AddWithValue("@SuperintendentName", txtSuperintendentName.Text);
                    cmd_f.Parameters.AddWithValue("@SuperintendentEmail", txtSuperintendentEmail.Text);
                    cmd_f.Parameters.AddWithValue("@SuperintendentPhone", txtSuperintendentPhone.Text);
                    cmd2_f.Parameters.AddWithValue("@LhdReviseDate", curDateTime);
                    cmd2_f.Parameters.AddWithValue("@SubmitDate", curDateTime);

                    //cmd.Parameters.AddWithValue("@SubmitDate", curDateTime);
                    cmd_f.Parameters.AddWithValue("@Password", "0000");

                    cmd2_f.Parameters.AddWithValue("@AllImm", 0);
                    cmd2_f.Parameters.AddWithValue("@MedExmp", 0);
                    cmd2_f.Parameters.AddWithValue("@BeleExmp", 0);
                    cmd2_f.Parameters.AddWithValue("@NoImm", 0);
                    cmd2_f.Parameters.AddWithValue("@TotNo", 0);
                    cmd2_f.Parameters.AddWithValue("@Polio", 0);
                    cmd2_f.Parameters.AddWithValue("@DTP", 0);
                    cmd2_f.Parameters.AddWithValue("@MMRDose1", 0);
                    cmd2_f.Parameters.AddWithValue("@MMRDose2", 0);
                    cmd2_f.Parameters.AddWithValue("@HepB", 0);
                    cmd2_f.Parameters.AddWithValue("@VZV", 0);

                    /*
                    cmd.Parameters("@SchCode").Value = Strings.Right("0000000" + txtSchoolCode.Text, 7);
                    cmd.Parameters("@CoCode").Value = Strings.Right("00" + txtCountyCode.Text, 2);
                    cmd.Parameters("@DistCode").Value = Strings.Right("00000" + txtDistrictCode.Text, 5);
                    cmd.Parameters("@SPACode").Value = txtSPACode.Text;
                    cmd.Parameters("@CoName").Value = Strings.UCase(txtCounty.Text);
                    */


                    /*
                    if ((cmbSchoolType.SelectedValue == "PUBLIC"))
                    {
                        cmd.Parameters("@DistName").Value = Strings.UCase(txtDistrict.Text);
                    }
                    else {
                        cmd.Parameters("@DistName").Value = "";
                    }
                    cmd.Parameters("@SchName").Value = Strings.UCase(txtSchoolName.Text);
                    cmd.Parameters("@PhysStreet").Value = Strings.UCase(txtPhyAddress.Text);
                    cmd.Parameters("@PhysCity").Value = Strings.UCase(txtPhyCity.Text);
                    cmd.Parameters("@PhysZip").Value = txtPhyZip.Text;
                    cmd.Parameters("@MailStreet").Value = Strings.UCase(txtMailAddress.Text);
                    cmd.Parameters("@MailCity").Value = Strings.UCase(txtMailCity.Text);
                    cmd.Parameters("@MailZip").Value = Strings.UCase(txtMailZip.Text);
                    cmd.Parameters("@SchPhone").Value = txtSchoolPhone.Text;
                    cmd.Parameters("@SchAdmin").Value = txtSchAdmin.Text;
                    cmd.Parameters("@SchEmail").Value = txtSchEmail.Text;
                    cmd.Parameters("@SchType").Value = cmbSchoolType.SelectedValue;
                    cmd.Parameters("@isCharter").Value = cmbCharter.SelectedValue;
                    cmd.Parameters("@ContactPerson").Value = Strings.UCase(txtContactPerson.Text);
                    cmd.Parameters("@ContactPhone").Value = txtContactPhone.Text;
                    cmd.Parameters("@ContactPhoneExt").Value = txtContactPhoneExt.Text;
                    cmd.Parameters("@ContactEmail").Value = Strings.UCase(txtContactEmail.Text);
                    cmd.Parameters("@FormPerson").Value = Strings.UCase(txtFormPerson.Text);
                    cmd.Parameters("@FormPhone").Value = txtFormPhone.Text;
                    cmd.Parameters("@FormPhoneExt").Value = txtFormPhoneExt.Text;
                    cmd.Parameters("@FormEmail").Value = Strings.UCase(txtFormEmail.Text);
                    cmd.Parameters("@KinderYesNo").Value = drpdwnKndrYesNo.SelectedValue;
                    cmd.Parameters("@Reason").Value = drpReason.SelectedValue;
                    cmd.Parameters("@Memo").Value = txtMemo.Text;
                    cmd.Parameters("@SuperintendentName").Value = Strings.UCase(txtSuperintendentName.Text);
                    cmd.Parameters("@SuperintendentPhone").Value = txtSuperintendentPhone.Text;
                    cmd.Parameters("@SuperintendentEmail").Value = Strings.UCase(txtSuperintendentEmail.Text);

                    cmd.Parameters("@SubmitDate").Value = curDateTime;
                    cmd.Parameters("@LhdReviseDate").Value = curDateTime;

                    cmd.Parameters("@Password").Value = "0000";

                    cmd.Parameters("@AllImm").Value = 0;
                    cmd.Parameters("@MedExmp").Value = 0;
                    cmd.Parameters("@BeleExmp").Value = 0;
                    cmd.Parameters("@NoImm").Value = 0;
                    cmd.Parameters("@TotNo").Value = 0;
                    cmd.Parameters("@Polio").Value = 0;
                    cmd.Parameters("@DTP").Value = 0;
                    cmd.Parameters("@MMRDose1").Value = 0;
                    cmd.Parameters("@MMRDose2").Value = 0;
                    cmd.Parameters("@HepB").Value = 0;
                    cmd.Parameters("@VZV").Value = 0;
                    */


                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                    ret2 = cmd2.ExecuteNonQuery();
                    ret_f = cmd_f.ExecuteNonQuery();
                    ret2_f = cmd2_f.ExecuteNonQuery();

                    if ((ret == 1) && (ret2 == 1) && (ret_f == 1) && (ret2_f == 1))
                    {
                        /*
                        if ((hdnSchCode.Value != txtSchoolCode.Text))
                        {
                            logaudittrail("SchCode", hdnSchCode.Value, txtSchoolCode.Text);
                        }
                        if ((hdnSchType.Value != cmbSchoolType.SelectedValue))
                        {
                            logaudittrail("SchType", hdnSchType.Value, cmbSchoolType.SelectedValue);
                        }
                        if ((hdnIsCharter.Value != cmbCharter.SelectedValue))
                        {
                            logaudittrail("isCharter", hdnSchCode.Value, cmbCharter.SelectedValue);
                        }
                        if ((hdnCoName.Value != txtCounty.Text))
                        {
                            logaudittrail("CoName", hdnCoName.Value, txtCounty.Text);
                        }
                        if ((hdnCoCode.Value != txtCountyCode.Text))
                        {
                            logaudittrail("CoCode", hdnCoCode.Value, txtCountyCode.Text);
                        }
                        if ((hdnDistName.Value != txtDistrict.Text))
                        {
                            logaudittrail("DistName", hdnDistName.Value, txtDistrict.Text);
                        }
                        if ((hdnDistCode.Value != txtDistrictCode.Text))
                        {
                            logaudittrail("DistCode", hdnDistCode.Value, txtDistrictCode.Text);
                        }
                        if ((hdnSchoolName.Value != txtSchoolName.Text))
                        {
                            logaudittrail("SchName", hdnSchoolName.Value, txtSchoolName.Text);
                        }
                        if ((hdnSchoolPhone.Value != txtSchoolPhone.Text))
                        {
                            logaudittrail("SchPhone", hdnSchoolPhone.Value, txtSchoolPhone.Text);
                        }
                        if ((hdnSchAdmin.Value != txtSchAdmin.Text))
                        {
                            logaudittrail("SchAdmin", hdnSchAdmin.Value, txtSchAdmin.Text);
                        }
                        if ((hdnSchEmail.Value != txtSchAdmin.Text))
                        {
                            logaudittrail("SchEmail", hdnSchEmail.Value, txtSchAdmin.Text);
                        }
                        if ((hdnSPACode.Value != txtSPACode.Text))
                        {
                            logaudittrail("SPACode", hdnSPACode.Value, txtSPACode.Text);
                        }
                        if ((hdnPhysStreet.Value != txtPhyAddress.Text))
                        {
                            logaudittrail("PhysStreet", hdnPhysStreet.Value, txtPhyAddress.Text);
                        }
                        if ((hdnPhysCity.Value != txtPhyCity.Text))
                        {
                            logaudittrail("PhysCity", hdnPhysCity.Value, txtPhyCity.Text);
                        }
                        if ((hdnPhysZip.Value != txtPhyZip.Text))
                        {
                            logaudittrail("PhysZip", hdnPhysZip.Value, txtPhyZip.Text);
                        }
                        if ((hdnMailStreet.Value != txtMailAddress.Text))
                        {
                            logaudittrail("MailStreet", hdnMailStreet.Value, txtMailAddress.Text);
                        }
                        if ((hdnMailCity.Value != txtMailCity.Text))
                        {
                            logaudittrail("MailCity", hdnMailCity.Value, txtMailCity.Text);
                        }
                        if ((hdnMailZip.Value != txtMailZip.Text))
                        {
                            logaudittrail("MailZip", hdnMailZip.Value, txtMailZip.Text);
                        }
                        if ((hdnContactPerson.Value != txtContactPerson.Text))
                        {
                            logaudittrail("ContactPerson", hdnContactPerson.Value, txtContactPerson.Text);
                        }
                        if ((hdnContactEmail.Value != txtContactEmail.Text))
                        {
                            logaudittrail("ContactEmail", hdnContactEmail.Value, txtContactEmail.Text);
                        }
                        if ((hdnContactPhone.Value != txtContactPhone.Text))
                        {
                            logaudittrail("ContactPhone", hdnContactPhone.Value, txtContactPhone.Text);
                        }
                        if ((hdnContactPhoneExt.Value != txtContactPhoneExt.Text))
                        {
                            logaudittrail("ContactPhoneExt", hdnContactPhoneExt.Value, txtContactPhoneExt.Text);
                        }
                        if ((hdnFormPerson.Value != txtFormPerson.Text))
                        {
                            logaudittrail("FormPerson", hdnFormPerson.Value, txtFormPerson.Text);
                        }
                        if ((hdnFormEmail.Value != txtFormEmail.Text))
                        {
                            logaudittrail("FormEmail", hdnFormEmail.Value, txtFormEmail.Text);
                        }
                        if ((hdnFormPhone.Value != txtFormPhone.Text))
                        {
                            logaudittrail("FormPhone", hdnFormPhone.Value, txtFormPhone.Text);
                        }
                        if ((hdnFormPhoneExt.Value != txtFormPhoneExt.Text))
                        {
                            logaudittrail("FormPhoneExt", hdnFormPhoneExt.Value, txtFormPhoneExt.Text);
                        }
                        if ((hdnKinderYesNo.Value != drpdwnKndrYesNo.Text))
                        {
                            logaudittrail("KinderYesNo", hdnKinderYesNo.Value, drpdwnKndrYesNo.SelectedValue);
                        }
                        if ((hdnMemo.Value != txtMemo.Text))
                        {
                            logaudittrail("Memo", hdnMemo.Value, txtMemo.Text);
                        }
                        if ((hdnReason.Value != drpReason.Text))
                        {
                            logaudittrail("Reason", hdnReason.Value, drpReason.SelectedValue);
                        }
                        if ((hdnSuperintendentName.Value != txtSuperintendentName.Text))
                        {
                            logaudittrail("SuperintendentName", hdnSuperintendentName.Value, txtSuperintendentName.Text);
                        }
                        if ((hdnSuperintendentEmail.Value != txtSuperintendentEmail.Text))
                        {
                            logaudittrail("SuperintendentEmail", hdnSuperintendentEmail.Value, txtSuperintendentEmail.Text);
                        }
                        if ((hdnSuperintendentPhone.Value != txtSuperintendentPhone.Text))
                        {
                            logaudittrail("SuperintendentPhone", hdnSuperintendentPhone.Value, txtSuperintendentPhone.Text);
                        }
                        if ((hdnLhdReviseDate.Value != curDateTime))
                        {
                            logaudittrail("LhdReviseDate", hdnLhdReviseDate.Value, curDateTime);
                        }
                        if ((newSubmit == "True"))
                        {
                            logaudittrail("SubmitDate", hdnSubmitDate.Value, curDateTime);
                        }
                        */
                        lblMsg.Text = "<p><span class=\"greenbold\">School updated</span></p>";
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
                    //cmd = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        protected void CustomValidator1_txtFormPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFormPhone.Text == "" || txtFormPhone_1.Text == "" || txtFormPhone_2.Text == "")
            {
                args.IsValid = false;

            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator1_txtFormPhone_digit_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Regex.IsMatch(txtFormPhone.Text, @"\d{3}$") && Regex.IsMatch(txtFormPhone_1.Text, @"\d{3}$") && Regex.IsMatch(txtFormPhone_2.Text, @"\d{4}$"))
            {
                args.IsValid = true;

            }
            else
            {
                if (txtFormPhone.Text == "" || txtFormPhone_1.Text == "" || txtFormPhone_2.Text == "")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void CustomValidator_txtContactPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtContactPhone.Text == "" || txtContactPhone_1.Text == "" || txtContactPhone_2.Text == "")
            {
                args.IsValid = false;

            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator_txtContactPhone_digit_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Regex.IsMatch(txtContactPhone.Text, @"\d{3}$") && Regex.IsMatch(txtContactPhone_1.Text, @"\d{3}$") && Regex.IsMatch(txtContactPhone_2.Text, @"\d{4}$"))
            {
                args.IsValid = true;

            }
            else
            {
                if (txtContactPhone.Text == "" || txtContactPhone_1.Text == "" || txtContactPhone_2.Text == "")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }


        protected void chkaddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkaddress.Checked == true)
            {
                if (txtPhyAddress.Text == "" && txtMailAddress.Text != "")
                {
                    txtPhyAddress.Text = txtMailAddress.Text;
                    txtPhyCity.Text = txtMailCity.Text;
                    txtPhyZip.Text = txtMailZip.Text;
                    chkaddress.Focus();
                }
                else
                {
                    txtMailAddress.Text = txtPhyAddress.Text;
                    txtMailCity.Text = txtPhyCity.Text;
                    txtMailZip.Text = txtPhyZip.Text;
                    chkaddress.Focus();

                }
            }

        }

        protected void chkcontact_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcontact.Checked == true)
            {
                if (txtContactPerson.Text == "" && txtFormPerson.Text != "")
                {
                    txtContactPerson.Text = txtFormPerson.Text;
                    txtContactPhone.Text = txtFormPhone.Text;
                    txtContactPhone_1.Text = txtFormPhone_1.Text;
                    txtContactPhone_2.Text = txtFormPhone_2.Text;
                    txtContactEmail.Text = txtFormEmail.Text;
                    //txtfccemail.Text = txtconfirmmail.Text;
                    txtContactPhoneExt.Text = txtFormPhoneExt.Text;
                    chkcontact.Focus();
                }
                else
                {
                    txtFormPerson.Text = txtContactPerson.Text;
                    txtFormPhone.Text = txtContactPhone.Text;
                    txtFormPhone_1.Text = txtContactPhone_1.Text;
                    txtFormPhone_2.Text = txtContactPhone_2.Text;
                    txtFormEmail.Text = txtContactEmail.Text;
                    //txtconfirmmail.Text = txtfccemail.Text;
                    txtFormPhoneExt.Text = txtContactPhoneExt.Text;
                    chkcontact.Focus();

                }
            }
            if (chkcontact.Checked == false)
            {
                txtContactPerson.Text = "";
                txtContactPhone.Text = "";
                txtContactPhone_1.Text = "";
                txtContactPhone_2.Text = "";
                txtContactPhoneExt.Text = "";
                txtContactEmail.Text = "";
                //txtfccemail.Text = "";
                txtContactPerson.Focus();
            }
        }
    }
}