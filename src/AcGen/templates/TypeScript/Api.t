
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);
outputFileName = model.RootModel.OutDir + "/TypeScript" + "/" + entityName + "Api.ts";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "any";
if(idColumn != null)
{
    keyType = idColumn.TypeScriptDataTypeName;
}

<%
import { defHttp } from "/@/utils/http/axios";
import { ErrorMessageMode } from "/#/axios";
import {
  <$ entityName $>Model as Model,
  Add<$ entityName $>Input as AddInput,
  Update<$ entityName $>Input as UpdateInput,
  ListQueryInput,
  PageListQueryInput
} from "./model/<$ entityName $>Model";

function appendApiPath(action: string) {
  return "/<$ entityName $>/" + action;
}

let Api = {
  List: appendApiPath("List"),
  PageList: appendApiPath("PageList"),
  Add: appendApiPath("Add"),
  Update: appendApiPath("Update"),
  Delete: appendApiPath("Delete")
};


export async function getList(params: ListQueryInput | any, mode: ErrorMessageMode = "modal"): Promise<Model[]> {

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

export async function getPageList(params: PageListQueryInput | any, mode: ErrorMessageMode = "modal"): Promise<Model[]> {

  return defHttp.get<Model[]>(
    {
      url: Api.PageList,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function add(params: AddInput, mode: ErrorMessageMode = "modal"): Promise<Model> {
  return defHttp.post<Model>(
    {
      url: Api.Add,
      params
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function update(params: UpdateInput, mode: ErrorMessageMode = "modal") {
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

export async function del(id: <$ keyType $>, mode: ErrorMessageMode = "modal") {
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