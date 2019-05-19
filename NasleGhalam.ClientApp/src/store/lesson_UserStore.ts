import Vue from "Vue";
import ILesson_User, { DefaultLesson_User } from "src/models/ILesson_User";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import {
  LESSON_USER_URL as baseUrl,
  USER_URL,
  LESSON_URL
} from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import ILesson from "src/models/ILesson";
import IUser from "src/models/IUser";

@Module({ namespacedPath: "lesson_UserStore/" })
export class Lesson_UserStore extends VuexModule {
  lesson_User: ILesson_User;
  private _lessonList: Array<ILesson>;
  private _userList: Array<IUser>;
  private _indexVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.lesson_User = util.cloneObject(DefaultLesson_User);
    this._lessonList = [];
    this._userList = [];
  }

  //#region ### getters ###
  get modelName() {
    return "اختصاص کاربر به درس";
  }

  get lessonData() {
    return this._lessonList;
  }

  get userData() {
    return this._userList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultLesson_User, this.lesson_User, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LESSON_LIST(list: Array<ILesson>) {
    list.forEach(lesson => {
      lesson.Checked = false;
    });
    this._lessonList = list;
  }

  @mutation
  private SET_USER_LIST(list: Array<IUser>) {
    list.forEach(user => {
      user.Checked = false;
    });
    this._userList = list;
  }

  @mutation
  SET_INDEX_VUE(vm: Vue) {
    this._indexVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async fillLessonList() {
    return axios
      .get(`${LESSON_URL}/GetAll`)
      .then((response: AxiosResponse<Array<ILesson>>) => {
        this.SET_LESSON_LIST(response.data);
      });
  }

  @action()
  async fillUserList() {
    return axios
      .get(`${USER_URL}/GetAll`)
      .then((response: AxiosResponse<Array<IUser>>) => {
        this.SET_USER_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async notify(payload: { vm: Vue; data: IMessageResult }) {
    const context = getRawActionContext(this);
    return context.dispatch(
      "notify",
      {
        body: payload.data.Message,
        type: payload.data.MessageType,
        vm: payload.vm
      },
      { root: true }
    );
  }

  @action()
  async submitChanges() {
    let vm = this._indexVue;

    return axios
      .post(`${baseUrl}/SubmitChanges`, this.lesson_User)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
        }
      });
  }
  //#endregion
}

export const lesson_UserStore = Lesson_UserStore.ExtractVuexModule(
  Lesson_UserStore
);
