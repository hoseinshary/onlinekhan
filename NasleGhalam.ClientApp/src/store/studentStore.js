import util from 'utilities/util';
import axios from 'utilities/axios';
import { STUDENT_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in studentGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.studentGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'دانش آموز',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    studentObj: {
      Id: 0,
      FatherName: '',
      Address: '',
      User: {
        Id: 0,
        Name: '',
        Family: '',
        Username: '',
        Password: undefined,
        IsActive: true,
        NationalNo: '',
        Gender: '',
        Phone: '',
        Mobile: '',
        RoleId: 0,
        CityId: 0,
        ProvinceId: 0,
        RoleName: '',
        CityName: ''
      }
    },
    studentGridData: [],
    studentDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new studentObj to studentGridData
     */
    insert(state, id) {
      state.studentObj.User.GenderName = state.studentObj.User.Gender
        ? 'پسر'
        : 'دختر';
      let createdObj = util.cloneObject(state.studentObj);
      createdObj.Id = id;
      state.studentGridData.push(createdObj);
    },

    /**
     * update studentObj of studentGridData
     */
    update(state) {
      state.studentObj.User.GenderName = state.studentObj.User.Gender
        ? 'پسر'
        : 'دختر';
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.studentObj, state.studentGridData[index]);
    },

    /**
     * delete from studentGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.studentGridData.splice(index, 1);
    },

    /**
     * rest value of studentObj
     */
    reset(state, $v) {
      util.clearObject(state.studentObj);
      state.studentObj.User.IsActive = true;
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
      return axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.studentObj);
        return response.data;
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
          state.studentGridData = response.data;
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
          state.studentDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.studentObj.$touch();
      if (vm.$v.studentObj.$error) {
        dispatch('notifyInvalidForm', vm, { root: true });
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
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        axios.post(`${baseUrl}/Create`, state.studentObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
            dispatch('modelChangedStore');
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
        state.studentObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.studentObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('update');
            dispatch('modelChangedStore');
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
          dispatch('modelChangedStore');
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
      return `${state.studentObj.User.Name} ${state.studentObj.User.Family}`;
    }
  }
};

export default store;
