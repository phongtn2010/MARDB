using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Management;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Reflection;

namespace DomainModel
{   
    public class Connection
    {
        public static string ConnectionString = "";
        public static string ConnectionString1 = "";
        public static string ConnectionString2 = "";
        public static string ConnectionString3 = "";
        public static string ConnectionString4 = "";
        public static string ConnectionString5 = "";
        public static string ConnectionString6 = "";
        public static string ConnectionString7 = "";
        
        private static String Get_ConnectionString(int KetNoi)
        {
            String sConnString = ConnectionString;
            switch (KetNoi)
            {
                case 1:
                    sConnString = ConnectionString1;
                    break;
                case 2:
                    sConnString = ConnectionString2;
                    break;
                case 3:
                    sConnString = ConnectionString3;
                    break;
                case 4:
                    sConnString = ConnectionString4;
                    break;
                case 5:
                    sConnString = ConnectionString5;
                    break;
                case 6:
                    sConnString = ConnectionString6;
                    break;
                case 7:
                    sConnString = ConnectionString7;
                    break;
                default:
                    sConnString = ConnectionString;
                    break;
            }
            return sConnString;
        }

        public static void Set_ConnectionString(int KetNoi, String strConnectionString)
        {
            switch (KetNoi)
            {
                case 1:
                    ConnectionString1 = strConnectionString;
                    break;
                case 2:
                    ConnectionString2 = strConnectionString;
                    break;
                case 3:
                    ConnectionString3 = strConnectionString;
                    break;
                case 4:
                    ConnectionString4 = strConnectionString;
                    break;
                case 5:
                    ConnectionString5 = strConnectionString;
                    break;
                case 6:
                    ConnectionString6 = strConnectionString;
                    break;
                case 7:
                    ConnectionString7 = strConnectionString;
                    break;
                default:
                    ConnectionString = strConnectionString;
                    break;
            }
        }

        public static int GetParameters(ref SqlCommand cmd, int KetNoi = 0)
        {
            int vR = 0;
            SqlConnection conn = null;
            //try
            //{
            conn = new SqlConnection(Get_ConnectionString(KetNoi));
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.Connection = conn;
            vR = cmd.ExecuteNonQuery();
            //cmd.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    vR = 0;
            //}
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
            }
            return vR;
        }

        public static DataTable GetDataTable(SqlCommand cmd, int KetNoi = 0)
        {
            DataTable dtR = null;
            SqlConnection conn = null;
            //try
            //{
            conn = new SqlConnection(Get_ConnectionString(KetNoi));
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            cmd.Connection = conn;
            IDataReader rdr = cmd.ExecuteReader();
            dtR = GetDataTableFromDataReader(rdr);
            rdr.Close();
            //}
            //catch
            //{
            //    dtR = null;
            //}
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Dispose();
            return dtR;
        }

        public static List<T> GetList<T>(SqlCommand cmd, int KetNoi = 0) where T : class, new()
        {
            List<T> res = new List<T>();

            SqlConnection conn = null;
            //try
            //{
            conn = new SqlConnection(Get_ConnectionString(KetNoi));
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            cmd.Connection = conn;
            IDataReader r = cmd.ExecuteReader();
            res = GetToListFromDataReader<T>(r);
            r.Close();

            //}
            //catch
            //{
            //    dtR = null;
            //}

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Dispose();

            return res;
        }


        public static List<T> GetList_News<T>(SqlCommand cmd, int KetNoi = 0)
        {
            try
            {
                SqlConnection conn = null;
                conn = new SqlConnection(Get_ConnectionString(KetNoi));
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                IDataReader dr = cmd.ExecuteReader();
                if (dr == null || dr.FieldCount == 0)
                    return null;
                int fCount = dr.FieldCount;
                Type m_Type = typeof(T);
                object obj;
                List<T> m_List = new List<T>();
                while (dr.Read())
                {
                    obj = Activator.CreateInstance(m_Type);
                    for (int i = 0; i < fCount; i++)
                    {
                        if (dr[i] != DBNull.Value)
                        {
                            var p = m_Type.GetProperty(dr.GetName(i));
                            if (p != null)
                            {
                                try
                                {
                                    var type = p.PropertyType;
                                    //var value = Convert.ChangeType(dr[i], type);
                                    var value = ChangeType(dr[i], type);
                                    p.SetValue(obj, value, null);
                                }
                                catch (Exception e)
                                {
                                }
                            }
                        }
                    }
                    m_List.Add((T)obj);
                }
                dr.Close();
                return m_List;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return null;
        }

        public static int UpdateDatabase(SqlCommand cmd, int KetNoi = 0)
        {
            int vR = 0;
            SqlConnection conn = null;
            //try
            //{
            conn = new SqlConnection(Get_ConnectionString(KetNoi));
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            cmd.Connection = conn;
            vR = cmd.ExecuteNonQuery();
            //}
            //catch
            //{
            //    vR = 0;
            //}
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Dispose();
            return vR;
        }

        public static void UpdateTransaction(List<SqlCommand> lstCmd, int KetNoi = 0)
        {
            if (lstCmd.Count > 0)
            {
                SqlConnection conn = null;

                conn = new SqlConnection(Connection.Get_ConnectionString(KetNoi));
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlTransaction Transaction = conn.BeginTransaction();
                for (int i = 0; i < lstCmd.Count; i++)
                {
                    SqlCommand cmd = lstCmd[i];
                    cmd.Connection = conn;
                    // Enlist the commands into this transaction.
                    cmd.Transaction = Transaction;
                    // Execute the commands.
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                Transaction.Commit();
                Transaction.Dispose();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
            }
        }

        public static object GetValue(string SQL, object DefaultValue, int KetNoi = 0)
        {
            SqlCommand cmd = new SqlCommand(SQL);
            object vR = GetValue(cmd, DefaultValue, KetNoi);
            cmd.Dispose();
            return vR;
        }

        public static String GetValueString(string SQL, object DefaultValue, int KetNoi = 0)
        {
            Object vR = GetValue(SQL, DefaultValue, KetNoi);
            return String.Format("{0}", vR);
        }

        public static object GetValue(SqlCommand cmd, object DefaultValue, int KetNoi = 0)
        {
            object vR = DefaultValue;
            DataTable dt = GetDataTable(cmd, KetNoi);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    vR = dt.Rows[0][0];
                }
            }
            dt.Dispose();
            return vR;
        }

        public static string GetValueString(SqlCommand cmd, string DefaultValue, int KetNoi = 0)
        {
            return Convert.ToString(GetValue(cmd, DefaultValue, KetNoi));
        }

        public static DataTable GetDataTable(string SQL, int KetNoi = 0)
        {
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dtR = GetDataTable(cmd, KetNoi);
            cmd.Dispose();
            return dtR;
        }

        public static List<T> GetList<T>(String SQL, int KetNoi = 0) where T : class, new()
        {
            List<T> res = new List<T>();

            SqlCommand cmd = new SqlCommand(SQL);
            res = GetList<T>(cmd, KetNoi);
            cmd.Dispose();

            return res;
        }

        private static DataTable GetDataTableFromDataReader(IDataReader dataReader)
        {
            DataTable schemaTable = dataReader.GetSchemaTable();
            DataTable resultTable = new DataTable();
            int d = 0;
            foreach (DataRow dataRow in schemaTable.Rows)
            {
                d++;
                DataColumn dataColumn = new DataColumn();
                String ColumnName = dataRow["ColumnName"].ToString();
                if (resultTable.Columns.IndexOf(ColumnName) >= 0)
                {
                    ColumnName = ColumnName + d;
                }
                dataColumn.ColumnName = ColumnName;
                dataColumn.DataType = Type.GetType(dataRow["DataType"].ToString());
                //dataColumn.ReadOnly = (bool)dataRow["IsReadOnly"];
                //dataColumn.AutoIncrement = (bool)dataRow["IsAutoIncrement"];
                //dataColumn.Unique = (bool)dataRow["IsUnique"];


                resultTable.Columns.Add(dataColumn);
            }
            int nCols = resultTable.Columns.Count;
            while (dataReader.Read())
            {
                DataRow dataRow = resultTable.NewRow();

                for (int i = 0; i < nCols; i++)
                {
                    dataRow[i] = dataReader[i];
                }
                resultTable.Rows.Add(dataRow);
            }

            return resultTable;
        }
        
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }


        //List<Person> personList = new List<Person>();
        //personList = GetToListFromDataReader<Person>(dataReaderForPerson);
        public static List<T> GetToListFromDataReader<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            if (dr == null || dr.FieldCount == 0)
                return null;
            int iCount = dr.FieldCount;
            Type m_Type = typeof(T);
            object obj;
            while (dr.Read())
            {
                obj = Activator.CreateInstance(m_Type);
                for (int i = 0; i < iCount; i++)
                {
                    if (dr[i] != DBNull.Value)
                    {
                        var p = m_Type.GetProperty(dr.GetName(i));
                        if (p != null)
                        {
                            try
                            {
                                var type = p.PropertyType;
                                var value = ChangeType(dr[i], type);
                                p.SetValue(obj, value, null);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
                list.Add((T)obj);
            }
            dr.Close();
            return list;
        }

        public static int DeleteRecord(string TableName, string FieldKeyName, object FieldKeyValue, int KetNoi = 0)
        {
            string SQL = "";
            int vR = 0;

            SQL = String.Format("DELETE FROM {0} WHERE {1}=@FieldKeyName;", TableName, FieldKeyName);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL;
            cmd.Parameters.AddWithValue("@FieldKeyName", FieldKeyValue);
            vR = UpdateDatabase(cmd, KetNoi);
            cmd.Dispose();
            return vR;
        }

        public static int InsertRecord(string TableName, SqlCommand cmd, int KetNoi = 0)
        {
            return InsertRecord(TableName, "", cmd, KetNoi);
        }

        public static int InsertRecord(string TableName, string FieldKeyName, SqlCommand cmd, int KetNoi = 0)
        {
            SqlParameterCollection prms = cmd.Parameters;
            string SQL = "", DSTruong = "", DSGiaTri = "";
            int i, vR = 0;

            for (i = 0; i < prms.Count; i++)
            {
                if (prms[i].ParameterName != "@" + FieldKeyName)
                {
                    string ParamName = prms[i].ParameterName;
                    string FieldName = ParamName.Substring(1);
                    if (ThongTinBangCSDL.TonTaiTruong(TableName, FieldName, KetNoi))
                    {
                        DSGiaTri += ((DSGiaTri == "") ? "" : ",") + ParamName;
                        DSTruong += ((DSTruong == "") ? "" : ",") + FieldName;
                    }
                }
            }
            if (DSTruong != "")
            {
                SQL = String.Format("INSERT INTO {0}({1}) VALUES({2});", TableName, DSTruong, DSGiaTri);
                cmd.CommandText = SQL;
                vR = UpdateDatabase(cmd, KetNoi);
            }
            return vR;
        }

        public static int UpdateRecord(string TableName, string FieldKeyName, SqlCommand cmd, int KetNoi = 0)
        {
            SqlParameterCollection prms = cmd.Parameters;
            string SQL = "", str = "", KeyParameterName = "@" + FieldKeyName.ToUpper();
            string Condition = FieldKeyName + "=@" + FieldKeyName;
            int i, vR = 0;
            object FieldKeyValue = prms["@" + FieldKeyName].Value;

            for (i = 0; i < prms.Count; i++)
            {
                if (prms[i].ParameterName.ToUpper() != KeyParameterName)
                {
                    string ParamName = prms[i].ParameterName;
                    string FieldName = ParamName.Substring(1);
                    if (ThongTinBangCSDL.TonTaiTruong(TableName, FieldName, KetNoi))
                    {
                        str += ((str == "") ? "" : ",") + FieldName + "=" + ParamName;
                    }
                }
            }
            if (str != "")
            {
                SQL = String.Format("UPDATE {0} SET {1} WHERE {2};", TableName, str, Condition);
                cmd.CommandText = SQL;
                vR = UpdateDatabase(cmd, KetNoi);
            }
            return vR;
        }

        public static int InsertOrUpdateRecord(string TableName, string FieldKeyName, SqlCommand cmd, int KetNoi = 0)
        {
            int vR = 0;
            Boolean ok = true;
            if (cmd.Parameters.IndexOf("@" + FieldKeyName) >= 0)
            {
                object FieldKeyValue = cmd.Parameters["@" + FieldKeyName].Value;
                if (String.IsNullOrEmpty((string)(FieldKeyValue)) == false)
                {
                    ok = false;
                }
            }
            if (ok)
            {
                vR = InsertRecord(TableName, FieldKeyName, cmd, KetNoi);
            }
            else
            {
                vR = UpdateRecord(TableName, FieldKeyName, cmd, KetNoi);
            }
            return vR;
        }
               
        public static DataTable DtTbl(DataTable[] dtToJoin)
        {
            DataTable dtJoined = new DataTable();

            foreach (System.Data.DataColumn dc in dtToJoin[0].Columns)
                dtJoined.Columns.Add(dc.ColumnName);

            foreach (System.Data.DataTable dt in dtToJoin)
                foreach (System.Data.DataRow dr1 in dt.Rows)
                {
                    System.Data.DataRow dr = dtJoined.NewRow();
                    foreach (System.Data.DataColumn dc in dtToJoin[0].Columns)
                        dr[dc.ColumnName] = dr1[dc.ColumnName];

                    dtJoined.Rows.Add(dr);
                }

            return dtJoined;
        }

        public static DataTable Gep_Hai_DataTable_GiongNhau(DataTable dt1, DataTable dt2)
        {
            DataTable dt = new DataTable();
            foreach (DataColumn col in dt1.Columns)
            {
                if (dt.Columns[col.ColumnName] == null)
                    dt.Columns.Add(col.ColumnName, col.DataType);
            }
            int nCols = dt.Columns.Count;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < nCols; j++)
                {
                    dataRow[j] = dt1.Rows[i][j];
                }
                dt.Rows.Add(dataRow);
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < nCols; j++)
                {
                    dataRow[j] = dt2.Rows[i][j];
                }
                dt.Rows.Add(dataRow);
            }

            return dt;

        }
        
        public static DataTable JoinDataTables(DataTable t1, DataTable t2, params Func<DataRow, DataRow, bool>[] joinOn)
        {
            DataTable result = new DataTable();
            foreach (DataColumn col in t1.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in t2.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataRow row1 in t1.Rows)
            {
                var joinRows = t2.AsEnumerable().Where(row2 =>
                {
                    foreach (var parameter in joinOn)
                    {
                        if (!parameter(row1, row2)) return false;
                    }
                    return true;
                });
                foreach (DataRow fromRow in joinRows)
                {
                    DataRow insertRow = result.NewRow();
                    foreach (DataColumn col1 in t1.Columns)
                    {
                        insertRow[col1.ColumnName] = row1[col1.ColumnName];
                    }
                    foreach (DataColumn col2 in t2.Columns)
                    {
                        insertRow[col2.ColumnName] = fromRow[col2.ColumnName];
                    }
                    result.Rows.Add(insertRow);
                }
            }
            return result;
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t); ;
            }

            return Convert.ChangeType(value, t);
        }

        private static object castValue(object value, string sourceType, Type targetType)
        {
            //* This implementation requires further extension to convert types as per requirement arose.
            switch (sourceType)
            {
                case "System.DateTime":
                    switch (targetType.Name)
                    {
                        case "TimeSpan":
                            var dtValue = (DateTime)value;
                            TimeSpan tsValue = dtValue.TimeOfDay;
                            value = tsValue;
                            break;
                    }
                    break;
            }

            return value;
        }
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }
            if(items != null)
            {
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }
            
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }


    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DataField : Attribute
    {
        public string dbField { get; set; }
        public string sourceType { get; set; }
        public string sourceDataFormatter { get; set; }
        public string targetDataFormatter { get; set; }
    }
}
