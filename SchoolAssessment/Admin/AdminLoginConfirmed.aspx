<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLoginConfirmed.aspx.cs" Inherits="SchoolAssessment.Admin.AdminLoginConfirmed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
  <script language="javascript" type="text/javascript">
    /*function Prevent_Session_Timeout() {
      var callerurl = "PreventTimeout.htm";

      if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest()
      }
      else {
        xhttp = new ActiveXObject("Microsoft.XMLHTTP")
      }
      xhttp.open("POST", callerurl, true);
      xhttp.send("");

      window.setTimeout("Prevent_Session_Timeout();", 300000);
    }

    //Initial calling
    Prevent_Session_Timeout(); */
  </script>
</head>
<frameset cols="220,*">
  <frame src="AdminMenu.aspx" name="left" ></frame>
  <frame src="blank.htm" name="right"></frame>
</frameset>
</html>

