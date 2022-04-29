<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReactivateSchool.aspx.cs" Inherits="SchoolAssessment.Admin.AdminReactivateSchool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <form id="form1" runat="server">
  <h1>Reactivate Schools</h1>
  <p>Find Schools/Facilities to reactivate. The School/Facility code must be 7 digits for Kindergarten or 7th grade. 9 digits for Childcare.</p>
  <asp:ValidationSummary ID="valSum" displaymode="SingleParagraph" headertext="Please see errors indicated by asterisks.<br>" runat="server" cssclass="ValidationSummary" />
  <asp:label id="lblMsg" runat="server" visible="false"></asp:label>
  <table style="background: #eee; border:1px solid #ccc;" cellpadding="5" >
    <tr>
      <td>
        School Code
      </td>
      <td>
          <asp:TextBox ID="TxtSchCode" runat="server" maxlength="9"></asp:TextBox>
          <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Code - Invalid format (must be 7 digits for Kindergarten or 7th grade.  9 digits for Childcare).<br>" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
           
      </td>
    </tr>
    <tr>
        <td></td>
        <td align="right"><asp:button runat="server" id="btnSearch" text="Search" OnClick="btnSearch_Click" /></td>
    </tr>
    

  </table>
  <p>
    </p>
    <hr />
      <asp:GridView ID="GridSchoolsToReactivate" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" autogeneratecolumns="False" OnRowCommand="GridSchoolsToReactivate_RowCommand" OnSelectedIndexChanged="GridSchoolsToReactivate_SelectedIndexChanged" OnRowDataBound="GridSchoolsToReactivate_RowDataBound">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          <Columns>
            
              <asp:BoundField DataField="id" sortexpression="id" readonly="True" headertext="id"/>
              <asp:BoundField DataField="Cohort" sortexpression="Cohort" readonly="True" headertext="Cohort"/><asp:BoundField />
              <asp:BoundField DataField="SchCode" sortexpression="SchCode" readonly="True" headertext="SchCode" /><asp:BoundField />
              <asp:BoundField DataField="SchName" sortexpression="SchName" readonly="True" headertext="SchName"/><asp:BoundField />
              <asp:BoundField DataField="PhysStreet" sortexpression="PhysStreet" readonly="True" headertext="PhysStreet"/><asp:BoundField />
              <asp:BoundField DataField="PhysCity" sortexpression="PhysCity" readonly="True" headertext="PhysCity"/><asp:BoundField />
              <asp:BoundField DataField="CoCode" sortexpression="CoCode" readonly="True" headertext="CoCode"/><asp:BoundField />

              

              <asp:TemplateField HeaderText="Activate" Visible="True">
                  <ItemTemplate>
                      <asp:Button ID="BtnReactivate" runat="server" Text="Reactivate" CommandName="Reactivate" CommandArgument ='<%# Eval("id") %>'  Visible="true" />
                      </ItemTemplate>
              </asp:TemplateField>

              

          </Columns>
          <EditRowStyle BackColor="#999999" />
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <SortedAscendingCellStyle BackColor="#E9E7E2" />
          <SortedAscendingHeaderStyle BackColor="#506C8C" />
          <SortedDescendingCellStyle BackColor="#FFFDF8" />
          <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
      </asp:GridView>
 <!-- 
 <asp:datagrid id="grdSchools" runat="server" allowpaging="false" borderstyle="None" bordercolor="#E7E7FF" borderwidth="1px" backcolor="White" autogeneratecolumns="True" gridlines="Horizontal" allowsorting="True" cellpadding="5">
    <footerstyle forecolor="#4A3C8C" backcolor="#BBD9EE"></footerstyle>
    <selecteditemstyle font-bold="True" forecolor="#000000" backcolor="#C0C0C0"></selecteditemstyle>
    <alternatingitemstyle backcolor="#ffffff"></alternatingitemstyle>
    <itemstyle forecolor="#333333" backcolor="#eeeeee"></itemstyle>
    <headerstyle font-bold="True" forecolor="#F7F7F7" backcolor="#206BA4"></headerstyle>
  </asp:datagrid>
      -->
  </form>
</body>
</html>
