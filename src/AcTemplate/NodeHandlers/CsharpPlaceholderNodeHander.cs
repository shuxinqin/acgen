namespace AcTemplate.NodeHandlers
{
    public class CsharpPlaceholderNodeHander : NodeHander
    {
        StringBuilder _sb = new StringBuilder();

        public CsharpPlaceholderNodeHander(HandleContext handleContext) : base(handleContext)
        {

        }

        public override string StartLabel { get; set; } = "<$";

        protected override void OnEnd()
        {
            this.Writer.Write($"writer.Write({this._sb.ToString().Trim()});");
            this._sb.Clear();
        }

        public override void OnLineEnd(FileLine fileLine)
        {
            base.Finish();
        }

        public override void Handle(FileLine fileLine, ref int position)
        {
            string line = fileLine.Text;
            if (StartsWith(line, position, "$>"))
            {
                this.End();
                position = position + 2;
                return;
            }

            char c = line[position];
            this._sb.Append(c);

            position++;
        }
    }
}
