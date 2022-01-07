using AcTemplate;

namespace AcGen
{
    public abstract class TemplateFileBase : TemplateBase
    {
        public TemplateFileBase(string templatePath) : base(templatePath)
        {
        }

        public string MakeFirstCharUpper(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return $"{char.ToUpper(str[0])}{str.Substring(1)}";
        }

        /// <summary>
        /// my_name -> MyName
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string UnderScoreCaseToPascal(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;


            List<char> chars = new List<char>();

            int i = 0;
            while (true)
            {
                if (i > str.Length - 1)
                    break;

                char c = str[i];

                if (i == 0)
                {
                    if (c == '_')
                    {
                        chars.Add(c);
                        i++;
                        continue;
                    }

                    chars.Add(char.ToUpper(c));
                    i++;
                    continue;
                }

                if (c == '_')
                {
                    i++;
                    continue;
                }

                if (str[i - 1] == '_')
                {
                    chars.Add(char.ToUpper(c));
                    i++;
                    continue;
                }

                chars.Add(c);
                i++;
            }

            return new string(chars.ToArray());
        }
    }
}
