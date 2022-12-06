
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/TypeScript" + "/Models/" + entityName + "Model.ts";

<%

export interface <$ entityName $>Model {
    <#
    foreach(var column in model.Table.Columns)
    {
        if(column.Name == "IsDeleted" || column.Name == "DeleteUserId" || column.Name == "DeleteTime")
        {
            continue;
        }

        <%
    //<$ column.Comment $>
    <$ column.Name $>: <$ column.TypeScriptDataTypeName $>;
        %>
    }
    #>
}

%>

<%
export interface <$ entityName $>InputBase {
    <#
    foreach(var column in model.Table.Columns)
    {
        if(column.Name == "IsDeleted" || column.Name == "DeleteUserId" || column.Name == "DeleteTime")
        {
            continue;
        }

        if(column.IsPrimaryKey)
        {
            continue;
        }

        <%
    //<$ column.Comment $>
    <$ column.Name $>: <$ column.TypeScriptDataTypeName $>;
        %>
    }
    #>
}

%>

<%
export interface Add<$ entityName $>Input extends <$ entityName $>InputBase {

}

%>

<%
export interface Update<$ entityName $>Input extends <$ entityName $>InputBase {
    <#
    foreach(var column in model.Table.Columns)
    {
        if(!column.IsPrimaryKey)
        {
            continue;
        }

        <%
    //<$ column.Comment $>
    <$ column.Name $>: <$ column.TypeScriptDataTypeName $>;
        %>
    }
    #>
}

%>

<%
export interface ListQueryInput {
    Keyword: string;
}
%>