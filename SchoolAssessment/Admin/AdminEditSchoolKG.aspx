<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEditSchoolKG.aspx.cs" Inherits="SchoolAssessment.Admin.AdminEditSchoolKG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<title>Kindergarten Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
  <script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#Form1').submit(function () {
        $(this).find('input:text').each(function () {
          $(this).val($.trim($(this).val()));
        });
      });
    });
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <h1>Edit Kindergarten School</h1>
  <asp:validationsummary id="valSum" displaymode="SingleParagraph" headertext="Please Correct the following errors: </br>" runat="server" cssclass="ValidationSummary" />
  <asp:label id="lblMsg" runat="server" visible="false"></asp:label>
  <table runat="server" id="Table1">
    <tr>
      <td height="30" width="90">
        Report Status
      </td>
      <td width="300" height="30">
        <asp:label runat="server" id="lblStatus"></asp:label>
      </td>
      <td width="80" height="30">
        Password
      </td>
      <td width="300" height="30">
        <asp:label runat="server" id="lblpin"></asp:label>
      </td>
    </tr>
    <tr>
      <td>
        School Year
      </td>
      <td>
        <asp:literal runat="server" id="litSchoolYear" />
      </td>
      <td width="80">
        Charter
      </td>
      <td>
        <asp:dropdownlist runat="server" id="cmbCharter" tabindex="2">
          <asp:listitem value="N">NO</asp:listitem>
          <asp:listitem value="Y">YES</asp:listitem>
        </asp:dropdownlist>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" controltovalidate="cmbCharter" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        School Type
      </td>
      <td>
        <asp:dropdownlist runat="server" id="cmbSchoolType" autopostback="true" tabindex="3">
          <asp:listitem value="PUBLIC">PUBLIC</asp:listitem>
          <asp:listitem value="PRIVATE">PRIVATE</asp:listitem>
        </asp:dropdownlist>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator15" runat="server" controltovalidate="cmbSchoolType" text="*" cssclass="RequiredFieldValidator" />
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
        County
      </td>
      <td>
        <asp:textbox id="txtCounty" runat="server" width="250px" tabindex="4"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="txtCounty" text="*" cssclass="RequiredFieldValidator" />
      </td>
      <td>
        County Code
      </td>
      <td>
        <asp:textbox id="txtCountyCode" runat="server" columns="10" maxlength="2" tabindex="5"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" controltovalidate="txtCountyCode" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        Public School District
      </td>
      <td>
        <asp:textbox id="txtDistrict" runat="server" width="250px" tabindex="6"></asp:textbox>
        <!-- Changed from requiredfieldvalidator to CustomValidator by AT on 09/17/2015 -->
        <asp:CustomValidator ID="schoolDistrictValidator" runat="server" OnServerValidate="schoolDistrictValidate" Display="Dynamic" Text="*" cssclass="RequiredFieldValidator"></asp:CustomValidator>
      </td>
      <td>
        District Code
      </td>
      <td>
        <asp:textbox id="txtDistrictCode" runat="server" columns="10" maxlength="5" tabindex="7"></asp:textbox>
        <!-- Changed from requiredfieldvalidator to CustomValidator by AT on 09/17/2015 -->
        <asp:CustomValidator ID="districtCodeValidator" runat="server" OnServerValidate="districtCodeValide" Display="Dynamic" Text="*" cssclass="RequiredFieldValidato"></asp:CustomValidator>
      </td>
    </tr>
    <tr>
      <td>
        School Name
      </td>
      <td>
        <asp:textbox id="txtSchoolName" runat="server" width="250px" tabindex="8"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" controltovalidate="txtSchoolName" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator11" controltovalidate="txtSchoolName" validationexpression="[.\w\s',-/]+" text="&bull;" errormessage="School Name contains invalid characters (only letters, numbers, spaces, commas, dashes, and single quotes allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td>
        School Code
      </td>
      <td>
        <asp:textbox id="txtSchoolCode" runat="server" columns="10" maxlength="7" tabindex="9"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" controltovalidate="txtSchoolCode" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator1" controltovalidate="txtSchoolCode" validationexpression="\d{7}" text="&bull;" errormessage="School Code - Invalid format (must be 7 digits with leading zeroes)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        School Phone
      </td>
      <td>
        <asp:textbox id="txtSchoolPhone" runat="server" maxlength="12" tabindex="10"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" controltovalidate="txtSchoolPhone" text="*" cssclass="RequiredFieldValidator" />
      </td>
      <td>
        <!--SPA Code--><!-- Commented out by AT on 07/31/2015 -->
        <!--SPA Code-->
      </td>
      <td>
        <!--<asp:textbox id="txtSPACode" runat="server" columns="10" maxlength="2" tabindex="11"></asp:textbox>-->
      </td>
    </tr>
    <tr>
      <td>
        Administrator Name
      </td>
      <td>
        <asp:textbox id="txtSchAdmin" runat="server" tabindex="12"></asp:textbox>
        <asp:literal id="litSchAdmin" runat="server"></asp:literal>
      </td>
      <td>
        Administrator Email
      </td>
      <td>
        <asp:textbox id="txtSchEmail" runat="server" tabindex="13"></asp:textbox>
        <asp:literal id="litSchEmail" runat="server"></asp:literal>
      </td>
    </tr>
    <tr>
      <td>
        Superintendent Name
      </td>
      <td>
        <asp:textbox id="txtSuperintendentName" runat="server" tabindex="14"></asp:textbox>
        <asp:literal id="litSuperintendentName" runat="server"></asp:literal>
      </td>
      <td>
        Superintendent Email
      </td>
      <td>
        <asp:textbox id="txtSuperintendentEmail" runat="server" tabindex="15"></asp:textbox>
        <asp:literal id="litSuperintendentEmail" runat="server"></asp:literal>
      </td>
    </tr>
    <tr>
      <td>
        Superintendent Phone
      </td>
      <td>
        <asp:textbox id="txtSuperintendentPhone" runat="server" tabindex="16"></asp:textbox>
        <asp:literal id="litSuperintendentPhone" runat="server"></asp:literal>
      </td>
      <td>
      </td>
      <td>
      </td>
    </tr>
    <tr>
      <td height="24">
        Address
      </td>
      <td>
        <em><strong>Physical Address</strong></em>
      </td>
      <td colspan="2" width="350">
        <em><strong>Mailing Address</strong></em> (<asp:checkbox id="chkaddress" runat="server" autopostback="True" tabindex="20"  OnCheckedChanged="chkaddress_CheckedChanged"></asp:checkbox>
        Check if same as Physical)
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Street</span>
      </td>
      <td>
        <asp:textbox id="txtPhyAddress" runat="server" width="250px" tabindex="17"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" controltovalidate="txtPhyAddress" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator6" controltovalidate="txtPhyAddress" validationexpression="[.\w\s,-]+" text="&bull;" errormessage="Physical Street contains invalid characters (only letters, numbers, spaces, periods, commas, and dashes allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td colspan="2">
        <asp:textbox id="txtMailAddress" runat="server" width="250px" tabindex="21"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" controltovalidate="txtMailAddress" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator9" controltovalidate="txtMailAddress" validationexpression="[.\w\s,-]+" text="&bull;" errormessage="Mail Street contains invalid characters (only letters, numbers, spaces, periods, commas, and dashes allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">City</span>
      </td>
      <td>
        <asp:textbox id="txtPhyCity" runat="server" width="250px" tabindex="18"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" controltovalidate="txtPhyCity" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator7" controltovalidate="txtPhyCity" validationexpression="[\w\s]+" text="&bull;" errormessage="Physical City contains invalid characters (only letters and spaces allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td colspan="2">
        <asp:textbox id="txtMailCity" runat="server" width="250px" tabindex="22"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" controltovalidate="txtMailCity" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator5" controltovalidate="txtPhyCity" validationexpression="[\w\s]+" text="&bull;" errormessage="Mail City contains invalid characters (only letters and spaces allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Zip</span>
      </td>
      <td>
        <asp:textbox id="txtPhyZip" runat="server" columns="20" maxlength="10" tabindex="19"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" controltovalidate="txtPhyZip" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator8" controltovalidate="txtPhyZip" validationexpression="\d{5}" errormessage="Physical Zip must be five digits<br>" runat="server" cssclass="RequiredFieldValidator" display="Dynamic">•</asp:regularexpressionvalidator>
      </td>
      <td colspan="2">
        <asp:textbox id="txtMailZip" runat="server" maxlength="10" tabindex="23"></asp:textbox>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" controltovalidate="txtMailZip" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator10" controltovalidate="txtMailZip" validationexpression="\d{5}" errormessage="Mail Zip must be five digits<br>" runat="server" cssclass="RequiredFieldValidator" display="Dynamic">•</asp:regularexpressionvalidator>
      </td>
    </tr>
    <tr>
      <td>
        Students Enrolled?
      </td>
      <td colspan="3">
        <asp:dropdownlist id="drpdwnKndrYesNo" runat="server" autopostback="True" tabindex="24">
          <asp:listitem value="Yes">Yes</asp:listitem>
          <asp:listitem value="No">No</asp:listitem>
        </asp:dropdownlist>
      </td>
    </tr>
    <tr>
      <td>
        Reason
      </td>
      <td colspan="3">
        <asp:dropdownlist id="drpReason" runat="server" tabindex="25">
          <asp:listitem value="">--Select--</asp:listitem>
          <asp:listitem value="NO KINDERGARTEN THIS YEAR">NO KINDERGARTEN THIS YEAR</asp:listitem>
          <asp:listitem value="NO KINDERGARTEN EVER">NO KINDERGARTEN EVER</asp:listitem>
          <asp:listitem value="SCHOOL CLOSED">SCHOOL CLOSED</asp:listitem>
        </asp:dropdownlist>
        <asp:requiredfieldvalidator id="reqReason" runat="server" controltovalidate="drpReason" text="*" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td height="24">
        Contact
      </td>
      <td>
        <strong><em>Person Completing The Form</em></strong>
      </td>
      <td colspan="2">
        <strong><em>Designated School Contact</em></strong> (<asp:checkbox id="chkcontact" runat="server" autopostback="True" tabindex="30" OnCheckedChanged="chkcontact_CheckedChanged"></asp:checkbox>
        Check if same as person)
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Name</span>
      </td>
      <td>
        <asp:textbox id="txtFormPerson" runat="server" width="250px" tabindex="26"></asp:textbox>
        <asp:requiredfieldvalidator id="reqFormPerson" runat="server" controltovalidate="txtFormPerson" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator4" controltovalidate="txtFormPerson" validationexpression="[.\w\s',-]+" text="&bull;" errormessage="Person Completing Form Name - Invalid characters (only letters, numbers, spaces, commas, dashes, and single quotes allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
      <td colspan="2">
        <asp:textbox id="txtContactPerson" runat="server" width="250px" tabindex="31"></asp:textbox>
        <asp:requiredfieldvalidator id="reqContactPerson" runat="server" controltovalidate="txtContactPerson" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator12" controltovalidate="txtContactPerson" validationexpression="[.\w\s',-]+" text="&bull;" errormessage="Designated Contact Name - Invalid characters (only letters, numbers, spaces, commas, dashes, and single quotes allowed)<br>" runat="server" cssclass="RequiredFieldValidator" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Email</span>
      </td>
      <td>
        <asp:textbox id="txtFormEmail" runat="server" width="250px" tabindex="27"></asp:textbox>
        <asp:requiredfieldvalidator id="reqFormEmail" runat="server" controltovalidate="txtFormEmail" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator2" controltovalidate="txtFormEmail" validationexpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z0-9]{2,17})$" text="&bull;" errormessage="Person Completing Form Email - Invalid format<br>" runat="server" cssclass="RequiredFieldValidator" enableclientscript="False" />
      </td>
      <td colspan="2">
        <asp:textbox id="txtContactEmail" runat="server" width="250px" tabindex="32"></asp:textbox>
        <asp:requiredfieldvalidator id="reqContactEmail" runat="server" controltovalidate="txtContactEmail" text="*" cssclass="RequiredFieldValidator" />
        <asp:regularexpressionvalidator id="RegularExpressionValidator13" controltovalidate="txtContactEmail" validationexpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z0-9]{2,17})$" text="&bull;" errormessage="Designated Contact Email - Invalid format<br>" runat="server" cssclass="RequiredFieldValidator" enableclientscript="False" />
      </td>
    </tr>
    <tr>
      <td>
        <span style="margin-left: 10px;">Phone</span>
      </td>
      <td>
        <asp:textbox id="txtFormPhone" runat="server" maxlength="3" tabindex="28" Width="35px"></asp:textbox><b>-</b>
        <asp:TextBox ID="txtFormPhone_1" runat="server" maxlength="3" Width="35px"></asp:TextBox><b>-</b>
        <asp:TextBox ID="txtFormPhone_2" runat="server" maxlength="4" Width="35px"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1_txtFormPhone_digit" Display="Dynamic" runat="server" ErrorMessage="The Phone Number For Person Completing The Form - Invalid character.<br> " Text="&bull;" cssclass="RequiredFieldValidator" OnServerValidate="CustomValidator1_txtFormPhone_digit_ServerValidate"></asp:CustomValidator>

        <span style="margin-left: 10px;">Ext.</span>
        <asp:textbox id="txtFormPhoneExt" runat="server" columns="7" maxlength="7" tabindex="29"></asp:textbox>
      </td>
      <td colspan="2">
        <asp:textbox id="txtContactPhone" runat="server" maxlength="3" Width="35px" tabindex="33"></asp:textbox><b>-</b>
          <asp:TextBox ID="txtContactPhone_1" runat="server" maxlength="3" Width="35px"></asp:TextBox><b>-</b>
          <asp:TextBox ID="txtContactPhone_2" runat="server"  maxlength="4" Width="35px"></asp:TextBox>

          <asp:CustomValidator ID="CustomValidator_txtContactPhone_digit" runat="server" Display="Dynamic"  ErrorMessage="Designated School Contact - Invalid character.<br>" Text="&bull;" cssclass="RequiredFieldValidator" OnServerValidate="CustomValidator_txtContactPhone_digit_ServerValidate"></asp:CustomValidator>
        <span style="margin-left: 10px;">Ext.</span>
        <asp:textbox id="txtContactPhoneExt" runat="server" columns="7" maxlength="7" tabindex="34"></asp:textbox>
      </td>
    </tr>
    <tr>
      <td>
        Memo
      </td>
      <td colspan="3">
        <asp:textbox runat="server" id="txtMemo" columns="60" rows="4" textmode="MultiLine" tabindex="35"></asp:textbox>
      </td>
    </tr>
  </table>
  <p>
    <asp:button runat="server" id="btnBack" text="Back to List" causesvalidation="false" OnClick="btnBack_Click" />
    <asp:button runat="server" id="btnSubmit" text="Update School" tabindex="33" OnClick="btnSubmit_Click" />
    <!-- Commented out by A.T. on 10/07/2014 Cherwell Ticket 196595  -->
    <!--<asp:button runat="server" id="btnReset" text="Unlock Report" causesvalidation="false" /> -->
    <asp:button runat="server" id="btnDelete" text="Delete School" causesvalidation="false" />
    <input id="hdnSchoolYear" type="hidden" name="hdnSchoolYear" runat="server" />
    <input id="hdnSchType" type="hidden" name="hdnSchType" runat="server" />
    <input id="hdnCoName" type="hidden" name="hdnCoName" runat="server" />
    <input id="hdnCoCode" type="hidden" name="hdnCoCode" runat="server" />
    <input id="hdnDistName" type="hidden" name="hdnDistName" runat="server" />
    <input id="hdnDistCode" type="hidden" name="hdnDistCode" runat="server" />
    <input id="hdnSchoolName" type="hidden" name="hdnSchoolName" runat="server" />
    <input id="hdnSchCode" type="hidden" name="hdnSchCode" runat="server" />
    <input id="hdnSchoolPhone" type="hidden" name="hdnSchoolPhone" runat="server" />
    <input id="hdnSchAdmin" type="hidden" name="hdnSchAdmin" runat="server" />
    <input id="hdnSchEmail" type="hidden" name="hdnSchEmail" runat="server" />
    <input id="hdnSPACode" type="hidden" name="hdnSPACode" runat="server" />
    <input id="hdnPhysStreet" type="hidden" name="hdnPhysStreet" runat="server" />
    <input id="hdnPhysCity" type="hidden" name="hdnPhysCity" runat="server" />
    <input id="hdnPhysZip" type="hidden" name="hdnPhysZip" runat="server" />
    <input id="hdnMailStreet" type="hidden" name="hdnMailStreet" runat="server" />
    <input id="hdnMailCity" type="hidden" name="hdnMailCity" runat="server" />
    <input id="hdnMailZip" type="hidden" name="hdnMailZip" runat="server" />
    <input id="hdnContactPerson" type="hidden" name="hdnContactPerson" runat="server" />
    <input id="hdnContactEmail" type="hidden" name="hdnContactEmail" runat="server" />
    <input id="hdnContactPhone" type="hidden" name="hdnContactPhone" runat="server" />
    <input id="hdnContactPhoneExt" type="hidden" name="hdnContactPhoneExt" runat="server" />
    <input id="hdnFormPerson" type="hidden" name="hdnFormPerson" runat="server" />
    <input id="hdnFormEmail" type="hidden" name="hdnFormEmail" runat="server" />
    <input id="hdnFormPhone" type="hidden" name="hdnFormPhone" runat="server" />
    <input id="hdnFormPhoneExt" type="hidden" name="hdnFormPhoneExt" runat="server" />
    <input id="hdnKinderYesNo" type="hidden" name="hdnKinderYesNo" runat="server" />
    <input id="hdnReason" type="hidden" name="hdnReason" runat="server" />
    <input id="hdnMemo" type="hidden" name="hdnMemo" runat="server" />
    <input id="hdnIsCharter" type="hidden" name="hdnIsCharter" runat="server" />
    <input id="hdnIsComplete" type="hidden" name="hdnIsComplete" runat="server" />
    <input id="hdnSubmitDate" type="hidden" name="hdnSubmitDate" runat="server" />
    <input id="hdnSuperintendentName" type="hidden" name="hdnSuperintendentName" runat="server" />
    <input id="hdnSuperintendentEmail" type="hidden" name="hdnSuperintendentEmail" runat="server" />
    <input id="hdnSuperintendentPhone" type="hidden" name="hdnSuperintendentPhone" runat="server" />
    <input id="hdnLastResetDate" type="hidden" name="hdnLastResetDate" runat="server" />
    <input id="hdnLhdReviseDate" type="hidden" name="hdnLhdReviseDate" runat="server" />    
    <input id="hdnReviseDate" type="hidden" name="hdnReviseDate" runat="server" />
  </p>
  </form>
</body>
</html>

