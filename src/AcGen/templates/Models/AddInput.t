
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Models/" + entityName + "/Add" + entityName + "Input.cs";

<%

namespace AceFx.Models
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
