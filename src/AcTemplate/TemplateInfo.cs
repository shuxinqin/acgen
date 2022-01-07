using System.Reflection;

namespace AcTemplate
{
    public class TemplateInfo
    {
        static readonly object[] EmptyArray = new object[0];

        public Assembly Assembly { get; set; }
        public string ClassName { get; set; }

        public Type Type { get; set; }

        public TemplateBase CreateInstance()
        {
            return (TemplateBase)this.Type.GetConstructors().First().Invoke(EmptyArray);
        }
    }
}
