namespace AcGen
{
    internal class MockDbService : IDbService
    {
        public List<DbTableInfo> GetTables()
        {
            List<DbTableInfo> tables = new List<DbTableInfo>();

            DbTableInfo userTable = new DbTableInfo();
            userTable.Name = "User";
            userTable.Schema = "test";
            userTable.Comment = "用户信息";

            userTable.Columns.AddRange(this.CreateUserColumns());

            tables.Add(userTable);

            DbTableInfo cityTable = new DbTableInfo();
            cityTable.Name = "City";
            cityTable.Schema = "test";
            cityTable.Comment = "城市信息";

            cityTable.Columns.AddRange(this.CreateCityColumns());

            tables.Add(cityTable);

            return tables;
        }

        List<DbColumnInfo> CreateUserColumns()
        {
            List<DbColumnInfo> columns = new List<DbColumnInfo>();

            columns.Add(new DbColumnInfo()
            {
                Name = "Id",
                DataTypeName = "int",
                IsPrimaryKey = true,
                IsAutoIncrement = true,
                Comment = "主键"
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "Name",
                DataTypeName = "string",
                Length = 50,
                Comment = "姓名",
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "Age",
                DataTypeName = "int?",
                Comment = "年龄",
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "CityId",
                DataTypeName = "int?",
                Comment = "所在城市",
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "CreateUserId",
                DataTypeName = "int",
                Comment = "创建人id",
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "CreateUserName",
                DataTypeName = "string",
                Length = 50,
                Comment = "创建人姓名",
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "CreateTime",
                DataTypeName = "DateTime",
                Comment = "创建时间",
            });

            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].Ordinal = i + 1;
            }

            return columns;
        }
        List<DbColumnInfo> CreateCityColumns()
        {
            List<DbColumnInfo> columns = new List<DbColumnInfo>();

            columns.Add(new DbColumnInfo()
            {
                Name = "Id",
                DataTypeName = "int",
                IsPrimaryKey = true,
                IsAutoIncrement = true,
                Comment = "主键"
            });

            columns.Add(new DbColumnInfo()
            {
                Name = "Name",
                DataTypeName = "string",
                Length = 50,
                Comment = "城市名",
            });

            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].Ordinal = i + 1;
            }

            return columns;
        }
    }
}
