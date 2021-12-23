using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewApp
{
    public partial class ChangeSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["o"].ToString().Equals("change"))
                    {
                        litMsg.Text = "Password change successfully";
                    }
                    else if (Session["o"].ToString().Equals("UserPasswordReset"))
                    {
                        litMsg.Text = "Password reset successfully";
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnbacktoChange_Click(object sender, EventArgs e)
        {
            int usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
            if (usertypeid == 1)
            {
                Response.Redirect("~/HomePage/Home");
            }
            else if (usertypeid == 2)
            {
                Response.Redirect("~/Sales/Index");
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