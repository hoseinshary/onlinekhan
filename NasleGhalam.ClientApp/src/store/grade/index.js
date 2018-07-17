import util from 'utilities/util';
import gradeService from 'serviceLayers/gradeService';

export default {
  namespaced: true,
  state: {
    modelName: 'مقطع تحصیلی',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    instanceObj: {
      Id: 0,
      Name: '',
      Priority: 0
    },
    allObj: [],
    selectedIndex: -1,
    selectedId: 0,
    createVue: null,
    editVue: null
  },
  mutations: {
    /**
     * insert new instanceObj to allObj
     */
    insert(state, id) {
      let createdObj = util.cloneObject(state.instanceObj);
      createdObj.Id = id;
      state.allObj.push(createdObj);
    },

    /**
     * update instanceObj of allObj
     */
    update(state) {
      let index = state.selectedIndex;
      if (index < 0) return;
      util.mapObject(state.instanceObj, state.allObj[index]);
    },

    /**
     * delete from allObj
     */
    delete(state) {
      let index = state.selectedIndex;
      if (index < 0) return;
      state.allObj.splice(index, 1);
    },

    /**
     * rest value of instanceObj
     */
    reset(state, $v) {
      util.clearObject(state.instanceObj);
      if ($v) {
        $v.$reset();
      }
    },

    /**
     * set selectedIndex
     */
    setIndex(state) {
      state.selectedIndex = state.allObj.findIndex(
        o => o.Id == state.instanceObj.Id
      );
    }
  },
  actions: {
    /**
     * get data by id
     */
    getByIdStore({ state }, id) {
      gradeService.getById(id).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.instanceObj);
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({ state }) {
      gradeService.getAll().then(response => {
        state.allObj = response.data;
      });
    },

    //### create section ###
    /**
     * toggle modal create
     */
    toggleModalCreateStore({ state }, isOpen) {
      state.isOpenModalCreate = isOpen;
    },

    /**
     * init create validation on load
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

        gradeService.create(state.instanceObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
            commit('reset', vm.$v);
            state.isOpenModalCreate = !closeModal;
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
     * init edit validation on load
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

        state.instanceObj = state.selectedId;
        gradeService.update(state.instanceObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('setIndex');
            commit('update');
            commit('reset', vm.$v);
            state.isOpenModalEdit = false;
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
      gradeService.delete(state.selectedId).then(response => {
        let data = response.data;
        if (data.MessageType == 1) {
          commit('setIndex');
          commit('delete');
          commit('reset');
          state.isOpenModalDelete = false;
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
    },
    //------------------------------------------------

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.instanceObj.$touch();
      if (vm.$v.instanceObj.$error) {
        dispatch('notifyInvalidForm', vm, { root: true });
        return false;
      }

      return true;
    }
  },
  getters: {
    recordName(state) {
      return state.instanceObj.Name;
    }
  }
};
