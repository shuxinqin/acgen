
var projectName = model.Args["project"];
var moduleName = model.Args["module"];

if(string.IsNullOrEmpty(projectName))
{
    projectName = "AceFx";
}

if(string.IsNullOrEmpty(moduleName))
{
    moduleName = "Biz";
}

foreach(var table in model.Tables)
{
    var newModel = new { RootModel = model, Table = table, ProjectName = projectName, ModuleName = moduleName };
    
    Emit("Entity.t", newModel);

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

    //前端代码
    Emit("TypeScript/Models/Model.t", newModel);
}