using Chloe.Annotations;

namespace AcGen.MySql
{
    [Table("TABLES", "information_schema")]
    public class Table
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_TYPE { get; set; }
        public string ENGINE { get; set; }
        public int? VERSION { get; set; }
        public string ROW_FORMAT { get; set; }
        public long? TABLE_ROWS { get; set; }
        public long? AVG_ROW_LENGTH { get; set; }
        public long? DATA_LENGTH { get; set; }
        public long? MAX_DATA_LENGTH { get; set; }
        public long? INDEX_LENGTH { get; set; }
        public long? DATA_FREE { get; set; }
        public long? AUTO_INCREMENT { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public DateTime? UPDATE_TIME { get; set; }
        public DateTime? CHECK_TIME { get; set; }
        public string TABLE_COLLATION { get; set; }
        public long? CHECKSUM { get; set; }
        public string CREATE_OPTIONS { get; set; }
        public string TABLE_COMMENT { get; set; }
    }
}
