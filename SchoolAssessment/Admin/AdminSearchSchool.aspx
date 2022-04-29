<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSearchSchool.aspx.cs" Inherits="SchoolAssessment.Admin.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Admin Search/Add School</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
     </head>
<body>
    <form id="form1" runat="server">
    <div>
     <h1>Add / Reactivate School</h1>
  <p>Check if Schools/Facilities already exists.&nbsp; The School/Facility code must be 7 digits for Kindergarten or 7th grade. 9 digits for Childcare.</p>
  <asp:ValidationSummary ID="valSum" displaymode="SingleParagraph" headertext="Please see errors indicated by asterisks.<br>" runat="server" cssclass="ValidationSummary" />
  <table  style="background: #eee; border:1px solid #ccc;" cellpadding="5"> 
    <tr>
      <td class="indent2">
        School Code
      </td>
      <td align="right">
          <asp:TextBox ID="TxtSchCode" runat="server" width=150px maxlength="9"></asp:TextBox>    
          <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Code - Invalid format (must be 7 digits for Kindergarten or 7th grade.  9 digits for Childcare).<br>" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
      </td>
    </tr>
    <!--
    <tr>
        <td>Cohort</td>
        <td align="right">
            <asp:DropDownList ID="DropDownCohort" runat="server" width=158px OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                <asp:ListItem Value="C">Childcare</asp:ListItem>
                <asp:ListItem Value="K">Kindergarten</asp:ListItem>
                <asp:ListItem Value="S">7th Grade</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
      -->
    <tr>
        <td ></td>
        <td align="right"> <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
    </tr>
  </table>
        
  <p><asp:label id="lblMsg" runat="server" visible="false"></asp:label></p>
        <p>
            <asp:LinkButton ID="AddSchoolBtn" runat="server" CommandArgument='<%# Eval("SchCode")%>' CommandName="SchCode" OnClick="AddSchoolBtn_Click">Add School</asp:LinkButton>
        </p>

        <hr />


    </div>
        <asp:Label ID="lblMsg1" runat="server" visible="false"></asp:Label>
        <p>
            <asp:GridView ID="GridSchoolsToSearch" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" OnRowCommand="GridSchoolsToSearch_RowCommand" OnRowDataBound="GridSchoolsToSearch_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="id"  HeaderText="id" ReadOnly="True" />
                    <asp:BoundField DataField="Cohort"  HeaderText="Cohort" ReadOnly="True" />
                    <asp:BoundField DataField="SchCode" HeaderText="SchCode" ReadOnly="True" />
                    <asp:BoundField DataField="SchName" HeaderText="SchName" ReadOnly="True" />
                    <asp:BoundField DataField="PhysStreet" HeaderText="PhysStreet" ReadOnly="True" />
                    <asp:BoundField DataField="PhysCity" HeaderText="PhysCity" ReadOnly="True" />
                    <asp:BoundField DataField="CoCode" HeaderText="CoCode" ReadOnly="True" />
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
        </p>

        <p>
            <asp:LinkButton ID="Add7thSchoolBtn"  CommandArgument='<%# Eval("SchCode")%>' CommandName="Add7thSchool" runat="server" OnClick="Add7thSchoolBtn_Click">Add 7th Grade School</asp:LinkButton>
        </p>
        <p>
            <asp:LinkButton ID="AddKSchoolBtn" CommandArgument='<%# Eval("SchCode")%>' CommandName="AddKSchool" runat="server" OnClick="AddKSchoolBtn_Click">Add Kindergarten</asp:LinkButton>
        </p>
       
        
    </form>
</body>
</html>
