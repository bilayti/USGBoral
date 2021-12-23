using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewApp.Captcha
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public  string _pwdFieldID = "";
        public string PasswordFieldID
        {
            get
            {
                return _pwdFieldID;
            }

            set
            {
                _pwdFieldID = value;
            }
        }
        public  Int32 _NumOfChars = 6;
        public int NumberOfChars
        {
            get
            {
                return _NumOfChars;
            }

            set
            {
                if (value >= 1)
                {
                    _NumOfChars = value;
                }
                else
                {
                    _NumOfChars = 6;
                }
            }
        }
        public  bool _CaseSensitiveCode = false;
        public bool CaseSensitiveCode
        {
            get
            {
                return _CaseSensitiveCode;
            }

            set
            {
                _CaseSensitiveCode = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return ValidateCaptcha();
            }
        }
        public  bool _ShowBorder = false;
        public bool ShowBorder
        {
            set
            {
                if (value == true)
                {
                    CaptchaDiv.Style.Add("border", "solid 1px #ccc");
                }
                else
                {
                    CaptchaDiv.Style.Remove("border");
                }
            }

            get
            {
                return _ShowBorder;
            }
        }
        public  string _validationGroup = "";
        public string ValidationGroup
        {
            get
            {
                return _validationGroup.ToString();
            }

            set
            {
                rvfCaptchaCode.ValidationGroup = value;
                _validationGroup = value;
            }
        }

        public bool ValidateCaptcha()
        {
            string storedCaptchaCode = Session["CaptchaCode"].ToString();
            if (CaseSensitiveCode)
            {
                return txtCaptchaCodeInput.Text.Trim() == storedCaptchaCode;
            }

            return txtCaptchaCodeInput.Text.Trim().ToLower() == storedCaptchaCode.ToLower();
        }
        public void Generate()
        {
            Session["CaptchaCode"] = GenerateCode();
            imgCaptcha.ImageUrl = "../Captcha/GetCaptchaCodedImage.ashx";
        }
        public string GenerateCode()
        {
            string[] charsMap = new string[61] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder("");
            int i;
            for (i = 1; i <= NumberOfChars; i++)
            {
                sb.Append(charsMap[rnd.Next(charsMap.Length)]);
            }

            return sb.ToString();
        }
        protected void btnReGenerate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Clear();
            Generate();
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Generate();
                rvfCaptchaCode.ValidationGroup = this.ValidationGroup;
            }
        }
        public override void Focus()
        {
            txtCaptchaCodeInput.Focus();
        }
        public void Clear()
        {
            txtCaptchaCodeInput.Text = "";
        }
    }
}