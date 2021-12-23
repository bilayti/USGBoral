using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class AuditLogController : BaseController
    {
        //
        // GET: /AuditLog/
        #region Local variable declaration
        SqlConnection _Con;
        string username = string.Empty;
        string lastseen = string.Empty;
        string UserCode = string.Empty;
        int usertype = 0;
        public List<CardCodeBind> _CustomerList = new List<CardCodeBind>();
        #endregion
        string _sExcelPath = ConfigurationManager.AppSettings["ExcelPath"];
        string dtCurrentDate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        #region Connection Methodm
        public void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _Con = new SqlConnection(constr);
        }
        #endregion
        public AuditLogController()
        {
            username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
            UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            usertype = (Int32)System.Web.HttpContext.Current.Session["USER_TYPEID"];
        }
        public ActionResult ViewAuditLog()
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
                    ViewBag.lastseen = "Last Login:" + Session["LastLoginDateTime"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
         
        }

    }
}
