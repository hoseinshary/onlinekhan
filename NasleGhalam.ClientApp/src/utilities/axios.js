import axios from 'axios';
// import store from "utilities/store";
// import router from "utilities/router";
import { API_URL } from './site-config';
// import { bus } from '../main';
import { Loading } from 'quasar';

axios.interceptors.request.use(
  cfg => {
    // if (store.getters.isAuthenticated) {
    //   const auth = store.getters.getAuth;
    //   // eslint-disable-next-line
    //   cfg.headers.Authorization = `${auth.token_type} ${auth.access_token}`;
    // }
    cfg.url = API_URL + cfg.url;
    //bus.$emit('enableLoader');
    //Loading.show();
    // cfg.headers.Token = `Bearer dsfsdfsddfvbfggh2315645dfgp`;

    return cfg;
  },
  err => {
    // bus.$emit('disableLoader');
    Promise.reject(err);
  }
);

axios.interceptors.response.use(
  res => {
    // bus.$emit('disableLoader');
    //Loading.hide();
    return res;
  },
  // eslint-disable-next-line
  err => {
    console.log(err);
    if (err.response && err.response.status)
      switch (err.response.status) {
        case 401:
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
