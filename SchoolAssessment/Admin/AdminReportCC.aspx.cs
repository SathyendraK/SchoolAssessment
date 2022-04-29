using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using System.Web;
using System.Web.UI.WebControls;

namespace SchoolAssessment.Admin
{
    public partial class AdminReportCC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            if ((Page.IsPostBack == true))
            {
                Session["CRptSchoolsFilterReport"] = cmbSubmissionStatus.SelectedValue;
                Session["CRptSchoolsFilterSchoolCode"] = txtSchoolCode.Text;
                Session["CRptSchoolsFilterSchoolName"] = txtSchoolName.Text;
                Session["CRptSchoolsFilterCounty"] = txtCounty.Text;
                Session["CRptSchoolsFilterCity"] = txtCity.Text;
                Session["CRptSchoolsFilterZip"] = txtZip.Text;
                Session["CRptSchoolsFilterSchoolYear"] = cmbSchoolYear.SelectedValue;
                Session["CRptSchoolsFilterDistrict"] = textDistrict.Text;

                //Bindgrid();
            }


            //if (Session["Sorted"] == null) { commented out on 10/19/2017
            if (Session["C_Sorted"] != "Y")
            { 
                Bindgrid();   

            }else
            {
                if (Session["CRptPageIndex"] != null)
                {
                    grdVSchools.PageIndex = Convert.ToInt32(Session["CRptPageIndex"]);
                }
                grdVSchools.DataSource = Session["CRptSchools"];// added 09/04/2016
                grdVSchools.DataBind();

            }


        }

        private void Bindgrid()
        {
            //DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            //dynamic sesAdminUserTYpe = Session["AdminUserType"];
            string sesAdminCoCode = Session["AdminCoCode"].ToString();
            string sql = "", sql1 = "", sql2 = "";
            SqlCommand cmd;

            //sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity <> 'BERKELEY' AND S.Cohort = 'C' order by SubmitDate desc";

            switch (sesAdminUserTYpe)
            {               
                case "ADMIN":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear order by SubmitDate desc";
                    sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id = S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";
                    break;
                case "LHD":
                    //sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity <> 'BERKELEY' AND S.Cohort = 'C' order by SubmitDate desc";

                    if ((sesAdminCoCode == "01"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity <> 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity <> 'BERKELEY' AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";
                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity = 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity = 'BERKELEY' AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";
                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity <> 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19' and PhysCity <> 'LONG BEACH' and PhysCity <> 'PASADENA' AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";
                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity = 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19' and PhysCity = 'LONG BEACH' AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";

                    }
                    else if ((sesAdminCoCode == "61"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity = 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19' and PhysCity = 'PASADENA' AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";

                    }
                    else {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = @CoCode order by SubmitDate desc";
                        sql1 = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear AND S.Cohort = 'C' and S.CoCode = @CoCode order by SubmitDate desc, S.SchName ASC";
                    }
                    break;
                case "FIELDREP":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment A left join Counties C on A.CoCode = C.CountyCode where A.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode order by SubmitDate desc";
                    sql2 = "SELECT A.Assmntid, A.SchCode, S.SchType,  D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.RegionCode = @RegionCode AND S.Cohort = 'C' order by SubmitDate desc, S.SchName ASC";
                    break;

            }


            if (((Session["CRptSchoolsFilterSchoolYear"] != null)))
            {
                SchoolYear = Session["CRptSchoolsFilterSchoolYear"].ToString(); 
            }

            //if (Session["CRptSchoolsFilterReport"] == null) { cmbSubmissionStatus.SelectedValue = ""; } else { cmbSubmissionStatus.SelectedValue = Session["CRptSchoolsFilterReport"].ToString(); }
            cmbSubmissionStatus.SelectedValue = (Session["CRptSchoolsFilterReport"] == null) ? cmbSubmissionStatus.SelectedValue = "" : cmbSubmissionStatus.SelectedValue = Session["CRptSchoolsFilterReport"].ToString();
            txtSchoolCode.Text = (Session["CRptSchoolsFilterSchoolCode"] == null) ? txtSchoolCode.Text = "" : txtSchoolCode.Text = Session["CRptSchoolsFilterSchoolCode"].ToString();
            txtSchoolName.Text = (Session["CRptSchoolsFilterSchoolName"] == null) ? txtCity.Text = "" : txtSchoolName.Text = Session["CRptSchoolsFilterSchoolName"].ToString();
            txtCounty.Text = (Session["CRptSchoolsFilterCounty"] == null) ? txtCounty.Text = "" : txtCounty.Text = Session["CRptSchoolsFilterCounty"].ToString();
            txtCity.Text = (Session["CRptSchoolsFilterCity"] == null) ? txtCity.Text = "" : txtCity.Text = Session["CRptSchoolsFilterCity"].ToString();
            txtZip.Text = (Session["CRptSchoolsFilterZip"] == null) ? txtZip.Text = "" : txtZip.Text = Session["CRptSchoolsFilterZip"].ToString();
            cmbSchoolYear.SelectedValue = (Session["CRptSchoolsFilterSchoolYear"] == null) ? cmbSchoolYear.SelectedValue = "" : cmbSchoolYear.SelectedValue = Session["CRptSchoolsFilterSchoolYear"].ToString();
            textDistrict.Text = (Session["CRptSchoolsFilterDistrict"] == null) ? textDistrict.Text = "" : textDistrict.Text = Session["CRptSchoolsFilterDistrict"].ToString();

            string filterNew = "";

            if ((cmbSubmissionStatus.SelectedValue != "All" & !string.IsNullOrEmpty(cmbSubmissionStatus.SelectedValue)))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                filterNew = filterNew + "isComplete ='" + cmbSubmissionStatus.SelectedValue + "'";
            }

            if (!string.IsNullOrEmpty(txtSchoolCode.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                // fix later 07/13/2016
                //filterNew = filterNew + "SchCode ='" + Strings.Right("0000000" + txtSchoolCode.Text, 7) + "'";
                string SchCode = txtSchoolCode.Text;
                filterNew = filterNew + "SchCode ='" + SchCode.Substring(SchCode.Length - 9) + "'";
            }

            if (!string.IsNullOrEmpty(txtSchoolName.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                // fix later 07/13/2016
                string SchName = txtSchoolName.Text;
                filterNew = filterNew + "SchName like '%" + SchName.Replace("'", "''") + "%'";
            }

            if (!string.IsNullOrEmpty(txtCounty.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                filterNew = filterNew + "CoName ='" + txtCounty.Text.ToString().ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(txtCity.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                filterNew = filterNew + "PhysCity = '" + txtCity.Text.ToString().ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(txtZip.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                filterNew = filterNew + "PhysZip = '" + txtZip.Text.ToString().ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(textDistrict.Text))
            {
                if (!string.IsNullOrEmpty(filterNew))
                {
                    filterNew = filterNew + " and ";
                }
                filterNew = filterNew + "DistName like '%" + textDistrict.Text.ToString().ToUpper() + "%'";
            }


            try
            {
                con.Open();

                if (sql == "")
                {

                    if (sql2 != "")
                    {
                        cmd = new SqlCommand(sql2, con);
                        cmd.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                        cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                        cmd.Parameters.AddWithValue("@CoCode", sesAdminCoCode);
                    }
                    else
                    {
                        cmd = new SqlCommand(sql1, con);
                        cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
                        cmd.Parameters.AddWithValue("@CoCode", sesAdminCoCode);

                    }

                }
                else
                {
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);

                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);


                /* this one works, but temporary commented out on 07/21/2016
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSchools.DataSource = ds;
                    grdSchools.DataBind();

                }
                */

                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = filterNew;
                Session["CRptSchools"] = dv;

                grdVSchools.DataSource = dv;
                grdVSchools.DataBind();



            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Session["C_Sorted"] = null;
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

        }

        private void sorter(DataView source)
        {
            DataView dv = source;

            dv.Sort = Session["CRptSchoolsSort"].ToString();
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            Session["CRptSchools"] = dv;
            //grdSchools.DataSource = dv;
            //grdSchools.DataBind();
        }

        protected void grdVSchools_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //DataView dv = Session["CRptSchools"] as DataView;
            DataView dv = Session["CRptSchoolsSort"] as DataView;


            //Int32 index = Convert.ToInt32(e.CommandArgument);
            int index = 0;

            if (e.CommandName == "Login")
            {
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["CC_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                
                Response.Redirect("../CC/LoginConfirmed.aspx", true);

                //Response.Redirect("../CC/LoginConfirmed.aspx?id=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "Edit")
            {
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["CC_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                Response.Redirect("AdminEditSchoolCC.aspx?id=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "SummaryRpt")
            {
                
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["CC_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                Response.Redirect("../CC/ViewAndPrint.aspx?id=" + e.CommandArgument, true);
                
                
            }
        }

        protected void grdVSchools_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string isComplete_Val = grdVSchools.SelectedRow.Cells[13].Text;
            LinkButton lbnSummaryRpt = grdVSchools.FindControl("SummaryRpt") as LinkButton;
            isComplete_Val = "Not Reported";
            if (isComplete_Val == "Not Reported")
            {
                lbnSummaryRpt.Visible = false;
            }
            */

        }

        protected void grdVSchools_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdVSchools.PageIndex = e.NewPageIndex;
            Session["CRptPageIndex"] = e.NewPageIndex;
            grdVSchools.DataSource = Session["CRptSchools"];// added 10/18/2017
            grdVSchools.DataBind();
        }

        private string ConvertSortDirection(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        protected void grdVSchools_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataView dv = Session["CRptSchools"] as DataView;

            if (dv.Sort == e.SortExpression)
            {
                dv.Sort = e.SortExpression + " DESC";
            }
            else if ((dv.Sort) == (e.SortExpression + " DESC"))
            {
                dv.Sort = e.SortExpression;
            }
            else {
                dv.Sort = e.SortExpression;
            }

            //Session["CRptSchoolsSort"] = dv.Sort; commented out on 10/18/2017

            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            //Session["CRptSchools"] = dv; commented out on 10/18/2017
            grdVSchools.DataSource = dv;
            grdVSchools.DataBind();

            Session["C_Sorted"] = "Y";

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            Session["CRptSchoolsFilterReport"] = "All";
            Session["CRptSchoolsFilterSchoolCode"] = "";
            Session["CRptSchoolsFilterSchoolName"] = "";
            Session["CRptSchoolsFilterCounty"] = "";
            Session["CRptSchoolsFilterCity"] = "";
            Session["CRptSchoolsFilterZip"] = "";
            Session["CRptSchoolsFilterSchoolYear"] = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            Session["CRptSchoolsCurrentPageIndex"] = 0;
            Session["CRptSchoolsFilterDistrict"] = "";
            Response.Redirect("AdminReportCC.aspx", true);

        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {

            //DataView dt = default(DataView);
            DataView dv = Session["CRptSchools"] as DataView;
            //dt = Session["CRptSchools"];

            string strName = "ChildcareList.xls";

            Response.Clear();
            Response.ContentType = "text/csv";
            //Response.AddHeader("Content-Disposition", "attachment; filename=Output.csv")
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strName);

            int row = 0;

            //Response.Write("""") : Response.Write("Code") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("Type") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("District") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("Name") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("County") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("PhysStreet") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("PhysCity") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("PhysZip") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("Phone") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("SubmitDate") : Response.Write("""") : Response.Write(",")
            //Response.Write("""") : Response.Write("isComplete") : Response.Write("""")
            //Response.Write(Environment.NewLine)

            //For row = 0 To dt.Count - 1
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("SchCode")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("SchType")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("DistName")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("SchName")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("CoName")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("PhysStreet")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("PhysCity")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("PhysZip")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("SchPhone")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("SubmitDate")) : Response.Write("""") : Response.Write(",")
            //  Response.Write("""") : Response.Write(dt.Item(row).Item("isComplete")) : Response.Write("""")
            //  Response.Write(Environment.NewLine)
            //Next

            Response.Write("<table>");
            Response.Write("<td>");
            Response.Write("Code");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("Type");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("District");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("Name");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("County");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("PhysStreet");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("PhysCity");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("PhysZip");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("Phone");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("SubmitDate");
            Response.Write("</td>");
            Response.Write("<td>");
            Response.Write("isComplete");
            Response.Write("</td>");
            Response.Write("</tr>");

            for (row = 0; row <= dv.Count - 1; row++)
            {
                Response.Write("<tr>");
                Response.Write("<td style=\"mso-number-format:0000000\\;\">");
                Response.Write(dv[row]["SchCode"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["SchType"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["DistName"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["SchName"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["CoName"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["PhysStreet"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["PhysCity"]);
                Response.Write("</td>");
                Response.Write("<td style=\"mso-number-format:00000\\;\">");
                Response.Write(dv[row]["PhysZip"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["SchPhone"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["SubmitDate"]);
                Response.Write("</td>");
                Response.Write("<td>");
                Response.Write(dv[row]["isComplete"]);
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            Response.End();

        }

        protected void grdVSchools_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            string SelectedYear = SchoolYear;

            if (((Session["CRptSchoolsFilterSchoolYear"] != null)))
            {
                SelectedYear = Session["CRptSchoolsFilterSchoolYear"].ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Show link to Original Report only for those reported. 
                if (e.Row.Cells[14].Text == "Not Reported")
                {
                    LinkButton SummaryRpt = e.Row.FindControl("SummaryRpt") as LinkButton;
                    SummaryRpt.Enabled = false;

                }
                else
                {
                    LinkButton SummaryRpt = e.Row.FindControl("SummaryRpt") as LinkButton;
                    SummaryRpt.Enabled = true;
                    SummaryRpt.Text = "RPT";
                }

                // Login link shows only schools with current year.  No link will be displayed for schools reported previous years.
                if (SelectedYear != SchoolYear)
                {
                    LinkButton LoginBtn = e.Row.FindControl("LoginBtn") as LinkButton;
                    LoginBtn.Enabled = false;
                }
                else
                {
                    /*
                     * Commented out on 03/04/2021 per Kristen requesting LHD and Fieldrep to be able to use this funciton.
                     * Comment out Admin checiing on 10/13/2021
                     * */
                    if (Session["AdminUserType"].ToString() == "ADMIN") // Added by AT on 01/04/2017 so that only Admin can see the Login link.  Commented out on 09/07/2017 so all user type can see login button. 
                    { 
                        LinkButton LoginBtn = e.Row.FindControl("LoginBtn") as LinkButton;
                        LoginBtn.Enabled = true;
                        LoginBtn.Text = "Login";
                    }
                    

                    // Kristen wants all user types to not see the Login link -- 06/03/2021
                    /*
                    LinkButton LoginBtn = e.Row.FindControl("LoginBtn") as LinkButton;
                    LoginBtn.Enabled = false;
                    */
                }
            }
            
        }


    }
}