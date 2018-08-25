import util from 'utilities/util';
import axios from 'utilities/axios';
import { USER_URL as baseUrl } from 'utilities/site-config';
import { LocalStorage } from 'quasar';
import router from 'router';

/**
 * find index of object in userGridData by id
 * @param {Number} id
 */
function getIndexById(id) {
  return store.state.userGridData.findIndex(o => o.Id == id);
}

const store = {
  namespaced: true,
  state: {
    modelName: 'کاربر',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    userObj: {
      Id: 0,
      Name: '',
      Family: '',
      Username: '',
      Password: '',
      IsActive: '',
      NationalNo: '',
      Gender: '',
      Phone: '',
      Mobile: '',
      RoleId: 0,
      CityId: 0,
      ProvinceId: 0,
      RoleName: '',
      CityName: ''
    },
    userGridData: [],
    userDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null,
    loginObj: {
      UserName: '',
      Password: ''
    }
  },
  mutations: {
    /**
     * insert new userObj to userGridData
     */
    insert(state, id) {
      state.userObj.GenderName = state.userObj.Gender ? 'پسر' : 'دختر';
      let createdObj = util.cloneObject(state.userObj);

      createdObj.Id = id;
      state.userGridData.push(createdObj);
    },

    /**
     * update userObj of userGridData
     */
    update(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;

      state.userObj.GenderName = state.userObj.Gender ? 'پسر' : 'دختر';
      util.mapObject(state.userObj, state.userGridData[index]);
    },

    /**
     * delete from userGridData
     */
    delete(state) {
      let index = getIndexById(state.selectedId);
      if (index < 0) return;
      state.userGridData.splice(index, 1);
    },

    /**
     * rest value of userObj
     */
    reset(state, $v) {
      util.clearObject(state.userObj);
      state.userObj.IsActive = true;
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
      return axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.userObj);
        return response.data;
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
          state.userGridData = response.data;
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
          state.userDdl = response.data;
          state.ddlModelChanged = false;
        });
      }
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.userObj.$touch();
      if (vm.$v.userObj.$error) {
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

        axios.post(`${baseUrl}/Create`, state.userObj).then(response => {
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
        state.userObj.Id = state.selectedId;
        axios.post(`${baseUrl}/Update`, state.userObj).then(response => {
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
    },
    //------------------------------------------------

    /**
     * login to website
     */
    loginStore({ dispatch, state }, vm) {
      axios.post(`${baseUrl}/Login`, state.loginObj).then(response => {
        let data = response.data;

        if (data.MessageType == 1) {
          axios.defaults.headers.common['Token'] = data.Token;
          LocalStorage.set('Token', data.Token);
          LocalStorage.set('FullName', data.FullName);
          LocalStorage.set(
            'authList',
            data.SubMenus.map(x => x.EnName.toLowerCase())
          );
          LocalStorage.set('menuList', data.Menus);
          LocalStorage.set('subMenuList', data.SubMenus);
          router.push(data.DefaultPage);
        } else {
          dispatch(
            'notify',
            { body: data.Message, type: data.MessageType, vm: vm },
            { root: true }
          );
        }
      });
    }
  },
  getters: {
    recordName(state) {
      return `${state.userObj.Name} ${state.userObj.Family}`;
    }
  }
};

export default store;
