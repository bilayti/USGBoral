using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using System.Data;
using NewApp.Models;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

namespace NewApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest()
        {
            Response.ClearHeaders();
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Cache-Control", "no-store");
        }
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;
            
        }
        public void Application_End(object sender, EventArgs e)
        {
            //LOG SESSION TIMEOUT
            if (Session["USER_CODE"] != null)
            {

                UpdateLoginStatus(Session["USER_CODE"].ToString(), "N");

                Session["IsLoggedIn"] = null;
                AuditLogModule.LogEntry("Session Timed Out!", Session["USER_CODE"].ToString(), "N.A", "N.A", "N.A", "N.A");

                Session["LoggedOut"] = null;
                Session.Clear();
                Session.Abandon();

            }
        }
        public void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }
        public void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }
        public void Session_End(object sender, EventArgs e)
        {
            //LOG SESSION TIMEOUT
            if (Session["USER_CODE"] != null )
            {
                
                UpdateLoginStatus(Session["USER_CODE"].ToString(), "N");
                
                Session["IsLoggedIn"] = null;
                AuditLogModule.LogEntry("Session Timed Out!", Session["USER_CODE"].ToString(), "N.A", "N.A", "N.A", "N.A");

                Session["LoggedOut"] = null;
                Session.Clear();
                Session.Abandon();
                
            }
        }
        private bool IsAuthenticationTokenMatching()
        {
            if (Request.Cookies[".AUTH"] != null)
            {
                string SessionASPXAUTH = "A";
                string CookieASPXAUTH = "B";
                if (Session["ASPXAUTHToken"] != null)
                {
                    SessionASPXAUTH = Session["ASPXAUTHToken"].ToString();
                }

                CookieASPXAUTH = Request.Cookies[".AUTH"].Value;

                //auth-token manipulation check.
                if (CookieASPXAUTH == SessionASPXAUTH)
                {
                    return true;
                }
            }
            return false;
        }
        public void UpdateLoginStatus(string _iUserId, string _sStatus)
        {
            try
            {
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserId", _iUserId.ToString()));
                lstparameters.Add(new Parameters("@Status", _sStatus.ToString()));
                int result = dataAccess.ExecuteNonQuery("SP_PORTAL_UpdateLoginStatus", lstparameters);
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
    }
}