using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccesLayer;
using System.Threading.Tasks;

namespace BussenisLayer
{
    public static class clsGlobal
    {
        public static DataTable SqlDataBases()
        {
            clsConnection.UserName = UserName;
            clsConnection.Password = Password;
            return clsConnection.GetAllDatabasees();
        }

        public static bool CheckIsDataBaseExist()
        {
            return clsConnection.IsDatabaseExist(DataBaseName);
        }

        public static string ConvertDataType(string DataType)
        {
            
            switch(DataType)
            {
                case "bigint":
                    return "long";
                case "bit":
                    return "bool";
                case "char":
                    return "string";
                case "date":
                    return "DateTime";
                case "datetime":
                    return "DateTime";
                case "datetime2":
                    return "DateTime";
                case "datetimeoffset":
                    return "DateTimeOffset";
                case "decimal":
                    return "decimal";
                case "float":
                    return "double";
                case "int":
                    return "int";
                case "money":
                    return "decimal";
                case "smallmoney":
                    return "decimal";
                case "nchar":
                    return "string";
                case "varchar":
                    return "string";
                case "nvarchar":
                    return "string";
                case "tinyint":
                    return "byte";
                case "time":
                    return "TimeSpan";
                case "smalldatetime":
                    return "DateTime";
                default:
                    return "object";

            }
        }

        public static string DataBaseName { get; set; }

        public static string PathFiles {  get; set; }

        public static string UserName { get; set; }
        public static string Password { get; set; }
    }
}
