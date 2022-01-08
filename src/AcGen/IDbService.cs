namespace AcGen
{
    public interface IDbService
    {
        List<DbTableInfo> GetTables(List<string> tablesOnly);
    }
}
