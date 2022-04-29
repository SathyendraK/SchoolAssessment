using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;


namespace SchoolAssessment.KG
{
    public partial class LoginForgotSchCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                MessagesRow.Visible = false;

                FillType();

                CountyList.Items.Add("--Select--");
                DistrictList.Items.Add("--Select--");
                CityList.Items.Add("--Select--");
                SchoolNameList.Items.Add("--Select--");
                SchoolAddressList.Items.Add("--Select--");
            }

            if (Page.IsPostBack)
            {
                MessagesRow.Visible = false;
                txtPassword.Enabled = true;
            }

            if ((Request.QueryString["reason"] == "TimedOut"))
            {
                MessagesRow.Visible = true;
                lblErrorMsg.Text = "Session timed out. Please log in.";
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
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT CoName FROM Counties ORDER BY CoName ASC", con);
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
                    sql = "SELECT DISTINCT S.PhysCity FROM Schools S INNER JOIN[Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and SchType = '" + type + "' and D.DistName = '" + district + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY S.PhysCity ASC";

                }
                else {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT S.PhysCity FROM Schools S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and CoName = '" + county + "' and SchType = '" + type + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY S.PhysCity ASC";
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
            string city = CityList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string ActiveSchool = System.Configuration.ConfigurationManager.AppSettings["ActiveSchool"];

            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' and DistName = '" + district + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode INNER JOIN Assessments A on A.id = S.id WHERE Cohort = 'K' and C.CoName = '" + county + "' and SchType = '" + type + "' and D.DistName = '" + district + "' AND A.schoolYear = '" + SchoolYear + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY[SchName] ASC";

                }
                else {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Counties] C ON C.CoCode = S.Cocode INNER JOIN Assessments A on A.id = S.id WHERE Cohort = 'K' and CoName = '" + county + "' and  S.Physcity = '" + city + "' and SchType = '" + type + "' AND A.schoolYear = '" + SchoolYear + "' and S.EditDate > '" + ActiveSchool + "' ORDER BY[SchName] ASC";
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
                    sql = "SELECT DISTINCT PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "'  ORDER BY PhysStreet";

                }
                else {
                    //sql = "SELECT distinct SchCode, PhysStreet, PhysCity FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' ORDER BY PhysCity, PhysStreet";
                    sql = "SELECT distinct PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "'  ORDER BY PhysStreet";
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
            // AT on 11/14/2016
            string selectedAddress = SchoolAddressList.SelectedItem.Value.Replace("'", "''");

            try
            {

                if ((type == "PUBLIC"))
                {
                    sql = "SELECT DISTINCT SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' and S.PhysStreet = '" + selectedAddress + "' ORDER BY SchCode";

                }
                else {
                    sql = "SELECT distinct SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' and S.EditDate > '" + ActiveSchool + "' and S.PhysStreet = '" + selectedAddress + "' ORDER BY SchCode";
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string schoolcode = txtSchoolCode.Text.Replace("'", "''");
            SqlCommand cmd = new SqlCommand("SELECT S.SchType, C.CoName, ISNULL(D.DistName,'') AS 'DistName', S.SchName, S.SchCode FROM Schools S INNER JOIN Counties C on S.CoCode = C.CoCode LEFT OUTER JOIN Districts D ON S.DistCode = D.DistCode WHERE SchCode = '" + schoolcode + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();


            try
            {
                if ((!string.IsNullOrEmpty(txtSchoolCode.Text)))
                {

                    da.Fill(ds);

                    //if ((ds.Tables[0].Rows.Count != 0))
                    if (DoesSchoolExist() == true)
                    {
                        FillType();
                        TypeList.SelectedItem.Value = ds.Tables[0].Columns["SchType"].ToString();
                        FillCounty();
                        CountyList.SelectedItem.Value = ds.Tables[0].Columns["CoName"].ToString();

                        if ((TypeList.SelectedItem.Value == "PUBLIC"))
                        {
                            Filldistrict();
                            DistrictList.SelectedItem.Value = ds.Tables[0].Columns["DistName"].ToString();
                        }
                        else {
                            DistrictList.SelectedItem.Value = "--Select--";
                            DistrictList.Enabled = false;
                        }

                        FillSchool();
                        SchoolNameList.SelectedItem.Value = ds.Tables[0].Columns["SchName"].ToString();
                        FillAddress();
                        SchoolNameList.SelectedItem.Value = ds.Tables[0].Columns["SchCode"].ToString();

                        if (IsSchoolActive() == true) {
                            MessagesRow.Visible = false;
                            txtPassword.Enabled = true;
                            txtPassword.Focus();
                        }
                        else {
                            //inactive schools
                            lblErrorMsg.Text = "This school code is not active" + "<br />" + "Please contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a> with your school code to reactivate.";
                            MessagesRow.Visible = true;
                            txtPassword.Enabled = false;
                        }
                    }
                    else {
                        //lblErrorMsg.Text = "This school code does not exist" & "<br />" & "Please contact <a href=""mailto:reporting-help@shotsforschool.org"">reporting-help@shotsforschool.org</a>" 'Commented out by AT on 09/16/2015
                        lblErrorMsg.Text = "This school code does not exist" + "<br />" + "Please retry or see <a href=\"http://cairweb.org/calkidshots/KSeventhFAQs.pdf\" target='_blank' alt='FAQs' title='FAQs'>FAQs</a> to search.";
                        MessagesRow.Visible = true;
                        txtPassword.Enabled = false;
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
                con.Close();
            }


        }



        private void CheckPassword(ref bool pwdstatus)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string Inpassword = (String)txtPassword.Text.Trim();
            String schoolCode = txtSchoolCode.Text;

            DataSet ds = new DataSet();

            string defaultPassword = System.Configuration.ConfigurationManager.AppSettings["kDefaultPassword"];
            string DBpassword = "";
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];

            //string sql = "SELECT TOP 1 id, SchCode, Password FROM Schools where Cohort = 'K' and SchCode = '" + schoolCode + "'";
            //string sql = "SELECT TOP 1 id FROM Schools WHERE Cohort = 'K' and SchCode = '" + schoolCode + "'";
            string sql = "SELECT TOP 1 a.Assmntid FROM Schools S INNER JOIN Assessments A ON S.id = a.id where S.Cohort = 'K' and S.SchCode = '" + schoolCode + "' AND A.schoolYear = " + SchoolYear;
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

                        Session["K_Assessment_id"] = sdr[0];

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

        protected void BtnForgotSchCode_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForgotSchCode.aspx", true);
        }

        protected void ImgBtnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            bool pwdstatus = true;
            string isComplete = "N";
            String schoolCode = txtSchoolCode.Text;

            SessionIDManager manager = new SessionIDManager();
            Session["STATE_SESSIONID"] = manager.CreateSessionID(Context);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string sql = "SELECT A.isComplete FROM Schools S INNER JOIN Assessments A ON S.id = A.id WHERE S.Cohort = 'K' AND A.SchCode = '" + schoolCode + "' AND A.SchoolYear = '" + SchoolYear + "'";


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
                if (pwdstatus == true)
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
            string sql = "SELECT COUNT(*) AS sch_num FROM Schools where SchCode ='" + schCode + "' AND cohort = 'K'";
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
                con.Close();
            }
        }

        private bool IsSchoolActive()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string schoolcode = txtSchoolCode.Text.Replace("'", "''");
            string sql = "SELECT COUNT(*) AS sch_num FROM schools s WHERE s.SchCode = '" + schoolcode + "' AND s.cohort = 'K' AND EXISTS (SELECT * FROM assessments WHERE id = s.id AND SchoolYear = '" + SchoolYear + "')";
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
                con.Close();
            }
        }


    }
}