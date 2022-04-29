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
using System.Net.Mail;


/*
 *  06/12/2019 AT Removed 'BERKELEY CITY', 'LONG BEACH CITY', 'PASADENA' from drop down list to add school
 *  07/02/2019 AT Updated Add School feature: Berkeley City -> ALAMEDA, Long Beach City --> Los Angeles, Pasadena --> Los Angeles.
 *  
 * */



namespace SchoolAssessment.Admin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
                //ElseIf (Session["AdminUserType"] <> "ADMIN") Then
            }
            else if ((Session["AdminUserType"].ToString() == "FIELDREP"))
            {
                Response.Redirect("AdminLogin.aspx?reason=InsufficientPrivileges", true);
            }

            // Set as Read-Only by AT on 06/15/2015
            txtCountyCode.ReadOnly = true;
            txtDistrictCode.ReadOnly = true;
            txtSchoolCode.ReadOnly = true;

            // Cohort has been selected already in Search School function
            if (Session["AdminAddSchoolCohort"].ToString() != "") { 
                DropDownCohort.SelectedValue = Session["AdminAddSchoolCohort"].ToString();
            }
            // Added below by AT on 06/10/2015
            if ((Page.IsPostBack == false & !string.IsNullOrEmpty(Request.QueryString["SchCode"])))
            {
                FillType();
                txtCounty.Items.Add("--Select--");
                txtDistrict.Items.Add("--Select--");

                dynamic schCode = Request.QueryString["SchCode"];
                Session["AdminAddSchCode"] = Request.QueryString["SchCode"];
                txtSchoolCode.Text = schCode;

                // Adding Kindergarten or 7th Grade School using existing other cohort's data
                FillInData();
            }

        }


        private void FillInData()
        { 
            // Adding school by using informaiton with existing cohort school.
            // Adding K using 7th grade information
            // Adding 7th using Kindergarten information. 

            dynamic schcode = Session["AdminAddSchCode"].ToString();
            string cohort = Session["AdminAddSchoolCohort"].ToString();
            string cohortExist = "";
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            // Select cohort of existing school
            if (cohort == "S" )
            {
                cohortExist = "K";
            } else if (cohort == "K")
            {
                cohortExist = "S";
            }
            //string sql = "SELECT S.*, C.CoName, A.*, D.* FROM[dbo].[Assessments] A INNER JOIN Schools S on A.id = S.id INNER JOIN Counties C on C.CoCode = S.CoCode LEFT OUTER JOIN Districts D ON D.DistCode = S.DistCode WHERE A.Assmntid = '" + id + "'";
            string sql = "SELECT S.*, c.CoName FROM SCHOOLS s INNER JOIN Counties c ON s.CoCode = c.CoCode WHERE SCHCODE = '" + schcode +  "' AND COHORT = '" + cohortExist + "'";


            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string SchPhone = "";
                string SuperIntendentPhone = "";
                string ContactPhone = "";

                while (reader.Read())
                {
                    cmbCharter.SelectedValue = reader["isCharter"].ToString();
                    cmbSchoolType.SelectedValue = reader["schType"].ToString();
                    //txtCounty.SelectedValue = reader["CoName"].ToString();
                    //txtCounty.SelectedValue = "Alameda";

                    txtCountyCode.Text = reader["CoCode"].ToString();
                    txtDistrictCode.Text = reader["DistCode"].ToString();

                    //txtCounty.DataTextField = reader["CoName"].ToString();
                    //txtCounty.DataValueField = reader["CoName"].ToString();

                    txtSchoolName.Text = reader["SchName"].ToString();
                    SchPhone = Regex.Replace(reader["SchPhone"].ToString(), "[^0-9]+", string.Empty);
                    txtSchoolPhone.Text = SchPhone.Substring(0, 3);
                    txtSchoolPhone_1.Text = SchPhone.Substring(3, 3);
                    txtSchoolPhone_2.Text = SchPhone.Substring(6, 4);
                    txtSchAdmin.Text = reader["SchAdmin"].ToString();
                    txtSchEmail.Text = reader["SchEmail"].ToString();
                    txtSuperintendentName.Text = reader["SuperintendentName"].ToString();
                    txtSuperintendentEmail.Text = reader["SuperintendentEmail"].ToString();
                    SuperIntendentPhone = Regex.Replace(reader["SuperintendentPhone"].ToString(), "[^0-9]+", string.Empty);
                    txtSuperintendentPhone.Text = SchPhone.Substring(0, 3);
                    txtSuperintendentPhone_1.Text = SchPhone.Substring(3, 3);
                    txtSuperintendentPhone_2.Text = SchPhone.Substring(6, 4);
                    txtPhyAddress.Text = reader["PhysStreet"].ToString();
                    txtPhyCity.Text = reader["PhysCity"].ToString();
                    txtPhyZip.Text = reader["PhysZip"].ToString();
                    txtMailAddress.Text = reader["MailStreet"].ToString();
                    txtMailCity.Text = reader["MailCity"].ToString();
                    txtMailZip.Text = reader["MailZip"].ToString();
                    txtContactPerson.Text = reader["ContactPerson"].ToString();
                    txtContactEmail.Text = reader["ContactEmail"].ToString();
                    txtMailZip.Text = reader["MailZip"].ToString();

                    ContactPhone = Regex.Replace(reader["ContactPhone"].ToString(), "[^0-9]+", string.Empty);
                    txtContactPhone.Text = SchPhone.Substring(0, 3);
                    txtContactPhone_1.Text = SchPhone.Substring(3, 3);
                    txtContactPhone_2.Text = SchPhone.Substring(6, 4);

                }

                if (cohort != "") { 
                    txtCounty.Enabled = false;
                    txtDistrict.Enabled = false;
                    Requiredfieldvalidator17.Enabled = false;
                    valDistrict.Enabled = false;
                    txtSchoolCode.Enabled = false;
                }

                if (cmbSchoolType.SelectedValue == "PRIVATE")
                {
                    Requiredfieldvalidator3.Enabled = false;
                }

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

        private void FillType()
        {
            // DataSet ds = new DataSet();
            try
            {
                cmbSchoolType.Items.Clear();
                cmbSchoolType.Items.Add("--Select--");
                cmbSchoolType.Items.Add("PUBLIC");
                cmbSchoolType.Items.Add("PRIVATE");
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void FillCountyCode()
        {

            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string connectString = System.Configuration.ConfigurationManager.AppSettings("kSchoolDBConnectString");
            //SqlConnection conn = new SqlConnection(connectString);

            dynamic county = txtCounty.SelectedItem.Value.Replace("'", "''");
            string sql = "SELECT distinct CoCode FROM Counties where CoName ='" + county + "' ";
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);

            //Dim ds As New DataSet
            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                txtCountyCode.Text = reader["CoCode"].ToString();


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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            Page.Validate();
            if ((Page.IsValid))
            {
                //string connectString = System.Configuration.ConfigurationManager.AppSettings("kSchoolDBConnectString");
                //SqlConnection conn = new SqlConnection(connectString);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
                string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];

                string sql = null, sql2 = null;
                SqlCommand cmd = default(SqlCommand);
                SqlCommand cmd2 = default(SqlCommand);
                int ret = 0, ret2 = 0;
                string cohort = "";

                /*if (Session["AdminAddSchoolCohort"].ToString() != "")
                {
                    cohort = Session["AdminAddSchoolCohort"].ToString();
                }else
                {
                  */  cohort = DropDownCohort.SelectedValue;
                /*}*/



                try
                {
                    sql = "INSERT INTO [dbo].[Schools] (Cohort, SchCode, CoCode, DistCode, SchName, PhysStreet, PhysCity, PhysZip, MailStreet, MailCity, MailZip, SchPhone, SchType, isCharter, EnterDate, EditDate, Password, SchAdmin, SchEmail, ContactPerson, ContactPhone, ContactPhoneExt, ContactEmail, SuperintendentName, SuperintendentEmail, SuperintendentPhone) VALUES (@Cohort, @SchCode, @CoCode, @DistCode, @SchName, @PhysStreet, @PhysCity, @PhysZip, @MailStreet, @MailCity, @MailZip, @SchPhone, @SchType, @isCharter, Getdate(), Getdate(), '', @SchAdmin, @SchEmail, @ContactPerson, @ContactPhone, @ContactPhoneExt, @ContactEmail, @SuperintendentName, @SuperintendentEmail, @SuperintendentPhone); SELECT CAST(scope_identity() AS int);";

                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Cohort", cohort);
                    cmd.Parameters.AddWithValue("@SchCode", txtSchoolCode.Text);
                    cmd.Parameters.AddWithValue("@SchoolYear", cmbSchoolYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@CoCode", txtCountyCode.Text);
                    cmd.Parameters.AddWithValue("@DistCode", txtDistrictCode.Text);
                    cmd.Parameters.AddWithValue("@SchName", txtSchoolName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@PhysStreet", txtPhyAddress.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@PhysCity", txtPhyCity.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@PhysZip", txtPhyZip.Text);
                    cmd.Parameters.AddWithValue("@MailStreet", txtMailAddress.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@MailCity", txtMailCity.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@MailZip", txtMailZip.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SchPhone", txtSchoolPhone.Text + txtSchoolPhone_1.Text + txtSchoolPhone_2.Text);
                    cmd.Parameters.AddWithValue("@SchType", cmbSchoolType.SelectedValue);
                    cmd.Parameters.AddWithValue("@isCharter", cmbCharter.SelectedValue);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text + txtContactPhone_1.Text + txtContactPhone_2.Text);
                    cmd.Parameters.AddWithValue("@ContactPhoneExt", txtContactPhoneExt.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ContactEmail", txtContactEmail.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SchAdmin", txtSchAdmin.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SchEmail", txtSchEmail.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SuperintendentName", txtSuperintendentName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SuperintendentEmail", txtSuperintendentEmail.Text);
                    cmd.Parameters.AddWithValue("@SuperintendentPhone", txtSuperintendentPhone.Text + txtSuperintendentPhone_1.Text + txtSuperintendentPhone_2.Text);

                    con.Open();

                    // Added by AT on 06/15/2015
                    // Insert the school only if it does not exist (schCode + SchoolYear)
                    if (DoesSchoolExist() == false)
                    {
                        // return lastly entered id
                        ret = (int)cmd.ExecuteScalar();

                    }
                    else
                    {
                        lblMsg.Text = "<p><span class=\"redbold\">School already exists. Please enter different School Code. </span></p>";
                        lblMsg.Visible = true;
                    }


                    if (ret > 0)
                    {
                        sql2 = "INSERT INTO Assessments SELECT S.id, S.SchCode, @schoolyear, 'N', 'Yes', NULL, 'No', 'No', '', '', '', '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, Getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0 FROM Schools S where S.SchCode = @SchoolCode and S.cohort = @cohort; SELECT CAST(scope_identity() AS int);";
                        cmd2 = new SqlCommand(sql2, con);
                        cmd2.Parameters.AddWithValue("@SchoolCode", txtSchoolCode.Text);
                        cmd2.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                        cmd2.Parameters.AddWithValue("@cohort", cohort);

                        ret2 = (int)cmd2.ExecuteScalar();
                    }



                    if ((ret > 0 && ret2 > 0))
                    {
                        if ((!string.IsNullOrEmpty(txtSchoolCode.Text)))
                        {
                            logaudittrail("SchCode", "", txtSchoolCode.Text);
                        }
                        if ((!string.IsNullOrEmpty(cmbSchoolType.SelectedValue)))
                        {
                            logaudittrail("SchType", "", cmbSchoolType.SelectedValue);
                        }
                        if ((!string.IsNullOrEmpty(cmbCharter.SelectedValue)))
                        {
                            logaudittrail("isCharter", "", cmbCharter.SelectedValue);
                        }
                        if ((!string.IsNullOrEmpty(txtCountyCode.Text)))
                        {
                            logaudittrail("CoCode", "", txtCountyCode.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtDistrictCode.Text)))
                        {
                            logaudittrail("DistCode", "", txtDistrictCode.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSchoolName.Text)))
                        {
                            logaudittrail("SchName", "", txtSchoolName.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSchoolPhone.Text)))
                        {
                            logaudittrail("SchPhone", "", txtSchoolPhone.Text);
                        }

                        if ((!string.IsNullOrEmpty(txtPhyAddress.Text)))
                        {
                            logaudittrail("PhysStreet", "", txtPhyAddress.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtPhyCity.Text)))
                        {
                            logaudittrail("PhysCity", "", txtPhyCity.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtPhyZip.Text)))
                        {
                            logaudittrail("PhysZip", "", txtPhyZip.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtMailAddress.Text)))
                        {
                            logaudittrail("MailStreet", "", txtMailAddress.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtMailCity.Text)))
                        {
                            logaudittrail("MailCity", "", txtMailCity.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtMailZip.Text)))
                        {
                            logaudittrail("MailZip", "", txtMailZip.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtContactPerson.Text)))
                        {
                            logaudittrail("ContactPerson", "", txtContactPerson.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtContactEmail.Text)))
                        {
                            logaudittrail("ContactEmail", "", txtContactEmail.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtContactPhone.Text)))
                        {
                            logaudittrail("ContactPhone", "", txtContactPhone.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtContactPhoneExt.Text)))
                        {
                            logaudittrail("ContactPhoneExt", "", txtContactPhoneExt.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSchAdmin.Text)))
                        {
                            logaudittrail("SchAdmin", "", txtSchAdmin.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSchEmail.Text)))
                        {
                            logaudittrail("SchEmail", "", txtSchEmail.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSuperintendentName.Text)))
                        {
                            logaudittrail("SuperintendentName", "", txtSuperintendentName.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSuperintendentEmail.Text)))
                        {
                            logaudittrail("SuperintendentEmail", "", txtSuperintendentEmail.Text);
                        }
                        if ((!string.IsNullOrEmpty(txtSuperintendentPhone.Text)))
                        {
                            logaudittrail("SuperintendentPhone", "", txtSuperintendentPhone.Text);
                        }

                        lblMsg.Text = "<p><span class=\"greenbold\">New school added: <a href=\"AdminEditSchool7th.aspx?id=" + ret2 + "\">Edit/view new school</a></span></p>";
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
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

        }

        protected void cmbSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbSchoolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cmbSchoolType.SelectedItem.Value == "--Select--"))
                {
                    txtCountyCode.Text = "";
                    txtDistrictCode.Text = "";
                    txtCounty.Items.Clear();
                    txtCounty.Items.Add("--Select--");
                    txtDistrict.Enabled = true;
                    txtDistrict.Items.Clear();
                    txtDistrict.Items.Add("--Select--");

                }
                else if ((cmbSchoolType.SelectedItem.Value == "PRIVATE"))
                {
                    txtCountyCode.Text = "";
                    txtDistrictCode.Text = "";
                    FillCounty();
                    txtDistrict.Items.Clear();
                    //txtDistrict.Items.Add("--Select--")
                    txtDistrict.Enabled = false;
                    valDistrict.Enabled = false;
                    Requiredfieldvalidator3.Enabled = false;
                    cmbCharter.SelectedValue = "N";
                }
                else
                {
                    txtCountyCode.Text = "";
                    FillCounty();
                    //txtDistrict.ReadOnly = False
                    valDistrict.Enabled = true;
                    txtDistrict.Enabled = true;
                    txtDistrict.Items.Clear();
                    txtDistrict.Items.Add("--Select--");

                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void FillCounty()
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            dynamic userCoCode = Session["AdminCoCode"];
            SqlCommand cmd;

            if ((userCoCode == "00"))
            {
                string sql_admin = "SELECT DISTINCT CoName FROM Counties WHERE CoCode > 0 and CoCode < 59 ";
                cmd = new SqlCommand(sql_admin, con);
            }
            else
            {
                if (userCoCode == "59")
                {
                    userCoCode = "01";

                } else if (userCoCode == "60" || userCoCode == "61")
                {
                    userCoCode = "19";
                }
               
                string sql = "SELECT DISTINCT CoName FROM Counties WHERE CoCode = '" + userCoCode + "' ";
                cmd = new SqlCommand(sql, con);

            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);

                txtCounty.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                txtCounty.DataValueField = ds.Tables[0].Columns["CoName"].ToString();

                txtCounty.DataSource = ds.Tables[0];
                txtCounty.DataBind();

                txtCounty.Items.Insert(0, ("--Select---"));
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void Filldistrict()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            dynamic county = txtCounty.SelectedItem.Value.Replace("'", "''");
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT DistName FROM Districts D INNER JOIN Counties C ON D.CoCode = C.CoCode WHERE C.CoName ='" + county + "' and DistName IS NOT NULL AND DistName <> '' ORDER BY DistName ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {

                da.Fill(ds);

                txtDistrict.DataTextField = ds.Tables[0].Columns["DistName"].ToString();
                txtDistrict.DataValueField = ds.Tables[0].Columns["DistName"].ToString();

                txtDistrict.DataSource = ds.Tables[0];
                txtDistrict.DataBind();

                txtDistrict.Items.Insert(0, ("--Select---"));
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void txtDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((txtDistrict.SelectedItem.Text == "--Select--"))
                {
                    txtDistrictCode.Text = "";
                }
                else
                {
                    FillDistrictCode();
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void FillDistrictCode()
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            dynamic distName = txtDistrict.SelectedItem.Value.Replace("'", "''");
            //Dim sql As String = "SELECT distinct DistCode FROM K_Assessment where SchoolYear = '" & schoolyear & "' and DistName ='" & distName & "' "
            //string sql = "SELECT distinct DistCode FROM K_Assessment where DistName ='" + distName + "' ";
            //SqlCommand cmd = new SqlCommand(sql);
            //SqlDataReader reader = default(SqlDataReader);

            SqlCommand cmd = new SqlCommand("SELECT DISTINCT DistCode FROM Districts WHERE DistName = '" + distName + "' ", con);
            //SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);


            //Dim ds As New DataSet
            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                txtDistrictCode.Text = reader["DistCode"].ToString();

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }


        protected void txtCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((txtCounty.SelectedItem.Text == "--Select--"))
                {
                    if ((txtDistrict.Enabled == true))
                    {
                        txtDistrict.Items.Clear();
                        txtDistrict.Items.Add("--Select--");
                        txtCountyCode.Text = "";
                        txtDistrictCode.Text = "";
                    }
                }
                else
                {
                    if ((txtDistrict.Enabled == true))
                    {
                        FillCountyCode();
                        Filldistrict();
                    }
                    else
                    {
                        FillCountyCode();
                    }

                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private bool DoesSchoolExist()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            string schoolyear = cmbSchoolYear.SelectedValue;
            dynamic schCode = txtSchoolCode.Text;
            string sql = "SELECT COUNT(*) AS sch_num FROM Schools where SchCode ='" + schCode + "' AND cohort = '" + DropDownCohort.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);

            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                return Convert.ToInt32(reader["sch_num"].ToString()) > 0;
                //return true;

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
                return false;
            }
        }

        private void logaudittrail(string fieldname, string fromval, string toval)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //SqlConnection conn = new SqlConnection(connectString);
            dynamic schoolcode = txtSchoolCode.Text;
            string sql = null;
            SqlCommand cmd = default(SqlCommand);


            try
            {
                sql = "insert into AuditTrail(SchCode, ScreenName, FieldName, FromValue, ToValue, IP, TimeStamp) values (@SchoolCode, @ScreenName, @FieldName, @FromValue, @ToValue, @IP, @TimeStamp)";
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@SchoolCode", schoolcode);
                cmd.Parameters.AddWithValue("@ScreenName", "AdminAddSchoolKG");
                cmd.Parameters.AddWithValue("@FieldName", fieldname);
                cmd.Parameters.AddWithValue("@FromValue", fromval);
                cmd.Parameters.AddWithValue("@ToValue", toval);
                cmd.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);

                cmd.Connection = con;
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
                cmd = null;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        protected void CustomValidator_txtSchoolPhone_1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtSchoolPhone.Text == "" || txtSchoolPhone_1.Text == "" || txtSchoolPhone_2.Text == "")
            {
                args.IsValid = false;

            }
            else
            {
                args.IsValid = true;
            }

        }

        /*
         * Requested by Kristen Sy not to make it required.  05/2/2019
         * 
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
        */

        protected void CustomValidator_txtSchoolPhone_Digit_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Regex.IsMatch(txtSchoolPhone.Text, @"\d{3}$") && Regex.IsMatch(txtSchoolPhone_1.Text, @"\d{3}$") && Regex.IsMatch(txtSchoolPhone_2.Text, @"\d{4}$"))
            {
                args.IsValid = true;

            }
            else
            {
                if (txtSchoolPhone.Text == "" || txtSchoolPhone_1.Text == "" || txtSchoolPhone_2.Text == "")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void CustomValidator_txtContactPhone_Digit_ServerValidate(object source, ServerValidateEventArgs args)
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

        protected void CustomValidator_txtSuperintendentPhone_Digit_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Regex.IsMatch(txtSuperintendentPhone.Text, @"\d{3}$") && Regex.IsMatch(txtSuperintendentPhone_1.Text, @"\d{3}$") && Regex.IsMatch(txtSuperintendentPhone_2.Text, @"\d{4}$"))
            {
                args.IsValid = true;

            }
            else
            {
                if (txtSuperintendentPhone.Text == "" || txtSuperintendentPhone_1.Text == "" || txtSuperintendentPhone_2.Text == "")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void CustomValidator_txtSuperintendentPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtSuperintendentPhone.Text == "" || txtSuperintendentPhone_1.Text == "" || txtSuperintendentPhone_2.Text == "")
            {
                args.IsValid = false;

            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void chkaddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkaddress.Checked == true)
            {
                txtMailAddress.Text = txtPhyAddress.Text;
                txtMailCity.Text = txtPhyCity.Text;
                txtMailZip.Text = txtPhyZip.Text;
            }
            if (chkaddress.Checked == false)
            {
                txtMailAddress.Text = "";
                txtMailCity.Text = "";
                txtMailZip.Text = "";
                txtMailAddress.Focus();
            }
        }

        protected void DropDownCohort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cohort has been selected already in Search School 
            //DropDownCohort.SelectedValue = Session["AdminAddSchoolCohort"].ToString();

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (DropDownCohort.SelectedValue == "C")
            {
                
                if(txtSchoolCode.Text.Length == 9 )
                {
                    args.IsValid = true;
                } else
                {
                    args.IsValid = false;
                }

            } else if (DropDownCohort.SelectedValue == "K" || DropDownCohort.SelectedValue == "S")
            {
               
                if (txtSchoolCode.Text.Length == 7)
                 {
                     args.IsValid = true;
                 }
                 else
                 {
                     args.IsValid = false;
                 }
            }
        }
        public bool IsEmailValid(string email)
        {

            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtContactEmail.Text.Length == 0)
            {
                args.IsValid = false;
                CustomValidator2.ErrorMessage = "Designated School Contact Email - Required.";
            }
            else if (Regex.IsMatch(txtContactEmail.Text, @"\s+"))
            {
                args.IsValid = false;
                CustomValidator2.ErrorMessage = "Please remove space from Designated School Contact Email.";
            }
            else if (!IsEmailValid(txtContactEmail.Text.ToString()))
            {
                args.IsValid = false;
                CustomValidator2.ErrorMessage = "Designated School Contact Email - Invalid format.";
            }
            else
            {
                args.IsValid = true;
            }
        }

        /*
        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool validEmail = IsValidEmail(txtContactEmail.Text.ToString());

            if (validEmail)
            {
                args.IsValid = true;
            } else
            {
                args.IsValid = false;
            }

        }

        bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
                //return true;
            }
            catch
            {
                return false;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtSchEmail.Text.ToString() != "") { 

                bool validEmail = IsValidEmail(txtSchEmail.Text.ToString());

                if (validEmail)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            } else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(txtSuperintendentEmail.Text.ToString() != "") {

                bool validEmail = IsValidEmail(txtSuperintendentEmail.Text.ToString());

                if (validEmail)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }
        */
    }
}