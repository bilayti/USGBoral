using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewApp.Models;

namespace NewApp.Models
{
    public class VariableClass:DBConnection
    {
        public String ERROR_MESSAGE { get; set; }

        public String XML { get; set; }
        public Int32 RCRE_USER { get; set; }
        public DateTime RCRE_TIME { get; set; }
        public string MODIFY_USER { get; set; }
        public DateTime MODIFY_TIME { get; set; }
        public string DML_MODE { get; set; }



        public string MENUID_GROUP { get; set; }
        public string USERID_GROUP { get; set; }
        public string ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public string LAST_UPDATED_DATE { get; set; }
        public string MANAGE_MODE { get; set; }
        public string MACHINE_ID { get; set; }
        public string SESSION_ID { get; set; }
        public int VALIDATION_USER { get; set; }

        public int USER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public int LOGIN_ID { get; set; }
        public string PASSWORD { get; set; }
        public string NEW_PASSWORD { get; set; }
        public string USER_TYPE { get; set; }
        public string SOL_ID { get; set; }
        public int RPU_ID { get; set; }
        public string EMAIL { get; set; }
        public string CC_MAIL { get; set; }
        public string MOBILE { get; set; }
        public string LOCATION { get; set; }
        public int STATE_ID { get; set; }
        public int LCHG_USER_ID { get; set; }
        public string DEL_FLG { get; set; }
        public string IS_LOGGED_ON { get; set; }
        public string IS_LOCKED { get; set; }
        public string IS_FIRST_LOGIN { get; set; }
        public int QUES_ID { get; set; }
        public string ANSWER { get; set; }
        public string LAST_NAME { get; set; }

        //circular class

        public string LANG_CODE { get; set; }
        public string CIRCULAR_ID { get; set; }
        public string CIRCULAR_DATE { get; set; }
        public string CIRCULAR_DESC { get; set; }
        public string NEW_FLG { get; set; }
        public string FILE_PATH { get; set; }
        public string FILE_EXTN { get; set; }
        public string ACTION_FLG { get; set; }

        //media gallery class
        public string ALBUM_ID { get; set; }
        public string ALBUM_TITLE { get; set; }
        public string ALBUM_DESCRIPTION { get; set; }
        public string ALBUM_TYPE { get; set; }

        //master data management
        public int RMT_ID { get; set; }
        public string RMT_TEXT { get; set; }
        public string REF_ID { get; set; }


        public int RMD_ID { get; set; }
        public int PARENT_RMD_ID { get; set; }
        public string RMD_TEXT { get; set; }

        //feedback master

        public Int32 FEEDBACK_ID { get; set; }
        public Int32 FEEDBACK_TYP { get; set; }
        public String REF_NO { get; set; }
        public Int32 DISTRICT_ID { get; set; }
        public Int32 BANK_ID { get; set; }
        public String BRANCH_NAME { get; set; }
        public String CUSTOMER_NAME { get; set; }
        public String PHONE { get; set; }
        public String COMMENTS { get; set; }
        public Int32 STATUS { get; set; }
        public String VERIFY_FLG { get; set; }
        public Int32 FEEDBACK_TYPE_ID { get; set; }
        public Int32 DEPARTMENT_ID { get; set; }
    }

}