import _areaService from "serviceLayers/AreaService";

var _resault = true;
var _message = "";
export default {
  strict: false,
  namespaced: true,
  state: {
    allAreas: [],
    areaObj: {}
  },
  getters: {},
  mutations: {
    ///
    getAllAreaByRegionId(state, regionId) {
      if (regionId) {
        _areaService.getAllAreaByRegionId(regionId).then(response => {
          state.allAreas = response.data;
        });
      }
    },
    getAreaById(state, areaId) {
      _areaService.getAreaById(areaId).then(response => {
        state.areaObj = response.data;
      });
      // state.areaObj = { Id: 1, Name: "شمال", PolygonPath: "1" };
    },

    pushArea(state, Id) {
      state.areaObj.Id = Id;
      state.allAreas.push(Object.assign({}, state.areaObj));
    },
    updateArea(state, edited) {
      var objIndex = state.allAreas.findIndex(
        obj => obj.Id == state.areaObj.Id
      );
      state.allAreas[objIndex] = edited;
    },
    deleteArea(state) {
      var objIndex = state.allAreas.findIndex(
        obj => obj.Id == state.areaObj.Id
      );
      state.allAreas.splice(objIndex, 1);
    }
  },
  actions: {
    getAllAreaByRegionId_store(context, regionId) {
      context.commit("getAllAreaByRegionId", regionId);
    },
    getAreaById_store(context, areaId) {
      context.commit("getAreaById", areaId);
    },
    getAllAreas_store(context) {
      context.commit("getAllAreas");
    },
    submitModalDeleteArea_store(context) {
      _areaService.deleteArea(context.state.areaObj.Id).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
          context.commit("deleteArea");
        }
        var myNoti = {
          message: axiosData.Message,
          isSuccess: axiosData.MessageType == 1,
          action: "delete"
        };
        context.dispatch("notify", myNoti, { root: true });
      });
    },
    submitModalEditArea_store(context, data) {
      var edited = {
        Id: context.state.areaObj.Id,
        Name: context.state.areaObj.Name,
        PolygonPath: data.poly,
        PointLatLng: data.point,
        RegionId: data.regionId
      };
      _areaService.editArea(edited).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
          context.commit("updateArea", edited);
        }
        var myNoti = {
          message: axiosData.Message,
          isSuccess: axiosData.MessageType == 1,
          action: "edit"
        };
        context.dispatch("notify", myNoti, { root: true });
      });
    },
    submitModalCreateArea_store(context, data) {
      _areaService
        .createArea({
          Name: context.state.areaObj.Name,
          PolygonPath: data.poly,
          PointLatLng: data.point,
          RegionId: data.regionId
        })
        .then(response => {
          var axiosData = response.data;
          if (axiosData.MessageType == 1) {
            context.commit("pushArea", axiosData.Id);
          }
          var myNoti = {
            message: axiosData.Message,
            isSuccess: axiosData.MessageType == 1,
            action: "create"
          };
          context.dispatch("notify", myNoti, { root: true });
        });
    }
  }
};
