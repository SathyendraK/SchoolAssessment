<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SchoolAssessment.KG.ForgotPassword" %>

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
                    <img src="Images/kheader.png" alt="Kindergarten Immunization Assessment" /></div>
            </div>
<div id="duedate">
    <!-- Removed the year from the Reports due by AT on 08/25/2015 -->
    Reports due<br /> October 15</div>
    </div>
    <div id="content-wrap">
      <div id="content">
        <form method="post" id="form1" runat="server">
        <h2>
          <img src="Images/passwordhelp.png" alt="Password Help" /></h2>
        <ul>
          <li><strong>Your login password is <font color="red">"school"</font></strong></li>
          <li>Please read the <a href="http://www.cairweb.org/calkidshots/KQuickGuide.pdf" target="_blank">Kindergarten Immunization Assessment Quick Start Guide</a> for more instructions.</li>
          <!-- Commented out by A.T. on 09/29/2014 
            <li>You are required to set a new password after you complete this year&#39;s report.<br />
            If you have forgotten your new password, email PasswordHelp@shotsforschool.org.<br />
            <strong>Include your School Code or full physical address of your school and &#39;Kindergarten&#39;</strong> in the email.</li>
          <li>If you have non-password related questions, email reporting-help@shotsforschool.org.</li>-->
        </ul>
        <p><a href="Login.aspx">&laquo; Back to Login</a></p>
        <hr />
        <p><a href="http://shotsforschool.org">
          <img src="Images/shotsforschool_smlogo.png" alt="ShotsForSchool.org" /></a></p>
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