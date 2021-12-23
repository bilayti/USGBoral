using System.Web.UI;
using System.Web.UI.WebControls;
using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewApp
{
    public partial class FrmAgree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["IsLoggedIn"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
        protected void btnAgreeTermCondition_Click(object sender, EventArgs e)
        {
            if (chkAgreeTermCondition.Checked == true)
            {
                int usertypeid = 0;
                usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
                if (usertypeid == 1)
                {
                    Response.Redirect("~/HomePage/Home");
                }
                else if (usertypeid == 2)
                {
                    Response.Redirect("~/Sale/Index");
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}