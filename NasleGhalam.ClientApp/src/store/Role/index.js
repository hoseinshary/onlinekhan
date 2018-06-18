import _queryService from "serviceLayers/RoleService";

export default {
  strict: false,
  namespaced: true,
  state: {
    controllerList: [],
    actionList: [],
    allObj: [{}],
    instanceObj: {},
    accessObj: {},
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
      // _queryService.getAll().then(response => {
      //   state.allObj = response.data;
      // });
      state.allObj = [
        {ID:1,Name:'کاربر ارشد',Level:2},
      ]
    },
    getAllController(state) {
      _queryService.getAllController().then(response => {
        state.controllerList = response.data;
      });
    },
    getActionByControllerId(state) {
      _queryService
        .getActionByControllerId({
          controllerId: state.accessObj.controllerId,
          roleId: state.instanceObj.Id
        })
        .then(response => {
          state.actionList = response.data;
        });
    },
    // changeAccess(state, customObj) {
    //   var obj = {
    //     roleId: state.instanceObj.Id,
    //     lstActionId: customObj.lsit,
    //     isChecked: customObj.ischecked
    //   };
    //   debugger;
    //   _queryService.changeAccess(obj).then(response => {
    //     var axiosData = response.data;
    //     var myNoti = {
    //       message: axiosData.Message,
    //       isSuccess: axiosData.MessageType == 1,
    //       action: ""
    //     };
    //     context.dispatch("notify", myNoti, { root: true });
    //   });
    // },
    buildVmodel(state) {
      debugger;
      var myObj = state.instanceObj;
      state.vmObj = { Id: myObj.Id, Name: myObj.Name, Level: myObj.Level };
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
      debugger;
      state.allObj[objIndex].Name = state.instanceObj.Name;
      state.allObj[objIndex].Level = state.instanceObj.Level;
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
        Id: 0,
        Name: "",
        Level: 0
      };
      debugger;
      context.state.accessObj = {
        controllerId: 0,
        roleId: 0,
        actionId: 0,
        isChecked: false
      };
    },
    getById_store(context, Id) {
      context.commit("getById", Id);
    },
    getAll_store(context) {
      context.commit("getAll");
    },
    getAllController_store(context) {
      context.commit("getAllController"); //ddl
    },
    getActionByControllerId_store(context) {
      context.commit("getActionByControllerId"); //ddl
    },
    changeAccess_Store(context, customObj) {
      var obj = {
        roleId: context.state.instanceObj.Id,
        lstActionId: customObj.lsit,
        isChecked: customObj.ischecked
      };
      debugger;
      _queryService.changeAccess(obj).then(response => {
        var axiosData = response.data;
        var myNoti = {
          message: axiosData.Message,
          isSuccess: axiosData.MessageType == 1,
          action: ""
        };
        context.dispatch("notify", myNoti, { root: true });
      }); //ddl
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
