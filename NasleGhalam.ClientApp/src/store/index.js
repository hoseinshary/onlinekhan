import Vue from "vue";
import Vuex from "vuex";
import { Notify } from "quasar";

import example from "./module-example";
import region from "./Region";
import area from "./Area";
import sensor from "./Sensor";
import role from "./Role";
import user from "./User";

Vue.use(Vuex);

const store = new Vuex.Store({
  actions: {
    notify(context, notiData) {
      debugger;
      if (notiData.message.includes("<br />"))
        Notify.create({
          message: notiData.message.split("<br />")[0],
          type: notiData.isSuccess ? "positive" : "negative",
          position: "top",
          detail: notiData.message.split("<br />")[1].replace(/<br \/>/g, "\n")
        });
      else
        Notify.create({
          message: notiData.message,
          type: notiData && notiData.isSuccess ? "positive" : "negative",
          position: "top"
        });
      window.$eventHub.emit("closeModal", notiData.action);
    }
  },
  modules: {
    example,
    region,
    area,
    sensor,
    role,
    user
  }
});
export default store;
