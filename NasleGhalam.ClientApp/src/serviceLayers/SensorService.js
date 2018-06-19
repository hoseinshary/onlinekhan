import { LoadingAxios, SimpleAxios } from "./_Axios";
import {
  Sensor_URL,
  API_URL,
  Region_URL,
  SensorGroup_URL,
  Area_URL
} from "utilities/site-config";

export default {
  getAll() {
    return LoadingAxios.get(`${API_URL}${Sensor_URL}/GetAll`);
  },
  getAllRegion() {
    return LoadingAxios.get(`${API_URL}${Region_URL}/GetAllDdl`);
  },
  getAllSensorGroup() {
    return LoadingAxios.get(`${API_URL}${SensorGroup_URL}/GetAllDdl`);
  },
  getAreaByRegionId(regionId) {
    return LoadingAxios.get(
      `${API_URL}${Area_URL}/GetByRegionIdDdl/` + regionId
    );
  },
  Create(instanceObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Sensor_URL}/Create`, instanceObj);
  },
  getById(Id) {
    return LoadingAxios.get(`${API_URL}${Sensor_URL}/GetById/` + Id);
  },
  Delete(Id) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Sensor_URL}/Delete/` + Id);
  },
  Edit(regionObj) {
    debugger;
    return LoadingAxios.post(`${API_URL}${Sensor_URL}/Update`, regionObj);
  }
};
