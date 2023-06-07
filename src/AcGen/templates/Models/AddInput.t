
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Models/" + entityName + "/Add" + entityName + "Input.cs";

<%

namespace <$ nc $>.Models
{
    [MapToType(typeof(<$ entityName $>))]
    public class Add<$ entityName $>Input : <$ entityName $>InputBase
    {
        <%
        public Add<$ entityName $>Input()
        {

        }

        %>
        <#
        foreach(var column in model.Table.Columns)
        {
            if(!(column.Name == "CreateUserId" || column.Name == "CreateUserName"))
            {
                continue;
            }
        <%
        public <$ column.DataTypeName $> <$ column.Name $> { get; set; }

        %>
        }
        #>
    }
}

%>
