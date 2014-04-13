using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainingAtentional.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mySession"] != null)
                {
                    string userName = (Session["mySession"] as MySession).UserName;
                    if (userName != Settings.CONST_AdminFullName)
                    {
                        Response.Redirect("~/Register.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Register.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}