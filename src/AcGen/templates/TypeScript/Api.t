
var projectName = model.ProjectName;
var moduleName = model.ModuleName;

var entityName = UnderScoreCaseToPascal(model.Table.TrimedName);

string moduleDir = $"";
string moduleApiPath = $"";

if(!string.IsNullOrEmpty(moduleName))
{
    moduleDir = $"{moduleName}/";
    moduleApiPath = $"{moduleName}/";
}

outputFileName = model.RootModel.OutDir + "/TypeScript/" + moduleDir + entityName + "Api.ts";

var table = (AcGen.DbTableInfo)model.Table;
var idColumn = table.Columns.Where(a => a.IsPrimaryKey).FirstOrDefault();

string keyType = "any";
if(idColumn != null)
{
    keyType = idColumn.TypeScriptDataTypeName;
}

<%
import { defHttp } from "@/utils/http/axios";
import { ErrorMessageMode } from "#/axios";
import { PageData } from "@/api/types/PageData";
import {
  <$ entityName $>Model as Model,
  Add<$ entityName $>Input as AddInput,
  Update<$ entityName $>Input as UpdateInput,
  QueryInput,
  PageQueryInput,
  ImportInput
} from "./Models/<$ entityName $>Model";

function appendApiPath(action: string) {
  return "/<$ moduleApiPath $><$ entityName $>/" + action;
}

let Api = {
  List: appendApiPath("List"),
  PageList: appendApiPath("PageList"),
  Add: appendApiPath("Add"),
  Update: appendApiPath("Update"),
  Delete: appendApiPath("Delete"),
  DeleteBatch: appendApiPath("DeleteBatch"),
  Import: appendApiPath("Import"),
};


export async function getList(params: QueryInput, mode: ErrorMessageMode = "modal"): Promise<Model[]> {
  return defHttp.get<Model[]>(
    {
      url: Api.List,
      params: params
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function getPageList(params: PageQueryInput, mode: ErrorMessageMode = "modal"): Promise<PageData<Model>> {
  return defHttp.get<PageData<Model>>(
    {
      url: Api.PageList,
      params: params
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function add(data: AddInput, mode: ErrorMessageMode = "modal"): Promise<Model> {
  return defHttp.post<Model>(
    {
      url: Api.Add,
      data: data
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function update(data: UpdateInput, mode: ErrorMessageMode = "modal"): Promise<Model> {
  return defHttp.post<Model>(
    {
      url: Api.Update,
      data: data
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function del(id: <$ keyType $>, mode: ErrorMessageMode = "modal") {
  return defHttp.post(
    {
      url: Api.Delete,
      data: id
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function deleteBatch(ids: <$ keyType $>[], mode: ErrorMessageMode = "modal") {
  return defHttp.post(
    {
      url: Api.DeleteBatch,
      data: ids
    },
    {
      errorMessageMode: mode
    }
  );
}

export async function importData(input: ImportInput, mode: ErrorMessageMode = "modal") {
  return defHttp.post(
    {
      url: Api.Import,
      data: input
    },
    {
      errorMessageMode: mode
    }
  );
}

%>