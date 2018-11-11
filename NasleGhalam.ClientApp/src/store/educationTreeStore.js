import util from 'utilities/util';
import axios from 'utilities/axios';
import { EDUCATIONTREE_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in educationTreeData by id
 * @param {Number} id
 */
function getIndexById(children, id) {
  return children.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'درخت آموزش',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    educationTreeObj: {
      Id: 0,
      Name: '',
      LookupId_EducationTreeState: 0,
      ParentEducationTreeId: 0
    },
    educationTreeData: [],
    educationTreeDdl: [],
    educationGroupDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new educationTreeObj to educationTreeData
     */
    insertToTree(state, id) {
      let createdObj = util.cloneObject(state.educationTreeObj);
      createdObj.Id = id;
      createdObj.label = createdObj.Name;
      createdObj.children = [];

      let node = util.searchTreeArray(
        state.educationTreeData,
        'Id',
        createdObj.ParentEducationTreeId
      );

      if (node != null) node.children.push(createdObj);
      else state.educationTreeData.push(createdObj);
    },

    /**
     * update educationTreeObj of educationTreeData
     */
    updateTree(state) {
      let node = util.searchTreeArray(
        state.educationTreeData,
        'Id',
        state.educationTreeObj.Id
      );

      if (node != null) node.label = state.educationTreeObj.Name;
    },

    /**
     * delete from educationTreeData
     */
    deleteTree(state) {
      if (state.educationTreeObj.ParentEducationTreeId != null) {
        let parentNode = util.searchTreeArray(
          state.educationTreeData,
          'Id',
          state.educationTreeObj.ParentEducationTreeId
        );
        let index = getIndexById(
          parentNode.children,
          state.educationTreeObj.Id
        );
        if (index < 0) return;
        parentNode.children.splice(index, 1);
      } else {
        let index = getIndexById(
          state.educationTreeData,
          state.educationTreeObj.Id
        );
        if (index < 0) return;
        state.educationTreeData.splice(index, 1);
      }
    },

    /**
     * rest value of educationTreeObj
     */
    reset(state, $v) {
      util.clearObject(state.educationTreeObj);
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
    /**
     * get tree by state
     */
    getAllEducationTreeByState({ state }, treeState) {
      axios
        .get(`${baseUrl}/getAllEducationTreeByState/${treeState}`)
        .then(response => {
          util.mapObject(
            response.data.map(x => ({
              value: x.Id,
              label: x.Name
            })),
            state.educationTreeDdl
          );
        });
    },
    /**
     * get data by id
     */
    getByIdStore({ state }, id) {
      axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.educationTreeObj);
      });
    },

    /**
     * fill grid data
     */
    fillTreeStore({ state }) {
      // fill grid if modelChanged
      if (state.gridModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.educationTreeData = util.listToTree(
            response.data.map(x => ({
              Id: x.Id,
              LookupId_EducationTreeState: x.LookupId_EducationTreeState,
              Lookup_EducationTreeState: x.Lookup_EducationTreeState,
              label: x.Name,
              ParentEducationTreeId: x.ParentEducationTreeId
            })),
            'Id',
            'ParentEducationTreeId'
          );
          state.gridModelChanged = false;
        });
      }
    },
    /**
     * fill dropDwonListFromEducationGroups
     */
    fillEducationGroupDdlStore({ state }) {
      axios.get(`${baseUrl}/GetAllEducationGroupsDdl`).then(response => {
        state.educationGroupDdl = response.data;
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
          state.educationTreeDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.educationTreeObj.$touch();
      if (vm.$v.educationTreeObj.$error) {
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
      state.gridModelChanged = true;
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
     * submit create data
     */
    submitCreateStore({ state, commit, dispatch }, closeModal) {
      debugger;
      console.log(state);
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        axios
          .post(`${baseUrl}/Create`, state.educationTreeObj)
          .then(response => {
            let data = response.data;

            if (data.MessageType == 1) {
              debugger;
              commit('insertToTree', data.Id);
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
        state.educationTreeObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.educationTreeObj)
          .then(response => {
            let data = response.data;
            if (data.MessageType == 1) {
              commit('updateTree');
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
          commit('deleteTree');
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
      return state.educationTreeObj.Name;
    }
  }
};

export default store;
