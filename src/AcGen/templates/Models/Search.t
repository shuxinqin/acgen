
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Models/" + entityName + "/" + entityName + "Search.cs";

<%

namespace AceFx.Models
{
    public class <$ entityName $>Search
    {
        public string Name { get; set; }
    }
}

%>
