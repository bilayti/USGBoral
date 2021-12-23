using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewApp.Models;

namespace NewApp
{
    public partial class LogOutPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsLoggedIn"] == null)
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Default.aspx");
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                }
            }
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
        protected void btnok_Click(object sender, EventArgs e)
        {
            string _SessionValue = Session["USER_CODE"] as string;
            if (!string.IsNullOrEmpty(_SessionValue))
            {
                UpdateLoginStatus(_SessionValue, "N");
            }
            if (Session["IsLoggedIn"] != null & IsAuthenticationTokenMatching())
            {
                Session["IsLoggedIn"] = null;

                //audit-log
                AuditLogModule.LogEntry("User Logs Out");

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

                Session["LoggedOut"] = "true";

            }

            Response.Redirect("~/Default.aspx");
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            if (Session["IsLoggedIn"] != null)
            {
                int usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
                if (usertypeid == 1)
                {
                    Response.Redirect("~/HomePage/Home");
                }
                else if (usertypeid == 2)
                {
                    Response.Redirect("~/Sale/Index");
                }
                else if (usertypeid == 3)
                {
                    Response.Redirect("~/UserPage/Index");
                }
                else
                {
                    Response.Redirect("~/Manager/Index");
                }
            }
        }
    }
}