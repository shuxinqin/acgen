
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Entity.Mapping.MySql/" + entityName + "Map.cs";

var table = (AcGen.DbTableInfo)model.Table;
var primaryKey = table.Columns.Where(a=> a.IsPrimaryKey).FirstOrDefault();
var autoIncrement = table.Columns.Where(a=> a.IsAutoIncrement).FirstOrDefault();

<%

namespace <$ projectName $>.Entity.Mapping.MySql
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
