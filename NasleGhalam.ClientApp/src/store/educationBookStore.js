import util from "src/utilities";
import axios from 'plugins/axios';
import { EDUCATION_BOOK_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in educationBookGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.educationBookGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'کتاب درسی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    educationBookObj: {
      Id: 0,
      Name: '',
      PublishYear: 1350,
      IsExamSource: false,
      IsActive: false,
      IsChanged: false,
      LessonName: '',
      LessonId: 0,
      TopicIds: []
    },
    educationBookIndexObj: {
      LessonId: 0,
      EducationTreeId_Grade: 0,
      EducationTreeIds: []
    },
    educationBookGridData: [],
    educationBookDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    createVue: null,
    editVue: null,
    topicFilter: {
      value: ''
    }
  },
  mutations: {
    /**
     * insert new educationBookObj to educationBookGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.educationBookObj);
      createdObj.Id = id;
      state.educationBookGridData.push(createdObj);
    },

    /**
     * update educationBookObj of educationBookGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.educationBookObj,
        state.educationBookGridData[index]
      );
    },

    /**
     * delete from educationBookGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.educationBookGridData.splice(index, 1);
    },

    /**
     * rest value of educationBookObj
     */
    reset(state, $v) {
      state.educationBookObj.Name = '';
      state.educationBookObj.PublishYear = undefined;
      state.educationBookObj.IsExamSource = false;
      state.educationBookObj.IsActive = false;
      state.educationBookObj.IsChanged = false;
      util.clearArray(state.educationBookObj.TopicIds);
      state.topicFilter.value = '';

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
        util.mapObject(response.data, state.educationBookObj);
        state.educationBookObj.TopicIds = response.data.Topics.map(x => x.Id);
        return response.data;
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }, gradeLevelId) {
      if (gradeLevelId) {
        // get data
        axios
          .get(`${baseUrl}/GetAllByLessonId/${gradeLevelId}`)
          .then(response => {
            state.educationBookGridData = response.data;
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
          state.educationBookDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.educationBookObj.$touch();
      if (vm.$v.educationBookObj.$error) {
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
          .post(`${baseUrl}/Create`, state.educationBookObj)
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
        state.educationBookObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.educationBookObj)
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
      return state.educationBookObj.Name;
    }
  }
};

export default store;
