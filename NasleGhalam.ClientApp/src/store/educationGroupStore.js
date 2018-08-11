import util from 'utilities/util';
import axios from 'utilities/axios';
import { EDUCATION_GROUP_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in educationGroupGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.educationGroupGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'گروه آموزشی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    educationGroupObj: {
      Id: 0,
      Name: ''
    },
    educationGroupGridData: [],
    educationGroupDdl: [],
    selectedId: 0,
    isModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new educationGroupObj to educationGroupGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.educationGroupObj);
      createdObj.Id = id;
      state.educationGroupGridData.push(createdObj);
    },

    /**
     * update educationGroupObj of educationGroupGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.educationGroupObj,
        state.educationGroupGridData[index]
      );
    },

    /**
     * delete from educationGroupGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.educationGroupGridData.splice(index, 1);
    },

    /**
     * rest value of educationGroupObj
     */
    reset(state, $v) {
      util.clearObject(state.educationGroupObj);
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
        util.mapObject(response.data, state.educationGroupObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state, dispatch }) {
      // fill grid if modelChanged
      if (state.isModelChanged) {
        dispatch('toggleModelChangeStore', false);

        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.educationGroupGridData = response.data;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state, dispatch }) {
      // fill grid if modelChanged
      if (state.isModelChanged) {
        dispatch('toggleModelChangeStore', false);

        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.educationGroupDdl = response.data;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.educationGroupObj.$touch();
      if (vm.$v.educationGroupObj.$error) {
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

        axios
          .post(`${baseUrl}/Create`, state.educationGroupObj)
          .then(response => {
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
        state.educationGroupObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.educationGroupObj)
          .then(response => {
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
    }
    //------------------------------------------------
  },
  getters: {
    recordName(state) {
      return state.educationGroupObj.Name;
    }
  }
};

export default store;
