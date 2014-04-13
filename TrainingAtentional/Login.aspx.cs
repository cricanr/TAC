using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainingAtentional
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                int userID;
                int stage;
                string doTraining;

                int trainingSessions = Int32.Parse(ConfigurationManager.AppSettings["TrainingSessions"].ToString());

                if (DBPool.CorrectLogin(txtUser.Text, txtPassword.Text, out userID, out stage, out doTraining))
                {
                    Session["DoTraining"] = doTraining == "1";

                    MySession mySession = new MySession(userID, txtUser.Text,stage);
                    Session["mySession"] = mySession;



                    if (txtUser.Text == Settings.CONST_AdminFullName)
                        Response.Redirect("~/Admin/TrainingResults.aspx");
                    else
                    {
                        if (stage == 0)
                        {
                            Response.Redirect("~/Prepare.aspx"); 
                        }
                        else if (stage > 0 && stage <= trainingSessions)
                        {
                            Response.Redirect("~/Training.aspx"); 
                        } 
                        else if (stage == trainingSessions + 1) {
                                //Response.Redirect("~/DotProbe.aspx");
                            Response.Redirect("~/Prepare.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Thanks.aspx");
                        }
                        
                        
                    }
                }
                else
                {
                    lblMessage.Text = "Eroare la logare. Informatie incorecta";
                }
            }
            else
            {
                lblMessage.Text = "Completati campurile utilizator si parola";
            }
        }
    }
}