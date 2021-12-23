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
    public class AddUserRegistrationController : BaseController
    {
        string username = string.Empty;
        string lastseen = string.Empty;
        string UserCode = string.Empty;
        DataSet _DS = null;

        public List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();
        public AddUserRegistrationController()
        {
            username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
        }
        public ActionResult AddUserReg()
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

        #region Local variable declaration
        SqlConnection _Con;
        SqlCommand _Cmd;
        //SqlDataAdapter _Ad;
        //DataSet _Ds;
        private List<UserRegistrationDetails> _UserReg = new List<UserRegistrationDetails>();
        #endregion

        #region Connection Method
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _Con = new SqlConnection(constr);
        }
        #endregion

        int result = 0;
        [HttpPost]
        public JsonResult UserRegDetails(UserRegistrationDetails _UserReg)
        {
            try
            {
                connection();
                _Cmd = new SqlCommand("SP_USERREGISTRATION_DETAILS", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@USERID", HttpUtility.HtmlEncode(_UserReg.USERID));
                _Cmd.Parameters.AddWithValue("@USER_TYPEID", HttpUtility.HtmlEncode(_UserReg.USER_TYPEID));
                _Cmd.Parameters.AddWithValue("@USER_CODE", HttpUtility.HtmlEncode(_UserReg.USER_CODE));
                _Cmd.Parameters.AddWithValue("@F_NAME", Encoder.HtmlEncode(_UserReg.F_NAME));
                _Cmd.Parameters.AddWithValue("@M_NAME", Encoder.HtmlEncode(_UserReg.M_NAME));
                _Cmd.Parameters.AddWithValue("@L_NAME", Encoder.HtmlEncode(_UserReg.L_NAME));
                // _Cmd.Parameters.AddWithValue("@PASSWORD1", _UserReg.PASSWORD1);
                _Cmd.Parameters.AddWithValue("@EMAIL", Encoder.HtmlEncode(_UserReg.EMAIL));
                _Cmd.Parameters.AddWithValue("@MOBILE", Encoder.HtmlEncode(_UserReg.MOBILE));
                _Cmd.Parameters.AddWithValue("@REMARKS1", Encoder.HtmlEncode(_UserReg.REMARKS1));
                _Cmd.Parameters.AddWithValue("@SAP_ID", _UserReg.SAP_ID);
                _Cmd.Parameters.AddWithValue("@USER_STATUS", Encoder.HtmlEncode(_UserReg.USER_STATUS.ToString()));
                _Cmd.Parameters.AddWithValue("@IsActive", Encoder.HtmlEncode(_UserReg.IsActive));
                _Con.Open();
                result = _Cmd.ExecuteNonQuery();
                _Con.Close();
                if (result > 0)
                {
                    return Json("Successfully Saved!");
                    
                }
                else
                {
                    return Json("Record Updated Successfully");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult UserAuthorization(UserRegistrationDetails _UserReg)
        {
            try
            {
                connection();
                _Cmd = new SqlCommand("SP_PORTAL_USERAuthorization", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@USERID", HttpUtility.HtmlEncode(_UserReg.USERID));
                _Cmd.Parameters.AddWithValue("@Aging", Encoder.HtmlEncode(_UserReg.Aging));
                _Cmd.Parameters.AddWithValue("@PendingOrder", Encoder.HtmlEncode(_UserReg.PendingOrder));
                _Cmd.Parameters.AddWithValue("@ItemPurchase", Encoder.HtmlEncode(_UserReg.ItemPurchase));
                _Cmd.Parameters.AddWithValue("@Invoice", Encoder.HtmlEncode(_UserReg.Invoice));
                _Cmd.Parameters.AddWithValue("@AccountStatement", Encoder.HtmlEncode(_UserReg.AccountStatement));
                _Cmd.Parameters.AddWithValue("@CollectionSummary", Encoder.HtmlEncode(_UserReg.CollectionSummary));
                _Cmd.Parameters.AddWithValue("@CreditNote", Encoder.HtmlEncode(_UserReg.CreditNote));
                _Con.Open();
                result = _Cmd.ExecuteNonQuery();
                _Con.Close();
                if (result > 0)
                {
                    return Json("Successfully Saved!");
                }
                else
                {
                    return Json("Already Exist");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        #region GET USER DETAILS
        [HttpPost]
        public JsonResult FillUserDetail(int sUserID)
        {
            try
            {
                _UserList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserID", Encoder.HtmlEncode(sUserID.ToString())));
                _DS = dataAccess.GetDataSet("SP_PORTAL_FillUserDetails", lstparameters);
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
                        USER_STATUS = Convert.ToInt32(_Dr["USER_STATUS"].ToString()),
                        REMARKS1 = _Dr["REMARKS1"].ToString(),
                        USER_TYPE = _Dr["USER_TYPE"].ToString(),
                        IsActive = _Dr["IsActive"].ToString(),
                        //Report part
                        Aging = _Dr["Aging"].ToString(),
                        PendingOrder = _Dr["PendingOrder"].ToString(),
                        ItemPurchase = _Dr["ItemPurchase"].ToString(),
                        Invoice = _Dr["Invoice"].ToString(),
                        AccountStatement = _Dr["AccountStatement"].ToString(),
                        CollectionSummary = _Dr["CollectionSummary"].ToString(),
                        CreditNote = _Dr["CreditNote"].ToString()
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
