# acgen
acgen 是基于 .net 6 开发的一个根据数据库表结构支持模版的代码生成器。可以根据现有数据库表仅用一个命令生成实体、DTO、Repository层、Service层、API层、前端模型、前端CURD代码，可以自定义生成代码模版和根据自己的项目结构定义生成代码的保存目录。

目前仅支持 mysql。如需连接其它类型数据库，请参考 mysql 实现自行扩展即可。

## 编译
在 AcGen 目录中打开 cmd 运行命令：
```
dotnet publish -c Release -r win-x64 --self-contained -o bin\Release\win-x64
```
相关程序文件将发布在 bin\Release\win-x64 目录下，进入 bin\Release\win-x64 目录在 cmd 里输入 acgen 命令即可启动程序：
```
acgen -h
```
## 运行 acgen 程序
**acgen 启动参数说明：**
```
-t(template):         模版文件。不指定则使用内置模板

-o(out):              指定输出文件保存目录。不指定则保存在当前目录的 out 文件夹中

-clean:               表示生成之前清空 -o(out) 目录

-db:                  数据库类型。目前支持 mysql

-conn:                用双引号("")包裹的数据库连接字符串

-tables               指定要生成代码的表(多个用 "," 分隔)。不传或传空则查询所有表

-trim_prefixes        指定要移除的表前缀(多个用 "," 分隔)

-v:                   查看版本号

-h:                   查看帮助
```
**例：**

```
acgen -clean -db mysql -conn "Server=localhost;Port=3306;Database=Chloe;Uid=root;Password=sasa;"
```

db 参数传入 test 可直接查看 mock db 生成效果：
```
acgen -clean -db test -conn ""
```

程序运行结束后，会根据 -t 传入的模版生成相应的文件，生成的文件会保存在 out 目录下。

## 模版文件
acgen 未使用其它第三方模版引擎，是鄙人根据个人喜好自主设计的一套简单模版语法。
### 编写说明
* 模版文件使用 .t 后缀

* 编写规则：
    * 模版内 <% %> 块内表示输出内容，<% 表示开始标记，%> 表示结束标记
    * 模版内 <# #> 块内表示 csharp 代码，<# 表示开始标记，#> 表示结束标记
    * 模版内 <$ $> 块内可填写 csharp 的变量，如 <$ model.Name $>，最终会直接输出 model.Name 值到相应位置，类似 vue 的 {{ model.Name }} 效果

* 根模版（root.t，即通过 -t 传入的模版）内可使用的变量：
    * model： model 为 TemplateModel 类型对象，具体参考 https://github.com/shuxinqin/acgen/blob/main/src/AcGen/TemplateModel.cs
    * indent：表示当前模版内缩进字符
    * writer：代码输出对象
    * outputFileName：表示要保存的文件全名。空则表示不生成文件，如需要输出文件，可通过 model 内属性根据自己的需求拼接。默认为空。

* 模版内可使用的方法：

    * Include(string templatePath, object model)：表示引用子模版。

          templatePath：子模版路径
          model：传给子模版的对象模型
      
    * Include(string templatePath, object model, string indent)：表示引用子模版。
      
          templatePath：子模版路径
          model：传给子模版的对象模型
          indent：指定子模版要缩进的字符
      
    * Emit(string templatePath, object model)：表示将 model 分发给模版 templatePath 处理。
      
          templatePath：要处理 model 模版的路径 
          model：传给模版（templatePath）的对象模型


### 示例
根模版：[root.t](./src/AcGen/templates/root.t)
```
var projectName = "AceFx";
var moduleName = "Sys";

foreach(var table in model.Tables)
{
    var newModel = new { RootModel = model, Table = table, ProjectName = projectName, ModuleName = moduleName };
    
    Emit("Entity.t", newModel);    //将 newModel 分发给 Entity.t 模版处理

    Emit("Models/AddInput.t", newModel);
    Emit("Models/UpdateInput.t", newModel);
    Emit("Models/InputBase.t", newModel);
    Emit("Models/Model.t", newModel);
    Emit("Models/Search.t", newModel);

    Emit("Mapping.t", newModel);
    Emit("MappingMySql.t", newModel);
    Emit("Repository.t", newModel);
    Emit("RepositoryImpl.t", newModel);

    Emit("Service.t", newModel);
    Emit("ServiceImpl.t", newModel);

    Emit("Controller.t", newModel);
}
```

子模版：[Entity.t](./src/AcGen/templates/Entity.t)
```
//root.t 传过来的 model 是一个匿名类型

var projectName = model.ProjectName;
var moduleName = model.ModuleName;

//将下划线分隔的名称转成 Pascal 风格名称
var entityName = UnderScoreCaseToPascal(model.Table.Name);

//设置输出的文件路径
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
```
## 附
更多参考：

模版文件：[templates](./src/AcGen/templates)
