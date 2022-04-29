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
using System.Web;
using System.Web.UI.WebControls;



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

            
            
            //SqlCommand command = default(SqlCommand);
            //string connectString = System.Configuration.ConfigurationManager.AppSettings("kSchoolDBConnectString");
            //string curyear = System.Configuration.ConfigurationManager.AppSettings("SchoolYear");
            //SqlConnection conn = new SqlConnection(connectString);

            if ((Page.IsPostBack == true))
            {
                Session["kRptSchoolsFilterReport"] = cmbSubmissionStatus.SelectedValue;
                Session["kRptSchoolsFilterSchoolCode"] = txtSchoolCode.Text;
                Session["kRptSchoolsFilterSchoolName"] = txtSchoolName.Text;
                Session["kRptSchoolsFilterCounty"] = txtCounty.Text;
                Session["kRptSchoolsFilterCity"] = txtCity.Text;
                Session["kRptSchoolsFilterZip"] = txtZip.Text;
                Session["kRptSchoolsFilterSchoolYear"] = cmbSchoolYear.SelectedValue;
            }


            //DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string SchoolYear = System.Configuration.ConfigurationManager.AppSettings["SchoolYear"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAConnection"].ConnectionString);

            //string sql = "";


            string sesAdminUserTYpe = Session["AdminUserType"].ToString();
            string sesAdminCoCode = Session["AdminCoCode"].ToString();

            //switch (Session["AdminUserType"])
            /* test switch later 7/14/2016
            switch (sesAdminUserTYpe)
            {
                case "ADMIN":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear order by SubmitDate desc";
                    sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";
                    break;
                case "LHD":
                    if ((sesAdminCoCode == "01"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity <> 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";

                    }
                    else if ((sesAdminCoCode == "59"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '01' and PhysCity = 'BERKELEY' order by SubmitDate desc";
                        sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";

                    }
                    else if ((sesAdminCoCode == "19"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity <> 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";

                    }
                    else if ((sesAdminCoCode == "60"))
                    {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = '19' and PhysCity = 'LONG BEACH' order by SubmitDate desc";
                        sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";
                    }
                    else {
                        //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment where SchoolYear = @SchoolYear and CoCode = @CoCode order by SubmitDate desc";
                        sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";
                        cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@CoCode", Session["AdminCoCode"]);
                       
                    }
                    break;
                case "FIELDREP":
                    //sql = "SELECT id, SchCode, SchType, DistName, SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete from K_Assessment A left join Counties C on A.CoCode = C.CountyCode where A.SchoolYear = @SchoolYear and C.RegionCode = @RegionCode order by SubmitDate desc";
                    sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@RegionCode", Session["AdminRegionCode"]);
                    break;
            }
            */

                        if (((Session["kRptSchoolsFilterSchoolYear"] != null)))
                        {
                            SchoolYear = Session["kRptSchoolsFilterSchoolYear"].ToString();
                        }
                        //cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);


                        // Need to figure out this one 07/13/2016
                        /*
                        var _with1 = adapter;
                        _with1.SelectCommand = cmd;
                        _with1.Fill(ds);
                        */

            //if (Session["kRptSchoolsFilterReport"] == null) { cmbSubmissionStatus.SelectedValue = ""; } else { cmbSubmissionStatus.SelectedValue = Session["kRptSchoolsFilterReport"].ToString(); }
            cmbSubmissionStatus.SelectedValue = (Session["kRptSchoolsFilterReport"] == null) ? cmbSubmissionStatus.SelectedValue = "" : cmbSubmissionStatus.SelectedValue = Session["kRptSchoolsFilterReport"].ToString();
            txtSchoolCode.Text = (Session["kRptSchoolsFilterSchoolCode"] == null) ? txtSchoolCode.Text = "" : txtSchoolCode.Text = Session["kRptSchoolsFilterSchoolCode"].ToString();
            txtSchoolName.Text = (Session["kRptSchoolsFilterSchoolName"] == null) ? txtCity.Text = "" : txtSchoolName.Text = Session["kRptSchoolsFilterSchoolName"].ToString();
            txtCounty.Text = (Session["kRptSchoolsFilterCounty"] == null) ? txtCounty.Text = "" : txtCounty.Text = Session["kRptSchoolsFilterCounty"].ToString();
            txtCity.Text = (Session["kRptSchoolsFilterCity"] == null) ? txtCity.Text = "" : txtCity.Text = Session["kRptSchoolsFilterCity"].ToString();
            txtZip.Text = (Session["kRptSchoolsFilterZip"] == null) ? txtZip.Text = "" : txtZip.Text = Session["kRptSchoolsFilterZip"].ToString();
            cmbSchoolYear.SelectedValue = (Session["kRptSchoolsFilterSchoolYear"] == null) ? cmbSchoolYear.SelectedValue = "" : cmbSchoolYear.SelectedValue = Session["kRptSchoolsFilterSchoolYear"].ToString();


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
                filterNew = filterNew + "SchCode ='" + SchCode.Substring(SchCode.Length-7) + "'";
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

            try
            {
                con.Open();

                string sql = "SELECT S.id, A.SchCode, S.SchType, D.DistName, S.SchName, CoName, PhysStreet, PhysCity, substring(PhysZip, 1, 5) as PhysZip, SchPhone, REPLACE(Memo,'''','’') as Memo, CONVERT(char(10), SubmitDate, 126) as SubmitDate, CASE WHEN isComplete = 'Y' THEN 'Reported' ELSE 'Not Reported' END as isComplete  FROM Assessments A INNER JOIN Schools S on A.SchCode = S.SchCode INNER JOIN Districts D on S.DistCode = D.DistCode INNER JOIN Counties C ON C.CoCode = D.CoCode WHERE SchoolYear = @SchoolYear order by SubmitDate desc";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@SchoolYear", SchoolYear);
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

                //cmd.ExecuteNonQuery();
                //da.SelectCommand = cmd;

                
                
                
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = filterNew;
               
                
                /*
                if (((Session["kRptSchoolsCurrentPageIndex"] != null)))
                {
                    grdSchools.CurrentPageIndex = (Int32)Session["kRptSchoolsCurrentPageIndex"];
                }
                if (grdSchools.CurrentPageIndex > (dv.Count / grdSchools.PageSize))
                {
                    grdSchools.CurrentPageIndex = 0;
                }
                */

                /* temporary disabling these 08/15/2016 */
                grdSchools.DataSource = dv;
                grdSchools.DataBind();

                grdVSchools.DataSource = dv;
                grdVSchools.DataBind();



            }
            catch (Exception ex)
            {

            }
            finally {
                con.Close();
            }
                          
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (((Session["AdminUserType"] == null)))
            {
                Response.Redirect("AdminLogin.aspx?reason=TimedOut", true);
            }

        }

        private void grdSchools_PageIndexChanged(System.Object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            grdSchools.DataSource = Session["kRptSchools"];
            grdSchools.CurrentPageIndex = e.NewPageIndex;
            Session["kRptSchoolsCurrentPageIndex"] = e.NewPageIndex;
            sorter(grdSchools.DataSource as DataView) ;
        }


        private void grdSchools_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            // Create a DataView from the DataTable.
            DataView dv = Session["kRptSchools"] as DataView;
            dv.Sort = Session["kRptSchoolsSort"].ToString();
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
            Session["kRptSchoolsSort"] = dv.Sort;
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            Session["kRptSchools"] = dv;
            grdSchools.DataSource = dv;
            grdSchools.DataBind();
        }

        private void sorter(DataView source)
        {
            DataView dv = source;
            
            dv.Sort = Session["kRptSchoolsSort"].ToString();
            // Rebind the data source and specify that 
            // it should be sorted by the field specified 
            // in the SortExpression property.
            Session["kRptSchools"] = dv;
            grdSchools.DataSource = dv;
            grdSchools.DataBind();
        }

        private void grdSchools_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Login")
            {
                Session["K_Assessment_id"] = e.CommandArgument;
                Response.Redirect("../KG/LoginConfirmed.aspx", true);
            }
            else if (e.CommandName == "PDF")
            {
                //CreatePDF(e.CommandArgument);
            }
            else if (e.CommandName == "Edit")
            {
                //Response.Redirect("AdminEditSchoolKG.aspx?id=" + e.CommandArgument, true);
                Response.Redirect("AdminLogout.aspx?id=" + e.CommandArgument, true);
            }
        }


        private void grdVSchools_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Login")
            {
                Session["K_Assessment_id"] = e.CommandArgument;
                Response.Redirect("../KG/LoginConfirmed.aspx", true);
            }
            else if (e.CommandName == "PDF")
            {
                //CreatePDF(e.CommandArgument);
            }
            else if (e.CommandName == "Edit")
            {
                //Response.Redirect("AdminEditSchoolKG.aspx?id=" + e.CommandArgument, true);
                Response.Redirect("AdminLogout.aspx?id=" + e.CommandArgument, true);
            }
        }





        protected void grdVSchools_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            //GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
            //Label lblID = (Label)clickedRow.FindControl("id");


            if (e.CommandName == "Login")
            {
                //GridViewRow row = grdVSchools.SelectedRow;
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow selectedRow = grdVSchools.Rows[index];

                Session["K_Assessment_id"] = "10"; //((GridViewRow(((LinkButton)e.CommandSource).NamingContainer)).Cells[2].Text;
                //Session["K_Assessment_id"] = grdVSchools.SelectedRow.Cells[0].Text;

                Response.Redirect("../KG/LoginConfirmed.aspx", true);
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect("AdminLogout.aspx?id=" + e.CommandArgument, true);
            }
        }
    }

}