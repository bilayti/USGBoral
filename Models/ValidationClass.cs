using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using NewApp.Models;
namespace NewApp.Models
{
    public static class ValidationClass
    {

        public const string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static string[] blackList = { "--", ";--", ";", "/*", "*/", "@@", "@", "char", "nchar", "varchar", "nvarchar", "alter", "begin", "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert", "kill", "open", "select", "sys", "sysobjects", "syscolumns", "table", "update" };

        public static bool IsEmpty(this string inputString)
        {
            inputString = inputString.Trim();

            if (string.IsNullOrEmpty(inputString))
                return true;
            else return false;
        }

        //public static bool IsGreaterThanZero(this string inputString)
        //{
        //    inputString = inputString.Trim();

        //    if (!string.IsNullOrEmpty(inputString))
        //    {
        //        if (inputString.IsValidNumber())
        //        {
        //            if (Convert.ToInt32(inputString) > 0)
        //            {
        //                return true;
        //            }
        //            else return false;
        //        }
        //        else return false;
        //    }
        //    else return false;
        //}

        public static bool IsAlphaNum(this string inputString)
        {
            inputString = inputString.Trim();

            if (string.IsNullOrEmpty(inputString))
                return false;

            return (inputString.ToCharArray().All(c => Char.IsLetter(c) || Char.IsNumber(c)));
        }

        public static bool IsValidEmail(this string inputString)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                validation = Regex.IsMatch(inputString, MatchEmailPattern);
            }
            return validation;
        }

        public static bool IsValidNumber(this string inputString)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                Regex regex = new Regex("^[0-9]+$");
                if (regex.IsMatch(inputString))
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsAlphaNumericSpace(this string inputString)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                Regex r = new Regex("^[a-zA-Z0-9\\s]+$");
                if (r.IsMatch(inputString))
                    validation = true;
            }
            return validation;
        }

        public static bool IsRemarks(this string inputString)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                Regex r = new Regex("^[a-zA-Z\\s]+");

                if (r.IsMatch(inputString))
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsLengthBetween(this string inputString, int minLen, int MaxLen)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                if (inputString.Length < minLen || inputString.Length > MaxLen)
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsMaxLength(this string inputString, int MaxLen)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                if (inputString.Length <= MaxLen)
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsMinLength(this string inputString, int MinLen)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                if (inputString.Length < MinLen)
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsExactLength(this string inputString, int ExactLen)
        {
            bool validation = false;
            inputString = inputString.Trim();

            if (!String.IsNullOrEmpty(inputString))
            {
                if (inputString.Length == ExactLen)
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool IsContainSQLKeyword(this string inputString)
        {
            bool validation = false;
            inputString = inputString.Trim();

            for (int i = 0; i < blackList.Length; i++)
            {
                if ((inputString.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    validation = true;
                }
            }
            return validation;
        }

        public static bool ValidateFileMetadata(Stream fs)
        {

            System.IO.StreamReader rs = null;
            rs = new System.IO.StreamReader(fs, true);
            if (rs.ReadLine().Length <= (1024 * 1024))
            {
                string firstLine = rs.ReadLine().ToString();
                //Dim lastline As String = rs.ReadToEnd()

                if ((firstLine.IndexOf("%PDF") > -1))
                {
                    return true;
                }
            }

            return false;

        }

        public static string Validateexcelextension(string strhyphen)
        {
            if (Regex.IsMatch(strhyphen, "^(([a-zA-Z]:)|(\\\\{2}\\w+)\\$?)(\\\\(\\w[\\w].*))(.xls|.XLS|.xlsx|.XLSX)$") == false)
            {
                return " ";
            }
            return strhyphen.ToString();
        }
        public static bool IsNumeric(this  string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
