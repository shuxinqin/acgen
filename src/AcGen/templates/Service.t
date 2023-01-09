
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Services/I" + entityName + "Service.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

var isSoftDelete = table.Columns.Any(a => a.Name =="IsDeleted");
var hasDeleteUserId = table.Columns.Any(a => a.Name =="DeleteUserId");

<%

namespace <$ projectName $>.Services
{
    public interface I<$ entityName $>Service : IService
    {
        Task<List<<$ entityName $>Model>> GetListAsync(<$ entityName $>Search condition);
        Task<PageData<<$ entityName $>Model>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition);
        Task<<$ entityName $>Model> GetDetailAsync(<$ keyType $> id);
        Task<<$ entityName $>Model> GetAsync(<$ keyType $> id);
        Task<<$ entityName $>Model> AddAsync(Add<$ entityName $>Input input);
        Task<<$ entityName $>Model> UpdateAsync(Update<$ entityName $>Input input);
    <#
        if(isSoftDelete && hasDeleteUserId)
        {
        <%
        Task DeleteAsync(<$ keyType $> id, string deleteUserId);
        %>
        }
        else
        {
        <%
        Task DeleteAsync(<$ keyType $> id);
        %>
        }
    #>
    }
}

%>