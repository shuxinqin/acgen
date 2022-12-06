namespace AcGen
{
    public class DbColumnInfo
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public string DataTypeName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int? Length { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }

        public string Comment { get; set; }

        public string TypeScriptDataTypeName { get { return this.GetTypeScriptDataTypeName(); } }

        public string GetTypeScriptDataTypeName()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            map["string"] = "string";
            map["int"] = "number";
            map["int?"] = "number | null";
            map["long"] = "number";
            map["long?"] = "number | null";
            map["float"] = "number";
            map["float?"] = "number | null";
            map["double"] = "number";
            map["double?"] = "number | null";
            map["decimal"] = "number";
            map["decimal?"] = "number | null";
            map["byte"] = "number";
            map["byte?"] = "number | null";
            map["short"] = "number";
            map["short?"] = "number | null";
            map["DateTime"] = "string";
            map["DateTime?"] = "string | null";

            string typeScriptTypeName;
            if (map.TryGetValue(this.DataTypeName, out typeScriptTypeName))
            {
                return typeScriptTypeName;
            }

            return "any";
        }
    }
}
