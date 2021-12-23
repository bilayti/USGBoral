using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Security.Application;

namespace NewApp.Controllers
{
    public class EnableDisableController : BaseController
    {
        string username = string.Empty;
        string lastseen = string.Empty;
        string UserCode = string.Empty;
        DataSet _DS = null;
        public List<CardCodeBind> _CustomerList = new List<CardCodeBind>();

        public EnableDisableController()
        {
            username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
        }
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
                    ViewBag.lastseen = "Last Login:" + Session["LastLoginDateTime"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }
        #region Local variable declaration
        SqlConnection _Con;
        SqlCommand _Cmd;
        SqlDataAdapter _Ad;
        DataSet _Ds;
        private List<CardCodeBind> _CardCode = new List<CardCodeBind>();
        public List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();
        #endregion
        #region Connection Method
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _Con = new SqlConnection(constr);
        }
        #endregion
        #region GET USER via User Type
        [HttpPost]
        public JsonResult GetUserDetails(Int32 sUserType)
        {
            try
            {
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserTypeId", Convert.ToInt32(sUserType).ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_GetUserTypeDetails", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardCode = _Dr["USER_CODE"].ToString(),
                        CardName = _Dr["USER_Name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return Json(new { draw = 1, recordsTotal = _UserList.Count, recordsFiltered = 10, data = _UserList }, JsonRequestBehavior.AllowGet);
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CustomerOnChange(string CardCode)
        {
            List<CardCode> _Mod = new List<CardCode>();
            connection();
            try
            {
                _Con.Open();
                _Cmd = new SqlCommand("POR_SP_Bind_HOME", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@CardCode", CardCode);
                _Ad = new SqlDataAdapter(_Cmd);
                DataSet _Ds;
                _Ds = new DataSet();
                _Ad.Fill(_Ds);
                foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                {
                    _Mod.Add(new CardCode()
                    {

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Con.Close();
                _Con.Dispose();
            }
            return Json(_Mod, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public JsonResult GetUserDetailstoDisable(string sUserType)
        {
            try
            {
                _UserList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserType", sUserType));
                _DS = dataAccess.GetDataSet("SP_PORTAL_GetUserDetailsForDisable", lstparameters);
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
                        //PASSWORD1 = _Dr["PASSWORD1"].ToString(),
                        EMAIL = _Dr["EmailId"].ToString(),
                        MOBILE = _Dr["Mobile"].ToString(),
                        SAP_ID = _Dr["SAP_ID"].ToString(),
                        USER_STATUS_NAME = _Dr["USER_STATUS"].ToString(),
                        REMARKS1 = _Dr["REMARKS1"].ToString(),
                        USER_TYPE = _Dr["USER_TYPE"].ToString(),
                        IsActive = _Dr["IsActive"].ToString()
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
        int result = 0;
        [HttpPost]
        public JsonResult GetUsertoDisable(string sUserId)
        {
            try
            {
                connection();
                _Cmd = new SqlCommand("SP_PORTAL_GetUsertoDisable", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@USERID", HttpUtility.HtmlEncode(sUserId));
                _Con.Open();
                result = _Cmd.ExecuteNonQuery();
                _Con.Close();
                if (result > 0)
                {
                    return Json("User has been disabled!");
                }
                else
                {
                    return Json("User has not been disabled");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetUsertoEnable(string sUserId)
        {
            try
            {
                connection();
                _Cmd = new SqlCommand("SP_PORTAL_GetUsertoEnable", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@USERID", HttpUtility.HtmlEncode(sUserId));
                _Con.Open();
                result = _Cmd.ExecuteNonQuery();
                _Con.Close();
                if (result > 0)
                {
                    return Json("User has been enabled!");
                }
                else
                {
                    return Json("User has not been enabled");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
