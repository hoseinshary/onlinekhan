import axios from 'utilities/axios';
import { LOOKUP_URL as baseUrl } from 'utilities/site-config';

const store = {
  namespaced: true,
  state: {
    lookupTopicHardnessTypeDdl: [],
    lookupTopicAreaTypeDdl: [],
    lookupPrintTypeDdl: [],
    lookupBookTypeDdl: [],
    lookupPaperTypeDdl: []
    lookupTopicHardnessType: [],
    lookupTopicAreaType: [],
    lookupQuestionType: [],
    lookupQuestionHardnessType: [],
    lookupQuestionRepeatnessType: [],
    lookupQuestionAuthorType: []
  },
  actions: {
    fillTopicHardnessTypeDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllTopicHardnessTypeDdl/`).then(response => {
        state.lookupTopicHardnessTypeDdl = response.data;
      });
    },
    fillTopicAreaTypeDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllAreaTypeDdl/`).then(response => {
        state.lookupTopicAreaTypeDdl = response.data;
      });
    },
    fillPrintTypeDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllPrintTypeDdl/`).then(response => {
        state.lookupPrintTypeDdl = response.data;
      });
    },
    fillBookTypeDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllBookTypeDdl/`).then(response => {
        state.lookupBookTypeDdl = response.data;
      });
    },
    fillPaperTypeDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllPaperTypeDdl/`).then(response => {
        state.lookupPaperTypeDdl = response.data;
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
  }
};

export default store;
