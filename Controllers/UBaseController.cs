using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using NewApp.Models;

namespace NewApp.Controllers
{
    public class UBaseController : Controller
    {
        public UBaseController()
        {

        }
        protected override void OnActionExecuting(ActionExecutingContext filtercontext)
        {
                ProtectFromASPXAUTHManipulation();
                CheckAccess();
                //CSRFCheck();
                return;
        }
        private void CheckAccess()
        {
            //else check if user is admin,
            int usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
            if (usertypeid != 3)
            {
                //if user is not Admin then redirect to NoAccess page
                Response.Redirect("~/NoAccess.htm");
            }
        }
        private void ProtectFromASPXAUTHManipulation()
        {
            if (Request.Cookies[".AUTH"] == null)
            {
                string sessToken = Guid.NewGuid().ToString().Replace("-", "");
                HttpCookie authCookie = new HttpCookie(".AUTH", sessToken) { HttpOnly = true, Expires = DateTime.Now.AddDays(-20d) };
                Session["ASPXAUTHToken"] = sessToken;
                Response.Cookies.Add(authCookie);
            }
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
                if (CookieASPXAUTH != SessionASPXAUTH)
                {
                    Session["IsLoggedIn"] = null;

                    //audit-log
                    AuditLogModule.LogEntry("Session Fixation Attempt prevented");

                    Session["IsLoggedIn"] = null;
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                    //remove cookie
                    if (Request.Cookies[".AUTH"] != null)
                    {
                        HttpCookie authCookie = new HttpCookie(".AUTH", "nothing");
                        authCookie.HttpOnly = true;
                        authCookie.Expires = DateTime.Now.AddDays(-20d);
                        Response.Cookies.Add(authCookie);
                    }


                    //flag to indicate log out action.
                    Session["LoggedOut"] = "true";

                    //FormsAuthentication.RedirectToLoginPage()
                    //Response.Headers.Clear();
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Session["IsLoggedIn"] = null;

                //audit-log
                AuditLogModule.LogEntry("Session Fixation Attempt prevented");

                Session["IsLoggedIn"] = null;
                Session.Clear();
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                //remove cookie
                if (Request.Cookies[".AUTH"] != null)
                {
                    HttpCookie authCookie = new HttpCookie(".AUTH", "nothing");
                    authCookie.HttpOnly = true;
                    authCookie.Expires = DateTime.Now.AddDays(-20d);
                    Response.Cookies.Add(authCookie);
                }


                //flag to indicate log out action.
                Session["LoggedOut"] = "true";

                //FormsAuthentication.RedirectToLoginPage()
                Response.Flush();
                Response.Redirect("~/Default.aspx");
            }
        }
        private void CSRFCheck()
        {
                if (AntiCSRF.ValidateToken() == false)
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/FileNotFound.htm");
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                }
                AntiCSRF.GenerateCSRFToken();
        }
    }
}
