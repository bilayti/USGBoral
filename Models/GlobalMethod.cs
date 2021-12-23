using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Net;
using System.IO;
using System.Xml;

namespace NewApp.Models
{

    public class GlobalMethod : DBConnection
    {
        public string PROC_NAME { get; set; }

        # region "Private variables"

        private string WholeCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0987654321";
        private string NumberCharacters = "0987654321";
        const string passphrase = "password";

        # endregion


        private const string initVector = "tu89geji340t89u2";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        //public bool CommonInsertMethod(NameValueCollection Params)
        //{
        //    try
        //    {
        //        int Length = Params.Count;
        //        SqlParameter[] oParam = new SqlParameter[Length];
        //        int i = 0;
        //        foreach (String s in Params.AllKeys)
        //        {
        //            oParam[i] = new SqlParameter(s, Params[s]);
        //            i++;
        //        }
        //        SqlHelper.ExecuteNonQuery(DataConnectionString, CommandType.StoredProcedure, PROC_NAME, oParam);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessage = e.Message;
        //        return false;
        //    }
        //}

        //public DataTable CommonDataTableMethod(NameValueCollection Params)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        SqlDataReader dr;
        //        if (Params != null)
        //        {
        //            int Length = Params.Count;
        //            SqlParameter[] oParam = new SqlParameter[Length];
        //            int i = 0;
        //            foreach (String s in Params.AllKeys)
        //            {
        //                oParam[i] = new SqlParameter(s, Params[s]);
        //                i++;
        //            }
        //            dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, PROC_NAME, oParam);
        //            dt.Load(dr);
        //            return dt;
        //        }
        //        else
        //        {
        //            dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, PROC_NAME);
        //            dt.Load(dr);
        //            return dt;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessage = e.Message;
        //        return null;
        //    }
        //}

        //public string CommonXMLMethod(NameValueCollection Params)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        if (Params != null)
        //        {
        //            int Length = Params.Count;
        //            SqlParameter[] oParam = new SqlParameter[Length];
        //            int i = 0;
        //            foreach (String s in Params.AllKeys)
        //            {
        //                oParam[i] = new SqlParameter(s, Params[s]);
        //                i++;
        //            }
        //            ds = SqlHelper.ExecuteDataset(DataConnectionString, CommandType.StoredProcedure, PROC_NAME, oParam);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                return ds.GetXml();
        //            }
        //            else
        //            {
        //                return "";
        //            }
        //        }
        //        else
        //        {
        //            ds = SqlHelper.ExecuteDataset(DataConnectionString, CommandType.StoredProcedure, PROC_NAME);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                return ds.GetXml();
        //            }
        //            else
        //            {
        //                return "";
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessage = e.Message;
        //        return null;
        //    }
        //}

        //public DataSet CommonDataSetMethod(NameValueCollection Params)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        if (Params != null)
        //        {
        //            int Length = Params.Count;
        //            SqlParameter[] oParam = new SqlParameter[Length];
        //            int i = 0;
        //            foreach (String s in Params.AllKeys)
        //            {
        //                oParam[i] = new SqlParameter(s, Params[s]);
        //                i++;
        //            }
        //            ds = SqlHelper.ExecuteDataset(DataConnectionString, CommandType.StoredProcedure, PROC_NAME, oParam);
        //            return ds;
        //        }
        //        else
        //        {
        //            ds = SqlHelper.ExecuteDataset(DataConnectionString, CommandType.StoredProcedure, PROC_NAME);
        //            return ds;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessage = e.Message;
        //        return null;
        //    }
        //}

        public string RandomNumText(int offset)
        {
            Random rnd = new Random();
            bool loop = true;
            int StrLen = NumberCharacters.Length;
            string SecureCharacters = "", ch;
            int Pos;
            while (loop)
            {
                Pos = rnd.Next(StrLen);
                ch = NumberCharacters.Substring(Pos, 1);
                if (SecureCharacters.IndexOf(ch) < 0)
                {
                    SecureCharacters = SecureCharacters + ch;
                    if (SecureCharacters.Length == offset)
                        loop = false;
                }
            }
            return SecureCharacters;
        }

        public static DataTable ExcelToDataTable(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();
                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [Sheet1$]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable ExcelToDataTableStateHindi(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();
                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [Sheet1$]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool isTableExists(DataSet dataSet, string TableName)
        {
            bool is_exists = false;
            // Print each table's TableName.
            foreach (DataTable table in dataSet.Tables)
            {
                if (table.TableName == TableName)
                {
                    is_exists = true;
                }
            }
            return is_exists;
        }
        public static DataTable ReadTextFile(string filePath, char Spliter)
        {
            var reader = ReadAsLines(filePath);
            var data = new DataTable();
            //this assume the first record is filled with the column names
            var headers = reader.FirstOrDefault().Split(Spliter);
            foreach (var header in headers)
            {
                data.Columns.Add(header);
            }
            var records = reader.Skip(1);
            foreach (var record in records)
            {
                data.Rows.Add(record.Split(Spliter).Select(s => s.Trim()).ToArray());
            }

            return data;
        }

        static IEnumerable<string> ReadAsLines(string filename)
        {
            using (var reader = new StreamReader(filename))
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
        }
    }
}
