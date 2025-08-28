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
    public class clsCreateSp
    {
        public List<string> TablesName {  get; set; }

        public clsCreateSp()
        {
            TablesName = new List<string>();
            DataTable dt = clsSQLTableData.GetAllTablesData();
            foreach (DataRow T in dt.Rows)
            {
                TablesName.Add(T[0].ToString());
            }
        }


        public bool CreateSPsFile()
        {
            
            foreach (string TableName in TablesName)
            {
                string Path = createSQlFile(TableName);
                StringBuilder Code = new StringBuilder();
                Code.AppendLine($"USE [{clsGlobal.DataBaseName}]");
                Code.AppendLine("Go");
                Code.AppendLine();
                Code.AppendLine(CreateSp_AddNew(TableName));
                Code.AppendLine(CreateSp_GetAll(TableName));
                Code.AppendLine(CreateSp_GetByID(TableName));
                Code.AppendLine(CreateSp_DeleteByID(TableName));
                Code.AppendLine(CreateSp_Update(TableName));
                Code.AppendLine(CreateSp_IsExist(TableName));
                if (Path == null)
                {
                    return false;
                }
                File.WriteAllText(Path, Code.ToString());
            }
            return true;
            
        }


        private string createSQlFile(string TableName)
        {
            string PathFile = Path.Combine(clsGlobal.PathFiles, "SPsFiles");
            string File = Path.Combine(PathFile, $"{TableName}.sql");
            string directory = Path.GetDirectoryName(File);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return File;
        }

        private string CreateSp_AddNew(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- Add_New SPs");
            sb.AppendLine("Create or Alter procedure SP_AddNew" + TableName);
            List<string> ColumnName = new List<string>();
            List<string> ColumnType = new List<string>();
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            foreach (DataRow row in ColumnNameAndType.Rows)
            {
                ColumnName.Add(row[0].ToString());
                ColumnType.Add(row[1].ToString());
            }
            for (int i = 1; i < ColumnName.Count; i++)
            {
                sb.AppendLine($"@{ColumnName[i]} {ColumnType[i]},");
            }
            sb.AppendLine($"@{ColumnName[0]} {ColumnType[0]} output");
            sb.AppendLine("AS");
            sb.AppendLine("Begin");
            sb.AppendLine($"INSERT INTO {TableName}");
            sb.AppendLine("Values(");
            for (int i = 1; i < ColumnName.Count; i++)
            {
                if (i ==  ColumnName.Count - 1)
                {
                    sb.Append($"@{ColumnName[i]})");
                    break;
                }
                sb.Append($"@{ColumnName[i]}, ");
            }
            sb.AppendLine();
            sb.AppendLine($"SET @{ColumnName[0]} = SCOPE_IDENTITY()");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();

        }

        private string CreateSp_GetByID(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- GetById SPs");
            sb.AppendLine("Create or Alter procedure SP_Get" + TableName + "ByID");
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ColumnName = ColumnNameAndType.Rows[0][0].ToString();
            string ColumnType = ColumnNameAndType.Rows[0][1].ToString();
            sb.AppendLine($"@{ColumnName} {ColumnType}");
            sb.AppendLine("As");
            sb.AppendLine("Begin");
            sb.AppendLine($"Select * from {TableName} where {ColumnName} = @{ColumnName}");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();
        }


        private string CreateSp_GetAll(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- GetAll SPs");
            sb.AppendLine("Create or Alter procedure SP_GetAll" + TableName);
            sb.AppendLine("As");
            sb.AppendLine("Begin");
            sb.AppendLine($"Select * from {TableName}");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();
        }

        private string CreateSp_DeleteByID(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- DeleteById SPs");
            sb.AppendLine("Create or Alter procedure SP_Delete" + TableName + "ByID");
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ColumnName = ColumnNameAndType.Rows[0][0].ToString();
            string ColumnType = ColumnNameAndType.Rows[0][1].ToString();
            sb.AppendLine($"@{ColumnName} {ColumnType}");
            sb.AppendLine("As");
            sb.AppendLine("Begin");
            sb.AppendLine($"Delete from {TableName} where {ColumnName} = @{ColumnName}");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();
        }

        private string CreateSp_Update(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- Update SPs");
            sb.AppendLine("Create or Alter procedure SP_Update" + TableName);
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
                if (i == ColumnName.Count - 1)
                {
                    sb.AppendLine($"@{ColumnName[i]} {ColumnType[i]}");
                    break;
                }
                sb.AppendLine($"@{ColumnName[i]} {ColumnType[i]},");
            }

            sb.AppendLine("AS");
            sb.AppendLine("Begin");
            sb.AppendLine($"Update {TableName}");
            sb.AppendLine("Set ");
            for (int i = 1; i < ColumnName.Count; i++)
            {
                if (i == ColumnName.Count - 1)
                {
                    sb.AppendLine($"[{ColumnName[i]}] = @{ColumnName[i]}");
                    break;
                }
                sb.AppendLine($"[{ColumnName[i]}] = @{ColumnName[i]}, ");
            }
            string PrimaryKey = ColumnNameAndType.Rows[0][0].ToString();
            sb.AppendLine($"Where {PrimaryKey} = @{PrimaryKey}");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();

        }

        private string CreateSp_IsExist(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-- Is Exist SPs");
            sb.AppendLine($"Create or Alter procedure SP_Is{TableName}Exist");
            DataTable ColumnNameAndType = clsSQLTableData.GetAllColumnNameAndTypeData(TableName);
            string ColumnName = ColumnNameAndType.Rows[0][0].ToString();
            string ColumnType = ColumnNameAndType.Rows[0][1].ToString();
            sb.AppendLine($"@{ColumnName} {ColumnType}");
            sb.AppendLine("As");
            sb.AppendLine("Begin");
            sb.AppendLine($"Select 1 from {TableName} where {ColumnName} = @{ColumnName}");
            sb.AppendLine("End");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
