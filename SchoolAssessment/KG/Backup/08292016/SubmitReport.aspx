<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitReport.aspx.cs" Inherits="SchoolAssessment.KG.SubmitReport" %>

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
            // Calculate TextTotal in the client-side.  Server side takes too much time and user weren't happy. By AT 07/01/2016
            var val1 = document.getElementById('txtAllimm').value;
            var val2 = document.getElementById('txtPermMedExmp').value;
            var val3 = document.getElementById('txtBelExmp').value;
            var val4 = document.getElementById('TextIEPServices').value;
            var val5 = document.getElementById('TextIndependentStudy').value;
            var val6 = document.getElementById('TextHomeBasedPrivate').value;
            var val7 = document.getElementById('txtNoimm').value;
            var val8 = document.getElementById('TextMedExmption').value;
            var val9 = document.getElementById('TextEnrolledButNotAttending').value;

            if (val1 == "") val1 = 0; 
            if (val2 == "") val2 = 0;
            if (val3 == "") val3 = 0;
            if (val4 == "") val4 = 0;
            if (val5 == "") val5 = 0;
            if (val6 == "") val6 = 0;
            if (val7 == "") val7 = 0;
            if (val8 == "") val8 = 0;
            if (val9 == "") val9 = 0;

            $("#TextTotal").val(parseInt(val1) + parseInt(val2) + parseInt(val3) + parseInt(val4) + parseInt(val5) + parseInt(val6) + parseInt(val7) + parseInt(val8) + parseInt(val9));
            $("#TextMissingDosesTotal").val(parseInt(val2) + parseInt(val3) + parseInt(val4) + parseInt(val5) + parseInt(val6) + parseInt(val7) + parseInt(val8) + parseInt(val9));
            $("#TxtOthersTotal").val(parseInt(val4) + parseInt(val5) + parseInt(val6));

            //document.getElementById("HiddenTextTotal").value = $("#TextTotal").val;

        }
    </script>
    <script type = "text/javascript">

        function Confirm() {

            var confirm_value = document.createElement("INPUT");

            confirm_value.type = "hidden";

            confirm_value.name = "confirm_value";

            if (confirm("The report will not submit if there is error with your numbers. However, once you submit, you may still update your report until reporting closes. Simply log back in.")) {

                confirm_value.value = "Yes";

            } else {

                confirm_value.value = "No";

            }

            document.forms[0].appendChild(confirm_value);

        }

    </script>

  <!-- Added below by AT on 10/22/2014 to prevent flashing at autopostback="True" -->
  <meta http-equiv="Page-Enter" content="revealTrans(Duration=0,Transition=5)">
    </head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
          <a href="http://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></a></div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
       
        <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions  <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <a href="http://www.cairweb.org/calkidshots/KSeventhFAQs.pdf" target="_blank" alt="FAQs" title="FAQs"> FAQs <img src="Images/Icon_instr.png" width="15" /> </a> | 
               Worksheet<a href="http://www.cairweb.org/calkidshots/pm236a.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank"> <img src="Images/Icon_adobe.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> | 
               <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
             </div>
      </div>
        <div id="duedate">Reports due October 15</div>
    </div>
    
    <div id="content-wrap">
      <div id="content" >
        <br />
        <center><img src="Images/Step_03.png" alt="School information:" /></center>
        <h2><img src="Images/hdr_SubmitReport.png" alt="Please confirm School information:" /></h2>
        <asp:label id="ErrorMsg" runat="server"></asp:label>
        <asp:validationsummary id="valSum" displaymode="BulletList" headertext="Please Correct the following errors: " runat="server" cssclass="ValidationSummary" />
        <!-- AT starts here on 06/27/2016 -->
        <table width="825">
            <tr>
                <td colspan ="5"><b>School Name:</b> <asp:label id="lblSchName" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td colspan="5"><b>School Code:</b> &nbsp;<asp:label id="lblSchCode" runat="server"></asp:label></td>
           
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td width="260"><b>Total Number of Kindergarteners:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px" onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator30" controltovalidate="txttotno" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator25" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="ActiveStudentsValidator" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td align="left" width="85"></td>
                <td width="390"></td>
            </tr>
            <tr>
                <td colspan="5">Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr><td colspan="5"><img src="Images/Title_UncondAdm.png" width="217" height="15"/></td></tr>
            <tr>
                <td colspan="5" class="indent"><b>Requirements Met</b> </td>
              
            </tr>
            <tr>
                <td  class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has all immunizations required for their grade.'> All required immunizations</td>
                <td><small>A</small></td>
                <td><asp:textbox id="txtAllimm" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator1" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator1" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
              </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan ="5"><br /><hr /></td>
            </tr>
            <tr>
                <td colspan="3" class="indent"><b>Requirements Met, But Missing Doses</b> </td>
                <td></td>
                <td><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Please account for each student "missing doses by vaccine". &#13;&#13;Each student should be missing at least one dose. &#13;&#13;Therefore, the total number of Polio, DTP, MMR, HepB and VAR should be at least equal to the number of students in that group.'> <b>Missing Doses By Vaccine </b></td>
            </tr>

            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a licensed physician’s written statement of a permanent medical exemption for missing shot(s) and immunization records with dates for all required shots not exempted. '> Permanent Medical Exemption</td>
                <td><small>C</small></td>
                <td><asp:TextBox ID="txtPermMedExmp" TabIndex="3" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator2" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="TextMissingDosesTotal" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                &nbsp;<asp:CustomValidator ID="CustomValidator8" OnServerValidate="NoImmMinimum" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The sum of missing doses by vaccine must be greater than or equal to sum of B-G" runat="server" />
                <asp:CustomValidator ID="CustomValidator26" OnServerValidate="NoImmZeroesValidate" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Please enter missing doses by vaccine since you have entered students in B-G." runat="server" />
                Students (from B-G) that are missing</td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Entered Transitional Kindergarten with a valid personal beliefs exemption (PBE) for missing shot(s) that was signed within 6 months prior to entry and filed before January 1, 2016 and immunization records with dates for all required shots not exempted, or The PBE must have been filed before January 1, 2016 and is only valid for the current grade span (TK/K through 6th or 7th through 12th grade). For complete details, visit ShotsforSchool.org.'> Personal Belief Exemption</td>
                <td><small>E</small></td>
                <td colspan="2">
                     <asp:TextBox ID="txtBelExmp" TabIndex="4" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBelExmp" Text="&bull;" ErrorMessage="Number of kindergarteners who have a Personal Belief Exemption’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtBelExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Personal Belief’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                <i><small>TK students only</small></i>
                </td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td> doses. Please indicate the total number of students  </td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14 alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their required immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Others:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="TxtOthersTotal" MaxLength="4" Columns="4"  Width="50px" runat="server" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
                <td></td>
                <td>missing each vaccines:</td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <td>
                     <table>
                        <tr>
                            <td class="missingDoses">Polio</td>
                            <td width="70px">
                                <asp:TextBox ID="txtPolio" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPolio" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator5" ControlToValidate="txtPolio" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator2" ControlToValidate="txtPolio" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Polio doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Hep B</td>
                            <td width="70px">
                    <asp:TextBox ID="txtHepb" TabIndex="13" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtHepb" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator9" ControlToValidate="txtHepb" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator6" ControlToValidate="txtHepb" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Hepatitis B doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                     <td><small>F2</small></td>
                <td>
                    <asp:TextBox ID="TextIndependentStudy" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TextIndependentStudy" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator13" ControlToValidate="TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
                <td><table>
                        <tr>
                            <td class="missingDoses">DTP</td>
                            <td width="70px">
                                <asp:TextBox ID="txtDtp" TabIndex="14" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDtp" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator6" ControlToValidate="txtDtp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator3" ControlToValidate="txtDtp" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing DTaP doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Varicella</td>
                            <td width="70px"> <asp:TextBox ID="txtVZV" TabIndex="15" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtVZV" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator10" ControlToValidate="TextMissingDosesTotal" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator7" ControlToValidate="txtVZV" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Varicella doses entered should be less than or equal to the total sum between B-G" runat="server" />
                </td>
                        </tr>
                    </table></td>
            </tr>
            <tr>
                <td class="indent2">Home-based Private School</td>
                     <td><small>F3</small></td>
                <td>
                    <asp:TextBox ID="TextHomeBasedPrivate" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator14" ControlToValidate="TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <td> <table>
                        <tr>
                            <td class="missingDoses">MMR</td>
                            <td width="120px">
                                <asp:TextBox ID="txtMMR2" TabIndex="16" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMMR2" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator8" ControlToValidate="txtMMR2" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator5" ControlToValidate="txtMMR2" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing MMR doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="40px">&nbsp;</td>
                            <td class="missingDosesRightSide"></td>
                            <td></td>
                        </tr>
                    </table></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3"><img src="Images/Title_CondAdm.png" width="331" height="15"/></td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Is missing a dose(s) in a series, but the next dose is not due yet. This means the child has received at least one dose in a series and the deadline for the next dose has not passed.'> Conditional Entrant</td>
                <td><small>B</small></td>
                <td>
                    <asp:TextBox ID="txtNoimm" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNoimm" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator4" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has a temporary medical exemption to certain vaccine(s) and has submitted immunization records for vaccines not exempted. The statement must indicate which immunization(s) must be postponed and when the child can be immunized.'> Temporary Medical Exemption</td>
                <td><small>D</small></td>
                <td>
                    <asp:TextBox ID="TextMedExmption" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextMedExmption" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator11" ControlToValidate="TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
             
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="5"><br/></td>
            </tr>
            <tr>
                <td colspan="5"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /> </td>
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Does not fit one of the previous categories and is subject to exclusion.'> Overdue Doses</td>
                <td ><small>G</small></td>
                <td >
                    <asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator3" ControlToValidate="TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td ></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>
                    
                    <asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                    <asp:CustomValidator ID="CustomValidator27" runat="server" OnServerValidate="KindergartenSum" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Does not match number of students.  Re-enter information."></asp:CustomValidator>
                   

                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>








            


 
           
        <p><small>By submitting this form and the the electronic signature attached hereto, I declare under penalty of perjury and the laws of the Stte of California that I am an authorized contact of the school and the information contained herein is true, accurate, and complete.</small></p>
        <p>
            <asp:ImageButton ID="ImgBtnBack" Enabled="true" ImageUrl="images/btn1_back.png" runat="server" causesvalidation="False" tabindex="25" OnClick="ImgBtnBack_Click" />
            <asp:ImageButton ID="ImgBtnSubmit" runat="server"  Enabled="true" ImageUrl="images/btn7_submitreport.png" tabindex="27" OnClick="ImgBtnSubmit_Click" OnClientClick = "Confirm()" />
        </p> 
        <p>&nbsp;</p>
        <hr />
            <br />
            For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a><p><a href="http://www.shotsforschool.org">
          <img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation"><i>Session will automatically time out in 30 minutes.</i><br />You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
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
            <input id="HdnConfirmSubmit" type="hidden" name="HdnConfirmSubmit" runat="server" />
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
