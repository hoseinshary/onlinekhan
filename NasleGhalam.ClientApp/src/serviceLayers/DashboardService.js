import { LoadingAxios, SimpleAxios } from "./_Axios";
import { DASHBOARD_URL, API_URL } from "utilities/site-config";

export default {
  fillBoxes() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/FillBoxes`);
  },
  getsensorwarningsModal() {
    return LoadingAxios.get(`${API_URL}${DASHBOARD_URL}/getsensorwarnings`);
  },
  getsensorwarningsDashboard() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/getsensorwarnings`);
  },
  DetailOfAreaByWarning() {
    return LoadingAxios.get(`${API_URL}${DASHBOARD_URL}/DetailOfAreaByWarning`);
  },
  GetSensorWarningsByAreaId(AreaId) {
    return LoadingAxios.get(
      `${API_URL}${DASHBOARD_URL}/GetSensorWarningsByAreaId?AreaId=` + AreaId
    );
  },
  DetailOfregionByWarning() {
    return LoadingAxios.get(
      `${API_URL}${DASHBOARD_URL}/DetailOfregionByWarning`
    );
  },
  DetailOfAreaByRegionId(RegionId) {
    return LoadingAxios.get(
      `${API_URL}${DASHBOARD_URL}/DetailOfAreaByRegionId?RegionId=` + RegionId
    );
  },
  GetAllAreas() {
    return LoadingAxios.get(`${API_URL}${DASHBOARD_URL}/GetAllAreas`);
  },
  GetAllSensorDatas() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/GetAllSensorDatas`);
  },
  GetAllAreaStates() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/GetAllAreaStates`);
  },
  GetAllSensorValues() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/GetAllSensorValues`);
  },
  ChangeAreaState(AreaId, customState) {
    return LoadingAxios.get(
      `${API_URL}${DASHBOARD_URL}/ChangeAreaState?areaId=` +
        AreaId +
        `&state=` +
        customState
    );
  },
  GetAreasByRegionId(RegionId) {
    return LoadingAxios.get(
      `${API_URL}${DASHBOARD_URL}/GetAreasByRegionId?RegionId=` + RegionId
    );
  },
  GetAllRegions() {
    return SimpleAxios.get(`${API_URL}${DASHBOARD_URL}/GetAllRegions`);
  }
};
