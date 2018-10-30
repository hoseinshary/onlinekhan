import Vue from 'vue';
import Vuex from 'vuex';

import gradeStore from './gradeStore';
import gradeLevelStore from './gradeLevelStore';
import cityStore from './cityStore';
import lessonStore from './lessonStore';
import provinceStore from './provinceStore';
import userStore from './userStore';
import roleStore from './roleStore';
import educationGroupStore from './educationGroupStore';
import educationSubGroupStore from './educationSubGroupStore';
import topicStore from './topicStore';
import lookupStore from './lookupStore';
import educationBookStore from './educationBookStore';
import examStore from './examStore';
import publisherStore from './publisherStore';
import tagStore from './tagStore';
import educationYearStore from './educationYearStore';
import axillaryBookStore from './axillaryBookStore';
import universityBranchStore from './universityBranchStore';
import questionStore from './questionStore';
import studentStore from './studentStore';
import educationTreeStore from './educationTreeStore';

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
    }
  },
  modules: {
    gradeStore,
    gradeLevelStore,
    cityStore,
    lessonStore,
    provinceStore,
    userStore,
    roleStore,
    educationGroupStore,
    educationSubGroupStore,
    topicStore,
    lookupStore,
    educationBookStore,
    examStore,
    publisherStore,
    tagStore,
    educationYearStore,
    axillaryBookStore,
    universityBranchStore,
    questionStore,
    axillaryBookStore,
    studentStore,
    educationTreeStore
  }
});

export default store;
