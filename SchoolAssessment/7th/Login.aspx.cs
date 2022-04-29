using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;

/*
 * 05/09/2019 Removing the link for Search For your School, and county search is available from the begining. 
 * 
 * 08/13/2019
 * Removed follwoing counties from drop-down in the login page: NONE', 'BERKELEY CITY', 'LONG BEACH CITY', 'PASADENA
 */


namespace SchoolAssessment._7th
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                MessagesRow.Visible = false;
                Session["AdminSelectedYear"] = "";
                Session["AdminUserType"] = ""; // Added on 01/26/2017

                //if (LinkBtnForgotSchCodeClicked == false) { 
                // School Search is not availble initially 
                /*
                LabelSchType.Visible = false;
                TypeList.Visible = false;
                LabelCounty.Visible = false;
                CountyList.Visible = false;
                LabelDistrict.Visible = false;
                DistrictList.Visible = false;
                CityList.Visible = false;
                LabelCity.Visible = false;
                LabelSchName.Visible = false;
                SchoolNameList.Visible = false;
                LabelSchAddress.Visible = false;
                SchoolAddressList.Visible = false;
                */

                
                FillType();
                
                CountyList.Items.Add("--Select--");
                DistrictList.Items.Add("--Select--");
                CityList.Items.Add("--Select--");
                SchoolNameList.Items.Add("--Select--");
                SchoolAddressList.Items.Add("--Select--");
                
               
            }

            if ((Request.QueryString["reason"] == "TimedOut"))
                {
                    MessagesRow.Visible = true;
                    lblErrorMsg.Text = "Session timed out. Please log in.";
                }
            

            if (Page.IsPostBack)
            {
                MessagesRow.Visible = false;
                txtPassword.Enabled = true;
            }

        }


        private void FillType()
        {
            DataSet ds = new DataSet();
            try
            {
                TypeList.Items.Clear();
                TypeList.Items.Add("--Select--");
                TypeList.Items.Add("PUBLIC");
                TypeList.Items.Add("PRIVATE");
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void FillCounty()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT CoName FROM Counties WHERE CoName NOT IN ( 'NONE', 'BERKELEY CITY', 'LONG BEACH CITY', 'PASADENA') ORDER BY CoName ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);


                CountyList.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                CountyList.DataValueField = ds.Tables[0].Columns["CoName"].ToString();

                CountyList.DataSource = ds.Tables[0];
                CountyList.DataBind();

                CountyList.Items.Insert(0, ("--Select---"));


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

        private void Filldistrict()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            dynamic county = CountyList.SelectedItem.Value.Replace("'", "''");
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT DistName FROM Districts D INNER JOIN Counties C ON D.CoCode = C.CoCode WHERE C.CoName ='" + county + "' and DistName IS NOT NULL AND DistName <> '' ORDER BY DistName ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {

                da.Fill(ds);

                DistrictList.DataTextField = ds.Tables[0].Columns["DistName"].ToString();
                DistrictList.DataValueField = ds.Tables[0].Columns["DistName"].ToString();

                DistrictList.DataSource = ds.Tables[0];
                DistrictList.DataBind();

                DistrictList.Items.Insert(0, ("--Select---"));
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

        private void FillCity()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            string ActiveSchool = System.Configuration.ConfigurationManager.AppSettings["ActiveSchool"];

            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' and DistName = '" + district + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT S.PhysCity FROM Schools S INNER JOIN[Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName = '" + county + "' and SchType = '" + type + "' and D.DistName = '" + district + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY S.PhysCity ASC";

                }
                else {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT S.PhysCity FROM Schools S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and CoName = '" + county + "' and SchType = '" + type + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY S.PhysCity ASC";
                }

                SchoolNameList.Items.Clear();
                SchoolNameList.Items.Add("--Select--");
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);

                CityList.DataTextField = ds.Tables[0].Columns["PhysCity"].ToString();
                CityList.DataValueField = ds.Tables[0].Columns["PhysCity"].ToString();

                CityList.DataSource = ds.Tables[0];
                CityList.DataBind();

                CityList.Items.Insert(0, ("--Select---"));
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


        private void FillSchool()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            string ActiveSchool = System.Configuration.ConfigurationManager.AppSettings["ActiveSchool"];

            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' and DistName = '" + district + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName = '" + county + "' and SchType = '" + type + "' and D.DistName = '" + district + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY[SchName] ASC";

                }
                else {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and CoName = '" + county + "' and SchType = '" + type + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY[SchName] ASC";
                }

                SchoolNameList.Items.Clear();
                SchoolNameList.Items.Add("--Select--");
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);

                SchoolNameList.DataTextField = ds.Tables[0].Columns["SchName"].ToString();
                SchoolNameList.DataValueField = ds.Tables[0].Columns["SchName"].ToString();

                SchoolNameList.DataSource = ds.Tables[0];
                SchoolNameList.DataBind();

                SchoolNameList.Items.Insert(0, ("--Select---"));
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


        private void FillAddress()
        {
            //string schoolyear = System.Configuration.ConfigurationManager.AppSettings("SchoolYear");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string schoolname = SchoolNameList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            DataSet ds = new DataSet();
            string ActiveSchool = System.Configuration.ConfigurationManager.AppSettings["ActiveSchool"];

            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT DISTINCT SchCode, PhysStreet, PhysCity FROM SCHOOLS INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' ORDER BY PhysCity, PhysStreet";
                    sql = "SELECT DISTINCT PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY PhysStreet";

                }
                else {
                    //sql = "SELECT distinct SchCode, PhysStreet, PhysCity FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' ORDER BY PhysCity, PhysStreet";
                    sql = "SELECT distinct PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY PhysStreet";
                }

                SchoolAddressList.Items.Clear();
                SchoolAddressList.Items.Add("--Select--");
                //txtSchoolCode.Text = ds.Tables[0].Columns["SchCode"].ToString();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);


                da.Fill(ds);

                SchoolAddressList.DataTextField = ds.Tables[0].Columns["PhysStreet"].ToString();
                SchoolAddressList.DataValueField = ds.Tables[0].Columns["PhysStreet"].ToString();


                SchoolAddressList.DataSource = ds.Tables[0];
                SchoolAddressList.DataBind();

                SchoolAddressList.Items.Insert(0, ("--Select---"));

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

        private void FillSchoolCode()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string schoolname = SchoolNameList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            DataSet ds = new DataSet();
            string ActiveSchool = System.Configuration.ConfigurationManager.AppSettings["ActiveSchool"];

            try
            {

                if ((type == "PUBLIC"))
                {
                    sql = "SELECT DISTINCT SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY SchCode";

                }
                else {
                    sql = "SELECT distinct SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'S' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY SchCode";
                }

                txtSchoolCode.Text = "";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    txtSchoolCode.Text = sdr[0] as String;
                }
                con.Close();
                txtPassword.Focus();


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


        protected void TypeList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if ((TypeList.SelectedItem.Value == "--Select--"))
                {
                    CountyList.Items.Clear();
                    CountyList.Items.Add("--Select--");
                    DistrictList.Enabled = true;
                    DistrictList.Items.Clear();
                    DistrictList.Items.Add("--Select--");
                    CityList.Enabled = true;
                    CityList.Items.Clear();
                    CityList.Items.Add("--Select--");
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else if ((TypeList.SelectedItem.Value == "PRIVATE"))
                {
                    FillCounty();
                    DistrictList.Items.Clear();
                    DistrictList.Items.Add("--Select--");
                    DistrictList.Enabled = false;
                    CityList.Enabled = true;
                    CityList.Items.Clear();
                    CityList.Items.Add("--Select--");
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    FillCounty();
                    DistrictList.Enabled = true;
                    DistrictList.Items.Clear();
                    DistrictList.Items.Add("--Select--");
                    CityList.Enabled = false;
                    CityList.Items.Clear();
                    CityList.Items.Add("--Select--");
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }

        }

        protected void CountyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((CountyList.SelectedItem.Text == "--Select--"))
                {
                    if ((DistrictList.Enabled == true))
                    {
                        DistrictList.Items.Clear();
                        DistrictList.Items.Add("--Select--");
                    }
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    if ((DistrictList.Enabled == true))
                    {
                        Filldistrict();
                        SchoolNameList.Items.Clear();
                        SchoolNameList.Items.Add("--Select--");
                    }
                    else {
                        //FillSchool();
                        FillCity(); // Added City Search
                    }
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }

        }

        protected void DistrictList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((DistrictList.SelectedItem.Text == "--Select--"))
                {
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    FillSchool();
                    //FillCity();
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void SchoolNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((SchoolNameList.SelectedItem.Text == "--Select--"))
                {
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    FillAddress();
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void SchoolAddressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((SchoolAddressList.SelectedItem.Text == "--Select--"))
                {
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    FillSchoolCode();
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        // I believe this funciton is for user if they fill out the school code first.  Work on later

        protected void txtSchoolCode_TextChanged(object sender, EventArgs e)
        {
            if (DoesSchoolExist() == true)
            {

                if (IsSchoolActive() == true)
                {
                    MessagesRow.Visible = false;
                    txtPassword.Enabled = true;
                    txtPassword.Focus();
                }
                else {
                    //inactive schools
                    lblErrorMsg.Text = "This school code is not active." + "<br />" + "Please see <A href=\"https://www.shotsforschool.org/reporting/kindergarten/faqs/\" target='_blank' alt='FAQs' title='FAQs'>FAQs</a> to find your school code then contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a> or your <A href=\"https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx\" target='_blank' alt='LHD' title='LHD'>local health department</a> with your school code and grade reporting to reactivate.";
                    MessagesRow.Visible = true;
                    txtPassword.Enabled = false;
                }
            }
            else {
                lblErrorMsg.Text = "This school code does not exist." + "<br />" + "Please see <A href=\"https://www.shotsforschool.org/reporting/kindergarten/faqs/\" target='_blank' alt='FAQs' title='FAQs'>FAQs</a> to find your school code then contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a> with your school code and grade reporting to reactivate.";
                MessagesRow.Visible = true;
                txtPassword.Enabled = false;
            }

        }



        private void CheckPassword(ref bool pwdstatus)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string Inpassword = (String)txtPassword.Text.Trim();
            String schoolCode = txtSchoolCode.Text;
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];


            DataSet ds = new DataSet();

            string defaultPassword = System.Configuration.ConfigurationManager.AppSettings["kDefaultPassword"];
            string DBpassword = "";

            //string sql = "SELECT TOP 1 id, SchCode, Password FROM Schools where Cohort = 'K' and SchCode = '" + schoolCode + "'";
            //string sql = "SELECT TOP 1 id FROM Schools WHERE Cohort = 'K' and SchCode = '" + schoolCode + "'";
            string sql = "SELECT TOP 1 a.Assmntid FROM Schools S INNER JOIN Assessments A ON S.id = a.id where S.Cohort = 'S' and S.SchCode = '" + schoolCode + "' AND A.schoolYear = " + SchoolYear;

            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if ((sdr.HasRows == true))
                {
                    sdr.Read();
                    //DBpassword = sdr[2] as String;  //("Password").ToString());

                    if (((String)Inpassword.ToUpper() == (String)defaultPassword.ToUpper()))
                    {
                        // Commented out by A.T. on 08/20/2014 Since No longer accepting password from each user
                        //If ((DBpassword <> "" And Inpassword = DBpassword) Or (DBpassword = "" And UCase(Inpassword) = UCase(defaultPassword))) Then
                        pwdstatus = true;

                        Session["7th_Assessment_id"] = sdr[0];
                        Set8thGradeAsmntID();
                    }
                    else {
                        pwdstatus = false;
                    }
                }
                else {
                    pwdstatus = false;
                }
                //LoginAuditTrail(pwdstatus);
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
                //sdr = null;
            }
        }



        protected void CityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((CityList.SelectedItem.Text == "--Select--"))
                {
                    if ((DistrictList.Enabled == true))
                    {
                        DistrictList.Items.Clear();
                        DistrictList.Items.Add("--Select--");
                    }
                    SchoolNameList.Items.Clear();
                    SchoolNameList.Items.Add("--Select--");
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
                else {
                    if ((DistrictList.Enabled == true))
                    {
                        FillSchool();
                        //SchoolNameList.Items.Clear();
                        //SchoolNameList.Items.Add("--Select--");
                    }
                    else {
                        FillSchool();
                    }
                    SchoolAddressList.Items.Clear();
                    SchoolAddressList.Items.Add("--Select--");
                    txtSchoolCode.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        protected void ImgbtnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            bool pwdstatus = true;
            string isComplete = "N";
            String schoolCode = txtSchoolCode.Text;

            SessionIDManager manager = new SessionIDManager();
            Session["STATE_SESSIONID"] = manager.CreateSessionID(Context);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string sql = "SELECT A.isComplete FROM Schools S INNER JOIN Assessments A ON S.id = A.id WHERE S.Cohort = 'S' AND A.SchCode = '" + schoolCode + "' AND A.SchoolYear = '" + SchoolYear + "'";


            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    isComplete = (String)reader["isComplete"].ToString();
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
            }

            if ((Page.IsValid))
            {
                CheckPassword(ref pwdstatus);

                // Added SchoolExistance and inactive checks here just in case user hit 'enter' on the textSchcode area after initial error of school existance check.
                // 06/12/2020
                if (DoesSchoolExist() == false)
                {
                    lblErrorMsg.Text = "This school code does not exist." + "<br />" + "Please see <A href=\"https://www.shotsforschool.org/reporting/kindergarten/faqs/\" target='_blank' alt='FAQs' title='FAQs'>FAQs</a> to find your school code then contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a> with your school code and grade reporting to reactivate.";
                    MessagesRow.Visible = true;
                    txtPassword.Enabled = false;
                }
                else if (IsSchoolActive() == false)
                {
                    lblErrorMsg.Text = "This school code is not active." + "<br />" + "Please see <A href=\"https://www.shotsforschool.org/reporting/kindergarten/faqs/\" target='_blank' alt='FAQs' title='FAQs'>FAQs</a> to find your school code then contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a> or your <A href=\"https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx\" target='_blank' alt='LHD' title='LHD'>local health department</a> with your school code and grade reporting to reactivate.";
                    MessagesRow.Visible = true;
                    txtPassword.Enabled = false;

                }
                else if(pwdstatus == true)
                {
                    if (isComplete == "N")
                    {
                        Response.Redirect("LoginConfirmed.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("ViewAndPrint.aspx", false);
                    }
                }
                else {
                    MessagesRow.Visible = true;
                    lblErrorMsg.Text = "Incorrect Password (<a href=\"ForgotPassword.aspx\">What's my password?</a>)";
                    txtPassword.Focus();
                }
            }
        }


        private bool DoesSchoolExist()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            dynamic schCode = txtSchoolCode.Text;
            string sql = "SELECT COUNT(*) AS sch_num FROM Schools where SchCode ='" + schCode + "' AND cohort = 'S'";
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
            finally
            {
                cmd = null;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private bool IsSchoolActive()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string schoolcode = txtSchoolCode.Text.Replace("'", "''");
            string sql = "SELECT COUNT(*) AS sch_num FROM schools s WHERE s.SchCode = '" + schoolcode + "' AND s.cohort = 'S' AND EXISTS (SELECT * FROM assessments WHERE id = s.id AND SchoolYear = '" + SchoolYear + "')";
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);

            try
            {
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                return Convert.ToInt32(reader["sch_num"].ToString()) > 0;


            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
                return false;
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

        private void Set8thGradeAsmntID()
        {
            string EithGradeID = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            String schoolCode = txtSchoolCode.Text;
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sql = "SELECT TOP 1 a.Assmntid FROM Schools S INNER JOIN Assessments A ON S.id = a.id where S.Cohort = 'E' and S.SchCode = '" + schoolCode + "' AND A.schoolYear = " + SchoolYear;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();

                if ((sdr.HasRows == true))
                {
                    sdr.Read();

                    Session["E_Assessment_id"] = sdr[0];
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
            }


            //return EithGradeID;
        }
    }
}