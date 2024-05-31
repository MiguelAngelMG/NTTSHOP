using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Example : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] == null && Session["session-language"] == null)
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }
            if (!IsPostBack && Session["session-id"] != null)
            {

            }
            else
            {
                Response.Redirect("~/WebForms/LoginPage.aspx");
            }
        }
    }
}