import util from 'utilities/util';
import axios from 'utilities/axios';
import { LOOKUP_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in gradeGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.gradeGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    lookupTopicHardnessType: [],
    lookupTopicAreaType: [],
    lookupQuestionType: [],
    lookupQuestionHardnessType: [],
    lookupQuestionRepeatnessType: [],
    lookupQuestionAuthorType: []
  },
  mutations: {},
  actions: {
    getLookupTopicHardnessType({ state }, id) {
      axios.get(`${baseUrl}/GetAllTopicHardnessType/`).then(response => {
        state.lookupTopicHardnessType = response.data;
      });
    },
    getLookupTopicAreaType({ state }, id) {
      axios.get(`${baseUrl}/GetAllAreaType/`).then(response => {
        state.lookupTopicAreaType = response.data;
      });
    },
    getLookupQuestionType({ state }, id) {
      axios.get(`${baseUrl}/GetAllQuestionType/`).then(response => {
        state.lookupQuestionType = response.data;
      });
    },
    getLookupQuestionHardnessType({ state }, id) {
      axios.get(`${baseUrl}/GetAllQuestionHardnessType/`).then(response => {
        state.lookupQuestionHardnessType = response.data;
      });
    },
    getLookupQuestionRepeatnessType({ state }, id) {
      axios.get(`${baseUrl}/GetAllRepeatnessType/`).then(response => {
        state.lookupQuestionRepeatnessType = response.data;
      });
    },
    getLookupQuestionAuthorType({ state }, id) {
      axios.get(`${baseUrl}/GetAllAuthorType/`).then(response => {
        state.lookupQuestionAuthorType = response.data;
      });
    }
  },
  getters: {}
};

export default store;
