using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Web.Mail;
using System.Net;
using System.Web.Configuration;
using System.Xml;
using System.Security;
using Microsoft.ApplicationBlocks.Data;
using NewApp.Models;

namespace NewApp.Models
{
    public class Authentication 
    {
        CommonClass common = new CommonClass();
        DBConnection dbConn = new DBConnection();
        public int GetExistingFaildLoginAttempts(string LoginId)
        {

            int attempts = 0;
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ISNULL(failed_login_attempts,0) AS AttemptsCount FROM pusr WHERE USER_CODE=@LoginID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                attempts = int.Parse(dt.Rows[0]["AttemptsCount"].ToString());
            }
            else
            {
                attempts = 0;
            }

            //return the datatable containing result rows.
            return attempts;
        }
        public int GetExistingUserDetailsIsActive(string LoginId)
        {

            int enabledisable = 0;
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT USER_STATUS AS userstatus FROM pusr WHERE USER_CODE=@LoginID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                enabledisable = int.Parse(dt.Rows[0]["userstatus"].ToString());
            }
            else
            {
                enabledisable = 0;
            }

            //return the datatable containing result rows.
            return enabledisable;
        }
        public string GetExistingLoggedInSalesUser(string LoginId)
        {

            string enabledisable = "";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT LoginStatus AS userstatus FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=2";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                enabledisable = dt.Rows[0]["userstatus"].ToString();
            }
            else
            {
                enabledisable = "";
            }

            //return the datatable containing result rows.
            return enabledisable;
        }
        public string GetExistingLoggedInCustomerUser(string LoginId)
        {

            string enabledisable = "";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT LoginStatus AS userstatus FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=3";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                enabledisable = dt.Rows[0]["userstatus"].ToString();
            }
            else
            {
                enabledisable = "";
            }

            //return the datatable containing result rows.
            return enabledisable;
        }
        public string GetExistingLoggedInManagerUser(string LoginId)
        {

            string enabledisable = "";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT LoginStatus AS userstatus FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=4";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                enabledisable = dt.Rows[0]["userstatus"].ToString();
            }
            else
            {
                enabledisable = "";
            }

            //return the datatable containing result rows.
            return enabledisable;
        }
        public static bool ChangePassword(string LoginID, string NewPasswordHash, string bankcode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE pusr SET PASSWORD3=(SELECT PASSWORD2 FROM pusr WHERE USERId=@LoginId and USER_CODE=@bankcode),PASSWORD2=(SELECT PASSWORD1 FROM pusr WHERE USERId=@LoginId and USER_CODE=@bankcode),Password1=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE USERId=@LoginId and USER_CODE=@bankcode ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NewPasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@bankcode",
                    DbType = DbType.String,
                    Size = 15,
                    Value = bankcode
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public string AuthenticateAdmin(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [pusr] WHERE USER_CODE=@LoginId";
                //and userlevel=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if (dt.Rows.Count > 0)
            {
                //mix salt with database hash
                string dbPasswordHash = dt.Rows[0]["Password1"].ToString();
                string saltedDbPasswordHash = shaHash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows[0]["USER_TYPEID"].ToString();
                }
            }
            //failure
            return "NOTAUTHENTICATED";
        }
        public string AuthenticateSales(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [pusr] WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                //and userlevel=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "2"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if (dt.Rows.Count > 0)
            {
                //mix salt with database hash
                string dbPasswordHash = dt.Rows[0]["Password1"].ToString();
                string saltedDbPasswordHash = shaHash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows[0]["USER_TYPEID"].ToString();
                }
            }
            //failure
            return "NOTAUTHENTICATED";
        }
        public string AuthenticateCustomer(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [pusr] WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                //and userlevel=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "3"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if (dt.Rows.Count > 0)
            {
                //mix salt with database hash
                string dbPasswordHash = dt.Rows[0]["Password1"].ToString();
                string saltedDbPasswordHash = shaHash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows[0]["USER_TYPEID"].ToString();
                }
            }
            //failure
            return "NOTAUTHENTICATED";
        }
        public string AuthenticateManager(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [pusr] WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                //and userlevel=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "4"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if (dt.Rows.Count > 0)
            {
                //mix salt with database hash
                string dbPasswordHash = dt.Rows[0]["Password1"].ToString();
                string saltedDbPasswordHash = shaHash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows[0]["USER_TYPEID"].ToString();
                }
            }
            //failure
            return "NOTAUTHENTICATED";
        }
        public static bool IsUserReusingOldPassword(string LoginId, string NewPasswordHash, string bankcode)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM pusr WHERE USERId=@LoginId and USER_CODE=@bankcode  AND (PASSWORD1=@PasswordHash OR PASSWORD2=@PasswordHash OR PASSWORD3=@PasswordHash) and  USER_STATUS in ('1')";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@bankcode",
                    DbType = DbType.String,
                    Size = 50,
                    Value = bankcode
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            //return true/false
            return (dt != null & dt.Rows.Count > 0);
        }
        public static DataTable GetUser(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [PUSR] WHERE USER_CODE=@LoginId ";
                cmd.CommandType = CommandType.Text;

                if (LoginId == null)
                {
                    LoginId = "USERIDTHATNEVEREXIST";
                }

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public static DataTable GetUsersDistrictWise()
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT USER_CODE,USERID,F_Name,SAP_ID,case user_status when '1' then 'Enabled User' else 'Disabled User' end 'UserStatus' FROM PUSR us where USER_TYPEID in('1','2','3','4') order by USER_CODE ";
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public static DataTable GetUsersData(int userid)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PUSR us where USER_TYPEID in('1','2','3','4') where UserId=@UserId order by USER_CODE ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserId",
                    DbType = DbType.String,
                    
                    Value = userid
                });
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public static bool DeleteUser(string LoginID, string bankcode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM [PUSR] WHERE USER_CODE=@LoginId and USERID=@bankcode";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@bankcode",
                    DbType = DbType.String,
                    Size = 15,
                    Value = bankcode
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public static bool UnlockUser(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [PUSR] SET failed_login_attempts=0 WHERE USERID=@LoginId  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public static bool DisableUser(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [PUSR] SET USER_STATUS=0 WHERE USERID=@LoginId  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public static bool EnableUser(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [PUSR] SET USER_STATUS=1 WHERE USERID=@LoginId  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public static bool ReleaseUser(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [PUSR] SET LoginStatus='N' WHERE USERID=@LoginId  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public string shaHash(string PlainText)
        {

            //ASSUMPTION password will be in english only (i.e ascii)
            //if password can be multilingual then use Encoding.UTF8.GetBytes() instead.

            //obtain byte array of plain text
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);

            //create managed instance of Sha256 algo
            SHA256Managed sha = new SHA256Managed();

            //obtain hash in byte array
            byte[] hashBytes = sha.ComputeHash(plainTextBytes);

            //obtain hexadecimal string of the hash.
            StringBuilder sb = new StringBuilder();

            foreach (byte hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }

            //return the hash string now
            return sb.ToString();
        }
        public DataTable GetUserDetailsAdmin(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = " SELECT * FROM pusr WHERE USER_CODE=@LoginId  and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "1"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public DataTable GetSalesUserDetails(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = " SELECT * FROM pusr WHERE USER_CODE=@LoginId  and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "2"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public DataTable GetCustomerUserDetails(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = " SELECT * FROM pusr WHERE USER_CODE=@LoginId  and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "3"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public DataTable GetManagerUserDetails(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = " SELECT * FROM pusr WHERE USER_CODE=@LoginId  and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "4"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            //return the datatable containing result rows.
            return dt;
        }
        public DateTime GetLastLoginDateTimeadmin(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID='1'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                if (dt.Rows[0].IsNull("Date_last_login"))
                {
                    lastLoginDateTime = Convert.ToDateTime(null);
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows[0]["Date_last_login"].ToString());
                }
            }
            else
            {
                lastLoginDateTime = Convert.ToDateTime(null);
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public static string Authenticate(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [PUSR] WHERE USER_CODE=@LoginId ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if (dt.Rows.Count > 0)
            {
                //mix salt with database hash
                string dbPasswordHash = dt.Rows[0]["PASSWORD1"].ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows[0]["USER_TYPEID"].ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public bool IsAdminOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password1] FROM [pusr] WHERE USER_CODE=@LoginId";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            string dbOldPasswordHash = "";
            string dbOldPasswordSaltedHash = "";
            if (dt != null & dt.Rows.Count > 0)
            {
                dbOldPasswordHash = dt.Rows[0]["Password1"].ToString();
                dbOldPasswordSaltedHash = SHA256Hasher.Hash(Salt + dbOldPasswordHash);

                if (OldPasswordSaltedHash.Equals(dbOldPasswordSaltedHash) == true)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("Database is missing password value for the user!");
            }

            //failure
            return false;
        }
        public bool IsSalesOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password1] FROM [PUSR] WHERE USER_CODE=@LoginId and USER_TYPEID='2'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            string dbOldPasswordHash = "";
            string dbOldPasswordSaltedHash = "";
            if (dt != null & dt.Rows.Count > 0)
            {
                dbOldPasswordHash = dt.Rows[0]["Password1"].ToString();
                dbOldPasswordSaltedHash = SHA256Hasher.Hash(Salt + dbOldPasswordHash);

                if (OldPasswordSaltedHash.Equals(dbOldPasswordSaltedHash) == true)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("Database is missing password value for the user!");
            }

            //failure
            return false;
        }
        public bool IsCustomerOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password1] FROM [PUSR] WHERE USER_CODE=@LoginId and USER_TYPEID='3'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            string dbOldPasswordHash = "";
            string dbOldPasswordSaltedHash = "";
            if (dt != null & dt.Rows.Count > 0)
            {
                dbOldPasswordHash = dt.Rows[0]["Password1"].ToString();
                dbOldPasswordSaltedHash = SHA256Hasher.Hash(Salt + dbOldPasswordHash);

                if (OldPasswordSaltedHash.Equals(dbOldPasswordSaltedHash) == true)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("Database is missing password value for the user!");
            }

            //failure
            return false;
        }
        public bool IsManagerOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password1] FROM [PUSR] WHERE USER_CODE=@LoginId and USER_TYPEID='4'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            string dbOldPasswordHash = "";
            string dbOldPasswordSaltedHash = "";
            if (dt != null & dt.Rows.Count > 0)
            {
                dbOldPasswordHash = dt.Rows[0]["Password1"].ToString();
                dbOldPasswordSaltedHash = SHA256Hasher.Hash(Salt + dbOldPasswordHash);

                if (OldPasswordSaltedHash.Equals(dbOldPasswordSaltedHash) == true)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("Database is missing password value for the user!");
            }

            //failure
            return false;
        }
        public bool IsAdminReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM pusr WHERE USER_CODE=@LoginId  AND (Password1=@PasswordHash OR PASSWORD2=@PasswordHash OR PASSWORD3=@PasswordHash)";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            //return true/false
            return (dt != null & dt.Rows.Count > 0);
        }
        public bool IsSalesReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM pusr WHERE USER_CODE=@LoginId  AND (Password1=@PasswordHash OR PASSWORD2=@PasswordHash OR PASSWORD3=@PasswordHash) and USER_TYPEID='2'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            //return true/false
            return (dt != null & dt.Rows.Count > 0);
        }
        public bool IsCustomerReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM pusr WHERE USER_CODE=@LoginId  AND (Password1=@PasswordHash OR PASSWORD2=@PasswordHash OR PASSWORD3=@PasswordHash) and USER_TYPEID='3'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            //return true/false
            return (dt != null & dt.Rows.Count > 0);
        }
        public bool IsManagerReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM pusr WHERE USER_CODE=@LoginId  AND (Password1=@PasswordHash OR PASSWORD2=@PasswordHash OR PASSWORD3=@PasswordHash) and USER_TYPEID='4'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
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

            //return true/false
            return (dt != null & dt.Rows.Count > 0);
        }
        public bool ChangeAdminPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE PUSR SET PASSWORD3=(SELECT PASSWORD2 FROM PUSR WHERE USER_CODE=@LoginId),PASSWORD2=(SELECT PASSWORD1 FROM PUSR WHERE USER_CODE=@LoginId),[Password1]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE USER_CODE=@LoginId";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NewPasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public bool ChangeSalesPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE PUSR SET PASSWORD3=(SELECT PASSWORD2 FROM PUSR WHERE USER_CODE=@LoginId),PASSWORD2=(SELECT PASSWORD1 FROM PUSR WHERE USER_CODE=@LoginId),[Password1]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID='2'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NewPasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public bool ChangeCustomerPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE PUSR SET PASSWORD3=(SELECT PASSWORD2 FROM PUSR WHERE USER_CODE=@LoginId),PASSWORD2=(SELECT PASSWORD1 FROM PUSR WHERE USER_CODE=@LoginId),[Password1]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID='3'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NewPasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public bool ChangeManagerPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE PUSR SET PASSWORD3=(SELECT PASSWORD2 FROM PUSR WHERE USER_CODE=@LoginId),PASSWORD2=(SELECT PASSWORD1 FROM PUSR WHERE USER_CODE=@LoginId),[Password1]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID='4'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NewPasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = NewPasswordHash
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return the datatable containing result rows.
            return (result > 0);
        }
        public bool UpdateFaildLoginAttemptsSales(string LoginID, int AttemptsCount)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET failed_login_attempts=@AttemptsCount WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@AttemptsCount",
                    DbType = DbType.Int32,
                    Value = AttemptsCount
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "2"
                });


                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateFaildLoginAttemptsCustomer(string LoginID, int AttemptsCount)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET failed_login_attempts=@AttemptsCount WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@AttemptsCount",
                    DbType = DbType.Int32,
                    Value = AttemptsCount
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "3"
                });


                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateFaildLoginAttemptsManager(string LoginID, int AttemptsCount)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET failed_login_attempts=@AttemptsCount WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel  ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@AttemptsCount",
                    DbType = DbType.Int32,
                    Value = AttemptsCount
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "4"
                });


                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public DateTime GetLastLoginDateTimeSales(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "2"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                if (dt.Rows[0].IsNull("Date_last_login"))
                {
                    lastLoginDateTime = Convert.ToDateTime(null);
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows[0]["Date_last_login"].ToString());
                }
            }
            else
            {
                lastLoginDateTime = Convert.ToDateTime(null);
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public DateTime GetLastLoginDateTimeCustomer(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "3"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                if (dt.Rows[0].IsNull("Date_last_login"))
                {
                    lastLoginDateTime = Convert.ToDateTime(null);
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows[0]["Date_last_login"].ToString());
                }
            }
            else
            {
                lastLoginDateTime = Convert.ToDateTime(null);
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public DateTime GetLastLoginDateTimeManager(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM pusr WHERE USER_CODE=@LoginID and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "4"
                });

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

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

            if ((dt != null & dt.Rows.Count > 0))
            {
                if (dt.Rows[0].IsNull("Date_last_login"))
                {
                    lastLoginDateTime = Convert.ToDateTime(null);
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows[0]["Date_last_login"].ToString());
                }
            }
            else
            {
                lastLoginDateTime = Convert.ToDateTime(null);
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public bool IncrementFaildLoginAttempts(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET failed_login_attempts=ISNULL(failed_login_attempts, 0)+1 WHERE USER_CODE=@LoginID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });




                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateLoginDateTimeadmin(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET Date_last_login=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID='1'";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateLoginDateTimeSales(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET Date_last_login=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "2"
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateLoginDateTimeCustomer(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET Date_last_login=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "3"
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
        public bool UpdateLoginDateTimeManager(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [pusr] SET Date_last_login=GETDATE() WHERE USER_CODE=@LoginId and USER_TYPEID=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginID",
                    DbType = DbType.String,
                    Size = 50,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = "4"
                });

                con.Open();
                result = cmd.ExecuteNonQuery();

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

            //return true/false
            return (result > 0);
        }
    }
}
