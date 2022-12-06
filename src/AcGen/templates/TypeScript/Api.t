
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.Name);
outputFileName = model.RootModel.OutDir + "/TypeScript" + "/" + entityName + "Api.ts";

<%
import { defHttp } from "/@/utils/http/axios";
import { ErrorMessageMode } from "/#/axios";
import {
  <$ entityName $>Model as Model,
  Add<$ entityName $>Input as AddInput,
  Update<$ entityName $>Input as UpdateInput,
  ListQueryInput
} from "./model/<$ entityName $>Model";

function appendApiPath(action: string) {
  return "/<$ entityName $>/" + action;
}

let Api = {
  List: appendApiPath("List"),
  Add: appendApiPath("Add"),
  Update: appendApiPath("Update"),
  Delete: appendApiPath("Delete")
};


export function getList(params: ListQueryInput, mode: ErrorMessageMode = "modal"): Promise<Model[]> {

  return defHttp.get<Model[]>(
    {
      url: Api.List,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

export function add(params: AddInput, mode: ErrorMessageMode = "modal"): Promise<void> {
  return defHttp.post<void>(
    {
      url: Api.Add,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

export function update(params: UpdateInput, mode: ErrorMessageMode = "modal") {
  return defHttp.post(
    {
      url: Api.Update,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

export function del(id: string, mode: ErrorMessageMode = "modal") {
  let params = { id: id };
  return defHttp.post(
    {
      url: Api.Delete,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

%>