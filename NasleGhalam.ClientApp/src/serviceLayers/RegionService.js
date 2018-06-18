import { LoadingAxios, SimpleAxios } from "./_Axios";
import { Region_URL, API_URL } from "utilities/site-config";

export default {
  getAllRegions() {
    return LoadingAxios.get(`${API_URL}${Region_URL}/GetAll`);
  },
  createRegion(regionObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Region_URL}/Create`, regionObj);
  },
  getRegionById(regionId) {
    return LoadingAxios.get(`${API_URL}${Region_URL}/GetById/` + regionId);
  },
  deleteRegion(regionId) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Region_URL}/Delete/` + regionId);
  },
  editRegion(regionObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Region_URL}/Update`, regionObj);
  }
};
