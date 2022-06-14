using AcGen.MySql;
using AcTemplate;

namespace AcGen
{
    internal class CodeGenerator
    {
        public static void Run(GenArg arg)
        {
            TemplateEngine.BaseTemplateClass = typeof(TemplateFileBase).FullName;

            IDbService dbService = null;
            if (arg.DbType.ToLower() == "test")
            {
                dbService = new MockDbService();
            }
            else if (arg.DbType.ToLower() == "mysql")
            {
                dbService = new MySqlDbService(arg.ConnectionString);
            }
            else
            {
                throw new NotSupportedException($"未支持 {arg.DbType} 数据库");
            }

            if (arg.DbType.ToLower() != "test" && string.IsNullOrEmpty(arg.ConnectionString))
            {
                Console.WriteLine($"数据库连接字符串不能为空");
                Environment.Exit(0);
            }

            string outDir = arg.GetOutDir();
            string templateFile = arg.GetRootTemplate();

            List<DbTableInfo> tables = dbService.GetTables(arg.TablesOnly);

            Console.WriteLine($"查询到 {tables.Count} 个表...");

            TemplateModel templateModel = new TemplateModel() { OutDir = outDir, Tables = tables };

            TemplateBase template = TemplateEngine.GetTemplate(templateFile);

            if (arg.CleanOutDir)
            {
                PathHelpers.CleanDirectory(outDir);
            }

            BuildResult result = template.Build(templateModel);
        }
    }
}
