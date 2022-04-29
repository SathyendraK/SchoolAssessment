<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndPrint.aspx.cs" Inherits="SchoolAssessment.KG.ViewAndPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />

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
            .auto-style4 {
                width: 207px;
                text-indent: 20px;
            }
    </style>

</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15</div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
       
         <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank">Instruction  <img src="Images/Icon_instr.png" width="8" /> </a> | 
               <a href="http://www.cairweb.org/calkidshots/KSeventhFAQs.pdf" target="_blank"> FAQs <img src="Images/Icon_instr.png" width="8" /> </a> | 
               Worksheet<a href="http://www.cairweb.org/calkidshots/pm236a.xls"target="_blank"> Xls <img src="Images/Icon_Excel.png" width="13" height="12"/> </a> <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank">pdf  <img src="Images/Icon_adobe.png" width="10" height="13" /></a> | 
               <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
          </div>
      </div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <center><img src="Images/Step_04.png" alt="School information:" /></center><br />
        <h2><img src="Images/hdr_ViewPrintReport.png" alt="Please confirm School information:" /></h2>
        
        <p><span class="redbold">Thank you - you have successfully submitted your annual report.<br />
            Please print and/or save a copy of your report for your records by selecting the option(s) below.</span></p>
        <table width="760">
          <tr>
            <td>
              <!--<h3 align="center">IMMUNIZATION ASSESSMENT OF KINDERGARTEN STUDENTS SCHOOL SUMMARY SHEET ONLINE</h3>-->
              <table cellspacing="0" width="100%">
                <tr>
                  <td>
                    <table width="100%">
                       <tr>
                            <td colspan="4" style="color: #4169E1"><b>SCHOOL INFORMATION</b></td>
                        </tr>
                      <tr>
                        <td class="auto-style4" ><strong>Name:</strong></td>
                        <td colspan="3"><asp:label id="lblSchName" runat="server"></asp:label></td>
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

                      <tr>
                          <td class="auto-style4"><strong>Administrator/Principal:</strong></td>
                          <td><asp:label id="lblSchAdmin" runat="server"></asp:label></td>
                          <td colspan="2"></td>
                      </tr>
                      <tr>
                          <td class="auto-style4"><strong>School Email:</strong> </td>
                          <td><asp:label id="lblSchEmail" runat="server"></asp:label></td>
                          <td colspan="2"></td>
                      </tr>      
                      <tr>
                        <td class="auto-style4"><strong>Physical Address:</strong></td>
                        <td colspan="3"><asp:label id="lblpaddress" runat="server"></asp:label></td>
                      </tr>
                        <tr>
                            <td class="auto-style4"><strong>Mailing Address:</strong></td>
                            <td colspan="3">
                                <asp:Label ID="lblmaddress" runat="server"></asp:Label></td>
                        </tr>
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
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number Ext:</strong></td>
                            <td>
                                <asp:Label ID="lblStaffPhoneExt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Report Submitted Date:</strong></td>
                            <td>
                                <asp:Label ID="lblSubmittedDate" runat="server"></asp:Label>
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
                        <tr>
                            <td class="indent" style="width: 207px"><strong>Phone Number Ext:</strong></td>
                            <td>
                                <asp:Label ID="lblDesPhoneExt" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                  </td>
                </tr>
              </table>







        <table width="800">
           
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
                    <asp:TextBox ID="TextIEPServices" TabIndex="5" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="TextIEPServices" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator12" ControlToValidate="TextIEPServices" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Independent Study</td>
                     <td><small>F</small></td>
                <td>
                    <asp:TextBox ID="TextIndependentStudy" TabIndex="6" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TextIndependentStudy" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘IEP Services’ Exemption - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator13" ControlToValidate="TextIndependentStudy" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who have a ‘Health Care Counseled’ Exemption - Numbers only" runat="server" Display="Dynamic" /> 
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent2">Home-based Private School</td>
                     <td><small>F</small></td>
                <td>
                    <asp:TextBox ID="TextHomeBasedPrivate" TabIndex="7" runat="server" MaxLength="4" Columns="4"  Width="50px" onkeyup="calculateTextTotal();"></asp:TextBox>
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
                    <asp:TextBox ID="txtNoimm" TabIndex="8" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNoimm" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Required" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
                    <asp:RangeValidator ID="Rangevalidator4" ControlToValidate="txtNoimm" MinimumValue="0" MaximumValue="9999" Type="Integer" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Number of kindergarteners who do not meet all the immunization requirements - Numbers only" runat="server" Display="Dynamic" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="indent">Temporary Medical Exemption</td>
                <td><small>D</small></td>
                <td>
                    <asp:TextBox ID="TextMedExmption" TabIndex="9" runat="server" Columns="4" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
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
                    <asp:TextBox ID="TextEnrolledButNotAttending" TabIndex="10" Columns="4" runat="server" MaxLength="4" Width="50px"  onkeyup="calculateTextTotal();"></asp:TextBox>
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
                    
                    <asp:TextBox ID="TextTotal" TabIndex="11" Columns="4" runat="server" MaxLength="4" Width="50px" EnableViewState= "false" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    
                    

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
                <td colspan="4">Number of students (from B-G) that are missing doses of each vaccine:
                <!-- If category 4 is a non-zero value, 4a-4e must not be all zero -->
            </td>
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
                            <td class="missingDosesRightSide"></td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                  
                </td>
                <td></td>
            </tr>
        </table>






             
        <p>
            <!--<asp:button id="btnprevious" runat="server" Text="Back" causesvalidation="False" OnClick="btnprevious_Click" ></asp:button>-->
            <asp:button runat="server" ID="btnReset" Text="Revise your Submitted Report" causesvalidation="false" Width="230px" OnClick="btnReset_Click" />
            <!--<asp:button id="btndownload" runat="server" Text="Download Report (PDF)" causesvalidation="False"></asp:button>-->
            <asp:button id="btnprint" runat="server" Text="Print Report" causesvalidation="False"></asp:button>            
            <!--<asp:button id="btnlogout" runat="server" Text="Logout" causesvalidation="False" OnClick="btnlogout_Click"></asp:button>-->
            <input id="hdnIsComplete" type="hidden" name="hdnIsComplete" runat="server" />
        </p>
        <hr />
        <p>For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="http://www.shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation">You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
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
