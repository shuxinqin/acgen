namespace AcTemplate
{
    public class CodeWriter
    {
        StringBuilder _builder;

        public string Indent { get; set; } = "";

        public CodeWriter() : this(new StringBuilder(), "")
        {

        }
        public CodeWriter(string indent) : this(new StringBuilder(), indent)
        {

        }
        public CodeWriter(StringBuilder builder, string indent)
        {
            this._builder = builder;
            this.Indent = indent;
        }

        public CodeWriter Write(char c)
        {
            this._builder.Append(c);
            return this;
        }
        public CodeWriter Write(object obj = null)
        {
            this._builder.Append(obj);
            return this;
        }
        public CodeWriter WriteLine(object obj = null)
        {
            this.Write(obj);
            this._builder.AppendLine();
            return this;
        }

        public CodeWriter WriteIndent(object obj = null)
        {
            this._builder.Append(this.Indent).Append(obj);
            return this;
        }
        public CodeWriter WriteIndentLine(object obj = null)
        {
            this.WriteIndent(obj);
            this._builder.AppendLine();
            return this;
        }

        public override string ToString()
        {
            return this._builder.ToString();
        }
    }
}
