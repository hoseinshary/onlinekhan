import util from "src/utilities";
import axios from 'plugins/axios';
import { EDUCATION_SUB_GROUP_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in educationSubGroupGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.educationSubGroupGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'زیر گروه آموزشی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    educationSubGroupObj: {
      Id: 0,
      Name: '',
      EducationTreeId: 0,
      EducationTreeName: ''
    },
    educationSubGroupGridData: [],
    educationSubGroupDdl: [],
    selectedId: 0,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new educationSubGroupObj to educationSubGroupGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.educationSubGroupObj);
      createdObj.Id = id;
      state.educationSubGroupGridData.push(createdObj);
    },

    /**
     * update educationSubGroupObj of educationSubGroupGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.educationSubGroupObj,
        state.educationSubGroupGridData[index]
      );
    },

    /**
     * delete from educationSubGroupGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.educationSubGroupGridData.splice(index, 1);
    },

    /**
     * rest value of educationSubGroupObj
     */
    reset(state, $v) {
      util.clearObject(state.educationSubGroupObj);
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
        util.mapObject(response.data, state.educationSubGroupObj);
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
          state.educationSubGroupGridData = response.data.map(x => ({
            Id: x.Id,
            EducationTreeId: x.EducationTreeId,
            Name: x.Name,
            EducationTreeName: x.EducationTree.Name
          }));
          state.gridModelChanged = false;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillEducationSubGroupByEducationTreeIdDdlStore({ state }, EducationTreeId) {
      // fill grid if modelChanged
      // get data
      axios
        .get(`${baseUrl}/GetAllByEducationTreeIdDdl/${EducationTreeId}`)
        .then(response => {
          state.educationSubGroupDdl = response.data;
        });
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.educationSubGroupObj.$touch();
      if (vm.$v.educationSubGroupObj.$error) {
        dispatch('notifyInvalidForm', vm, { root: true });
        return false;
      }

      return true;
    },

    /**
     * model changed
     */
    modelChangedStore({ state }) {
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

        axios
          .post(`${baseUrl}/Create`, state.educationSubGroupObj)
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
        state.educationSubGroupObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.educationSubGroupObj)
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
      return state.educationSubGroupObj.Name;
    }
  }
};

export default store;
