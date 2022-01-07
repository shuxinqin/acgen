
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Models/" + entityName + "/" + entityName + "Model.cs";

<%

namespace AceFx.Models
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
