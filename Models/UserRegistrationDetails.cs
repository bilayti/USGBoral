using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewApp.Models
{
    public class UserRegistrationDetails
    {
        public int USERID { get; set; }
        public int USER_TYPE_ID { get; set; }
        public int USER_TYPEID { get; set; }
        public string USER_CODE { get; set; }
        public string F_NAME { get; set; }
        public string M_NAME { get; set; }
        public string L_NAME { get; set; }
        public string PASSWORD1 { get; set; }
        public string REMARKS1 { get; set; }
        public string SAP_ID { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public int USER_STATUS { get; set; }
        public string USER_STATUS_NAME { get; set; }
        public string IsActive { get; set; }
        public string USER_TYPE { get; set; }
        public string lastlogin { get; set; }
        public string lastpassword { get; set; }
        // User Authorization
        public string Aging { get; set; }
        public string PendingOrder { get; set; }
        public string ItemPurchase { get; set; }
        public string Invoice { get; set; }
        public string AccountStatement { get; set; }
        public string CollectionSummary { get; set; }
        public string CreditNote { get; set; }
        //User Type Details
        public int User_Count{get;set;}

    }
}