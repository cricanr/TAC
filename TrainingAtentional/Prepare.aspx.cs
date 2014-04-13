using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainingAtentional
{
    public partial class Prepare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DoTraining"] != null && (bool)Session["DoTraining"] == false)
            {
                divTraining.Visible = false;
                divNontraining.Visible = true;
            }
            else
            {
                divTraining.Visible = true;
                divNontraining.Visible = false;
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DotProbe.aspx");
        }
    }
}