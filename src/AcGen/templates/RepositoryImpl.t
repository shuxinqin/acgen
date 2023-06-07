
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Repositories/Impls/" + entityName + "Repository.cs";

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
    class <$ entityName $>Repository : RepositoryBase<<$ entityName $>>, I<$ entityName $>Repository
    {
        public <$ entityName $>Repository(IDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<<$ entityName $>> GetDetailAsync(<$ keyType $> id)
        {
            var q = this.Query();
            q = q.IncludeAll();
            <#
            if(idColumn != null)
            {
            
                <%
            q = q.Where(a => a.<$ idColumn.Name $> == id);
                %>

            }
            #>

            return await q.FirstOrDefaultAsync();
        }

        public async Task<List<<$ entityName $>>> GetListAsync(<$ entityName $>Search condition)
        {
            var q = this.Query();
            <#
            if(idColumn != null)
            {
            
                <%
            q = q.OrderByDesc(a => a.<$ idColumn.Name $>);
                %>

            }
            #>

            return await q.ToListAsync();
        }

        public async Task<PageData<<$ entityName $>>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition)
        {
            var q = this.Query();
            <#
            if(idColumn != null)
            {
            
                <%
            q = q.OrderByDesc(a => a.<$ idColumn.Name $>);
                %>

            }
            #>

            return await q.TakePageDataAsync(pagination);
        }
    }
}

%>