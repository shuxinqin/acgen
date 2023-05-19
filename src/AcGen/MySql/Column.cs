using Chloe.Annotations;

namespace AcGen.MySql
{
    [Table("columns", "information_schema")]
    public class Column
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public int ORDINAL_POSITION { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        public string IS_NULLABLE { get; set; }
        public string DATA_TYPE { get; set; }
        public long? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public long? CHARACTER_OCTET_LENGTH { get; set; }
        public long? NUMERIC_PRECISION { get; set; }
        public long? NUMERIC_SCALE { get; set; }
        public long? DATETIME_PRECISION { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string COLLATION_NAME { get; set; }
        public string COLUMN_TYPE { get; set; }
        public string COLUMN_KEY { get; set; }
        public string EXTRA { get; set; }
        public string PRIVILEGES { get; set; }
        public string COLUMN_COMMENT { get; set; }
        public string GENERATION_EXPRESSION { get; set; }
        public int? SRS_ID { get; set; }

        public bool IsPrimaryKey()
        {
            return this.COLUMN_KEY != null && this.COLUMN_KEY.Split(',').Contains("PRI");
        }

        public bool IsAutoIncrement()
        {
            return this.EXTRA != null && this.EXTRA.Split(',').Contains("auto_increment");
        }

        public bool IsNullable()
        {
            return this.IS_NULLABLE == "YES";
        }
    }
}
