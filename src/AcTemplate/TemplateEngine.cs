namespace AcTemplate
{
    public static class TemplateEngine
    {
        public static bool SaveTemplateCode { get; set; } = true;
        public static string SaveTemplateCodeFileExtension { get; set; } = ".cs";
        public static string BaseTemplateClass { get; set; } = typeof(TemplateBase).FullName;

        public static TemplateBase GetTemplate(string templatePath)
        {
            return TemplateClassGenerator.GetTemplateInfo(templatePath).CreateInstance();
        }
        public static TemplateInfo GetTemplateInfo(string templatePath)
        {
            return TemplateClassGenerator.GetTemplateInfo(templatePath);
        }
    }
}
