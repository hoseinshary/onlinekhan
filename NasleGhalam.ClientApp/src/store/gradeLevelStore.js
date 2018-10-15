import util from 'utilities/util';
import axios from 'utilities/axios';
import { GRADE_LEVEL_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in gradeLevelGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.gradeLevelGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'پایه تحصیلی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    gradeLevelObj: {
      Id: 0,
      Name: '',
      Priority: 0,
      GradeId: 0,
      GradeName: ''
    },
    gradeLevelGridData: [],
    gradeLevelDdl: [],
    gradeLevelByGradeDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new gradeLevelObj to gradeLevelGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.gradeLevelObj);
      createdObj.Id = id;
      state.gradeLevelGridData.push(createdObj);
    },

    /**
     * update gradeLevelObj of gradeLevelGridData
     */
    update(state) {
      var index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.gradeLevelObj, state.gradeLevelGridData[index]);
    },

    /**
     * delete from gradeLevelGridData
     */
    delete(state) {
      let index = getIndexById(state.gradeLevelObj.Id);
      if (index < 0) return;
      state.gradeLevelGridData.splice(index, 1);
    },

    /**
     * rest value of gradeLevelObj
     */
    reset(state, $v) {
      util.clearObject(state.gradeLevelObj);
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
    /**
     * get data by id
     */
    getByIdStore({ state, dispatch }, id) {
      axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.gradeLevelObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      // get data if model changed
      if (state.gridModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.gradeLevelGridData = response.data;
          state.gridModelChanged = false;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      // get data if model changed
      if (state.ddlModelChanged) {
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.gradeLevelDdl = response.data.map(x => ({
            label: x.Name,
            value: x.Id,
            gradeId: x.GradeId
          }));
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     *  fill dropDwonList gradeLevelByGradeId
     * @param {*} param0
     * @param {Number} gradeId
     */
    fillGradeLevelByGradeId({ state }, gradeId) {
      // get data
      if (gradeId) {
        axios.get(`${baseUrl}/GetAllByGradeIdDdl/${gradeId}`).then(response => {
          state.gradeLevelByGradeDdl = response.data;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.gradeLevelObj.$touch();
      if (vm.$v.gradeLevelObj.$error) {
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

        axios.post(`${baseUrl}/Create`, state.gradeLevelObj).then(response => {
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
        state.gradeLevelObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.gradeLevelObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('update');
            dispatch('resetEditStore');
            dispatch('modelChangedStore');
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
      return state.gradeLevelObj.Name;
    }
  }
};

export default store;
