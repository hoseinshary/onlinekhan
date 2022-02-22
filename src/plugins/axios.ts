import axios, { AxiosResponse } from "axios";
import { API_URL } from "src/utilities/site-config";
import { Loading, QSpinnerIos } from "quasar";
import { LocalStorage } from "quasar";
import util from "src/utilities";

axios.defaults.headers.common["Token"] = LocalStorage.get.item("Token");

axios.interceptors.request.use(
  cfg => {
    cfg.url = API_URL + cfg.url;
    Loading.show({ spinner: QSpinnerIos, delay: 0 });

    return cfg;
  },
  err => {
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
    return Promise.reject(err);
  }
);

export default axios;
export { AxiosResponse };
