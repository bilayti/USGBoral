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
    public partial class ChangePassword : System.Web.UI.Page
    {
        Authentication auth = new Authentication();
        CommonClass objCom = new CommonClass();
        DataTable dt = new DataTable();
        string userlevel = "";
        protected void btnReset_Click(object sender, System.EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
        }
        protected void btnSubmit_Click(object sender, System.EventArgs e)
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
                    litInfo.Text = "Password & confirm passwords do not match!";
                    txtOldPassword.Focus();
                    return;
                }

                //check if old password match.
                bool oldPasswordCorrect = false;
                if (Session["UserLevel"].Equals("1"))
                {
                    oldPasswordCorrect = Authentication.IsAdminOldPasswordCorrect(Session["LoginId"], oldPasswordSaltedHash, Session["PasswordSalt"]);
                }
                    // Sales User
                else if (Session["UserLevel"].Equals("2"))
                {
                    oldPasswordCorrect = Authentication.IsPMJDYOldPasswordCorrect(Session["LoginId"], oldPasswordSaltedHash, Session["PasswordSalt"]);
                }
                    // Customer User
                else if (Session["UserLevel"].Equals("3"))
                {
                    oldPasswordCorrect = Authentication.IsPMJDYOldPasswordCorrect(Session["LoginId"], oldPasswordSaltedHash, Session["PasswordSalt"]);
                }
                     // Manager User
                else
                {
                    oldPasswordCorrect = Authentication.IsOldPasswordCorrect(Session["LoginId"], oldPasswordSaltedHash, Session["PasswordSalt"], Session["bankcode"]);
                }
                if (oldPasswordCorrect == false)
                {
                    litInfo.Text = "Old password is invalid! Please check.";
                    txtOldPassword.Focus();
                    return;
                }

                //check if user reusing password.
                bool reusingOldPassword = false;
                if (Session["UserLevel"].Equals("A"))
                {
                    reusingOldPassword = AuthenticationModule.IsAdminReusingOldPassword(Session["LoginId"], newPasswordHash);
                    //PMJDY Login
                }
                else if (Session["UserLevel"].Equals("P"))
                {
                    reusingOldPassword = AuthenticationModule.IsPMJDYReusingOldPassword(Session["LoginId"], newPasswordHash);
                    //Jansuraksha Login
                }
                else if (Session["UserLevel"].Equals("J"))
                {
                    reusingOldPassword = AuthenticationModule.IsJansurakshaReusingOldPassword(Session["LoginId"], newPasswordHash);
                    //Insurance Companies Login
                }
                else if (Session["UserLevel"].Equals("S"))
                {
                    reusingOldPassword = AuthenticationModule.IsSLBCReusingOldPassword(Session["LoginId"], newPasswordHash);
                    //Bank Login
                }
                else
                {
                    reusingOldPassword = AuthenticationModule.IsUserReusingOldPassword(Session["LoginId"], newPasswordHash, Session["bankcode"]);
                }
                if (reusingOldPassword == true)
                {
                    litInfo.Text = "You can't use your old passwords again!";
                    txtOldPassword.Focus();
                    return;
                }


                //normal flow
                //change the password
                if (Session["UserLevel"].Equals("A"))
                {
                    AuthenticationModule.ChangeAdminPassword(Session["LoginId"], newPasswordHash);
                    litInfo.Text = "Password changed successfully.";
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/Success.aspx?token=" + Microsoft.SqlServer.Server.UrlEncode(AntiCSRF.GetCSRFToken()));
                    //PMJDY Login
                }
                else if (Session["UserLevel"].Equals("P"))
                {
                    AuthenticationModule.ChangePMJDYPassword(Session["LoginId"], newPasswordHash);
                    litInfo.Text = "Password changed successfully.";
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/Success.aspx?token=" + Server.UrlEncode(AntiCSRF.GetCSRFToken()));
                    //Jansuraksha Login
                }
                else if (Session["UserLevel"].Equals("J"))
                {
                    AuthenticationModule.ChangeJansurakshaPassword(Session["LoginId"], newPasswordHash);
                    litInfo.Text = "Password changed successfully.";
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/Success.aspx?token=" + Server.UrlEncode(AntiCSRF.GetCSRFToken()));
                    //Insurance Companies
                }
                else if (Session["UserLevel"].Equals("S"))
                {
                    AuthenticationModule.ChangeSLBCPassword(Session["LoginId"], newPasswordHash);
                    litInfo.Text = "Password changed successfully.";
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/Success.aspx?token=" + Server.UrlEncode(AntiCSRF.GetCSRFToken()));
                    //Bank Login
                }
                else
                {
                    AuthenticationModule.ChangePassword(Session["LoginId"], newPasswordHash, Session["bankcode"]);
                    litInfo.Text = "Password changed successfully.";
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    //audit-log
                    AuditLogModule.LogEntry("Changed Password");
                    //redirect
                    Session["o"] = "change";
                    Response.Redirect("~/Success.aspx?token=" + Server.UrlEncode(AntiCSRF.GetCSRFToken()));
                }

            }
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            //set random salt.
            if (Page.IsPostBack == false)
            {
                //generate salt
                string salt = Guid.NewGuid.ToString();
                //hfPasswordSalt.Value = salt
                Session["PasswordSalt"] = salt;
            }
            hfSalt.Value = Session["PasswordSalt"].ToString();
        }
    }
}