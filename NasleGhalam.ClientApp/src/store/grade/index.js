import util from 'utilities/util';
import gradeService from 'serviceLayers/gradeService';

export default {
  namespaced: true,
  state: {
    modelName: 'مقطع تحصیلی',
    isOpenModalCreate: false,
    instanceObj: {
      Id: 0,
      Name: '',
      Priority: ''
    },
    allObj: []
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
    update() {},

    /**
     * delete from allObj
     */
    delete() {},

    /**
     * rest value of instanceObj
     */
    reset(state, $v) {
      util.clearObject(state.instanceObj);
      if ($v) {
        $v.$reset();
      }
    }
  },
  actions: {
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
    },

    /**
     * reset form
     */
    resetStore({ commit }, $v) {
      commit('reset', $v);
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
     * open modal create
     */
    openModalCreateStore({ state }) {
      state.isOpenModalCreate = true;
    },

    /**
     * close modal create
     */

    closeModalCreateStore({ state }) {
      state.isOpenModalCreate = false;
    },

    /**
     * submit create data
     */
    submitCreateStore({ state, commit, dispatch }, submitedData) {
      dispatch('validateFormStore', submitedData.vm).then(isValid => {
        if (!isValid) return;

        gradeService.create(state.instanceObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
            commit('reset', submitedData.vm.$v);
            state.isOpenModalCreate = !submitedData.closeModal;
          }

          dispatch(
            'notify',
            {
              body: data.Message,
              type: data.MessageType,
              vm: submitedData.vm
            },
            { root: true }
          );
        });
      });
    }
    //------------------------------------------------
  }
  //   getters: {
  //     getCount: state => {
  //       console.log('getCount:')
  //       console.log(state)
  //       return state.count
  //     }
  //   }
};
