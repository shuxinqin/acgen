namespace AcGen
{
    public class DbTableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 经过处理后的名称，如移除前缀 
        /// </summary>
        public string TrimedName { get; set; }
        public string Schema { get; set; }
        public string Comment { get; set; }

        public List<DbColumnInfo> Columns { get; set; } = new List<DbColumnInfo>();
    }
}
