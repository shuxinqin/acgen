namespace AcGen
{
    internal class Program
    {
        public static string VERSION = "1.0.0";

        static void Main(string[] args)
        {
            /*
             * usage:
             * dotnet acgen.dll -t ./templates/root.t -o ./out -clean -db mysql -conn "Server=localhost;Port=3306;Database=Chloe;Uid=root;Password=sasa;"
             */
            Run(args);
        }

        static void Run(string[] args)
        {

#if DEBUG

            List<string> argList = new List<string>();
            string argsString = "-t ./templates/root.t -o ../../out -clean -db mysql -conn";
            string connString = "Server=localhost;Port=3306;Database=Chloe;Uid=root;Password=sasa;";

            argList = argsString.Split(" ").ToList();
            argList.Add(connString);

            args = argList.ToArray();

            //使用 mock db 直接查看生成效果
            argsString = "-t ./templates/root.t -o ../../out -clean -db test -conn";
            args = argsString.Split(" ");
#endif

            ProgramStartInfo.LoadArgs(args);

            if (ProgramStartInfo.Args.Count == 0 || ProgramStartInfo.Args.HasArg("h"))
            {
                ShowHelp();
                ShowVersion();
                Environment.Exit(0);
                return;
            }

            if (ProgramStartInfo.Args.HasArg("v"))
            {
                ShowVersion();
                Environment.Exit(0);
                return;
            }

            ProgramStartInfo.Args.EnsureHasArgs("o", "t", "db");

            StartArgs startArgs = ProgramStartInfo.Args;

            bool cleanOutDir = startArgs.ContainsKey("clean");

            GenArg arg = new GenArg();
            arg.TemplateFile = startArgs["t"];
            arg.OutDir = startArgs["o"];
            arg.CleanOutDir = cleanOutDir;
            arg.DbType = startArgs["db"];
            arg.ConnectionString = startArgs["conn"];

            if (startArgs.ContainsKey("tables"))
            {
                arg.TablesOnly.AddRange(startArgs["tables"].Split(",", StringSplitOptions.RemoveEmptyEntries));
            }

            CodeGenerator.Run(arg);

            Environment.Exit(0);
        }
        static void ShowVersion()
        {
            Console.WriteLine($"acgen {VERSION} <最终解释权归 acgen 所有>");
        }
        static void ShowHelp()
        {
            string connString = "\"Your connection string\"";
            string tablesString = "\"User, City\"";
            string help = $@"启动命令参数:
    -t(template):      表示模版文件
    -o(out):           表示输出文件保存目录
    -clean:            表示生成之前清空 -o(out) 目录
    -db:               表示数据库类型。目前支持 mysql
    -conn:             表示用双引号({"\"\""})包裹的连接字符串
    -tables            指定要生成代码的表(多个用 {"\",\""} 分隔)。不传或传空则查询所有表
    -v:                查看版本号
    -h:                查看帮助

例: dotnet acgen.dll -t ./templates/root.t -o ./out -clean -db mysql -conn {connString} -tables {tablesString}";
            help = help.Trim();
            Console.WriteLine(help);
        }
    }
}
