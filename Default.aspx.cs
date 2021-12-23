using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using NewApp.Models;

namespace NewApp.Views.Login
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            //maintain no cache
            Response.ClearHeaders();
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Cache-Control", "no-store");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProtectFromASPXAUTHManipulation();
        }
        private void ProtectFromASPXAUTHManipulation()
        {
            if (Session["IsLoggedIn"] != null)
            {
                if (Request.Cookies[".AUTH"] != null)
                {
                    string SessionASPXAUTH = "A";
                    string CookieASPXAUTH = "B";
                    if (Session["ASPXAUTHToken"] != null)
                    {
                        SessionASPXAUTH = Session["ASPXAUTHToken"].ToString();
                    }
                    CookieASPXAUTH = Request.Cookies[".AUTH"].Value;
                    //auth-token manipulation check.
                    if (CookieASPXAUTH != SessionASPXAUTH)
                    {
                        Session["IsLoggedIn"] = null;
                        //audit-log
                        AuditLogModule.LogEntry("Session Fixation Attempt prevented");
                        Session["IsLoggedIn"] = null;
                        Session.Clear();
                        Session.Abandon();
                        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                        //remove cookie
                        if (Request.Cookies[".AUTH"] != null)
                        {
                            HttpCookie authCookie = new HttpCookie(".AUTH", "nothing");
                            authCookie.HttpOnly = true;
                            authCookie.Expires = DateTime.Now.AddDays(-20d);
                            Response.Cookies.Add(authCookie);
                        }


                        //flag to indicate log out action.
                        Session["LoggedOut"] = "true";

                        //FormsAuthentication.RedirectToLoginPage()
                        Response.Redirect("~/Default.aspx");

                    }
                }
                else
                {
                    Session["IsLoggedIn"] = null;

                    //audit-log
                    AuditLogModule.LogEntry("Session Fixation Attempt prevented");

                    Session["IsLoggedIn"] = null;
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "") { HttpOnly = true });
                    //remove cookie
                    if (Request.Cookies[".AUTH"] != null)
                    {
                        HttpCookie authCookie = new HttpCookie(".AUTH", "nothing");
                        authCookie.HttpOnly = true;
                        authCookie.Expires = DateTime.Now.AddDays(-20d);
                        Response.Cookies.Add(authCookie);
                    }


                    //flag to indicate log out action.
                    Session["LoggedOut"] = "true";

                    //FormsAuthentication.RedirectToLoginPage()
                    Response.Redirect("~/Default.aspx");
                }

            }


        }
        protected void btnLoginPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        string saltedPasswordHash = txtPassword.Text;
        //        int attemptscount = Convert.ToInt32(auth.GetExistingFaildLoginAttempts(txtLoginId.Text));
        //        if (attemptscount > 5)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "your account is locked! please contact webmaster for further help." + "');", true);
        //            //lblerr.Text = "your account is locked! please contact webmaster for further help.";
        //            AuditLogModule.LogEntry("locked user tried to login.", txtLoginId.Text);
        //            return;
        //        }
        //        userlevel = Authentication.Authenticate(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
        //        //if (userlevel == "1")
        //        //{
        //        //    //Admin Login
        //        //    userlevel = auth.AuthenticateAdmin(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
        //        //    if (!userlevel.Equals("1"))
        //        //    {
        //        //        lblerr.Text = "Invalid LoginId/Password! Please check your credentials.";
        //        //        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //        //        return;
        //        //    }

        //        //}
        //        //else if (userlevel == "2")
        //        //{
        //        //    // Sales login'
        //        //    userlevel = auth.AuthenticateSales(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
        //        //    if (!userlevel.Equals("2"))
        //        //    {
        //        //        //increase failed attempt count.
        //        //        auth.IncrementFaildLoginAttempts(txtLoginId.Text);
        //        //        lblerr.Text = "Invalid LoginId/Password! Please check your credentials.";
        //        //        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //        //        return;
        //        //    }
        //        //}
        //        //else if (userlevel == "3")
        //        //{
        //        //    // Customer Login
        //        //    userlevel = auth.AuthenticateCustomer(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
        //        //    if (!userlevel.Equals("3"))
        //        //    {
        //        //        //increase failed attempt count.
        //        //        auth.IncrementFaildLoginAttempts(txtLoginId.Text);
        //        //        lblerr.Text = "Invalid LoginId/Password! Please check your credentials.";
        //        //        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //        //        return;
        //        //    }
        //        //}
        //        //else if (userlevel == "4")
        //        //{
        //        //    // Manager Login
        //        //    userlevel = auth.AuthenticateManager(txtLoginId.Text, saltedPasswordHash, Session["PasswordSalt"].ToString());
        //        //    if (!userlevel.Equals("4"))
        //        //    {
        //        //        //increase failed attempt count.
        //        //        auth.IncrementFaildLoginAttempts(txtLoginId.Text);
        //        //        lblerr.Text = "Invalid LoginId/Password! Please check your credentials.";
        //        //        AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //        //        return;
        //        //    }

        //        //}



        //        if (userlevel.Equals("NOTAUTHENTICATED") == false)
        //        {

        //            AntiCSRF.GenerateCSRFToken();
        //            Session["UserLevel"] = userlevel.ToString();
        //            Session["USER_CODE"] = txtLoginId.Text;

        //            if (userlevel.Equals("1"))
        //            {
        //                //Admin User
        //                DataTable AdminDT = auth.GetUserDetailsAdmin(txtLoginId.Text);
        //                Session["USER_CODE"] = AdminDT.Rows[0]["USER_CODE"];
        //                Session["F_NAME"] = AdminDT.Rows[0]["F_NAME"];
        //                Session["_SAP_ID"] = AdminDT.Rows[0]["SAP_ID"];
        //                Session["USER_TYPEID"] = AdminDT.Rows[0]["USER_TYPEID"];
        //                DateTime lastLoginDateTime = auth.GetLastLoginDateTimeadmin(Session["USER_CODE"].ToString());
        //                Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");

        //                auth.UpdateLoginDateTimeadmin(txtLoginId.Text);
        //                UpdateLoginStatus(txtLoginId.Text, "Y");
        //            }
        //            //Sales User
        //            else if (userlevel.Equals("2"))
        //            {
        //                auth.UpdateFaildLoginAttemptsSales(txtLoginId.Text, 0);
        //                DataTable SalesDT = auth.GetSalesUserDetails(txtLoginId.Text);
        //                Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
        //                Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
        //                Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
        //                Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
        //                DateTime lastLoginDateTime = auth.GetLastLoginDateTimeSales(Session["USER_CODE"].ToString());
        //                Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
        //                //update last login date/time
        //                auth.UpdateLoginDateTimeSales(txtLoginId.Text);
        //                UpdateLoginStatus(txtLoginId.Text, "Y");
        //            }
        //            // CUSTOMER login
        //            else if (userlevel.Equals("3"))
        //            {
        //                auth.UpdateFaildLoginAttemptsCustomer(txtLoginId.Text, 0);
        //                DataTable SalesDT = auth.GetCustomerUserDetails(txtLoginId.Text);
        //                Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
        //                Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
        //                Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
        //                Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
        //                DateTime lastLoginDateTime = auth.GetLastLoginDateTimeCustomer(Session["USER_CODE"].ToString());
        //                Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
        //                //update last login date/time
        //                auth.UpdateLoginDateTimeCustomer(txtLoginId.Text);
        //                UpdateLoginStatus(txtLoginId.Text, "Y");
        //            }
        //            // Manager login 
        //            else if (userlevel.Equals("4"))
        //            {
        //                auth.UpdateFaildLoginAttemptsManager(txtLoginId.Text, 0);
        //                DataTable SalesDT = auth.GetManagerUserDetails(txtLoginId.Text);
        //                Session["USER_CODE"] = SalesDT.Rows[0]["USER_CODE"];
        //                Session["F_NAME"] = SalesDT.Rows[0]["F_NAME"];
        //                Session["_SAP_ID"] = SalesDT.Rows[0]["SAP_ID"];
        //                Session["USER_TYPEID"] = SalesDT.Rows[0]["USER_TYPEID"];
        //                DateTime lastLoginDateTime = auth.GetLastLoginDateTimeManager(Session["USER_CODE"].ToString());
        //                Session["LastLoginDateTime"] = (lastLoginDateTime != Convert.ToDateTime(null) ? Convert.ToString(lastLoginDateTime) : "N.A");
        //                //update last login date/time
        //                auth.UpdateLoginDateTimeManager(txtLoginId.Text);
        //                UpdateLoginStatus(txtLoginId.Text, "Y");
        //            }

        //            AuditLogModule.LogEntry("User Logged In Successfully", txtLoginId.Text);
        //            Session["IsLoggedIn"] = "true";
        //            //set cookie and redirect - login user
        //            string sessToken = Guid.NewGuid().ToString().Replace("-", "");
        //            HttpCookie authCookie = new HttpCookie(".AUTH", sessToken) { HttpOnly = true };
        //            Session["ASPXAUTHToken"] = sessToken;
        //            Response.Cookies.Add(authCookie);
        //            //Perform a 302 Redirect.
        //            var page = HttpContext.Current.Handler as Page;
        //            Response.Redirect(page.GetRouteUrl("Default", new { Controller = "Agree", Action = "Index" }), false);
        //            //Response.Redirect("/Agree/Index");

        //        }

        //        else
        //        {
        //            if (txtLoginId.Text == "admin" || txtLoginId.Text =="ADMIN")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Invalid LoginId/Password! Please check your credentials." + "');", true);
        //                AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //            }
        //            else
        //            {
        //                auth.IncrementFaildLoginAttempts(txtLoginId.Text);
        //                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Invalid LoginId/Password! Please check your credentials." + "');", true);
        //                AuditLogModule.LogEntry("Invalid LoginId/Password attempt!", txtLoginId.Text);
        //            }
        //        }
        //    }
        //}
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    ClearInputFields();
        //}
        //public void ClearInputFields()
        //{
        //    txtLoginId.Text = "";
        //    txtPassword.Text = "";
        //}
        //public void UpdateLoginStatus(string _iUserId, string _sStatus)
        //{
        //    try
        //    {
        //        List<Parameters> lstparameters = new List<Parameters>();
        //        lstparameters.Add(new Parameters("@UserId", _iUserId.ToString()));
        //        lstparameters.Add(new Parameters("@Status", _sStatus.ToString()));
        //        int result = dataAccess.ExecuteNonQuery("SP_PORTAL_UpdateLoginStatus", lstparameters);
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write(ex.Message);
        //    }
        //}
    }
}