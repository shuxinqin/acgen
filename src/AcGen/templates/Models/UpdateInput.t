
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Models/" + entityName + "/Update" + entityName + "Input.cs";

<%

namespace <$ nc $>.Models
{
    [MapToType(typeof(<$ entityName $>))]
    public class Update<$ entityName $>Input : <$ entityName $>InputBase
    {
        <%
        public Update<$ entityName $>Input()
        {

        }

        %>
        <#
        foreach(var column in model.Table.Columns)
        {
            if(!column.IsPrimaryKey)
            {
                continue;
            }
        <%
        /// <summary>
        /// <$ column.Comment $>
        /// </summary>
        public <$ column.DataTypeName $> <$ column.Name $> { get; set; }

        %>
        }
        #>
    }
}

%>
