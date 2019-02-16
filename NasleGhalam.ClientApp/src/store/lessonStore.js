import util from 'utilities/util';
import axios from 'utilities/axios';
import {
  LESSON_URL as baseUrl,
  EDUCATION_GROUP_URL
} from 'utilities/site-config';

export default {
  namespaced: true,
  state: {
    modelName: 'درس',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    instanceObj: {
      Id: 0,
      Name: '',
      IsMain: undefined,
      EducationGroups: [],
      EducationTreeIds: [],
      LookupId_Nezam: 0,
      GradeId: 0,
      GradeLevelId: 0,
      TreeId_Grade: 0,
      EducationTrees:[],
      Ratios:[]
    },
    allObj: [],
    // selectedNodeIds: [],
    educationGroupList: [],
    allObjDdl: [],
    selectedIndex: -1,
    selectedId: 0,
    createVue: null,
    editVue: null,
    EducationGroups: [],
    educationGroup_LessonDdl: []
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
      var xx = state.instanceObj.GradeId;
      util.clearObject(state.instanceObj);
      state.EducationGroups.filter(x => x.IsChecked).forEach(element => {
        element.IsChecked = false;
        element.SubGroups.forEach(item => {
          item.Rate = 0;
        });
      });
      if ($v) {
        $v.$reset();
      }
      state.instanceObj.GradeId = xx;
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
    getByIdStore({
      state
    }, id) {
      axios.get(`${baseUrl}/GetById/${id}`).then(response => {
        state.selectedId = id;
        util.mapObject(response.data, state.instanceObj);
        if (state.isOpenModalEdit) {
          state.instanceObj.EducationTreeIds = response.data.EducationTrees.map(x => x.Id);
        }
      });
    },

    /**
     * fill grid data
     */
    fillGridStore({
      state
    }, lstIds) {
      axios.get(`${baseUrl}/GetAllByEducationTreeIds/?`+ util.toParam({Ids:lstIds})).then(response => {
        state.allObj = response.data;
      });
    },

    /**
     * fill dropDwonList lesson by educationGroupId
     */
    fillLessonByEducationGroupDdlStore({
      state
    }, educationGroupId) {
      axios
        .get(`${baseUrl}/GetAllByEducationGroupIdDdl/${educationGroupId}`)
        .then(response => {
          state.educationGroup_LessonDdl = response.data;
        });
    },

    /**
     * fill dropDwonList
     */
    fillDdlStore({
      state
    }, ids) {
      axios
        .get(
          `${baseUrl}/GetAllByEducationTreeIds?${util.toParam({
            ids: ids
          })}`
        )
        .then(response => {
          state.allObjDdl = response.data.map(x => ({
            value: x.Id,
            label: x.Name
          }));
        });
    },
    /**
     * vlidate form
     */
    validateFormStore({
      state,
      dispatch
    }, vm) {
      var flag = true;
      var message = '';
      state.EducationGroups.filter(x => x.IsChecked).forEach(groups => {
        groups.SubGroups.forEach(group => {
          if (group.Rate < 0 || group.Rate > 10) {
            flag = false;
            message += group.Name + ' و ';
          }
        });
      });

      if (!flag) {
        var cnt = message.split(' و ').length;
        dispatch(
          'notify', {
            body: message.substring(0, message.length - 2) +
              '، باید بین 0 تا 10 ' +
              (cnt > 2 ? 'باشند.' : 'باشد.'),
            type: 0,
            vm: vm
          }, {
            root: true
          }
        );
      }

      vm.$v.instanceObj.$touch();
      if (vm.$v.instanceObj.$error) {
        dispatch('notifyInvalidForm', vm, {
          root: true
        });
        flag = false;
      }

      return flag;
    },

    /**
     * fill dropDwon EduGroupAndEduSubGroup
     */
    getAllEduGroupAndEduSubGroupStore({
      state
    }) {
      axios
        .get(`${EDUCATION_GROUP_URL}/GetAllEducationWithSubGroups`) // todo: چرا از استور گروه آموزشی استفاده نشده؟
        .then(response => {
          state.EducationGroups = response.data;
        });
    },
    setEducationGroupListLesson({
      state
    }, lstEducationGroups) {

      state.EducationGroupListLesson = lstEducationGroups;
    },

    //### create section ###
    /**
     * toggle modal create
     */
    toggleModalCreateStore({
      state
    }, isOpen) {
      state.isOpenModalCreate = isOpen;
    },

    /**
     * init create vue on load
     */
    createVueStore({
      state
    }, vm) {
      state.createVue = vm;
    },

    /**
     * submit create data
     */
    submitCreateStore({
      state,
      commit,
      dispatch
    }, closeModal) {
      var vm = state.createVue;
      dispatch('validateFormStore', vm).then(isValid => {

        if (!isValid) return;
        state.instanceObj.EducationGroups = state.EducationGroups.filter(
          x => x.IsChecked
        );
        state.instanceObj.Ratios = [];
        state.EducationGroupListLesson.forEach(element => {
          element.SubGroups.filter(x => x.Rate != undefined).forEach(item => {
            state.instanceObj.Ratios.push({
              EducationSubGroupId: item.Id,
              Rate: item.Rate
            })
          });
        });
        delete state.instanceObj.TreeId_Grade;
        delete state.instanceObj.GradeId;
        delete state.instanceObj.EducationGroups;
        axios.post(`${baseUrl}/Create`, state.instanceObj).then(response => {
          let data = response.data;

          if (data.MessageType == 1) {
            commit('insert', data.Id);
            dispatch('resetCreateStore');
            dispatch('toggleModalCreateStore', !closeModal);
          }

          dispatch(
            'notify', {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            }, {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset create vue
     */
    resetCreateStore({
      state,
      commit
    }) {
      commit('reset', state.createVue.$v);
    },
    //------------------------------------------------

    //### edit section ###
    /**
     * toggle modal edit
     */
    toggleModalEditStore({
      state
    }, isOpen) {
      state.isOpenModalEdit = isOpen;
    },

    /**
     * init edit vue on load
     */
    editVueStore({
      state
    }, vm) {
      state.editVue = vm;
    },

    /**
     * submit edit data
     */
    submitEditStore({
      state,
      commit,
      dispatch
    }) {
      var vm = state.editVue;
      dispatch('validateFormStore', vm).then(isValid => {
        if (!isValid) return;
        state.instanceObj.Id = state.selectedId;
        state.instanceObj.EducationGroups = state.EducationGroups.filter(
          x => x.IsChecked
        );
        state.instanceObj.Ratios = [];
        state.EducationGroupListLesson.filter(x => x.IsChecked).forEach(element => {
          element.SubGroups.filter(x => x.Rate != undefined).forEach(item => {
            state.instanceObj.Ratios.push({
              EducationSubGroupId: item.Id,
              Rate: item.Rate,
              LessonId: state.instanceObj.Id
            })
          });
        });
        delete state.instanceObj.TreeId_Grade;
        delete state.instanceObj.GradeId;
        delete state.instanceObj.EducationGroups;

        axios.post(`${baseUrl}/Update`, state.instanceObj).then(response => {
          let data = response.data;
          if (data.MessageType == 1) {
            commit('setIndex');
            commit('update');
            dispatch('resetEditStore');
            dispatch('toggleModalEditStore', false);
          }

          dispatch(
            'notify', {
              body: data.Message,
              type: data.MessageType,
              vm: vm
            }, {
              root: true
            }
          );
        });
      });
    },

    /**
     * reset edit vue
     */
    resetEditStore({
      state,
      commit
    }) {
      commit('reset', state.editVue.$v);
    },
    //------------------------------------------------

    //### delete section ###
    /**
     * toggle modal delete
     */
    toggleModalDeleteStore({
      state
    }, isOpen) {
      state.isOpenModalDelete = isOpen;
    },

    /**
     * submit to delete data
     */
    submitDeleteStore({
      state,
      commit,
      dispatch
    }, vm) {
      axios.post(`${baseUrl}/Delete/${state.selectedId}`).then(response => {
        let data = response.data;
        if (data.MessageType == 1) {
          commit('setIndex');
          commit('delete');
          commit('reset');
          dispatch('toggleModalDeleteStore', false);
        }

        dispatch(
          'notify', {
            body: data.Message,
            type: data.MessageType,
            vm: vm
          }, {
            root: true
          }
        );
      });
    }
    //------------------------------------------------
  },
  getters: {
    recordName(state) {
      return state.instanceObj.Name;
    }
  }
};
