<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportContactInfo.aspx.cs" Inherits="SchoolAssessment.KG.ReportContactInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment Report Contact Info</title>
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
  <!-- Added below by AT on 10/22/2014 to prevent flashing at autopostback="True" -->
  <meta http-equiv="Page-Enter" content="revealTrans(Duration=0,Transition=5)">
    <style type="text/css">
        .auto-style2 {
            height: 26px;  
            
        }
        .auto-style4 {
            height: 26px;
            width: 112px;
        }

         .auto-style7 {
            width: 114px;
        }
        .auto-style8 {
            width: 100%;
            height: 233px;
        }
        .auto-style9 {
            width: 827px;
        }
        .auto-style10 {
            height: 26px;
            width: 142px;
        }
    </style>
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
          <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15</div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
        <div id="help"><a target="_blank" href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf ">Help</a></div>
      </div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <center><img src="Images/OnStepOneImg.png" alt="School information:" /></center> <!-- 06/20/2016 by AT Show the status of steps -->
        <form id="Form1" method="post" runat="server" autocomplete="off">
        <h2>
          <img src="Images/pleasecomplete.png" alt="Please complete the following form:" /></h2>
        <asp:label id="ErrorMsg" runat="server"></asp:label>
        <asp:validationsummary id="valSum" displaymode="BulletList" headertext="PLEASE CORRECT THE FOLLOWING ERRORS: (Invalid fields marked with bullets.  If the value is zero, enter a 0.)" runat="server" cssclass="ValidationSummary" />
        <table class="auto-style9">
          <tr>
            <td>
             
              <table cellspacing="0" width="100%">
                <tr>
                  <td>Information updated on this page is for reporting purposes only.  All updates to school be routed through the California School Directory - Summiting Corections to be reflected.<br /><br />
                  </td>

                </tr>
              </table>

<table cellspacing="0" class="auto-style8">
    <tr>
        <td class="auto-style4">
            School Code:
        </td>
        <td colspan="6">
            <asp:Label ID="lblSchCode" runat="server"></asp:Label>
        </td>
        <td colspan="6"></td>
    </tr>
    <tr>
        <td class="auto-style4">
            School Name:
        </td>
        <td colspan="6" class="auto-style2">
            <asp:Label ID="lblSchName" runat="server"></asp:Label>
        </td>
        <td colspan="6" class="auto-style2"></td>
    </tr>
    <tr><td colspan="8"><br /></td></tr>
    <tr>
        <td colspan="4" class="auto-style2"><strong>School Staff Member Completing This Form</strong></td>
        <td colspan="4" class="auto-style2"><strong>Designated School Contact</strong>
        <span class="small">(<asp:checkbox id="chkcontact" runat="server" autopostback="True" tabindex="19"></asp:checkbox>Check if same person)</span>
        </td>
    </tr>
    <tr>
		<td class="auto-style4">Name:</td>
		<td colspan="3">
                          <asp:textbox id="txtStaffPrsn" runat="server" width="220px" tabindex="14" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator26" controltovalidate="txtStaffPrsn" text="&bull;" errormessage="School Staff Member Completing This Form Name - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator1" controltovalidate="txtStaffPrsn" validationexpression="[.\w\s',-]+" text="&bull;" errormessage="School Staff Member Completing This Form Name - Invalid characters (only letters, numbers, spaces, commas, dashes, and single quotes allowed)" runat="server" cssclass="RequiredFieldValidator" />
        </td>
		<td>Name:</td>
		<td colspan="3">
                          <asp:textbox id="txtfcperson" runat="server" width="220px" tabindex="20" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator28" controltovalidate="txtfcperson" text="&bull;" errormessage="Designated School Contact Name - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator6" controltovalidate="txtfcperson" validationexpression="[.\w\s',-]+" text="&bull;" errormessage="Designated School Contact Name - Invalid characters (only letters, numbers, spaces, commas, dashes, and single quotes allowed)" runat="server" cssclass="RequiredFieldValidator" />
                        </td>
	</tr>
	<tr>
		<td class="auto-style4">Email:</td>
		<td colspan="3">
                          <asp:textbox id="txtmail" runat="server" width="220px" tabindex="15" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator12" controltovalidate="txtmail" text="&bull;" errormessage="School Staff Member Completing This Form Email - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator2" controltovalidate="txtmail" validationexpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" text="&bull;" errormessage="School Staff Member Completing This Form Email - Invalid format" runat="server" cssclass="RequiredFieldValidator" enableclientscript="False" />
        </td>
		<td>Email:</td>
		<td colspan="3">
                          <asp:textbox id="txtfcemail" width="220px" runat="server" tabindex="21" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator11" controltovalidate="txtfcemail" text="&bull;" errormessage="Designated School Contact Email - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator5" controltovalidate="txtfcemail" validationexpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" text="&bull;" errormessage="Designated School Contact Email - Invalid format" runat="server" cssclass="RequiredFieldValidator" enableclientscript="False" />
        </td>
	</tr>
	<tr>
		<td class="auto-style4">Confirm Email:</td>
		<td colspan="3">
                          <asp:textbox id="txtconfirmmail" runat="server" width="220px" tabindex="16" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator15" controltovalidate="txtconfirmmail" text="&bull;" errormessage="School Staff Member Completing This Form Confirm Email - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:comparevalidator id="Comparevalidator1" controltovalidate="txtconfirmmail" type="String" controltocompare="txtmail" operator="Equal" display="Dynamic" cssclass="RequiredFieldValidator" text="&bull;" errormessage="School Staff Member Completing This Form Confirm Email - Does not match email" runat="server" />
        </td>
		<td>Confirm Email:</td>
		<td colspan="3">
                          <asp:textbox id="txtfccemail" width="220px" runat="server" tabindex="22" maxlength="50"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator16" controltovalidate="txtfccemail" text="&bull;" errormessage="Designated School Contact Confirm Email - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:comparevalidator id="Comparevalidator2" controltovalidate="txtfccemail" type="String" controltocompare="txtfcemail" operator="Equal" display="Dynamic" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Designated School Contact Confirm Email - Does not match email" runat="server" />
        </td>
	</tr>
	<tr>
		<td class="auto-style4">Phone:</td>
		<td class="auto-style4" >
                          <asp:textbox id="txtStaffPhNo" maxlength="12" runat="server" width="100px" tabindex="17" ></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator27" controltovalidate="txtStaffPhNo" text="&bull;" errormessage="School Staff Member Completing This Form Phone - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator3" controltovalidate="txtStaffPhNo" validationexpression="^\d{3}-\d{3}-\d{4}$" text="&bull;" errormessage="School Staff Member Completing This Form Phone - Invalid format (must be xxx-xxx-xxxx)" runat="server" cssclass="RequiredFieldValidator" />
        </td>
		<td class="auto-style8">Ext:</td>
        <td >
                          <asp:textbox id="txtStaffPhNoExt" maxlength="4" columns="4" runat="server" tabindex="18" width="40px"></asp:textbox>
        </td>
		<td class="auto-style4">Phone:</td>
		<td class="auto-style4">
                          <asp:textbox id="txtfcNumber"  runat="server" maxlength="12" tabindex="23" width="100px"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator29" controltovalidate="txtfcNumber" text="&bull;" errormessage="Designated School Contact Phone - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:regularexpressionvalidator id="RegularExpressionValidator4" controltovalidate="txtfcNumber" validationexpression="^\d{3}-\d{3}-\d{4}$" text="&bull;" errormessage="Designated School Contact Phone - Invalid format (must be xxx-xxx-xxxx)" runat="server" cssclass="RequiredFieldValidator" />
        </td>
		<td class="auto-style8" >Ext:</td>
        <td class="auto-style4">
                          <asp:textbox id="txtfcNumberExt" runat="server" maxlength="4" columns="4" tabindex="24" width="40px"></asp:textbox>
         </td>
		
	</tr>
</table>

        <p><span class="redbold">Your data will not be submitted unless you select 'Submit' and you see the next screen confirming your school information!</span></p>
        <p>
          <asp:button id="btnCancel" runat="server" text="Back" causesvalidation="False" tabindex="26" OnClick="btnCancel_Click"></asp:button>
          <asp:button id="Button1" runat="server" text="Next" tabindex="25" OnClick="Button1_Click"></asp:button> 

        </p> 
        <hr />
        <p>For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="http://www.shotsforschool.org">
          <img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation">You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
        <input id="hdnSchCode" type="hidden" name="hdnSchCode" runat="server" />
        <input id="hdnTotNo" type="hidden" name="hdnTotNo" runat="server" />
        <input id="hdnAllImm" type="hidden" name="hdnAllImm" runat="server" />
        <input id="hdnMedExmp" type="hidden" name="hdnMedExmp" runat="server" />
        <input id="hdnPerExmp" type="hidden" name="hdnPerExmp" runat="server" />
        <input id="hdnNoImm" type="hidden" name="hdnNoImm" runat="server" />
        <input id="hdnPolio" type="hidden" name="hdnPolio" runat="server" />
        <input id="hdnDTP" type="hidden" name="hdnDTP" runat="server" />
        <input id="hdnMMR1" type="hidden" name="hdnMMR1" runat="server" />
        <input id="hdnMMR2" type="hidden" name="hdnMMR2" runat="server" />
        <input id="hdnHepB" type="hidden" name="hdnHepB" runat="server" />
        <input id="hdnVZV" type="hidden" name="hdnVZV" runat="server" />
        <input id="hdnFormPerson" type="hidden" name="hdnFormPerson" runat="server" />
        <input id="hdnFormEmail" type="hidden" name="hdnFormEmail" runat="server" />
        <input id="hdnFormPhone" type="hidden" name="hdnFormPhone" runat="server" />
        <input id="hdnFormPhoneExt" type="hidden" name="hdnFormPhoneExt" runat="server" />
        <input id="hdnContactPerson" type="hidden" name="hdnContactPerson" runat="server" />
        <input id="hdnContactEmail" type="hidden" name="hdnContactEmail" runat="server" />
        <input id="hdnContactPhone" type="hidden" name="hdnContactPhone" runat="server" />
        <input id="hdnContactPhoneExt" type="hidden" name="hdnContactPhoneExt" runat="server" />
        <input id="hdnPassword" type="hidden" name="hdnPassword" runat="server" />
        <input id="hdnSubmitDate" type="hidden" name="hdnSubmitDate" runat="server" />
        <input id="hdnIsComplete" type="hidden" name="hdnIsComplete" runat="server" />
        <input id="hdnReviseDate" type="hidden" name="hdnReviseDate" runat="server" />
        <input id="hdnLhdReviseDate" type="hidden" name="hdnReviseDate" runat="server" />
            <input id="hdnTxtPreJanuaryExmpt" type="hidden" name="hdnTxtPreJanuaryExmpt" runat="server" />
            <input id="hdnTxtHealthCareExmpt" type="hidden" name="hdnTxtHealthCareExmpt" runat="server" />
            <input id="hdnTxtReligiousExmpt" type="hidden" name="hdnTxtReligiousExmpt" runat="server" />
            <input id="hdnEnrolledButNotAttending" type="hidden" name="hdnEnrolledButNotAttending" runat="server" />
        </form>
      </div>
    </div>
    <div id="footer-wrap">
      <div id="footer">
      </div>
    </div>
  </div>
</body>
</html>
