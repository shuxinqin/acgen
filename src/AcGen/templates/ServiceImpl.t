
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Services/Impls/" + entityName + "Service.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

var isSoftDelete = table.Columns.Any(a => a.Name =="IsDeleted");
var hasCreateTime = table.Columns.Any(a => a.Name =="CreateTime");
var hasDeleteUserId = table.Columns.Any(a => a.Name =="DeleteUserId");

<%

namespace <$ projectName $>.Services
{
    public class <$ entityName $>Service : BizServiceBase<I<$ entityName $>Repository>, I<$ entityName $>Service
    {
        public <$ entityName $>Service(IServiceProvider services) : base(services)
        {

        }

        public async Task<List<<$ entityName $>Model>> GetListAsync(<$ entityName $>Search condition)
        {
            List<<$ entityName $>> entities = await this.Repository.GetListAsync(condition);
            return entities.ListMap<<$ entityName $>Model>();
        }

        public async Task<PageData<<$ entityName $>Model>> GetPageListAsync(Pagination pagination, <$ entityName $>Search condition)
        {
            PageData<<$ entityName $>> pageData = await this.Repository.GetPageListAsync(pagination, condition);

            List<<$ entityName $>Model> models = pageData.Models.ListMap<<$ entityName $>Model>();
            PageData<<$ entityName $>Model> pageList = new PageData<<$ entityName $>Model>(models, pageData.TotalCount, pageData.CurrentPage, pageData.PageSize);
            return pageList;
        }

        public async Task<<$ entityName $>Model> GetDetailAsync(<$ keyType $> id)
        {
            <$ entityName $> entity = await this.Repository.GetDetailAsync(id);
            return entity.MapTo<<$ entityName $>Model>();
        }

        public async Task<<$ entityName $>Model> GetAsync(<$ keyType $> id)
        {
            return await this.GetAsync<<$ entityName $>, <$ entityName $>Model>(id);
        }

        public async Task<<$ entityName $>Model> AddAsync(Add<$ entityName $>Input input)
        {
            input.Validate();

            <$ entityName $> entity = new <$ entityName $>();
            input.MapTo(entity);

            <#
            if(idColumn !=null && idColumn.DataTypeName == "string")
            {
                <%
            entity.<$ idColumn.Name $> = IdHelper.CreateStringSnowflakeId();
                %>
            }

            if(idColumn !=null && !idColumn.IsAutoIncrement && idColumn.DataTypeName == "long")
            {
                <%
            entity.<$ idColumn.Name $> = IdHelper.CreateSnowflakeId();
                %>
            }

            if(hasCreateTime)
            {
                <%
            entity.CreateTime = DateTime.Now;    
                %>
            }
            #>
            await this.Repository.InsertAsync(entity);

            <$ entityName $>Model model = entity.MapTo<<$ entityName $>Model>();
            return model;
        }

        public async Task<<$ entityName $>Model> UpdateAsync(Update<$ entityName $>Input input)
        {
            input.Validate();

            return await this.UpdateFromInputAsync<Update<$ entityName $>Input, <$ entityName $>, <$ entityName $>Model>(input);
        }

        <#
            if(isSoftDelete)
            {
                if(hasDeleteUserId)
                {
            <%
        public async Task DeleteAsync(<$ keyType $> id, string deleteUserId)
        {
            await this.SolfDeleteAsync<<$ entityName $>>(id, deleteUserId);
        }
            %>
                }
                else
                {
            <%
        public async Task DeleteAsync(<$ keyType $> id)
        {
            await this.SolfDeleteAsync<<$ entityName $>>(id);
        }
            %>
                }
            }
            else
            {
            <%
        public async Task DeleteAsync(<$ keyType $> id)
        {
            await this.Repository.DeleteByIdAsync(id);
        }
            %>
            }
        #>
    }
}

%>