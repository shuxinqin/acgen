
namespace AcGen
{
    public class TemplateModel
    {
        public string OutDir { get; set; }

        public List<DbTableInfo> Tables { get; set; } = new List<DbTableInfo>();
    }
}
