import util from 'utilities/util';
import axios from 'utilities/axios';
import { CITY_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in cityGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.cityGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'عنوان',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    cityObj: {
      Id: 0,
      Name: '',
      ProvinceId: 0
    },
    cityGridData: [],
    cityDdl: [],
    selectedId: 0,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new cityObj to cityGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.cityObj);
      createdObj.Id = id;
      state.cityGridData.push(createdObj);
    },

    /**
     * update cityObj of cityGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.cityObj, state.cityGridData[index]);
    },

    /**
     * delete from cityGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.cityGridData.splice(index, 1);
    },

    /**
     * rest value of cityObj
     */
    reset(state, $v) {
      util.clearObject(state.cityObj);
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
        util.mapObject(response.data, state.cityObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      axios.get(`${baseUrl}/GetAll`).then(response => {
        state.cityGridData = response.data;
      });
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllDdl`).then(response => {
        state.cityDdl = response.data;
      });
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
      dispatch('validateFormStore', vm, { root: true }).then(isValid => {
        if (!isValid) return;

        axios.post(`${baseUrl}/Create`, state.cityObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
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
      dispatch('validateFormStore', vm, { root: true }).then(isValid => {
        if (!isValid) return;
        state.cityObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.cityObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('update');
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
      return state.cityObj.Name;
    }
  }
};

export default store;
