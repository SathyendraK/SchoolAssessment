<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitReport.aspx.cs" Inherits="SchoolAssessment._7th.SubmitReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California 7th Grade Immunization Assessment</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
  <!--<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>-->
  <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
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
            var val4 = document.getElementById('TextIEPServices').value;
            var val5 = document.getElementById('TextIndependentStudy').value;
            var val6 = document.getElementById('TextHomeBasedPrivate').value;
            var val8 = document.getElementById('TextMedExmption').value;
            var val9 = document.getElementById('TextEnrolledButNotAttending').value;

            // Varicella
            var val1_v = document.getElementById('V_txtAllimm').value;
            var val2_v = document.getElementById('V_txtPermMedExmp').value;
            var val4_v = document.getElementById('V_TextIEPServices').value;
            var val5_v = document.getElementById('V_TextIndependentStudy').value;
            var val6_v = document.getElementById('V_TextHomeBasedPrivate').value;
            var val8_v = document.getElementById('V_TextMedExmption').value;
            var val9_v = document.getElementById('V_TextEnrolledButNotAttending').value;
            var val10_v = document.getElementById('V_MDMO_PermMedExmp').value;
            var val11_v = document.getElementById('V_ConditionalNotDue').value;

            //var val3 = document.getElementById('txtBelExmp').value;
            //var val7 = document.getElementById('txtNoimm').value;

            if (val1 == "") val1 = 0; 
            if (val2 == "") val2 = 0;
            if (val4 == "") val4 = 0;
            if (val5 == "") val5 = 0;
            if (val6 == "") val6 = 0;
            if (val8 == "") val8 = 0;
            if (val9 == "") val9 = 0;
            if (val1_v == "") val1_v = 0;
            if (val2_v == "") val2_v = 0;
            if (val4_v == "") val4_v = 0;
            if (val5_v == "") val5_v = 0;
            if (val6_v == "") val6_v = 0;
            if (val8_v == "") val8_v = 0;
            if (val9_v == "") val9_v = 0;
            if (val10_v == "") val10_v = 0;
            if (val11_v == "") val11_v = 0;

            //if (val3 == "") val3 = 0;
            //if (val7 == "") val7 = 0;

            $("#TextTotal").val(parseInt(val1) + parseInt(val2) + parseInt(val4) + parseInt(val5) + parseInt(val6) + parseInt(val8) + parseInt(val9));
            $("#V_TextTotal").val(parseInt(val1_v) + parseInt(val2_v) + parseInt(val4_v) + parseInt(val5_v) + parseInt(val6_v) + parseInt(val8_v) + parseInt(val9_v) + parseInt(val10_v) + parseInt(val11_v));
            $("#TextMissingDosesTotal").val(parseInt(val2) + parseInt(val4) + parseInt(val5) + parseInt(val6) +  parseInt(val8) + parseInt(val9));
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
          <a href="https://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/hdr_7th_2.jpg" alt="7th Grade Immunization Assessment" /></a></div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
       
        <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="https://www.shotsforschool.org/reporting/7th/" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions  <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <a href="https://www.shotsforschool.org/reporting/kindergarten/faqs/" target="_blank" alt="FAQs" title="FAQs"> FAQs <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <!--Worksheet<a href="http://cairweb.org/calkidshots/PM236a.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://cairweb.org/calkidshots/PM236a.pdf" target="_blank"> <img src="Images/Icon_adobe.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> | -->
               Worksheet<a href="http://eziz.org/SA/IMM1272.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>
                    <a href="http://eziz.org/SA/IMM1272.pdf" target="_blank"> <img src="Images/Icon_pdf.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> |
                   <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
             </div>
      </div>
        <div id="duedate">Reporting is due January 31, 2022</div>
    </div>
    
    <div id="content-wrap">
      <div id="content" >
        <br />
        <center><img src="Images/step_7_3.png" alt="School information:" /></center>
        <h2><img src="Images/hdr_submit7th.jpg" alt="Please confirm School information:" /></h2>
        <asp:label id="ErrorMsg" runat="server"></asp:label>
        <asp:validationsummary id="valSum" displaymode="BulletList" headertext="Please correct the following errors: " runat="server" cssclass="ValidationSummary" />
        <!-- AT starts here on 06/27/2016 -->
        <table width="825">
            <tr>
                <td colspan ="7"><b>School Name:</b> <asp:label id="lblSchName" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td colspan="7"><b>School Code:</b> &nbsp;<asp:label id="lblSchCode" runat="server"></asp:label></td>
           
            </tr>
            <tr>
                <td colspan="7"><b>Status:</b>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
            <tr>
                <td width="450"><b>Total Number of 7th Grade Students:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px" onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator30" controltovalidate="txttotno" text="&bull;" errormessage="Number of 7th grade students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator25" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 7th grade students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="ActiveStudentsValidator" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="If your school does not have students enrolled this year, please go back to step 1, School Information to report no students are enrolled and why." runat="server" display="Dynamic" />
                        
                </td>
                <td width="30"></td>
                <td width="10"></td>
                <td width="80" align="left" ></td>
                <td width="165"></td>
            </tr>
            <tr>
                <td colspan="7">Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="7"><br /></td>
            </tr>
            <tr><td colspan="7"><img src="Images/Title_UncondAdm.png" width="217" height="15"/></td></tr>
            <tr>
                <td class="indent"><b>Requirements Met</b> </td>
                <td></td>
                <td><b>Varicella</b></td>
                <td></td>
                <td></td>
                <td><strong>Tdap</strong></td>
                <td></td>
               
              
            </tr>
            <tr>
                <td  class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has a single dose of TDaP (or DTP/DTaP given on or after the 7th birthday.)'> All Required Vaccine Doses</td>
                <td><small>A</small></td>
                <td>
                    <asp:TextBox ID="V_txtAllimm" runat="server"  maxlength="4" Width="50px"  onkeyup="calculateTextTotal();" tabindex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" controltovalidate="V_txtAllimm" text="&bull;" errormessage="Number of 7th graders with Varicella doses - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" ></asp:RequiredFieldValidator>
                   </td>
                <td></td>
                <td><small>A</small></td>
                <td><asp:textbox id="txtAllimm" tabindex="11" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator1" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of 7th graders with Tdap doses - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator1" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 7th graders with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan ="7"><br /><!--<hr />--></td>
            </tr>
            <tr>
                <td colspan="4" class="indent"><b>Requirements Met, But Missing Doses</b> </td>
                <td colspan="3"></td>
                
            </tr>
            <tr>
                 <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a licensed physician’s written statement of a permanent medical exemption for missing shot(s) and immunization records with dates for all required shots not exempted.'> Permanent Medical Exemption</td>
                 <td colspan="6"></td>
            </tr>

            <tr>
                <td class="indent3"> Medical reason other than varicella disease</td>
                <td><small>C1</small></td>
                <td>
                    <asp:TextBox ID="V_txtPermMedExmp" Columns="4" runat="server" MaxLength="4" Width="50px" onkeyup="calculateTextTotal();" tabindex="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="V_txtPermMedExmp" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" ></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="V_txtPermMedExmp"  MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" ></asp:RangeValidator>
                </td>
                <td></td>
                <td><small>C1</small></td>
                <td><asp:TextBox ID="txtPermMedExmp" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td></td>
            </tr>
			<tr>
                <td class="indent3"> MD/DO verification of varicella disease</td>
                <td><small>C2</small></td>
                <td>
                    <asp:TextBox ID="V_MDMO_PermMedExmp" Columns="4" runat="server" MaxLength="4" Width="50px" onkeyup="calculateTextTotal();" tabindex="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="V_MDMO_PermMedExmp" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator4" ControlToValidate="V_MDMO_PermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>                
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
			</tr>			
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14 alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their TDaP immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Other</td>
                <td></td>
                <td>
                    <!--<asp:TextBox ID="TxtOthersTotal" MaxLength="4" Columns="4"  Width="50px" runat="server" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>-->
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent3">IEP Services</td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="V_TextIEPServices" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();" tabindex="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="V_TextIEPServices" Text="&bull;" ErrorMessage="Number of 7th graders with IEP Service Exemptions - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator5" ControlToValidate="V_TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with IEP Service Exemptions - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>                
                </td>
                <td></td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="TextIEPServices" TabIndex="13" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent3">Independent Study</td>
                     <td><small>F2</small></td>
                <td>
                    <asp:TextBox ID="V_TextIndependentStudy" runat="server"  MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();" tabindex="6" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="V_TextIndependentStudy" Text="&bull;" ErrorMessage="Number of 7th graders with Independent Study Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator6" ControlToValidate="V_TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Independent StudyExemptions to any immunizations - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>
                    </td>
                <td></td>
                <td><small>F2</small></td>
                <td><asp:TextBox ID="TextIndependentStudy" TabIndex="14" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TextIndependentStudy" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘Independent Study’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator13" ControlToValidate="TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘Independent Study’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent3">Home-Based Private School</td>
                     <td><small>F3</small></td>
                <td>
                    <asp:TextBox ID="V_TextHomeBasedPrivate" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();" tabindex="7"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="V_TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator8" ControlToValidate="V_TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>
                    </td>
                <td></td>
                <td><small>F3</small></td>
                <td><asp:TextBox ID="TextHomeBasedPrivate" TabIndex="15" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator14" ControlToValidate="TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4"><img src="Images/Title_CondAdm.png" width="331" height="15"/></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has a temporary medical exemption to certain vaccine(s) and has submitted immunization records for vaccines not exempted. The statement must indicate which immunization(s) must be postponed and when the child can be immunized.'> Conditional-Missing Doses Not Currently Due</td>
                <td><small>B</small></td>
                <td>
                    <asp:TextBox ID="V_ConditionalNotDue" runat="server"  maxlength="4" Width="50px" onkeyup="calculateTextTotal();" tabindex="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="V_ConditionalNotDue" Text="&bull;" ErrorMessage="Number of 7th graders with Conditional-Missing Doses Not Currently Due - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator9" ControlToValidate="V_ConditionalNotDue" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Conditional-Missing Doses Not Currently Due - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                 <td colspan="4" class="indentInfoImg"><small>Varicella only</small></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has a temporary medical exemption to certain vaccine(s) and has submitted immunization records for vaccines not exempted. The statement must indicate which immunization(s) must be postponed and when the child can be immunized.'> Temporary Medical Exemption</td>
                <td><small>D</small></td>
                <td>
                    <asp:TextBox ID="V_TextMedExmption" runat="server" Columns="4" MaxLength="4" Width="50px" onkeyup="calculateTextTotal();" tabindex="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="V_TextMedExmption" Text="&bull;" ErrorMessage="Number of 7th graders with Temporary Medical Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator15" ControlToValidate="V_TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Temporary Medical Exemption - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>
                </td>
                <td></td>
                <td><small>D</small></td>
                <td><asp:TextBox ID="TextMedExmption" TabIndex="16" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextMedExmption" Text="&bull;" ErrorMessage="Number of 7th graders who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator11" ControlToValidate="TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7"><br/></td>
            </tr>
            <tr>
                <td colspan="7"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /> </td>
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Enrolled but not attending, does not fit one of the previous categories and is subject to exclusion.'> Overdue-Needs Doses Now</td>
                <td ><small>G</small></td>
                <td >
                    <asp:TextBox ID="V_TextEnrolledButNotAttending" Columns="4" runat="server" MaxLength="4" Width="50px" onkeyup="calculateTextTotal();" tabindex="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="V_TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of 7th graders with Overdue-Needs Doses Now - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator10" ControlToValidate="V_TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders with Overdue-Needs Doses Now - Numbers only" runat="server" Display="Dynamic"></asp:RangeValidator>
                    </td>
                <td ></td>
                <td ><small>G</small></td>
                <td><asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="17" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of 7th graders who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator3" ControlToValidate="TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of 7th graders who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                 <td colspan="4" class="indentInfoImg"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr><td colspan="7"><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td> 
                    <asp:TextBox ID="V_TextTotal" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="CustomValidator1_ServerValidate" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The total number of students with Varicella do not match with total number of students.  Re-enter Varicella information."></asp:CustomValidator>
                </td>
                <td></td>
                <td></td>
                <td>
                    
                    <asp:TextBox ID="TextTotal"  Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                    <asp:CustomValidator ID="CustomValidator27" runat="server" OnServerValidate="KindergartenSum" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The total number of students with Tdap do not match with total number of students.  Re-enter Tdap information."></asp:CustomValidator>
                   

               </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
        </table>








            


 
           
        <p><small>By submitting this form and the electronic signature attached hereto, I declare under penalty of perjury and the laws of the State of California that I am an authorized contact of the school and the information contained herein is true, accurate, and complete.</small></p>
        <p>
            <asp:ImageButton ID="ImgBtnBack" Enabled="true" ImageUrl="images/btn1_back.png" runat="server" causesvalidation="False" tabindex="18" OnClick="ImgBtnBack_Click" />
            <asp:ImageButton ID="ImgBtnSubmit" runat="server"  Enabled="true" ImageUrl="images/btn4_next.png" tabindex="19" OnClick="ImgBtnSubmit_Click" /><!--OnClientClick = "Confirm()"  -->
        </p> 
        <p>&nbsp;</p>
        <hr />
            <br />
            For questions about assessment, contact your <a href="https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx" target="_blank">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a><p><a href="https://www.shotsforschool.org">
          <img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation"><i>Session will automatically time out in 20 minutes.</i><br />You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
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
        <input id="hdnIEPService" type="hidden" name="hdnIEPService" runat="server" />
        <input id="hdnIndependentStudy" type="hidden" name="IndependentStudy" runat="server" />
        <input id="hdnHomeBased" type="hidden" name="hdnHomeBased" runat="server" />
            <input id="hdnTxtPreJanuaryExmpt" type="hidden" name="hdnTxtPreJanuaryExmpt" runat="server" />
            <input id="hdnTxtHealthCareExmpt" type="hidden" name="hdnTxtHealthCareExmpt" runat="server" />
            <input id="hdnTxtReligiousExmpt" type="hidden" name="hdnTxtReligiousExmpt" runat="server" />
            <input id="hdnEnrolledButNotAttending" type="hidden" name="hdnEnrolledButNotAttending" runat="server" />
            <input id="HdnConfirmSubmit" type="hidden" name="HdnConfirmSubmit" runat="server" />

           <input id="hdn_V_txtAllimm" type="hidden" name="hdn_V_txtAllimm" runat="server" />
            <input id="hdn_V_txtPermMedExmp" type="hidden" name="hdn_V_txtPermMedExmp" runat="server" />
            <input id="hdn_V_MDMO_PermMedExmp" type="hidden" name="hdn_V_MDMO_PermMedExmp" runat="server" />
            <input id="hdn_V_TextIEPServices" type="hidden" name="hdn_V_TextIEPServices" runat="server" />
            <input id="hdn_V_TextIndependentStudy" type="hidden" name="hdn_V_TextIndependentStudy" runat="server" />
           <input id="hdn_V_TextHomeBasedPrivate" type="hidden" name="hdn_V_TextHomeBasedPrivate" runat="server" />
            <input id="hdn_V_ConditionalNotDue" type="hidden" name="hdn_V_ConditionalNotDue" runat="server" />
            <input id="hdn_V_TextMedExmption" type="hidden" name="hdn_V_TextMedExmption" runat="server" />
            <input id="hdn_V_TextEnrolledButNotAttending" type="hidden" name="hdn_V_TextEnrolledButNotAttending" runat="server" />
            

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