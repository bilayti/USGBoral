using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NewApp.Models;


    public class DBConnection
    {
        #region "Private variables"

        private string _sConstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        private static string _ErrMsg = string.Empty;


        #endregion

        #region "Public variables"
        public string DataConnectionString
        {
            get { return _sConstring; }
            set { _sConstring = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }
   #endregion
    }

