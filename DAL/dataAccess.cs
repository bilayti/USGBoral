using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Threading;


namespace NewApp
{
    public class dataAccess
    {
        public static string appId = "ConnectionString";
        public static string GetConnectionString()
        {
            string connection = "";
            connection = Convert.ToString(ConfigurationManager.ConnectionStrings[appId]);
            return connection;
        }
        public static SqlConnection GetConnection(string appId)
        {
            SqlConnection con;
            con = new SqlConnection(GetConnectionString());
            con.Open();
            return con;
        }
        public static string ExecuteScalar(string procName, List<Parameters> lstParameters)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;
            foreach (Parameters p in lstParameters)
            {                
                switch (p.Type)
                {
                    case ParameterType.String:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.integer:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Int);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.image:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.ItemImagevalue;
                            break;
                        }
                    case ParameterType.bytearray:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.ItemImagevalue;
                            break;
                        }
                    default:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                }
            }
            string result = Convert.ToString(command.ExecuteScalar());
            con.Close();
            return result;
        }
        public static SqlDataReader GetReader(string procName)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            SqlDataReader dr = default(SqlDataReader);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        public static SqlDataReader GetReader(string procName, List<Parameters> lstParameters)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            SqlDataReader dr = default(SqlDataReader);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            foreach (Parameters p in lstParameters)
            {
                switch (p.Type)
                {
                    case ParameterType.String:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.integer:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Int);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.image:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.bytearray:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    default:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                }
            }
            command.CommandType = CommandType.StoredProcedure;
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        public static DataSet GetDataSet(string procName, List<Parameters> lstParameters)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            foreach (Parameters p in lstParameters)
            {
                command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                command.Parameters[p.Itemname].Value = p.Itemvalue;
            }
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            return ds;
        }
        public static DataSet GetDataSet(string procName)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            return ds;
        }
        public static int ExecuteNonQuery(string procName, List<Parameters> lstParameters)
        {
            SqlConnection con = new SqlConnection();
            con = GetConnection(appId);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;
            foreach (Parameters p in lstParameters)
            {
                switch (p.Type)
                {
                    case ParameterType.String:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.integer:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Int);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                    case ParameterType.image:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.ItemImagevalue;
                            break;
                        }
                    case ParameterType.bytearray:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.Image);
                            command.Parameters[p.Itemname].Value = p.ItemImagevalue;
                            break;
                        }
                    default:
                        {
                            command.Parameters.Add(p.Itemname, SqlDbType.NVarChar);
                            command.Parameters[p.Itemname].Value = p.Itemvalue;
                            break;
                        }
                }
            }
            int result = 0;
            result = command.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //public int ExecuteNonQuery(string procName, Hashtable parameters = null)
        //{
        //    SqlConnection con = new SqlConnection();
        //    con = GetConnection(appId);
        //    SqlCommand command = new SqlCommand();
        //    command.Connection = con;
        //    command.CommandText = procName;
        //    command.CommandType = CommandType.StoredProcedure;
        //    if (parameters != null)
        //    {
        //        foreach (DictionaryEntry p in parameters)
        //        {
        //            command.Parameters.Add(p.Key.ToString(), SqlDbType.NVarChar);
        //            command.Parameters[p.Key.ToString()].Value = p.Value.ToString();
        //        }
        //    }
        //    int result = 0;
        //    result = command.ExecuteNonQuery();
        //    con.Close();
        //    return result;
        //}       
    }
    //This class will be used for passing parameters into business layer
    public class Parameters
    {
        public Parameters(string name, string val)
        {
            _name = name;
            _value = val;
        }
        public Parameters(string name, string val, ParameterType type)
        {
            _name = name;
            _value = val;
            _type = type;
        }
        public Parameters(string name, byte[] val, ParameterType type)
        {
            _name = name;
            _type = type;
            if (type == ParameterType.image || type == ParameterType.bytearray)
            {
                _imagevalue = val;
            }
            else
            {
                _value = val.ToString();
            }
        }
        public string Itemname
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Itemvalue
        {
            get { return _value; }
            set { _value = value; }
        }
        public byte[] ItemImagevalue
        {
            get { return _imagevalue; }
            set { _imagevalue = value; }
        }
        public ParameterType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        string _name;
        string _value;
        byte[] _imagevalue;
        ParameterType _type;
    }
    public enum ParameterType : int { String = 1, integer = 2, image = 3, bytearray = 4 };
}
