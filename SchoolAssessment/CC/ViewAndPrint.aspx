<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndPrint.aspx.cs" Inherits="SchoolAssessment.CC.ViewAndPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Childcare Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />

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
               
            .auto-style8 {
                height: 18px;
            }
               
            </style>
        
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <a href="https://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/PRE_K_NOBKGRD.png" alt="Kindergarten Immunization Assessment" /></a></div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
       
         <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="https://www.shotsforschool.org/reporting/childcare/" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions   <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <a href="https://www.shotsforschool.org/reporting/childcare/faqs/" target="_blank" alt="FAQs" title="FAQs"> FAQs  <img src="Images/Icon_instr.png" width="15" /> </a> | 
               Worksheet<a href="http://eziz.org/SA/cdph8342.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://eziz.org/SA/cdph8342.pdf" target="_blank"> <img src="Images/Icon_pdf.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> | 
               <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
             </div>
      </div>
      <div id="duedate">Reporting is due January 31, 2022</div>
    </div>
    
    <div id="content-wrap">
      <div id="content" >
        <center><img src="Images/Step_Fac_04.png" alt="School information:" /></center><br />
        <h2><img src="Images/hdr_ViewPrintReport.png" alt="Please confirm School information:" /></h2>
        
        <p><!--<span class="redbold">Thank you - you have successfully submitted your annual report.<br />
            Please print and/or save a copy of your report for your records by selecting the option(s) below.</span>--></p>
        <img src="Images/thankyou.png" width="760px" height="115px"/><br /><br />
          <p>
            <!--<asp:button id="btndownload" runat="server" Text="Download Report (PDF)" causesvalidation="False"></asp:button>-->
              <!--<asp:button id="btnlogout" runat="server" Text="Logout" causesvalidation="False" OnClick="btnlogout_Click"></asp:button>-->
              <input id="hdnIsComplete" runat="server" name="hdnIsComplete" type="hidden" />
              <asp:ImageButton ID="ImgBtnReviseYourRPT" causesvalidation="false" Enabled="true" ImageUrl="images/btn2_revise.png" runat="server" OnClick="ImgBtnReviseYourRPT_Click"/>
              <asp:ImageButton ID="ImgBtnPrintRPT"  causesvalidation="False" Enabled="true" ImageUrl="images/btn3_printreport.png"  runat="server" OnClick="ImgBtnPrintRPT_Click" />
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
                            <td colspan="4" style="color: #4169E1" class="auto-style6"><b>FACILITY INFORMATION</b></td>
                        </tr>
                      <tr>
                        <td class="auto-style5" ><strong>Facility Name:</strong></td>
                        <td colspan="3" class="auto-style6"><asp:label id="lblSchName" runat="server"></asp:label></td>
                      </tr>
                        <tr>
                        <td class="auto-style4"><strong>Facility Number:</strong></td>
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
                            <td class="auto-style4"><strong>Status:</strong> </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                            <td colspan="2"></td>
                        </tr>
                        
                      <!--  
                      <tr>
                        
                        <td class="auto-style4"><strong>Public School District:</strong></td>
                        <td><asp:label id="lbldistrict" runat="server"></asp:label></td>
                        <td colspan="2"></td>
                      </tr>
                       --> 
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
                          <td class="auto-style4"><strong>Facility Email:</strong> </td>
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
                            <td colspan="4" style="color: #4169E1"><b>FACILITY STAFF MEMBER COMPLETING THIS FORM</b></td>
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
                            <td colspan="4" style="color: #4169E1"><b>DESIGNATED FACILITY CONTACT</b></td>
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
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td width="385"><b>Total Number of Children:</b></td>
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
                <td colspan="5"><small>Report on ages 2-5 years</small><br /><br />Account for each student in <b>one</b> of the categories below.</td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr><td colspan="5" class="balanceheight"><img src="Images/Title_UncondAdm.png" width="217" height="15"/></td></tr>
            <tr>
                <td colspan="5" class="indent" ><b>Requirements Met</b> </td>
              
            </tr>
            <tr>
                <td  class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Is 24 months and older and has all immunizations required for their age'> All Required Vaccine Doses</td>
                <td><small>A</small></td>
                <td><asp:textbox id="txtAllimm" tabindex="2" columns="4" runat="server" maxlength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:textbox>
                          <asp:requiredfieldvalidator id="RequiredFieldValidator1" controltovalidate="txtAllimm" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Required" runat="server" cssclass="RequiredFieldValidator" display="Dynamic" />
                          <asp:rangevalidator id="Rangevalidator1" controltovalidate="txtAllimm" minimumvalue="0" maximumvalue="9999" type="Integer" cssclass="RequiredFieldValidator" text="&bull;" errormessage="Number of kindergarteners with all required immunizations and/or documented history of disease - Numbers only" runat="server" display="Dynamic" />
              </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan ="5" class="balanceheight"><br /></td>
            </tr>
            <tr>
                <td colspan="3" class="indent"><b>Requirements Met, But Missing Doses</b> </td>
                <td></td>
                <td><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Please account for each student "missing doses by vaccine". &#13;&#13;Each student should be missing at least one dose. &#13;&#13;Therefore, the total number of Polio, DTP, MMR, HepB and VAR should be at least equal to the number of students in that group.'><b> Missing Doses By Vaccine</b> </td>
            </tr>

            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a personal beliefs exemption (PBE) filed at a prior California child-care facility for missing shots(s) and immunization records with dates for all required shots not exempted. The PBE must have been filed before January 1, 2016 and is only valid until entry to transitional kindergarten/ kindergarten.'> Personal Belief Exemption</td>
                <td><small>E</small></td>
                <td colspan="2">
                     <asp:TextBox ID="txtBelExmp" TabIndex="3" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtBelExmp" Text="&bull;" ErrorMessage="Number of kindergarteners who have a Personal Belief Exemption’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator2" ControlToValidate="txtBelExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Personal Belief’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                
                <td>
                    <asp:TextBox ID="TextMissingDosesTotal" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                &nbsp;Students are missing doses.</td>
            </tr>
            <tr>
                        <td colspan="3" class="indentInfoImg"><small>Pre-2016 </small></td>
                        <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Submits a licensed physician’s written statement of a permanent medical exemption for missing shot(s) and immunization records with dates for all required shots not exempted'> Permanent Medical Exemption</td>
                <td><small>C</small></td>
                <td><asp:TextBox ID="txtPermMedExmp" TabIndex="4" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPermMedExmp" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator7" ControlToValidate="txtPermMedExmp" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners with Permanent Medical Exemptions to any immunizations - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
                <!--<td align="left"><i><small>TK students only</small></i></td>-->
                <td>Total number of students missing each vaccine(s)</td>
            </tr>
             <tr>
                
                <td colspan="3" class="indentInfoImg"><small>Includes MD/DO verification of varicella disease</small></td>
                <td></td>
                <td> <table>
                        <tr>
                            <td class="missingDoses">Polio</td>
                            <td width="80px">
                                <asp:TextBox ID="txtPolio" TabIndex="12" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPolio" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator5" ControlToValidate="txtPolio" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Polio - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator2" ControlToValidate="txtPolio" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Polio doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Hep B&nbsp;</td>
                            <td width="80px">
                    <asp:TextBox ID="txtHepb" TabIndex="13" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtHepb" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator9" ControlToValidate="txtHepb" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for Hepatitis B - Numbers only" runat="server" Display="Dynamic" />
                    <asp:CompareValidator ID="CustomValidator6" ControlToValidate="txtHepb" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing Hepatitis B doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                        </tr>
                    </table></td>   
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE &#13; &#149; All their required immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP).  &#13; &#13;  The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13; &#13; * Schools may contact their local educational agency for additional information about these categories'> Other: IEP Services</td>
                <td><small>F1</small></td>
                <td>
                    <asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘‘IEP Services’ Exemption - Numbers only" runat="server" Display="Dynamic" />
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
                            <td width="20px">&nbsp;</td>
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
                <td></td>
                <td></td>
                <td><table>
                        <tr>
                            <td class="missingDoses">MMR</td>
                            <td width="80px">
                                <asp:TextBox ID="txtMMR2" TabIndex="16" Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMMR2" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                                <asp:RangeValidator ID="Rangevalidator8" ControlToValidate="txtMMR2" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet the immunization requirements for 2nd Dose MMR - Numbers only" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="CustomValidator5" ControlToValidate="txtMMR2" Type="Integer" ControlToCompare="TextMissingDosesTotal" Operator="LessThanEqual" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="The number of kindergarten missing MMR doses entered should be less than or equal to the total sum between B-G" runat="server" />
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td class="missingDosesRightSide">Hib</td>
                            <td width="80px">
                                <asp:TextBox ID="txtHib"  Columns="4" runat="server" MaxLength="4" Width="50px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><img src="Images/Title_CondAdm.png" width="331" height="15"/><!--Independent Study--></td>
                <td><small><!--F2--></small></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="indent"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Is missing a dose(s) in a series, but the next dose is not due yet. This means the child has received at least one dose in a series and the deadline for the next dose has not passed.  &#13; &#13; Homeless, transfer and foster students counted here if still in the process of meeting requirements/ waiting for records'> 
                            Conditional- Missing Doses Not Currently Due<!--Home-based Private School--></td>
                     <td><small>B</small></td>
                
                <td><asp:TextBox ID="txtNoimm" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNoimm" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator4" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
               </td>
                <td></td>
                <td> </td>
            </tr>
            <!--<tr>
                <td colspan="3" class="indentInfoImg" valign="TOP" height="14px"><small>Not including Temporary Medical Exemptions</small></td>
                <td></td>
            </tr>-->

            <tr>
                <td class="indent" style="height: 18px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='WHO DO NOT HAVE &#13; &#149; All their TDaP immunizations AND &#13; &#149; Temporary medical exemptions or conditional admission &#13;&#13; AND WHO ARE &#13; &#149; Enrolled in a home-based private school, OR &#13; & Enrolled in an independent study program and do not receive classroom-based instruction, OR &#13; &#149; Accessing special education or related services required by his or her individualized education program (IEP). &#13;&#13; The immunization requirements do not prohibit pupils from accessing special education and related services required by their individualized education programs. &#13;&#13; * Schools may contact their local educational agency for additional information about these categories.'> Temporary Medical Exemption</td>
                <td class="auto-style8"><small>D</small></td>
                <td class="auto-style8">
                    <asp:TextBox ID="TextMedExmption" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextMedExmption" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator11" ControlToValidate="TextMedExmption" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
             
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td colspan="5"><br/></td>
            </tr>
            <tr>
                <td colspan="5"><img src="Images/Title_ReqsNotMet.png" width="442" height="14" /></td>
                
            </tr>
            <tr>
                <td class="indent" style="height: 32px"><img src="./images/icon_Info.png" width="14" height="14" alt="" align="center" title='Enrolled but not attending, does not fit one of the previous categories and is subject to exclusion.'> Overdue
                    </td>
                <td ><small>G</small></td>
                <td >
                    <asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextEnrolledButNotAttending" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator3" ControlToValidate="TextEnrolledButNotAttending" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td ></td>
            </tr>
            <tr>
                
                <td colspan="3" class="indentInfoImg"><small>Includes homeless or foster care students in process of locating records</small></td>
                <td></td>
                   
            </tr>
            <tr><td colspan="5"><br /></td></tr>
            <tr><td colspan="5"><br /></td></tr>
            <tr>
                <td style="color: #4169E1" align="right"><b>TOTAL</b></td>
                <td></td>
                <td>
                    
                    <asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                   

                </td>
                <td></td>
            </tr>
            <tr>
                        <td align="right" class="indentInfoImg"><small>ages 2-5 years</small></td>
                        <td></td>
                        <td></td>
                        <td></td>
            </tr>
        </table>






             

        <hr />
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

