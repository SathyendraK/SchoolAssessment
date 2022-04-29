<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginConfirmed.aspx.cs" Inherits="SchoolAssessment.KG.LoginConfirmed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />
    <style type="text/css">
        .auto-style1 {
            width: 248px;
        }
        .auto-style2 {
            width: 294px;
        }
        .auto-style3 {
            width: 266px;
        }
        .auto-style4 {
            width: 617px;
        }
    </style>
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
          <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15</div>
        <!-- Commented out by A.T. on 09/18/2014 
            <div id="help"><a href="http://shotsforschool.org/reportingtools.html">Help</a></div>-->
            <div id="help"><a target="_blank" href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf ">Help</a></div>
               
      </div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <center><img src="Images/Step_01.png" alt="School information:" /></center>
        <form id="Form1" method="post" runat="server">
            <h2><img src="Images/hdr_SchoolInfo.png" alt="Please confirm School information:" /></h2>
            &nbsp;<asp:ValidationSummary ID="valSum" DisplayMode="BulletList" HeaderText="Please correct the follwoing errors:" runat="server" CssClass="ValidationSummary" />
        <table id="Table1" cellpadding="4">
          <tr>
            <td colspan="3" class="auto-style2">
              <b>School: </b><asp:Label ID="lblSchName" runat="server"></asp:Label>
            </td>
          </tr>
            <tr>
                <td class="auto-style4"><b>Type: </b>
                    <asp:Label ID="lblSchtype" runat="server"></asp:Label></td>
                <td colspan="2">
                    <b>School Code: </b>
                    <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Last 7 digits of CDE Code'>
                    <asp:Label ID="lblSchCode" runat="server"></asp:Label></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td class="auto-style4">
                    <b>County: </b>
                    <asp:Label ID="lblCounty" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <b>Public School District: </b>
                    <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                </td>
            </tr>

             <tr>
                <td colspan="2"><b>Administrator/Principal:</b>
                     <asp:Label ID="lblSchAdmin" runat="server" ></asp:Label>
                </td>
                <td class="auto-style3" ></td>
            </tr>
            <tr>
                <td colspan="2"><b>School Email:</b>
                    <asp:Label ID="lblSchEmail" runat="server"></asp:Label></td>
                <td colspan="2" class="auto-style3"></td>
            </tr>
          <tr>
            <td class="auto-style4">
              <b>Physical Address</b>
            </td>
            <td class="auto-style1">
              <b>Mailing Address</b>
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              <asp:Label ID="txtPhyAddress" runat="server"></asp:Label>
            </td>
            <td class="auto-style1">
              <asp:Label ID="txtMailAddress" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              <asp:Label ID="txtPhyCity" runat="server"></asp:Label>
            </td>
            <td class="auto-style1">
              <asp:Label ID="txtMailCity" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              <asp:Label ID="txtPhyZip" runat="server"></asp:Label>
            </td>
            <td class="auto-style1">
              <asp:Label ID="txtMailZip" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              Do you have any kindergarten<br /> 
              students enrolled this year?
            </td>
            <td>
              <asp:DropDownList ID="drpdwnKndrYesNo" runat="server" AutoPostBack="True" Width="120px">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
              </asp:DropDownList>
              <!-- Commented out on 07/12/2016
                  <asp:Label ID="lblConfirm" runat="server" CssClass="redbold" Width="500">- Please select a reason. Then you must 'Confirm and Continue' and 'Submit'
the following page to complete your submission!</asp:Label>-->
            </td>
            <td class="auto-style3"></td>
          </tr>
          <tr>
            <td class="auto-style4">
              &nbsp;&nbsp;&nbsp; If not, why?
            </td>
            <td colspan="2">
              <asp:DropDownList ID="drpReason" runat="server"><asp:ListItem Value="">--Select--</asp:ListItem>
                <asp:ListItem Value="NO KINDERGARTEN THIS YEAR">NO KINDERGARTEN THIS YEAR</asp:ListItem>
                <asp:ListItem Value="NO KINDERGARTEN EVER">NO KINDERGARTEN EVER</asp:ListItem>
                <asp:ListItem Value="SCHOOL CLOSED">SCHOOL CLOSED</asp:ListItem>
              </asp:DropDownList>              
              <br /><asp:RequiredFieldValidator ID="valReason" ControlToValidate="drpReason" Text="Please select a reason" ErrorMessage="Please select a reason for not having any kindergarten students enrolled this year" runat="server" CssClass="RequiredFieldValidator" Display="Dynamic" />
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              Are students 
              schooled at home? <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='"homeschooled students” should be understood to include students who are: &#13; &#149; Categorized as homeschooled by a state or local authority, even if enrolled in public or private school classes, sports, or other extracurricular activities to &#160;&#160;&#160;&#160; supplement homeschool study &#13; &#149; OR enrolled in public or private online academies, but do not attend classes on campus.'> 
            </td>
            <td colspan="2">
              <asp:DropDownList ID="drphome" runat="server" Width="120px"><asp:ListItem Value="No">No</asp:ListItem>
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
              </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td class="auto-style4">
              Is this an online/virtual  
              school? <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Type of virtual instruction offered by the school. Virtual instruction is instruction in which students and teachers are separated by time and/or location, and interaction occurs via computers and/or telecommunications technologies)'> 
            </td>
            <td colspan="2">
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
          
          <asp:Button ID="btnExit" runat="server" Text="Logout" Width="100px" CausesValidation="false" OnClick="btnExit_Click"></asp:Button>
          <asp:Button ID="btnConfirm" runat="server" Text="Next" Width="100px" OnClick="btnConfirm_Click"></asp:Button>

           </p>
          
            <!-- Commented out by AT on 06/20/2016 by Kristen Sy's request
            <p><span class="small">* A public school where students are schooled at home, or a private school with a Private School Affidavit registered with the Department of Education<br />
          ** A school where attendance, teacher interaction, and daily lessons are conducted mainly online and lessons may use physical materials as well as offline tools</span></p>
        -->
        </br>
        <hr />
        <p>For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="http://shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation">You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
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
