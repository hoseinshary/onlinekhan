import Vue from 'vue';
import Vuex from 'vuex';

import gradeStore from './gradeStore';
import gradeLevelStore from './gradeLevelStore';
import cityStore from './cityStore';
import lessonStore from './lessonStore';
import provinceStore from './provinceStore';
import userStore from './userStore';

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
          ? 'error'
          : notify.type == 1
            ? 'success'
            : notify.type == 2
              ? 'warning'
              : 'error';

      //show notification
      notify.vm.$snotify.html(html, {
        type: type,
        timeout: 4000,
        showProgressBar: true,
        position: 'leftTop'
      });
    },

    /**
     * show invalid form notification
     */
    notifyInvalidForm({ dispatch }, vm) {
      dispatch('notify', {
        vm,
        body: 'تمام مقادیر را بصورت صحیح وارد نمایید.'
      });
    },

    /**
     * vlidate form
     */
    validateFormStore({ dispatch }, vm) {
      // check instance validation
      vm.$v.instanceObj.$touch();
      if (vm.$v.instanceObj.$error) {
        dispatch('notifyInvalidForm', vm);
        return false;
      }

      return true;
    }
  },
  modules: {
    gradeStore,
    gradeLevelStore,
    cityStore,
    lessonStore,
    provinceStore,
    userStore
  }
});

export default store;
