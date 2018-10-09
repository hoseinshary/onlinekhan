import util from 'utilities/util';
import axios from 'utilities/axios';
import { UNIVERSITY_BRANCH_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in universityBranchGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.universityBranchGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'رشته دانشگاهی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    universityBranchObj: {
      Id: 0,
      Name: '',
      SiteAverage: 0,
      Balance1Low: 0,
      Balance1High: 0,
      Balance2Low: 0,
      Balance2High: 0,
      EducationSubGroupId: 0,
      EducationSubGroupName: ''
    },
    universityBranchIndexObj: {
      EducationGroupId: undefined
    },
    universityBranchGridData: [],
    universityBranchDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new universityBranchObj to universityBranchGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.universityBranchObj);
      createdObj.Id = id;
      state.universityBranchGridData.push(createdObj);
    },

    /**
     * update universityBranchObj of universityBranchGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.universityBranchObj,
        state.universityBranchGridData[index]
      );
    },

    /**
     * delete from universityBranchGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.universityBranchGridData.splice(index, 1);
    },

    /**
     * rest value of universityBranchObj
     */
    reset(state, $v) {
      util.clearObject(state.universityBranchObj);
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
        util.mapObject(response.data, state.universityBranchObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }, educationGroupId) {
      // get data
      axios
        .get(`${baseUrl}/GetAllByEducationGroupId/${educationGroupId}`)
        .then(response => {
          state.universityBranchGridData = response.data;
        });
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      // fill grid if modelChanged
      if (state.ddlModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.universityBranchDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.universityBranchObj.$touch();
      if (vm.$v.universityBranchObj.$error) {
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
          .post(`${baseUrl}/Create`, state.universityBranchObj)
          .then(response => {
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
        state.universityBranchObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.universityBranchObj)
          .then(response => {
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
      return state.universityBranchObj.Name;
    }
  }
};

export default store;
