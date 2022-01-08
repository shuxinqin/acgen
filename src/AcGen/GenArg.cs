namespace AcGen
{
    public class GenArg
    {
        public string OutDir { get; set; }
        public string TemplateFile { get; set; }
        public bool CleanOutDir { get; set; }
        public string DbType { get; set; }
        public string ConnectionString { get; set; }

        public List<string> TablesOnly { get; set; } = new List<string>();
    }
}
