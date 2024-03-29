﻿
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

outputFileName = model.RootModel.OutDir + "/csharp/" + moduleDir + ".Entity.Mapping/" + entityName + "MapBase.cs";

var table = (AcGen.DbTableInfo)model.Table;
var IsDeletedColumn = table.Columns.Where(a => a.Name == "IsDeleted").FirstOrDefault();

<%

namespace <$ nc $>.Entity.Mapping
{
    public abstract class <$ entityName $>MapBase : EntityTypeBuilder<<$ entityName $>>
    {
        protected <$ entityName $>MapBase()
        {
            <#
            foreach(var column in table.Columns)
            {
                if(column.Name.ToLower() == "id" && !column.IsAutoIncrement && (column.DataTypeName == "int" || column.DataTypeName == "long"))
                {
            <%
            this.Property(a => a.<$ column.Name $>).IsAutoIncrement(false);
            %>
                }
                if(column.IsPrimaryKey)
                {
            <%
            this.Property(a => a.<$ column.Name $>).IsPrimaryKey();
            %>
                }
                if(column.IsAutoIncrement)
                {
            <%
            this.Property(a => a.<$ column.Name $>).IsAutoIncrement();
            %>
                }
                if(column.DataTypeName == "string")
                {
            <%
            this.Property(a => a.<$ column.Name $>).HasSize(<$ column.Length $>);
            %>
                }
                if(column.DataTypeName == "string" && !column.IsNullable)
                {
            <%
            this.Property(a => a.<$ column.Name $>).IsNullable(false);
            %>
                }
                if(column.DataTypeName == "decimal")
                {
            <%
            this.Property(a => a.<$ column.Name $>).HasPrecision(<$ column.Precision $>);
            this.Property(a => a.<$ column.Name $>).HasScale(<$ column.Scale $>);
            %>
                }
                if(column.Name.ToLower() == "createuserid" || column.Name.ToLower() == "createusername" || column.Name.ToLower() == "createtime")
                {
            <%
            this.Property(a => a.<$ column.Name $>).UpdateIgnore();
            %>
                }
            }

            if(IsDeletedColumn != null)
            {
            <%

            this.HasQueryFilter(a => a.<$ IsDeletedColumn.Name $> == false);
            %>
            }
            #>
        }
    }
}

%>
