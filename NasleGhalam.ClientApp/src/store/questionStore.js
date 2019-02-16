import util from 'utilities/util';
import axios from 'utilities/axios';
import {
  QUESTION_URL as baseUrl,
  TAG_URL as tagUrl
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
    questionObj: {
      Id: 0,
      Context: '',
      QuestionNumber: 0,
      LookupId_QuestionType: 0,
      QuestionPoint: 0,
      LookupId_QuestionHardnessType: 0,
      LookupId_RepeatnessType: 0,
      UseEvaluation: false,
      IsStandard: false,
      LookupId_AuthorType: 0,
      AuthorName: '',
      LookupId_AreaType: 0,
      ResponseSecond: 0,
      Description: '',
      FileName: '',
      InsertDateTime: '',
      UserId: 0,
      File: '',
      // EducationGroupId: 0,
      // EducationGroup_LessonId: 0,
      // EducationTreeId_Grade: 0,
      // LessonId: 0,
      IndexTopicsId: [],
      TopicsId: [],
      EducationTreeIds: [],
      AnswerNumber: 0,
      TagsId: []
    },
    questionGridData: [],
    questionDdl: [],
    tagDdl: [],
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
      createdObj.FileName = data.Obj.FileName;
      createdObj.Context = data.Obj.Context;
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
      util.clearObject(state.questionObj);
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
    /**
     * get data by id
     */
    getByIdStore({
      state
    }, id) {
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
    fillGridStore({
      state
    }) {
      // fill grid if modelChanged
      // if (state.gridModelChanged) {
      // get data
      axios.get(`${baseUrl}/GetAllByTopicIds?` + util.toParam({
        Ids: state.questionObj.IndexTopicsId
      })).then(response => {
          state.questionGridData = response.data;
          state.gridModelChanged = false;
        });
      // }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({
      state
    }) {
      // fill grid if modelChanged
      if (state.ddlModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.questionDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },
    fillTagsDdlStore({
      state
    }) {
        axios.get(`${tagUrl}/GetAll`).then(response => {
          state.tagDdl = response.data.map(x => ({
            value: x.Id,
            label: x.Name
          }));
        });
    },

    /**
     * vlidate form
     */
    validateFormStore({
      dispatch
    }, vm) {
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
    modelChangedStore({
      state
    }) {
      state.ddlModelChanged = true;
      state.gridModelChanged = true;
    },

    //### create section ###
    /**
     * toggle modal create
     */
    toggleModalCreateStore({
      state
    }, isOpen) {
      state.isOpenModalCreate = isOpen;
    },

    /**
     * init create vue on load
     */
    createVueStore({
      state
    }, vm) {
      state.createVue = vm;
    },

    /**
     * submit create data
     */
    submitCreateStore({
      state,
      commit,
      dispatch
    }, closeModal) {
      if (state.questionObj.TopicsId.length == 0 || state.questionObj.File == '') {
        var msg = '';
        msg += (state.questionObj.TopicsId.length == 0 ?'<div class="snotifyToast__body">مبحثی انتخاب نکرده اید.</div><br/>':'')
        msg += (state.questionObj.File == '' ?'<div class="snotifyToast__body">فایلی انتخاب نکرده اید.</div>':'')

        state.createVue.$snotify.html(msg, {
          type: 'error',
          timeout: 4000,
          showProgressBar: true,
          position: 'leftTop'
        });
        return
      }

      state.questionObj.UserId = 0;
      delete state.questionObj.Context;
      state.questionObj.FileName = '1';
      state.questionObj.InsertDateTime = new Date().toLocaleString();;

      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        var formData = new FormData();
        var fileUpload = state.questionObj.File;
        if (fileUpload && fileUpload.size > 0) {
          formData.append("word", fileUpload);
        }
        var tmp1 = util.toParam(state.questionObj);
        axios({
          method: 'post',
          url: `${baseUrl}/Create?${tmp1}`,
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
            'notify', {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            }, {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset create vue
     */
    resetCreateStore({
      state,
      commit
    }) {
      commit('reset', state.createVue.$v);
    },
    //------------------------------------------------

    //### edit section ###
    /**
     * toggle modal edit
     */
    toggleModalEditStore({
      state
    }, isOpen) {
      state.isOpenModalEdit = isOpen;
    },

    /**
     * init edit vue on load
     */
    editVueStore({
      state
    }, vm) {
      state.editVue = vm;
    },

    /**
     * submit edit data
     */
    submitEditStore({
      state,
      commit,
      dispatch
    }) {
      if (state.questionObj.TopicsId.length == 0) {
        state.createVue.$snotify.html('<div class="snotifyToast__body">مبحثی انتخاب نکرده اید.</div>', {
          type: 'error',
          timeout: 4000,
          showProgressBar: true,
          position: 'leftTop'
        });
        return
      }
      var vm = state.editVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;
        state.questionObj.Id = state.selectedId;

        
        var formData = new FormData();
        var fileUpload = state.questionObj.File;
        if (fileUpload && fileUpload.size > 0) {
          formData.append("word", fileUpload);
        }
      delete state.questionObj.Context;
      var tmp1 = util.toParam(state.questionObj);
      axios({
          method: 'post',
          url: `${baseUrl}/Update?${tmp1}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        })



        // axios.post(`${baseUrl}/Update`, state.questionObj)
        .then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('update');
            dispatch('modelChangedStore');
            dispatch('resetEditStore');
            dispatch('toggleModalEditStore', false);
          }

          dispatch(
            'notify', {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            }, {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset edit vue
     */
    resetEditStore({
      state,
      commit
    }) {
      commit('reset', state.editVue.$v);
    },
    //------------------------------------------------

    //### delete section ###
    /**
     * toggle modal delete
     */
    toggleModalDeleteStore({
      state
    }, isOpen) {
      state.isOpenModalDelete = isOpen;
    },

    /**
     * submit to delete data
     */
    submitDeleteStore({
      state,
      commit,
      dispatch
    }, vm) {
      axios.post(`${baseUrl}/Delete/${state.selectedId}`).then(response => {
        let data = response.data;
        if (data.MessageType == 1) {
          commit('delete');
          commit('reset');
          dispatch('modelChangedStore');
          dispatch('toggleModalDeleteStore', false);
        }

        dispatch(
          'notify', {
            body: data.Message,
            type: data.MessageType,
            vm: vm
          }, {
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
    }
  }
};

export default store;
