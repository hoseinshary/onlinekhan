import util from 'utilities/util';
import axios from 'utilities/axios';
import { QUESTIONGROUP_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in questionGroupGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.questionGroupGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'سوال گروهی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    questionGroupObj: {
      Id: 0,
      Title: '',
      LessonId: ''
    },
    questionGroupIndexObj: {
      LessonId: 0,
      EducationTreeId_Grade: 0,
      EducationTreeIds: [],
      selectedTopicId: null
    },
    questionGroupGridData: [],
    questionGroupDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new questionGroupObj to questionGroupGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.questionGroupObj);
      createdObj.Id = id;
      state.questionGroupGridData.push(createdObj);
    },

    /**
     * update questionGroupObj of questionGroupGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.questionGroupObj,
        state.questionGroupGridData[index]
      );
    },

    /**
     * delete from questionGroupGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.questionGroupGridData.splice(index, 1);
    },

    /**
     * rest value of questionGroupObj
     */
    reset(state, $v) {
      util.clearObject(state.questionGroupObj);
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
        util.mapObject(response.data, state.questionGroupObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridByLessonIdStore({ state }, lessonId) {
      // fill grid if modelChanged
      axios.get(`${baseUrl}/GetAllByLessonId/${lessonId}`).then(response => {
        state.questionGroupGridData = response.data;
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
          state.questionGroupDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.questionGroupObj.$touch();
      if (vm.$v.questionGroupObj.$error) {
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
     * submit pre create data
     */
    submitPreCreateStore({ state, commit, dispatch }) {
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        var formData = new FormData();
        var wordFileUpload = state.createVue.$refs.wordFileUpload;
        if (wordFileUpload && wordFileUpload.files) {
          formData.append(wordFileUpload.name, wordFileUpload.files[0]);
        }

        var excelFileUpload = state.createVue.$refs.excelFileUpload;
        if (excelFileUpload && excelFileUpload.files) {
          formData.append(excelFileUpload.name, excelFileUpload.files[0]);
        }

        axios({
          method: 'post',
          url: `${baseUrl}/PreCreate?${util.toParam(state.questionGroupObj)}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        }).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            debugger;
            state.createVue.selectedTab = 'previewTab';
            state.createVue.modalSize = 'lg';
            state.createVue.previewImages = data.Obj;
            state.createVue.isPreCreate = false;
          }
        });
      });
    },

    /**
     * submit create data
     */
    submitCreateStore({ state, commit, dispatch }, closeModal) {
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        var formData = new FormData();
        var wordFileUpload = state.createVue.$refs.wordFileUpload;
        if (wordFileUpload && wordFileUpload.files) {
          formData.append(wordFileUpload.name, wordFileUpload.files[0]);
        }

        var excelFileUpload = state.createVue.$refs.excelFileUpload;
        if (excelFileUpload && excelFileUpload.files) {
          formData.append(excelFileUpload.name, excelFileUpload.files[0]);
        }

        axios({
          method: 'post',
          url: `${baseUrl}/Create?${util.toParam(state.questionGroupObj)}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        }).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            // commit('insert', data.Id);
            // dispatch('modelChangedStore');
            debugger;
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
        state.questionGroupObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.questionGroupObj)
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
      return state.questionGroupObj.Name;
    }
  }
};

export default store;
