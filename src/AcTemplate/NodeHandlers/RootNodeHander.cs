namespace AcTemplate.NodeHandlers
{
    public class RootNodeHander : CsharpCodeNodeHander
    {
        public RootNodeHander(HandleContext handleContext) : base(handleContext)
        {

        }

        public override void OnNewLine(FileLine fileLine)
        {
            if (fileLine.LineNumber > 1)
            {
                this.Writer.WriteLine();
            }

            base.OnNewLine(fileLine);
        }

        protected override void HandleChar(FileLine fileLine, char c, ref int position)
        {
            this.Writer.Write(c);
            position++;
        }

        public override void Finish()
        {

        }
    }
}
