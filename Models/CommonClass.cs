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
using System.Net.Mail;
using System.Web.Mail;
using System.Net;
using System.Web.Configuration;
using System.Xml;
using System.Security;
using Microsoft.ApplicationBlocks.Data;
using NewApp.Models;

namespace NewApp.Models
{
    public class CommonClass : DBConnection
    {
        VariableClass objVc = new VariableClass();
        //Admin objAdmin = new Admin();
        DBConnection obj = new DBConnection();
        GlobalMethod objGm = new GlobalMethod();

        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string MACHINE_ID { get; set; }
        public string SESSION_ID { get; set; }

        # region "Private variables"

        private string WholeCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0987654321";
        private string NumberCharacters = "0987654321";
        private string NumberUpperLetterCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0987654321";
        const string passphrase = "password";

        # endregion


        private const string initVector = "tu89geji340t89u2";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                //return "";
                throw ex;
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                //return "";
                throw ex;
            }
        }

        public static string GetVirtualPath(string physicalPath)
        {
            string rootpath = System.Web.HttpContext.Current.Server.MapPath("~/");
            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");
            return "~/" + physicalPath;
        }

        public DataTable getWebConfig()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataReader dr;
                dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, "proc_get_email_authentication_detail");
                dt.Load(dr);
                return dt;
            }
            catch (Exception e)
            {
                //ErrorMessage = e.Message;
                //return null;
                throw e;
            }
        }

        //public static string Encrypt(string StringValue)
        //{
        //    byte[] key = { };
        //    byte[] IV = { 0x32, 0x41, 0x54, 0x67, 0x73, 0x21, 0x47, 0x19 };
        //    MemoryStream ms = null;

        //    try
        //    {
        //        string encryptionKey = "bd5ygNc8";
        //        key = Encoding.UTF8.GetBytes(encryptionKey);
        //        byte[] bytes = Encoding.UTF8.GetBytes(StringValue);
        //        DESCryptoServiceProvider dcp = new DESCryptoServiceProvider();
        //        ICryptoTransform ict = dcp.CreateEncryptor(key, IV);
        //        ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
        //        cs.Write(bytes, 0, bytes.Length);
        //        cs.FlushFinalBlock();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Convert.ToBase64String(ms.ToArray());
        //}

        //public static string Decrypt(string StringValue)
        //{
        //    byte[] key = { };
        //    byte[] IV = { 0x32, 0x41, 0x54, 0x67, 0x73, 0x21, 0x47, 0x19 };
        //    MemoryStream ms = null;

        //    try
        //    {
        //        string encryptionKey = "bd5ygNc8";
        //        key = Encoding.UTF8.GetBytes(encryptionKey);
        //        byte[] bytes = new byte[StringValue.Length];
        //        bytes = Convert.FromBase64String(StringValue);
        //        DESCryptoServiceProvider dcp = new DESCryptoServiceProvider();
        //        ICryptoTransform ict = dcp.CreateDecryptor(key, IV);
        //        ms = new MemoryStream();
        //        CryptoStream cryptoStream = new CryptoStream(ms, ict, CryptoStreamMode.Write);
        //        cryptoStream.Write(bytes, 0, bytes.Length);
        //        cryptoStream.FlushFinalBlock();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write(ex.Message);
        //    }
        //    Encoding en = Encoding.UTF8;
        //    return en.GetString(ms.ToArray());
        //}

        public string ConvertDataTableToXML(DataTable dtBuildSQL)
        {
            try
            {
                DataSet table = new DataSet();
                table.Tables.Add(dtBuildSQL);
                System.IO.StringWriter writer = new System.IO.StringWriter();
                //notice that we're ignoring the schema so we get clean XML back
                //you can change the write mode as needed to get your result
                table.WriteXml(writer);
                string dataTableXml = writer.ToString();
                return dataTableXml;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //return "";
                throw ex;
            }
        }

        public string ConvertDataTableToXML(DataSet dsBuildSQL)
        {
            try
            {
                //DataSet table = new DataSet();
                //table.Tables.Add(dtBuildSQL);
                System.IO.StringWriter writer = new System.IO.StringWriter();
                //notice that we're ignoring the schema so we get clean XML back
                //you can change the write mode as needed to get your result
                dsBuildSQL.WriteXml(writer, XmlWriteMode.IgnoreSchema);
                dsBuildSQL.WriteXml("C:\\a.xml");
                string dataTableXml = writer.ToString();
                return dataTableXml;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //return "";
                throw ex;

            }
        }

        public static string ConvertDataTableToHtml(DataTable targetTable)
        {
            string htmlString = "";
            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
            }
            StringBuilder htmlBuilder = new StringBuilder();
            //Create Top Portion of HTML Document
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>");
            htmlBuilder.Append("Page-");
            htmlBuilder.Append(Guid.NewGuid().ToString());
            htmlBuilder.Append("</title>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            htmlBuilder.Append("style='border: solid 1px Black; font-size: small;'>");
            //Create Header Row
            htmlBuilder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn targetColumn in targetTable.Columns)
            {
                htmlBuilder.Append("<td align='left' valign='top'>");
                htmlBuilder.Append(targetColumn.ColumnName);
                htmlBuilder.Append("</td>");
            }
            htmlBuilder.Append("</tr>");
            //Create Data Rows
            foreach (DataRow myRow in targetTable.Rows)
            {
                htmlBuilder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }
                htmlBuilder.Append("</tr>");
            }
            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");
            //Create String to be Returned
            htmlString = htmlBuilder.ToString();
            return htmlString;
        }

        public string shaHash(string PlainText)
        {

            //ASSUMPTION password will be in english only (i.e ascii)
            //if password can be multilingual then use Encoding.UTF8.GetBytes() instead.

            //obtain byte array of plain text
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);

            //create managed instance of Sha256 algo
            SHA256Managed sha = new SHA256Managed();

            //obtain hash in byte array
            byte[] hashBytes = sha.ComputeHash(plainTextBytes);

            //obtain hexadecimal string of the hash.
            StringBuilder sb = new StringBuilder();

            foreach (byte hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }

            //return the hash string now
            return sb.ToString();
        }

        public string RandomText(int offset)
        {
            Random rnd = new Random();
            bool loop = true;
            int StrLen = WholeCharacters.Length;
            string SecureCharacters = "", ch;
            int Pos;
            while (loop)
            {
                Pos = rnd.Next(StrLen);
                ch = WholeCharacters.Substring(Pos, 1);
                if (SecureCharacters.IndexOf(ch) < 0)
                {
                    SecureCharacters = SecureCharacters + ch;
                    if (SecureCharacters.Length == offset)
                        loop = false;
                }
            }
            return SecureCharacters;
        }

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

        public string TicketRandomNumText(int offset)
        {
            Random rnd = new Random();
            bool loop = true;
            int StrLen = NumberUpperLetterCharacters.Length;
            string SecureCharacters = "", ch;
            int Pos;
            while (loop)
            {
                Pos = rnd.Next(StrLen);
                ch = NumberUpperLetterCharacters.Substring(Pos, 1);
                if (SecureCharacters.IndexOf(ch) < 0)
                {
                    SecureCharacters = SecureCharacters + ch;
                    if (SecureCharacters.Length == offset)
                        loop = false;
                }
            }
            return SecureCharacters;
        }

        public static void EmptyTextBoxes(Control parent, TextBox tb)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)(c)).Text = string.Empty;
                    tb.Focus();
                }
                else if (c.GetType() == typeof(DropDownList))
                {
                    ((DropDownList)(c)).SelectedIndex = 0;
                }
                else if (c.GetType() == typeof(HiddenField))
                {
                    ((HiddenField)(c)).Value = string.Empty;
                }
                //else if (c.GetType() == typeof(GridView))
                //{
                //    ((GridView)(c)).DataSource = null;
                //    ((GridView)(c)).DataBind();
                //}
                if (c.HasControls())
                {
                    EmptyTextBoxes(c, tb);
                }
            }
        }

        public DataTable CSVToDataTable(string filepath)
        {
            //Convert CSV to DataTable
            DataTable dt = new DataTable();
            string[] csvRows = System.IO.File.ReadAllLines(filepath);
            string[] fields = null;
            foreach (string csvRow in csvRows)
            {
                fields = csvRow.Split('|');
                DataRow row = dt.NewRow();
                row.ItemArray = fields;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable Pivot(DataTable dataValues, string keyColumn, string pivotNameColumn, string pivotValueColumn)
        {
            DataTable tmp = new DataTable();
            DataRow r;
            string LastKey = "//dummy//";
            int i, pValIndex, pNameIndex;
            string s;
            bool FirstRow = true;

            pValIndex = dataValues.Columns[pivotValueColumn].Ordinal;
            pNameIndex = dataValues.Columns[pivotNameColumn].Ordinal;

            for (i = 0; i <= dataValues.Columns.Count - 1; i++)
            {
                if (i != pValIndex && i != pNameIndex)
                    tmp.Columns.Add(dataValues.Columns[i].ColumnName, dataValues.Columns[i].DataType);
            }

            r = tmp.NewRow();

            foreach (DataRow row1 in dataValues.Rows)
            {
                if (row1[keyColumn].ToString() != LastKey)
                {
                    if (!FirstRow)
                        tmp.Rows.Add(r);

                    r = tmp.NewRow();
                    FirstRow = false;

                    //loop thru fields of row1 and populate tmp table
                    for (i = 0; i <= row1.ItemArray.Length - 3; i++)
                        r[i] = row1[tmp.Columns[i].ToString()];

                    LastKey = row1[keyColumn].ToString();
                }

                s = row1[pNameIndex].ToString();

                if (!tmp.Columns.Contains(s))
                    tmp.Columns.Add(s, dataValues.Columns[pNameIndex].DataType);
                r[s] = row1[pValIndex];
            }

            //add that final row to the datatable:
            tmp.Rows.Add(r);

            return tmp;
        }

        public static string ConvertDateString(string dt)
        {
            if (!String.IsNullOrEmpty(dt))
            {
                string[] date = dt.Split('-');
                DateTime dtResult = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                return String.Format("{0:ddd, MMM d, yyyy}", dtResult);
            }
            else
                return string.Empty;
        }

        public static string ConvertDate(string dt)
        {
            if (!String.IsNullOrEmpty(dt))
            {
                string[] date = dt.Split('-');
                DateTime dtResult = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                return String.Format("{0:ddd, MMM d, yyyy}", dtResult);
            }
            else
                return string.Empty;
        }

        public static string ConvertSqlServerDate(string dt)
        {
            if (!String.IsNullOrEmpty(dt))
            {
                string[] date = dt.Split('-');
                //DateTime dtResult = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                return date[2].ToString() + "-" + date[1].ToString() + "-" + date[0].ToString();
            }
            else
                return string.Empty;
        }

        public static string AddSqlServerDay(string dt, int daycnt)
        {
            if (!String.IsNullOrEmpty(dt))
            {
                string[] date = dt.Split('-');
                DateTime date1 = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0])).AddDays(daycnt);
                return date1.ToString("dd-MM-yyyy");
            }
            else
                return string.Empty;
        }

        public static DateTime ConvertCsharpDateTime(string dt)
        {
            if (!String.IsNullOrEmpty(dt))
            {
                string[] date = dt.Split('-');
                DateTime dtResult = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                return dtResult;
            }
            else
                return DateTime.Now;
        }

        public static DataSet ToDataSet(string[] input)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add();
            DataColumn dc = new DataColumn("SEAT");
            dataTable.Columns.Add(dc);
            Array.ForEach(input, c => dataTable.Rows.Add()[0] = c);
            return dataSet;
        }

        public static DataTable ToDataTable(string input)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEAT");
            dataTable.Columns.Add("FARE");
            DataRow dr;
            string[] col = input.Split('#');

            string[] seat = col[0].Split(',');
            string[] fare = col[1].Split(',');

            for (int k = 0; k < seat.Length; k++)
            {
                dr = dataTable.NewRow();
                dr["SEAT"] = seat[k];
                dr["FARE"] = fare[k];
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

        public static DataTable GetDataTableFromArray(object[] array)
        {
            DataTable dataTable = new DataTable();
            dataTable.LoadDataRow(array, true);
            //Pass array object to LoadDataRow method
            return dataTable;
        }

        public static object CheckSession(object session)
        {
            if (session != null)
            {
                return session;
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("Session_Expired.aspx");
                return null;
            }
        }

        public static object isSessionNullOrEmpty(object session)
        {
            if (session != null)
            {
                return session;
            }
            else
            {
                return "";
            }
        }

        public static object isNullZeroSession(object session)
        {
            if (session != null)
            {
                return session;
            }
            else
            {
                return 0;
            }
        }

        public static void FillAjaxGrid(GridView gv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Columns");
            dt.Rows.Add();
            gv.DataSource = dt;
            gv.DataBind();
            int columncount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
        }

        public static void FileDownload(string FilePath)
        {
            WebClient wc = new WebClient();

            string FileName = Path.GetFileName(FilePath);

            string Extn = Path.GetExtension(FilePath);


            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", FileName));
            if (Extn.ToString().ToLower() == ".pdf")
            {
                response.ContentType = "application/pdf";
            }
            else
            {
                response.ContentType = "application/octet-stream";
            }

            byte[] data = wc.DownloadData(FilePath);
            Stream sr = new MemoryStream(data);
            if (ValidationClass.ValidateFileMetadata(sr))
            {
                response.BinaryWrite(data);
                response.Flush();
                response.End();
            }
        }
        public static string SplitParagraphBetween(string Text, string FirstString, string LastString)
        {
            string STR = Text;
            string STRFirst = FirstString;
            string STRLast = LastString;
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        public static void BindPage(Literal Panel)
        {
            string pageName = Path.GetFileName(System.Web.HttpContext.Current.Request.Path);
            string FolderName = "HTML_ENGLSIH";
            if (System.Web.HttpContext.Current.Session["WLANG"] != null)
            {
                if (System.Web.HttpContext.Current.Session["WLANG"].ToString() == "HI")
                {
                    FolderName = "HTML_HINDI";
                }
            }
            pageName = pageName.Split('.')[0].ToString() + ".htm";

            string html = File.ReadAllText(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + FolderName), pageName));
            //html = SplitParagraphBetween(html, "<body>", "</body>");
            Panel.Text = html;
        }
        public void HitCounter()
        {
            DataSet tmpDs = new DataSet();
            tmpDs.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/counter.xml"));
            int hits = Int32.Parse(tmpDs.Tables[0].Rows[0]["hits"].ToString());
            hits += 1;
            tmpDs.Tables[0].Rows[0]["hits"] = hits.ToString();
            tmpDs.WriteXml(System.Web.HttpContext.Current.Server.MapPath("~/counter.xml"));
        }
        public string ShowVisitorsCount()
        {
            DataSet tmpDs = new DataSet();
            tmpDs.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/counter.xml"));
            return tmpDs.Tables[0].Rows[0]["hits"].ToString();
        }
        public string FindFolderName(string HindiState)
        {
            string folder_name = string.Empty;
            try
            {

                GlobalMethod objGM = new GlobalMethod();

                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("App_Data"), "STATE_MAPPING.txt");

                DataTable dt = new DataTable();

                dt = GlobalMethod.ReadTextFile(path, '|');

                DataView dv = new DataView(dt);

                dv.RowFilter = "STATE_HINDI='" + HindiState + "'";

                if (dv.Count > 0)
                {
                    folder_name = dv[0]["STATE_ENGLISH"].ToString();
                }
            }
            catch (Exception ex)
            {
                //Secure.ExceptionRedirect("");
                throw ex;
            }
            return folder_name;
        }
        public DateTime GetDateTime(string ddmmyyyy)
        {
            string strDateTime = ddmmyyyy;
            string year = strDateTime.Substring(strDateTime.LastIndexOf("/") + 1, 4);
            string month = strDateTime.Substring(strDateTime.IndexOf("/") + 1, 2);
            string day = strDateTime.Substring(0, 2);
            DateTime _dateTime = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));

            return _dateTime;
        }
        public bool ValidateFileMetadata(Stream fs)
        {

            System.IO.StreamReader rs = null;
            rs = new System.IO.StreamReader(fs, true);

            string firstLine = rs.ReadLine().ToString();
            //Dim lastline As String = rs.ReadToEnd()

            if ((firstLine.IndexOf("%PDF") > -1))
            {
                return true;
            }

            return false;
        }
        public static string GetHumanReadableSize(object size_in_bytes)
        {
            string output = "";
            double size_in_kb = Math.Ceiling(Convert.ToDouble(size_in_bytes) / 1024);
            double size_in_mb = Math.Ceiling((size_in_kb / 1024));
            if ((Convert.ToDouble(size_in_bytes) <= 1024))
            {
                output = string.Format("({0} Bytes)", size_in_bytes);
            }
            else if ((size_in_kb <= 1024))
            {
                output = string.Format("({0} KB)", size_in_kb);
            }
            else
            {
                output = string.Format("({0} MB)", size_in_mb);
            }
            return output;
        }

        public void FillDropDown(DropDownList ddl, string DisplayValue, string qstr)
        {
            try
            {

                SqlDataReader dr;
                dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, qstr);
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem(DisplayValue, "0"));
                while (dr.Read())
                {
                    ddl.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                    ddl.DataTextField = dr[0].ToString();
                    ddl.DataValueField = dr[1].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void FillDropDownWithParameter(DropDownList ddl, string DisplayValue, string qstr, string condition)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = qstr;
            SqlConnection con = new SqlConnection(DataConnectionString);

            cmd.Connection = con;
            SqlDataReader dr;
            cmd.Parameters.Add("@condition", SqlDbType.VarChar, 500).Value = condition;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem(DisplayValue, "0"));
                while (dr.Read())
                {
                    ddl.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                    ddl.DataTextField = dr[0].ToString();
                    ddl.DataValueField = dr[1].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        public DataTable Getdatatable(string qstr)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataReader dr;
                dr = SqlHelper.ExecuteReader(DataConnectionString, CommandType.StoredProcedure, qstr);
                dt.Load(dr);
                return dt;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void SendMail(string _to, string _subject, string _body)
        {
            try
            {
                MailAddress _fromMailAddress = new MailAddress(ProjectConfig.MailSenderEmail, ProjectConfig.MailSenderDisplayName);
                // Dim _bccMailAddress As MailAddress = New MailAddress(ProjectConfig.MailSenderEmail, ProjectConfig.MailSenderDisplayName)
                MailAddress _toMailAddress = new MailAddress(_to);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = ProjectConfig.MailServer;
                smtpClient.Port = ProjectConfig.MailPort;
                smtpClient.EnableSsl = ProjectConfig.MailIsSSL;
                // smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(ProjectConfig.MailSenderUsername, ProjectConfig.MailSenderPassword);
                System.Net.Mail.MailMessage _mailMessage = new System.Net.Mail.MailMessage();
                _mailMessage.From = _fromMailAddress;
                // _mailMessage.Bcc.Add(_fromMailAddress)
                _mailMessage.To.Add(_toMailAddress);
                _mailMessage.Subject = ProjectConfig.HtmlEncode(_subject);
                _mailMessage.Body = _body;
                _mailMessage.IsBodyHtml = true;
                smtpClient.Send(_mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Getdatatablewithparameter

        public DataTable Getdatatablewithparameter(string qstr, string condition)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = qstr;
            SqlConnection con = new SqlConnection(DataConnectionString);

            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataReader dr;
            cmd.Parameters.Add("@condition", SqlDbType.VarChar, 200).Value = condition;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        public static string GetDateInHindi(DateTime _date)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("hi-IN");
            return string.Format(ci, "{0:MMM dd, yyyy}", _date);
        }
        #endregion
    }
}
