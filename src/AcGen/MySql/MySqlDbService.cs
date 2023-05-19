using Chloe.MySql;
using MySqlConnector;
using System.Data;

namespace AcGen.MySql
{
    internal class MySqlDbService : IDbService
    {
        string ConnString;
        public MySqlDbService(string connString)
        {
            this.ConnString = connString;
        }
        public List<DbTableInfo> GetTables(List<string> tablesOnly)
        {
            using IDbConnection conn = new MySqlConnection(this.ConnString);
            string database = conn.Database;

            using MySqlContext dbContext = new MySqlContext(() =>
            {
                return new MySqlConnection(this.ConnString);
            });

            var q = dbContext.Query<Table>().Where(a => a.TABLE_SCHEMA == database);
            if (tablesOnly.Count > 0)
            {
                q = q.Where(a => tablesOnly.Contains(a.TABLE_NAME));
            }

            var tables = q.ToList();
            var columns = dbContext.Query<Column>().Where(a => a.TABLE_SCHEMA == database);

            List<DbTableInfo> dbTables = new List<DbTableInfo>();

            foreach (var table in tables)
            {
                DbTableInfo dbTable = new DbTableInfo();
                dbTable.Name = table.TABLE_NAME;
                dbTable.Schema = table.TABLE_SCHEMA;
                dbTable.Comment = table.TABLE_COMMENT;

                var tableColumns = columns.Where(a => a.TABLE_NAME == table.TABLE_NAME).ToList();

                foreach (var tableColumn in tableColumns.OrderBy(a => a.ORDINAL_POSITION))
                {
                    DbColumnInfo dbColumn = new DbColumnInfo();
                    dbColumn.Name = tableColumn.COLUMN_NAME;
                    dbColumn.Ordinal = tableColumn.ORDINAL_POSITION;
                    dbColumn.DataTypeName = GetMapCsharpTypeName(tableColumn);
                    dbColumn.Length = tableColumn.CHARACTER_MAXIMUM_LENGTH == null ? null : (int)tableColumn.CHARACTER_MAXIMUM_LENGTH;
                    dbColumn.Precision = tableColumn.NUMERIC_PRECISION == null ? null : (byte)tableColumn.NUMERIC_PRECISION;
                    dbColumn.Scale = tableColumn.NUMERIC_SCALE == null ? null : (byte)tableColumn.NUMERIC_SCALE;
                    dbColumn.Comment = tableColumn.COLUMN_COMMENT;
                    dbColumn.IsPrimaryKey = tableColumn.IsPrimaryKey();
                    dbColumn.IsAutoIncrement = tableColumn.IsAutoIncrement();
                    dbColumn.IsNullable = tableColumn.IsNullable();

                    dbTable.Columns.Add(dbColumn);
                }

                dbTables.Add(dbTable);
            }

            return dbTables;
        }

        static string GetMapCsharpTypeName(Column column)
        {
            string dbType = column.DATA_TYPE;
            bool isNullable = column.IsNullable();

            if (dbType == "int")
            {
                string lowerColumnName = column.COLUMN_NAME.ToLower();
                if (lowerColumnName.StartsWith("is_"))
                {
                    return isNullable ? "bool?" : "bool";
                }

                if (lowerColumnName.StartsWith("is"))
                {
                    if (column.COLUMN_NAME.Length > 2 && char.IsUpper(column.COLUMN_NAME[2]))
                    {
                        return isNullable ? "bool?" : "bool";
                    }
                }
            }

            switch (dbType)
            {
                case "char":
                case "varchar":
                case "tinytext":
                case "text":
                case "mediumtext":
                case "longtext":
                    return "string";
                case "int":
                    return isNullable ? "int?" : "int";
                case "bigint":
                    return isNullable ? "long?" : "long";
                case "float":
                    return isNullable ? "float?" : "float";
                case "double":
                    return isNullable ? "double?" : "double";
                case "decimal":
                    return isNullable ? "decimal?" : "decimal";
                case "datetime":
                case "timestamp":
                    return isNullable ? "DateTime?" : "DateTime";
                case "enum":
                    return "string";
                case "binary":
                    return "byte[]";
                case "tinyint":
                    return isNullable ? "byte?" : "byte";
                case "smallint":
                    return isNullable ? "short?" : "short";
                default:
                    break;
            }

            throw new NotSupportedException($"未支持数据库类型 {dbType} 映射");
        }
    }
}
