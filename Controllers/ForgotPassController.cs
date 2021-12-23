using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewApp.Models;

namespace NewApp.Controllers
{
    public class ForgotPassController : Controller
    {
        //
        // GET: /ForgotPass/

        public ActionResult Index()
        {
            ChangePassword cng = new Models.ChangePassword();
            string _SessionValue = Session["USER_CODE"] as string;
            if (_SessionValue == null || _SessionValue == "")
            {
                return Redirect("~/Default.aspx");
                
            }
            else
            {
                cng.UserTypeId = Convert.ToInt32(Session["USER_TYPEID"]);
                return View();
            }
        }

        int result = 0;
        [HttpPost]
        public JsonResult ChangePassword(string UserId, string NewPassword)
        {
            try
            {
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserId", UserId.ToString()));
                lstparameters.Add(new Parameters("@NewPassword", NewPassword.ToString()));
                int result = dataAccess.ExecuteNonQuery("SP_PORTAL_ChangePassword", lstparameters);
                if (result > 0)
                {
                    return Json("Saved!");
                }
                else
                {
                    return Json("Already Exit");
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
