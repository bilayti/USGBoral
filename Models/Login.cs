using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.ApplicationBlocks.Data;

namespace NewApp.Models
{
    public class Login:DBConnection
    {
        public string USER_CODE { get; set; }
        public string UserName { get; set; }
        public string USER_TYPEID { get; set; }
        public string SAP_ID { get; set; }
        public DataTable UserLogin()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataReader dr;
                SqlParameter[] oParam = new SqlParameter[1];
                oParam[0] = new SqlParameter("@UserName", UserName);
                //oParam[1] = new SqlParameter("@Pwd", Pwd);
                dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, "Sp_UserLogin", oParam);
                dt.Load(dr);
                return dt;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    
}