import util from 'utilities/util';
import axios from 'utilities/axios';
import { AXILLARY_BOOK_URL as baseUrl } from 'utilities/site-config';

/**
 * find index of object in axillaryBookGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.axillaryBookGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'کتاب کمک درسی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    axillaryBookObj: {
      Id: 0,
      Name: '',
      PublishYear: 0,
      Author: '',
      Isbn: '',
      Font: '',
      LookupId_PrintType: 0,
      Price: 0,
      OriginalPrice: 0,
      LookupId_BookType: 0,
      LookupId_PaperType: 0,
      Description: '',
      PublisherId: 0,
      PublisherName: '',
      BookTypeName: '',
      PaperTypeName: '',
      PrintTypeName: ''
    },
    axillaryBookGridData: [],
    axillaryBookDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new axillaryBookObj to axillaryBookGridData
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.axillaryBookObj);
      createdObj.Id = id;
      state.axillaryBookGridData.push(createdObj);
    },

    /**
     * update axillaryBookObj of axillaryBookGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      util.mapObject(state.axillaryBookObj, state.axillaryBookGridData[index]);
    },

    /**
     * delete from axillaryBookGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.axillaryBookGridData.splice(index, 1);
    },

    /**
     * rest value of axillaryBookObj
     */
    reset(state, $v) {
      util.clearObject(state.axillaryBookObj);
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
        util.mapObject(response.data, state.axillaryBookObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      // fill grid if modelChanged
      if (state.gridModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAll`).then(response => {
          state.axillaryBookGridData = response.data;
          state.gridModelChanged = false;
        });
      }
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({ state }) {
      // fill grid if modelChanged
      if (state.ddlModelChanged) {
        // get data
        axios.get(`${baseUrl}/GetAllDdl`).then(response => {
          state.axillaryBookDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.axillaryBookObj.$touch();
      if (vm.$v.axillaryBookObj.$error) {
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
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;

        var formData = new FormData();
        var fileUpload = state.createVue.$refs.fileUpload;
        //todo: check file ext
        if (fileUpload && fileUpload.files) {
          formData.append(fileUpload.name, fileUpload.files[0]);
        }

        axios({
          method: 'post',
          url: `${baseUrl}/Create?${util.toParam(state.axillaryBookObj)}`,
          data: formData,
          config: { headers: { 'Content-Type': 'multipart/form-data' } }
        }).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
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

      var fileUpload = state.createVue.$refs.fileUpload;
      if (fileUpload) {
        fileUpload.reset();
      }
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
        state.axillaryBookObj.Id = state.selectedId;
        axios
          .post(`${baseUrl}/Update`, state.axillaryBookObj)
          .then(response => {
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
          { root: true }
        );
      });
    }
    //------------------------------------------------
  },
  getters: {
    recordName(state) {
      return state.axillaryBookObj.Name;
    }
  }
};

export default store;
