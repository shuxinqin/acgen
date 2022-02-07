using AcTemplate.NodeHandlers;

namespace AcTemplate
{
    public class TemplateParser
    {
        public static readonly TemplateParser Instance = new TemplateParser { };
        public TemplateParser()
        {

        }

        public void Parse(CodeWriter writer, string templatePath)
        {
            HandleContext handleContext = new HandleContext() { Writer = writer };
            handleContext.AddHandler(new RootNodeHandler(handleContext));

            IEnumerable<FileLine> fileLines = FileHelpers.MakeFileLines(templatePath);

            int lineNumber = 0;
            foreach (var fileLine in fileLines)
            {
                lineNumber++;
                string trimedLine = fileLine.Text.Trim();
                if (Helpers.IsLabel(trimedLine))
                {
                    fileLine.Text = trimedLine;
                }

                handleContext.LastHandler.OnNewLine(fileLine);

                int position = 0;
                while (true)
                {
                    if (position >= fileLine.Text.Length)
                        break;

                    handleContext.LastHandler.Handle(fileLine, ref position);
                }

                handleContext.LastHandler.OnLineEnd(fileLine);
            }

            handleContext.LastHandler.Finish();
        }
    }
}
