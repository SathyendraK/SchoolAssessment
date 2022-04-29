
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


namespace SchoolAssessment.Admin
{
    public partial class AdminMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Start allowing LHD to add school on 06/08/2015 by AT
            //If (Session["AdminUserType"] <> "ADMIN") Then
            if ((Session["AdminUserType"].ToString() == "FIELDREP"))
            {
                lnkAdminAddSchoolCC.Visible = false;
                lnkAdminAddSchoolKG.Visible = false;
                lnkAdminAddSchoolMH.Visible = false;
                panelAdmin.Visible = false;
                lnkAdminAddDistricts.Visible = false;
                lnkAdminEditUsers.Visible = false; // Added 01/13/2021

                //lnkAdminReportCC.Visible = False
                //lnkAdminSummaryCC.Visible = False
                //LHD can add new school, but cannot edit the admin user information added by AT on 07/31/2015
            }
            else if ((Session["AdminUserType"].ToString()  == "LHD"))
            {
                //panelAdmin.Visible = false;  commented out on 09/27/2017
                //lnkAdminEditUsers.Visible = false;
                lnkAdminEditUsers.Visible = false;
                lnkAdminReactivateSchool.Visible = true;
                lnkAdminAddDistricts.Visible = false;
                lnkAdminEditUsers.Visible = false; // Added 01/13/2021
            }
        }
    }
}