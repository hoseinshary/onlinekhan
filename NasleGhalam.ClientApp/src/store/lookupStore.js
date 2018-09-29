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
    }
  }
};

export default store;
