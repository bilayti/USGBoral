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
using System.Net;
using System.Web.Configuration;
using System.Xml;
using System.Security;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Security.Application;
using NewApp.Models;
namespace NewApp.Models
{
    public class ProjectConfig : DBConnection
    {
        VariableClass objVc = new VariableClass();
        GlobalMethod objGm = new GlobalMethod();

        public static string DBConnectionString
        {
            get
            {
                //Return WebConfigurationManager.ConnectionStrings("WebAppConStr").ConnectionString
                string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnectionStringBuilder ConnectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
                return ConnectionBuilder.ConnectionString;
                //' Return Encoder.HtmlEncode(WebConfigurationManager.ConnectionStrings("WebAppConStr").ConnectionString)

            }
        }
        public int InvalidLoginAttemptLockCount
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["InvalidLoginAttemptLockCount"].ToString());
            }
        }
        public int MaxUploadSizeInMB
        {
            get { return int.Parse(WebConfigurationManager.AppSettings["MaxUploadSizeInMB"].ToString()); }
        }
        public static int MaxInvalidLoginAttempts
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["MaxInvalidLoginAttempts"].ToString());
            }
        }
        public static int FreshnessOfUploadInDays
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["FreshnessOfUploadInDays"].ToString());
            }
        }
        public static int DisplayForNDays
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["DisplayForNDays"].ToString());
            }
        }
        public static int DisplayForNDaysVC
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["DisplayForNDaysVC"].ToString());
            }
        }
        public static string MailServer
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailServer"].ToString();
            }
        }
        public static int MailPort
        {
            get
            {
                return int.Parse(WebConfigurationManager.AppSettings["MailPort"].ToString());
            }
        }
        public static bool MailIsSSL
        {
            get
            {
                return bool.Parse(WebConfigurationManager.AppSettings["MailIsSSL"].ToString());
            }
        }
        public static string MailSenderEmail
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailSenderEmail"].ToString();
            }
        }
        public static string MailSenderUsername
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailSenderUsername"].ToString();
            }
        }
        public static string MailSenderPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailSenderPassword"].ToString();
            }
        }
        public static string MailSenderDisplayName
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailSenderDisplayName"].ToString();
            }
        }
        public static string HtmlEncode(string plainInput)
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(plainInput);
        }
        public static string UrlEncode(string plainInput)
        {
            return Microsoft.Security.Application.Encoder.UrlEncode(plainInput);
        }
        public static string JavaScriptEncode(string plainInput)
        {
            return Microsoft.Security.Application.Encoder.JavaScriptEncode(plainInput);
        }
    }
}

