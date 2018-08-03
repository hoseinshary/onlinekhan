import axios from 'axios';
// import store from "utilities/store";
// import router from "utilities/router";
import { API_URL } from './site-config';
// import { bus } from '../main';
import { Loading, QSpinnerIos } from 'quasar';
import { LocalStorage } from 'quasar';
import util from 'utilities/util';

axios.defaults.headers.common['Token'] = LocalStorage.get.item('Token');

axios.interceptors.request.use(
  cfg => {
    // if (store.getters.isAuthenticated) {
    //   const auth = store.getters.getAuth;
    //   // eslint-disable-next-line
    //   cfg.headers.Authorization = `${auth.token_type} ${auth.access_token}`;
    // }
    cfg.url = API_URL + cfg.url;
    //bus.$emit('enableLoader');
    Loading.show({ spinner: QSpinnerIos, delay: 0 });
    //Loading.show();
    // cfg.headers.Token = `Bearer dsfsdfsddfvbfggh2315645dfgp`;

    return cfg;
  },
  err => {
    // bus.$emit('disableLoader');
    if (401 === err.response.status) util.logout();
    setTimeout(() => {
      Loading.hide();
    }, 500);
    Promise.reject(err);
  }
);

axios.interceptors.response.use(
  res => {
    setTimeout(() => {
      Loading.hide();
    }, 500);
    // bus.$emit('disableLoader');
    //Loading.hide();
    return res;
  },
  // eslint-disable-next-line
  err => {
    setTimeout(() => {
      Loading.hide();
    }, 500);
    console.log(err);
    if (err.response && err.response.status)
      switch (err.response.status) {
        case 401:
          util.logout();
          // store.commit("removeAuthentication");
          // router.push({
          //   path: "/signin",
          //   query: {
          //     redirect: router.app._route.fullPath
          //   }
          // });
          break;
      }

    //Loading.hide();
    // bus.$emit('disableLoader');
    return Promise.reject(err);
  }
);

export default axios;
