using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DataAccesLayer
{
    public static class clsConnection
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string database {  get; set; }

        public static DataTable GetAllDatabasees()
        {
            DataTable dt = new DataTable();
            string ConnString = $"server=.;Database=master;User ID= {UserName};Password={Password}";
            string Query = @"SELECT name FROM master.dbo.sysdatabases
                            WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb');";
            using(SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(Query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                }
                catch { }
            }
            return dt;
            
        }




        public static bool IsDatabaseExist(string Database)
        {
            bool result = false;
            string ConnString = $"server=.;Database=master;User ID= {UserName};Password={Password}";
            string Query = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", Database);
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(Query, conn);
                try
                {
                    conn.Open();
                    object ResultObject = cmd.ExecuteScalar();
                    if (ResultObject != null)
                    {
                        int.TryParse(ResultObject.ToString(), out int Num);
                        if (Num > 0)
                        {
                            result = true;
                        }
                    }

                }
                catch { }
                if (result)
                {
                    database = Database;
                }
                return result;
            }
        }
        public static string ConnectionString()
        {
            return $"server=.;Database={database};User ID= {UserName};Password={Password}";
        }
    }
}
