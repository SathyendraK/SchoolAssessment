<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDetails.aspx.cs" Inherits="SchoolAssessment.KG.EditDetails" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head>
  <title>California Kindergarten Immunization Assessment</title><link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header"> <div id="logo">
          <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
          <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reporting is due January 29, 2021</div>
        <!--Commented out by A.T. on 09/18/2014 
            <div id="help"><a href="https://shotsforschool.org/reportingtools.html">Help</a></div>-->
        <div id="help"><a target="_blank" href="https://www.cairweb.org/calkidshots/KOnlineInstructions.pdf">Help</a></div>
      </div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <form id="Form1" method="post" runat="server">
        <h2><img src="Images/editschoolinfo.png" alt="Edit school information:" /></h2>
        <asp:validationsummary id="Validationsummary1" displaymode="BulletList" headertext="PLEASE CORRECT THE FOLLOWING ERRORS: (Invalid fields marked with bullets)" runat="server" cssclass="ValidationSummary" />
        <table>
          <tr>
            <td>
              School Name
            </td>
            <td colspan="2">
                <asp:label id="txtSchoolName" runat="server" width="450px" maxlength="150"></asp:label>
              </td>
          </tr>
          <tr>
            <td>
              School Type
            </td>
            <td colspan="2">
              <asp:label id="txtSchoolType" runat="server"></asp:label>
            </td>
          </tr>
          <tr>
            <td>
              School Code
            </td>
            <td colspan="2">
              <asp:label id="txtSchoolCode" runat="server"></asp:label>
            </td>
          </tr>
          <tr>
            <td>
              County
            </td>
            <td colspan="2">
              <asp:label id="txtCounty" runat="server"></asp:label>
            </td>
          </tr>
          <tr>
            <td>
              Public School District
            </td>
            <td colspan="2">
              <asp:label id="txtDistrict" runat="server"></asp:label>
            </td>
          </tr>
          <tr>
            <td>
              Address
            </td>
            <td>
              <em><strong>Physical Address</strong></em>
            </td>
            <td>
              <em><strong>Mailing Address</strong></em> 
            </td>
          </tr>
          <tr>
            <td>
              Street
            </td>
              <td>
                  <asp:Label ID="txtPhyAddress" runat="server" Width="220px"></asp:Label>
              </td>
              <td>
                  <asp:Label ID="txtMailAddress" runat="server" Width="220px"></asp:Label>
              </td>
          </tr>
            <tr>
                <td>City
                </td>
                <td>
                    <asp:Label ID="txtPhyCity" runat="server" Width="220px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtMailCity" runat="server" Width="220px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Zip
                </td>
                <td>
                    <asp:Label ID="txtPhyZip" runat="server" Width="220px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtMailZip" runat="server" Width="220px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>School Email</td>
                <td>
                    <asp:TextBox ID="TextSchEmail" Width="220px" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
          <tr>
              <td>Administrator/Principal</td>
              <td>
                  <asp:TextBox ID="TextSchAdmin" width="220px" runat="server"></asp:TextBox>
              </td>
              <td></td>
          </tr>
          <tr>
            
            <td colspan="3" >
                <br />Information updated on this page is for reporting purposes only. All updates to school Information must be routed through the <a target="_blank" href="https://www.cde.ca.gov/re/sd/corrections.asp ">California School Directory- Submitting Corrections</a> and will be reflected next year.<br />
                <!--  Commented out by AT on 06/20/2016 per Kristen Sy's instruction
                <ul> 
                    <li>For private schools, email <a href="mailto:privateschools@cde.ca.gov" target="_top">privateschools@cde.ca.gov</a></li>
                    <li>For public schools, contact your <a href="https://www.cde.ca.gov/re/sd/" target="_top">CDS administrator</a></li>
                </ul>
                -->
                </br></br>
            </td>
          </tr>
        </table>
        <p>
          <asp:button id="btnEdit" runat="server" text="Update" OnClick="btnEdit_Click"></asp:button>
          <asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" OnClick="btnCancel_Click"></asp:button>
          <input id="hdnSchCode" type="hidden" name="hdnSchCode" runat="server" />
          <input id="hdnschname" type="hidden" name="hdnschname" runat="server" />          
          <input id="hdnphystreet" type="hidden" name="hdnphystreet" runat="server" />
          <input id="hdnphycity" type="hidden" name="hdnphycity" runat="server" />
          <input id="hdnphyzip" type="hidden" name="hdnphyzip" runat="server" />
          <input id="hdnmailstreet" type="hidden" name="hdnmailstreet" runat="server" />
          <input id="hdnmailcity" type="hidden" name="hdnmailcity" runat="server" />
          <input id="hdnmailzip" type="hidden" name="hdnmailzip" runat="server" />
          <input id="hdnLhdReviseDate" type="hidden" name="hdnLhdReviseDate" runat="server" />
          <input id="hdnReviseDate" type="hidden" name="hdnReviseDate" runat="server" />
          <input id="hdnSchEmail" type="hidden" name="hdnSchEmail" runat="server" />
          <input id="hdnSchAdmin" type="hidden" name="hdnSchAdmin" runat="server" />
        </p>
        <hr />
        <p>For questions about assessment, contact your <a href="https://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx" target="_blank">local health department</a> or  email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help"><em>SchoolAssessments@cdph.ca.gov</em></a></p>
        <p><a href="https://shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
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
