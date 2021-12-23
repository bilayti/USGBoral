using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class LoginStatusController : Controller
    {
        // GET: /LoginStatus/
        string username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
        string UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
        static string lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
        DataSet _DS = null;
        public List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();

        public ActionResult Index()
        {
            try
            {
                if (UserCode == null || username == "")
                {
                    return Redirect("~/Default.aspx");
                }
                else
                {
                    int time = DateTime.Now.Hour;
                    if (time > 24)
                    {
                        time = 24;
                    }
                    if (time < 12)
                    {
                        ViewBag.UserName = "Good Morning : " + Session["F_NAME"].ToString();
                    }
                    else if (time < 17)
                    {
                        ViewBag.UserName = "Good Afternoon : " + Session["F_NAME"].ToString();
                    }
                    else
                    {
                        ViewBag.UserName = "Good Evening : " + Session["F_NAME"].ToString();
                    }
                    //ViewBag.UserName = Session["F_NAME"].ToString();
                    ViewBag.lastseen = "Last Login:" + Session["LastLoginDateTime"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }

        #region GET USER DETAILS
        [HttpPost]
        public JsonResult GetLoginUserDetails()
        {
            try
            {
                _UserList.Clear();
                _DS = new DataSet();
                _DS = dataAccess.GetDataSet("SP_PORTAL_LoginDeatils");
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _UserList.Add(new UserRegistrationDetails()
                    {
                        USERID = Convert.ToInt32(_Dr["USERID"].ToString()),
                        USER_CODE = _Dr["USER_CODE"].ToString(),
                        USER_TYPEID = Convert.ToInt32(_Dr["USER_TYPEID"].ToString()),
                        F_NAME = _Dr["F_NAME"].ToString(),
                        M_NAME = _Dr["M_NAME"].ToString(),
                        L_NAME = _Dr["L_NAME"].ToString(),
                        PASSWORD1 = _Dr["PASSWORD1"].ToString(),
                        EMAIL = _Dr["EmailId"].ToString(),
                        MOBILE = _Dr["Mobile"].ToString(),
                        SAP_ID = _Dr["SAP_ID"].ToString(),
                        USER_STATUS_NAME = _Dr["USER_STATUS"].ToString(),
                        REMARKS1 = _Dr["REMARKS1"].ToString(),
                        USER_TYPE = _Dr["USER_TYPE"].ToString(),
                        IsActive = _Dr["LoginStatus"].ToString(),
                        lastlogin = _Dr["DATE_LAST_LOGIN"].ToString()
                        //lastpassword = _Dr["Date_last_password_change"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return Json(new { draw = 1, recordsTotal = _UserList.Count, recordsFiltered = 10, data = _UserList }, JsonRequestBehavior.AllowGet);
            return Json(_UserList, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
