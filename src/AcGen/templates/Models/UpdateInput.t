
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Models/" + entityName + "/Update" + entityName + "Input.cs";

<%

namespace AceFx.Models
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
