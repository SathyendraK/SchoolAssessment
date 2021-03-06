<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginConfirmed.aspx.cs" Inherits="SchoolAssessment._7th.LoginConfirmed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California 7th Grade Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />
    <style type="text/css">
        .auto-style1 {
            width: 475px;
        }
        .auto-style2 {
            width: 450px;
        }
        .auto-style3 {
            width: 475px;
        }
        .auto-style5 {
            width: 450px;
        }
    </style>
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
          <a href="https://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/mhheader_2.png" alt="7th Grade Immunization Assessment" /></a></div>
          <!-- Commented out by A.T. on 09/18/2014 
          <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
          <!--<div id="help"><a target="_blank" href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf ">Help</a></div>-->
          <div id="topbnr">
               <form id="Form1" method="post" runat="server">
               <a href="https://www.shotsforschool.org/reporting/7th/" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions  <img src="Images/Icon_instr.png" width="15" /> </a> | 
               <a href="https://www.shotsforschool.org/reporting/kindergarten/faqs/" target="_blank" alt="FAQs" title="FAQs"> FAQs <img src="Images/Icon_instr.png" width="15" /> </a> <!--| 
               Worksheet<a href="http://cairweb.org/calkidshots/PM236a.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://cairweb.org/calkidshots/PM236a.pdf" target="_blank"> <img src="Images/Icon_adobe.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a>--> | 
               Worksheet<a href="http://www.cairweb.org/calkidshots/IMM1272.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>
                    <a href="http://www.cairweb.org/calkidshots/IMM1272.pdf" target="_blank"> <img src="Images/Icon_pdf.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a> |
                   <asp:LinkButton ID="hdrLogout" runat="server" OnClick="hdrLogout_Click" CausesValidation="false">Logout</asp:LinkButton>
          </div>
              
      </div>
      <div id="duedate">Reporting is due November 1</div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <center><img src="Images/Step_01.png" alt="School information:" /></center>
        
            <h2><img src="Images/hdr_SchoolInfo.png" alt="Please confirm School information:" /></h2>
            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" HeaderText="Please correct the following errors:" runat="server" CssClass="ValidationSummary" />
          
          
        
        <table id="Table1" cellpadding="4">
          <tr>
            <td colspan="2">Disclaimer: Information updated on this page is for reporting purposes only. All updates to school information must be routed through the <a href="http://www.cde.ca.gov/re/sd/corrections.asp" target="_blank">California School Directory- Submitting Corrections</a> and will be reflected next year. <br /><br /></td>
          </tr>
          <tr>
            <td class="auto-style1"><b>School:</b></td>
            <td class="auto-style2"><asp:Label ID="lblSchName" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="auto-style1"><b>School Code <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Last 7 digits of CDE Code'>: </b></td>
            <td class="auto-style2"><asp:Label ID="lblSchCode" runat="server"></asp:Label></asp:Label></td>
          </tr>
          <tr>
            <td class="auto-style1"><b>Type: </b></td>
            <td class="auto-style2"><asp:Label ID="lblSchtype" runat="server"></asp:Label></td>    
          </tr>
          <tr>
            <td class="auto-style1"><b>County: </b></td>
            <td class="auto-style2"><asp:Label ID="lblCounty" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="auto-style1"><b>Public School District: </b></td>
            <td class="auto-style2"><asp:Label ID="lblDistrict" runat="server"></asp:Label></td>
          </tr>
          <!--<tr>
                <td colspan="3"><b>Administrator/Principal:</b>
                     <asp:Label ID="lblSchAdmin" runat="server" ></asp:Label>
                </td>
               
         </tr>-->
         <tr>
            <td class="auto-style1"><b>School Email:</b></td>
            <td class="auto-style2"><asp:TextBox ID="txtSchEmail" size="70" runat="server"></asp:TextBox></td>
         </tr>
         <tr>
            <td class="auto-style1"><b>Physical Address</b></td>
            <td class="auto-style2"><b>Mailing Address</b></td>
         </tr>
         <tr>
            <td class="auto-style3"><asp:Label ID="txtPhyAddress" runat="server"></asp:Label></td>
            <td class="auto-style5"><asp:Label ID="txtMailAddress" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="auto-style3">
              <asp:Label ID="txtPhyCity" runat="server"></asp:Label>
            </td>
            <td class="auto-style5">
              <asp:Label ID="txtMailCity" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="auto-style3">
              <asp:Label ID="txtPhyZip" runat="server"></asp:Label>
            </td>
            <td class="auto-style5">
              <asp:Label ID="txtMailZip" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="auto-style3">
              Do you have any 7th Grade students enrolled this year?
            </td>
            <td class="auto-style5">
              <asp:DropDownList ID="drpdwnKndrYesNo" runat="server" AutoPostBack="True" Width="120px">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
              </asp:DropDownList>
              <!-- Commented out on 07/12/2016
                  <asp:Label ID="lblConfirm" runat="server" CssClass="redbold" Width="500">- Please select a reason. Then you must 'Confirm and Continue' and 'Submit'
the following page to complete your submission!</asp:Label>-->
            </td>
            
          </tr>
          <tr>
            <td class="auto-style3">
              &nbsp;&nbsp;&nbsp; If not, why?
            </td>
            <td class="auto-style5" >
              <asp:DropDownList ID="drpReason" runat="server"><asp:ListItem Value="">--Select--</asp:ListItem>
                <asp:ListItem Value="NO 7th Grade THIS YEAR">NO 7th Grade THIS YEAR</asp:ListItem>
                <asp:ListItem Value="NO 7th Grade EVER">NO 7th EVER</asp:ListItem>
                <asp:ListItem Value="SCHOOL CLOSED">SCHOOL CLOSED</asp:ListItem>
              </asp:DropDownList>              
              <br /><asp:RequiredFieldValidator ID="valReason" ControlToValidate="drpReason" Text="Please select a reason" ErrorMessage="Please select a reason for not having any kindergarten students enrolled this year" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
            </td>
          </tr>
          <tr>
            <td class="auto-style3">
              Are students 
              schooled at home? <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='"homeschooled students” should be understood to include students who are: &#13; &#149; Categorized as homeschooled by a state or local authority, even if enrolled in public or private school classes, sports, or other extracurricular activities to &#160;&#160;&#160;&#160; supplement homeschool study &#13; &#149; OR enrolled in public or private online academies, but do not attend classes on campus.'> 
            </td>
            <td class="auto-style5">
              <asp:DropDownList ID="drphome" runat="server" Width="120px"><asp:ListItem Value="No">No</asp:ListItem>
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
              </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td class="auto-style3">
              Is this an online/virtual  
              school? <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Type of virtual instruction offered by the school. Virtual instruction is instruction in which students and teachers are separated by time and/or location, and interaction occurs via computers and/or telecommunications technologies)'> 
            </td>
            <td class="auto-style5">
              <asp:DropDownList ID="drpvirtual" runat="server" Width="120px"><asp:ListItem Value="No">No</asp:ListItem>
                <asp:ListItem Value="F">Full Virtual</asp:ListItem>
                <asp:ListItem Value="P">Partial Virtual</asp:ListItem>
              </asp:DropDownList>
            </td>
          </tr>
        </table>
        <p>
          
          <!--<asp:Button ID="btnEdit" runat="server" Text="Edit School Info" Width="140px" OnClick="btnEdit_Click"></asp:Button>-->
          <!--<asp:Button ID="btnPrintView" runat="server" Text="View Completed Report" Enabled="false" Width="190px" OnClick="btnPrintView_Click"></asp:Button>-->
          <!-- Added btnReset by A.T. on 09/04/2014 -->
          
          
          <!--<asp:Button ID="btnExit" runat="server" Text="Logout" Width="100px" CausesValidation="false" OnClick="btnExit_Click"></asp:Button>-->
            <asp:ImageButton ID="ImgBtnNext" Enabled="true" ImageUrl="images/btn4_next.png" runat="server" OnClick="ImgBtnNext_Click"/></p>
             <!--<p><span class="redbold">Electronic reporting has closed for this school year. Schools that have not yet reported should contact their <a href="https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx" target="_blank">local health department</a> or email <a href="mailto:SchoolAssessments@cdph.ca.gov">SchoolAssessments@cdph.ca.gov</a></span></p>-->
           
          
            <!-- Commented out by AT on 06/20/2016 by Kristen Sy's request
            <p><span class="small">* A public school where students are schooled at home, or a private school with a Private School Affidavit registered with the Department of Education<br />
          ** A school where attendance, teacher interaction, and daily lessons are conducted mainly online and lessons may use physical materials as well as offline tools</span></p>
        -->
        </br>
        <hr />
        <p>For questions about assessment, contact your <a href="https://www.cdph.ca.gov/Programs/CID/DCDC/Pages/Immunization/Local-Health-Department.aspx" target="_blank">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="http://shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation"><i>Session will automatically time out in 20 minutes.</i><br />You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
        <input id="hdnSchCode" type="hidden" name="hdnSchCode" runat="server" />
        <input id="hdnKinderYesNo" type="hidden" name="hdnKinderYesNo" runat="server" />
        <input id="hdnReason" type="hidden" name="hdnReason" runat="server" />
        <input id="hdnVirtual" type="hidden" name="hdnVirtual" runat="server" />
        <input id="hdnHome" type="hidden" name="hdnHome" runat="server" />
        <!-- Added below by A.T. on 09/04/2014 -->
        <input id="hdnIsComplete" type="hidden" name="hdnIsComplete" runat="server" />
        <input id="hdnLastResetDate" type="hidden" name="hdnLastResetDate" runat="server" />
        <input id="hdnLhdReviseDate" type="hidden" name="hdnLhdReviseDate" runat="server" /> 
        <input id="hdnReviseSubmittedRPT" type="hidden" name="hdnReviseSubmittedRPT" runat="server" /> 
        <input id="hdnSchEmail" type="hidden" name="hdnSchEmail" runat="server" /> 
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
