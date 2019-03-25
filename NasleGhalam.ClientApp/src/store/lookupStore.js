import axios from 'plugins/axios';
import { LOOKUP_URL as baseUrl } from 'utilities/site-config';

const store = {
  namespaced: true,
  state: {
    lookupTopicHardnessTypeDdl: [],
    lookupTopicAreaTypeDdl: [],
    lookupPrintTypeDdl: [],
    lookupBookTypeDdl: [],
    lookupPaperTypeDdl: [],
    lookupTopicHardnessType: [],
    lookupTopicAreaType: [],
    lookupQuestionType: [],
    lookupQuestionHardnessType: [],
    lookupQuestionRepeatnessType: [],
    lookupQuestionAuthorType: [],
    lookupTopicNezamDdl: [],
    lookupEducationTreeStateDdl: []
  },
  actions: {
    fillTopicHardnessTypeDdlStore({ state }) {
      if (!state.lookupTopicHardnessTypeDdl.length) {
        axios.get(`${baseUrl}/GetAllTopicHardnessTypeDdl/`).then(response => {
          state.lookupTopicHardnessTypeDdl = response.data;
        });
      }
    },
    fillTopicAreaTypeDdlStore({ state }) {
      if (!state.lookupTopicAreaTypeDdl.length) {
        axios.get(`${baseUrl}/GetAllAreaTypeDdl/`).then(response => {
          state.lookupTopicAreaTypeDdl = response.data;
        });
      }
    },
    fillPrintTypeDdlStore({ state }) {
      if (!state.lookupPrintTypeDdl.length) {
        axios.get(`${baseUrl}/GetAllPrintTypeDdl/`).then(response => {
          state.lookupPrintTypeDdl = response.data;
        });
      }
    },
    fillBookTypeDdlStore({ state }) {
      if (!state.lookupBookTypeDdl.length) {
        axios.get(`${baseUrl}/GetAllBookTypeDdl/`).then(response => {
          state.lookupBookTypeDdl = response.data;
        });
      }
    },
    fillPaperTypeDdlStore({ state }) {
      if (!state.lookupPaperTypeDdl.length) {
        axios.get(`${baseUrl}/GetAllPaperTypeDdl/`).then(response => {
          state.lookupPaperTypeDdl = response.data;
        });
      }
    },
    getLookupQuestionType({ state }, id) {
      if (!state.lookupQuestionType.length) {
        axios.get(`${baseUrl}/GetAllQuestionTypeDdl/`).then(response => {
          state.lookupQuestionType = response.data;
        });
      }
    },
    getLookupQuestionHardnessType({ state }, id) {
      if (!state.lookupQuestionHardnessType.length) {
        axios
          .get(`${baseUrl}/GetAllQuestionHardnessTypeDdl/`)
          .then(response => {
            state.lookupQuestionHardnessType = response.data;
          });
      }
    },
    getLookupQuestionRepeatnessType({ state }, id) {
      if (!state.lookupQuestionRepeatnessType.length) {
        axios.get(`${baseUrl}/GetAllRepeatnessTypeDdl/`).then(response => {
          state.lookupQuestionRepeatnessType = response.data;
        });
      }
    },
    getLookupQuestionAuthorType({ state }, id) {
      if (!state.lookupQuestionAuthorType.length) {
        axios.get(`${baseUrl}/GetAllAuthorTypeDdl/`).then(response => {
          state.lookupQuestionAuthorType = response.data;
        });
      }
    },
    fillTopicNezamStore({ state }) {
      if (!state.lookupTopicNezamDdl.length) {
        axios.get(`${baseUrl}/GetAllNezamDdl/`).then(response => {
          state.lookupTopicNezamDdl = response.data;
        });
      }
    },
    fillEducationTreeStateDdlStore({ state }) {
      if (!state.lookupEducationTreeStateDdl.length) {
        axios.get(`${baseUrl}/GetAllEducationTreeStateDdl/`).then(response => {
          state.lookupEducationTreeStateDdl = response.data;
        });
      }
    }
  }
};

export default store;
