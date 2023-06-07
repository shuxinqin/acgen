
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Entity.Mapping.MySql/" + entityName + "Map.cs";

var table = (AcGen.DbTableInfo)model.Table;
var primaryKey = table.Columns.Where(a=> a.IsPrimaryKey).FirstOrDefault();
var autoIncrement = table.Columns.Where(a=> a.IsAutoIncrement).FirstOrDefault();

<%

namespace <$ nc $>.Entity.Mapping.MySql
{
    public class <$ entityName $>Map : <$ entityName $>MapBase
    {
        public <$ entityName $>Map()
        {
            this.MapTo("<$ model.Table.Name $>");
        }
    }
}

%>
