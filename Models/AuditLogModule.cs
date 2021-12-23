using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Microsoft.Security.Application;

namespace NewApp.Models
{
    public class AuditLogModule:DBConnection
    {
        public static DataTable GetLogEntries(DateTime fromDate, DateTime toDate)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM AuditLog WHERE dateadd(dd,0, datediff(dd,0, OperationPerformedDateTime)) BETWEEN dateadd(dd,0, datediff(dd,0, @FromDate)) AND dateadd(dd,0, datediff(dd,0, @ToDate)) ORDER BY RecordID DESC";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromDate",
                    DbType = DbType.DateTime,
                    Value = fromDate
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ToDate",
                    DbType = DbType.DateTime,
                    Value = toDate
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
        //returns audit-log detail
        public static DataTable GetLogEntries(long RecordId)
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM AuditLog WHERE RecordID=@RecordID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@RecordID",
                    DbType = DbType.Int64,
                    Value = (RecordId)
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
        //returns audit-log for displaying.
        public static DataTable GetLogEntries()
        {


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM AuditLog ORDER BY RecordID DESC";
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
        //adds new entry to audit-log
        public static void LogEntry(string OperationDetails)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO AuditLog([OperationDetails],[OperationPerformedDateTime],[LoginId],[FromPage],[FromIP],[UrlReferrer],[UserAgent]) VALUES(@OperationDetails,@OperationPerformedDateTime,@LoginId,@FromPage,@FromIP,@UrlReferrer,@UserAgent)";
                cmd.CommandType = CommandType.Text;
                string _OperationDetails = OperationDetails.Replace("'", " ").Replace("-", " ");
                string _LoginId = null;
                if (HttpContext.Current.Session["LoginId"] != null)
                {
                    _LoginId = HttpContext.Current.Session["LoginId"].ToString();
                }
                else
                {
                    _LoginId = "N.A";
                }
                string _FromPage = null;
                if (HttpContext.Current.Request.RawUrl != null)
                {
                    _FromPage = ((HttpContext.Current.Request.RawUrl.Replace("'", " ").Replace("-", " ")));
                }
                else
                {
                    _FromPage = "N.A";
                }
                string _FromIP = ((string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]).ToString().Replace("'", " ").Replace("-", " "));
                string _UrlReferrer = null;
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    _UrlReferrer = (HttpContext.Current.Request.UrlReferrer.ToString().Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UrlReferrer = "N.A";
                }
                string _UserAgent = null;
                if (HttpContext.Current.Request.UserAgent != null)
                {
                    _UserAgent = (HttpContext.Current.Request.UserAgent.Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UserAgent = "N.A";
                }

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationDetails",
                    DbType = DbType.String,
                    Value = _OperationDetails
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationPerformedDateTime",
                    DbType = DbType.DateTime,
                    Value = DateTime.Now
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromPage",
                    DbType = DbType.String,
                    Value = _FromPage
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromIP",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _FromIP
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UrlReferrer",
                    DbType = DbType.String,
                    Size = 100,
                    Value = _UrlReferrer
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserAgent",
                    DbType = DbType.String,
                    Value = _UserAgent
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

        }
        public static void LogEntry(string OperationDetails, string LoginId)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO AuditLog([OperationDetails],[OperationPerformedDateTime],[LoginId],[FromPage],[FromIP],[UrlReferrer],[UserAgent]) VALUES(@OperationDetails,@OperationPerformedDateTime,@LoginId,@FromPage,@FromIP,@UrlReferrer,@UserAgent)";
                cmd.CommandType = CommandType.Text;

                string _OperationDetails = OperationDetails.Replace("'", " ").Replace("-", " ");
                string _LoginId = null;
                if (LoginId != null)
                {
                    _LoginId = LoginId;
                }
                else
                {
                    _LoginId = "N.A";
                }
                string _FromPage = null;
                if (HttpContext.Current.Request.RawUrl != null)
                {
                    _FromPage = (HttpContext.Current.Request.RawUrl.Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _FromPage = "N.A";
                }
                string _FromIP = ((string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]).ToString().Replace("'", " ").Replace("-", " "));
                string _UrlReferrer = null;
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    _UrlReferrer = (HttpContext.Current.Request.UrlReferrer.ToString().Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UrlReferrer = "N.A";
                }
                string _UserAgent = null;
                if (HttpContext.Current.Request.UserAgent != null)
                {
                    _UserAgent = (HttpContext.Current.Request.UserAgent.Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UserAgent = "N.A";
                }
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationDetails",
                    DbType = DbType.String,
                    Value = _OperationDetails
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationPerformedDateTime",
                    DbType = DbType.DateTime,
                    Value = DateTime.Now
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromPage",
                    DbType = DbType.String,
                    Value = _FromPage
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromIP",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _FromIP
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UrlReferrer",
                    DbType = DbType.String,
                    Size = 100,
                    Value = _UrlReferrer
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserAgent",
                    DbType = DbType.String,
                    Value = _UserAgent
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

        }
        public static void LogEntry(string OperationDetails, string LoginId, string RawUrl, string IpAddress, string UrlReferrer, string UserAgent)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = default(SqlCommand);
            int result = 0;

            try
            {
                con.ConnectionString = ProjectConfig.DBConnectionString;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO AuditLog([OperationDetails],[OperationPerformedDateTime],[LoginId],[FromPage],[FromIP],[UrlReferrer],[UserAgent]) VALUES(@OperationDetails,@OperationPerformedDateTime,@LoginId,@FromPage,@FromIP,@UrlReferrer,@UserAgent)";
                cmd.CommandType = CommandType.Text;

                string _OperationDetails = OperationDetails.Replace("'", " ").Replace("-", " ");
                string _LoginId = null;
                if (LoginId != null)
                {
                    _LoginId = LoginId;
                }
                else
                {
                    _LoginId = "N.A";
                }
                string _FromPage = null;
                if (RawUrl != null)
                {
                    _FromPage = (RawUrl.Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _FromPage = "N.A";
                }
                string _FromIP = IpAddress;
                string _UrlReferrer = null;
                if (UrlReferrer != null)
                {
                    _UrlReferrer = (UrlReferrer.ToString().Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UrlReferrer = "N.A";
                }
                string _UserAgent = null;
                if (UserAgent != null)
                {
                    _UserAgent = (UserAgent.Replace("'", " ").Replace("-", " "));
                }
                else
                {
                    _UserAgent = "N.A";
                }

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationDetails",
                    DbType = DbType.String,
                    Value = _OperationDetails
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OperationPerformedDateTime",
                    DbType = DbType.DateTime,
                    Value = DateTime.Now
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LoginId",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _LoginId
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromPage",
                    DbType = DbType.String,
                    Value = _FromPage
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FromIP",
                    DbType = DbType.String,
                    Size = 50,
                    Value = _FromIP
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UrlReferrer",
                    DbType = DbType.String,
                    Size = 100,
                    Value = _UrlReferrer
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserAgent",
                    DbType = DbType.String,
                    Value = _UserAgent
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

        }
    }
}