<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SchoolAssessment.Models.KG.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>California Kindergarten Immunization Assessment</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
    <style type="text/css">
        .auto-style6 {
            
            float: middle;
            width: 825px;
            height: 400px;
            border-spacing: 2px;
            padding: 10px;
        }



        </style>
</head>
<body>
     <div id="container">
        <div id="header-wrap">
            <div id="header">
                <div id="logo">
                    <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
                    <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div><div id="duedateLogin">Reports due October 15</div>
                </div>
                <div id="topbnr">
                    <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions <img src="Images/Icon_instr.png" width="15" /> </a>| 
                    <a href="http://www.cairweb.org/calkidshots/KSeventhFAQs.pdf" target="_blank" alt="FAQs" title="FAQs">FAQs <img src="Images/Icon_instr.png" width="15" /> </a>|
                    Worksheet <a href="http://www.cairweb.org/calkidshots/pm236a.xls" target="_blank"><img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet" /></a>&nbsp;   
                    <a href="http://www.cairweb.org/calkidshots/KOnlineInstructions.pdf" target="_blank"> <img src="Images/Icon_adobe.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a>
               </div>
        </div>
        <div id="content-wrap">
            <div id="content">
                
                <!--<img src="Images/Title_HowToReport.png" />
                    <ul>
                        <li>This screen provides access to the IMMUNIZATION ASSESSMENT OF KINDERGARTEN STUDENTS- ANNUAL REPORT and is for reporting purposes only.</li>
                        <li>Enter your School Code (last 7 digits of your <a href="http://www.cde.ca.gov/ds/si/ds/fspubschls.asp" target="_blank">CDE Code</a>) and password and select “Log in” to access the report. If you do not know your CDE Code, contact your school administrator or search for your school information page in the <a href="http://www.cde.ca.gov/ds/si/ds/fspubschls.asp" target="_blank">California School Directory</a>.</li>
                        <li>For information about immunizations required for school entry and how to complete this report, please visit <a href="www.shotsforschool.org/reporting" target="_blank">www.shotsforschool.org/reporting</a>.</li>
                    </ul>
                -->
               <!-- <p>
                    <span class="redbold">2013-2014 reporting will open the week of September 3, 2013.</span></p>	-->		
                <!-- divide this table into 3 column -->
                
                	
                <form method="post" id="form1" runat="server">
                <table id="Table1" class="auto-style6"  >
                    <tr>
                        <td width="40px">&nbsp;</td>
                        <td width="350px"></td>
                        <td width="380px"></td>
                        <td width="30px"></td>
                    </tr>
                    <tr id="MessagesRow" runat="server">
                        <td colspan="4">
                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="RequiredFieldValidator"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td width="350px" valign="center">
                            <table width="350px" cellspacing="2">
                                
                                <tr><td class="indent2" ><img src="Images/Title_Login.png" width="70" height="25"/></td></tr>
                                <tr><td><br /></td></tr>
                                <tr>
                                    <td class="indent2" >CDE School Code
                                        <img src="./images/icon_Info.png" width="14" height="14" alt="" align="top" title='Last 7 digits of CDE Code'>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="indent2">
                                        <asp:TextBox ID="txtSchoolCode" runat="server" AutoPostBack="True" Width="175px"
                                            MaxLength="7" Enabled="true" OnTextChanged="txtSchoolCode_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<small> - Required</small>"
                                            CssClass="RequiredFieldValidator" ControlToValidate="txtSchoolCode"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="indent2" >Password</td>
                                </tr>
                                <tr>
                                    <td class="indent2">
                                        <asp:TextBox ID="txtPassword" runat="server" Width="175px" TextMode="Password" MaxLength="10" Enabled="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<small> - Required"
                                            CssClass="RequiredFieldValidator" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr><td><br /></td></tr>
                                <tr>
                                    <td class="indent2" >
                                        <asp:Button ID="btLogin" runat="server" Text="Log in" Enabled="true" OnClick="btLogin_Click"></asp:Button>
                                        
                                        <asp:ImageButton ID="ImgbtnLogin" Enabled="true" ImageUrl="images/btn5_login.png" runat="server" OnClick="ImgbtnLogin_Click" />
                                        
                                    </td>
                                </tr>
                                <tr><td><br /></td></tr>
                                <tr><td class="indent2"><a href="LoginForgotSchCode.aspx"><img src="Images/Icon_triangle.png" /> Forgot CDE School Code?</a></td></tr>
                                <tr><td class="indent2"><a href="ForgotPassword.aspx"><img src="Images/Icon_triangle.png" /> Forgot Password?</a></td></tr>
                            </table>
                        </td>
                        <!--<td style="border-left: 1px solid black; padding: 5px; height: 250px; ">-->
                        <!--<td class="auto-style8" >
                            
                            <table width="30px" height="175px" cellspacing="0"><tr><td style="text-indent: 5px;">&nbsp;</td><td style="border-left: 1px solid black; padding: 15px;">&nbsp;</td></tr></table>
                            <br />OR<br /><br />
                            <table width="30px" height="175px" cellspacing="0"><tr><td style="text-indent: 5px;">&nbsp;</td><td style="border-left: 1px solid black; padding: 15px;">&nbsp;</td></tr></table>
                            
                        </td>-->
                        <td width ="380px" align="center" >
                            <table cellspacing="2">
                                <tr><td>
                                    <asp:Label ID="LabelSchType" runat="server" Text="School Type"></asp:Label></td></tr>
                                <tr><td  ><asp:DropDownList ID="TypeList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="TypeList_SelectedIndexChanged1">
                                     </asp:DropDownList></td>
                                </tr>
                                <tr><td>
                                    <asp:Label ID="LabelCounty" runat="server" Text="County"></asp:Label></td></tr>
                                <tr><td >
                                      <asp:DropDownList ID="CountyList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="CountyList_SelectedIndexChanged">
                                      </asp:DropDownList></td></tr>
                                <tr><td >
                                    <asp:Label ID="LabelDistrict" runat="server" Text="District"></asp:Label></td></tr>
                                <tr><td >
                                    <asp:DropDownList ID="DistrictList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="DistrictList_SelectedIndexChanged">
                                    </asp:DropDownList></td></tr>
                                <tr><td >
                                    <asp:Label ID="LabelCity" runat="server" Text="City"></asp:Label></td></tr>
                                <tr><td >
                                    <asp:DropDownList ID="CityList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="CityList_SelectedIndexChanged" >
                                    </asp:DropDownList></td></tr>
                                <tr><td >
                                    <asp:Label ID="LabelSchName" runat="server" Text="School Name"></asp:Label></td></tr>
                                <tr><td >
                                    <asp:DropDownList ID="SchoolNameList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="SchoolNameList_SelectedIndexChanged">
                                    </asp:DropDownList></td></tr>
                                <tr><td >
                                    <asp:Label ID="LabelSchAddress" runat="server" Text="School Address"></asp:Label></td></tr>
                                <tr><td >
                                    <asp:DropDownList ID="SchoolAddressList" runat="server" AutoPostBack="True" Width="230PX" Enabled="true" OnSelectedIndexChanged="SchoolAddressList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td></tr>
                               
                            </table>

                        </td>
                        <td>&nbsp;</td>
                    </tr>     
                </table>
                    <br />
                    <!--
                <table id="Table2" class="auto-style8">
                    <tr><td class="auto-style9"></td>
                        <td><em><span class="small"><i>Session will time-out after 20 minutes.</i></span></em></td>
                    </tr>
                </table>
                    -->
                
                
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
        </div>
    </div>
</body>

</html>
