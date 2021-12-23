using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        string iUserId = string.Empty;
        public int portalid = 0;
        List<Login> _ModLogin = new List<Login>();

        public ActionResult Index()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }


        [HttpPost]
        public JsonResult GetLogin(string UserId, string Password)
        {
            System.Web.HttpContext.Current.Session["sessionString"] = UserId; 
            DataSet _Ds = null;
            try
            {
                _Ds = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserId", UserId.ToString()));
                lstparameters.Add(new Parameters("@Password", Password.ToString()));
                _Ds = dataAccess.GetDataSet("SP_GetLogin", lstparameters);
                if (_Ds.Tables[0].Columns.Count > 0)
                {
                    foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                    {
                        _ModLogin.Add(new Login()
                        {
                            USER_CODE = _Dr["USER_CODE"].ToString(),
                            UserName = _Dr["UserName"].ToString(),
                            USER_TYPEID = _Dr["USER_TYPEID"].ToString(),
                            SAP_ID = _Dr["SAP_ID"].ToString(),
                        });
                        System.Web.HttpContext.Current.Session["_SAP_ID"] = _Dr["SAP_ID"].ToString();
                        
                        portalid = Convert.ToInt32(System.Web.HttpContext.Current.Session["_SAP_ID"]);
                        System.Web.HttpContext.Current.Session["USER_CODE"] = _Dr["USER_CODE"].ToString();
                        System.Web.HttpContext.Current.Session["USER_TYPEID"] = _Dr["USER_TYPEID"].ToString();
                        System.Web.HttpContext.Current.Session["UserName"] = _Dr["UserName"].ToString();
                        iUserId = _Dr["USER_CODE"].ToString();
                    }
                    UpdateLoginStatus(iUserId,"Y");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(_ModLogin, JsonRequestBehavior.AllowGet);
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
            catch(Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        public ActionResult Logout()
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
            
            return Redirect("~/Default.aspx");
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
    }
}
