using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;


namespace TrainingAtentional.Logs
{
    public static class MyLogs
    {
        public static void WriteError(string errorMessage)
        {

            try
            {
                string path = "~/Error/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";

                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();

                }

                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {

                    w.WriteLine("\r\nLog Entry : ");

                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +

                                  ". Error Message:" + errorMessage;

                    w.WriteLine(err);
                    //w.WriteLine(" ____ ");
                    //w.WriteLine(err


                    w.WriteLine("__________________________");

                    w.Flush();

                    w.Close();

                }

            }

            catch (Exception ex)
            {

                WriteError(ex.Message);

            }



        }
    }
}