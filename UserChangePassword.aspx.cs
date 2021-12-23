using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using NewApp.Models;

namespace NewApp
{
    public partial class UserChangePassword : System.Web.UI.Page
    {
        CommonClass objCom = new CommonClass();
        Authentication authmodule = new Authentication();
        DataTable dt = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            string lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
            string UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            if (UserCode == null || username == "")
            {
                Response.Redirect("~/Default.aspx");
            }
            if (Page.IsPostBack == false)
            {
                //generate salt
                string salt = Guid.NewGuid().ToString();
                //hfPasswordSalt.Value = salt
                Session["PasswordSalt"] = salt;
            }
            hfSalt.Value = Session["PasswordSalt"].ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string oldPasswordSaltedHash = txtOldPassword.Text;
                string newPasswordHash = txtNewPassword.Text;
                string newPasswordSaltedHash = null;
                //to be computed for validation.
                string newPasswordConfirmSaltedHash = txtNewPasswordConfirm.Text;

                //check if hashes are of correct length.
                if (oldPasswordSaltedHash.Length != 64 | newPasswordHash.Length != 64 | newPasswordConfirmSaltedHash.Length != 64)
                {
                    throw new Exception("Password change attack detected!");
                }

                //check if password & confirm-password match.
                //compute new password salted-hash
                newPasswordSaltedHash = SHA256Hasher.Hash(Session["PasswordSalt"] + newPasswordHash);
                if (newPasswordConfirmSaltedHash.Equals(newPasswordSaltedHash) == false)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password & confirm passwords do not match!" + "');", true);
                    txtOldPassword.Focus();
                    return;
                }

                //check if old password match.
                bool oldPasswordCorrect = false;
                if (Session["USER_TYPEID"].ToString()=="1")
                {
                    oldPasswordCorrect = authmodule.IsAdminOldPasswordCorrect(Session["USER_CODE"].ToString(), oldPasswordSaltedHash.ToString(), Session["PasswordSalt"].ToString());
                }
                else if (Session["USER_TYPEID"].ToString() == "2")
                {
                    oldPasswordCorrect = authmodule.IsSalesOldPasswordCorrect(Session["USER_CODE"].ToString(), oldPasswordSaltedHash.ToString(), Session["PasswordSalt"].ToString());
                }
                else if (Session["USER_TYPEID"].ToString() == "3")
                {
                    oldPasswordCorrect = authmodule.IsCustomerOldPasswordCorrect(Session["USER_CODE"].ToString(), oldPasswordSaltedHash.ToString(), Session["PasswordSalt"].ToString());
                }
                else if (Session["USER_TYPEID"].ToString() == "4")
                {
                    oldPasswordCorrect = authmodule.IsManagerOldPasswordCorrect(Session["USER_CODE"].ToString(), oldPasswordSaltedHash.ToString(), Session["PasswordSalt"].ToString());
                }
                if (oldPasswordCorrect == false)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Old password is invalid! Please check." + "');", true);
                    txtOldPassword.Focus();
                    return;
                }

                //check if user reusing password.
                bool reusingOldPassword = false;
                if (Session["USER_TYPEID"].ToString() == "1")
                {
                    reusingOldPassword = authmodule.IsAdminReusingOldPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                }
                else if (Session["USER_TYPEID"].ToString() == "2")
                {
                    reusingOldPassword = authmodule.IsSalesReusingOldPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                }
                else if (Session["USER_TYPEID"].ToString() == "3")
                {
                    reusingOldPassword = authmodule.IsCustomerReusingOldPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                }
                else if (Session["USER_TYPEID"].ToString() == "4")
                {
                    reusingOldPassword = authmodule.IsManagerReusingOldPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                }
                if (reusingOldPassword == true)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "You can't use your old passwords again!" + "');", true);
                    txtOldPassword.Focus();
                    return;
                }

                if (Session["USER_TYPEID"].ToString() == "1")
                {
                    authmodule.ChangeAdminPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password changed successfully." + "');", true);
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/ChangeSuccess.aspx?token=" + AntiCSRF.GetCSRFToken());
                    //PMJDY Login
                }
                else if (Session["USER_TYPEID"].ToString() == "2")
                {
                    authmodule.ChangeSalesPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password changed successfully." + "');", true);
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/ChangeSuccess.aspx?token=" + AntiCSRF.GetCSRFToken());
                    //PMJDY Login
                }
                else if (Session["USER_TYPEID"].ToString() == "3")
                {
                    authmodule.ChangeCustomerPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password changed successfully." + "');", true);
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/ChangeSuccess.aspx?token=" + AntiCSRF.GetCSRFToken());
                    //PMJDY Login
                }
                else if (Session["USER_TYPEID"].ToString() == "4")
                {
                    authmodule.ChangeManagerPassword(Session["USER_CODE"].ToString(), newPasswordHash);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password changed successfully." + "');", true);
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/ChangeSuccess.aspx?token=" + AntiCSRF.GetCSRFToken());
                }

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
        }
        protected void btnBackToHome_Click(object sender, System.EventArgs e)
        {

            int usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
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
}