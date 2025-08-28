using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class clsSQLTableData
    {
        public static bool CheckIfTableExistData(string TableName)
        {
            bool Found = false;
            using (SqlConnection Conn = new SqlConnection(clsConnection.ConnectionString()))
            {
                string query = @"IF EXISTS (SELECT 1 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' 
           AND TABLE_NAME=@TableName) 
           SELECT 1 AS res ;";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.AddWithValue("@TableName", TableName);
                try
                {
                    Conn.Open();
                    object Result = cmd.ExecuteScalar();
                    if (Result != null)
                        Found = true;
                }
                catch { }

            }
            return Found;
        }

        public static DataTable GetAllTablesData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection Conn = new SqlConnection(clsConnection.ConnectionString()))
            {
                string query = @"SELECT TABLE_NAME
                            FROM INFORMATION_SCHEMA.TABLES
                            WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME != 'sysdiagrams'";
                SqlCommand cmd = new SqlCommand(query, Conn);
                try
                {
                    Conn.Open();
                    SqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);

                }
                catch { }
                
            }
            return dt;
        }

        public static DataTable GetAllColumnNameAndTypeData(string TableName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Conn = new SqlConnection(clsConnection.ConnectionString()))
            {
                string query = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
                                 FROM INFORMATION_SCHEMA.COLUMNS
                                 WHERE TABLE_NAME = @TableName";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.AddWithValue("@TableName", TableName);
                try
                {
                    Conn.Open();
                    SqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);

                }
                catch { }

            }
            return dt;
        }

    }
}