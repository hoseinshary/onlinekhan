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
    lookupTopicAreaType: []
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
    }
  },
  getters: {}
};

export default store;
