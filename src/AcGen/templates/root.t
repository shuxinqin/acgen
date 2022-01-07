
foreach(var table in model.Tables)
{
    var newModel = new { Table = table, RootModel = model };
    
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
}