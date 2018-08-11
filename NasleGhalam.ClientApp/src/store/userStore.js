import axios from 'utilities/axios';
import { USER_URL as baseUrl } from 'utilities/site-config';
import { LocalStorage } from 'quasar';
import router from 'router';

export default {
  namespaced: true,
  strict: true,
  state: {
    modelName: 'کاربر',
    isOpenModalCreate: false,
    instanceObj: {
      id: 0,
      name: '',
      family: '',
      age: '',
      gender: '',
      isActive: false,
      children: [],
      roleId: '',
      Username: '',
      Password: ''
    },
    instanceLoginObj: {
      UserName: '',
      Password: ''
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
  },
  actions: {
    // create(context) {
    //   context.commit('create');
    // }
    // logoutStore(context, vm) {
    //   LocalStorage.remove('authList');
    //   LocalStorage.remove('menuList');
    //   LocalStorage.remove('Token');
    //   LocalStorage.remove('FullName');
    //   // this.$axios.defaults.headers.common['Token'] = '';
    //   this.$router.push('/user/login');
    // },
    loginStore(context, vm) {
      axios
        .post(`${baseUrl}/Login`, context.state.instanceLoginObj)
        .then(response => {
          let data = response.data;

          context.dispatch(
            'notify',
            { body: data.Message, type: data.MessageType, vm: vm },
            { root: true }
          );

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
          }
        });
    }
  }
  //   getters: {
  //     getCount: state => {
  //       console.log('getCount:')
  //       console.log(state)
  //       return state.count
  //     }
  //   }
};
