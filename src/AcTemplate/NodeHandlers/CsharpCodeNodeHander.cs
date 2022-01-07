namespace AcTemplate.NodeHandlers
{
    public class CsharpCodeNodeHander : NodeHander
    {
        public CsharpCodeNodeHander(HandleContext handleContext) : base(handleContext)
        {

        }

        public override string StartLabel { get; set; } = "<#";

        public override void OnNewLine(FileLine fileLine)
        {
            this.Writer.WriteLine();
            base.OnNewLine(fileLine);
        }

        protected override void HandleChar(FileLine fileLine, char c, ref int position)
        {
            string line = fileLine.Text;
            if (StartsWith(line, position, "#>"))
            {
                this.End();
                position = position + 2;
                return;
            }

            this.Writer.Write(c);
            position++;
        }
    }
}
