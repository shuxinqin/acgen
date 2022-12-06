
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/csharp/" + projectName + "." + "Entities/" + entityName + ".cs";

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
