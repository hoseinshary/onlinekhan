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
    }
  }
};

export default store;
