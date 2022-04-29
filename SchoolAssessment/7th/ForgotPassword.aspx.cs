using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolAssessment._7th
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx", true);
        }

    }
}