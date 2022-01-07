
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Repositories/Impls/" + entityName + "Repository.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

<%

namespace AceFx.Repositories
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

            q = q.Where(a => a.Id == id);
            return await q.FirstOrDefaultAsync();
        }

        public async Task<List<<$ entityName $>>> GetListAsync(<$ entityName $>Search condition)
        {
            var q = this.Query();

            q = q.OrderByDesc(a => a.Id);
            return await q.ToListAsync();
        }

        public async Task<PageData<<$ entityName $>>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition)
        {
            var q = this.Query();

            q = q.OrderByDesc(a => a.Id);
            return await q.TakePageDataAsync(pagination);
        }
    }
}

%>