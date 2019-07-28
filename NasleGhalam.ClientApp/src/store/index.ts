import Vue from "vue";
import Vuex from "vuex";

import { CityStore, cityStore } from "./cityStore";
import { ProvinceStore, provinceStore } from "./provinceStore";
import { RoleStore, roleStore } from "./roleStore";
import { AccessStore, accessStore } from "./accessStore";
import { UserStore, userStore } from "./userStore";
import { EducationTreeStore, educationTreeStore } from "./educationTreeStore";
import { LookupStore, lookupStore } from "./lookupStore";
import {
  EducationSubGroupStore,
  educationSubGroupStore
} from "./educationSubGroupStore";
import { EducationYearStore, educationYearStore } from "./educationYearStore";
import { LessonStore, lessonStore } from "./lessonStore";
import { TopicStore, topicStore } from "./topicStore";
import { QuestionStore, questionStore } from "./questionStore";
import { TagStore, tagStore } from "./tagStore";
import { QuestionJudgeStore, questionJudgeStore } from "./questionJudgeStore";
import { QuestionGroupStore, questionGroupStore } from "./questionGroupStore";
import {
  QuestionAnswerStore,
  questionAnswerStore
} from "./questionAnswerStore";
import {
  QuestionAnswerMultiStore,
  questionAnswerMultiStore
} from "./questionAnswerMultiStore";
import { PublisherStore, publisherStore } from "./publisherStore";
import { EducationBookStore, educationBookStore } from "./educationBookStore";
import { StudentStore, studentStore } from "./studentStore";
import { WriterStore, writerStore } from "./writerStore";
import { Lesson_UserStore, lesson_UserStore } from "./lesson_UserStore";
import {
  LessonDepartmentStore,
  lessonDepartmentStore
} from "./lessonDepartmentStore";
import { AxillaryBookStore, axillaryBookStore } from "./axillaryBookStore";
import { AssayStore, assayStore } from "./assayStore";
import { ResumeStore, resumeStore } from "./resumeStore";

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
    accessStore,
    userStore,
    educationTreeStore,
    lookupStore,
    educationSubGroupStore,
    educationYearStore,
    lessonStore,
    topicStore,
    questionStore,
    tagStore,
    questionJudgeStore,
    questionGroupStore,
    questionAnswerStore,
    questionAnswerMultiStore,
    publisherStore,
    educationBookStore,
    studentStore,
    writerStore,
    lesson_UserStore,
    lessonDepartmentStore,
    axillaryBookStore,
    assayStore,
    resumeStore
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
  accessStore: AccessStore.CreateProxy(store, AccessStore) as AccessStore,
  userStore: UserStore.CreateProxy(store, UserStore) as UserStore,
  educationTreeStore: UserStore.CreateProxy(
    store,
    EducationTreeStore
  ) as EducationTreeStore,
  lookupStore: LookupStore.CreateProxy(store, LookupStore) as LookupStore,
  educationSubGroupStore: EducationSubGroupStore.CreateProxy(
    store,
    EducationSubGroupStore
  ) as EducationSubGroupStore,
  educationYearStore: EducationYearStore.CreateProxy(
    store,
    EducationYearStore
  ) as EducationYearStore,
  lessonStore: LessonStore.CreateProxy(store, LessonStore) as LessonStore,
  topicStore: TopicStore.CreateProxy(store, TopicStore) as TopicStore,
  questionStore: QuestionStore.CreateProxy(
    store,
    QuestionStore
  ) as QuestionStore,
  tagStore: TagStore.CreateProxy(store, TagStore) as TagStore,
  questionJudgeStore: QuestionJudgeStore.CreateProxy(
    store,
    QuestionJudgeStore
  ) as QuestionJudgeStore,
  questionGroupStore: QuestionGroupStore.CreateProxy(
    store,
    QuestionGroupStore
  ) as QuestionGroupStore,
  questionAnswerStore: QuestionAnswerStore.CreateProxy(
    store,
    QuestionAnswerStore
  ) as QuestionAnswerStore,
  questionAnswerMultiStore: QuestionAnswerMultiStore.CreateProxy(
    store,
    QuestionAnswerMultiStore
  ) as QuestionAnswerMultiStore,
  publisherStore: PublisherStore.CreateProxy(
    store,
    PublisherStore
  ) as PublisherStore,
  educationBookStore: EducationBookStore.CreateProxy(
    store,
    EducationBookStore
  ) as EducationBookStore,
  studentStore: StudentStore.CreateProxy(store, StudentStore) as StudentStore,
  writerStore: WriterStore.CreateProxy(store, WriterStore) as WriterStore,
  lesson_UserStore: Lesson_UserStore.CreateProxy(
    store,
    Lesson_UserStore
  ) as Lesson_UserStore,
  lessonDepartmentStore: LessonDepartmentStore.CreateProxy(
    store,
    LessonDepartmentStore
  ) as LessonDepartmentStore,
  axillaryBookStore: AxillaryBookStore.CreateProxy(
    store,
    AxillaryBookStore
  ) as AxillaryBookStore,
  assayStore: AssayStore.CreateProxy(store, AssayStore) as AssayStore,
  resumeStore: ResumeStore.CreateProxy(store, ResumeStore) as ResumeStore
};
