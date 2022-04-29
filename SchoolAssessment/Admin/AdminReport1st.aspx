<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReport1st.aspx.cs" Inherits="SchoolAssessment.Admin.AdminReport1st" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <link rel="stylesheet" type="text/css" href="Styles.css" />
    <title>1st Grader Admin</title>
</head>
</head>
<body>
  <form id="form1" runat="server" enableviewstate="True">
      <asp:ValidationSummary ID="valSum" displaymode="SingleParagraph" headertext="Please Correct the following errors: </br>" runat="server" cssclass="ValidationSummary" />
  <h1>1st Grader Schools</h1>
  <table cellpadding="5" style="background: #eee; border:1px solid #ccc;">
    <tr>
      <td>        
        <table cellpadding="2" cellspacing="5">
          <tr>
            <td>
              School Year:
            </td>
            <td>
              <asp:dropdownlist id="cmbSchoolYear" runat="server" autopostback="true">
                <asp:listitem value="20202021">2020-2021</asp:listitem>
                <asp:listitem value="20192020">2019-2020</asp:listitem>
                <asp:listitem value="20182019">2018-2019</asp:listitem>
                <asp:listitem value="20172018">2017-2018</asp:listitem>
                <asp:listitem value="20162017">2016-2017</asp:listitem>
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
              <asp:textbox runat="server" id="txtSchoolCode" columns="20" maxlength="7"></asp:textbox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" controltovalidate="txtSchoolCode" validationexpression="\d{7}" text="&bull;"  runat="server" ErrorMessage="School Code - Invalid format (must be 7 digits with leading zeroes)<br>"></asp:RegularExpressionValidator>
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
              <asp:textbox runat="server" id="txtZip" columns="20" maxlength="5"></asp:textbox>
            </td>
              <td>City:</td>
            <td colspan="3">
              <asp:textbox runat="server" id="txtCity" columns="40"></asp:textbox>    
            </td>
          </tr>
            <tr>
                <td>
                    County:
                </td>
                <td>
                    <asp:textbox runat="server" id="txtCounty"></asp:textbox>
                </td>
                <td>District:</td>
                <td colspan="3">
                    <asp:TextBox ID="textDistrict" runat="server" columns="40"></asp:TextBox>
                </td>
            </tr>
          <tr>
            <td colspan="6">
              <asp:button id="btnFilter" runat="server" text="Filter" OnClick="btnFilter_Click"></asp:button>
              <asp:button id="btnReset" runat="server" text="Reset" OnClick="btnReset_Click"></asp:button>
              <asp:button id="btnCSV" runat="server" text="Download Current View as Excel" OnClick="btnCSV_Click" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  <!--<p>
  <asp:button id="btnReported" visible="false" runat="server" text="All Reported Schools" /> 
  <asp:button id="btnNotReported" visible="false" runat="server" text="All Not Reported Schools" /></p>-->


     
      <asp:GridView ID="grdVSchools" runat="server"  allowpaging="True" PageSize = "15"  CellPadding="4" ForeColor="#333333" GridLines="None"  autogeneratecolumns="False"   AllowSorting="True" OnRowCommand="grdVSchools_RowCommand" OnSelectedIndexChanged="grdVSchools_SelectedIndexChanged" OnPageIndexChanging="grdVSchools_PageIndexChanging" OnSorting="grdVSchools_Sorting" OnRowDataBound="grdVSchools_RowDataBound"   >
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
              <asp:TemplateField HeaderText="Login">
                  <ItemTemplate>
                      <asp:LinkButton ID="LoginBtn" CommandName="Login" runat="server"  CommandArgument ='<%# Eval("Assmntid") %>'></asp:LinkButton>
                  </ItemTemplate>
              </asp:TemplateField>


              <asp:TemplateField HeaderText="Edit/View">
                   <ItemTemplate>
                       <asp:LinkButton ID="EditBtn" CommandName="Edit" runat="server" CommandArgument ='<%# Eval("Assmntid") %>'>Edit/View</asp:LinkButton>
                   </ItemTemplate>
              </asp:TemplateField>


              <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date/Time" ReadOnly="True" SortExpression="SubmitDate" />
              <asp:BoundField DataField="isComplete" HeaderText="Submit Status" ReadOnly="True" SortExpression="isComplete" />


              <asp:TemplateField HeaderText="Summary Report">
                  <ItemTemplate>
                      <asp:LinkButton ID="SummaryRpt"   CommandName="SummaryRpt" runat="server" CommandArgument ='<%# Eval("Assmntid") %>'></asp:LinkButton>
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
