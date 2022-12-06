
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "WebApi/" + moduleName + "/Controllers/" + entityName + "Controller.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

var isSoftDelete = table.Columns.Any(a => a.Name =="IsDeleted");

<%

namespace <$ projectName $>.<$ moduleName $>.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Login]
    public class <$ entityName $>Controller : WebApiController<I<$ entityName $>Service>
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<<$ entityName $>Model>>), 200)]
        public async Task<ApiResult> GetList([FromQuery] <$ entityName $>Search condition)
        {
            List<<$ entityName $>Model> models = await this.Service.GetListAsync(condition);
            return this.SuccessData(models);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PageData<<$ entityName $>Model>>), 200)]
        public async Task<ApiResult> GetPageList([FromQuery] Pagination pagination, [FromQuery] <$ entityName $>Search condition)
        {
            PageData<<$ entityName $>Model> pageData = await this.Service.GetPageListAsync(pagination, condition);
            return this.SuccessData(pageData);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<<$ entityName $>Model>), 200)]
        public async Task<ApiResult> GetDetail(<$ keyType $> id)
        {
            <$ entityName $>Model model = await this.Service.GetDetailAsync(id);
            return this.SuccessData(model);
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<<$ entityName $>Model>), 200)]
        public async Task<ApiResult> Get(<$ keyType $> id)
        {
            <$ entityName $>Model model = await this.Service.GetAsync(id);
            return this.SuccessData(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [Permission("<$ moduleName.ToLower() $>.<$ model.Table.TrimedName.ToLower() $>.add")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<<$ entityName $>Model>), 200)]
        public async Task<ApiResult> Add([FromBody] Add<$ entityName $>Input input)
        {
            <$ entityName $>Model model = await this.Service.AddAsync(input);
            return this.AddSuccessData(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [Permission("<$ moduleName.ToLower() $>.<$ model.Table.TrimedName.ToLower() $>.update")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> Update([FromBody] Update<$ entityName $>Input input)
        {
            <$ entityName $>Model model = await this.Service.UpdateAsync(input);
            return this.UpdateSuccessMsg();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [Permission("<$ moduleName.ToLower() $>.<$ model.Table.TrimedName.ToLower() $>.delete")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> Delete([FromBody] IdInput<<$ keyType $>> input)
        {
        <#
            if(isSoftDelete)
            {
            <%
            await this.Service.DeleteAsync(input.Id, this.CurrentSession.UserId);
            %>
            }
            else
            {
            <%
            await this.Service.DeleteAsync(input.Id);
            %>
            }
        #>
            return this.DeleteSuccessMsg();
        }
    }
}

%>