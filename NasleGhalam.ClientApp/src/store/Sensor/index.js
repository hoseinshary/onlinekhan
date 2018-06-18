import _queryService from "serviceLayers/SensorService";

export default {
  strict: false,
  namespaced: true,
  state: {
    regionList: [],
    areaList: [],
    sensorGroupList: [],
    allObj: [{}],
    instanceObj: {},
    vmObj: {}
  },
  getters: {},
  mutations: {
    getById(state, Id) {
      _queryService.getById(Id).then(response => {
        state.instanceObj = response.data;
      });
    },
    getAll(state) {
      _queryService.getAll().then(response => {
        state.allObj = response.data;
      });
    },
    getAllRegion(state) {
      _queryService.getAllRegion().then(response => {
        state.regionList = response.data;
      });
    },
    getAllSensorGroup(state) {
      _queryService.getAllSensorGroup().then(response => {
        state.sensorGroupList = response.data;
      });
    },
    getAreaByRegionId(state) {
      debugger;
      _queryService
        .getAreaByRegionId(state.instanceObj.RegionId)
        .then(response => {
          state.areaList = response.data;
        });
    },
    buildVmodel(state) {
      debugger;
      var myObj = state.instanceObj;
      state.vmObj = {
        Id:myObj.Id,
        RegionId: myObj.RegionId,
        AreaId: myObj.AreaId,
        Code: myObj.Code,
        SensorGroupId: myObj.SensorGroupId
      };
    },
    pushRegion(state, Id) {
      debugger;
      state.instanceObj.Id = Id;
      state.allObj.push(Object.assign({}, state.instanceObj));
    },
    updateRegion(state) {
      var objIndex = state.allObj.findIndex(
        obj => obj.Id == state.instanceObj.Id
      );
      debugger
      state.allObj[objIndex].AreaName = state.instanceObj.AreaName;
      state.allObj[objIndex].RegionName = state.instanceObj.RegionName;
      state.allObj[objIndex].SensorGroupName = state.instanceObj.SensorGroupName;
      state.allObj[objIndex].Code = state.instanceObj.Code;
    },
    deleteRegion(state) {
      var objIndex = state.allObj.findIndex(
        obj => obj.Id == state.instanceObj.Id
      );
      state.allObj.splice(objIndex, 1);
    }
  },
  actions: {
    reset_store(context, Id) {
      context.state.instanceObj = {
        AreaId: 0,
        AreaName: "",
        Code: "",
        Id: 0,
        RegionId: 0,
        RegionName: "",
        SensorGroupId: 0,
        SensorGroupName: ""
      };
    },
    getById_store(context, Id) {
      context.commit("getById", Id);
    },
    getAll_store(context) {
      context.commit("getAll");
    },
    getAllRegion_store(context) {
      context.commit("getAllRegion"); //ddl
    },
    getAllSensorGroup_store(context) {
      context.commit("getAllSensorGroup"); //ddl
    },
    getAreaByRegionId_store(context) {
      context.commit("getAreaByRegionId"); //ddl
    },
    submitModalDelete_store(context) {
      _queryService.Delete(context.state.instanceObj.Id).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
          debugger;
          context.commit("deleteRegion");
        }
        var myNoti = {
          message: axiosData.Message,
          isSuccess: axiosData.MessageType == 1,
          action: "delete"
        };
        context.dispatch("notify", myNoti, { root: true });
      });
    },
    submitModalEdit_store(context) {
      context.commit("buildVmodel");
      _queryService.Edit(context.state.vmObj).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
          context.commit("updateRegion");
        }
        var myNoti = {
          message: axiosData.Message,
          isSuccess: axiosData.MessageType == 1,
          action: "edit"
        };
        context.dispatch("notify", myNoti, { root: true });
      });
    },
    submitModalCreate_store(context) {
      context.commit("buildVmodel");
      _queryService.Create(context.state.vmObj).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
          debugger;
          context.commit("pushRegion", axiosData.Id);
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
