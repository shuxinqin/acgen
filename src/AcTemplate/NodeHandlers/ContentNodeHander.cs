namespace AcTemplate.NodeHandlers
{
    public class ContentNodeHander : NodeHander
    {
        StringBuilder _sb = new StringBuilder();
        public ContentNodeHander(HandleContext handleContext) : base(handleContext)
        {

        }

        public override string StartLabel { get; set; } = "<%";

        protected override void OnEnd()
        {
            this.FlushContent();
        }

        public override void OnNewLine(FileLine fileLine)
        {
            string line = fileLine.Text;
            this.FlushContent();
            var writer = this.Writer;
            if (!Helpers.IsLabel(line))
            {
                writer.WriteLine("writer.WriteLine();");
                writer.WriteIndent();
                writer.WriteLine("writer.WriteIndent();");
                writer.WriteIndent();
            }
        }

        protected override void HandleChar(FileLine fileLine, char c, ref int position)
        {
            string line = fileLine.Text;
            if (StartsWith(line, position, "%>"))
            {
                this.End();
                position = position + 2;
                return;
            }

            if (c == '"' || c == '\\')
            {
                this._sb.Append('\\').Append(c);
            }
            else
            {
                this._sb.Append(c);
            }

            position++;
        }

        public override void Suspend()
        {
            this.FlushContent();
        }

        void FlushContent()
        {
            if (this._sb.Length > 0)
            {
                this.Writer.Write($"writer.Write(\"{this._sb.ToString()}\");");
                this._sb.Clear();
            }
        }
    }
}
