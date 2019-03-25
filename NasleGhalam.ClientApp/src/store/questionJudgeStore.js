import util from "src/utilities";
import axios from 'utilities/axios';
import { QUESTION_JUDGE_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in questionJudgeGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.questionJudgeGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'ارزیابی سوال',
    isOpenModal: false,
    questionJudgeObj: {
      Id: 0,
      IsStandard: false,
      IsDelete: false,
      IsUpdate: false,
      IsLearning: false,
      ResponseSecond: 0,
      LookupId_RepeatnessType: 0,
      LookupId_QuestionHardnessType: 0,
      QuestionId: 0
    },
    questionJudgeGridData: [],
    selectedId: 0,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new questionJudgeObj to questionJudgeGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.questionJudgeObj);
      createdObj.Id = id;
      state.questionJudgeGridData.push(createdObj);
    },

    /**
     * update questionJudgeObj of questionJudgeGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(
        state.questionJudgeObj,
        state.questionJudgeGridData[index]
      );
    },

    /**
     * delete from questionJudgeGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.questionJudgeGridData.splice(index, 1);
    },

    /**
     * rest value of questionJudgeObj
     */
    reset(state, $v) {
      util.clearObject(state.questionJudgeObj);
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
        util.mapObject(response.data, state.questionJudgeObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }, questionId) {
      return axios
        .get(`${baseUrl}/GetAllByQuestionId/${questionId}`)
        .then(response => {
          state.questionJudgeGridData = response.data;
        });
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.questionJudgeObj.$touch();
      if (vm.$v.questionJudgeObj.$error) {
        dispatch('notifyInvalidForm', vm, { root: true });
        return false;
      }

      return true;
    },

    /**
     * toggle modal
     */
    toggleModalStore({ state }, data) {
      state.isOpenModal = data.isOpen;
      state.questionJudgeObj.QuestionId = data.questionId;
    },

    //### create section ###
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
          .post(`${baseUrl}/Create`, state.questionJudgeObj)
          .then(response => {
            let data = response.data;

            if (data.MessageType == 1) {
              commit('insert', data.Id);
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
        state.questionJudgeObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.questionJudgeObj)
          .then(response => {
            let data = response.data;
            if (data.MessageType == 1) {
              commit('update');
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
     * submit to delete data
     */
    submitDeleteStore({ state, commit, dispatch }, vm) {
      axios.post(`${baseUrl}/Delete/${state.selectedId}`).then(response => {
        let data = response.data;
        if (data.MessageType == 1) {
          commit('delete');
          commit('reset');
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
      return state.questionJudgeObj.Name;
    }
  }
};

export default store;
