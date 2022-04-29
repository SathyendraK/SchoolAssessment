<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReportKG.aspx.cs" Inherits="SchoolAssessment.Admin.AdminReportKG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Kindergarten Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <form id="form1" runat="server" enableviewstate="True">
  <h1>Kindergarten Schools</h1>
  <table style="background: #eee; border:1px solid #ccc;" cellpadding="5">
    <tr>
      <td>        
        <table>
          <tr>
            <td>
              School Year:
            </td>
            <td>
              <asp:dropdownlist id="cmbSchoolYear" runat="server" autopostback="true">
                <asp:listitem value="20152016">2016-2017</asp:listitem>
                <asp:listitem value="20152016">2015-2016</asp:listitem>
                <asp:listitem value="20142015">2014-2015</asp:listitem>
                <asp:listitem value="20132014">2013-2014</asp:listitem>
                <asp:listitem value="20122013">2012-2013</asp:listitem>
                
              </asp:dropdownlist>
            </td>
            <td>
              Report Status:
            </td>
            <td colspan="3">
              <asp:dropdownlist id="cmbSubmissionStatus" runat="server" autopostback="true">
                <asp:listitem value="All">All</asp:listitem>
                <asp:listitem value="Reported">Reported Only</asp:listitem>
                <asp:listitem value="Not Reported">Not Reported Only</asp:listitem>
              </asp:dropdownlist>
            </td>
          </tr>
          <tr>
          <td>
              School Code:
            </td>
            <td>
              <asp:textbox runat="server" id="txtSchoolCode" columns="9" maxlength="7"></asp:textbox>
            </td>
            <td>
              School Name:
            </td>
            <td colspan="3">
              <asp:textbox runat="server" id="txtSchoolName" columns="40"></asp:textbox>
            </td>
          </tr>
          <tr>
            <td>
              Zip Code:
            </td>
            <td>
              <asp:textbox runat="server" id="txtZip" columns="9" maxlength="5"></asp:textbox>
            </td>
            <td colspan="4">
              City:
              <asp:textbox runat="server" id="txtCity"></asp:textbox>           
              County:
              <asp:textbox runat="server" id="txtCounty"></asp:textbox>
            </td>
          </tr>
          <tr>
            <td colspan="6">
              <asp:button id="btnFilter" runat="server" text="Filter" OnClick="btnFilter_Click"></asp:button>
              <asp:button id="btnReset" runat="server" text="Reset"></asp:button>
              <asp:button id="btnCSV" runat="server" text="Download Current View as Excel" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  <!--<p>
  <asp:button id="btnReported" visible="false" runat="server" text="All Reported Schools" /> 
  <asp:button id="btnNotReported" visible="false" runat="server" text="All Not Reported Schools" /></p>-->
  <asp:datagrid id="grdSchools" runat="server" allowpaging="True" borderstyle="None" bordercolor="#E7E7FF" borderwidth="1px" backcolor="White" autogeneratecolumns="False" gridlines="Horizontal" allowsorting="True" cellpadding="5" pagesize="10"  >
    <footerstyle forecolor="#4A3C8C" backcolor="#BBD9EE"></footerstyle>
    <selecteditemstyle font-bold="True" forecolor="#000000" backcolor="#C0C0C0"></selecteditemstyle>
    <alternatingitemstyle backcolor="#ffffff"></alternatingitemstyle>
    <itemstyle forecolor="#333333" backcolor="#eeeeee"></itemstyle>
    <headerstyle font-bold="True" forecolor="#F7F7F7" backcolor="#206BA4"></headerstyle>
    <columns>
      <asp:boundcolumn datafield="id" sortexpression="id" readonly="True" headertext="id" visible="false"></asp:boundcolumn>
      <asp:boundcolumn datafield="SchCode" sortexpression="SchCode" readonly="True" headertext="Code"></asp:boundcolumn>
      <asp:boundcolumn datafield="SchType" sortexpression="SchType" readonly="True" headertext="Type"></asp:boundcolumn>
      <asp:boundcolumn datafield="DistName" sortexpression="DistName" readonly="True" headertext="District"></asp:boundcolumn>
      <asp:boundcolumn datafield="SchName" sortexpression="SchName" readonly="True" headertext="Name"></asp:boundcolumn>
      <asp:boundcolumn datafield="CoName" sortexpression="CoName" readonly="True" headertext="County"></asp:boundcolumn>
      <asp:boundcolumn datafield="PhysStreet" sortexpression="PhysStreet" readonly="True" headertext="Address"></asp:boundcolumn>
      <asp:boundcolumn datafield="PhysCity" sortexpression="PhysCity" readonly="True" headertext="City"></asp:boundcolumn>
      <asp:boundcolumn datafield="PhysZip" sortexpression="PhysZip" readonly="True" headertext="Zip"></asp:boundcolumn>
      <asp:boundcolumn datafield="SchPhone" sortexpression="SchPhone" readonly="True" headertext="Phone" itemstyle-wrap="False"></asp:boundcolumn>
      <asp:HyperLinkColumn HeaderText="Memo" DataNavigateUrlField="Memo" DataNavigateUrlFormatString="javascript:alert('{0}');" datatextfield="Memo" datatextformatstring="Memo"></asp:HyperLinkColumn>
        <asp:buttoncolumn text="Edit/View" headertext="Edit/View" commandname="Edit"></asp:buttoncolumn>
      <asp:buttoncolumn text="Login" headertext="Login" commandname="Login"></asp:buttoncolumn>
      <asp:buttoncolumn text="PDF" headertext="PDF Summary Sheet" commandname="PDF"></asp:buttoncolumn>
      <asp:boundcolumn datafield="SubmitDate" sortexpression="SubmitDate" readonly="True" headertext="Submit Date/Time"></asp:boundcolumn>
      <asp:boundcolumn datafield="isComplete" sortexpression="isComplete" readonly="True" headertext="Submit Status"></asp:boundcolumn>
    </columns>
    <pagerstyle horizontalalign="Left" forecolor="#4A3C8C" position="TopAndBottom" backcolor="#E7E4D3" mode="NumericPages"></pagerstyle>
  </asp:datagrid>

     
      <asp:GridView ID="grdVSchools" runat="server"  allowpaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  autogeneratecolumns="False"   AllowSorting="True" OnRowCommand="grdVSchools_RowCommand">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          <Columns>
              <asp:BoundField DataField="id" sortexpression="id" readonly="True" headertext="id" visible="false"/><asp:BoundField />
              <asp:BoundField DataField="SchCode" sortexpression="SchCode" readonly="True" headertext="Code"/><asp:BoundField />
              <asp:BoundField DataField="SchType" HeaderText="Type" ReadOnly="True" SortExpression="SchType" />
              <asp:BoundField DataField="DistName" HeaderText="District" ReadOnly="True" SortExpression="DistName" />

              <asp:BoundField DataField="SchName" HeaderText="Name" ReadOnly="True" SortExpression="SchName" />
              <asp:BoundField DataField="CoName" HeaderText="County" ReadOnly="True" SortExpression="CoName" />
              <asp:BoundField DataField="PhysStreet" HeaderText="Address" ReadOnly="True" SortExpression="PhysStreet" />
              <asp:BoundField DataField="PhysCity" HeaderText="City" ReadOnly="True" SortExpression="PhysCity" />
              <asp:BoundField DataField="PhysZip" HeaderText="Zip" ReadOnly="True" SortExpression="PhysZip" />
              <asp:BoundField DataField="SchPhone" HeaderText="Phone" ReadOnly="True" SortExpression="SchPhone" />
              <asp:ButtonField ButtonType="Button" CommandName="Edit" HeaderText="Edit/View" ShowHeader="True" Text="Edit" />     
              <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date/Time" ReadOnly="True" SortExpression="SubmitDate" />
              <asp:BoundField DataField="isComplete" HeaderText="Submit Status" ReadOnly="True" SortExpression="isComplete" />
              <asp:TemplateField HeaderText="Login">
                  <ItemTemplate>
                      <asp:LinkButton ID="LoginBtn" CommandName="Login" runat="server"   >Login</asp:LinkButton>
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

     
  </form>
</body>
</html>
