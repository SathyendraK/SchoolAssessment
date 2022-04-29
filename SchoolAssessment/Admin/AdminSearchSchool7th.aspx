<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSearchSchool7th.aspx.cs" Inherits="SchoolAssessment.Admin.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Search/Add School</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <h1>Add 7th Grade School</h1>
  <p>Check if Schools/Facilities already exists.</p>
  
  <table width="250px" cellspacing="2"> 
    <tr>
      <td class="indent2" >
        School Code
      </td>
      <td align="right">
          <asp:TextBox ID="TxtSchCode" runat="server" width=150px ></asp:TextBox>    
      </td>
    </tr>
    <tr>
        <td></td>
        <td align="right"><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
    </tr>
  </table>
  <p><asp:label id="lblMsg" runat="server" visible="false"></asp:label></p>
  <p><asp:LinkButton ID="AddSchoolBtn" runat="server" OnClick="AddSchoolBtn_Click" CommandArgument='<%# Eval("SchCode")%>' CommandName="SchCode">Add School</asp:LinkButton></p><!-- Add the link to AdminAddSchool7th.aspx and carry schcode to this page-->     
              
   
      <p>&nbsp;</p>
      <hr />
    


    <asp:GridView ID="GridSchoolsToSearch" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" autogeneratecolumns="False" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="id" sortexpression="id" readonly="True" HeaderText="id" />
            <asp:BoundField DataField="Cohort" sortexpression="Cohort" readonly="True" HeaderText="Cohort" /><asp:BoundField />
            <asp:BoundField DataField="SchCode" sortexpression="SchCode" readonly="True" HeaderText="SchCode"/><asp:BoundField />
            <asp:BoundField DataField="SchName" sortexpression="SchName" HeaderText="SchName" ReadOnly="True" /><asp:BoundField />
            <asp:BoundField DataField="PhysStreet" sortexpression="PhysStreet" HeaderText="PhysStreet" ReadOnly="True" /><asp:BoundField />
            <asp:BoundField DataField="PhysCity" sortexpression="PhysCity" HeaderText="PhysCity" ReadOnly="True" /><asp:BoundField />
            <asp:BoundField DataField="CoCode" sortexpression="CoCode" HeaderText="CoCode" ReadOnly="True" /><asp:BoundField />
        </Columns>
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

            </div>
    </form>
</body>
</html>
