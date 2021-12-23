using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewApp.Models
{
    public class FormattingHelper
    {
        public static string OriginalOrEmptyString(object value)
        {
            if (value == null)
            {
                return "";
            }

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return "";
            }

            return value.ToString();

        }
        public static string ValidateIntegersOnly(string strint)
        {
            if (Regex.IsMatch(strint, "^[0-9 ]+$") == false)
            {
                return " ";
            }
            return strint.ToString();
        }
        public static string ValidateValueRangeOnly(string strint)
        {
            if (Regex.IsMatch(strint, "^[1-12 ]+$") == false)
            {
                return " ";
            }
            return strint.ToString();
        }
        public static string ValidateYearRangeOnly(string strint)
        {
            if (Regex.IsMatch(strint, "^[12-99 ]+$") == false)
            {
                return " ";
            }
            return strint.ToString();
        }
        public static string ValidateStringsOnly(string str)
        {
            if (Regex.IsMatch(str, "^[a-zA-Z]+$") == false)
            {
                return " ";
            }
            return str.ToString();
        }
        public static string ValidateDate(string strdate)
        {
            if (Regex.IsMatch(strdate, "^(0[1-9]|[12][0-9]|3[01])[- ](0[1-9]|1[012])[- ](19|20)\\d\\d$") == false)
            {
                return " ";
            }
            return strdate.ToString();
        }
        public static string ValidateAlphabetsandDot(string strdot)
        {
            if (Regex.IsMatch(strdot, "^[A-Za-z](?>\\.?[A-Za-z]+)*\\.?$") == false)
            {
                return " ";
            }
            return strdot.ToString();
        }
        public static string ValidateAlphabetsanddash(string strhyphen)
        {
            if (Regex.IsMatch(strhyphen, "^[a-zA-Z0-9\\-]+$") == false)
            {
                return " ";
            }
            return strhyphen.ToString();
        }
        public static string Validateexcelextension(string strhyphen)
        {
            if (Regex.IsMatch(strhyphen, "^(([a-zA-Z]:)|(\\\\{2}\\w+)\\$?)(\\\\(\\w[\\w].*))(.xls|.XLS|.xlsx|.XLSX)$") == false)
            {
                return " ";
            }
            return strhyphen.ToString();
        }
        public static string ValidatedecimalOnly(string strint)
        {
            if (Regex.IsMatch(strint, "^\\d+(\\.\\d+)?$") == false)
            {
                return " ";
            }
            return strint.ToString();
        }
    }
}