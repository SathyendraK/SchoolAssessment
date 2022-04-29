<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndPrint.aspx.cs" Inherits="SchoolAssessment.KG.ViewAndPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />

        <style type="text/css">
            .auto-style4 {
                width: 207px;
                text-indent: 20px;
            }
            .auto-style5 {
                width: 207px;
                text-indent: 20px;
                height: 22px;
            }
            .auto-style6 {
                height: 22px;
            }
            .auto-style7 {
                width: 825px;
            }
               
            </style>
        
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <a href="https://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/K_Fst_header.jpg" alt="Kindergarten Immunization Assessment" /></a></div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
       
         <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="https://www.shotsforschool.org/reporting/kindergarten/" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions  <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <a href="https://www.shotsforschool.org/reporting/kindergarten/faqs/" target="_blank" alt="FAQs" title="FAQs"> FAQs <img src="Images/Icon_instr.png" width="15" /> </a> | 
               Worksheet<a href="http://eziz.org/SA/PM236a.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://eziz.org/SA/PM236a.pdf" target="_blank"> <img src="Images/Icon_pdf.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> | 
               <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
             </div>
      </div>
      <div id="duedate">Reporting is due January 31, 2022</div>
    </div>
    
    <div id="content-wrap">
      <div id="content" >
        <center><img src="Images/KFst_step5.png" alt="School information:" /></center><br />
        <h2><img src="Images/hdr_ViewPrintReport.png" alt="Please confirm School information:" /></h2>
        
        <p><!--<span class="redbold">Thank you - you have successfully submitted your annual report.<br />
            Please print and/or save a copy of your report for your records by selecting the option(s) below.</span>--></p>
        <img src="Images/thankyou.png" width="760px" height="115px"/><br /><br />
          <p>
            <!--<asp:button id="btndownload" runat="server" Text="Download Report (PDF)" causesvalidation="False"></asp:button>-->
              <!--<asp:button id="btnlogout" runat="server" Text="Logout" causesvalidation="False" OnClick="btnlogout_Click"></asp:button>-->
              <input id="hdnIsComplete" runat="server" name="hdnIsComplete" type="hidden" />
              <asp:ImageButton ID="ImgBtnReviseYourRPT" causesvalidation="false" Enabled="true" ImageUrl="images/btnRevise.png" runat="server" OnClick="ImgBtnReviseYourRPT_Click" />
              <asp:ImageButton ID="ImgBtnPrintRPT"  causesvalidation="False" Enabled="true" ImageUrl="images/btnPrintK.png"  runat="server" OnClick="ImgBtnPrintRPT_Click" />
              <asp:ImageButton ID="ImgBtnPrintRPT1st" causesvalidation="False" Enabled="true" ImageUrl="images/btnPrint1st.png" runat="server" OnClick="ImgBtnPrintRPT1st_Click" />
          </p>
        <table width="760">
          <tr>
            <td class="auto-style7">
              <!--<h3 align="center">IMMUNIZATION ASSESSMENT OF KINDERGARTEN STUDENTS SCHOOL SUMMARY SHEET ONLINE</h3>-->
              <table cellspacing="0" width="100%">
                <tr>
                  <td>
                    <table width="100%">
                       <tr>
                            <td colspan="4" style="color: #4169E1" class="auto-style6"><b>SCHOOL INFORMATION</b></td>
                        </tr>
                      <tr>
                        <td class="auto-style5" ><strong>Name:</strong></td>
                        <td colspan="3" class="auto-style6"><asp:label id="lblSchName" runat="server"></asp:label></td>
                      </tr>
                        <tr>
                        <td class="auto-style4"><strong>School code:</strong></td>
                        <td><asp:label id="lblSchCode" runat="server"></asp:label></td>
                        <td colspan="2"></td>
                      </tr>
                        <tr>
                            <td class="auto-style4"><strong>Type:</strong> </td>
                            <td>
                                <asp:Label ID="lblstype" runat="server"></asp:Label></td>
                            <td colspan="2"></td>
                        </tr>
                      <tr>
                        <td class="auto-style4"><strong>Kindergarten Status:</strong> </td>
                        <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                          <td colspan="2"></td>
                        </tr>
                      <tr>
                          <td class="auto-style4"><strong>1st Grade Status:</strong></td>
                          <td>
                              <asp:Label ID="lblStatus_1st" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                        <!--<td>Login password:</td>
                        <td><asp:label id="lblpin" runat="server"></asp:label></td>-->
                        <td class="auto-style4"><strong>Public School District:</strong></td>
                        <td><asp:label id="lbldistrict" runat="server"></asp:label></td>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td class="auto-style4"><strong>County:</strong></td>
                        <td><asp:label id="lblcounty" runat="server"></asp:label></td>
                        <td></td>
                        <td></td>
                      </tr>    
                      <!--
                      <tr>
                          <td class="auto-style4"><strong>Administrator/Principal:</strong></td>
                          <td><asp:label id="lblSchAdmin" runat="server"></asp:label></td>
                          <td colspan="2"></td>
                      </tr>
                      -->
                      <tr>
                          <td class="auto-style4"><strong>School Email:</strong> </td>
                          <td><asp:label id="lblSchEmail" runat="server"></asp:label></td>
                          <td colspan="2"></td>
                      </tr>      
                      <tr>
                        <td class="auto-style4"><strong>Physical Address:</strong></td>
                        <td colspan="3"><asp:label id="lblpaddress" runat="server"></asp:label></td>
                      </tr>
                       <!-- <tr>
                            <td class="auto-style4"><strong>Mailing Address:</strong></td>
                            <td colspan="3">
                                <asp:Label ID="lblmaddress" runat="server"></asp:Label></td>
                        </tr>-->
                        <tr><td colspan="4"><br /></td></tr>
                        <tr>
                            <td colspan="4" style="color: #4169E1"><b>SCHOOL STAFF MEMBER COMPLETING THIS FORM</b></td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Name:</strong></td>
                            <td>
                                <asp:Label ID="lblstaffPrsn" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Email:</strong></td>
                            <td>
                                <asp:Label ID="LblStaffEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number:</strong></td>
                            <td>
                                <asp:Label ID="lbsStaffPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <!--<tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number Ext:</strong></td>
                            <td>
                                <asp:Label ID="lblStaffPhoneExt" runat="server"></asp:Label>
                            </td>
                        </tr>-->
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Report Submitted Date:</strong></td>
                            <td>
                                <asp:Label ID="lblSubmittedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Report Revised Date:</strong></td>
                            <td>
                                <asp:Label ID="lblRevisedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr><td colspan="4"><br /></td></tr>
                        <tr>
                            <td colspan="4" style="color: #4169E1"><b>DESIGNATED SCHOOL CONTACT</b></td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Name:</strong></td>
                            <td>
                                <asp:Label ID="lblDesContactName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Email:</strong></td>
                            <td>
                                <asp:Label ID="lblDesContactEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number:</strong></td>
                            <td>
                                <asp:Label ID="lblDesContactPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <!--<tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number Ext:</strong></td>
                            <td>
                                <asp:Label ID="lblDesPhoneExt" runat="server"></asp:Label>
                            </td>
                        </tr>-->
                    </table>
                  </td>
                </tr>
              </table>
            </td>
            </tr>
        </table>







        <table width="825">
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td colspan="5" style="color: #4169E1"><b>TK/KINDERGARTEN REPORT</b></td>
            </tr>
            <tr>
                <td width="385"><b>Total Number of Kindergarteners:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px" onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator30" controltovalidate="txttotno" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator25" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="ActiveStudentsValidator" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td align="left" width="10"></td>
                <td width="340"></td>
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
                <td  class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has all immunizations required for their grade.'> All Required Vaccine Doses</td>
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
                <td><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Please account for each student "missing doses by vaccine". &#13;&#13;Each student should be missing at least one dose. &#13;&#13;Therefore, the total number of Polio, DTP, MMR, HepB and VAR should be at least equal to the number of students in that group.'><b> Missing Doses By Vaccine</b> </td>
            </tr>

 <tr>
                <td class="indent"><!--<img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Entered Transitional Kindergarten with a valid personal beliefs exemption (PBE) for missing shot(s) that was signed within 6 months prior to entry and filed before January 1, 2016 and immunization records with dates for all required shots not exempted, or The PBE must have been filed before January 1, 2016 and is only valid for the current grade span (TK/K through 6th or 7th through 12th grade). For complete details, visit ShotsforSchool.org.'> Personal Belief Exemption-->
                    <img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a licensed physician’s written statement of a permanent medical exemption for missing shot(s) and immunization records with dates for all required shots not exempted. '> Permanent Medical Exemption
                </td>
                <td><small>C</small></td>
                <td colspan="2">
                     <asp:TextBox ID="txtPermMedExmp" TabIndex="4" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                <!--<i><small>&nbsp; From TK</small></i>-->
                <td>
                    <asp:TextBox ID="TextMissingDosesTotal" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                &nbsp;Students that are missing doses.&nbsp; </td>
</tr>
            <tr>
                 <td colspan="3" class="indentInfoImg"><small>Includes MD/DO verification of varicella disease</small></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their required immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Other</td>
                <td><small></small></td>
                <td> </td>
                <td></td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td>Total number of students missing each </td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F1</small></td>
                <td><asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <td>vaccine(s)</td>
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
                <td>
                     <table>
                        <tr>
                            <td class="missingDoses">Polio</td>
                            <td width="80px">
                                <asp:TextBox ID="txtPolio" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPolio" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator5" ControlToValidate="txtPolio" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator2" ControlToValidate="txtPolio" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Polio doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Hep B</td>
                            <td width="80px">
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
                <td class="indent2">Home-Based Private School</td>
                     <td><small>F3</small></td>
                <td><asp:TextBox ID="TextHomeBasedPrivate" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator14" ControlToValidate="TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
               </td>
                <td></td>
                <td><table>
                        <tr>
                            <td class="missingDoses">DTaP</td>
                            <td width="80px">
                                <asp:TextBox ID="txtDtp" TabIndex="14" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDtp" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator6" ControlToValidate="txtDtp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator3" ControlToValidate="txtDtp" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing DTaP doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px"></td>
                            <td class="missingDosesRightSide">Varicella</td>
                            <td width="80px"> <asp:TextBox ID="txtVZV" TabIndex="15" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtVZV" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator10" ControlToValidate="TextMissingDosesTotal" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator7" ControlToValidate="txtVZV" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Varicella doses entered should be less than or equal to the total sum between B-G" runat="server" />
                </td>
                        </tr>
                    </table></td>
            </tr>
            <tr>
                <td class="indent2"></td>
                     <td></td>
                <td>
                     </td>
                <td></td>
                <td> <table>
                        <tr>
                            <td class="missingDoses">MMR</td>
                            <td width="80px">
                                <asp:TextBox ID="txtMMR2" TabIndex="16" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMMR2" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator8" ControlToValidate="txtMMR2" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator5" ControlToValidate="txtMMR2" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing MMR doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide"></td>
                            <td width="80px"></td>
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
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Is missing a dose(s) in a series, but the next dose is not due yet. This means the child has received at least one dose in a series and the deadline for the next dose has not passed.'> Conditional- Missing Doses Not Currently Due</td>
                <td><small>B</small></td>
                <td>
                    <asp:TextBox ID="txtNoimm" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNoimm" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator4" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <!--<tr> <td colspan="3" class="indentInfoImg" height="30"><small>Not including Temporary Medical Exemptions</small></td>
            </tr>-->
            <tr><td colspan="5"><br /></td></tr>
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
                <td colspan="5"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /></td>
                
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
                 <td colspan="3" class="indentInfoImg" height="30"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
            </tr>
            <tr><td colspan="5"><br /><br /><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>
                    
                    <asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                   

                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>


        <hr />

        <!--Display 1st Grade Report ---------------------------------------------------------------------------------------------------------------------->
        <table width="825">
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td colspan="5" style="color: #4169E1"><b>1ST GRADE REPORT</b></td>
            </tr>
            <tr>
                <td width="385"><b>Total Number of 1st Grade:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno_1st" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px" onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator2" controltovalidate="txttotno_1st" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator2" controltovalidate="txttotno_1st" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator15" controltovalidate="txttotno_1st" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarten students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td align="left" width="10"></td>
                <td width="340"></td>
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
                <td  class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has all immunizations required for their grade.'> All Required Vaccine Doses</td>
                <td><small>A</small></td>
                <td><asp:textbox id="txtAllimm_1st" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator15" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator16" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
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
                <td><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Please account for each student "missing doses by vaccine". &#13;&#13;Each student should be missing at least one dose. &#13;&#13;Therefore, the total number of Polio, DTP, MMR, HepB and VAR should be at least equal to the number of students in that group.'><b> Missing Doses By Vaccine</b> </td>
            </tr>

 <tr>
                <td class="indent"><!--<img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Entered Transitional Kindergarten with a valid personal beliefs exemption (PBE) for missing shot(s) that was signed within 6 months prior to entry and filed before January 1, 2016 and immunization records with dates for all required shots not exempted, or The PBE must have been filed before January 1, 2016 and is only valid for the current grade span (TK/K through 6th or 7th through 12th grade). For complete details, visit ShotsforSchool.org.'> Personal Belief Exemption-->
                    <img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a licensed physician’s written statement of a permanent medical exemption for missing shot(s) and immunization records with dates for all required shots not exempted. '> Permanent Medical Exemption
                </td>
                <td><small>C</small></td>
                <td colspan="2">
                     <asp:TextBox ID="txtPermMedExmp_1st" TabIndex="4" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtPermMedExmp_1st" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator17" ControlToValidate="txtPermMedExmp_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                <!--<i><small>&nbsp; From TK</small></i>-->
                <td>
                    <asp:TextBox ID="TextMissingDosesTotal_1st" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                &nbsp;Students that are missing doses.&nbsp; </td>
</tr>
            <tr>
                 <td colspan="3" class="indentInfoImg"><small>Includes MD/DO verification of varicella disease</small></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their required immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Other</td>
                <td><small></small></td>
                <td> </td>
                <td></td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td>Total number of students missing each </td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F1</small></td>
                <td><asp:TextBox ID="TextIEPServices_1st" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="TextIEPServices_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator18" ControlToValidate="TextIEPServices_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <td>vaccine(s)</td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                <td><small>F2</small></td>
                <td>
                    <asp:TextBox ID="TextIndependentStudy_1st" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="TextIndependentStudy_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator19" ControlToValidate="TextIndependentStudy_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                
                     </td>
                <td></td>
                <td>
                     <table>
                        <tr>
                            <td class="missingDoses">Polio</td>
                            <td width="80px">
                                <asp:TextBox ID="txtPolio_1st" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtPolio_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator20" ControlToValidate="txtPolio_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtPolio_1st" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Polio doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Hep B</td>
                            <td width="80px">
                    <asp:TextBox ID="txtHepb_1st" TabIndex="13" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="txtHepb_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator21" ControlToValidate="txtHepb_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtHepb_1st" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Hepatitis B doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td class="indent2">Home-Based Private School</td>
                     <td><small>F3</small></td>
                <td><asp:TextBox ID="TextHomeBasedPrivate_1st" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="TextHomeBasedPrivate_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator22" ControlToValidate="TextHomeBasedPrivate_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
               </td>
                <td></td>
                <td><table>
                        <tr>
                            <td class="missingDoses">DTaP</td>
                            <td width="80px">
                                <asp:TextBox ID="txtDtp_1st" TabIndex="14" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="txtDtp_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator23" ControlToValidate="txtDtp_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for DTP/DTaP/DT - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtDtp_1st" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing DTaP doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px"></td>
                            <td class="missingDosesRightSide">Varicella</td>
                            <td width="80px"> <asp:TextBox ID="txtVZV_1st" TabIndex="15" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtVZV_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator24" ControlToValidate="txtVZV_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Varicella - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtVZV_1st" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Varicella doses entered should be less than or equal to the total sum between B-G" runat="server" />
                </td>
                        </tr>
                    </table></td>
            </tr>
            <tr>
                <td class="indent2"></td>
                     <td></td>
                <td>
                     </td>
                <td></td>
                <td> <table>
                        <tr>
                            <td class="missingDoses">MMR</td>
                            <td width="80px">
                                <asp:TextBox ID="txtMMR2_1st" TabIndex="16" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="txtMMR2_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator26" ControlToValidate="txtMMR2_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator5" ControlToValidate="txtMMR2_1st" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing MMR doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide"></td>
                            <td width="80px"></td>
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
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Is missing a dose(s) in a series, but the next dose is not due yet. This means the child has received at least one dose in a series and the deadline for the next dose has not passed.'> Conditional- Missing Doses Not Currently Due</td>
                <td><small>B</small></td>
                <td>
                    <asp:TextBox ID="txtNoimm_1st" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="txtNoimm_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator27" ControlToValidate="txtNoimm_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <!--<tr> <td colspan="3" class="indentInfoImg" height="30"><small>Not including Temporary Medical Exemptions</small></td>
            </tr>-->
            <tr><td colspan="5"><br /></td></tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Has a temporary medical exemption to certain vaccine(s) and has submitted immunization records for vaccines not exempted. The statement must indicate which immunization(s) must be postponed and when the child can be immunized.'> Temporary Medical Exemption</td>
                <td><small>D</small></td>
                <td>
                    <asp:TextBox ID="TextMedExmption_1st" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="TextMedExmption_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator28" ControlToValidate="TextMedExmption_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
             
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="5"><br/></td>
            </tr>
            <tr>
                <td colspan="5"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /></td>
                
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Does not fit one of the previous categories and is subject to exclusion.'> Overdue Doses</td>
                <td ><small>G</small></td>
                <td >
                    <asp:TextBox ID="TextEnrolledButNotAttending_1st" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="TextEnrolledButNotAttending_1st" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator29" ControlToValidate="TextEnrolledButNotAttending_1st" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td ></td>
            </tr>
            <tr>
                 <td colspan="3" class="indentInfoImg" height="30"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
            </tr>
            <tr><td colspan="5"><br /><br /><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>
                    
                    <asp:TextBox ID="TextTotal_1st" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                   

                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>


        <p>For questions about assessment, contact your <a href="https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx" target="_blank">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="https://www.shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation"><i>Session will automatically time out in 20 minutes.</i><br />You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
       <input id="HdnTextMissingDosesTotal" type="hidden" name="HdnTextMissingDosesTotal" runat="server" />
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
