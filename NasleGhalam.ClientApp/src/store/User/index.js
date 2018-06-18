import _queryService from "serviceLayers/UserService";

export default {
  strict: false,
  namespaced: true,
  state: {
    roleList: [],
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
      // _queryService.getAll().then(response => {
      //   state.allObj = response.data;
      // });
      state.allObj = [
        {Id:2,Name:'مصطفی',Family:'خرم نیا',Username:'Mostafa',RoleName:'کاربر ارشد',IsActive:true,},
      ]
    },
    getAllRole(state) {
      _queryService.getAllRole().then(response => {
        state.roleList = response.data;
      });
    },
    buildVmodel(state) {
      debugger;
      var myObj = state.instanceObj;
      state.vmObj = {
        Id: myObj.Id,
        Name: myObj.Name,
        Family: myObj.Family,
        Username: myObj.Username,
        Password: myObj.Password,
        RoleId: myObj.RoleId,
        IsActive: myObj.IsActive
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
      debugger;
      state.allObj[objIndex].Name = state.instanceObj.Name;
      state.allObj[objIndex].Name = state.instanceObj.Family;
      state.allObj[objIndex].RoleName = state.instanceObj.RoleName;
      state.allObj[objIndex].Username = state.instanceObj.Username;
      state.allObj[objIndex].IsActive = state.instanceObj.IsActive;
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
        Family: "",
        Username: "",
        Password: "",
        IsActive: true,
        RoleId: 0,
        RoleName: ""
      };
    },
    getById_store(context, Id) {
      context.commit("getById", Id);
    },
    getAll_store(context) {
      context.commit("getAll");
    },
    getAllRole_store(context) {
      context.commit("getAllRole"); //ddl
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
