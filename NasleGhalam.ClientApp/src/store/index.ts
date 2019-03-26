import Vue from "vue";
import Vuex from "vuex";

import { CityStore, cityStore } from "./cityStore";
import { ProvinceStore, provinceStore } from "./provinceStore";
import { RoleStore, roleStore } from "./roleStore";
import { AccessStore, accessStore } from "./accessStore";

Vue.use(Vuex);

const store = new Vuex.Store({
  actions: {
    /**
     * create notification and show
     */
    notify({}, notify) {
      // if vue instance not pass
      if (!notify.vm) return;
      var html = `<div class="snotifyToast__body">${notify.body}</div> `;
      if (notify.title) {
        html = `<div class="snotifyToast__title">${notify.title}</div>` + html;
      }

      //fine notification type
      var type =
        notify.type == 0
          ? "error"
          : notify.type == 1
          ? "success"
          : notify.type == 2
          ? "warning"
          : "error";

      //show notification
      notify.vm.$snotify.html(html, {
        type: type,
        timeout: 4000,
        showProgressBar: true,
        position: "leftTop"
      });
    },

    /**
     * show invalid form notification
     */
    notifyInvalidForm({ dispatch }, vm) {
      dispatch("notify", {
        vm,
        body: "تمام مقادیر را بصورت صحیح وارد نمایید."
      });
    }
  },
  modules: {
    cityStore,
    provinceStore,
    roleStore,
    accessStore
  }
});

export default store;

export const vxm = {
  cityStore: CityStore.CreateProxy(store, CityStore) as CityStore,
  provinceStore: ProvinceStore.CreateProxy(
    store,
    ProvinceStore
  ) as ProvinceStore,
  roleStore: RoleStore.CreateProxy(store, RoleStore) as RoleStore,
  accessStore: AccessStore.CreateProxy(store, AccessStore) as AccessStore
};
