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
 * 06/21/2018 AT 
 * Added btnConditionalOverdue_Click to list schools which has > 15% of Conditional + TME + Overdue   
 * 
 * 02/19/2019 AT 
 * Updateing btnConditionalOverdue_Click from 15% to 10 % 
 * 
 *  
 * * 06/03/2019 by AT
 * Added Pasadena as county (Cocode 61)
 * 
 * 06/11/219
 * Fixed to show the correct counties for FieldRep for 'Percentage Reported by County' chart
 */

namespace SchoolAssessment.Admin
{
    public partial class AdminChart7th : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sesAdminUserTYpe = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
                //ElseIf (Session["AdminUserType"] <> "ADMIN") Then
            }
            else
            {
                sesAdminUserTYpe = Session["AdminUserType"].ToString();
            }


            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    if ((!Page.IsPostBack))
                    {
                        cmbDateChartCounties.Items.Clear();
                        cmbDateChartCounties.Items.Add(new ListItem("All", ""));

                        SqlCommand cmd1 = new SqlCommand("SELECT * from Counties where CoCode != '00' order by CoName ASC", con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        cmbDateChartCounties.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                        cmbDateChartCounties.DataValueField = ds.Tables[0].Columns["CoCode"].ToString();


                        cmbDateChartCounties.DataSource = ds.Tables[0];
                        cmbDateChartCounties.DataBind();
                    }
                    break;
                case "LHD":
                    panelCountyChart.Visible = false;
                    DateChartOptions.Visible = false;
                    break;
                case "FIELDREP":
                    lblCountyChartRegion.Visible = false;
                    cmbCountyChart.Visible = false;
                    if ((!Page.IsPostBack))
                    {


                        cmbDateChartCounties.Items.Clear();
                        cmbDateChartCounties.Items.Add(new ListItem("All", ""));

                        SqlCommand cmd2 = new SqlCommand("SELECT * from Counties where RegionCode = '" + Session["AdminRegionCode"] + "' order by CoName", con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd2);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        cmbDateChartCounties.DataTextField = ds.Tables[0].Columns["CoName"].ToString();
                        cmbDateChartCounties.DataValueField = ds.Tables[0].Columns["CoCode"].ToString();

                        cmbDateChartCounties.DataSource = ds.Tables[0];
                        cmbDateChartCounties.DataBind();

                    }
                    break;
            }

            //SqlDataAdapter adapter = new SqlDataAdapter();

            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sesAdminCoCode = Session["AdminCoCode"].ToString();


            // SQL FOR GETTING NUMBER OF REPORTED SCHOOLS BY DATE
            string sqlString = "SELECT t.SubmitDate, COUNT(t.SchCode) as NumberReported FROM (SELECT convert(date, t1.SubmitDate, 102) as SubmitDate, t1.SchCode FROM Assessments t1 INNER JOIN schools S on S.id = T1.id AND S.Cohort = 'S' ";
            string orderbyString = ") t GROUP BY t.SubmitDate ORDER BY t.SubmitDate ASC";
            string sql1 = "", sql2 = "", sql3 = "";
            SqlCommand cmd = new SqlCommand(sql1, con);

            // Cumulative Percentage Reported by Date Graph
            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    if ((string.IsNullOrEmpty(cmbDateChartCounties.SelectedValue)))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear ";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "01"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' ";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "19"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' ";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "59"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' ";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "60"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' ";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "61"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA' ";
                    }
                    else
                    {
                        //sql2 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = @CoCode ";
                        sql2 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '" + cmbDateChartCounties.SelectedValue + "' ";
                    }
                    break;
                case "LHD":
                    if ((sesAdminCoCode == "01"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' "; ;
                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'";
                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        sql1 = "WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY'";
                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' ";
                    }
                    else if ((sesAdminCoCode == "61"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA' ";
                    }
                    else {
                        sql2 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = @CoCode ";
                    }
                    break;
                case "FIELDREP":
                    if ((string.IsNullOrEmpty(cmbDateChartCounties.SelectedValue)))
                    {
                        sql3 = " left join Counties C on S.CoCode = C.CoCode WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode ";
                    }
                    else if ((sesAdminCoCode == "01"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' ";
                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'";
                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' ";
                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and S.SchoolYear = @SchoolYear and S.CoCode = '19' and t1.PhysCity = 'LONG BEACH'";
                    }
                    else if ((sesAdminCoCode == "61"))
                    {
                        sql1 = " WHERE t1.isComplete = @isComplete and S.SchoolYear = @SchoolYear and S.CoCode = '19' and t1.PhysCity = 'PASADENA'";
                    }
                    else {
                        //sql2 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = @CoCode ";
                        sql2 = " WHERE t1.isComplete = @isComplete and t1.SchoolYear = @SchoolYear and S.CoCode = '" + cmbDateChartCounties.SelectedValue + "' ";
                    }
                    break;
            }

            if (sql1 != "")
            {
                cmd = new SqlCommand(sqlString + sql1 + orderbyString, con);
                cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                cmd.Parameters.AddWithValue("@isComplete", "Y");

            }
            else if (sql2 != "")
            {
                cmd = new SqlCommand(sqlString + sql2 + orderbyString, con);
                cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                cmd.Parameters.AddWithValue("@isComplete", "Y");
                cmd.Parameters.AddWithValue("@CoCode", sesAdminCoCode);
                //cmd.Parameters.AddWithValue("@CoCode", selectedCounty.ToString());


            }
            else if (sql3 != "")
            {
                cmd = new SqlCommand(sqlString + sql3 + orderbyString, con);
                cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                cmd.Parameters.AddWithValue("@isComplete", "Y");
                cmd.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"].ToString());
            }

            //string test = "SELECT t.SubmitDate, COUNT(t.SchCode) as NumberReported FROM (SELECT convert(date, t1.SubmitDate, 102) as SubmitDate, SchCode FROM Assessments t1  WHERE t1.isComplete = 'Y' and t1.SchoolYear = '20152016' ) t GROUP BY t.SubmitDate ORDER BY t.SubmitDate ASC";
            //cmd = new SqlCommand(test, con);
            //cmd = new SqlCommand(sqlString + sql1 + orderbyString, con);
            //cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            //cmd.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];

            string output = null;
            int row = 0;
            int column = 0;
            string dateStr;
            DateTime dTime;


            if ((dt.Rows.Count > 0))
            {
                for (row = 0; row <= dt.Rows.Count - 1; row++)
                {
                    dTime = DateTime.Parse(dt.Rows[row][0].ToString());
                    output += dTime.ToString("yyyy-MM-dd") + "|" + dt.Rows[row][1].ToString() + ",";
                }

                chartData.Value = output.Substring(0, output.Length - 1);

            }

            //totalSchools.Value = "100";

            // Set CharCountyData 
            SetChartCountyData();

            // Set Total number of school
            SetTotalNumSchools();


        }

        private void SetChartCountyData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sesAdminCoCode = Session["AdminCoCode"].ToString();
            string sesAdminUserTYpe = "";
            sesAdminUserTYpe = Session["AdminUserType"].ToString();


            // SQL FOR GETTING NUMBER OF REPORTED SCHOOLS BY DATE
            string sqlString = "", sql0 = "", sql1 = "", sql2 = "", sqlOrderby = "";
            SqlCommand cmd = new SqlCommand(sql1, con);


            // SQL FOR GETTING NUMBER OF REPORTED SCHOOLS BY COUNTY
            // RETURNS COLUMNS (COUNTY NAME, TOTAL REPORTED, TOTAL REPORTED UP TO LAST WEEK, TOTAL SCHOOLS)
            //sql1 = "select t.CountyName, t.NumberReported, t.NumberReportedLastWeek, t.Total, cast((cast(t.NumberReported as float) * 100/t.Total) as decimal(6,2)) as Percentage from (select c.CountyName as CountyName, c.CountyCode as CountyCode, c.RegionCode as RegionCode, isnull(t1.NumberReported, 0) as NumberReported, isnull(t3.NumberReportedLastWeek, 0) as NumberReportedLastWeek, t2.Total as Total from Counties C left join (select CoCode, COUNT(SchCode) as NumberReported from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, SchCode, isComplete, SchoolYear from K_Assessment where isComplete = @isComplete and SchoolYear = @SchoolYear) as a group by CoCode) t1 on t1.CoCode = c.CountyCode left join (select CoCode, COUNT(SchCode) as NumberReportedLastWeek from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, SchCode, isComplete, SchoolYear from K_Assessment where isComplete = @isComplete and SchoolYear = @SchoolYear and SubmitDate <= GETDATE()-7) as b group by CoCode) t3 on t3.CoCode = c.CountyCode left join (select CoCode, COUNT(SchCode) as Total from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, SchCode, isComplete, SchoolYear from K_Assessment where SchoolYear = @SchoolYear) as c group by CoCode) t2 on t2.CoCode = c.CountyCode ) t";
            //sqlString = "select t.CountyName, t.NumberReported, t.NumberReportedLastWeek, t.Total, cast((cast(t.NumberReported as float) * 100/t.Total) as decimal(6,2)) as Percentage from (select c.CoName as CountyName, c.CoCode as CountyCode, c.RegionCode as RegionCode, isnull(t1.NumberReported, 0) as NumberReported, isnull(t3.NumberReportedLastWeek, 0) as NumberReportedLastWeek, t2.Total as Total from Counties C left join (select CoCode, COUNT(SchCode) as NumberReported from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, S.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN Schools S on A.schcode = S.SchCode where isComplete = @isComplete and  S.Cohort = 'S' SchoolYear = @SchoolYear) as a group by CoCode) t1 on t1.CoCode = c.CoCode left join (select CoCode, COUNT(SchCode) as NumberReportedLastWeek from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, S.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode where isComplete = @isComplete and SchoolYear = @SchoolYear and SubmitDate <= GETDATE()-7) as b group by CoCode) t3 on t3.CoCode = c.CoCode left join (select CoCode, COUNT(c.SchCode) as Total from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' else CoCode end as CoCode, S.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN Schools S on A.SchCode  = A.SchCode  where SchoolYear = @SchoolYear) as c group by CoCode) t2 on t2.CoCode = c.CoCode ) t";
            sqlString = "select t.CountyName, t.NumberReported, t.NumberReportedLastWeek, t.Total, cast((cast(t.NumberReported as float) * 100/t.Total) as decimal(6,2)) as Percentage from (select c.CoName as CountyName, c.CoCode as CountyCode, c.RegionCode as RegionCode, isnull(t1.NumberReported, 0) as NumberReported, isnull(t3.NumberReportedLastWeek, 0) as NumberReportedLastWeek, t2.Total as Total from Counties C left join (select CoCode, COUNT(SchCode) as NumberReported from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59'  when (CoCode = '19' and PhysCity = 'LONG BEACH') then '60' when (CoCode = '19' and PhysCity = 'PASADENA') then '61' else CoCode end as CoCode, A.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN Schools S on s.id = a.id where isComplete = @isComplete and SchoolYear = @SchoolYear and S.Cohort = 'S') as a group by CoCode) t1 on t1.CoCode = c.CoCode left join (select CoCode, COUNT(SchCode) as NumberReportedLastWeek from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' when (CoCode = '19' and PhysCity = 'LONG BEACH') then '60'  when (CoCode = '19' and PhysCity = 'PASADENA') then '61' else CoCode end as CoCode, S.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN Schools S on S.id = A.id where isComplete = @isComplete and SchoolYear = @SchoolYear and SubmitDate <= GETDATE()-7 and S.Cohort = 'S') as b group by CoCode) t3 on t3.CoCode = c.CoCode left join (select CoCode, COUNT(SchCode) as Total from (select case when (CoCode = '01' and PhysCity = 'BERKELEY') then '59' when (CoCode = '19' and PhysCity = 'LONG BEACH') then '60'  when (CoCode = '19' and PhysCity = 'PASADENA') then '61' else CoCode end as CoCode, s.SchCode, isComplete, SchoolYear from Assessments A INNER JOIN SCHOOLS S ON A.id = S.id where SchoolYear = @SchoolYear and s.Cohort = 'S') as c group by CoCode) t2 on t2.CoCode = c.CoCode ) t";

            if ((!Page.IsPostBack | cmbCountyChartSort.SelectedValue == "CoName"))
            {
                sqlOrderby = "order by t.CountyName asc";
            }
            else {
                sqlOrderby = "order by Percentage asc";
            }

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    if ((!Page.IsPostBack | cmbCountyChart.SelectedValue == "00"))
                    {
                        sql0 = " where t.Regioncode > 0 ";
                    }
                    else {
                        sql1 = " where t.Regioncode = @RegionCode ";
                    }

                    break;
                case "LHD":
                    sql2 = " where t.CountyCode = @CoCode ";

                    break;
                case "FIELDREP":
                    sql1 = " where t.RegionCode = @RegionCode ";
                    // Fixed to show the correct counties for FieldRep for 'Percentage Reported by County' chart
                    cmbCountyChart.SelectedValue = Session["AdminRegionCode"].ToString();
                    break;
            }



            if (sql0 != "")
            {

                cmd = new SqlCommand(sqlString + sql0 + sqlOrderby, con);
            }
            else if (sql1 != "")
            {
                cmd = new SqlCommand(sqlString + sql1 + sqlOrderby, con);
                cmd.Parameters.AddWithValue("@RegionCode", cmbCountyChart.SelectedValue);

            }
            else if (sql2 != "")
            {
                cmd = new SqlCommand(sqlString + sql2 + sqlOrderby, con);
                cmd.Parameters.AddWithValue("@CoCode", cmbDateChartCounties.SelectedValue);
            }

            cmd.Parameters.AddWithValue("@isComplete", "Y");
            cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            string output = "";
            int row = 0;


            if ((dt.Rows.Count > 0))
            {
                for (row = 0; row <= dt.Rows.Count - 1; row++)
                {
                    output += dt.Rows[row][0] + "|" + dt.Rows[row][1].ToString() + "|" + dt.Rows[row][2].ToString() + "|" + dt.Rows[row][3].ToString() + ",";
                }


                chartCountyData.Value = output.Substring(0, output.Length - 1);
            }

        }

        private void SetTotalNumSchools()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string sesAdminCoCode = Session["AdminCoCode"].ToString();
            string sesAdminUserTYpe = "";
            sesAdminUserTYpe = Session["AdminUserType"].ToString();



            // SQL FOR GETTING NUMBER OF REPORTED SCHOOLS BY DATE
            string sql = "SELECT COUNT(t1.SchCode) AS total FROM Assessments t1 INNER JOIN Schools S ON t1.id = S.id AND S.cohort = 'S'";

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    if ((string.IsNullOrEmpty(cmbDateChartCounties.SelectedValue)))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "01"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "19"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "59"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity = 'BERKELEY'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "60"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and t1.CoCode = '19' and t1.PhysCity = 'LONG BEACH'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "61"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and t1.CoCode = '19' and t1.PhysCity = 'PASADENA'";
                    }
                    else {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '" + cmbDateChartCounties.SelectedValue + "'";
                    }
                    break;
                case "LHD":
                    if ((sesAdminCoCode == "01"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY'";
                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'";
                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity = 'BERKELEY'";
                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH'";
                    }
                    else if ((sesAdminCoCode == "61"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity = 'PASADENA'";
                    }
                    else {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '" + Session["AdminCoCode"].ToString()  + "'";
                    }
                    break;
                case "FIELDREP":
                    if ((string.IsNullOrEmpty(cmbDateChartCounties.SelectedValue)))
                    {
                        sql += " left join Counties C on t1.CoCode = C.CountyCode WHERE t1.SchoolYear = '" + SchoolYear + "' and C.RegionCode = '" + Session["AdminRegionCode"] + "'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "01"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "19"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "59"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '01' and S.PhysCity = 'BERKELEY'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "60"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH'";
                    }
                    else if ((cmbDateChartCounties.SelectedValue == "61"))
                    {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '19' and S.PhysCity = 'PASADENA'";
                    }
                    else {
                        sql += " WHERE t1.SchoolYear = '" + SchoolYear + "' and S.CoCode = '" + cmbDateChartCounties.SelectedValue + "'";
                    }
                    break;
            }


            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    totalSchools.Value = reader["total"].ToString();
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

            /*
            SqlCommand cmd = new SqlCommand(sql);
            SqlDataReader reader = default(SqlDataReader);
            cmd.Connection = con;
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            totalSchools.Value = reader("total").ToString();
            command = null;
            */


        }

        protected void btnAllList_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();

            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,   S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType,S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";

            //string sqlString = "SELECT C.CoCode as CoCode, C.CoName as CoName, ISNULL(D.DistCode, '') as DistCode, ISNULL(D.DistName, '') as DistName, S.SchCode as SchCode,   S.SchName as SchoolName, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP', ISNULL(t1.MedExmp, 0) as 'Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'IEP Services', ISNULL(t1.IndependentStudy, 0) as 'Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'Home Based Private School', ISNULL(t1.EnrolledButNotAttending, 0) as 'Overdue', ISNULL(t1.TempMedExemption, 0) as 'Temporary Medical Exemption', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as SchoolType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            //string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode, S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName, S.SchName as SchoolName, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP', ISNULL(t1.MedExmp, 0) as 'Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'IEP Services', ISNULL(t1.IndependentStudy, 0) as 'Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'Home Based Private School', ISNULL(t1.EnrolledButNotAttending, 0) as 'Overdue', ISNULL(t1.TempMedExemption, 0) as 'Temporary Medical Exemption', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as SchoolType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            //string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode, S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName, S.SchName as SchoolName, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.TotNo as Enrollment, t1.MedExmp as PME, t1.BeleExmp as PBE, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as FacilityType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.SchCode = S.SchCode INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";


            // added sqlString_admin by A.T. on 09/23/2014 to show PBE sub fields for ONLY ADMIN user type
            //string sqlString_admin = "SELECT t1.CoCode as CoCode, t1.SchCode as FacilityCode, t1.CoName as CoName, t1.SchName as FacilityName, t1.PhysStreet as PhysStreet, t1.PhysCity as PhysCity, t1.PhysZip as PhysZip, t1.MailStreet as MailStreet, t1.MailCity as MailCity, t1.MailZip as MailZip, t1.SchPhone as SchPhone, t1.SchAdmin as SchAdmin, t1.SchEmail as SchEmail, t1.SuperintendentName as SuperintendentName, t1.SuperintendentEmail as SuperintendentEmail,  t1.FormPerson as StaffCompletingReport, t1.FormPhone as StaffCompletingReportPhone, t1.FormPhoneExt as StaffCompletingReportPhoneExt, t1.FormEmail as StaffCompletingReportEmail, t1.ContactPerson as DesignatedContact, t1.ContactPhone as DesignatedPhone, t1.ContactPhoneExt as DesignatedPhoneExt, t1.ContactEmail as DesignatedEmail, t1.KidCountUnderTwo as EnrollmentUnder2, t1.confirmedUnderTwo as ImmCheckedUnder2, t1.KidCountOverTwo as Enrollment, t1.Polio as Polio0, t1.Polio1 as Polio1, t1.Polio2 as Polio2, t1.Polio3 as Polio3, t1.Dtp as DTP0, t1.Dtp1 as DTP1, t1.Dtp2 as DTP2, t1.Dtp3 as DTP3, t1.Dtp4 as DTP4, t1.MMR as MMR0, t1.MMR1 as MMR1, t1.HIB as HIB0, t1.HIB1 as HIB1, t1.HepB as HepB0, t1.HepB1 as HepB1, t1.HepB2 as HepB2, t1.HepB3 as HepB3, t1.Varicella as Varicella0, t1.Varicella1 as Varicella1, t1.PME as PME, t1.PBE as PBE, t1.PBE_PreJanuaryExmpt as PBE_PreJanuaryExmpt, t1.PBE_HealthCareExmpt as PBE_HealthCareExmpt, t1.PBE_ReligiousExmpt as PBE_ReligiousExmpt, t1.FollowUp as FollowUp, t1.NoFollowUp as NoFollowUp, t1.SPACode as SPACode, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.SchType as FacilityType, t1.Status as Status, t1.AddSchoolDate as AddSchoolDate, t1.SubmitDate as SubmitDate, t1.ReviseDate as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.Password as Password, t1.skipSummary as skipSummary, t1.isReset as isReset, t1.SchoolYear as SchoolYear, t1.Memo as Memo from CC_Assessment t1";
            string sqlString_admin = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,  S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    // Updated sqlString by A.T. on 09/23/2014 to Show PBE sub fields to ONLY Admin. 
                    // command = New SqlCommand(sqlString & " where t1.SchoolYear = @SchoolYear order by t1.CoName Asc, t1.SchType Asc, t1.DistName Asc, t1.SchName Asc", conn)
                    command = new SqlCommand(sqlString_admin + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' order by C.CoName Asc, S.SchType Asc, S.SchName Asc", con);
                    break;

                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.CoCode = '01' and S.PhysCity <> 'BERKELEY' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S'  and C.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.CoCode = '01' and S.PhysCity = 'BERKELEY' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.CoCode = '19' and S.PhysCity = 'LONG BEACH' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.CoCode = '19' and S.PhysCity = 'PASADENA' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.CoCode = @CoCode order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and C.RegionCode = @RegionCode order by C.CoName Asc, S.SchType Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];



            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=7thGAll.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
                //Response.Write("<td>" + dv[row][column] + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else {
                                Response.Write("<td>" + (dt.Rows[row][column]).ToString() + "</td>");

                            }
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();

        }

        protected void btnNotReported_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();


            //string sqlString = "SELECT S.CoCode as CoCode, C.CoName as CoName, S.DistCode as DistCode, D.DistName as DistName, t1.SchCode as SchCode, S.SchName as SchName, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  ISNULL(S.ContactPerson, '') as DesignatedContact, ISNULL(S.ContactPhone, '') as DesignatedPhone, ISNULL(S.ContactPhoneExt, '') as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, S.SchType as Type, S.isCharter as Charter, T1.StudentYesNo as KThisYear, t1.Reason as WhyNoK,  'Not Reported' as ReportingStatus, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,  S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '01' and S.PhysCity = 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity = 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = @CoCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "N");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=7thNotReported.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "DistCode":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnReported_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();


            //string sqlString = "SELECT S.CoCode as CoCode, C.CoName as CoName, S.DistCode as DistCode, t1.SchCode as SchCode, S.SchName as SchName,  t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  ISNULL(S.ContactPerson, '') as DesignatedContact, ISNULL(S.ContactPhone, '') as DesignatedPhone, ISNULL(S.ContactPhoneExt, '') as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, S.SchType as Type, S.isCharter as Charter, T1.StudentYesNo as KThisYear, t1.Reason as WhyNoK,  'Reported' as ReportingStatus, convert(varchar(10), t1.SubmitDate, 101) as SubmitDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode ";
            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,   S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";


            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '01' and S.PhysCity = 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = '19' and S.PhysCity = 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'S' and S.CoCode = @CoCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=7thReported.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "DistCode":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnSummaryReport_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            // ask Kristen which informaiton needs to be on the admin report

            // Added t2.submitDate by A.T. on 09/24/2014
            string sqlString = "select t1.SchCode as [School Code], isnull(S.SchName, 'ALL SCHOOLS') as [School], S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%],  ISNULL(t1.PME, 0) AS [TDap_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_PME%], t1.OtherIEP as [TDap_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_IEP%],t1.IndependentStudy as [TDap_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Independent Study%],t1.HomeBasedPrivate as [TDap_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Home Based Private%],  t1.Conditional as [TDap_Conditional], case when (t1.Enrollment > 0) then round(t1.Conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Conditional%], t1.TempMedExemption as [TDap_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_TME%], t1.EnrolledButNotAttending as [TDap_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Overdue%],t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%],  t1.V_PME AS [Var_PME], case when (t1.Enrollment > 0) then round(t1.V_PME*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], t1.V_MDMO_PermMedExmp as [Var_PMEpox], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%],t1.V_IEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_IEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%],t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],  t1.V_conditional as [Var_Conditional_Missing], case when (t1.Enrollment > 0) then round(t1.V_conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Conditional_Missing%], t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%] from(select a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.NoImm) as conditional, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption,sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_conditional, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_IEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate from Assessments a ";

            // Added sqlString_admin by A.T. on 09/24/2014 to capture PBE subfields in the export. 
            string sqlString_admin = "select t1.SchCode as [School Code], isnull(S.SchName, 'ALL SCHOOLS') as [School], S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%],  ISNULL(t1.PME, 0) AS [TDap_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_PME%], t1.OtherIEP as [TDap_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_IEP%],t1.IndependentStudy as [TDap_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Independent Study%],t1.HomeBasedPrivate as [TDap_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Home Based Private%],  t1.Conditional as [TDap_Conditional], case when (t1.Enrollment > 0) then round(t1.Conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Conditional%], t1.TempMedExemption as [TDap_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_TME%], t1.EnrolledButNotAttending as [TDap_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Overdue%],t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%],  t1.V_PME AS [Var_PME], case when (t1.Enrollment > 0) then round(t1.V_PME*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], t1.V_MDMO_PermMedExmp as [Var_PMEpox], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%],t1.V_IEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_IEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%],t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],  t1.V_conditional as [Var_Conditional_Missing], case when (t1.Enrollment > 0) then round(t1.V_conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Conditional_Missing%], t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%]  from(select a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.NoImm) as conditional, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption,sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_conditional, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_IEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate from Assessments a ";

            string orderbyString = "group by a.SchCode with rollup) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'S' order by S.PhysCity, t1.SchCode asc";

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    } 
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA' " + orderbyString, con);
                    }
                    else {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=7thSummary.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnConditionalOverdue_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            //string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            //string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [TDaP_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_PME%], t1.OtherIEP as [TDaP_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_IEP%], t1.IndependentStudy as [TDaP_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Independent Study%],t1.HomeBasedPrivate as [TDaP_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Home Based Private%],t1.TempMedExemption as [TDaP_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_TME%], t1.EnrolledButNotAttending as [TDaP_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Overdue%], case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending + T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap_TME+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";
            string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [TDaP_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_PME%], t1.OtherIEP as [TDaP_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_IEP%], t1.IndependentStudy as [TDaP_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Independent Study%],t1.HomeBasedPrivate as [TDaP_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Home Based Private%],t1.TempMedExemption as [TDaP_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_TME%], t1.EnrolledButNotAttending as [TDaP_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending )*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap_TME+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate, sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";

            string orderbyString = "GROUP BY a.SchCode WITH ROLLUP) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'S' and (case when (t1.Enrollment > 0) then round((  t1.TempMedExemption + t1.EnrolledButNotAttending )*100 /cast(t1.Enrollment as float), 2) else 0 end) >= 10 ORDER BY S.PhysCity, t1.SchCode asc"; 
            


            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA'" + orderbyString, con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=Tdap_7thConditionalOverdueRPT.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnVaricellaConditionalOverdue_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            //string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            //string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%], ISNULL(t1.V_PME, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], ISNULL(t1.V_MDMO_PermMedExmp, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%], t1.V_OtherIEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%], t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],t1.V_ConditionalNotDue as [Var_ConditionalNotDue], case when (t1.Enrollment > 0) then round(t1.V_ConditionalNotDue*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_ConditionalNotDue%],t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%],case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending + T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME+Cond+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";
            string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%], ISNULL(t1.V_PME, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], ISNULL(t1.V_MDMO_PermMedExmp, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%], t1.V_OtherIEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%], t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],t1.V_ConditionalNotDue as [Var_ConditionalNotDue], case when (t1.Enrollment > 0) then round(t1.V_ConditionalNotDue*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_ConditionalNotDue%],t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%],case when (t1.Enrollment > 0) then round(( T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME+Cond+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate, sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";

            string orderbyString = "GROUP BY a.SchCode WITH ROLLUP) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'S' and (case when (t1.Enrollment > 0) then round((  T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end) >= 10 ORDER BY S.PhysCity, t1.SchCode asc";



            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA'" + orderbyString, con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'S' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=Var_7thConditionalOverdueRPT.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnAllList_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();

            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,   S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType,S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";

            //string sqlString = "SELECT C.CoCode as CoCode, C.CoName as CoName, ISNULL(D.DistCode, '') as DistCode, ISNULL(D.DistName, '') as DistName, S.SchCode as SchCode,   S.SchName as SchoolName, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP', ISNULL(t1.MedExmp, 0) as 'Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'IEP Services', ISNULL(t1.IndependentStudy, 0) as 'Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'Home Based Private School', ISNULL(t1.EnrolledButNotAttending, 0) as 'Overdue', ISNULL(t1.TempMedExemption, 0) as 'Temporary Medical Exemption', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as SchoolType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            //string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode, S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName, S.SchName as SchoolName, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP', ISNULL(t1.MedExmp, 0) as 'Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'IEP Services', ISNULL(t1.IndependentStudy, 0) as 'Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'Home Based Private School', ISNULL(t1.EnrolledButNotAttending, 0) as 'Overdue', ISNULL(t1.TempMedExemption, 0) as 'Temporary Medical Exemption', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as SchoolType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            //string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode, S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName, S.SchName as SchoolName, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.TotNo as Enrollment, t1.MedExmp as PME, t1.BeleExmp as PBE, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, s.SchType as FacilityType,  s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, s.Password as Password, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.SchCode = S.SchCode INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";


            // added sqlString_admin by A.T. on 09/23/2014 to show PBE sub fields for ONLY ADMIN user type
            //string sqlString_admin = "SELECT t1.CoCode as CoCode, t1.SchCode as FacilityCode, t1.CoName as CoName, t1.SchName as FacilityName, t1.PhysStreet as PhysStreet, t1.PhysCity as PhysCity, t1.PhysZip as PhysZip, t1.MailStreet as MailStreet, t1.MailCity as MailCity, t1.MailZip as MailZip, t1.SchPhone as SchPhone, t1.SchAdmin as SchAdmin, t1.SchEmail as SchEmail, t1.SuperintendentName as SuperintendentName, t1.SuperintendentEmail as SuperintendentEmail,  t1.FormPerson as StaffCompletingReport, t1.FormPhone as StaffCompletingReportPhone, t1.FormPhoneExt as StaffCompletingReportPhoneExt, t1.FormEmail as StaffCompletingReportEmail, t1.ContactPerson as DesignatedContact, t1.ContactPhone as DesignatedPhone, t1.ContactPhoneExt as DesignatedPhoneExt, t1.ContactEmail as DesignatedEmail, t1.KidCountUnderTwo as EnrollmentUnder2, t1.confirmedUnderTwo as ImmCheckedUnder2, t1.KidCountOverTwo as Enrollment, t1.Polio as Polio0, t1.Polio1 as Polio1, t1.Polio2 as Polio2, t1.Polio3 as Polio3, t1.Dtp as DTP0, t1.Dtp1 as DTP1, t1.Dtp2 as DTP2, t1.Dtp3 as DTP3, t1.Dtp4 as DTP4, t1.MMR as MMR0, t1.MMR1 as MMR1, t1.HIB as HIB0, t1.HIB1 as HIB1, t1.HepB as HepB0, t1.HepB1 as HepB1, t1.HepB2 as HepB2, t1.HepB3 as HepB3, t1.Varicella as Varicella0, t1.Varicella1 as Varicella1, t1.PME as PME, t1.PBE as PBE, t1.PBE_PreJanuaryExmpt as PBE_PreJanuaryExmpt, t1.PBE_HealthCareExmpt as PBE_HealthCareExmpt, t1.PBE_ReligiousExmpt as PBE_ReligiousExmpt, t1.FollowUp as FollowUp, t1.NoFollowUp as NoFollowUp, t1.SPACode as SPACode, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.SchType as FacilityType, t1.Status as Status, t1.AddSchoolDate as AddSchoolDate, t1.SubmitDate as SubmitDate, t1.ReviseDate as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.Password as Password, t1.skipSummary as skipSummary, t1.isReset as isReset, t1.SchoolYear as SchoolYear, t1.Memo as Memo from CC_Assessment t1";
            string sqlString_admin = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,  S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S on t1.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    // Updated sqlString by A.T. on 09/23/2014 to Show PBE sub fields to ONLY Admin. 
                    // command = New SqlCommand(sqlString & " where t1.SchoolYear = @SchoolYear order by t1.CoName Asc, t1.SchType Asc, t1.DistName Asc, t1.SchName Asc", conn)
                    command = new SqlCommand(sqlString_admin + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' order by C.CoName Asc, S.SchType Asc, S.SchName Asc", con);
                    break;

                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.CoCode = '01' and S.PhysCity <> 'BERKELEY' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E'  and C.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.CoCode = '01' and S.PhysCity = 'BERKELEY' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.CoCode = '19' and S.PhysCity = 'LONG BEACH' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.CoCode = '19' and S.PhysCity = 'PASADENA' order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.CoCode = @CoCode order by C.CoName Asc, S.SchType Asc,  S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and C.RegionCode = @RegionCode order by C.CoName Asc, S.SchType Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];



            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=8thGAll.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
                //Response.Write("<td>" + dv[row][column] + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + (dt.Rows[row][column]).ToString() + "</td>");

                            }
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();

        }

        protected void btnNotReported_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();


            //string sqlString = "SELECT S.CoCode as CoCode, C.CoName as CoName, S.DistCode as DistCode, D.DistName as DistName, t1.SchCode as SchCode, S.SchName as SchName, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  ISNULL(S.ContactPerson, '') as DesignatedContact, ISNULL(S.ContactPhone, '') as DesignatedPhone, ISNULL(S.ContactPhoneExt, '') as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, S.SchType as Type, S.isCharter as Charter, T1.StudentYesNo as KThisYear, t1.Reason as WhyNoK,  'Not Reported' as ReportingStatus, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,  S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";
            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '01' and S.PhysCity = 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity = 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = @CoCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "N");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=8thNotReported.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "DistCode":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnReported_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();


            //string sqlString = "SELECT S.CoCode as CoCode, C.CoName as CoName, S.DistCode as DistCode, t1.SchCode as SchCode, S.SchName as SchName,  t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  ISNULL(S.ContactPerson, '') as DesignatedContact, ISNULL(S.ContactPhone, '') as DesignatedPhone, ISNULL(S.ContactPhoneExt, '') as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, S.SchType as Type, S.isCharter as Charter, T1.StudentYesNo as KThisYear, t1.Reason as WhyNoK,  'Reported' as ReportingStatus, convert(varchar(10), t1.SubmitDate, 101) as SubmitDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode ";
            string sqlString = "SELECT C.CoCode as CoCode, ISNULL(D.DistCode, '') as DistCode,   S.SchCode as SchCode, C.CoName as CoName, ISNULL(D.DistName, '') as DistName,  s.SchType as SchoolType, S.SchName as SchoolName,  S.PhysStreet as PhysStreet, S.PhysCity as PhysCity, S.PhysZip as PhysZip, S.MailStreet as MailStreet, S.MailCity as MailCity, S.MailZip as MailZip, S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail,  t1.ReportedPerson as StaffCompletingReport, t1.ReportedPhone as StaffCompletingReportPhone, t1.ReportedPhoneExt as StaffCompletingReportPhoneExt, t1.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, ISNULL(t1.TotNo, 0) as Enrollment, ISNULL(t1.AllImm, 0) as 'TDaP',ISNULL(t1.MedExmp, 0) as 'TDaP_Permanent Medical Exemption', ISNULL(t1.IEPService, 0) as 'TDaP_IEP Services', ISNULL(t1.IndependentStudy, 0) as 'TDaP_Independent Study', ISNULL(t1.HomeBasedPrivate, 0) as 'TDaP_Home Based Private School', ISNULL(t1.TempMedExemption, 0) as 'TDaP_Temporary Medical Exemption', ISNULL(t1.EnrolledButNotAttending, 0) as 'TDaP_Overdue', ISNULL(t1.V_AllImm, 0) as 'Varicella', ISNULL(t1.V_MedExmp, 0) as 'Var_Permanent Medical Exemption', ISNULL(t1.V_MDMO_PermMedExmp, 0) as 'Var_PMEpox', ISNULL(t1.V_IEPService, 0) as 'Var_IEP Services', ISNULL(t1.V_IndependentStudy, 0) as 'Var_Independent Study', ISNULL(t1.V_HomeBasedPrivate, 0) as 'Var_Home Based Private School', ISNULL(t1.V_ConditionalNotDue, 0) as 'Var_Conditional Missing Doses', ISNULL(t1.V_TempMedExemption, 0) as 'Var_Temporary Medical Exemption', ISNULL(t1.V_EnrolledButNotAttending, 0) as 'Var_Overdue', CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as ReportingStatus, t1.StudentYesNo as 'SeventhThisYear', ISNULL(t1.reason, '') as Reason, ISNULL(S.isCharter, '') as Charter, ISNULL(t1.HomeSchl, '') as 'Home School', ISNULL(t1.VirtualSchl, '') as 'Virtual School', s.EnterDate as AddSchoolDate, ISNULL(t1.SubmitDate, '') as SubmitDate, ISNULL(t1.ReviseDate, '') as ReviseDate, t1.LhdReviseDate as LhdReviseDate, t1.SchoolYear as SchoolYear, t1.Memo as Memo from Assessments t1 INNER JOIN Schools S ON S.id = T1.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN DISTRICTS D ON D.distCode = S.distCode";


            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '01' and S.PhysCity = 'BERKELEY' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = '19' and S.PhysCity = 'PASADENA' and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear AND S.Cohort = 'E' and S.CoCode = @CoCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);

                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " where t1.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode and t1.isComplete = @isComplete order by C.CoName Asc, S.SchType Asc, D.DistName Asc, S.SchName Asc", con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=8thReported.xls");

            int row = 0;
            int column = 0;

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "SchCode":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "CoCode":
                            Response.Write("<td style=\"mso-number-format:00\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "DistCode":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "PhysZip":
                        case "MailZip":
                            Response.Write("<td style=\"mso-number-format:00000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "SubmitDate":
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        case "Password":
                            Response.Write("<td style=\"mso-number-format:0000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnSummaryReport_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            // ask Kristen which informaiton needs to be on the admin report

            // Added t2.submitDate by A.T. on 09/24/2014
            string sqlString = "select t1.SchCode as [School Code], isnull(S.SchName, 'ALL SCHOOLS') as [School], S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%],  ISNULL(t1.PME, 0) AS [TDap_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_PME%], t1.OtherIEP as [TDap_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_IEP%],t1.IndependentStudy as [TDap_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Independent Study%],t1.HomeBasedPrivate as [TDap_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Home Based Private%],  t1.Conditional as [TDap_Conditional], case when (t1.Enrollment > 0) then round(t1.Conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Conditional%], t1.TempMedExemption as [TDap_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_TME%], t1.EnrolledButNotAttending as [TDap_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Overdue%],t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%],  t1.V_PME AS [Var_PME], case when (t1.Enrollment > 0) then round(t1.V_PME*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], t1.V_MDMO_PermMedExmp as [Var_PMEpox], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%],t1.V_IEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_IEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%],t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],  t1.V_conditional as [Var_Conditional_Missing], case when (t1.Enrollment > 0) then round(t1.V_conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Conditional_Missing%], t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%] from(select a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.NoImm) as conditional, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption,sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_conditional, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_IEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate from Assessments a ";

            // Added sqlString_admin by A.T. on 09/24/2014 to capture PBE subfields in the export. 
            string sqlString_admin = "select t1.SchCode as [School Code], isnull(S.SchName, 'ALL SCHOOLS') as [School], S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%],  ISNULL(t1.PME, 0) AS [TDap_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_PME%], t1.OtherIEP as [TDap_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_IEP%],t1.IndependentStudy as [TDap_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Independent Study%],t1.HomeBasedPrivate as [TDap_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Home Based Private%],  t1.Conditional as [TDap_Conditional], case when (t1.Enrollment > 0) then round(t1.Conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Conditional%], t1.TempMedExemption as [TDap_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_TME%], t1.EnrolledButNotAttending as [TDap_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDap_Overdue%],t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%],  t1.V_PME AS [Var_PME], case when (t1.Enrollment > 0) then round(t1.V_PME*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], t1.V_MDMO_PermMedExmp as [Var_PMEpox], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%],t1.V_IEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_IEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%],t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],  t1.V_conditional as [Var_Conditional_Missing], case when (t1.Enrollment > 0) then round(t1.V_conditional*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Conditional_Missing%], t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%]  from(select a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.NoImm) as conditional, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption,sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_conditional, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_IEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate from Assessments a ";

            string orderbyString = "group by a.SchCode with rollup) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'E' order by S.PhysCity, t1.SchCode asc";

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA' " + orderbyString, con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=8thSummary.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnConditionalOverdue_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            //string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            //string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [TDaP_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_PME%], t1.OtherIEP as [TDaP_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_IEP%], t1.IndependentStudy as [TDaP_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Independent Study%],t1.HomeBasedPrivate as [TDaP_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Home Based Private%],t1.TempMedExemption as [TDaP_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_TME%], t1.EnrolledButNotAttending as [TDaP_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Overdue%], case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending + T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap_TME+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";
            string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [TDaP_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_PME%], t1.OtherIEP as [TDaP_IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_IEP%], t1.IndependentStudy as [TDaP_Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Independent Study%],t1.HomeBasedPrivate as [TDaP_Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Home Based Private%],t1.TempMedExemption as [TDaP_TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_TME%], t1.EnrolledButNotAttending as [TDaP_Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [TDaP_Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending )*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap_TME+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate, sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";

            string orderbyString = "GROUP BY a.SchCode WITH ROLLUP) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'E' and (case when (t1.Enrollment > 0) then round((  t1.TempMedExemption + t1.EnrolledButNotAttending )*100 /cast(t1.Enrollment as float), 2) else 0 end) >= 10 ORDER BY S.PhysCity, t1.SchCode asc"; 
            


            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA'" + orderbyString, con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=Tdap_8thConditionalOverdueRPT.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        protected void btnVaricellaConditionalOverdue_8th_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = default(SqlCommand);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            //string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SchoolYear = DownloadListYear.SelectedValue;
            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            //string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            //string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysCity as [City],t1.Enrollment, t2.submitDate as [SubmitDate], t1.AllImm as [Tdap], case when (t1.Enrollment > 0) then round(t1.AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Tdap%], ISNULL(t1.PME, 0) AS [PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [PME%], t1.OtherIEP as [IEP], case when (t1.Enrollment > 0) then round(t1.OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [IEP%], t1.IndependentStudy as [Independent Study], case when (t1.Enrollment > 0) then round(t1.IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Independent Study%], t1.HomeBasedPrivate as [Home Based Private], case when (t1.Enrollment > 0) then round(t1.HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Home Based Private%],  t1.TempMedExemption as [TME], case when (t1.Enrollment > 0) then round(t1.TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME%], t1.EnrolledButNotAttending as [Overdue], case when (t1.Enrollment > 0) then round(t1.EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Overdue%],  case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [TME+Overdue%]  FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption,sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate  FROM Assessments a ";
            string sqlString = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%], ISNULL(t1.V_PME, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], ISNULL(t1.V_MDMO_PermMedExmp, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%], t1.V_OtherIEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%], t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],t1.V_ConditionalNotDue as [Var_ConditionalNotDue], case when (t1.Enrollment > 0) then round(t1.V_ConditionalNotDue*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_ConditionalNotDue%],t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%],case when (t1.Enrollment > 0) then round(( t1.TempMedExemption + t1.EnrolledButNotAttending + T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME+Cond+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate,sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";
            string sqlString_admin = "SELECT C.CoName as CoName, t1.SchCode as [School Code],ISNULL(S.SchName, 'ALL SCHOOLS') as [School],S.SchType as [Public/Private], isNULL(D.DistName, '')  as [District Name], S.PhysStreet as PhysStreet, S.PhysCity as [City],S.SchPhone as SchPhone, S.SchAdmin as SchAdmin, S.SchEmail as SchEmail, S.SuperintendentName as SuperintendentName, S.SuperintendentEmail as SuperintendentEmail, t2.ReportedPerson as StaffCompletingReport, t2.ReportedPhone as StaffCompletingReportPhone, t2.ReportedPhoneExt as StaffCompletingReportPhoneExt, t2.ReportedEmail as StaffCompletingReportEmail, S.ContactPerson as DesignatedContact, S.ContactPhone as DesignatedPhone, S.ContactPhoneExt as DesignatedPhoneExt, S.ContactEmail as DesignatedEmail, t1.Enrollment, t1.V_AllImm as [Varicella], case when (t1.Enrollment > 0) then round(t1.V_AllImm*100 /cast(t1.Enrollment as float), 2) else 0 end as [Varicella%], ISNULL(t1.V_PME, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_PME, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PME%], ISNULL(t1.V_MDMO_PermMedExmp, 0) AS [Var_PME], case when (t1.Enrollment > 0) then round(ISNULL(t1.V_MDMO_PermMedExmp, 0)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_PMEpox%], t1.V_OtherIEP as [Var_IEP], case when (t1.Enrollment > 0) then round(t1.V_OtherIEP*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_IEP%], t1.V_IndependentStudy as [Var_Independent Study], case when (t1.Enrollment > 0) then round(t1.V_IndependentStudy*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Independent Study%],t1.V_HomeBasedPrivate as [Var_Home Based Private], case when (t1.Enrollment > 0) then round(t1.V_HomeBasedPrivate*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Home Based Private%],t1.V_ConditionalNotDue as [Var_ConditionalNotDue], case when (t1.Enrollment > 0) then round(t1.V_ConditionalNotDue*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_ConditionalNotDue%],t1.V_TempMedExemption as [Var_TME], case when (t1.Enrollment > 0) then round(t1.V_TempMedExemption*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME%], t1.V_EnrolledButNotAttending as [Var_Overdue], case when (t1.Enrollment > 0) then round(t1.V_EnrolledButNotAttending*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_Overdue%],case when (t1.Enrollment > 0) then round(( T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end as [Var_TME+Cond+Overdue%], S.isCharter as [Charter], t2.HomeSchl as [HomeSchool], t2.VirtualSchl as [VirtualSchool], t2.submitDate as [SubmitDate], t2.schoolyear as [SchoolYear ] FROM (SELECT a.SchCode, sum(a.TotNo) as Enrollment, sum(a.AllImm) as AllImm, sum(a.MedExmp) as PME, sum(a.TempMedExemption) as TempMedExemption, sum(a.EnrolledButNotAttending) as EnrolledButNotAttending, sum(a.IEPService) as OtherIEP, sum(a.IndependentStudy) as IndependentStudy, sum(a.HomeBasedPrivate) as HomeBasedPrivate, sum(ISNULL(a.V_AllImm, 0)) as V_AllImm, sum(ISNULL(a.V_MedExmp, 0)) as V_PME, sum(ISNULL(a.V_MDMO_PermMedExmp, 0)) as V_MDMO_PermMedExmp, sum(ISNULL(a.V_TempMedExemption, 0)) as V_TempMedExemption, sum(ISNULL(a.V_EnrolledButNotAttending, 0)) as V_EnrolledButNotAttending, sum(ISNULL(a.V_IEPService, 0)) as V_OtherIEP, sum(ISNULL(a.V_IndependentStudy, 0)) as V_IndependentStudy, sum(ISNULL(a.V_HomeBasedPrivate, 0)) as V_HomeBasedPrivate, sum(ISNULL(a.V_ConditionalNotDue, 0)) as V_ConditionalNotDue FROM Assessments a ";

            string orderbyString = "GROUP BY a.SchCode WITH ROLLUP) t1 left join Assessments t2 on t1.schcode = t2.schcode INNER JOIN Schools S on t2.id = S.id INNER JOIN Counties C ON C.CoCode = S.CoCode LEFT OUTER JOIN Districts D on D.distCode = S.DistCode WHERE t2.SchoolYear = @SchoolYear AND S.Cohort = 'E' and (case when (t1.Enrollment > 0) then round((  T1.V_TempMedExemption + T1.V_ConditionalNotDue + t1.V_EnrolledButNotAttending)*100 /cast(t1.Enrollment as float), 2) else 0 end) >= 10 ORDER BY S.PhysCity, t1.SchCode asc";



            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    command = new SqlCommand(sqlString_admin + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear " + orderbyString, con);

                    break;
                case "LHD":
                    if ((Session["AdminCoCode"].ToString() == "01"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity <> 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "19"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity <> 'LONG BEACH' and S.PhysCity <> 'PASADENA'" + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "59"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '01' and S.PhysCity = 'BERKELEY' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "60"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'LONG BEACH' " + orderbyString, con);
                    }
                    else if ((Session["AdminCoCode"].ToString() == "61"))
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = '19' and S.PhysCity = 'PASADENA'" + orderbyString, con);
                    }
                    else
                    {
                        command = new SqlCommand(sqlString + "INNER JOIN Schools S on S.id = a.id WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and S.CoCode = @CoCode " + orderbyString, con);
                        command.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                    }
                    break;
                case "FIELDREP":
                    command = new SqlCommand(sqlString + " INNER JOIN Schools S ON S.id = a.id INNER JOIN Counties C on S.CoCode = C.CoCode WHERE s.cohort = 'E' and a.isComplete = @isComplete and a.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode " + orderbyString, con);
                    command.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }

            command.Parameters.AddWithValue("@SchoolYear", SchoolYear);
            command.Parameters.AddWithValue("@isComplete", "Y");

            SqlDataAdapter da1 = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];


            string output = null;
            int row = 0;
            int column = 0;

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=Var_8thConditionalOverdueRPT.xls");

            Response.Write("<table>");
            Response.Write("<tr>");
            for (column = 0; column <= dt.Columns.Count - 1; column++)
            {
                Response.Write("<td>" + dt.Columns[column].ColumnName + "</td>");
            }
            Response.Write("</tr>");

            for (row = 0; row <= dt.Rows.Count - 1; row++)
            {
                Response.Write("<tr>");
                for (column = 0; column <= dt.Columns.Count - 1; column++)
                {
                    switch (dt.Columns[column].ColumnName)
                    {
                        case "School Code":
                            Response.Write("<td style=\"mso-number-format:0000000\\\">" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                        // Added SubmitDate by A.T. on 09/24/2014
                        case "SubmitDate":
                            if ((string.IsNullOrEmpty(dt.Rows[row][column].ToString())))
                            {
                                Response.Write("<td></td>");
                            }
                            else
                            {
                                Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            }
                            break;
                        default:
                            Response.Write("<td>" + dt.Rows[row][column].ToString() + "</td>");
                            break;
                    }
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }
    }
}