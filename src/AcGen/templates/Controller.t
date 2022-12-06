
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

string areaName = moduleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Web/Areas/" + areaName + "/Controllers/" + entityName + "Controller.cs";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "object";
if(idColumn != null)
{
    keyType = idColumn.DataTypeName;
}

var isSoftDelete = table.Columns.Any(a => a.Name =="IsDeleted");

<%

namespace <$ projectName $>.Areas.<$ areaName $>.Controllers
{
    [Area("<$ areaName $>")]
    [Permission("<$ areaName.ToLower() $>.<$ model.Table.Name.ToLower() $>")]
    public class <$ entityName $>Controller : WebController<I<$ entityName $>Service>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetList(<$ entityName $>Search condition)
        {
            List<<$ entityName $>Model> models = await this.Service.GetListAsync(condition);
            return this.SuccessData(models);
        }

        [HttpGet]
        public async Task<ActionResult> GetPageList(Pagination pagination, <$ entityName $>Search condition)
        {
            PageData<<$ entityName $>Model> pageData = await this.Service.GetPageListAsync(pagination, condition);
            return this.SuccessData(pageData);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetail(<$ keyType $> id)
        {
            <$ entityName $>Model model = await this.Service.GetDetailAsync(id);
            return this.SuccessData(model);
        }

        [HttpGet]
        public async Task<ActionResult> Get(<$ keyType $> id)
        {
            <$ entityName $>Model model = await this.Service.GetAsync(id);
            return this.SuccessData(model);
        }

        [Permission("<$ areaName.ToLower() $>.<$ model.Table.Name.ToLower() $>.add")]
        [HttpPost]
        public async Task<ActionResult> Add(Add<$ entityName $>Input input)
        {
            <$ entityName $>Model model = await this.Service.AddAsync(input);
            return this.AddSuccessData(model);
        }

        [Permission("<$ areaName.ToLower() $>.<$ model.Table.Name.ToLower() $>.update")]
        [HttpPost]
        public async Task<ActionResult> Update(Update<$ entityName $>Input input)
        {
            <$ entityName $>Model model = await this.Service.UpdateAsync(input);
            return this.UpdateSuccessMsg();
        }

        [Permission("<$ areaName.ToLower() $>.<$ model.Table.Name.ToLower() $>.delete")]
        [HttpPost]
        public async Task<ActionResult> Delete(<$ keyType $> id)
        {
        <#
            if(isSoftDelete)
            {
            <%
            await this.Service.DeleteAsync(id, this.CurrentSession.UserId);
            %>
            }
            else
            {
            <%
            await this.Service.DeleteAsync(id);
            %>
            }
        #>
            return this.DeleteSuccessMsg();
        }
    }
}

%>