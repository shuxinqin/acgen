
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Models/" + entityName + "/" + entityName + "Model.cs";

<%

namespace <$ projectName $>.Models
{
    /// <summary>
    /// <$ model.Table.Comment $>
    /// </summary>
    [MapToType(typeof(<$ entityName $>))]
    public class <$ entityName $>Model
    {
        <%
        public <$ entityName $>Model()
        {

        }

        %>
        <#
        foreach(var column in model.Table.Columns)
        {
            if(column.Name == "IsDeleted" || column.Name == "DeleteUserId" || column.Name == "DeleteTime")
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
