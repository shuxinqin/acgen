
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Mapping/MySql/" + entityName + "Map.cs";

var table = (AcGen.DbTableInfo)model.Table;
var primaryKey = table.Columns.Where(a=> a.IsPrimaryKey).FirstOrDefault();
var autoIncrement = table.Columns.Where(a=> a.IsAutoIncrement).FirstOrDefault();

<%

namespace AceFx.Entity.Mapping.MySql
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
