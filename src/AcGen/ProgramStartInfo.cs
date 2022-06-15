namespace System
{
    public static class ProgramStartInfo
    {
        static StartArgs _args = new StartArgs();
        public static StartArgs Args { get { return _args; } }
        public static void LoadArgs(string[] args)
        {
            _args.Set(ParseArgs(args));
        }
        public static Dictionary<string, string> ParseArgs(string[] args)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                int pamareterIndex = 0;//表示 -xx 的位置
                while (true)
                {
                    if (args.Length < pamareterIndex + 1)
                        break;

                    string p = args[pamareterIndex];
                    if (p.IndexOf('-') != 0 || !(p.Length > 1))
                    {
                        throw new ArgumentException("输入参数格式不正确");
                    }

                    if (args.Length >= pamareterIndex + 2)//说明 -xx 后还有元素
                    {
                        //-xx 后的元素有两种可能，一种是没 "-" 前缀的字符串，则表示 -xx 的值；另一种是带 "-" 的字符串，则表示 -xx 的值为空
                        string nextString = args[pamareterIndex + 1];
                        if (nextString[0] == '-')//当 -xx 后紧接的是带 "-" 的字符串
                        {
                            dic[p.Substring(1)] = string.Empty;
                            pamareterIndex = pamareterIndex + 1;
                        }
                        else//当是没 "-" 前缀的字符串，则表示 -xx 的值
                        {
                            dic[p.Substring(1)] = nextString;
                            pamareterIndex = pamareterIndex + 2;//设置下一个参数的位置
                        }
                    }
                    else
                    {
                        //-xx 后没有元素，则表示 -xx 参数值为空
                        dic[p.Substring(1)] = string.Empty;
                        break;
                    }
                }

                return dic;
            }
            catch (Exception ex)
            {
                throw new Exception("解析参数异常", ex);
            }
        }
    }
    public class StartArgs
    {
        Dictionary<string, string> _args = new Dictionary<string, string>();

        public StartArgs()
        {
        }

        public int Count { get { return this._args.Count; } }

        public string this[string key]
        {
            get
            {
                return this.GetValue(key);
            }
        }

        public bool HasArg(string name)
        {
            return this._args.ContainsKey(name);
        }
        public string GetValue(string name, string def = null)
        {
            this._args.TryGetValue(name, out string value);
            return value ?? def;
        }
        public T GetValue<T>(string name, T def = default)
        {
            if (this._args.TryGetValue(name, out string value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return def;
        }

        public void EnsureHasArg(string name, string error = "")
        {
            string val = this.GetValue<string>(name);
            if (string.IsNullOrEmpty(val))
            {
                if (string.IsNullOrEmpty(error))
                    error = $"-{name} 参数不能为空";

                Console.WriteLine(error);
                Environment.Exit(1);
            }
        }
        public void EnsureHasArgs(params string[] names)
        {
            foreach (var name in names)
            {
                this.EnsureHasArg(name);
            }
        }

        public void Set(Dictionary<string, string> args)
        {
            this._args.Clear();
            foreach (var arg in args)
            {
                this._args.Add(arg.Key, arg.Value);
            }
        }
        public void MergeFrom(Dictionary<string, string> args)
        {
            foreach (var arg in args)
            {
                this._args[arg.Key] = arg.Value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Input {0} args: ", this._args.Count));
            sb.Append("{");
            bool first = true;
            foreach (var kv in this._args)
            {
                if (!first)
                    sb.Append(",");
                else
                    first = false;

                sb.Append(kv.Key);
                sb.Append(":");
                sb.Append(kv.Value);
            }
            sb.Append("}");
            string message = sb.ToString();

            return message;
        }
    }
}
