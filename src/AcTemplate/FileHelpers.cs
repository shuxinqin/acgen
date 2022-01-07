namespace AcTemplate
{
    internal class FileHelpers
    {
        public static IEnumerable<FileLine> MakeFileLines(string file)
        {
            return MakeFileLines(File.OpenText(file));
        }
        public static IEnumerable<FileLine> MakeFileLines(TextReader reader)
        {
            using (reader)
            {
                int lineNumber = 0;
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    lineNumber++;
                    yield return new FileLine() { Text = line, LineNumber = lineNumber };
                }
            }
        }
    }
}
