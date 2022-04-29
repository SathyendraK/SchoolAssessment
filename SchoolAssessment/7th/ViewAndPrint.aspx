<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndPrint.aspx.cs" Inherits="SchoolAssessment._7th.ViewAndReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California 7th Grade Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />

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
        <center><img src="Images/step_7_5.png" alt="School information:" /></center><br />
        <h2><img src="Images/hdr_ViewPrintReport.png" alt="Please confirm School information:" /></h2>
        
        <p><!--<span class="redbold">Thank you - you have successfully submitted your annual report.<br />
            Please print and/or save a copy of your report for your records by selecting the option(s) below.</span>--></p>
        <img src="Images/thankyou.png" width="760px" height="115px"/><br /><br />
          <p>
            <!--<asp:button id="btndownload" runat="server" Text="Download Report (PDF)" causesvalidation="False"></asp:button>-->
              <!--<asp:button id="btnlogout" runat="server" Text="Logout" causesvalidation="False" OnClick="btnlogout_Click"></asp:button>-->
              <input id="hdnIsComplete" runat="server" name="hdnIsComplete" type="hidden" />
              <asp:ImageButton ID="ImgBtnReviseYourRPT" causesvalidation="false" Enabled="true" ImageUrl="images/btnRevise.png" runat="server" OnClick="ImgBtnReviseYourRPT_Click" />
              <asp:ImageButton ID="ImgBtnPrintRPT"  causesvalidation="False" Enabled="true" ImageUrl="images/btnPrint7th.png"  runat="server" OnClick="ImgBtnPrintRPT_Click" />
              <asp:ImageButton ID="ImgBtnPrintRPT8th" causesvalidation="False" Enabled="true" ImageUrl="images/btnPrint8th.png"  runat="server" OnClick="ImgBtnPrintRPT8th_Click" />
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
                            <td class="auto-style4"><strong>7th Grade Status:</strong> </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr><td class="auto-style4"><strong>8th Grade Status:</strong></td>
                            <td>
                                <asp:Label ID="lblStatus_8th" runat="server"></asp:Label></td>
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







        <table width="760">
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="7" style="color: #4169E1">
                    <b>7TH GRADE REPORT</b>
                </td>
            </tr>
            <tr>
                <td width="450"><b>Total Number of 7th Grade Students:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator30" controltovalidate="txttotno" text="&bull;" errormessage="Number of 7th Grade students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator25" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 7th Grade students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="ActiveStudentsValidator" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 7th Grade students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td width="30"></td>
                <td width="10"></td>
                <td align="left" width="80"></td>
                <td width="100"></td>
            </tr>
            <tr>
                <td colspan="5">Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr><td colspan="5"><img src="Images/Title_UncondAdm.png" width="217" height="15"/></td></tr>
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
                    <asp:TextBox ID="V_txtAllimm" runat="server"  maxlength="4" Width="50px" ></asp:TextBox>
                </td>
                <td></td>
                <td><small>A</small></td>
                <td>
                <asp:textbox id="txtAllimm" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator1" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator1" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
              
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
                    <asp:TextBox  ID="V_txtPermMedExmp" Columns="4" runat="server" MaxLength="4" Width="50px" ></asp:TextBox></td>
                <td></td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td><small>C1</small></td>
                <td><asp:TextBox ID="txtPermMedExmp" TabIndex="4" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent3"> MD/DO verification of varicella disease</td>
                <td><small>C2</small></td>
                <td>
                    <asp:TextBox ID="V_MDMO_PermMedExmp" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
			</tr>	
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their TDaP immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Other</td>
                <td></td>
                <td>
                   
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="V_TextIEPServices" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                    </td>
                <td></td>
                <td><small>F1</small></td>
                <td><asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                     <td><small>F2</small></td>
                <td><asp:TextBox  ID="V_TextIndependentStudy" runat="server"  MaxLength="4" Columns="4"  Width="50px" ></asp:TextBox> </td>
                <td></td>
                 <td><small>F2</small></td>
                <td><asp:TextBox ID="TextIndependentStudy" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TextIndependentStudy" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator13" ControlToValidate="TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Home-Based Private School</td>
                     <td><small>F3</small></td>
                <td>
                    <asp:TextBox ID="V_TextHomeBasedPrivate" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                       </td>
                <td></td>
                <td><small>F3</small></td>
                <td><asp:TextBox ID="TextHomeBasedPrivate" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TextHomeBasedPrivate" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator14" ControlToValidate="TextHomeBasedPrivate" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
             </td>
                <td> </td>
            </tr>
            <tr>
                <td colspan="4">
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
                    <asp:TextBox ID="V_ConditionalNotDue" runat="server"  maxlength="4" Width="50px" ></asp:TextBox>
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
                    <asp:TextBox ID="V_TextMedExmption" runat="server" Columns="4" MaxLength="4" Width="50px"></asp:TextBox>    
                </td>
                <td></td>
                <td><small>D</small></td>
                <td><asp:TextBox ID="TextMedExmption" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextMedExmption" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator11" ControlToValidate="TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
            </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7"><br/></td>
            </tr>
            <tr>
                <td colspan="7"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /></td>
                
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Enrolled but not attending, does not fit one of the previous categories and is subject to exclusion.'> Overdue-Needs Doses Now</td>
                <td><small>G</small></td>
                <td>
                    <asp:TextBox ID="V_TextEnrolledButNotAttending" Columns="4" runat="server" MaxLength="4" Width="50px" ></asp:TextBox>
                    </td>
                <td ></td>
                <td ></td>
                <td><asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator3" ControlToValidate="TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                 <td colspan="6" class="indentInfoImg"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
            </tr>
            <tr><td colspan="7"><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>  <asp:TextBox ID="V_TextTotal" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                <td></td>
                <td></td>
                <td><asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox></td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>


        <hr />

        <!-------------------------Display 8th grade report ------------------------------------------------------------------------------------------------------------------>
        <table width="760">
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="7" style="color: #4169E1">
                    <b>8TH GRADE REPORT</b>
                </td>
            </tr>
            <tr>
                <td width="450"><b>Total Number of 8th Grade Students:</b></td>
                <td width="10"></td>
                <td width="80">
                    <asp:textbox id="txttotno_8th" tabindex="1" columns="4" runat="server" maxlength="4" Width="50px"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator2" controltovalidate="txttotno_8th" text="&bull;" errormessage="Number of 8th Grade students enrolled this year - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator2" controltovalidate="txttotno" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 8th Grade students enrolled this year - Numbers only" runat="server" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator4" controltovalidate="txttotno" minimumvalue="1" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of 8th Grade students enrolled this year - Must be greater than 0" runat="server" display="Dynamic" />
                        
                </td>
                <td width="30"></td>
                <td width="10"></td>
                <td align="left" width="80"></td>
                <td width="100"></td>
            </tr>
            <tr>
                <td colspan="5">Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr><td colspan="5"><img src="Images/Title_UncondAdm.png" width="217" height="15"/></td></tr>
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
                    <asp:TextBox ID="V_txtAllimm_8th" runat="server"  maxlength="4" Width="50px" ></asp:TextBox>
                </td>
                <td></td>
                <td><small>A</small></td>
                <td>
                <asp:textbox id="txtAllimm_8th" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator4" controltovalidate="txtAllimm_8th" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator5" controltovalidate="txtAllimm_8th" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
              
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
                    <asp:TextBox  ID="V_txtPermMedExmp_8th" Columns="4" runat="server" MaxLength="4" Width="50px" ></asp:TextBox></td>
                <td></td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td><small>C1</small></td>
                <td><asp:TextBox ID="txtPermMedExmp_8th" TabIndex="4" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPermMedExmp_8th" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator6" ControlToValidate="txtPermMedExmp_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent3"> MD/DO verification of varicella disease</td>
                <td><small>C2</small></td>
                <td>
                    <asp:TextBox ID="V_MDMO_PermMedExmp_8th" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
			</tr>	
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE   &#13; &#149; All their TDaP immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; &#149; Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13;The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#149; Schools may contact their local educational agency for additional information about these categories'> Other</td>
                <td></td>
                <td>
                   
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">IEP Services</td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="V_TextIEPServices_8th" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                    </td>
                <td></td>
                <td><small>F1</small></td>
                <td><asp:TextBox ID="TextIEPServices_8th" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TextIEPServices_8th" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator8" ControlToValidate="TextIEPServices_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                     <td><small>F2</small></td>
                <td><asp:TextBox  ID="V_TextIndependentStudy_8th" runat="server"  MaxLength="4" Columns="4"  Width="50px" ></asp:TextBox> </td>
                <td></td>
                 <td><small>F2</small></td>
                <td><asp:TextBox ID="TextIndependentStudy_8th" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TextIndependentStudy_8th" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator9" ControlToValidate="TextIndependentStudy_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Independent Study’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Home-Based Private School</td>
                     <td><small>F3</small></td>
                <td>
                    <asp:TextBox ID="V_TextHomeBasedPrivate_8th" runat="server" MaxLength="4" Columns="4"  Width="50px"></asp:TextBox>
                       </td>
                <td></td>
                <td><small>F3</small></td>
                <td><asp:TextBox ID="TextHomeBasedPrivate_8th" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="TextHomeBasedPrivate_8th" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator10" ControlToValidate="TextHomeBasedPrivate_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Home-based Private School’ Exemption - Numbers only" runat="server" Display="Dynamic" />
             </td>
                <td> </td>
            </tr>
            <tr>
                <td colspan="4">
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
                    <asp:TextBox ID="V_ConditionalNotDue_8th" runat="server"  maxlength="4" Width="50px" ></asp:TextBox>
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
                    <asp:TextBox ID="V_TextMedExmption_8th" runat="server" Columns="4" MaxLength="4" Width="50px"></asp:TextBox>    
                </td>
                <td></td>
                <td><small>D</small></td>
                <td><asp:TextBox ID="TextMedExmption_8th" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="TextMedExmption_8th" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator15" ControlToValidate="TextMedExmption_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
            </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7"><br/></td>
            </tr>
            <tr>
                <td colspan="7"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /></td>
                
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Enrolled but not attending, does not fit one of the previous categories and is subject to exclusion.'> Overdue-Needs Doses Now</td>
                <td><small>G</small></td>
                <td>
                    <asp:TextBox ID="V_TextEnrolledButNotAttending_8th" Columns="4" runat="server" MaxLength="4" Width="50px" ></asp:TextBox>
                    </td>
                <td ></td>
                <td ></td>
                <td><asp:TextBox ID="TextEnrolledButNotAttending_8th" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="TextEnrolledButNotAttending_8th" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator16" ControlToValidate="TextEnrolledButNotAttending_8th" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                 <td colspan="6" class="indentInfoImg"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
            </tr>
            <tr><td colspan="7"><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>  <asp:TextBox ID="V_TextTotal_8th" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                <td></td>
                <td></td>
                <td><asp:TextBox ID="TextTotal_8th" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox></td></td>
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
