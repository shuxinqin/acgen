
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Services/I" + entityName + "Service.cs";

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

namespace <$ nc $>.Services
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
    <#
        if(isSoftDelete && hasDeleteUserId)
        {
        <%
        Task DeleteBatchAsync(List<<$ keyType $>> ids, string deleteUserId);
        %>
        }
        else
        {
        <%
        Task DeleteBatchAsync(List<<$ keyType $>> ids);
        %>
        }
    #>
    }
}

%>