<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewAndSubmit.aspx.cs" Inherits="SchoolAssessment.KG.ReviewAndSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15</div>
        <!--Commented out by A.T. on 09/18/2014  
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
        <div id="help"><a target="_blank" href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf">Help</a></div>
      </div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <form id="Form1" method="post" runat="server" autocomplete="off">
        <p><span class="redbold">Thank you - you have successfully submitted your annual report.<br />
Please print and/or save a copy of your report for your records by selecting the option(s) below.</span></p>
        <table width="760">
          <tr>
            <td>
              <h3 align="center">IMMUNIZATION ASSESSMENT OF KINDERGARTEN STUDENTS SCHOOL SUMMARY SHEET ONLINE</h3>
              <table cellspacing="0" width="100%">
                <tr>
                  <td>
                    <table width="100%">
                      <tr>
                        <td>School code:</td>
                        <td><asp:label id="lblSchCode" runat="server"></asp:label></td>
                        <td>This School is:</td>
                        <td><asp:label id="lblstype" runat="server"></asp:label></td>
                      </tr>
                      <tr>
                        <td>Login password:</td>
                        <td><asp:label id="lblpin" runat="server"></asp:label></td>
                        <td>Public School District:</td>
                        <td><asp:label id="lbldistrict" runat="server"></asp:label></td>
                      </tr>
                      <tr>
                        <td>County:</td>
                        <td><asp:label id="lblcounty" runat="server"></asp:label></td>
                        <td></td>
                        <td></td>
                      </tr>    
                      <tr>
                        <td>School Name:</td>
                        <td colspan="3"><asp:label id="lblSchName" runat="server"></asp:label></td>
                      </tr>    
                      <tr>
                        <td>Physical Address:</td>
                        <td colspan="3"><asp:label id="lblpaddress" runat="server"></asp:label></td>
                      </tr>    
                      <tr>
                        <td>Mailing Address:</td>
                        <td colspan="3"><asp:label id="lblmaddress" runat="server"></asp:label></td>
                      </tr>
                      <tr>
                        <td>
                          <strong>Number of Kindergarten <br />
                            Students Enrolled This Year:</strong>
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txttotno" tabindex="1" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>                
                    </table>
                  </td>
                </tr>
              </table>
              <table border="1" width="100%">
                <tr>
                  <td colspan="2" align="center">
                    <strong>IMMUNIZATION STATUS OF KINDERGARTEN STUDENTS</strong>
                  </td>
                </tr>
                <tr>
                  <td align="center" width="40%">
                    UNCONDITIONAL ENTRANTS
                  </td>
                  <td align="center" width="60%">
                    CONDITIONAL ENTRANTS
                  </td>
                </tr>
                <tr>
                  <td valign="top" rowspan="3">
                    <!--Indicate the number of kindergartners with:-->
                    <table>
                      <tr>
                        <td colspan="3" align="top">
                          Indicate the number of kindergartners with:
                        </td>
                      </tr>
                      <tr>
                        <td valign="top">
                          1.
                        </td>
                        <td valign="top">
                          All required immunizations and/or documented history of disease
                        </td>
                        <td valign="top">
                          <asp:textbox id="txtAllimm" tabindex="2" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="top">
                          2.
                        </td>
                        <td valign="top">
                          Permanent Medical Exemptions to any immunizations
                        </td>
                        <td valign="top">
                          <asp:textbox id="txtPermMedExmp" tabindex="3" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="top">
                          3.
                        </td>
                        <td valign="top">
                          Personal Beliefs Exemption to any immunizations
                        </td>
                        <td valign="top">
                          <asp:textbox id="txtPerBelExmp" tabindex="4" columns="4" runat="server" BackColor="#CCCCCC"></asp:textbox>
                        </td>
                      </tr>
                        <tr>
                        <td valign="top"></td>
                        <td colspan = "2" valign="top">
                          (<strong>Row 3 = 3a + 3b</strong>)
                        </td>
                      </tr>
                      <!-- Added by A.T. on 08/06/2014 -->
                      <!-- Commented out by AT on 06/01/2015
                      <tr>
                         <td colspan="2">
                             <ol start="1" type="a" class="roman" >
                                <li>'Pre-January 2014' Exemption</li>
                             </ol>
                         </td>
                         <td>
                             <asp:TextBox ID="TxtPreJanuaryExmpt" runat="server" MaxLength="4" Columns="4"></asp:TextBox>
                         </td>
                      </tr>
                      -->
                      <tr>
                          <td colspan="2">
                             <ol start="1" type="a" class="roman">
                                 <li>'Health Care Practitioner Counseled' Exemption</li>
                             </ol>
                          </td>
                          <td>
                              <asp:TextBox ID="TxtHealthCareExmpt" runat="server" MaxLength="4" Columns="4"></asp:TextBox>
                          </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <ol start="2" type="a" class="roman">
                                 <li>'Religious' Exemption</li>
                             </ol>
                         </td>
                         <td>
                             <asp:TextBox ID="TxtReligiousExmpt" runat="server" MaxLength="4" Columns="4"></asp:TextBox>
                         </td>
                     </tr>
                     <!-- Added by A.T. till here on 08/06/2014 -->
                    </table>
                      <!--<div style="padding: 6px; background: #e2f1f4; text-align: center; font-weight: bold;">-->
                      <div style="padding: 6px; text-align: center; font-weight: bold;">
                      <!-- Moved the text after Note: to outside so that it spans over 1,2,3,4,5 by AT on 08/14/2015 --> 
                      <!--Note: The total of lines 1+2+3+4 should equal NUMBER OF KINDERGARTEN STUDENTS ENROLLED THIS YEAR, shown above -->
                    </div>
                  </td>
                  <td valign="top">
                    <table>
                      <tr>
                        <td valign="top">
                          4.
                        </td>
                        <td valign="top">
                          <!--Number of kindergartners who do not meet all the immunization requirements: i.e., who have not documented one or more required immunizations or who have a <span class="underline">temporary</span> medical exemption (THESE STUDENTS MUST BE FOLLOWED UP)-->
                          Number of Kindergartners who do not meet the immunization requirements, but are <span class="underline">Not Currently Due</span> for a missing dose or qualify for other reasons*. (THESE STUDENTS REQUIRE FOLLOW-UP)
                        </td>
                        <td valign="top" width="100">
                          <asp:textbox id="txtNoimm" tabindex="5" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          Of the pupils in category 4 above, please indicate the numbers <span class="underline">NOT</span> meeting the requirement for:
                        </td>
                        <td>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          <ol start="1" type="a">
                            <li>Polio</li></ol>
                        </td>
                        <td>
                          <asp:textbox id="txtPolio" tabindex="6" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          <ol start="2" type="a">
                            <li>DTP/DTaP/DT</li></ol>
                        </td>
                        <td>
                          <asp:textbox id="txtDtp" tabindex="7" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          <ol start="3" type="a">
                            <li>MMR</li></ol>
                        </td>
                        <td>
                          <asp:textbox id="txtMMR2" tabindex="9" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          <ol start="4" type="a">
                            <li>Hepatitis B</li></ol>
                        </td>
                        <td>
                          <asp:textbox id="txtHepb" tabindex="10" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          <ol start="5" type="a">
                            <li>Varicella (child has not received vaccine and has not had chickenpox)</li></ol>
                        </td>
                        <td>
                          <asp:textbox id="txtVZV" tabindex="11" columns="4" runat="server"></asp:textbox>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <!-- Commented out by AT since Steve Nickle didn't want the divider. <td align="center" width="40%" bordercolor="white">-->
                    <!--UNCONDITIONAL ENTRANTS-->
                  <!--</td> -->
                  <td colspan="2" align="center" width="60%" >
                    ENROLLED BUT NOT ATTENDING
                  </td>
                </tr>
                <tr>
                  <!--<td bordercolor="white"></td>-->
                  <td colspan="2" valign="top">
                      <table>
                      <tr>
                        <td valign="top">
                          5.
                        </td>
                        <td valign="top">
                           <!--Number of Kindergartners not attending school because they do not meet the immunization requirement and do not qualify for conditional status.-->
                           Number of children not in attendance (at time of reporting) due to not meeting unconditional or conditional admission requirements.
                        </td>
                        <td valign="top" width="100">
                          <asp:textbox id="TextboxEnrolledButNotAttending" tabindex="14" columns="4" runat="server" maxlength="4"></asp:textbox>
                          </td>
                      </tr>
                      </table>
                  </td>
                </tr>
                <!-- Kristen Sy's suggestion to take Note: out Added by AT on 08/14/2015-->
                <tr><td colspan="2"><div style="padding: 6px; background: #e2f1f4; text-align: center; font-weight: bold;">
                      Note: The total of lines 1+2+3+4+5 should equal NUMBER OF KINDERGARTEN STUDENTS ENROLLED THIS YEAR, shown above</div>
                    </td>
                </tr>
              </table>
              <table cellspacing="0" width="100%">
                <tr><td colspan="2">*Temporary medical exemptions or foster care/homeless students.<br /><br /></td></tr>
                <tr>
                  <td valign="top" width="50%">
                    <strong>School Staff Member Completing This Form</strong>
                    <table cellspacing="0">
                      <tr>
                        <td>
                          Name:
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txtStaffPrsn" runat="server" size="30"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Email:
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txtmail" runat="server" size="30"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Phone:
                        </td>
                        <td>
                          <asp:textbox id="txtStaffPhNo" maxlength="12" runat="server" size="12"></asp:textbox>
                        </td>
                        <td>Ext:</td>
                        <td><asp:textbox id="txtStaffPhNoExt" maxlength="7" runat="server" size="7"></asp:textbox></td>
                      </tr>
                      <tr>
                        <td>
                          Date:
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txtDate" runat="server" size="30"></asp:textbox>
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td valign="top">
                    <strong>Designated School Contact</strong>
                    <table cellspacing="0">
                      <tr>
                        <td>
                          Name:
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txtfcperson" runat="server" size="30"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Email:
                        </td>
                        <td colspan="3">
                          <asp:textbox id="txtfcemail" size="30" runat="server"></asp:textbox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Phone:
                        </td>
                        <td>
                          <asp:textbox id="txtfcNumber" size="12" runat="server" maxlength="12"></asp:textbox>
                        </td>
                        <td>Ext:</td>
                        <td>
                          <asp:textbox id="txtfcNumberExt" size="7" runat="server" maxlength="7"></asp:textbox>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>                
              </table>
            </td>
          </tr>
        </table>
        <p><asp:button id="btnprevious" runat="server" Text="Previous" causesvalidation="False"></asp:button>
            <asp:button id="btndownload" runat="server" Text="Download Report (PDF)" causesvalidation="False"></asp:button>
            <asp:button id="btnprint" runat="server" Text="Print Report" causesvalidation="False"></asp:button>            
            <asp:button id="btnlogout" runat="server" Text="Logout" causesvalidation="False"></asp:button></p>
        <p>For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <hr />
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
