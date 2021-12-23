using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class ForgetPassController : Controller
    {
        //
        // GET: /ForgetPass/

        public ActionResult Index()
        {
            return View();
        }

        public void ChangePassword(string UserId, string NewPassword)
        {
            try
            {
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserId", UserId.ToString()));
                lstparameters.Add(new Parameters("@NewPassword", NewPassword.ToString()));
                int result = dataAccess.ExecuteNonQuery("SP_PORTAL_ChangePassword", lstparameters);
            }
            catch
            {
                throw;
            }
        }


    }
}
