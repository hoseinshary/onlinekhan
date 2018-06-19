import { LoadingAxios, SimpleAxios } from "./_Axios";
import { Role_URL, API_URL } from "utilities/site-config";

export default {
  getAll() {
    return LoadingAxios.get(`${API_URL}${Role_URL}/GetAll`);
  },
  getAllController() {
    return LoadingAxios.get(`${API_URL}${Role_URL}/GetAllControllerDdl`);
  },
  getActionByControllerId(customObj) {
    return LoadingAxios.get(
      `${API_URL}${Role_URL}/GetActionBycontrollerId/?roleId=` +
        customObj.roleId +
        "&controllerId=" +
        customObj.controllerId
    );
  },
  changeAccess(customObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Role_URL}/changeAccess/`, customObj);
  },
  Create(instanceObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Role_URL}/Create`, instanceObj);
  },
  getById(Id) {
    return LoadingAxios.get(`${API_URL}${Role_URL}/GetById/` + Id);
  },
  Delete(Id) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Role_URL}/Delete/` + Id);
  },
  Edit(instanceObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Role_URL}/Update`, instanceObj);
  }
};
