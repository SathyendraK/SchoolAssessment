<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="SchoolAssessment.Admin.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Admin Login</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<script type="text/javascript">
  function checkTopWindow() {
    if (window.top != window.self) {
      top.window.location = self.window.location;
    }
  }
</script>
<body onload="checkTopWindow()">
  <div id="container" style="width: 700px; background: #b4dde3; margin: 1em auto 0; padding: 1em">
    <form id="form1" runat="server">
    <p><img src="Images/aheader.png" alt="Immunization Assessment Admin" /></p>    	
	<!--<p align="center"><span class="redbold">LHD admin access has closed for the fall and will open in the spring for updating lists.</span></p>-->
    <!--<p align="center"><span class="redbold">The School Assessment admin site will not be available for a few days as of Tuesday August 4, 4:30 PM PDT to revise some of the data.</span></p>-->
	<asp:label id="lblMsg" runat="server" visible="false"></asp:label>
    <table cellspacing="0" style="margin-left:auto; margin-right:auto;">
      <tr>
        <td>
          Email:
        </td>
      </tr>
      <tr>
        <td>
          <asp:textbox runat="server" id="txtUsername" columns="40"></asp:textbox>
        </td>
      </tr>
      <tr>
        <td>
          Password:
        </td>
      </tr>
      <tr>
        <td>
          <asp:textbox runat="server" id="txtPassword" textmode="Password" columns="40"></asp:textbox>
        </td>
      </tr>
      <tr>
        <td>
          <asp:button runat="server" id="btnLogin" text="Login" OnClick="btnLogin_Click"  />
        </td>
      </tr>
    </table>
    <p>&nbsp;</p>	
    <!-- Commented out by AT on 
        /16/2014 -->
    <!--<p align="center">For technical assistance, please call (510) 620-3757 or (510) 620-3746</p>-->
    <p align="center">&nbsp;</p>
    </form>
  </div>
</body>
</html>
