using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewApp.Models
{
    public class AuthenticationModule
    {
        //checks if old password supplied is correct for loginid
        public static bool IsOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt, string bankcode)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password] FROM [Users2] WHERE LoginId=@LoginId and bankcode=@bankcode and  userlevel='F' ";
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
                    ParameterName = "@bankcode",
                    DbType = DbType.String,
                    Size = 15,
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

            string dbOldPasswordHash = "";
            string dbOldPasswordSaltedHash = "";
            if (dt != null & dt.Rows.Count > 0)
            {
                dbOldPasswordHash = dt.Rows(0)("Password").ToString();
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
        public static bool IsAdminOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password] FROM [Users2] WHERE LoginId=@LoginId";
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
                dbOldPasswordHash = dt.Rows[0]["Password"].ToString();
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
        public static bool IsSLBCOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password] FROM [Users2] WHERE LoginId=@LoginId and userlevel='S'";
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
                dbOldPasswordHash = dt.Rows(0)("Password").ToString();
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
        public static bool IsPMJDYOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password] FROM [Users2] WHERE LoginId=@LoginId and userlevel='P'";
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
                dbOldPasswordHash = dt.Rows(0)("Password").ToString();
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
        public static bool IsJansurakshaOldPasswordCorrect(string LoginId, string OldPasswordSaltedHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT [Password] FROM [Users2] WHERE LoginId=@LoginId and userlevel='J'";
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
                dbOldPasswordHash = dt.Rows(0)("Password").ToString();
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
        //TODO: to be modified
        //obtains a particular user.
        public static DataTable GetUserBankDetails(string LoginId, string StateCode, string DistrictCode)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode, BM.BankName FROM Users2 U INNER JOIN m_bank BM ON U.BankCode=BM.BankCode WHERE u.LoginId=@LoginId  and u.StateCode=@StateCode and u.DistrictCode=@DistrictCode";
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
                    ParameterName = "@StateCode",
                    DbType = DbType.String,
                    Size = 2,
                    Value = StateCode
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@DistrictCode",
                    DbType = DbType.String,
                    Size = 3,
                    Value = DistrictCode
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

        public static DataTable GetUserBankDetailsSLBC(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode, BM.BankName FROM Users2 U INNER JOIN m_bank BM ON U.BankCode=BM.BankCode WHERE u.LoginId=@LoginId  and u.userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("F")
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
        public static DataTable GetDetailsSLBC(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode,u.UserLevel,mb.BankName,u.StateCode,ms.StateName FROM Users2 U INNER JOIN M_SLBC BM ON U.StateCode=BM.StateCode inner join M_Bank mb on mb.BankCode=bm.BankCode inner join M_State ms on ms.StateCode=bm.StateCode WHERE u.LoginId=@LoginId  and u.userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("S")
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
        //Digital Banking
        public static DataTable GetDetailsDIGI(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode,u.BankName FROM Users2 U WHERE u.LoginId=@LoginId  and u.userlevel=@userlevel";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 20,
                    Value = LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userlevel",
                    DbType = DbType.String,
                    Size = 1,
                    Value = Encoder.HtmlEncode("D")
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
        public static DataTable GetUserBankDetailspmjdy(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode, BM.BankName FROM Users2 U INNER JOIN m_bank BM ON U.BankCode=BM.BankCode WHERE u.LoginId=@LoginId  and u.userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("P")
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
        //get data for SLBC credentials
        public static DataTable GetSLBCBankDetails(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U.LoginId, U.BankCode, BM.BankName FROM Users2 U INNER JOIN m_bank BM ON U.BankCode=BM.BankCode WHERE u.LoginId=@LoginId  and u.StateCode=@StateCode and u.DistrictCode=@DistrictCode";
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

            //return the datatable containing result rows.
            return dt;
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
                cmd.CommandText = "UPDATE [Users2] SET failed_login_attempts=0 WHERE LoginId=@LoginId  ";
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
        //returns a particular user.
        public static DataTable GetUser(string LoginId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId ";
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
        //returns all users
        public static DataTable GetUsers()
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] where UserLevel<>'A'";
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
        public static DataTable GetUsersDistrictWise()
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT (select bankname from m_bank mb where mb.bankcode=us.bankcode )as bankname,* FROM [Users2] us where UserLevel in('F','I')";
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
        //returns existing failed login attempts
        public static int GetExistingFaildLoginAttemps(string LoginId)
        {

            int attempts = 0;
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ISNULL(failed_login_attempts,0) AS AttemptsCount FROM Users2 WHERE LoginID=@LoginID ";
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
                attempts = int.Parse(dt.Rows(0)("AttemptsCount").ToString());
            }
            else
            {
                attempts = 0;
            }

            //return the datatable containing result rows.
            return attempts;
        }
        //Digital Banking
        public static DateTime GetLastLoginDateTimeforDIGI(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM Users2 WHERE LoginID=@LoginID and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("D")
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
                if (dt.Rows(0).IsNull("Date_last_login"))
                {
                    lastLoginDateTime = null;
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows(0)("Date_last_login").ToString());
                }
            }
            else
            {
                lastLoginDateTime = null;
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public static bool UpdateLoginDateTimeSLBC(string LoginID)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE [Users2] SET Date_last_login=GETDATE() WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("F")
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
        public static bool IsUserReusingOldPassword(string LoginId, string NewPasswordHash, string bankcode)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users2 WHERE LoginId=@LoginId and bankcode=@bankcode  AND (OldPassword1=@PasswordHash OR OldPassword2=@PasswordHash OR OldPassword3=@PasswordHash) and  userlevel in ('F','I')";
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
                    Size = 15,
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
        public static bool IsSLBCReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users2 WHERE LoginId=@LoginId  AND (OldPassword1=@PasswordHash OR OldPassword2=@PasswordHash OR OldPassword3=@PasswordHash)";
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
        public static bool IsJansurakshaReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users2 WHERE LoginId=@LoginId  AND (OldPassword1=@PasswordHash OR OldPassword2=@PasswordHash OR OldPassword3=@PasswordHash)";
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
        public static bool IsAdminReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users2 WHERE LoginId=@LoginId  AND (OldPassword1=@PasswordHash OR OldPassword2=@PasswordHash OR OldPassword3=@PasswordHash)";
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

        public static bool IsPMJDYReusingOldPassword(string LoginId, string NewPasswordHash)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users2 WHERE LoginId=@LoginId  AND (OldPassword1=@PasswordHash OR OldPassword2=@PasswordHash OR OldPassword3=@PasswordHash) and userlevel='P'";
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
        //changes user password and returns true, false otherwise.
        public static bool ChangePassword(string LoginID, string NewPasswordHash, string bankcode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Users2 SET OldPassword3=(SELECT OldPassword2 FROM Users2 WHERE LoginId=@LoginId and bankcode=@bankcode and userlevel in ('F','I') ),OldPassword2=(SELECT OldPassword1 FROM Users2 WHERE LoginId=@LoginId and bankcode=@bankcode and userlevel in ('F','I') ),OldPassword1=(SELECT [Password] FROM Users2 WHERE LoginId=@LoginId and bankcode=@bankcode and userlevel in ('F','I') ),[Password]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE LoginId=@LoginId and bankcode=@bankcode and userlevel in ('F','I') ";
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
        public static bool ChangeSLBCPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Users2 SET OldPassword3=(SELECT OldPassword2 FROM Users2 WHERE LoginId=@LoginId),OldPassword2=(SELECT OldPassword1 FROM Users2 WHERE LoginId=@LoginId),OldPassword1=(SELECT [Password] FROM Users2 WHERE LoginId=@LoginId),[Password]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE LoginId=@LoginId and userlevel='S'";
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
        public static bool ChangeJansurakshaPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Users2 SET OldPassword3=(SELECT OldPassword2 FROM Users2 WHERE LoginId=@LoginId),OldPassword2=(SELECT OldPassword1 FROM Users2 WHERE LoginId=@LoginId),OldPassword1=(SELECT [Password] FROM Users2 WHERE LoginId=@LoginId),[Password]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE LoginId=@LoginId and userlevel='J'";
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
        public static bool ChangePMJDYPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Users2 SET OldPassword3=(SELECT OldPassword2 FROM Users2 WHERE LoginId=@LoginId),OldPassword2=(SELECT OldPassword1 FROM Users2 WHERE LoginId=@LoginId),OldPassword1=(SELECT [Password] FROM Users2 WHERE LoginId=@LoginId),[Password]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE LoginId=@LoginId and userlevel='P'";
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
        public static bool ChangeAdminPassword(string LoginID, string NewPasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Users2 SET OldPassword3=(SELECT OldPassword2 FROM Users2 WHERE LoginId=@LoginId),OldPassword2=(SELECT OldPassword1 FROM Users2 WHERE LoginId=@LoginId),OldPassword1=(SELECT [Password] FROM Users2 WHERE LoginId=@LoginId),[Password]=@NewPasswordHash,Date_last_password_change=GETDATE() WHERE LoginId=@LoginId";
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
        //returns user-level if user authenticates, "NOTAUTHENTICATED" string otherwise.
        public static string Authenticate(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId ";
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticateAdmin(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId";
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticateSLBC(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("S")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        //Digital Banking
        public static string AuthenticateDIGI(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("D")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticatePMJDY(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("P")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticateforBanks(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("F")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticateJansuraksha(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("J")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        public static string AuthenticateDir(string LoginId, string SaltedPasswordHash, string Salt)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("D")
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
                string dbPasswordHash = dt.Rows(0)("Password").ToString();
                string saltedDbPasswordHash = SHA256Hasher.Hash(Salt + dbPasswordHash);

                if (SaltedPasswordHash.Equals(saltedDbPasswordHash))
                {
                    //success
                    return dt.Rows(0)("UserLevel").ToString();
                }
            }


            //failure
            return "NOTAUTHENTICATED";
        }
        //returns true if user with loginid already exists, false otherwise.
        public static bool IsLoginAlreadyExist(string LoginId)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM [Users2] WHERE LoginId=@LoginId ";
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

            //return the datatable containing result rows.
            return (dt != null & dt.Rows.Count > 0);
        }
        //deletes an existing login.
        public static bool DeleteUser(string LoginID, string bankcode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM [Users2] WHERE LoginId=@LoginId and bankcode=@bankcode";
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

        //TODO: needs to be changed
        //creates a user account and returns true if success, false otherwise.
        public static bool CreateUser(string BankCode, string LoginID, string PasswordHash)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO [Users2]([BankCode],[LoginId],[Password],[UserLevel]) VALUES(@BankCode,@LoginId,@PasswordHash,'F')";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@BankCode",
                    DbType = DbType.String,
                    Size = 15,
                    Value = BankCode
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 15,
                    Value = LoginID
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    DbType = DbType.String,
                    Size = 64,
                    Value = PasswordHash
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
        //Enabled User
        public static bool EnableUser(string BankCode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "update M_Bank set pmjdy=1 where bankcode=@BankCode";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@BankCode",
                    DbType = DbType.String,
                    Size = 15,
                    Value = BankCode
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
        //Disabled User
        public static bool DisableUser(string BankCode)
        {

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "update M_Bank set pmjdy=0 where bankcode=@BankCode";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@BankCode",
                    DbType = DbType.String,
                    Size = 15,
                    Value = BankCode
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
        public static DateTime GetLastLoginDateTime(string LoginId, string statecode, string districtcode)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM Users2 WHERE LoginID=@LoginID and statecode=@statecode and Districtcode=@districtcode";
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
                    ParameterName = "@statecode",
                    DbType = DbType.String,
                    Size = 2,
                    Value = statecode
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@districtcode",
                    DbType = DbType.String,
                    Size = 3,
                    Value = districtcode
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
                if (dt.Rows(0).IsNull("Date_last_login"))
                {
                    lastLoginDateTime = null;
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows(0)("Date_last_login").ToString());
                }
            }
            else
            {
                lastLoginDateTime = null;
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        //slbc time
        public static DateTime GetLastLoginDateTimeSLBC(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM Users2 WHERE LoginID=@LoginID and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("F")
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
                if (dt.Rows(0).IsNull("Date_last_login"))
                {
                    lastLoginDateTime = null;
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows(0)("Date_last_login").ToString());
                }
            }
            else
            {
                lastLoginDateTime = null;
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        //pmjdy
        public static DateTime GetLastLoginDateTimePMJDY(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM Users2 WHERE LoginID=@LoginID and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("P")
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
                if (dt.Rows(0).IsNull("Date_last_login"))
                {
                    lastLoginDateTime = null;
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows(0)("Date_last_login").ToString());
                }
            }
            else
            {
                lastLoginDateTime = null;
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
        public static DateTime GetLastLoginDateTimeDir(string LoginId)
        {

            DateTime lastLoginDateTime = default(DateTime);
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Date_last_login FROM Users2 WHERE LoginID=@LoginID and userlevel=@userlevel";
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
                    Value = Encoder.HtmlEncode("D")
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
                if (dt.Rows(0).IsNull("Date_last_login"))
                {
                    lastLoginDateTime = null;
                }
                else
                {
                    lastLoginDateTime = DateTime.Parse(dt.Rows(0)("Date_last_login").ToString());
                }
            }
            else
            {
                lastLoginDateTime = null;
            }

            //return the datatable containing result rows.
            return lastLoginDateTime;
        }
    }
}