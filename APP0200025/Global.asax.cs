using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DomainModel;

namespace APP0200025
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //string Jfm = FormsAuthentication.HashPasswordForStoringInConfigFile("Pp90@thing", "MD5").ToLower();

            Connection.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Globals.PageSize = 10;           

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

            // Code that runs on application startup
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
            timer.Enabled = true;
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpAntiForgeryException)
            {
                Response.Clear();
                Server.ClearError(); //make sure you log the exception first
                //Response.Redirect("/errors", true);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 2;

            Session["PageSize"] = Globals.PageSize;
        }

        public void TimerElapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            //// Runs at every dat 12 AM.
            //if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            //{
            //    // Filter and get the DataTable whose DOB falls on Today's Date.
            //    DataTable dt = new DataTable();
            //    string query = "SELECT Id,Name,DOB,Email FROM Employee WHERE DATEPART(DAY, DOB) = @Day AND DATEPART(MONTH, DOB) = @Month";
            //    string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(constr))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(query))
            //        {
            //            cmd.Connection = con;
            //            cmd.Parameters.AddWithValue("@Day", DateTime.Today.Day);
            //            cmd.Parameters.AddWithValue("@Month", DateTime.Today.Month);
            //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            //            {
            //                sda.Fill(dt);
            //            }
            //        }
            //    }
            //    // Loop througn each Employee and send email.
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        // Call the send mail method.
            //        using (MailMessage mm = new MailMessage("sender@gmail.com", dr["Email"].ToString()))
            //        {
            //            mm.Subject = "Birthday Greetings";
            //            mm.Body = string.Format("<b>Happy Birthday </b>{0}<br /><br />Many happy returns of the day.", dr["Name"].ToString());
            //            mm.IsBodyHtml = true;
            //            SmtpClient smtp = new SmtpClient();
            //            smtp.Host = "smtp.gmail.com";
            //            smtp.EnableSsl = true;
            //            NetworkCredential credentials = new NetworkCredential(mm.From.Address, "xxxxxx");
            //            smtp.UseDefaultCredentials = true;
            //            smtp.Credentials = credentials;
            //            smtp.Port = 587;
            //            smtp.Send(mm);
            //        }
            //    }
            //}
        }
    }
}
