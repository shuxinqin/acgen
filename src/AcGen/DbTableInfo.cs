namespace AcGen
{
    public class DbTableInfo
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string Comment { get; set; }

        public List<DbColumnInfo> Columns { get; set; } = new List<DbColumnInfo>();
    }
}
