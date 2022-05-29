using System.Dynamic;

namespace AcTemplate
{
    public abstract class TemplateBase
    {
        string _templatePath = "";

        protected TemplateBase(string templatePath)
        {
            this._templatePath = templatePath;
            this.Writer = new CodeWriter();
        }

        CodeWriter Writer { get; set; }
        public string Indent { get { return this.Writer.Indent; } set { this.Writer.Indent = value; } }

        public BuildResult Build(dynamic model)
        {
            try
            {
                string outputFileName = string.Empty;
                this.Build(model, this.Indent, this.Writer, ref outputFileName);
                BuildResult buildResult = new BuildResult() { Content = this.Writer.ToString(), OutputFileName = outputFileName };
                if (!string.IsNullOrEmpty(buildResult.OutputFileName))
                {
                    //保存输出内容
                    FileInfo fileInfo = new FileInfo(buildResult.OutputFileName);
                    if (fileInfo.Directory.Exists == false)
                    {
                        fileInfo.Directory.Create();
                    }

                    string content = buildResult.Content;
                    if (content.StartsWith(System.Environment.NewLine))
                    {
                        content = content.Substring(System.Environment.NewLine.Length);
                    }

                    File.WriteAllText(buildResult.OutputFileName.AsStdPath(), content);
                    PrintLogger.Instance.Info($"已生成 {buildResult.OutputFileName}");
                }

                return buildResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"根据模版 {this._templatePath} 生成代码失败: {ex.Message}");
                throw;
            }
        }

        public static object ConvertToExpandoObject(object model)
        {
            if (model == null)
            {
                return null;
            }

            var modelType = model.GetType();
            if (!IsAnonymousType(modelType))
            {
                return model;
            }

            //将匿名类型转成 ExpandoObject 对象，因为匿名类型的成员不能跨程序集访问
            var properties = modelType.GetProperties();
            IDictionary<string, object> exp = new ExpandoObject();

            foreach (var property in properties)
            {
                exp.Add(property.Name, property.GetValue(model));
            }

            return exp;
        }
        protected abstract void Build(dynamic model, string indent, CodeWriter writer, ref string outputFileName);

        protected void Include(string templatePath, object model, string indent)
        {
            var result = this.ChildTemplateBuild(templatePath, model, indent);
            this.Writer.Write(result.Content);
        }
        protected void Include(string templatePath, object model)
        {
            this.Include(templatePath, model, null);
        }

        /// <summary>
        /// 将 model 对象转交给 templatePath 模版处理
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="model"></param>
        protected void Emit(string templatePath, object model)
        {
            this.ChildTemplateBuild(templatePath, model, null);
        }

        BuildResult ChildTemplateBuild(string templatePath, object model, string indent)
        {
            model = ConvertToExpandoObject(model);

            string templateAbsolutePath = PathHelpers.GetAbsolutePath(this._templatePath, templatePath);
            TemplateBase template = this.GetTemplate(templateAbsolutePath);
            template.Indent = indent;
            var result = template.Build(model);
            return result;
        }

        TemplateBase GetTemplate(string templateAbsolutePath)
        {
            TemplateBase template = TemplateClassGenerator.GetTemplateInfo(templateAbsolutePath).CreateInstance();
            return template;
        }

        public static bool IsAnonymousType(Type type)
        {
            string typeName = type.Name;
            return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains("AnonymousType");
        }
    }

    class FileTemplate : TemplateBase
    {
        public FileTemplate() : base("template.t")
        {
        }

        protected override void Build(dynamic model, string indent, CodeWriter writer, ref string outputFileName)
        {
            writer.WriteLine("这是生成的内容");
        }
    }

    public class BuildResult
    {
        /// <summary>
        /// 生成内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 保存的文件名
        /// </summary>
        public string OutputFileName { get; set; }
    }
}
