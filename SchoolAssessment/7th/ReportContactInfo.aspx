                                                                     <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportContactInfo.aspx.cs" Inherits="SchoolAssessment._7th.ReportContactInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California 7th Grade Immunization Assessment Report Contact Info</title>
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
  <!-- Added below by AT on 10/22/2014 to prevent flashing at autopostback="True" -->
  <meta http-equiv="Page-Enter" content="revealTrans(Duration=0,Transition=5)">
    <style type="text/css">
        .auto-style2 {
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
            width: 160px;
        }
            


         </style>
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
        <center><img src="Images/step_7_2.png" alt="School information:" /></center>
        <h2><img src="Images/hdr_contact.png" alt="Please confirm School information:" /></h2>
        <asp:label id="ErrorMsg" runat="server"></asp:label>
        <asp:validationsummary id="valSum" displaymode="BulletList" headertext="Please correct the following errors:" runat="server" cssclass="ValidationSummary" />
          <table width="800px">
              <tr>
                  <td>

                      <table cellspacing="0" width="100%">
                          <tr>
                              <td>
                                  <p class="MsoNormal">
                                      Disclaimer: Information updated on this page is for reporting purposes only. All updates to school information must be routed through the <a href="http://www.cde.ca.gov/re/sd/corrections.asp">California School Directory- Submitting Corrections </a>and will be reflected next year.
                                  </p>
                                  <br />
                              </td>

                          </tr>
                      </table>

          <table cellspacing="0" width="800px">
              <tr>
                  <td class="auto-style4">
                      <b>School Code:</b>
                  </td>
                  <td colspan="6">
                      <asp:Label ID="lblSchCode" runat="server"></asp:Label>
                  </td>
                  <td colspan="6"></td>
              </tr>
              <tr>
                  <td class="auto-style4">
                      <b>School Name:</b>
                  </td>
                  <td colspan="6">
                      <asp:Label ID="lblSchName" runat="server"></asp:Label>
                  </td>
                  <td></td>
              </tr>
              <tr>
                  <td class="auto-style4"><b>7th Grade Status:</b></td>
                  <td colspan="6">
                      <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style4"><b>8th Grade Status:</b></td>
                  <td>
                      <asp:Label ID="lblStatus_8th" runat="server"></asp:Label></td></tr>
              <tr>
                  <td colspan="8">
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="4" class="auto-style2"><strong>School Staff Member Completing This Form</strong></td>
                  
              </tr>
              <tr>
                  <td class="auto-style4">Name:</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtStaffPrsn" runat="server" Width="500px" TabIndex="14" MaxLength="50"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator5" runat="server"  Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;"  ErrorMessage="" OnServerValidate="CustomValidator5_ServerValidate"></asp:CustomValidator>
                  </td>
                  
                  
              </tr>
              <tr>
                  <td class="auto-style4">Email:</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtmail" runat="server" Width="500px" TabIndex="15" MaxLength="50" ></asp:TextBox><!--KeyPressChanged="txtmail_TextChanged"-->
                      <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Please remove space from Staff Member Completing This Form Email." OnServerValidate="CustomValidator_txtEmailValidateSpace"></asp:CustomValidator>
                       </td>
                  
              </tr>
              <tr>
                  <td class="auto-style4">Confirm Email:</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtconfirmmail" runat="server" Width="500px" TabIndex="16" MaxLength="50"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="" OnServerValidate="CustomValidator2_ServerValidateSpace"></asp:CustomValidator>
                     </td>
                  
              </tr>
              <tr>
                  <td class="auto-style4">Phone:

                  </td>
                  <td class="auto-style2" colspan="3">
                      <asp:TextBox ID="txtStaffPhNo" MaxLength="3" runat="server" Width="35px" TabIndex="17"></asp:TextBox><b>-</b>
                      <asp:TextBox ID="txtStaffPhNo_1" runat="server" MaxLength="3" Width="35px" TabIndex="18"></asp:TextBox><b>-</b>
                      <asp:TextBox ID="txtStaffPhNo_2" runat="server" MaxLength="4" Width="40px" TabIndex="19"></asp:TextBox>
                      &nbsp;Ext:
                      <asp:TextBox ID="txtStaffPhNoExt" MaxLength="4" Columns="4" runat="server" TabIndex="20" Width="35px"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator_txtStaffPhNo" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Staff Member Completing This Form Phone - Required" OnServerValidate="CustomValidator_txtStaffPhNo_ServerValidate"></asp:CustomValidator>
                      <asp:CustomValidator ID="CustomValidator_txtStaffPhNo_Digit" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="School Staff Member Completing This Form Phone - Invalid character" OnServerValidate="CustomValidator_txtStaffPhNo_Digit_ServerValidate"></asp:CustomValidator>


                  </td>


                  


              </tr>
              <tr>
                  <td></BR></td>
              </tr>
              <tr>
                <td colspan="6" class="auto-style2"><strong>Designated School Contact</strong><span class="small">(<asp:CheckBox ID="chkcontact" runat="server" AutoPostBack="True" TabIndex="21" OnCheckedChanged="chkcontact_CheckedChanged" /></asp:checkbox>Check if same person)</span>
                </td>
              </tr>
              <tr>
                  <td>Name:</td>
                  <td colspan="4">
                      <asp:TextBox ID="txtfcperson" runat="server" Width="500px" TabIndex="22" MaxLength="50"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator6" runat="server"  Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;"  ErrorMessage="" OnServerValidate="CustomValidator6_ServerValidate"></asp:CustomValidator>
                  </td></tr>
              <tr>
                  <td>Email:</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtfcemail" Width="500px" runat="server" TabIndex="23" MaxLength="50"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="" OnServerValidate="CustomValidator3_ServerValidate"></asp:CustomValidator>
                      </td>
              </tr>
              <tr>
                  <td>Confirm Email:</td>
                  <td colspan="3">
                      <asp:TextBox ID="txtfccemail" Width="500px" runat="server" TabIndex="24" MaxLength="50"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator4" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="" OnServerValidate="CustomValidator4_ServerValidate"></asp:CustomValidator>
                      </td>
              </tr>
              <tr>
                  <td class="auto-style4">Phone:
                      </td>
                  <td class="auto-style2" colspan="3">
                      <asp:TextBox ID="txtfcNumber" runat="server" MaxLength="3" TabIndex="25" Width="35px"></asp:TextBox>
                      <b>-</b>
                      <asp:TextBox ID="txtfcNumber_1" runat="server" MaxLength="3" TabIndex="26" Width="35px"></asp:TextBox>
                      <b>-</b>
                      <asp:TextBox ID="txtfcNumber_2" runat="server" MaxLength="4" TabIndex="27" Width="40px"></asp:TextBox>
                      &nbsp;
                          Ext:
                      <asp:TextBox ID="txtfcNumberExt" runat="server" MaxLength="4" Columns="4" TabIndex="28" Width="35px"></asp:TextBox>
                      <asp:CustomValidator ID="CustomValidator_txtfcNumber" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Designated School Contact Phone - Required" OnServerValidate="CustomValidator_txtfcNumber_ServerValidate"></asp:CustomValidator>
                      <asp:CustomValidator ID="CustomValidator_txtfcNumber_Digit" runat="server" Display="Dynamic" CssClass="RequiredFieldValidator" Text="&bull;" ErrorMessage="Designated School Contact Phone - Invalid character" OnServerValidate="CustomValidator_txtfcNumber_Digit_ServerValidate"></asp:CustomValidator>
                  
                  </td>
              </tr>
            </table>

            </td>
           </tr>
          </table>


        <p>&nbsp;</p>
        <p>
            <asp:ImageButton ID="ImgBtnBack" causesvalidation="False" tabindex="29" Enabled="true" ImageUrl="images/btn1_back.png" runat="server" OnClick="ImgBtnBack_Click" />
            <asp:ImageButton ID="ImgBtnNext"  tabindex="30" Enabled="true" ImageUrl="images/btn4_next.png" runat="server" OnClick="ImgBtnNext_Click" />
        </p> 
        
        <hr />
                <br />
        <p>For questions about assessment, contact your <a href="https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx" target="_blank">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="https://www.shotsforschool.org">
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
            <input id="hdnTxtPreJanuaryExmpt" type="hidden" name="hdnTxtPreJanuaryExmpt" runat="server" />
            <input id="hdnTxtHealthCareExmpt" type="hidden" name="hdnTxtHealthCareExmpt" runat="server" />
            <input id="hdnTxtReligiousExmpt" type="hidden" name="hdnTxtReligiousExmpt" runat="server" />
            <input id="hdnEnrolledButNotAttending" type="hidden" name="hdnEnrolledButNotAttending" runat="server" />
        </form>
      </div>
    </div>
    <div id="footer-wrap">
    </div>
  </div>
</body>
</html>

