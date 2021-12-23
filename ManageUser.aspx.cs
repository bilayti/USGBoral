using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using System.Data;
using NewApp.Models;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using AdminBase;

namespace NewApp
{
    public partial class ManageUser : AdminBasepage
    {
        Authentication auth = new Authentication();
        DataTable dt = new DataTable();
        protected void gridUsers_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "ERROR: Record could not be updated!" + "');", true);
                //litInfo2.Text = "ERROR: Record could not be updated!";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Record updated successfully." + "');", true);
                //litInfo2.Text = "Record updated successfully.";

                //audit-log
                AuditLogModule.LogEntry("User Lock/Unlock Updated");
            }
        }
        protected void gridUsers_RowDeleted(object sender, System.Web.UI.WebControls.GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "ERROR: Record could not be deleted!" + "');", true);
                //litInfo2.Text = "ERROR: Record could not be deleted!";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Record deleted successfully." + "');", true);
                //litInfo2.Text = "Record deleted successfully.";

                //audit-log
                AuditLogModule.LogEntry("User Deleted");
            }
        }
        protected void gridUsers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                char[] sepa = { '-' };
                string tokens = e.CommandArgument.ToString();
                string loginId = tokens;
                //if resetting Administrator password then not to allow.
                if (IsItAdministrator(loginId))
                {
                    return;
                }
                else
                {
                    //show ChangePassword DetailsView
                    spanResetPasssword.Visible = true;
                }
            }
            else if (e.CommandName == "UnlockUser")
            {
                char[] sepa = { '-' };
                string tokens = e.CommandArgument.ToString();
                string loginId = tokens;


                spanResetPasssword.Visible = false;
                if (Authentication.UnlockUser(loginId))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "User unlocked!" + "');", true);
                    Bindgrid();
                }
            }
            else if (e.CommandName == "DisableUser")
            {
                char[] sepa = { '-' };
                string tokens = e.CommandArgument.ToString();
                string loginId = tokens;


                spanResetPasssword.Visible = false;
                if (Authentication.DisableUser(loginId))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "User disabled!" + "');", true);
                    Bindgrid();
                }
            }
            else if (e.CommandName == "EnableUser")
            {
                char[] sepa = { '-' };
                string tokens = e.CommandArgument.ToString();
                string loginId = tokens;


                spanResetPasssword.Visible = false;
                if (Authentication.EnableUser(loginId))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "User enabled!" + "');", true);
                    Bindgrid();
                }
            }
            else if (e.CommandName == "ReleaseUser")
            {
                char[] sepa = { '-' };
                string tokens = e.CommandArgument.ToString();
                string loginId = tokens;


                spanResetPasssword.Visible = false;
                if (Authentication.ReleaseUser(loginId))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "User released!" + "');", true);
                    Bindgrid();
                }
            }
            else
            {
                spanResetPasssword.Visible = false;
            }
        }
        private bool IsItAdministrator(string LoginId)
        {
            DataTable dt = Authentication.GetUser(LoginId);
            if (dt != null & dt.Rows.Count > 0)
            {
                if (FormattingHelper.OriginalOrEmptyString(dt.Rows[0]["USER_TYPEID"]).ToUpper().Equals("1"))
                {
                    return true;
                }
            }

            return false;
        }
        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                //generate salt
                string salt = Guid.NewGuid().ToString();

                hfSalt.Value = salt;
                //GridView1.Rows[0].Cells[0].Text
                Label loginIdd = (Label)gridUsers.SelectedRow.FindControl("lblDistrictCodeR");
                Label lblLoginIdUser = (Label)gridUsers.SelectedRow.FindControl("lblLoginIdUser");
                string loginId = lblLoginIdUser.Text.ToString();
                string bankcode = loginIdd.Text.ToString();
                string newPasswordHash = txtNewPassword.Text;

                //if resetting Administrator password then not to allow.
                if (IsItAdministrator(loginId))
                {
                    return;
                }
                //?if user is using a fresh new password, then change it.
                if (Authentication.IsUserReusingOldPassword(loginId, newPasswordHash, bankcode) == false)
                {
                    //change the password
                    Authentication.ChangePassword(loginId, newPasswordHash, bankcode);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "Password reset successfully." + "');", true);
                    //litInfo.Text = "Password reset successfully.";
                    txtNewPassword.Text = "";

                    //audit-log
                    AuditLogModule.LogEntry("Password Reset");

                    Session["o"] = "UserPasswordReset";
                    Response.Redirect("~/ChangeSuccess.aspx");
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + "You can't use old passwords again!" + "');", true);
                    litInfo.Text = "You can't use old passwords again!";
                    txtNewPassword.Focus();
                }

            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                string _SessionValue = Session["USER_CODE"].ToString();
                if (_SessionValue == null || _SessionValue == "")
                {
                    Response.Redirect("~/Default.aspx");
                }
                Bindgrid();
            }
            string hf = hfSalt.Value;
        }
        public void Bindgrid()
        {
            DataTable dtt = Authentication.GetUsersDistrictWise();
            if (dtt.Rows.Count > 0 && dtt != null)
            {

                gridUsers.DataSource = dtt;
                gridUsers.DataBind();

            }
        }
        protected void btnReset_Click(object sender, System.EventArgs e)
        {
            spanResetPasssword.Visible = false;
            gridUsers.SelectedIndex = -1;
        }
        protected void gridUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string loginId = FormattingHelper.ValidateStringsOnly(((Label)gridUsers.Rows[e.RowIndex].FindControl("lblLoginIdUser")).Text);
            string districtCode = FormattingHelper.ValidateIntegersOnly(((HiddenField)gridUsers.Rows[e.RowIndex].FindControl("hfDistrictCodeR")).Value);
            string statecode = FormattingHelper.ValidateIntegersOnly(((HiddenField)gridUsers.Rows[e.RowIndex].FindControl("hdnstatecode")).Value);
            //if resetting Administrator password then not to allow.
            if (IsItAdministrator(Encoder.HtmlEncode(loginId)))
            {
                e.Cancel = true;
                return;
            }
        }
        protected void gridUsers_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string loginId = FormattingHelper.ValidateStringsOnly(((Label)gridUsers.Rows[e.NewEditIndex].FindControl("lblLoginIdUser")).Text);
            string districtCode = FormattingHelper.ValidateIntegersOnly(((HiddenField)gridUsers.Rows[e.NewEditIndex].FindControl("hfDistrictCodeE")).Value);

            //if resetting Administrator password then not to allow.
            if (IsItAdministrator(Encoder.HtmlEncode(loginId)))
            {
                e.Cancel = true;
                return;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //SearchData.Visible = true;
        }
        protected void btnGO_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT USER_CODE,USERID,F_Name,SAP_ID,case user_status when '1' then 'Enabled User' else 'Disabled User' end 'UserStatus' FROM PUSR us where USER_TYPEID in('1','2','3','4') and UserId=@UserId order by USER_CODE ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserId",
                    DbType = DbType.String,

                    Value = hfCustomerId.Value
                });
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    //gridUsers.DataSourceID = null;
                    gridUsers.DataSource = dt;
                    gridUsers.DataBind();

                }
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

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            int usertypeid = 0;
            usertypeid = Convert.ToInt32(Session["USER_TYPEID"].ToString());
            if (usertypeid == 1)
            {
                Response.Redirect("~/HomePage/Home");
            }
            //Response.Redirect("~/HomePage/Home");
        }
        protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsers.PageIndex = e.NewPageIndex;
            Bindgrid();
        }
        protected void gridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string data = e.Row.Cells[5].Text;
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (data == "Disabled User")
                    {
                        e.Row.Cells[6].Text = "User Disabled";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        protected void lnkDisabled_Click(object sender, EventArgs e)
        {
            LinkButton lnkDisabled = sender as LinkButton;
            lnkDisabled.Text = "Disabled User";
            lnkDisabled.ForeColor = System.Drawing.Color.Red;
            if (lnkDisabled.Text == "Disabled User")
            {
                lnkDisabled.ToolTip = "User Disabled.";
                

            }
        }
        
    }
}