using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class UserRegistrationController : BaseController
    {
        string username = string.Empty;
        string lastseen = string.Empty;
        string UserCode = string.Empty;
        
        DataSet _DS = null;
        public List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();
        public UserRegistrationController()
        {
            username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
        }
        public ActionResult UserReg()
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
        public JsonResult GetUserDetails(string sUserType, string sSapId, string sUserName, string sStatus, string sUserID)
        {
            try
            {
                _UserList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserType", sUserType));
                lstparameters.Add(new Parameters("@SapId", sSapId));
                lstparameters.Add(new Parameters("@UserID", sUserID));
                lstparameters.Add(new Parameters("@UserName", sUserName));
                lstparameters.Add(new Parameters("@Status", sStatus));
                _DS = dataAccess.GetDataSet("SP_PORTAL_GetUserDetails", lstparameters);
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
        [HttpPost]
        public int AddInvoice(string sUserID)
        {
            int Invoice_Id = Convert.ToInt32(UnlockUser(sUserID).ToString());
            int success;
            if (Invoice_Id > 0)
            {
                success = Invoice_Id;
            }
            else
            {
                success = -1;

            }
            return success;
        }
        public bool UnlockUser(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET failed_login_attempts=0 WHERE USERID=@LoginId  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Log exception here
                //rethrow so that ui-layer also throw it and show a custom error page
                //to visitor.
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            //return the datatable containing result rows.
            return (result > 0);
        }
        #endregion

    }
}
