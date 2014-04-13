using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainingAtentional
{
    public partial class Register : System.Web.UI.Page
    {
        bool DoTraining = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            if (!IsUniqueUser(txtUser.Text))
            {
                lblMessage.Text = "Va rugam folositi alta valoare pentru campul Utilizator";
                return;
            }

            int userID = 0;
            
            //string browserResolution = hScreenWidth.Value + " x " + hScreenHeight.Value +  " DPI:" + hScreenDPI.Value;
            
            //string stringDoTraining = DoTraining?"1":"0";
            string stringDoTraining = "Control";

            bool result = DBPool.InsertUser(txtUser.Text, txtPassword.Text, txtLastName.Text, txtFirstName.Text, Int32.Parse(ddlGender.SelectedValue), Int32.Parse(txtAge.Text), Int32.Parse(ddlDominantHand.SelectedValue), Int32.Parse(ddlPerifericInterface.SelectedValue), stringDoTraining, out userID);
            if (!result)
            {
                lblMessage.Text = "Eroare la inserare user";
                return;
            }
            else
            {
                MySession mySession = new MySession(userID, txtUser.Text,0);
                Session["mySession"] = mySession;

                if (txtUser.Text == Settings.CONST_AdminFullName)
                    Response.Redirect("~/TrainingResults.aspx");
                else
                {
                    Response.Redirect("~/Prepare.aspx");
                    
                }
            }
            


        }
        private bool IsValid()
        {
            if (txtLastName.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Nume";
                return false;
            }
            if (txtFirstName.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Prenume";
                return false;
            }
           
            if (txtAge.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Varsta";
                return false;
            }
            if (txtUser.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Utilizator";
                return false;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Parola";
                return false;
            }
            if (txtCode.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Nu ati completat campul Cod";
                return false;
            }

            if (ddlDominantHand.SelectedIndex == 0)
            {
                lblMessage.Text = "Nu ati completat campul Mana dominanta";
                return false;
            }
            if (ddlGender.SelectedIndex == 0)
            {
                lblMessage.Text = "Nu ati completat campul Sex";
                return false;
            }
            if (ddlPerifericInterface.SelectedIndex == 0)
            {
                lblMessage.Text = "Nu ati completat campul Interfata periferica";
                return false;
            }
            


            int age;
            if (!Int32.TryParse(txtAge.Text, out age))
            {

                lblMessage.Text = "Campul Varsta trebuie completat cu o valoare numerica intreaga";
                return false;
            }



            if (txtCode.Text != Settings.CONST_NormalUserCode && txtCode.Text != Settings.CONST_NormalUserCodeForNoTraining)
            {
                lblMessage.Text = "Nu ati introdus corect campul Cod";
                return false;
            }
            else
            {
                DoTraining = txtCode.Text == Settings.CONST_NormalUserCode;
                Session["DoTraining"] = DoTraining;
            }

            

            return true;
        }

        private bool IsUniqueUser(string userName)
        {
            bool result = DBPool.IsUniqueUser(userName);
            return result;
        }
    }
}