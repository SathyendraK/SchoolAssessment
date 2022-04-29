<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddDistricts.aspx.cs" Inherits="SchoolAssessment.Admin.AdminAddDistricts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Add School District</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>Add School Districts</h1>
        <p>Please enter value for required fields to add school district.</p>
        <p><asp:Label ID="lblMsg" runat="server" visible="false"></asp:Label></p>
        
         <asp:ValidationSummary ID="ValidationSummary1" headertext="Please enter values for required fields indicated by asterisks." runat="server" cssclass="ValidationSummary"/>
        
         <table style="background: #eee; border:1px solid #ccc;" cellpadding="5" >
             
             <tr>
                 <td>District Code:</td>
                 <td>
                     <asp:TextBox ID="TexDistCode" runat="server" columns="50"  maxlength="5"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" controltovalidate="TexDistCode" text="*" cssclass="RequiredFieldValidator" ></asp:RequiredFieldValidator>
                    
                 </td>
             </tr>
             <tr>
                 <td>District Name:</td>
                 <td>
                     <asp:TextBox ID="TextDistName" runat="server" columns="50" OnTextChanged="TextDistName_TextChanged"></asp:TextBox>
                     
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" controltovalidate="TextDistName" text="*" cssclass="RequiredFieldValidator" ></asp:RequiredFieldValidator>
                     
                 </td>
             </tr>
             <tr>
                 <td>County:</td>
                 <td>
                     <asp:DropDownList ID="txtCounty" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="txtCounty_SelectedIndexChanged"></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" controltovalidate="txtCounty" text="*" cssclass="RequiredFieldValidator"></asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td></td>
                 <td align="left">
                     <asp:Button ID="btnSubmit" runat="server" Text="Add District" OnClick="btnSubmit_Click" /></td>
             </tr>
        </table>
    <div>
    </div>
        <br></br>
        <asp:GridView ID="GridAddDistrict" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="GridAddDistrict_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="DistCode"  HeaderText="District Code"  ReadOnly="True" />
                <asp:BoundField DataField="DistName" HeaderText="District Name" ReadOnly="True" />
                <asp:BoundField DataField="CoName" HeaderText="County" ReadOnly="True" />
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
