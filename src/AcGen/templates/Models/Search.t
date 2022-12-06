
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Models/" + entityName + "/" + entityName + "Search.cs";

<%

namespace <$ projectName $>.Models
{
    public class <$ entityName $>Search
    {
        public string Name { get; set; }
    }
}

%>
