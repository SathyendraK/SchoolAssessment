<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddSchool.aspx.cs" Inherits="SchoolAssessment.Admin.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Schools </title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
    <style type="text/css">

hr {
	height:1px;
	background-color:#b4dde1;
	border:0;
	border-top:1px solid #b4dde1;
	border-collapse:collapse;
	color:#b4dde1;
	margin: 1em 0;
	padding:0;
	clear: both;
}

.ValidationSummary 
{
  font-weight: bold;
  color: #000000;
  border: 1px solid #cc0000;
  background: #F5D7EA;
  padding: 10px;
}

table 
{
  margin-top: .5em;
  margin-bottom: .5em;  
  border-collapse: collapse;
}
td, input, select, option {
	font-family: Arial, Sans-Serif;
	font-size: 1em;	
}

.RequiredFieldValidator 
{
  color:#cc0000;
  font-weight: bold;    
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Add School</h1>
      <p>&nbsp;</p>
    </div>
      <hr />
  <asp:validationsummary id="valSum" displaymode="SingleParagraph" headertext="Please enter values for required fields indicated by asterisks.<br>" runat="server" cssclass="ValidationSummary" />
  <asp:label id="lblMsg" runat="server" visible="false"></asp:label>
  <table>
    <tr>
        <td>Cohort</td>
        <td>
            <asp:DropDownList ID="DropDownCohort" runat="server" OnSelectedIndexChanged="DropDownCohort_SelectedIndexChanged">
                <asp:listitem value="">--Select--</asp:listitem>
                <asp:listitem value="C">Childcare</asp:listitem>
                <asp:listitem value="K">Kindergarten</asp:listitem>
                <asp:listitem value="S">7th Grade</asp:listitem>
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" controltovalidate="DropDownCohort" text="*" cssclass="RequiredFieldValidator" ></asp:RequiredFieldValidator>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
      <td>
        School Year 
      </td>
      <td width="300">
        <asp:dropdownlist runat="server" id="cmbSchoolYear" tabindex="1" OnSelectedIndexChanged="cmbSchoolYear_SelectedIndexChanged">
        <asp:listitem value="20192020">2019-2020</asp:listitem>
        </asp:dropdownlist>
         
      </td>
      <td>
        Charter <font color="black">*</font>
      </td>
      <td>
        <asp:dropdownlist runat="server" id="cmbCharter" tabindex="2">
          <asp:listitem value="">--Select--</asp:listitem>
          <asp:listitem value="N">NO</asp:listitem>
          <asp:listitem value="Y">YES</asp:listitem>
        </asp:dropdownlist>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" controltovalidate="cmbCharter" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        School Type <font color="black">*</font>
      </td>
      <td>
        <asp:dropdownlist runat="server" id="cmbSchoolType" autopostback="true" tabindex="3" OnSelectedIndexChanged="cmbSchoolType_SelectedIndexChanged"></asp:dropdownlist>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator15" runat="server" controltovalidate="cmbSchoolType" text="*" cssclass="RequiredFieldValidator" InitialValue="--Select--" />
      </td>
      <td>
        &nbsp;
      </td>
      <td>
        &nbsp;
      </td>
    </tr>
    <tr>
      <td>
        County <font color="black">*</font>
      </td>
      <td>
          <!--Changed to drop down by AT on 06/10/2015-->
          <asp:DropDownList ID="txtCounty" runat="server" AutoPostBack="True" Width="250px" tabindex="4" Enabled="true" OnSelectedIndexChanged="txtCounty_SelectedIndexChanged"></asp:DropDownList>
          <asp:requiredfieldvalidator ID="Requiredfieldvalidator17" runat="server" controltovalidate="txtCounty" text="*" cssclass="RequiredFieldValidator" InitialValue="--Select--" />

      </td>
      <td>
        County Code <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtCountyCode" runat="server" columns="10" maxlength="2" tabindex="5" BackColor="#CCCCCC"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="txtCountyCode" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        Public School District <font color="black">*</font>
      </td>
      <td>
          <!--Changed to drop down by AT on 06/10/2015-->
          <asp:DropDownList ID="txtDistrict" runat="server" AutoPostBack="True" Width="250px" tabindex="6" OnSelectedIndexChanged="txtDistrict_SelectedIndexChanged"></asp:DropDownList>
          <asp:requiredfieldvalidator id="valDistrict" runat="server" controltovalidate="txtDistrict" text="*" cssclass="RequiredFieldValidator" InitialValue="--Select--"/>
      </td>
      <td>
        District Code <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtDistrictCode" runat="server" columns="10" maxlength="5" tabindex="7" BackColor="#CCCCCC"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" controltovalidate="txtDistrictCode" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        School Name <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtSchoolName" runat="server" width="250px" tabindex="8"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" controltovalidate="txtSchoolName" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator11" controltovalidate="txtSchoolName" validationexpression="[.\w\s',#&-]+" text="&bull;" errormessage="School Name contains invalid characters (only letters, numbers, spaces, commas, dashes, pound sign, ampersand, and single quotes allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td>
        School Code <font color="black">*</font>
          <img align="top" alt="" height="14" src="images/icon_Info.png" title="Last 7 digits of CDE Code" width="14" /></td>
      <td>
        <asp:textbox id="txtSchoolCode" runat="server" columns="10" maxlength="9" tabindex="9" BackColor="#CCCCCC"></asp:textbox>
          <asp:CustomValidator ID="CustomValidator1" CssClass="RequiredFieldValidator"  runat="server" text="*" ErrorMessage="School Code - Invalid format (must be 7 digits for Kindergarten or 7th grade.  9 digits for Childcare).<br>" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" controltovalidate="txtSchoolCode" text="*" cssclass="RequiredFieldValidator" />
        </td>
    </tr>
    <tr>
      <td>
        School Phone <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtSchoolPhone" runat="server" maxlength="3" Width="35px" tabindex="10"></asp:textbox>
          <b>-</b>
          <asp:TextBox ID="txtSchoolPhone_1" runat="server" MaxLength="3" Width="35px" TabIndex="11"></asp:TextBox><b>-</b>
          <asp:TextBox ID="txtSchoolPhone_2" runat="server" MaxLength="4" Width="35px" TabIndex="12"></asp:TextBox>
        
          <asp:CustomValidator ID="CustomValidator_txtSchoolPhone_1" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Phone - Required<br>" OnServerValidate="CustomValidator_txtSchoolPhone_1_ServerValidate"></asp:CustomValidator>
          <asp:CustomValidator ID="CustomValidator_txtSchoolPhone_Digit" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Phone - Invalid character<br>" OnServerValidate="CustomValidator_txtSchoolPhone_Digit_ServerValidate"></asp:CustomValidator>  
        <!-- Added below by AT on 06/22/2015 -->
        <asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" controltovalidate="txtSchoolPhone" text="*" cssclass="RequiredFieldValidator" />
       
      </td>
      <td>
        <!-- Commented out by AT on 08/12/2015 SPA info no longer necessary -->
        <!--SPA Code -->
      </td>
      <td>
        <!--<asp:textbox id="txtSPACode" runat="server" columns="10" maxlength="2" tabindex="11"></asp:textbox>-->
      </td>
    </tr>
    <tr>
      <td>
        Administrator Name</td>
      <td>
        <asp:textbox id="txtSchAdmin" runat="server" tabindex="13"></asp:textbox>
      </td>
      <td>
        Administrator Email
      </td>
      <td>
        <asp:textbox id="txtSchEmail" runat="server" tabindex="14"></asp:textbox>
           </td>
    </tr>
    <tr>
      <td>
        Superintendent Name
      </td>
      <td>
        <asp:textbox id="txtSuperintendentName" runat="server" tabindex="15"></asp:textbox>
      </td>
      <td>
        Superintendent Email
      </td>
      <td>
        <asp:textbox id="txtSuperintendentEmail" runat="server" tabindex="16"></asp:textbox>
          </td>
    </tr>
    <tr>
      <td>
        Superintendent Phone 
      </td>
      <td>
        <asp:textbox id="txtSuperintendentPhone" MaxLength="3" runat="server" Width="35px" tabindex="17"></asp:textbox><b>-</b>
          <asp:TextBox ID="txtSuperintendentPhone_1" MaxLength="3" runat="server" Width="35px"  tabindex="18"></asp:TextBox><b>-</b>
          <asp:TextBox ID="txtSuperintendentPhone_2" MaxLength="4" runat="server" Width="35px"  tabindex="19"></asp:TextBox>
        <!-- Added below by AT on 06/22/2015 -->
          <asp:CustomValidator ID="CustomValidator_txtSuperintendentPhone_Digit" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Superintendent Phone - Invalid character<br>" OnServerValidate="CustomValidator_txtSuperintendentPhone_Digit_ServerValidate"></asp:CustomValidator>
        </td>
      <td>
      </td>
      <td>
      </td>
    </tr>
    <tr>
      <td>
        Address
      </td>
      <td>
        <em><strong>Physical Address</strong></em>
      </td>
      <td colspan="2" width="350">
        <em><strong>Mailing Address</strong></em> (<asp:CheckBox ID="chkaddress" runat="server" autopostback="True" tabindex="20" OnCheckedChanged="chkaddress_CheckedChanged"/>
          
        Check if same as Physical)
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Street</span> <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtPhyAddress" runat="server" width="250px" tabindex="21"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" controltovalidate="txtPhyAddress" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator6" controltovalidate="txtPhyAddress" validationexpression="[.\w\s,#&-]+" text="&bull;" errormessage="Physical Street contains invalid characters (only letters, numbers, spaces, commas, dashes, pound sign, ampersand, and single quotes allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td colspan="2"><font color="black">*</font>
        <asp:textbox id="txtMailAddress" runat="server" width="250px" tabindex="24"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" controltovalidate="txtMailAddress" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator9" controltovalidate="txtMailAddress" validationexpression="[.\w\s,#&-]+" text="&bull;" errormessage="Mail Street contains invalid characters (only letters, numbers, spaces, commas, dashes, pound sign, ampersand, and single quotes allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">City</span> <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtPhyCity" runat="server" width="250px" tabindex="22"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" controltovalidate="txtPhyCity" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator7" controltovalidate="txtPhyCity" validationexpression="[\w\s]+" text="&bull;" errormessage="Physical City contains invalid characters (only letters and spaces allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td colspan="2"><font color="black">*</font>
        <asp:textbox id="txtMailCity" runat="server" width="250px" tabindex="25"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" controltovalidate="txtMailCity" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator5" controltovalidate="txtPhyCity" validationexpression="[\w\s]+" text="&bull;" errormessage="Mail City contains invalid characters (only letters and spaces allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Zip</span> <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtPhyZip" runat="server" columns="20" maxlength="5" tabindex="23"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" controltovalidate="txtPhyZip" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator8" controltovalidate="txtPhyZip" validationexpression="\d{5}" errormessage="Physical Zip must be five digits.<br>" runat="server" cssclass="RequiredFieldValidator" display="Dynamic">•</asp:regularexpressionvalidator>
      </td>
      <td colspan="2"><font color="black">*</font>
        <asp:textbox id="txtMailZip" runat="server" maxlength="5" tabindex="26"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" controltovalidate="txtMailZip" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator10" controltovalidate="txtMailZip" validationexpression="\d{5}" errormessage="Mail Zip must be five digits.<br>" runat="server" cssclass="RequiredFieldValidator" display="Dynamic">•</asp:regularexpressionvalidator>
      </td>
    </tr>
    <tr>
      <td>
        Designated Contact Name <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtContactPerson" runat="server" width="250px" tabindex="27"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" controltovalidate="txtContactPerson" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator4" controltovalidate="txtContactPerson" validationexpression="[.\w\s',#&-]+" text="&bull;" errormessage="School Contact Name - Invalid characters (only letters, numbers, spaces, commas, dashes, pound sign, ampersand, and single quotes allowed).<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td>
      </td>
      <td>
      </td>
    </tr>
    <tr>
      <td>
        Designated Contact Email <font color="black">*</font>
      </td>
      <td>
        <asp:textbox id="txtContactEmail" runat="server" width="250px" tabindex="28"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator13" runat="server" controltovalidate="txtContactEmail" text="*" cssclass="RequiredFieldValidator" />
          <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" controltovalidate="txtContactEmail" validationexpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z0-9]{2,17})$" text="&bull;" errormessage="School Contact Email - Invalid format.<br>" runat="server" cssclass="RequiredFieldValidator" enableclientscript="False"></asp:RegularExpressionValidator>   --> 
          <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
      </td>
      <td>
      </td>
      <td>
      </td>
    </tr>
    <tr>
      <td>
        Designated Contact Phone 
      </td>
      <td>
        <asp:textbox id="txtContactPhone" runat="server" maxlength="3" tabindex="29" Width="35px"></asp:textbox><b>-</b> 
          <asp:TextBox ID="txtContactPhone_1" runat="server" MaxLength="3" Width="35px" TabIndex="30"></asp:TextBox><b>-</b>
          <asp:TextBox ID="txtContactPhone_2" runat="server" MaxLength="4" Width="35px" TabIndex="31"></asp:TextBox>
          <asp:CustomValidator ID="CustomValidator_txtContactPhone_Digit" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;"  ErrorMessage="School Contact Phone - Invalid character<br>" OnServerValidate="CustomValidator_txtContactPhone_Digit_ServerValidate"></asp:CustomValidator>
&nbsp;&nbsp;
        </td>
      <td>
        Extension
      </td>
      <td>
        <asp:textbox id="txtContactPhoneExt" runat="server" columns="10" maxlength="7" tabindex="32"></asp:textbox>
      </td>
    </tr>
  </table>
         <asp:Button ID="btnSubmit" runat="server" text="Add New School" tabindex="33" OnClick="btnSubmit_Click"/>
    </form>
</body>
</html>
