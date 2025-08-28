using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLayer
{
    public class clsCreateBussenisLayer
    {
        private Dictionary<string, string> DefaultValues = new Dictionary<string, string>
                 {
                     { "int", "0" },
                     { "short", "0" },
                     { "long", "0" },
                     { "float", "0f" },
                     { "double", "0.0" },
                     { "decimal", "0m" },
                     { "string", "\"\"" },
                     { "DateTime", "DateTime.Now" },
                     { "bool", "false" }
                 };
        public List<string> TablesName { get; set; }
        public clsCreateBussenisLayer()
        {
            TablesName = new List<string>();
            DataTable dt = clsSQLTableData.GetAllTablesData();
            foreach (DataRow T in dt.Rows)
            {
                TablesName.Add(T[0].ToString());
            }
        }


        public bool CreateBussenisLayer()
        {
            foreach (string T in TablesName)
            {
                if (!CreateCSFile(T))
                    return false;
            }
            return true;
        }


        private string createDataAccesFile(string TableName)
        {
            string PathFile = Path.Combine(clsGlobal.PathFiles, "BussenisFiles");
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
            sb.AppendLine("using DataAccessLayer;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("namespace BussenisLayer");
            sb.AppendLine("{");
            sb.AppendLine($"    public class cls{TableName}");
            sb.AppendLine("     {");
            sb.AppendLine();
            sb.AppendLine(MethodsAndConstructor_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(AddNew_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(Update_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(GetByID_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(GetAll_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(Delete_Bussenis(TableName));
            sb.AppendLine();
            sb.AppendLine(IsExist_Bussenis(TableName));
            sb.AppendLine("}\n}");
            string PathFile = createDataAccesFile(TableName);
            try
            {
                File.WriteAllText(PathFile, sb.ToString());
                return true;
            }
            catch { }

            return false;

        }








        private string MethodsAndConstructor_Bussenis(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"private enum _enMode { AddNew = 1, UpdateMode = 2 };
        private _enMode _Mode = _enMode.AddNew;");
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
            }
            for (int i = 0; i < ColumnName.Count; i++)
            {
                sb.AppendLine($@"public {clsGlobal.ConvertDataType(ColumnType[i])} {ColumnName[i]} {{get; set;}}");
            }

            sb.AppendLine();
            sb.AppendLine($"public cls{TableName}()");
            sb.AppendLine("{");
            sb.AppendLine(@"_Mode = _enMode.AddNew;");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                string dataType = clsGlobal.ConvertDataType(ColumnType[i]);
                string defaultValue = DefaultValues.ContainsKey(dataType)
                        ? DefaultValues[dataType]
                        : $"default({dataType})";
                sb.AppendLine($"{ColumnName[i]} = {defaultValue};");
            }
            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine($"private cls{TableName}(");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                string dataType = clsGlobal.ConvertDataType(ColumnType[i]);
                if (i == ColumnName.Count - 1)
                {
                    sb.Append($"{dataType} @{ColumnName[i]})");
                    break;
                }
                sb.Append($"{dataType} @{ColumnName[i]}, ");
            }
            sb.AppendLine("{");
            sb.AppendLine(@"_Mode = _enMode.UpdateMode;");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                sb.AppendLine($"this.{ColumnName[i]} = @{ColumnName[i]};");
            }
            sb.AppendLine("}");

            sb.AppendLine($@"public bool Save()
        {{
            switch (_Mode)
            {{
                case _enMode.AddNew:
                    if (_AddNew{TableName}())
                    {{
                        _Mode = _enMode.UpdateMode;
                        return true;
                    }}
                    return false;
                case _enMode.UpdateMode:
                    return _Update{TableName}();
            }}
            return false ;
        }}");
            return sb.ToString();
        }

        private string AddNew_Bussenis(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ColumnName = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
            }
            sb.AppendLine($"private bool _AddNew{TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"int ID = cls{TableName}Data.AddNew{TableName}Data(");
            for (int i = 1; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {
                    sb.Append($"{ColumnName[i]});");
                    break;
                }
                sb.Append($"{ColumnName[i]}, ");
            }
            sb.AppendLine("if (ID > 0)");
            sb.AppendLine("{");
            sb.AppendLine($"this.{ColumnName[0]} = ID");
            sb.AppendLine("return true");
            sb.AppendLine("}");
            sb.AppendLine("return false");
            sb.AppendLine("}");
            return sb.ToString();
        }
        
        private string GetByID_Bussenis(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
            }
            sb.AppendLine($"public static cls{TableName} Get{TableName}ById({clsGlobal.ConvertDataType(ColumnType[0])} {ColumnName[0]})");
            sb.AppendLine("{");
            for (int i = 1; i < ColumnName.Count; i++)
            {
                string dataType = clsGlobal.ConvertDataType(ColumnType[i]);
                string defaultValue = DefaultValues.ContainsKey(dataType)
                        ? DefaultValues[dataType]
                        : $"default({dataType})";
                sb.AppendLine($"{dataType} {ColumnName[i]} = {defaultValue};");
            }
            sb.AppendLine($"if(cls{TableName}Data.Get{TableName}ByIdData(");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append($"{ColumnName[i]}, ");
                }
                else if (i == ColumnName.Count - 1)
                {
                    sb.Append($"ref {ColumnName[i]}))");
                }
                else
                {
                    sb.Append($"ref {ColumnName[i]}, ");
                }
            }
            sb.AppendLine("{");
            sb.AppendLine($"    return new cls{TableName}(");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {
                    sb.Append($"{ColumnName[i]});");
                }
                else
                {
                    sb.Append($"{ColumnName[i]}, ");
                }
            }
            sb.AppendLine(@"}
            return null;
        }");
            return sb.ToString();
        }

        private string GetAll_Bussenis(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static DataTable GetAll{TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"return cls{TableName}Data.GetAll{TableName}Data();");
            sb.AppendLine("}");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string Update_Bussenis(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ColumnName = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
            }
            sb.AppendLine($"private bool _Update{TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"return cls{TableName}Data.Update{TableName}Data(");
            for (int i = 0; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {
                    sb.Append($"{ColumnName[i]});");
                    break;
                }
                sb.Append($"{ColumnName[i]}, ");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string Delete_Bussenis(string TableName)
        {
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ID = ColumnNameAndType.Rows[0][0].ToString();
            string Type = ColumnNameAndType.Rows[0][1].ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static bool Delete{TableName}({clsGlobal.ConvertDataType(Type)} {ID})");
            sb.AppendLine("{");
            sb.AppendLine($"return cls{TableName}Data.Delete{TableName}Data({ID});");
            sb.AppendLine();
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string IsExist_Bussenis(string TableName)
        {
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ID = ColumnNameAndType.Rows[0][0].ToString();
            string Type = ColumnNameAndType.Rows[0][1].ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static bool Is{TableName}Exist({clsGlobal.ConvertDataType(Type)} {ID})");
            sb.AppendLine("{");
            sb.AppendLine($"return cls{TableName}Data.Is{TableName}ExistData({ID});");
            sb.AppendLine("}");
            sb.AppendLine();

            return sb.ToString();
        }

        




    }
}
