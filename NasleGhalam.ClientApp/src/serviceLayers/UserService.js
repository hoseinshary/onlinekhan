import { LoadingAxios, SimpleAxios } from "./_Axios";
import { User_URL, API_URL, Role_URL } from "utilities/site-config";

export default {
  logIn(username, password) {
    // return utility.getData(`${conf.DASHBOARD_URL}/GetData`, callback);

    return LoadingAxios.post(`${API_URL}${User_URL}/login`, {
      username,
      password
    });
  },
  getMenu() {
    return LoadingAxios.get(`${API_URL}${User_URL}/GetMenu`);
  },
  getsensorwarningsModal() {
    debugger;
    return LoadingAxios.get(`${API_URL}${User_URL}/getsensorwarnings`);
  },
  getAll() {
    return LoadingAxios.get(`${API_URL}${User_URL}/GetAll`);
  },
  getAllRole() {
    return LoadingAxios.get(`${API_URL}${Role_URL}/GetAllDdl`);
  },
  Create(instanceObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${User_URL}/Create`, instanceObj);
  },
  getById(Id) {
    return LoadingAxios.get(`${API_URL}${User_URL}/GetById/` + Id);
  },
  Delete(Id) {
    debugger;
    return LoadingAxios.post(`${API_URL}${User_URL}/Delete/` + Id);
  },
  Edit(instanceObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${User_URL}/Update`, instanceObj);
  }
};
