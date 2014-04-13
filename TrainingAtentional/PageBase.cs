using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using TrainingAtentional.Logs;

namespace TrainingAtentional
{
    
    public class BasePage : Page
    {
        #region Properties
        
        public MySession ExtraMySession
        {
            get
            {
                if (Session != null && Session["mySession"] != null)
                {
                    return Session["mySession"] as MySession;
                }
                else
                {
                    if (ViewState["mySession"] != null)
                    { return ViewState["mySession"] as MySession; }
                }

                if (Session == null)
                    Response.Redirect("~/Login.aspx?k=1",false);
                else if (Session["mySession"] == null)
                    Response.Redirect("~/Login.aspx?k=2",false);
                else if (ViewState["mySession"] == null)
                    Response.Redirect("~/Login.aspx?k=3",false);

                return null;

            }
        }

        #endregion

        public BasePage()
        {
            this.Load += new EventHandler(BasePage_Load);
        }

        void BasePage_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["mySession"] != null)
                    {
                        ViewState["mySession"] = Session["mySession"];
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx?x=3", false);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + "~~~~" + ex.StackTrace;
                if (ex.InnerException != null) error += "@@@@@@" + ex.InnerException.ToString();
                MyLogs.WriteError(error);
            }
        }





        private static string[] aspNetFormElements = new string[] 

  { 

    "__EVENTTARGET",

    "__EVENTARGUMENT",

    "__VIEWSTATE",

    "__EVENTVALIDATION",

    "__VIEWSTATEENCRYPTED",

  };


        #region Comment

        protected override void Render(HtmlTextWriter writer)
        {

            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            base.Render(htmlWriter);

            string html = stringWriter.ToString();

            int formStart = html.IndexOf("<form");

            int endForm = -1;

            if (formStart >= 0)

                endForm = html.IndexOf(">", formStart);



            if (endForm >= 0)
            {

                StringBuilder viewStateBuilder = new StringBuilder();

                foreach (string element in aspNetFormElements)
                {

                    int startPoint = html.IndexOf("<input type=\"hidden\" name=\"" + element + "\"");

                    if (startPoint >= 0 && startPoint > endForm)
                    {

                        int endPoint = html.IndexOf("/>", startPoint);

                        if (endPoint >= 0)
                        {

                            endPoint += 2;

                            string viewStateInput = html.Substring(startPoint, endPoint - startPoint);

                            html = html.Remove(startPoint, endPoint - startPoint);

                            viewStateBuilder.Append(viewStateInput).Append("\r\n");

                        }

                    }

                }



                if (viewStateBuilder.Length > 0)
                {

                    viewStateBuilder.Insert(0, "\r\n");

                    html = html.Insert(endForm + 1, viewStateBuilder.ToString());

                }

            }



            writer.Write(html);

        }

        #endregion

    }


}