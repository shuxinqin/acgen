
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Entities/" + entityName + ".cs";

<%

namespace <$ nc $>.Entities
{
    /// <summary>
    /// <$ model.Table.Comment $>
    /// </summary>
    public class <$ entityName $>
    {
        <%
        public <$ entityName $>()
        {

        }

        %>
        <#
        foreach(var column in model.Table.Columns)
        {
            var dataTypeName = column.DataTypeName;
            if(column.Name == "IsDeleted")
            {
                dataTypeName = "bool";
            }

        <%
        /// <summary>
        /// <$ column.Comment $>
        /// </summary>
        public <$ dataTypeName $> <$ column.Name $> { get; set; }

        %>
        }
        #>
    }
}

%>
