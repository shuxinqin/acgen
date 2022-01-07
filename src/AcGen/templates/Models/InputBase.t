
var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/Models/" + entityName + "/" + entityName + "InputBase.cs";

<%

namespace AceFx.Models
{
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
