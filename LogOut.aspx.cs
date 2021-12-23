using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewApp
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsLoggedIn"] != null)
                {
                    UpdateLoginStatus(Session["USER_CODE"].ToString(), "N");
                    Session.Abandon();
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
    }
}