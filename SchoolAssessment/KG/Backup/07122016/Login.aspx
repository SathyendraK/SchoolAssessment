<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SchoolAssessment.Models.KG.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>California Kindergarten Immunization Assessment</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
    <style type="text/css">
        .auto-style1 {
            /*width: 64px;*/
            height: 26px;
            width: 77px;
        }
        .auto-style2 {
            width: 208px;
            height: 26px;
        }
        .auto-style3 {
            width: 260px;
            height: 26px;
        }
        .auto-style4 {
            width: 120px;
            height: 26px;
        }
        .auto-style6 {
            background: #e2f1f4;
            float: left;
            width: 825px;
            border-spacing: 2px;
            padding: 10px;
        }
        .auto-style7 {
            height: 26px;
        }
        .auto-style8 {
            background: #b4dde1;
            width: 825px;
            
        }
        .auto-style9 {
            width: 552px;
            height: 26px;
        }
    </style>
</head>
<body>
    <div id="container">
        <div id="header-wrap">
            <div id="header">
                <div id="logo">
                    <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
                    <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15</div>
                </div>
        </div>
        <div id="content-wrap">
            <div id="content">
                
                <p>
                    <span class="redbold"><!--Kindergarten Immunization Reporting for the 2014-2015 school year
                        has now closed.
                        <br />-->
                        <!--The list of non-reporting schools has been forwarded to the CA Department of Education.--></span></p>
                <p>Instructions<br />
                    <ul>
                        <li>This screen provides access to the IMMUNIZATION ASSESSMENT OF KINDERGARTEN STUDENTS- ANNUAL REPORT and is for reporting purposes only.</li>
                        <li>Enter your School Code (last 7 digits of your <a href="http://www.cde.ca.gov/ds/si/ds/fspubschls.asp" target="_blank">CDE Code</a>) and password and select “Log in” to access the report. If you do not know your CDE Code, contact your school administrator or search for your school information page in the <a href="http://www.cde.ca.gov/ds/si/ds/fspubschls.asp" target="_blank">California School Directory</a>.</li>
                        <li>Session will time out after 1 hour.</li>
                        <li>For information about immunizations required for school entry and how to complete this report, please visit <a href="www.shotsforschool.org/reporting" target="_blank">www.shotsforschool.org/reporting</a>.</li>
                    </ul>
                </p>
                
               <!-- <p>
                    <span class="redbold">2013-2014 reporting will open the week of September 3, 2013.</span></p>	-->		
                <!-- divide this table into 3 column -->
                
                	
                <form method="post" id="form1" runat="server">
                <table id="Table1" class="auto-style6"  >
                    <tr><td colspan="5" >Search directly by:</td></tr>
                    <tr><td colspan="5"><br /></td></tr>
                    <tr id="MessagesRow" runat="server">
                        <td colspan="5">
                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="RequiredFieldValidator"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            School Code <img src="./images/helpicon.png" width="14" height="14" alt="" align="top" title='Last 7 digits of CDE Code'> 
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtSchoolCode" runat="server" AutoPostBack="True" Width="130px"
                                MaxLength="7" Enabled="true" OnTextChanged="txtSchoolCode_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<small> - Required</small>"
                                 CssClass="RequiredFieldValidator" ControlToValidate="txtSchoolCode"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style1"></td>
                        <td>
                            School Type
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="TypeList" runat="server" AutoPostBack="True" Width="260PX" Enabled="true" OnSelectedIndexChanged="TypeList_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            Password
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPassword" runat="server" Width="130px" TextMode="Password" MaxLength="10" Enabled="true" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<small> - Required"
                                CssClass="RequiredFieldValidator" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style1"></td>
                        <td>
                            County
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="CountyList" runat="server" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="CountyList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4"></td>
                       <td class="auto-style2">
                            <asp:Button ID="btLogin" runat="server" Text="Log in" Enabled="true" OnClick="btLogin_Click" ></asp:Button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        </td>
                        <td class="auto-style1" ><small>OR</small></td>
                        <td>
                            District
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="DistrictList" runat="server" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="DistrictList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="auto-style4"></td>
                       <td class="auto-style2"></td>
                        <td class="auto-style1" ></td>
                        <td>
                            City
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="CityList" runat="server" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="CityList_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="auto-style4"></td>
                        <td class="auto-style2">
                            
                        </td>
                        <td class="auto-style1"></td>
                        <td class="auto-style7">
                            School Name
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="SchoolNameList" runat="server" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="SchoolNameList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style1"></td>
                        <td>
                            School Address
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="SchoolAddressList" runat="server" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="SchoolAddressList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <!--
                    <tr>
                        <td class="boldBgLogin" colspan="4"><a href="ForgotPassword.aspx"><span class="small">Forgot Password or can't find your school?</span></a></td>
                        <td class="auto-style3" style="background-color: #b4dde1;">
                            <em><span class="small"><i>Session will time-out after 1 hour.</i></span></em>
                        </td>
                    </tr>
                    -->
                     
                </table>
                <table id="Table2" class="auto-style8">
                    <tr><td class="auto-style9"><a href="ForgotPassword.aspx"><span class="small">Forgot Password or can't find your school?</span></a></td>
                        <td><em><span class="small"><i>Session will time-out after 1 hour.</i></span></em></td>
                    </tr>
                </table>
                
                
                <!-- Commented out by AT on 09/25/2015 -->
                <!--
                <p>
                    Download <a href="http://www.adobe.com/products/reader.html">Adobe PDF Reader</a>
                    to view documents</p>
                <p>
                    For questions about assessment, contact your <a href="http://www.cdph.ca.gov/programs/immunize/Pages/CaliforniaLocalHealthDepartments.aspx">
                        local health department</a> or email <a href="mailto:SchoolAssessments@cdph.ca.gov?subject=Kindergarten%20Reporting%20Help">
                            <em>SchoolAssessments@cdph.ca.gov</em></a>
                </p>
                -->
                    <!-- commented out by AT on 06/16/2016 
                <p>
                    For questions about assessment or help with logging in, please follow the procedures in the FAQs and instructions above.

                </p>
                        -->

                <hr />
                <p>
                    <a href="http://shotsforschool.org"><img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a>
                     <a href ="http://www.adobe.com/products/reader.html" target="_blank"><img align="right" src="Images/get_adobe_reader.png" alt="Adobe Download" /></a>
                </p>
                <p>
                    <span class="regulation">You are required to submit this report in accordance with California
                        Health and Safety Code section 120375 and California Code of Regulation section
                        6075.</span></p>
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
