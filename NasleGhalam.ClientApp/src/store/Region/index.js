import _regionService from "serviceLayers/RegionService";

export default {
  strict: false,
  namespaced: true,
  state: {
    allRegions: [{}],
    regionObj: {}
  },
  getters: {},
  mutations: {
    getRegionById(state, regionId) {
      _regionService.getRegionById(regionId).then(response => {
        state.regionObj = response.data;
      });
      // state.regionObj = { Id: 1, Name: "شمال", PolygonPath: "1" };
    },
    getAllRegions(state) {
      _regionService.getAllRegions().then(response => {
        state.allRegions = response.data;
      });
    },
    pushRegion(state, Id) {
      debugger;
      state.regionObj.Id = Id;
      state.allRegions.push(Object.assign({}, state.regionObj));
    },
    updateRegion(state) {
      var objIndex = state.allRegions.findIndex(
        obj => obj.Id == state.regionObj.Id
      );
      state.allRegions[objIndex].Name = state.regionObj.Name;
    },
    deleteRegion(state) {
      var objIndex = state.allRegions.findIndex(
        obj => obj.Id == state.regionObj.Id
      );
      state.allRegions.splice(objIndex, 1);
    }
  },
  actions: {
    getRegionById_store(context, regionId) {
      debugger;
      context.commit("getRegionById", regionId);
    },
    getAllRegions_store(context) {
      context.commit("getAllRegions");
    },
    submitModalDeleteRegion_store(context) {
      _regionService.deleteRegion(context.state.regionObj.Id).then(response => {
        var axiosData = response.data;
        if (axiosData.MessageType == 1) {
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
    submitModalEditRegion_store(context) {
      _regionService.editRegion(context.state.regionObj).then(response => {
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
    submitModalCreateRegion_store(context) {
      debugger;
      _regionService
        .createRegion({ Name: context.state.regionObj.Name, PolygonPath: "1" })
        .then(response => {
          var axiosData = response.data;
          if (axiosData.MessageType == 1) {
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
