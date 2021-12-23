using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.CSharp;
using NewApp.Models;

namespace AdminBase
{
    public class AdminBasepage:System.Web.UI.Page
    {
        protected override void OnLoad(System.EventArgs e)
        {
            ProtectFromASPXAUTHManipulation();
            CheckAccess();
            this.MaintainScrollPositionOnPostBack = true;
            CSRFCheck();
            return;
        }
        private void CheckAccess()
        {
            //else check if user is admin,
            int usertypeid = Convert.ToInt32(System.Web.HttpContext.Current.Session["USER_TYPEID"].ToString());
            if (usertypeid != 1)
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
                System.Web.HttpContext.Current.Session["ASPXAUTHToken"] = sessToken;
                Response.Cookies.Add(authCookie);
            }
            if (Request.Cookies[".AUTH"] != null)
            {
                string SessionASPXAUTH = "A";
                string CookieASPXAUTH = "B";
                if (System.Web.HttpContext.Current.Session["ASPXAUTHToken"] != null)
                {
                    SessionASPXAUTH = System.Web.HttpContext.Current.Session["ASPXAUTHToken"].ToString();
                }

                CookieASPXAUTH = Request.Cookies[".AUTH"].Value;

                //auth-token manipulation check.
                if (CookieASPXAUTH != SessionASPXAUTH)
                {
                    System.Web.HttpContext.Current.Session["IsLoggedIn"] = null;

                    //audit-log
                    AuditLogModule.LogEntry("System.Web.HttpContext.Current.Session Fixation Attempt prevented");

                    System.Web.HttpContext.Current.Session["IsLoggedIn"] = null;
                    System.Web.HttpContext.Current.Session.Clear();
                    System.Web.HttpContext.Current.Session.Abandon();
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
                    System.Web.HttpContext.Current.Session["LoggedOut"] = "true";

                    //FormsAuthentication.RedirectToLoginPage()
                    //Response.Headers.Clear();
                    //Response.Redirect("~/HomePage/Home");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["IsLoggedIn"] = null;

                //audit-log
                AuditLogModule.LogEntry("System.Web.HttpContext.Current.Session Fixation Attempt prevented");

                System.Web.HttpContext.Current.Session["IsLoggedIn"] = null;
                System.Web.HttpContext.Current.Session.Clear();
                System.Web.HttpContext.Current.Session.Abandon();
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
                System.Web.HttpContext.Current.Session["LoggedOut"] = "true";

                //FormsAuthentication.RedirectToLoginPage()
                Response.Flush();
                Response.Redirect("~/Default.aspx");
            }
        }
        private void CSRFCheck()
        {
            if (Page.IsPostBack == true)
            {
                if (AntiCSRF.ValidateToken() == false)
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    System.Web.HttpContext.Current.Session.Abandon();
                    Response.Redirect("~/FileNotFound.htm");
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                }
                AntiCSRF.GenerateCSRFToken();
            }
            else
            {
                AntiCSRF.GenerateCSRFToken();
            }
        }
    }
}