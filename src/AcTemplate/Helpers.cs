namespace AcTemplate
{
    public class Helpers
    {
        static List<string> Labels = "<%,%>,<#,#>,<$,$>".Split(',').ToList();

        public static bool IsLabel(string str)
        {
            return Labels.Contains(str);
        }
    }
}
