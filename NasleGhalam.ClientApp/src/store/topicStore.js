import util from 'utilities/util';
import axios from 'utilities/axios';
import { TOPIC_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in topicGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.topicGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'مبحث',
    isOpenModalCreate: false,
    isOpenModalDetails: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    topicObj: {
      Id: 0,
      Title: '',
      ExamStock: 0,
      ExamStockSystem: 0,
      Importance: 0,
      IsExamSource: false,
      LookupId_HardnessType: 0,
      LookupId_AreaType: 0,
      IsActive: false,
      ParentTopicId: 0,
      EducationGroup_LessonId: 0,
      EducationGroupId: 0
    },
    moduleDdl: [],
    treeLst: [],
    controllerDdl: [],
    topicGridData: [],
    topicDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new topicObj to topicGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.topicObj);
      createdObj.Id = id;
      state.topicGridData.push(createdObj);
    },

    /**
     * update topicObj of topicGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.topicObj, state.topicGridData[index]);
    },

    /**
     * delete from topicGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.topicGridData.splice(index, 1);
    },

    /**
     * rest value of topicObj
     */
    reset(state, $v) {
      util.clearObject(state.topicObj);
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
        util.mapObject(response.data, state.topicObj);
      });
    },

    /**
     * get data by id
     */
    GetAllTreeStore({ state }, id) {
      return axios.get(`${baseUrl}/GetAllTree/${id}`).then(response => {
        state.treeLst = [response.data];
      });
    },
    change({ state }, id) {},

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      // fill grid if modelChanged
      if (state.gridModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.topicGridData = response.data;
          state.gridModelChanged = false;
        });
      }
    },
    setLessonIdQndParentIdStore({ state }, obj) {
      state.topicObj.EducationGroup_LessonId = obj.lessonid;
      state.topicObj.ParentTopicId = obj.parentId;
    },
    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      // fill grid if modelChanged
      if (state.ddlModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.topicDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.topicObj.$touch();
      if (vm.$v.topicObj.$error) {
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

        axios.post(`${baseUrl}/Create`, state.topicObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            dispatch('GetAllTreeStore', state.topicObj.EducationGroup_LessonId);
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
    toggleModalDetailsStore({ state }, isOpen) {
      state.isOpenModalDetails = isOpen;
    },
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
        state.topicObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.topicObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            dispatch('GetAllTreeStore', state.topicObj.EducationGroup_LessonId);
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
      return state.topicObj.Name;
    }
  }
};

export default store;
