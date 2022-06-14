using AcTemplate;

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

        public string GetOutDir()
        {
            if (!string.IsNullOrEmpty(this.OutDir))
                return Path.GetFullPath(this.OutDir).AsStdPath();

            return Path.GetFullPath("./out").AsStdPath();
        }

        public string GetRootTemplate()
        {
            if (!string.IsNullOrEmpty(this.TemplateFile))
                return Path.GetFullPath(this.TemplateFile).AsStdPath();

            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates/root.t")).AsStdPath();
        }
    }
}
