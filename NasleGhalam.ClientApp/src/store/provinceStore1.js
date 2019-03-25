import util from "src/utilities";
import axios from "plugins/axios";
import { PROVINCE_URL as baseUrl } from "utilities/site-config";

/**
 * find index of object in provinceGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.provinceGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: "استان",
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    provinceObj: {
      Id: 0,
      Name: "",
      Code: ""
    },
    provinceGridData: [],
    provinceDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new provinceObj to provinceGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.provinceObj);
      createdObj.Id = id;
      state.provinceGridData.push(createdObj);
    },

    /**
     * update provinceObj of provinceGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.provinceObj, state.provinceGridData[index]);
    },

    /**
     * delete from provinceGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.provinceGridData.splice(index, 1);
    },

    /**
     * rest value of provinceObj
     */
    reset(state, $v) {
      util.clearObject(state.provinceObj);
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
        util.mapObject(response.data, state.provinceObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      // fill grid if modelChanged
      if (state.gridModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.provinceGridData = response.data;
          state.gridModelChanged = false;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      // fill grid if modelChanged
      if (state.ddlModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.provinceDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.provinceObj.$touch();
      if (vm.$v.provinceObj.$error) {
        dispatch("notifyInvalidForm", vm, { root: true });
        return false;
      }

      return true;
    },

    /**
     * model changed
     */
    modelChangedStore({ state }) {
      state.ddlModelChanged = true;
      state.gridModelChanged = true;
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
      dispatch("validateFormStore", vm).then(isValid => {
        if (!isValid) return;

        axios.post(`${baseUrl}/Create`, state.provinceObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit("insert", data.Id);
            dispatch("modelChangedStore");
            dispatch("resetCreateStore");
            dispatch("toggleModalCreateStore", !closeModal);
          }

          dispatch(
            "notify",
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
      commit("reset", state.createVue.$v);
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
      dispatch("validateFormStore", vm).then(isValid => {
        if (!isValid) return;
        state.provinceObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.provinceObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit("update");
            dispatch("modelChangedStore");
            dispatch("resetEditStore");
            dispatch("toggleModalEditStore", false);
          }

          dispatch(
            "notify",
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
      commit("reset", state.editVue.$v);
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
          commit("delete");
          commit("reset");
          dispatch("modelChangedStore");
          dispatch("toggleModalDeleteStore", false);
        }

        dispatch(
          "notify",
          {
            body: data.Message,
            type: data.MessageType,
            vm: vm
          },
          { root: true }
        );
      });
    }
    //------------------------------------------------
  },
  getters: {
    recordName(state) {
      return state.provinceObj.Name;
    }
  }
};

export default store;
