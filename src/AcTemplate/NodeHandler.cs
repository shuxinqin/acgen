using AcTemplate.NodeHandlers;

namespace AcTemplate
{
    public class NodeHandler
    {
        public NodeHandler(HandleContext handleContext)
        {
            this.HandleContext = handleContext;
        }

        public HandleContext HandleContext { get; set; }
        public CodeWriter Writer { get { return this.HandleContext.Writer; } }

        public FileLine StartLine { get; set; }
        public int StartPosition { get; set; }

        public virtual string StartLabel { get; set; } = string.Empty;

        public void Start(FileLine fileLine, ref int position)
        {
            this.StartLine = fileLine;
            this.StartPosition = position;
            this.OnStart(fileLine, ref position);
        }
        protected virtual void OnStart(FileLine fileLine, ref int position)
        {
            position = position + this.StartLabel.Length;
        }


        public virtual void Suspend()
        {

        }

        public void End()
        {
            this.HandleContext.RemoveLastHandler();
            this.OnEnd();
        }
        protected virtual void OnEnd()
        {

        }

        public virtual void Finish()
        {
            throw new Exception($"未找到 '{this.StartLabel}' 对应的结束标签。在文件 {this.StartLine.FilePath} 第 {this.StartLine.LineNumber} 行，位置 {this.StartPosition + 1} 附近\nline text: {this.StartLine.Text}");
        }

        public virtual void OnNewLine(FileLine fileLine)
        {
            this.Writer.WriteIndent();
        }
        public virtual void OnLineEnd(FileLine fileLine)
        {

        }

        public virtual void Handle(FileLine fileLine, ref int position)
        {
            string line = fileLine.Text;
            if (StartsWith(line, position, "<%"))
            {
                this.HandleContext.SuspendLastHandler();
                ContentNodeHandler contentNodeHandler = new ContentNodeHandler(this.HandleContext);
                this.HandleContext.AddHandler(contentNodeHandler);
                contentNodeHandler.Start(fileLine, ref position);
                return;
            }

            if (StartsWith(line, position, "<#"))
            {
                this.HandleContext.SuspendLastHandler();
                CsharpCodeNodeHandler csharpCodeNodeHandler = new CsharpCodeNodeHandler(this.HandleContext);
                this.HandleContext.AddHandler(csharpCodeNodeHandler);
                csharpCodeNodeHandler.Start(fileLine, ref position);
                return;
            }

            if (StartsWith(line, position, "<$"))
            {
                this.HandleContext.SuspendLastHandler();
                CsharpPlaceholderNodeHandler csharpPlaceholderNodeHandler = new CsharpPlaceholderNodeHandler(this.HandleContext);
                this.HandleContext.AddHandler(csharpPlaceholderNodeHandler);
                csharpPlaceholderNodeHandler.Start(fileLine, ref position);
                return;
            }

            this.HandleChar(fileLine, fileLine.Text[position], ref position);
        }

        protected virtual void HandleChar(FileLine fileLine, char c, ref int position)
        {

        }

        public static bool StartsWith(string str, int start, string value)
        {
            return str.IndexOf(value, start) == start;
        }
    }
}
