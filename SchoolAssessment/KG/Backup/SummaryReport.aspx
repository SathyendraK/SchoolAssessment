<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryReport.aspx.cs" Inherits="SchoolAssessment.KG.SummaryReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title>
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
    <script>
        function calculateTextTotal() {
            //for example

            //var val0 = parseInt($("#txttotno").val());
 
            //var val1 = parseInt($("#txtAllimm").val());
            var val1 = document.getElementById('txtAllimm').value;
            var val2 = document.getElementById('txtPermMedExmp').value;
            var val3 = document.getElementById('txtBelExmp').value;

            //var val2 = parseInt($("#txtPermMedExmp").val());

            //if (val0 == "") val0 = 0; 
            if (val1 == "") val1 = 0; 
            if (val2 == "") val2 = 0;
            if (val3 == "") val3 = 0;

            $("#TextTotal").val(parseInt(val1) + parseInt(val2) + parseInt(val3));
    }
    </script>
  <!-- Added below by AT on 10/22/2014 to prevent flashing at autopostback="True" -->
  <meta http-equiv="Page-Enter" content="revealTrans(Duration=0,Transition=5)">
    <style type="text/css">
        .auto-style1 {
            width: 400px;
        }
        .auto-style2 {
            width: 11px;
        }
        .auto-style3 {
            height: 32px;
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
        <form id="Form1" method="post" runat="server" autocomplete="off">
        <center><img src="Images/OnStepOneImg.png" alt="School information:" /></center> <!-- 06/20/2016 by AT Show the status of steps -->
        <!--<h2><img src="Images/pleasecomplete.png" alt="Please complete the following form:" /></h2>-->
        <h2>Summary Report</h2>
        <asp:label id="ErrorMsg" runat="server"></asp:label>
        <asp:validationsummary id="valSum" displaymode="BulletList" headertext="PLEASE CORRECT THE FOLLOWING ERRORS: (Invalid fields marked with bullets.  If the value is zero, enter a 0.)" runat="server" cssclass="ValidationSummary" />
        <!-- AT starts here on 06/27/2016 -->
        <table width="800">
            <tr>
                <td colspan ="4" class="auto-style1"><b>School Name:</b> <asp:label id="lblSchName" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td colspan="4"><b>School Code:</b> &nbsp;<asp:label id="lblSchCode" runat="server"></asp:label></td>
           
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Total Number of Kindergarteners:</td>
                <td class="auto-style2"></td>
                <td>
                    <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px" onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator30" controltovalidate="txttotno" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator25" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="ActiveStudentsValidator" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="4">Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="4"><br /></td>
            </tr>
            <tr>
                <td class="auto-style1" style="color:#4169E1"><b>REQUIREMENTS MET</b> <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Reruirements'> </td>
                <td class="auto-style2"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td  class="indent">All required immunizations</td>
                <td><small>A</small></td>
                <td><asp:textbox id="txtAllimm" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator1" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator1" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
              </td>
                <td></td>
            </tr>
            <tr>
                <td colspan ="4"><br /></td>
            </tr>
            <tr>
                <td style="color:#4169E1"><b>REQUIREMENTS MET, BUT MISSING DOSES</b> <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Contents follows...'> </td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td><b>Unconditional Admission:</b></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td class="indent">Permanent Medical Exemption</td>
                <td><small>C</small></td>
                <td><asp:TextBox ID="txtPermMedExmp" TabIndex="3" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator2" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent">Personal Belief Exemption</td>
                <td><small>E</small></td>
                <td>
                     <asp:TextBox ID="txtBelExmp" TabIndex="4" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBelExmp" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtBelExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent">Others:</td>
               <td colspan="3"></td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F</small></td>
                <td>
                    <asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                     <td><small>F</small></td>
                <td>
                    <asp:TextBox ID="TextIndependentStudy" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TextIndependentStudy" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator13" ControlToValidate="TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Home-based Private School</td>
                     <td><small>F</small></td>
                <td>
                    <asp:TextBox ID="TextHomeBasedPrivate" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator14" ControlToValidate="TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td><b>Conditional Admission, need follow-up:</b></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td class="indent">Conditional Entrant</td>
                <td><small>B</small></td>
                <td>
                    <asp:TextBox ID="txtNoimm" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNoimm" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator4" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent">Temporary Medical Exemption</td>
                <td><small>D</small></td>
                <td>
                    <asp:TextBox ID="TextMedExmption" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextMedExmption" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator11" ControlToValidate="TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
             
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4"><br/></td>
            </tr>
            <tr>
                <td style="color: #4169E1"><b>REQUIREMENTS NOT MET, MISSING DOSES</b> <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Contents follows...'></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td class="indent" style="height: 32px">Overdue Doses</td>
                <td class="auto-style3"><small>G</small></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator3" ControlToValidate="TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="color: #4169E1"><b>TOTAL</b></td>
                <td></td>
                <td>
                    <asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="color: #4169E1"><b>MISSING DOSES BY VACCINE</b> <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Contents follows...'></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td colspan="4">Number of students (from B-G) that are missing doses of each vaccine:</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="missingDoses">Polio</td>
                            <td>
                                <asp:TextBox ID="txtPolio" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPolio" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator5" ControlToValidate="txtPolio" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator2" ControlToValidate="txtPolio" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4a should be less than or equal to the total number in category 4" runat="server" />
                            </td>
                            <td class="missingDosesRightSide">Hep B</td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtHepb" TabIndex="13" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtHepb" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator9" ControlToValidate="txtHepb" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator6" ControlToValidate="txtHepb" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4d should be less than or equal to the total number in category 4" runat="server" />

                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="missingDoses">DTP</td>
                            <td>
                                <asp:TextBox ID="txtDtp" TabIndex="14" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDtp" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator6" ControlToValidate="txtDtp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator3" ControlToValidate="txtDtp" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4b should be less than or equal to the total number in category 4" runat="server" />
                            </td>
                            <td class="missingDosesRightSide">Varicella</td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtVZV" TabIndex="15" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtVZV" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator10" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator7" ControlToValidate="txtVZV" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4e should be less than or equal to the total number in category 4" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="missingDoses">MMR</td>
                            <td>
                                <asp:TextBox ID="txtMMR2" TabIndex="16" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMMR2" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator8" ControlToValidate="txtMMR2" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator5" ControlToValidate="txtMMR2" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4c should be less than or equal to the total number in category 4" runat="server" />
                            </td>
                            <td class="missingDosesRightSide">Hib</td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="TextHib" runat="server" TabIndex="17" Columns="4" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="TextHib" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator15" ControlToValidate="TextHib" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hib - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="Comparevalidator1" ControlToValidate="TextHib" Type="Integer" ControlToCompare="txtNoimm" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten students entered in 4c should be less than or equal to the total number in category 4" runat="server" />

                </td>
                <td></td>
            </tr>
        </table>








            <!--Note: The total of lines 1+2+3+4 should equal NUMBER OF KINDERGARTEN STUDENTS ENROLLED THIS YEAR, shown above -->
            <asp:CustomValidator ID="CustomValidator1" OnServerValidate="KindergartenSum" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The total of lines 1+2+3+4+5 should equal NUMBER OF KINDERGARTEN STUDENTS ENROLLED THIS YEAR" runat="server" />
            <asp:CustomValidator ID="CustomValidator26" OnServerValidate="NoImmZeroesValidate" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="If category 4 is a non-zero value, 4a-4e must not be all zero" runat="server" />
            <asp:CustomValidator ID="CustomValidator8" OnServerValidate="NoImmMinimum" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The sum of 4a through 4e must be greater than or equal to 4" runat="server" />



 
           
        <p><span class="redbold">Your data will not be submitted unless you select 'Submit' and you see the next screen confirming your school information!</span></p>
        <p>
          <asp:button id="btnBack" runat="server" text="Back" causesvalidation="False" tabindex="25" OnClick="btnBack_Click"></asp:button>
          <asp:button id="btnNext" runat="server" text="Next" tabindex="25" OnClick="btnNext_Click"></asp:button>
          <asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" tabindex="26" OnClick="btnCancel_Click"></asp:button>
        </p> 
        <p>For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <hr />
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
