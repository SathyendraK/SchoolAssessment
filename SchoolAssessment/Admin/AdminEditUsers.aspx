<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEditUsers.aspx.cs" Inherits="SchoolAssessment.Admin.AdminEditUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <form id="form1" runat="server">
  <h1>Edit Admin Users</h1>
  <p>Select a county or region to edit. <!--If both county and region have a selection, only county will be changed on update.--></p>
  <asp:label id="lblMsg" runat="server" visible="false"></asp:label>
  <table>
    <tr>
      <td>
        County
      </td>
      <td>
        <asp:dropdownlist id="CountyList" runat="server"></asp:dropdownlist>        
      </td>
    </tr>
    <tr>
      <td>
        Region
      </td>
      <td>
        <asp:dropdownlist id="RegionList" runat="server">
          <asp:listitem value="" text="--Select--" />
          <asp:listitem value="01" text="North CA (01)" />
          <asp:listitem value="02" text="Bay Area (02)" />
          <asp:listitem value="03" text="Central CA (03)" />
          <asp:listitem value="04" text="South CA (04)" />
          <asp:listitem value="05" text="Los Angeles (05)" />
        </asp:dropdownlist>        
      </td>
    </tr>
    <tr>
      <td>
        UserName From
      </td>
      <td>
        <asp:textbox id="txtEmail" runat="server" width="250px"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator17" runat="server" controltovalidate="txtEmail" text="- Required field" cssclass="RequiredFieldValidator" display="Dynamic" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator2" controltovalidate="txtEmail" validationexpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" text="- Invalid email format" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
      </td>
    </tr>
    <tr>
      <td>
        UserName To
      </td>
      <td>
        <asp:textbox id="txtEmailTo" runat="server" width="250px"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="txtEmailTo" text="- Required field" cssclass="RequiredFieldValidator" display="Dynamic" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator1" controltovalidate="txtEmailTo" validationexpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" text="- Invalid email format" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
        
      </td>
    </tr>
  </table>
  <p>
    <asp:button runat="server" id="btnSubmit" text="Update" OnClick="btnSubmit_Click" /></p>
    <hr />
  <asp:datagrid id="grdUsers" runat="server" allowpaging="false" borderstyle="None" bordercolor="#E7E7FF" borderwidth="1px" backcolor="White" autogeneratecolumns="True" gridlines="Horizontal" allowsorting="True" cellpadding="5" OnSortCommand="grdUsers_SortCommand">
    <footerstyle forecolor="#4A3C8C" backcolor="#BBD9EE"></footerstyle>
    <selecteditemstyle font-bold="True" forecolor="#000000" backcolor="#C0C0C0"></selecteditemstyle>
    <alternatingitemstyle backcolor="#ffffff"></alternatingitemstyle>
    <itemstyle forecolor="#333333" backcolor="#eeeeee"></itemstyle>
    <headerstyle font-bold="True" forecolor="#F7F7F7" backcolor="#206BA4"></headerstyle>
  </asp:datagrid>
  </form>
</body>
</html>
