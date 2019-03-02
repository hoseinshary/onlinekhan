import util from 'utilities/util';
import axios from 'utilities/axios';
import {
  API_URL as apiUrl,
  QUESTION_URL as baseUrl
} from 'utilities/site-config';

/**
 * find index of object in questionGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.questionGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'سوال تکی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    isOpenModalQuestions: false,
    questionObj: {
      Id: 0,
      Context: '',
      FileName: '',
      QuestionNumber: 0,
      AnswerNumber: 0,
      QuestionPoint: 0,
      UseEvaluation: false,
      IsStandard: false,
      AuthorName: '',
      ResponseSecond: 0,
      Description: '',
      LookupId_QuestionType: 0,
      LookupId_QuestionHardnessType: 0,
      LookupId_RepeatnessType: 0,
      LookupId_AuthorType: 0,
      LookupId_AreaType: 0,
      TopicsId: [],
      EducationTreeIds: [],
      TagsId: []
    },
    questionIndexObj: {
      EducationTreeId_Grade: 0,
      LessonId: 0,
      TickedEducationTreeIds: [],
      TickedTopicsIds: []
    },
    questionGridData: [],
    questionDdl: [],
    questionByQuestionGroupIdData: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new questionObj to questionGridData
     */
    insert(state, data) {
      let createdObj = util.cloneObject(state.questionObj);
      createdObj.Id = data.Id;
      createdObj.Context = data.Context;
      state.questionGridData.push(createdObj);
    },

    /**
     * update questionObj of questionGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.questionObj, state.questionGridData[index]);
    },

    /**
     * delete from questionGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.questionGridData.splice(index, 1);
    },

    /**
     * rest value of questionObj
     */
    reset(state, $v) {
      state.questionObj.Context = '';
      state.questionObj.FileName = '';
      state.questionObj.QuestionNumber = 0;
      state.questionObj.AnswerNumber = 0;
      state.questionObj.QuestionPoint = 0;
      state.questionObj.UseEvaluation = false;
      state.questionObj.IsStandard = false;
      state.questionObj.AuthorName = '';
      state.questionObj.ResponseSecond = 0;
      state.questionObj.Description = '';
      state.questionObj.LookupId_QuestionType = 7;
      state.questionObj.LookupId_QuestionHardnessType = 12;
      state.questionObj.LookupId_RepeatnessType = 22;
      state.questionObj.LookupId_AuthorType = 1039;
      state.questionObj.LookupId_AreaType = 1036;
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
        util.mapObject(response.data, state.questionObj);
        state.questionObj.TagsId = response.data.Tags.map(x => x.Id);
        state.questionObj.TopicsId = response.data.Topics.map(x => x.Id);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }, topicsIds) {
      axios
        .get(
          `${baseUrl}/GetAllByTopicIds?` +
            util.toParam({
              Ids: topicsIds
            })
        )
        .then(response => {
          state.questionGridData = response.data;
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
          state.questionDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.questionObj.$touch();
      if (vm.$v.questionObj.$error) {
        dispatch('notifyInvalidForm', vm, {
          root: true
        });
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

    //### questionGroup section ###
    /**
     * get question by questionGroupId
     */
    getByQuestionGroupIdStore({ state }, questionGroupId) {
      return axios
        .get(`${baseUrl}/GetAllByQuestionGroupId/${questionGroupId}`)
        .then(response => {
          state.questionByQuestionGroupIdData = response.data;
        });
    },

    toggleModalQuestionStore({ state }, isOpen) {
      state.isOpenModalQuestions = isOpen;
    },

    resetQuestionStore({ state }) {
      state.questionByQuestionGroupIdData = [];
    },
    //------------------------------------------------

    //### create section ###
    /**
     * toggle modal create
     */
    toggleModalCreateStore({ state }, isOpen) {
      state.isOpenModalCreate = isOpen;
      state.questionObj.TopicsId = util.cloneObject(
        state.questionIndexObj.TickedTopicsIds
      );
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
      var wordFile = state.createVue.$refs.wordFile;
      var msg = '';

      if (wordFile.files.length == 0) {
        msg = 'فایل ورد انتخاب نشده است.';
      }
      if (state.questionObj.TopicsId.length == 0) {
        msg = 'مبحثی انتخاب نکرده اید.';
      }
      if (msg) {
        dispatch(
          'notify',
          {
            body: msg,
            type: 0,
            vm: vm
          },
          {
            root: true
          }
        );
        return;
      }

      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        var formData = new FormData();
        formData.append(wordFile.name, wordFile.files[0]);

        var params = util.toParam(state.questionObj);
        axios({
          method: 'post',
          url: `${baseUrl}/Create?${params}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        }).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data);
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
            {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset create vue
     */
    resetCreateStore({ state, commit }) {
      commit('reset', state.createVue.$v);

      var wordFile = state.createVue.$refs.wordFile;
      wordFile.reset();
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
      var wordFile = state.editVue.$refs.wordFile;
      var msg = '';

      if (wordFile.files.length == 0) {
        msg = 'فایل ورد انتخاب نشده است.';
      }
      if (state.questionObj.TopicsId.length == 0) {
        msg = 'مبحثی انتخاب نکرده اید.';
      }
      if (msg) {
        dispatch(
          'notify',
          {
            body: msg,
            type: 0,
            vm: vm
          },
          {
            root: true
          }
        );
        return;
      }

      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;
        state.questionObj.Id = state.selectedId;

        var formData = new FormData();
        formData.append(wordFile.name, wordFile.files[0]);

        var params = util.toParam(state.questionObj);
        axios({
          method: 'post',
          url: `${baseUrl}/Update?${params}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        }).then(response => {
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
            {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset edit vue
     */
    resetEditStore({ state, commit }) {
      commit('reset', state.editVue.$v);

      var wordFile = state.createVue.$refs.wordFile;
      wordFile.reset();
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
          {
            root: true
          }
        );
      });
    }
    //------------------------------------------------
  },
  getters: {
    recordName(state) {
      return state.questionObj.Context;
    },
    questionFilePath() {
      return `${apiUrl}${baseUrl}/GetPictureFile`;
    }
  }
};

export default store;
