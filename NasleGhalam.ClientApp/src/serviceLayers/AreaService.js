import { LoadingAxios, SimpleAxios } from "./_Axios";
import { Area_URL, API_URL } from "utilities/site-config";

export default {
  getAllRegions() {
    return LoadingAxios.get(`${API_URL}${Region_URL}/GetAll`);
  },
  getAllAreaByRegionId(regionId) {
    return LoadingAxios.get(
      `${API_URL}${Area_URL}/GetAllByRegionId/` + regionId
    );
  },
  createArea(newAreaObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Area_URL}/Create`, newAreaObj);
  },
  getAreaById(areaId) {
    return LoadingAxios.get(`${API_URL}${Area_URL}/GetById/` + areaId);
  },
  deleteArea(areaId) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Area_URL}/Delete/` + areaId);
  },
  editArea(areaObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Area_URL}/Update`, areaObj);
  }
};
