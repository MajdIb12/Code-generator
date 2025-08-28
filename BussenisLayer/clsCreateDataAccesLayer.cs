using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BussenisLayer
{
    public class clsCreateDataAccesLayer
    {
        public List<string> TablesName { get; set; }
        public clsCreateDataAccesLayer()
        {
            TablesName = new List<string>();
            DataTable dt = clsSQLTableData.GetAllTablesData();
            foreach (DataRow T in dt.Rows)
            {
                TablesName.Add(T[0].ToString());
            }
        }


        public bool CreateDataAccesLayer()
        {
            CreateConnectionSettings_DataAcces();
            foreach (string T in TablesName)
            {
               if(!CreateCSFile(T))
                    return false;
            }
            return true;
        }


        private string createDataAccesFile(string TableName)
        {
            string PathFile = Path.Combine(clsGlobal.PathFiles, "DataAccesFiles");
            string File = Path.Combine(PathFile, $"{TableName}.cs");
            string directory = Path.GetDirectoryName(File);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return File;
        }




        private bool CreateCSFile(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("namespace DataAccesLayer");
            sb.AppendLine("{");
            sb.AppendLine($"    public class cls{TableName}Data");
            sb.AppendLine("     {");
            sb.AppendLine(          AddNew_DataAcces(TableName));
            sb.AppendLine();
            sb.AppendLine(          Update_DataAcces(TableName));
            sb.AppendLine();
            sb.AppendLine(          GetByID_DataAcces(TableName));
            sb.AppendLine();
            sb.AppendLine(          GetAll_DataAcces(TableName));
            sb.AppendLine();
            sb.AppendLine(          Delete_DataAcces(TableName));
            sb.AppendLine();
            sb.AppendLine(          IsExist_DataAcces(TableName));
            sb.AppendLine("}\n}");
            string PathFile = createDataAccesFile(TableName + "Data");
            try
            {
                File.WriteAllText(PathFile, sb.ToString());
                return true;
            }
            catch {}
            
            return false;
            
        }




        private string AddNew_DataAcces(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            List <string> ColumnIsNulluble = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
                ColumnIsNulluble.Add(row[2].ToString());
            }
            sb.AppendLine($"public static int AddNew{TableName}Data(");
            for (int i = 1; i < ColumnName.Count; i++)
            {
                if  (i == ColumnName.Count - 1)
                {
                    sb.Append($"{clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]})");
                    break;
                }
                sb.Append($"{clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]}, ");
            }
            sb.AppendLine("{");
            sb.AppendLine(@"int ID = -1;
            string Connectionstring = clsConnectionSettings.ConnectionString;");
            sb.AppendLine("using (SqlConnection conn = new SqlConnection(Connectionstring))");
            sb.AppendLine("{");
            sb.AppendLine($@"SqlCommand cmd = new SqlCommand(""SP_AddNew{TableName}"", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;");
            sb.AppendLine();
            for (int i = 1; i < ColumnName.Count; i++)
            {
                if (ColumnIsNulluble[i] == "YES")
                {
                    sb.AppendLine($"if (!string.IsNullOrEmpty({ColumnName[i]}.ToString()))");
                }
                sb.AppendLine($@"cmd.Parameters.AddWithValue(""@{ColumnName[i]}"", {ColumnName[i]});");
            }
            sb.AppendLine($@"SqlParameter IDParam = new SqlParameter(""@{ColumnName[0]}"", System.Data.SqlDbType.Int)
                {{
                    Direction = System.Data.ParameterDirection.Output
                }};
                cmd.Parameters.Add(IDParam);
                try
                {{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    ID = (int)cmd.Parameters[""@{ColumnName[0]}""].Value;
                    
                }}
                catch {{ }}
                

            }}
            return ID;
        }}");
            return sb.ToString();
        }

        private string GetByID_DataAcces(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            List<string> ColumnIsNulluble = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
                ColumnIsNulluble.Add(row[2].ToString());
            }
            sb.AppendLine($"public static bool Get{TableName}ByIdData(");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {
                    sb.Append($"ref {clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]})");
                    
                }
                else if (i == 0)
                {
                    sb.Append($"{clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]}, ");
                }
                else
                {
                    sb.Append($"ref {clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]}, ");
                }
                
            }
            sb.AppendLine("{");
            sb.AppendLine(@"bool Found = false;;
            string Connectionstring = clsConnectionSettings.ConnectionString;");
            sb.AppendLine("using (SqlConnection conn = new SqlConnection(Connectionstring))");
            sb.AppendLine("{");
            sb.AppendLine($@"SqlCommand cmd = new SqlCommand(""SP_Get{TableName}ByID"", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;");
            sb.AppendLine($@"cmd.Parameters.AddWithValue(""@{ColumnName[0]}"", {ColumnName[0]});");
            sb.AppendLine();
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine(@"conn.Open();
                    SqlDataReader Reader = cmd.ExecuteReader();
                    if (Reader.Read())");
            sb.AppendLine("{");
            for (int j = 0; j < ColumnName.Count; j++)
            {
                if (ColumnIsNulluble[j] == "YES")
                {
                    sb.AppendLine($@"if(Reader[""{ColumnName[j]}""] != DBNull.Value)");
                }
                sb.AppendLine($@"{ColumnName[j]} = ({clsGlobal.ConvertDataType(ColumnType[j])})Reader[""{ColumnName[j]}""];");
            }
            sb.AppendLine("Found = true;");
            sb.AppendLine(@"}
                }
                catch {  }
                
            }
            return Found;
        }");
            return sb.ToString();
        }

        private string GetAll_DataAcces(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static DataTable GetAll{TableName}Data()");
            sb.AppendLine($@"{{
    DataTable result = new DataTable();
    string Connectionstring = clsConnectionSettings.ConnectionString;
    using (SqlConnection conn = new SqlConnection(Connectionstring))
    {{
        SqlCommand cmd = new SqlCommand(""SP_GetAll{TableName}"", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        try
        {{
            conn.Open();
            SqlDataReader Reader = cmd.ExecuteReader();
            result.Load(Reader);

        }}
        catch {{ }}
    }}
    return result;
}}");
            return sb.ToString();
        }

        private string Update_DataAcces(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static bool Update{TableName}Data(");
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            List<string> ColumnIsNulluble = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
                ColumnIsNulluble.Add(row[2].ToString());
            }
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {

                    sb.Append($"{clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]})");
                    break;
                }
                sb.Append($"{clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]}, ");
                
            }
            sb.AppendLine("{");
            sb.AppendLine(@"bool Result = false;
            string Connectionstring = clsConnectionSettings.ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connectionstring))");
            sb.AppendLine("{");
            sb.AppendLine($@"SqlCommand cmd = new SqlCommand(""SP_Update{TableName}"", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;");
            sb.AppendLine();
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (ColumnIsNulluble[i] == "YES")
                {
                    sb.AppendLine($"if (!string.IsNullOrEmpty({ColumnName[i]}.ToString()))");
                }
                sb.AppendLine($@"cmd.Parameters.AddWithValue(""@{ColumnName[i]}"", {ColumnName[i]});");
            }
            sb.AppendLine(@"try
                {
                    conn.Open();
                    int RowAffected =  cmd.ExecuteNonQuery();
                    if (RowAffected > 0) { Result = true; }
                    

                }
                catch { }
                

            }
            return Result;
        }");
            return sb.ToString();
        }

        private string Delete_DataAcces(string TableName)
        {
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ID = ColumnNameAndType.Rows[0][0].ToString();
            string Type = ColumnNameAndType.Rows[0][1].ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static bool Delete{TableName}Data({clsGlobal.ConvertDataType(Type)} {ID})");
            sb.AppendLine($@"{{
            bool Result = false;
            string Connectionstring = clsConnectionSettings.ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {{
                SqlCommand cmd = new SqlCommand(""SP_Delete{TableName}ByID"", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(""@{ID}"", {ID});
                try
                {{
                    conn.Open();
                    int RowAffected = cmd.ExecuteNonQuery();
                    if (RowAffected > 0) {{ Result = true; }}

                }}catch {{ }}
            }}   
            return Result;
        }}");
            return sb.ToString();
        }

        private string IsExist_DataAcces(string TableName)
        {
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ID = ColumnNameAndType.Rows[0][0].ToString();
            string Type = ColumnNameAndType.Rows[0][0].ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static bool Is{TableName}ExistData({clsGlobal.ConvertDataType(Type)} {ID})");
            sb.AppendLine($@"{{
            bool Result = false;
            string Connectionstring = clsConnectionSettings.ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {{
                SqlCommand cmd = new SqlCommand(""SP_Is{TableName}Exist"", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(""@{ID}"", {ID});
                try
                {{
                    conn.Open();
                    int RowAffected = (int)cmd.ExecuteScalar();
                    if (RowAffected > 0) {{ Result = true; }}

                }}
                catch {{ }}
            }}
            return Result;
        }}");
            return sb.ToString();
        }

        private void CreateConnectionSettings_DataAcces()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{{
    public class clsConnectionSettings
    {{
        public static string ConnectionString = ""Server=.;Database={clsGlobal.DataBaseName};User Id={clsGlobal.UserName};Password={clsGlobal.Password};"";
    }}
}}");
            string PathFile = createDataAccesFile("ConnectionSettings");
            File.WriteAllText(PathFile, sb.ToString());
        }

    }
}

