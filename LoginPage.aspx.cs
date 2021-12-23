using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.CSharp;
using NewApp.Models;

namespace NewApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        Authentication auth = new Authentication();
        DataTable dt = new DataTable();
        string userlevel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Captcha1.PasswordFieldID = txtPassword.ClientID;
            if (Session["IsLoggedIn"] != null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                string salt = Guid.NewGuid().ToString();
                Session["PasswordSalt"] = salt;
            }
            hfSalt.Value = Session["PasswordSalt"].ToString();
            this.Page.Form.DefaultButton = btnLogin.UniqueID;

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                if (Captcha1.IsValid == false)
                {
                    AuditLogModule.LogEntry("Captcha code doesn't match!", txtLoginId.Text);
                    //regenerate captcha code & present new challenge.;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Invalid verification code! Please check." + "');", true);
                    Captcha1.Generate();
                    Captcha1.Clear();
                    return;
                }

                Captcha1.Generate();

                string saltedPasswordHash = txtPassword.Text;
                int attemptscount = Convert.ToInt32(auth.GetExistingFaildLoginAttempts(txtLoginId.Text));
                int enabledisable = Convert.ToInt32(auth.GetExistingUserDetailsIsActive(txtLoginId.Text));
                
                if (enabledisable == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "your account is disable! please contact site administrator for further help." + "');", true);
                    AuditLogModule.LogEntry("Disabled user tried to login.", txtLoginId.Text);
                    Captcha1.Clear();
                    return;
                }
                if (attemptscount > 5)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "your account is locked! please contact site administrator for further help." + "');", true);
                    AuditLogModule.LogEntry("Locked user tried to login.", txtLoginId.Text);
                    Captcha1.Clear();
                    return;
                }

                userlevel = Authentication.Authenticate(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
                if (userlevel.Equals("NOTAUTHENTICATED") == false)
                {

                    AntiCSRF.GenerateCSRFToken();
                    Session["UserLevel"] = userlevel.ToString();
                    Session["USER_CODE"] = txtLoginId.Text;

                    if (userlevel.Equals("1"))
                    {
                        //Admin User                      
                        DataTable AdminDT = auth.GetUserDetailsAdmin(txtLoginId.Text);
                        Session["USER_CODE"] = AdminDT.Rows[0]["USER_CODE"];
                        Session["F_NAME"] = AdminDT.Rows[0]["F_NAME"];
                        Session["_SAP_ID"] = AdminDT.Rows[0]["SAP_ID"];
                        Session["USER_TYPEID"] = AdminDT.Rows[0]["USER_TYPEID"];
                        DateTime lastLoginDateTime = auth.GetLastLoginDateTimeadmin(Session["USER_CODE"].ToString());
                        Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");

                        auth.UpdateLoginDateTimeadmin(txtLoginId.Text);
                        UpdateLoginStatus(txtLoginId.Text, "Y");
                    }
                    //Sales User
                    else if (userlevel.Equals("2"))
                    {
                        string logineduser = auth.GetExistingLoggedInSalesUser(txtLoginId.Text).ToString();

                        if (logineduser.ToString() == "Y")
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "This user is already logged in." + "');", true);
                            AuditLogModule.LogEntry("Already logged in user tried to login.", txtLoginId.Text);
                            Captcha1.Clear();
                            return;
                        }
                        auth.UpdateFaildLoginAttemptsSales(txtLoginId.Text, 0);
                        DataTable SalesDT = auth.GetSalesUserDetails(txtLoginId.Text);
                        Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
                        Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
                        Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
                        Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
                        DateTime lastLoginDateTime = auth.GetLastLoginDateTimeSales(Session["USER_CODE"].ToString());
                        Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
                        //update last login date/time
                        auth.UpdateLoginDateTimeSales(txtLoginId.Text);
                        UpdateLoginStatus(txtLoginId.Text, "Y");
                    }
                    // CUSTOMER login
                    else if (userlevel.Equals("3"))
                    {
                        string logineduser = auth.GetExistingLoggedInCustomerUser(txtLoginId.Text).ToString();

                        if (logineduser.ToString() == "Y")
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "This user is already logged in." + "');", true);
                            AuditLogModule.LogEntry("Already logged in user tried to login.", txtLoginId.Text);
                            Captcha1.Clear();
                            return;
                        }
                        auth.UpdateFaildLoginAttemptsCustomer(txtLoginId.Text, 0);
                        DataTable SalesDT = auth.GetCustomerUserDetails(txtLoginId.Text);
                        Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
                        Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
                        Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
                        Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
                        DateTime lastLoginDateTime = auth.GetLastLoginDateTimeCustomer(Session["USER_CODE"].ToString());
                        Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
                        //update last login date/time
                        auth.UpdateLoginDateTimeCustomer(txtLoginId.Text);
                        UpdateLoginStatus(txtLoginId.Text, "Y");
                    }
                    // Manager login 
                    else if (userlevel.Equals("4"))
                    {
                        string logineduser = auth.GetExistingLoggedInManagerUser(txtLoginId.Text).ToString();

                        if (logineduser.ToString() == "Y")
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "This user is already logged in." + "');", true);
                            AuditLogModule.LogEntry("Already logged in user tried to login.", txtLoginId.Text);
                            Captcha1.Clear();
                            return;
                        }
                        auth.UpdateFaildLoginAttemptsManager(txtLoginId.Text, 0);
                        DataTable SalesDT = auth.GetManagerUserDetails(txtLoginId.Text);
                        Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
                        Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
                        Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
                        Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
                        DateTime lastLoginDateTime = auth.GetLastLoginDateTimeManager(Session["USER_CODE"].ToString());
                        Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
                        //update last login date/time
                        auth.UpdateLoginDateTimeManager(txtLoginId.Text);
                        UpdateLoginStatus(txtLoginId.Text, "Y");
                    }

                    AuditLogModule.LogEntry("User Logged In Successfully", txtLoginId.Text);
                    Session["IsLoggedIn"] = "true";
                    //set cookie and redirect - login user
                    string sessToken = Guid.NewGuid().ToString().Replace("-", "");
                    HttpCookie authCookie = new HttpCookie(".AUTH", sessToken) { HttpOnly = true, Expires = DateTime.Now.AddDays(-20d) };
                    Session["ASPXAUTHToken"] = sessToken;
                    Response.Cookies.Add(authCookie);
                    //Perform a 302 Redirect.
                    var page = HttpContext.Current.Handler as Page;
                    //Response.Redirect(page.GetRouteUrl("HomePage", new { Controller = "Home", Action = "Home" }), false);
                    Response.Redirect("FrmAgree.aspx");
                    //Response.Redirect("~/HomePage/Home");

                }

                else
                {
                    if (txtLoginId.Text.ToLower() == "admin" || txtLoginId.Text.ToLower() == "superadmin")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Invalid LoginId/Password! Please check your credentials." + "');", true);
                        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
                        Captcha1.Clear();
                    }
                    else
                    {
                        auth.IncrementFaildLoginAttempts(txtLoginId.Text);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Invalid LoginId/Password! Please check your credentials." + "');", true);
                        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
                        Captcha1.Clear();
                    }
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            Captcha1.Generate();
            Captcha1.Clear();
        }
        public void ClearInputFields()
        {
            txtLoginId.Text = "";
            txtPassword.Text = "";
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