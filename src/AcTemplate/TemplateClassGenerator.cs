using System.Reflection;

namespace AcTemplate
{
    public class TemplateClassGenerator
    {
        static readonly string Indent = "    ";
        static object _lockRoot = new object();
        static bool TempFileHasCleaned = false;
        static int TemplateId = 0;
        static System.Collections.Concurrent.ConcurrentDictionary<string, TemplateInfo> Templates = new System.Collections.Concurrent.ConcurrentDictionary<string, TemplateInfo>();

        public static TemplateInfo GetTemplateInfo(string templatePath)
        {
            var ret = Templates.GetOrAdd(templatePath, key =>
             {
                 return GenTemplateInfo(key);
             });

            return ret;
        }
        public static TemplateInfo GenTemplateInfo(string templatePath)
        {
            string templateName = Path.GetFileNameWithoutExtension(templatePath);
            templateName = "T" + templateName.Replace(".", "_").Replace("-", "_");

            string random = Guid.NewGuid().ToString("N").Substring(0, 5);

            string className = $"{templateName}_{random}_{System.Threading.Interlocked.Increment(ref TemplateId)}";

            string code = GenTemplateClassFile(templatePath, className, "AcTemplate");

            SaveTemplate(className, code);

            Assembly assembly;
            try
            {
                assembly = CSharpCompilationHelper.Compile(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"编译模版 {templatePath} 出错：{ex.Message}");
                throw;
            }
            var type = assembly.GetTypes().Where(a => a.Name == className).FirstOrDefault();
            return new TemplateInfo() { Assembly = assembly, ClassName = className, Type = type };
        }
        static void SaveTemplate(string className, string code)
        {
            if (!TemplateEngine.SaveTemplateCode)
                return;

            try
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                string saveDir = entryAssembly.Location;
                if (string.IsNullOrEmpty(saveDir))
                {
                    saveDir = Directory.GetCurrentDirectory();
                }
                saveDir = Path.Combine(PathHelpers.GetFileDir(saveDir), "temp");

                lock (_lockRoot)
                {
                    if (!TempFileHasCleaned)
                    {
                        try
                        {
                            PathHelpers.CleanDirectory(saveDir);
                        }
                        catch (Exception ex)
                        {

                        }

                        TempFileHasCleaned = true;
                    }
                }

                PathHelpers.CreateIfDirectoryNotExists(saveDir);
                string fileFullName = Path.Combine(saveDir, $"{className}{TemplateEngine.SaveTemplateCodeFileExtension}");
                File.WriteAllText(fileFullName, code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.StackTrace}");
            }
        }

        public static string GenTemplateClassFile(string templatePath, string className, string nameSpace)
        {
            string indent = "";
            CodeWriter writer = new CodeWriter(indent);
            writer.WriteIndentLine("using System;");
            writer.WriteIndentLine("using System.IO;");
            writer.WriteIndentLine("using System.Text;");
            writer.WriteIndentLine("using System.Linq;");
            writer.WriteIndentLine("using System.Collections.Generic;");
            writer.WriteIndentLine("using AcTemplate;");

            writer.WriteIndentLine();
            writer.WriteIndentLine($"namespace {nameSpace}");
            writer.WriteIndentLine("{");

            string classCode = GenTemplateClass(templatePath, className, indent + Indent);
            writer.WriteLine(classCode);

            writer.WriteIndentLine("}");

            return writer.ToString();
        }
        public static string GenTemplateClass(string templatePath, string className, string indent)
        {
            CodeWriter writer = new CodeWriter(indent);
            writer.WriteIndentLine($"public class {className} : {TemplateEngine.BaseTemplateClass}");
            writer.WriteIndentLine("{");

            string code = GenTemplateClassBody(templatePath, className, writer.Indent + Indent);
            writer.WriteLine(code);

            writer.WriteIndent("}");
            return writer.ToString();
        }
        public static string GenTemplateClassBody(string templatePath, string className, string indent)
        {
            CodeWriter writer = new CodeWriter(indent);

            //构造函数
            writer.WriteIndentLine($"public {className}() : base(@\"{templatePath}\")");
            writer.WriteIndentLine("{");
            writer.WriteIndentLine("}");
            writer.WriteIndentLine();

            string methodCode = GenTemplateClassMethod(templatePath, indent);
            writer.Write(methodCode);

            return writer.ToString();
        }

        public static string GenTemplateClassMethod(string templatePath, string indent)
        {
            CodeWriter writer = new CodeWriter();

            string buildMethodCode = GenTemplateClassBuildMethod(templatePath, indent);
            writer.Write(buildMethodCode);

            return writer.ToString();
        }
        public static string GenTemplateClassBuildMethod(string templatePath, string indent)
        {
            CodeWriter writer = new CodeWriter(indent);
            writer.WriteIndentLine("protected override BuildResult Build(dynamic model, string indent, CodeWriter writer)");
            writer.WriteIndentLine("{");

            string bodyCode = GenTemplateClassBuildMethodBody(templatePath, indent + Indent);
            writer.Write(bodyCode);

            writer.WriteLine();
            writer.WriteIndent("}");
            return writer.ToString();
        }
        public static string GenTemplateClassBuildMethodBody(string templatePath, string indent)
        {
            CodeWriter writer = new CodeWriter(indent);

            writer.WriteIndentLine("string outputFileName = string.Empty;");

            TemplateParser.Instance.Parse(writer, templatePath);

            writer.WriteLine();
            writer.WriteIndent("return new BuildResult() { Content = writer.ToString(), OutputFileName = outputFileName };");

            return writer.ToString();
        }
    }
}
