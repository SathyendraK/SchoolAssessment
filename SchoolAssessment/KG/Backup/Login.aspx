<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SchoolAssessment.Models.KG.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>California Kindergarten Immunization Assessment</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
    <div id="container">
        <div id="header-wrap">
            <div id="header">
                <div id="logo">
                    <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
                    <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedate">Reports due<br /> October 15, 2015</div>
                </div>
        </div>
        <div id="content-wrap">
            <div id="content">
                
                <p>
                    <span class="redbold"><!--Kindergarten Immunization Reporting for the 2014-2015 school year
                        has now closed.
                        <br />-->
                        <!--The list of non-reporting schools has been forwarded to the CA Department of Education.--></span></p>
                
               <!-- <p>
                    <span class="redbold">2013-2014 reporting will open the week of September 3, 2013.</span></p>	-->			
                <form method="post" id="form1" runat="server">
                <table id="Table1" class="loginbox">
                    <tr id="MessagesRow" runat="server">
                        <td colspan="2">
                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="RequiredFieldValidator"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            School Type
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeList" runat="server" AutoPostBack="True" Width="280px" Enabled="true" OnSelectedIndexChanged="TypeList_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            County
                        </td>
                        <td>
                            <asp:DropDownList ID="CountyList" runat="server" AutoPostBack="True" Width="280px" Enabled="true" OnSelectedIndexChanged="CountyList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            District
                        </td>
                        <td>
                            <asp:DropDownList ID="DistrictList" runat="server" AutoPostBack="True" Width="280px" Enabled="true" OnSelectedIndexChanged="DistrictList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            School Name
                        </td>
                        <td>
                            <asp:DropDownList ID="SchoolNameList" runat="server" AutoPostBack="True" Width="280px" Enabled="true" OnSelectedIndexChanged="SchoolNameList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            School Address
                        </td>
                        <td>
                            <asp:DropDownList ID="SchoolAddressList" runat="server" AutoPostBack="True" Width="280px" Enabled="true" OnSelectedIndexChanged="SchoolAddressList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            School Code
                        </td>
                        <td>
                            <asp:TextBox ID="txtSchoolCode" runat="server" AutoPostBack="True" Width="140px"
                                MaxLength="7" Enabled="true" OnTextChanged="txtSchoolCode_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" - Required"
                                CssClass="RequiredFieldValidator" ControlToValidate="txtSchoolCode"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" Width="140px" TextMode="Password" MaxLength="10" Enabled="true" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" - Required"
                                CssClass="RequiredFieldValidator" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btLogin" runat="server" Text="Log in" Enabled="true" OnClick="btLogin_Click" ></asp:Button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="ForgotPassword.aspx"><span class="small">Forgot
                                Password?</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <em><span class="small">Note: The session will time-out in one hour.</span></em>
                        </td>
                    </tr>
                </table>
                <h2>
                    <img src="Images/reportingtools.png" alt="Reporting Tools" /></h2>
                <ul>
                    <!--<li><a href="http://cairweb.org/calkidshots/K7FallMemo.pdf" target="_blank">Memo to
                        Schools</a></li>-->
                    <li><a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank">
                        Instructions</a></li>
                    <li><a href="http://www.cairweb.org/calkidshots/KSeventhFAQs.pdf" target="_blank">FAQs</a></li>
                    <li>Worksheet <a href="http://www.cairweb.org/calkidshots/pm236a.pdf"
                        target="_blank">pdf</a> | <a href="http://www.cairweb.org/calkidshots/pm236a.xls"
                            target="_blank">xls</a></li>
                    <!-- Commented out by A.T. on 09/15/2014 <li>Worksheet <a href="http://www.cdph.ca.gov/programs/immunize/Documents/PM236A.pdf"
                        target="_blank">pdf</a> | <a href="http://www.cdph.ca.gov/programs/immunize/Documents/PM236A.xls"
                            target="_blank">xls</a></li>
                    <li><a href="http://eziz.org/assets/docs/IMM-231.pdf" target="_blank">Immunization Guide</a></li>
                    <li><a href="http://cairweb.org/how-cair-helps-schools-and-child-care/">How the CA Immunization Registry Can Help Schools</a></li>-->
                    <li><a href="http://www.cairweb.org/calkidshots/KPrintConfirmation.pdf" target="_blank">
                        How to verify and print your report submission</a></li>
                </ul>
                    <br /><br /><br /><br /><br />
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
                <p>
                    For questions about assessment or help with logging in, please follow the procedures in the FAQs and instructions above.

                </p>
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
