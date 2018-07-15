import Vue from 'vue';
import Vuex from 'vuex';

import grade from './grade';
import gradeLevel from './gradeLevel';

Vue.use(Vuex);

const store = new Vuex.Store({
  actions: {
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
        showProgressBar: true
      });
    },
    notifyInvalidForm({ dispatch }, vm) {
      dispatch('notify', {
        vm,
        body: 'تمام مقادیر را بصورت صحیح وارد نمایید.'
      });
    }
  },
  modules: {
    grade,
    gradeLevel
  }
});

export default store;
