export default {
  namespaced: true,
  strict: true,
  state: {
    modelName: 'کاربر',
    isOpenModalCreate: false,
    instance: {
      id: 0,
      name: '',
      family: '',
      age: '',
      gender: '',
      isActive: false,
      children: [],
      roleId: ''
    },
    allInstance: [
      {
        id: 0,
        name: 'علیرضا',
        family: 'اعتمادی',
        age: '17',
        gender: '1',
        isActive: false,
        children: [],
        roleId: '3'
      }
    ]
  },
  mutations: {
    create(state) {
      state.instance.id++;
      let newObj = JSON.parse(JSON.stringify(state.instance));
      state.allInstance.push(newObj);
    },
    openModalCreate(state) {
      state.isOpenModalCreate = true;
    },
    closeModalCreate(state) {
      state.isOpenModalCreate = false;
    }
  }
  //,
  //   actions: {
  //     create(context) {
  //       context.commit('create')
  //     }
  //   },
  //   getters: {
  //     getCount: state => {
  //       console.log('getCount:')
  //       console.log(state)
  //       return state.count
  //     }
  //   }
};
