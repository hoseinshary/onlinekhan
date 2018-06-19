import axios from "axios";
import { LocalStorage } from "quasar";

axios.defaults.headers.common["Token"] = LocalStorage.get.item("Token");

axios.interceptors.response.use(
  function(response) {
    return response;
  },
  function(error) {
    if (401 === error.response.status) {
      debugger;
      window.$eventHub.emit("logoutAxios");
    }
    return Promise.reject(error);
  }
);

function createAxios() {
  var cmlx = axios.create();
  cmlx.interceptors.request.use(
    conf => {
      window.$eventHub.emit("enableLoader"); //axios-before-request
      return conf;
    },
    error => {
      window.$eventHub.emit("disableLoader"); //"axios-request-error");
      if (401 === error.response.status) window.$eventHub.emit("logoutAxios");
      return Promise.reject(error);
    }
  );
  cmlx.interceptors.response.use(
    response => {
      window.$eventHub.emit("disableLoader"); //"axios-after-response");
      return response;
    },
    error => {
      window.$eventHub.emit("disableLoader"); //"axios-response-error");
      if (401 === error.response.status) window.$eventHub.emit("logoutAxios");
      return Promise.reject(error);
    }
  );
  return cmlx;
}
const LoadingAxios = createAxios();
const SimpleAxios = axios;
export { LoadingAxios, SimpleAxios };
