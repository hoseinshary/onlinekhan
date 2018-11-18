import util from 'utilities/util';
import axios from 'utilities/axios';
import { TOPIC_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in topicTreeData by id
 * @param {Number} id
 */
function getIndexById(arr, id) {
  return arr.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'مبحث',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    topicObj: {
      Id: 0,
      Title: '',
      ExamStock: 0,
      Importance: 0,
      IsExamSource: false,
      LookupId_HardnessType: 0,
      LookupId_AreaType: 0,
      IsActive: false,
      ParentTopicId: 0,
      LessonId: 0
    },
    topicIndexObj: {
      LessonId: 0,
      EducationTreeId_Grade: 0,
      EducationTreeIds: [],
      selectedTopicId: null
    },
    topicTreeData: [],
    topicDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new topicObj to topicTreeData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.topicObj);
      createdObj.Id = id;
      let node = util.searchTreeArray(
        state.topicTreeData,
        'Id',
        createdObj.ParentTopicId
      );
      debugger;
      if (!node) {
        node = {
          Id: createdObj.Id,
          label: createdObj.Title,
          ParentTopicId: createdObj.ParentTopicId,
          header: 'custome',
          visible: false,
          children: []
        };
        state.topicTreeData.push(node);
      } else {
        node.children.push({
          Id: createdObj.Id,
          label: createdObj.Title,
          ParentTopicId: createdObj.ParentTopicId,
          header: 'custome',
          visible: false,
          children: []
        });
      }

      state.topicIndexObj.selectedTopicId = null;
    },

    /**
     * update topicObj of topicTreeData
     */
    update(state) {
      let node = util.searchTreeArray(
        state.topicTreeData,
        'Id',
        state.selectedId
      );
      if (!node) return;
      node.label = state.topicObj.Title;
    },

    /**
     * delete from topicTreeData
     */
    delete(state) {
      let index = 0;
      let parentNode = util.searchTreeArray(
        state.topicTreeData,
        'Id',
        state.topicObj.ParentTopicId
      );
      debugger;
      // if has parentNode
      if (parentNode) {
        index = getIndexById(parentNode.children, state.selectedId);
        if (index < 0) return;
        parentNode.children.splice(index, 1);
      } else {
        // if has not parentNode
        index = getIndexById(state.topicTreeData, state.selectedId);
        if (index < 0) return;
        state.topicTreeData.splice(index, 1);
      }
    },

    /**
     * rest value of topicObj
     */
    reset(state, $v) {
      // util.clearObject(state.topicObj);
      state.topicObj.Title = '';
      state.topicObj.ExamStock = 0;
      state.topicObj.Importance = 0;
      state.topicObj.IsExamSource = undefined;
      state.topicObj.LookupId_HardnessType = 0;
      state.topicObj.LookupId_AreaType = 0;
      state.topicObj.IsActive = true;
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
    /**
     * clear topic tree
     */
    clearTreeStore({ state }) {
      let len = state.topicTreeData.length;
      state.topicTreeData.splice(0, len);
    },

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
     * fill tree data
     */
    fillTreeStore({ state }, lessonId) {
      // get tree
      axios.get(`${baseUrl}/GetAllByLessonId/${lessonId}`).then(response => {
        let res = response.data.map(x => ({
          Id: x.Id,
          label: x.Title,
          ParentTopicId: x.ParentTopicId,
          header: 'custome',
          visible: false
        }));
        state.topicTreeData = util.listToTree(res, 'Id', 'ParentTopicId');
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
        state.topicObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.topicObj).then(response => {
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
      return state.topicObj.Title;
    }
  }
};

export default store;
