<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SchoolAssessment._7th.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>California Kindergarten Immunization Assessment</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
  <div id="container">
    <div id="header-wrap">
      <div id="header">
        <div id="logo">
                    <a href="https://www.shotsforschool.org/reporting/" target="_blank"><img src="Images/mhheader_2.png" alt="7th Grade Immunization Assessment" /></a></div>
          <div id="topbnr">
              
               <a href="https://www.shotsforschool.org/reporting/7th/" target="_blank" alt="Instruction" title="Reporting Instruction">Instructions  <img src="Images/Icon_instr.png" width="15" /> </a>| 
               <a href="https://www.shotsforschool.org/reporting/kindergarten/faqs/" target="_blank" alt="FAQs" title="FAQs"> FAQs <img src="Images/Icon_instr.png" width="15" /></a> <!--| 
               Worksheet<a href="http://cairweb.org/calkidshots/PM236a.xls"target="_blank"> <img src="Images/Icon_Excel.png" width="15" height="15" alt="Xls" title="Xls Worksheet"/> </a>&nbsp; <a href="http://cairweb.org/calkidshots/PM236a.pdf" target="_blank"><img src="Images/Icon_adobe.png" width="15" height="15" alt="PDF" title="PDF Worksheet" /></a>  -->
               
          </div>
            </div>
<div id="duedate">
    <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
    <!--2016-2017 Reporting is Closed-->Reporting is due January 31, 2022</div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <form method="post" id="form1" runat="server">
        <h2>
          <img src="Images/passwordhelp.png" alt="Password Help" /></h2>
        <ul>
          <li><strong>Your login password is <font color="red">"shotsforschool"</font></strong></li>
        </ul>
            <p>
                &nbsp;</p>
        <p><asp:ImageButton ID="ImgBtnBack" Enabled="true" ImageUrl="images/btn1_back.png" runat="server" OnClick="ImgBtnBack_Click" />
            </p>
        <hr />
        <p><a href="http://shotsforschool.org">
          <img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
        <p><span class="regulation"><i>Session will automatically time out in 20 minutes.</i><br />You are required to submit this report in accordance with California Health and Safety Code section 120375 and California Code of Regulation section 6075.</span></p>
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