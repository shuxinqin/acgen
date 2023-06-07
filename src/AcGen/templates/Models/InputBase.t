
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Models/" + entityName + "/" + entityName + "InputBase.cs";

<%

namespace <$ nc $>.Models
{
    [MapToType(typeof(<$ entityName $>))]
    public class <$ entityName $>InputBase : ValidationModel
    {
        <%
        public <$ entityName $>InputBase()
        {

        }

        %>
        <#
        foreach(var column in model.Table.Columns)
        {
            if(column.IsPrimaryKey)
            {
                continue;
            }
            if(column.Name == "CreateTime" || column.Name == "CreateUserId" || column.Name == "CreateUserName" || column.Name == "IsDeleted" || column.Name == "DeleteUserId" || column.Name == "DeleteTime")
            {
                continue;
            }
        <%
        /// <summary>
        /// <$ column.Comment $>
        /// </summary>
        <#
            if(column.Name == "Name")
            {
        <%
        [RequiredAttribute(ErrorMessage = "名称不能为空")]
        %>
            }
        #>
        public <$ column.DataTypeName $> <$ column.Name $> { get; set; }

        %>
        }
        #>
    }
}

%>
