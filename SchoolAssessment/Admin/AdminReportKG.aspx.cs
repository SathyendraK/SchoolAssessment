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

/*
 *  05/08/2019 Ayumi Taniguchi Wildcard search was added for District
 *  
 * */


namespace SchoolAssessment.Admin
{
    public partial class AdminReportKG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

            if ((Page.IsPostBack == true))
            {
                Session["kRptSchoolsFilterReport"] = cmbSubmissionStatus.SelectedValue;
                Session["kRptSchoolsFilterSchoolCode"] = txtSchoolCode.Text;
                Session["kRptSchoolsFilterSchoolName"] = txtSchoolName.Text;
                Session["kRptSchoolsFilterCounty"] = txtCounty.Text;
                Session["kRptSchoolsFilterCity"] = txtCity.Text;
                Session["kRptSchoolsFilterZip"] = txtZip.Text;
                Session["kRptSchoolsFilterSchoolYear"] = cmbSchoolYear.SelectedValue;
                Session["kRptSchoolsFilterDistrict"] = textDistrict.Text;
            }


            //if (Session["Sorted"] == null) commented out on 10/19/2017
            if (Session["K_Sorted"] != "Y")
            {
                Bindgrid();
            }
            else
            {
                if (Session["KRptPageIndex"] != null)
                {
                    grdVSchools.PageIndex = Convert.ToInt32(Session["KRptPageIndex"]);
                }
                grdVSchools.DataSource = Session["kRptSchools"];// added 09/04/2016
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
            string sesAdminCoCode = Session["AdminCoCode"].ToString();
            string sql = "", sql1 = "", sql2 = "";
            SqlCommand cmd;

            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear order by SubmitDate desc";
                    sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear AND S.Cohort = 'K' order by SubmitDate desc";
                    break;
                case "LHD":
                    if ((sesAdminCoCode == "01"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity <> 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity <> 'BERKELEY' AND S.Cohort = 'K' order by SubmitDate desc";
                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity = 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '01' and PhysCity = 'BERKELEY' AND S.Cohort = 'K' order by SubmitDate desc";
                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity <> 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19' and PhysCity <> 'LONG BEACH' and PhysCity <> 'PASADENA' AND S.Cohort = 'K' order by SubmitDate desc";
                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity = 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19' and PhysCity = 'LONG BEACH' AND S.Cohort = 'K' order by SubmitDate desc";

                    }
                    else if ((sesAdminCoCode == "61"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity = 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.CoCode = '19'  and PhysCity = 'PASADENA' AND S.Cohort = 'K' order by SubmitDate desc";

                    }
                    else {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = @CoCode order by SubmitDate desc";
                        //sql1 = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE A.SchoolYear = @SchoolYear and S.CoCode = @CoCode AND S.Cohort = 'K' order by SubmitDate desc";
                        sql1 = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear AND S.Cohort = 'K' and S.CoCode = @CoCode order by SubmitDate desc";

                    }
                    break;
                case "FIELDREP":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment A left join Counties C on A.CoCode = C.CountyCode where A.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode order by SubmitDate desc";
                    sql2 = "SELECT A.Assmntid, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.id= S.id LEFT OUTER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = S.CoCode WHERE SchoolYear = @SchoolYear and C.RegionCode = @RegionCode AND S.Cohort = 'K' order by SubmitDate desc";
                    break;
            }


            if (((Session["kRptSchoolsFilterSchoolYear"] != null)))
            {
                SchoolYear = Session["kRptSchoolsFilterSchoolYear"].ToString();
            }

            //if (Session["kRptSchoolsFilterReport"] == null) { cmbSubmissionStatus.SelectedValue = ""; } else { cmbSubmissionStatus.SelectedValue = Session["kRptSchoolsFilterReport"].ToString(); }
            cmbSubmissionStatus.SelectedValue = (Session["kRptSchoolsFilterReport"] == null) ? cmbSubmissionStatus.SelectedValue = "" : cmbSubmissionStatus.SelectedValue = Session["kRptSchoolsFilterReport"].ToString();
            txtSchoolCode.Text = (Session["kRptSchoolsFilterSchoolCode"] == null) ? txtSchoolCode.Text = "" : txtSchoolCode.Text = Session["kRptSchoolsFilterSchoolCode"].ToString();
            txtSchoolName.Text = (Session["kRptSchoolsFilterSchoolName"] == null) ? txtCity.Text = "" : txtSchoolName.Text = Session["kRptSchoolsFilterSchoolName"].ToString();
            txtCounty.Text = (Session["kRptSchoolsFilterCounty"] == null) ? txtCounty.Text = "" : txtCounty.Text = Session["kRptSchoolsFilterCounty"].ToString();
            txtCity.Text = (Session["kRptSchoolsFilterCity"] == null) ? txtCity.Text = "" : txtCity.Text = Session["kRptSchoolsFilterCity"].ToString();
            txtZip.Text = (Session["kRptSchoolsFilterZip"] == null) ? txtZip.Text = "" : txtZip.Text = Session["kRptSchoolsFilterZip"].ToString();
            textDistrict.Text = (Session["kRptSchoolsFilterDistrict"] == null) ? textDistrict.Text = "" : textDistrict.Text = Session["kRptSchoolsFilterDistrict"].ToString();
            cmbSchoolYear.SelectedValue = (Session["kRptSchoolsFilterSchoolYear"] == null) ? cmbSchoolYear.SelectedValue = "" : cmbSchoolYear.SelectedValue = Session["kRptSchoolsFilterSchoolYear"].ToString();
            Session["AdminSelectedYear"] = cmbSchoolYear.SelectedValue;

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
                filterNew = filterNew + "SchCode ='" + SchCode.Substring(SchCode.Length - 7) + "'";
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
                //filterNew = filterNew + "DistName = '" + textDistrict.Text.ToString().ToUpper() + "'";
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
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = filterNew;
                Session["kRptSchools"] = dv;

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
            Session["K_Sorted"] = null;

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

        }

        private void sorter(DataView source)
        {
            DataView dv = source;

            dv.Sort = Session["kRptSchoolsSort"].ToString();
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            Session["kRptSchools"] = dv;
            //grdSchools.DataSource = dv;
            //grdSchools.DataBind();
        }

        protected void grdVSchools_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataView dv = Session["kRptSchools"] as DataView;
            //Int32 index = Convert.ToInt32(e.CommandArgument);
            int index = 0;

            if (e.CommandName == "Login")
            {
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["K_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                Set1stGradeAsmntID(Convert.ToInt32(commandSource.CommandArgument));
                Response.Redirect("../KG/LoginConfirmed.aspx", true);
            }
            else if (e.CommandName == "Edit")
            {
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["K_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                Set1stGradeAsmntID(Convert.ToInt32(commandSource.CommandArgument));
                Response.Redirect("AdminEditSchoolKG.aspx?id=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "SummaryRpt")
            {
                //GridView selectedGV = (GridView)e.CommandSource;
                //GridViewRow row = grdVSchools.Rows[index];

                //LinkButton lbnSummaryRpt = grdVSchools.FindControl("SummaryRpt") as LinkButton;
                //lbnSummaryRpt.Visible = false;

                /*
                if (row.Cells[13].Text == "Not Reported")
                {
                    //row.Cells[13].Visible = false;
                } else
                {
                    //row.Cells[13].Visible = false;
                   
                    
                                    LinkButton commandSource = e.CommandSource as LinkButton;
                                    string commandText = commandSource.Text;
                                    string commandName = commandSource.CommandName;
                                    Session["K_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                                    Response.Redirect("../KG/ViewAndPrint.aspx?id=" + e.CommandArgument, true);
                                    
                }
            */

                
                LinkButton commandSource = e.CommandSource as LinkButton;
                string commandText = commandSource.Text;
                string commandName = commandSource.CommandName;
                Session["K_Assessment_id"] = Convert.ToInt32(commandSource.CommandArgument);
                Set1stGradeAsmntID(Convert.ToInt32(commandSource.CommandArgument));
                Response.Redirect("../KG/ViewAndPrint.aspx?id=" + e.CommandArgument, true);
                


            }
        }

        private void Set1stGradeAsmntID(int FirstGradeAsmntID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);
            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];

            DataSet ds = new DataSet();
            string sql = "select TOP 1 A.Assmntid From Schools S INNER JOIN Assessments A ON S.id = A.id where A.SchoolYear = " + SchoolYear + " and S.schcode in (SELECT schcode from Assessments where Assmntid = " + FirstGradeAsmntID + ") and S.cohort = 'F'";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                if ((sdr.HasRows == true))
                {
                    sdr.Read();
                    Session["F_Assessment_id"] = sdr[0];
                        
                }
                else
                {
                    //pwdstatus = false;
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
            Session["KRptPageIndex"] = e.NewPageIndex;
            grdVSchools.DataSource = Session["kRptSchools"];// added 09/04/2016
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
            DataView dv = Session["kRptSchools"] as DataView;

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

            // trying these
            //grdVSchools.DataSource = dv;
            //grdVSchools.DataBind();



            //Session["kRptSchoolsSort"] = dv.Sort;
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            //Session["kRptSchools"] = dv;
            grdVSchools.DataSource = dv;
            grdVSchools.DataBind();

            Session["K_Sorted"] = "Y";

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }
            Session["kRptSchoolsFilterReport"] = "All";
            Session["kRptSchoolsFilterSchoolCode"] = "";
            Session["kRptSchoolsFilterSchoolName"] = "";
            Session["kRptSchoolsFilterCounty"] = "";
            Session["kRptSchoolsFilterCity"] = "";
            Session["kRptSchoolsFilterZip"] = "";
            Session["kRptSchoolsFilterDistrict"] = "";
            Session["kRptSchoolsFilterSchoolYear"] = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];

            Session["kRptSchoolsCurrentPageIndex"] = 0;
            Response.Redirect("AdminReportKG.aspx", true);

        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {

            //DataView dt = default(DataView);
            DataView dv = Session["kRptSchools"] as DataView;
            //dt = Session["kRptSchools"];

            string strName = "KindergartenList.xls";

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

            if (((Session["kRptSchoolsFilterSchoolYear"] != null)))
            {
                SchoolYear = Session["kRptSchoolsFilterSchoolYear"].ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Show link to Original Report only for those reported. 
                if (e.Row.Cells[15].Text == "Not Reported")
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
                     *  Commented out on 03/04/2021 per Kristen requesting LHD and Fieldrep to be able to use this funciton.
                     *  Comment out Admin checiing on 10/13/2021
                     */
                    if (Session["AdminUserType"].ToString() == "ADMIN") // Added by AT on 01/04/2017 so that only Admin can see the Login link. //Commented out on 09/07/2017 so all user type can see login button.  
                    {
                        LinkButton LoginBtn = e.Row.FindControl("LoginBtn") as LinkButton;
                        LoginBtn.Enabled = true;
                        LoginBtn.Text = "Login";
                    }
                    

                    // Kristen wants all user types to not see the Login link -- 05/17/2021
                    /*
                    LinkButton LoginBtn = e.Row.FindControl("LoginBtn") as LinkButton;
                    LoginBtn.Enabled = false;
                    */
                }
            }

        }

    }

}