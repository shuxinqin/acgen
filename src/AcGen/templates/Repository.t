
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);

string moduleDir = $"{projectName}";
string nc = $"{projectName}";

if(!string.IsNullOrEmpty(moduleName))
{
    moduleDir = $"{projectName}.{moduleName}/{projectName}.{moduleName}";
    nc = $"{projectName}.{moduleName}";
}

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Repositories/I" + entityName + "Repository.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

<%

namespace <$ nc $>.Repositories
{
    public interface I<$ entityName $>Repository : IRepository<<$ entityName $>>
    {
        Task<<$ entityName $>> GetDetailAsync(<$ keyType $> id);
        Task<List<<$ entityName $>>> GetListAsync(<$ entityName $>Search condition);
        Task<PageData<<$ entityName $>>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition);
    }
}

%>