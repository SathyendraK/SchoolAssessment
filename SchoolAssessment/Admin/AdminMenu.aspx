<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.aspx.cs" Inherits="SchoolAssessment.Admin.AdminMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="refresh" content="120;url=AdminMenu.aspx" />
  <title>Admin Menu</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body >
    <form id="form1" runat="server">
   <!-- <body bgcolor="#b4dde1">-->
 
  <div id="nav">
    <p>
      <img src="Images/aheader.png" alt="Immunization Assessment Admin" style="padding-bottom: 0.5em" /></p>
    <asp:panel id="panelAdmin" runat="server">
      <p>
        <img src="Images/admin.png" alt="Admin"/></p>
      <ul>
        <asp:hyperlink runat="server" id="lnkAdminEditUsers" navigateurl="AdminEditUsers.aspx" target="right"><li>Edit Admin Users</li></asp:hyperlink>
        <asp:hyperlink runat="server" id="lnkAdminReactivateSchool" navigateurl="AdminReactivateSchool.aspx" target="right"><li>Reactivate Schools</li></asp:hyperlink>
        <asp:HyperLink ID="linkAdminAddSchool" navigateurl="AdminSearchSchool.aspx" target="right" runat="server"><li>Add School</li></asp:HyperLink>
        <asp:HyperLink ID="lnkAdminAddDistricts" navigateurl="AdminAddDistricts.aspx" target="right" runat="server">Add School District</asp:HyperLink>
      </ul>
        
    </asp:panel>
    <p>
      <img src="Images/childcare.png" alt="Child Care" /></p>
    <ul>      
      <asp:hyperlink runat="server" id="lnkAdminReportCC" navigateurl="AdminReportCC.aspx" target="right"><li>List/Edit Facilities</li></asp:hyperlink>
      <asp:hyperlink runat="server" id="lnkAdminSummaryCC" navigateurl="AdminChartCC.aspx" target="right"><li>Summary/Downloads</li></asp:hyperlink>      
      <!--<asp:hyperlink runat="server" id="lnkAdminAddSchoolCC" navigateurl="AdminAddSchoolCC.aspx" target="right"><li>Add Facility</li></asp:hyperlink>-->
    </ul>
    <p>
      <img src="Images/kindergarten.png" alt="Kindergarten" /></p>
    <ul>
      <asp:hyperlink runat="server" id="lnkAdminReportKG" navigateurl="AdminReportKG.aspx" target="right"><li>List/Edit Schools</li></asp:hyperlink>
      <asp:hyperlink runat="server" id="lnkAdminSummaryKG" navigateurl="AdminChartKG.aspx" target="right"><li>Summary/Downloads</li></asp:hyperlink>
      <!--<asp:hyperlink runat="server" id="lnkAdminAddSchoolKG" navigateurl="AdminAddSchoolKG.aspx" target="right"><li>Add School</li></asp:hyperlink>-->
       
    </ul>
    <p>
      <img src="Images/seventh.png" alt="7th Grade" /></p>
    <ul>
      <asp:hyperlink runat="server" id="lnkAdminReportMH" navigateurl="AdminReport7th.aspx" target="right"><li>List/Edit Schools</li></asp:hyperlink>
      <asp:hyperlink runat="server" id="lnkAdminSummaryMH" navigateurl="AdminChart7th.aspx" target="right"><li>Summary/Downloads</li></asp:hyperlink>
      <!--<asp:hyperlink runat="server" id="lnkAdminAddSchoolMH" navigateurl="AdminAddSchool7th.aspx" target="right"><li>Add School</li></asp:hyperlink>-->
      <!--<asp:hyperlink runat="server" id="lnkAdminSearchMH" navigateurl="AdminSearchSchool7th.aspx" target="right"><li>Add School</li></asp:hyperlink>-->
    </ul>
    <p>&nbsp;</p>
    <ul>
      <asp:hyperlink runat="server" id="lnkLogout" navigateurl="AdminLogout.aspx" target="_top"><li><img src="Images/logout.png" alt="logout" /></li></asp:hyperlink></ul>
    <p><span style="font-size: 0.9em">
      <img src="images/helpicon.png" alt="" align="absmiddle" />
      <a href="http://www.cairweb.org/calkidshots//LHD%20Admin%20Site%20Refresher%20(1).mp4" target="_blank">How To Use the Admin</a></span></p>
  </div>

    </form>

</body>
</html>
