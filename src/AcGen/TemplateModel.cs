
namespace AcGen
{
    public class TemplateModel
    {
        public string OutDir { get; set; }

        public List<DbTableInfo> Tables { get; set; } = new List<DbTableInfo>();

        /// <summary>
        /// 程序启动参数
        /// </summary>
        public StartArgs Args { get { return ProgramStartInfo.Args; } }
    }
}
