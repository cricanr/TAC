using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainingAtentional
{
    public partial class Site : System.Web.UI.MasterPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            //Session["mySession"] = null;
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}