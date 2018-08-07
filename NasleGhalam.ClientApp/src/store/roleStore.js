import util from 'utilities/util';
import axios from 'utilities/axios';
import { ROLE_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in roleGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.roleGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'نقش',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    isOpenModalAccess: false,
    roleObj: {
      Id: 0,
      Name: '',
      Level: 0
    },
    roleGridData: [],
    roleDdl: [],
    selectedId: 0,
    isModelChanged: true,
    createVue: null,
    editVue: null,
    accessObj: {
      ModuleId: 0,
      ControllerId: 0
    },
    moduleDdl: [],
    controllerDdl: [],
    actionGridData: [],
    roleName: ''
  },
  mutations: {
    /**
     * insert new roleObj to roleGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.roleObj);
      createdObj.Id = id;
      state.roleGridData.push(createdObj);
    },

    /**
     * update roleObj of roleGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.roleObj, state.roleGridData[index]);
    },

    /**
     * delete from roleGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.roleGridData.splice(index, 1);
    },

    /**
     * rest value of roleObj
     */
    reset(state, $v) {
      util.clearObject(state.roleObj);
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
    /**
     * get data by id
     */
    getByIdStore({ state }, id) {
      axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.roleObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state, dispatch }) {
      if (state.isModelChanged) {
        dispatch('toggleModelChangeStore', false);
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.roleGridData = response.data;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state, dispatch }) {
      if (state.isModelChanged) {
        dispatch('toggleModelChangeStore', false);
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.roleDdl = response.data;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.roleObj.$touch();
      if (vm.$v.roleObj.$error) {
        dispatch('notifyInvalidForm', vm, { root: true });
        return false;
      }

      return true;
    },

    /**
     * change isModelChange
     * @param {Boolean} b
     */
    toggleModelChangeStore({ state }, b) {
      state.isModelChanged = b;
    },

    //### create section ###
    /**
     * toggle modal create
     */
    toggleModalCreateStore({ state }, isOpen) {
      state.isOpenModalCreate = isOpen;
    },

    /**
     * init create vue on load
     */
    createVueStore({ state }, vm) {
      state.createVue = vm;
    },

    /**
     * submit create data
     */
    submitCreateStore({ state, commit, dispatch }, closeModal) {
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        axios.post(`${baseUrl}/Create`, state.roleObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
            dispatch('toggleModelChangeStore', true);
            dispatch('resetCreateStore');
            dispatch('toggleModalCreateStore', !closeModal);
          }

          dispatch(
            'notify',
            {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            },
            { root: true }
          );
        });
      });
    },

    /**
     * reset create vue
     */
    resetCreateStore({ state, commit }) {
      commit('reset', state.createVue.$v);
    },
    //------------------------------------------------

    //### edit section ###
    /**
     * toggle modal edit
     */
    toggleModalEditStore({ state }, isOpen) {
      state.isOpenModalEdit = isOpen;
    },

    /**
     * init edit vue on load
     */
    editVueStore({ state }, vm) {
      state.editVue = vm;
    },

    /**
     * submit edit data
     */
    submitEditStore({ state, commit, dispatch }) {
      var vm = state.editVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;
        state.roleObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.roleObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('update');
            dispatch('toggleModelChangeStore', true);
            dispatch('resetEditStore');
            dispatch('toggleModalEditStore', false);
          }

          dispatch(
            'notify',
            {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            },
            { root: true }
          );
        });
      });
    },

    /**
     * reset edit vue
     */
    resetEditStore({ state, commit }) {
      commit('reset', state.editVue.$v);
    },
    //------------------------------------------------

    //### delete section ###
    /**
     * toggle modal delete
     */
    toggleModalDeleteStore({ state }, isOpen) {
      state.isOpenModalDelete = isOpen;
    },

    /**
     * submit to delete data
     */
    submitDeleteStore({ state, commit, dispatch }, vm) {
      axios.post(`${baseUrl}/Delete/${state.selectedId}`).then(response => {
        let data = response.data;
        if (data.MessageType == 1) {
          commit('delete');
          commit('reset');
          dispatch('toggleModelChangeStore', true);
          dispatch('toggleModalDeleteStore', false);
        }

        dispatch(
          'notify',
          {
            body: data.Message,
            type: data.MessageType,
            vm: vm
          },
          { root: true }
        );
      });
    },
    //------------------------------------------------

    //### access section ###
    /**
     * toggle modal access
     */
    toggleModalAccessStore({ state }, isOpen) {
      state.isOpenModalAccess = isOpen;
    },
    /**
     * fill module ddl
     */
    fillModuleDdlStore({ state }) {
      if (state.moduleDdl.length) {
        return;
      }

      axios.get(`${baseUrl}/GetAllModuleDdl`).then(response => {
        state.moduleDdl = response.data;
      });
    },

    /**
     * fill controller by module id ddl
     */
    fillControllerByModuleIdDdlStore({ state }, id) {
      axios
        .get(`${baseUrl}/GetAllControllerByModuleIdDdl/${id}`)
        .then(response => {
          state.controllerDdl = response.data;
          state.accessObj.ControllerId = 0;
        });
    },

    /**
     * fill action grid by roleId and controllerId ddl
     */
    fillActionByControllerIdAndModuleIdGridStore({ state }, obj) {
      axios
        .get(
          `${baseUrl}/GetActionByControllerIdAndModuleId?roleId=${
            state.selectedId
          }&controllerId=${obj.controllerId}&moduleId=${obj.moduleId}`
        )
        .then(response => {
          state.actionGridData = response.data;
        });
    },

    /**
     * set role id
     * @param {Number} id
     */
    setRoleIdStore({ state }, id) {
      state.selectedId = id;
    },

    /**
     * set role name
     * @param {String} name roleName
     */
    setRoleNameStore({ state }, name) {
      state.roleName = name;
    },

    /**
     * change access
     */
    changeAccessStore({ state, dispatch }, obj) {
      obj.RoleId = state.selectedId;

      axios.post(`${baseUrl}/ChangeAccess`, obj).then(response => {
        let data = response.data;

        if (data.MessageType != 1) {
          dispatch(
            'notify',
            {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            },
            { root: true }
          );
        }
      });
    }
  },
  getters: {
    recordName(state) {
      return state.roleObj.Name;
    }
  }
};

export default store;
