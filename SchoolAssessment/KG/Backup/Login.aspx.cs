/*using System;
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

/* Still Missing
 1. Display city in Address 
 2. School Code Check 
 3. Login Audit Trail
*/

namespace SchoolAssessment.Models.KG
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                MessagesRow.Visible = false;
                FillType();
                
                CountyList.Items.Add("--Select--");
                DistrictList.Items.Add("--Select--");
                SchoolNameList.Items.Add("--Select--");
                SchoolAddressList.Items.Add("--Select--");
                //if ((Request.QueryString("reason") == "TimedOut"))
                if ((Request.QueryString["reason"] == "TimedOut"))
                {
                    MessagesRow.Visible = true;
                    lblErrorMsg.Text = "Session timed out. Please log in.";
                }
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
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
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
        }
               
        private void Filldistrict()
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
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
        }

        
        private void FillSchool()
        {

            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;


            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' and DistName = '" + district + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and SchType = '" + type + "' and D.DistName = '" + district + "' ORDER BY[SchName] ASC";

                }
                else {
                    //sql = "SELECT distinct [SchName] FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY [SchName] asc";
                    sql = "SELECT DISTINCT [SchName] FROM Schools S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and CoName = '" + county + "' and SchType = '" + type + "' ORDER BY[SchName] ASC";
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
        }

        
        private void FillAddress()
        {
            //string schoolyear = System.Configuration.ConfigurationManager.AppSettings("SchoolYear");
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string schoolname = SchoolNameList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            DataSet ds = new DataSet();
            try
            {
                if ((type == "PUBLIC"))
                {
                    //sql = "SELECT DISTINCT SchCode, PhysStreet, PhysCity FROM SCHOOLS INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' ORDER BY PhysCity, PhysStreet";
                    sql = "SELECT DISTINCT PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' ORDER BY PhysStreet";

                }
                else {
                    //sql = "SELECT distinct SchCode, PhysStreet, PhysCity FROM K_Assessment where SchoolYear = '" + schoolyear + "' and CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' ORDER BY PhysCity, PhysStreet";
                    sql = "SELECT distinct PhysStreet, PhysCity, SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' ORDER BY PhysStreet";
                }

                SchoolAddressList.Items.Clear();
                SchoolAddressList.Items.Add("--Select--");
                //txtSchoolCode.Text = ds.Tables[0].Columns["SchCode"].ToString();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
               

                da.Fill(ds);

                SchoolAddressList.DataTextField = ds.Tables[0].Columns["PhysStreet"].ToString();
                SchoolAddressList.DataValueField = ds.Tables[0].Columns["PhysStreet"].ToString();
                //SchoolAddressList.DataTextField = ds.Tables[0].Columns["PhysStreet"].ToString() + "," + ds.Tables[0].Columns["PhysCity"].ToString();
                //SchoolAddressList.DataValueField = ds.Tables[0].Columns["PhysStreet"].ToString() + "," + ds.Tables[0].Columns["PhysCity"].ToString();

                SchoolAddressList.DataSource = ds.Tables[0];
                SchoolAddressList.DataBind();

                SchoolAddressList.Items.Insert(0, ("--Select---"));

            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
        }

        private void FillSchoolCode()
        {
            
            string county = CountyList.SelectedItem.Value.Replace("'", "''");
            string district = DistrictList.SelectedItem.Value.Replace("'", "''");
            string schoolname = SchoolNameList.SelectedItem.Value.Replace("'", "''");
            string type = TypeList.SelectedItem.Value;
            string sql = null;
            DataSet ds = new DataSet();

            try
            {
                
                if ((type == "PUBLIC"))
                {
                    sql = "SELECT DISTINCT SchCode FROM SCHOOLS S INNER JOIN [Districts] D ON D.DistCode = S.DistCode INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName = '" + county + "' and[SchType] = '" + type + "'and D.DistName = '" + district + "' and SchName = '" + schoolname + "' ORDER BY SchCode";

                }
                else {
                    sql = "SELECT distinct SchCode FROM SCHOOLS S INNER JOIN[Counties] C ON C.CoCode = S.Cocode WHERE Cohort = 'K' and C.CoName ='" + county + "' and [SchType] = '" + type + "'and SchName = '" + schoolname + "' ORDER BY SchCode";
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
            /*
            //string schoolyear = System.Configuration.ConfigurationManager.AppSettings("SchoolYear");
            string schoolcode = txtSchoolCode.Text.Replace("'", "''");
            string sql = "SELECT * FROM K_Assessment where SchoolYear = '" + schoolyear + "' and [SchCode] ='" + schoolcode + "'";
            DataSet ds = new DataSet();
            try
            {
                if ((!string.IsNullOrEmpty(txtSchoolCode.Text)))
                {
                    objbl.FillNewDataSet(ds, sql);
                    if ((ds.Tables(0).Rows.Count != 0))
                    {
                        FillType();
                        TypeList.SelectedValue = Strings.Trim(Strings.UCase(ds.Tables(0).Rows(0)("SchType").ToString()));
                        FillCounty();
                        CountyList.SelectedValue = Strings.Trim(Strings.UCase(ds.Tables(0).Rows(0)("CoName").ToString()));
                        if ((TypeList.SelectedValue == "PUBLIC"))
                        {
                            Filldistrict();
                            DistrictList.SelectedValue = Strings.Trim(Strings.UCase(ds.Tables(0).Rows(0)("DistName").ToString()));
                        }
                        else {
                            DistrictList.SelectedValue = "--Select--";
                            DistrictList.Enabled = false;
                        }
                        FillSchool();
                        SchoolNameList.SelectedValue = Strings.Trim(Strings.UCase(ds.Tables(0).Rows(0)("SchName").ToString()));
                        FillAddress();
                        SchoolAddressList.SelectedValue = Strings.Trim(Strings.UCase(ds.Tables(0).Rows(0)("SchCode").ToString()));
                        txtPassword.Focus();
                    }
                    else {
                        //lblErrorMsg.Text = "This school code does not exist" & "<br />" & "Please contact <a href=""mailto:reporting-help@shotsforschool.org"">reporting-help@shotsforschool.org</a>" 'Commented out by AT on 09/16/2015
                        lblErrorMsg.Text = "This school code does not exist" + "<br />" + "Please contact <a href=\"mailto:SchoolAssessments@cdph.ca.gov\">SchoolAssessments@cdph.ca.gov</a>";
                        MessagesRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                dynamic appError = ex.Message;
                Response.Write(appError);
            }
            */
        }

 

        private void CheckPassword(ref bool pwdstatus)
        {
            
            string Inpassword = (String)txtPassword.Text.Trim();
            String schoolCode = txtSchoolCode.Text;

            DataSet ds = new DataSet();

            string defaultPassword = System.Configuration.ConfigurationManager.AppSettings["kDefaultPassword"];
            string DBpassword = "";
            
            //string sql = "SELECT TOP 1 id, SchCode, Password FROM Schools where Cohort = 'K' and SchCode = '" + schoolCode + "'";
            string sql = "SELECT TOP 1 id FROM Schools WHERE Cohort = 'K' and SchCode = '" + schoolCode + "'";

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

        protected void btLogin_Click(object sender, EventArgs e)
        {
            bool pwdstatus = true;

            SessionIDManager manager = new SessionIDManager();
            Session["STATE_SESSIONID"] = manager.CreateSessionID(Context);

            if ((Page.IsValid))
            {
                CheckPassword(ref pwdstatus);
                if (pwdstatus == true)
                {
                    Response.Redirect("LoginConfirmed.aspx", false);
                }
                else {
                    MessagesRow.Visible = true;
                    lblErrorMsg.Text = "Incorrect Password (<a href=\"ForgotPassword.aspx\">What's my password?</a>)";
                    txtPassword.Focus();
                }
            }

        }
    }
}