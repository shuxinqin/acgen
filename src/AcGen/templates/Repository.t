
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Repositories/I" + entityName + "Repository.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

<%

namespace <$ projectName $>.Repositories
{
    public interface I<$ entityName $>Repository : IRepository<<$ entityName $>>
    {
        Task<<$ entityName $>> GetDetailAsync(<$ keyType $> id);
        Task<List<<$ entityName $>>> GetListAsync(<$ entityName $>Search condition);
        Task<PageData<<$ entityName $>>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition);
    }
}

%>