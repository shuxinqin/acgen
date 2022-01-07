namespace AcGen
{
    public class DbColumnInfo
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public string DataTypeName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int? Length { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }

        public string Comment { get; set; }
    }
}
