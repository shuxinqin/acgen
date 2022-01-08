
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/" + projectName + "." + "Entities/" + entityName + ".cs";

<%

namespace <$ projectName $>.Entities
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
