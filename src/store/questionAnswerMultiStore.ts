import Vue from "Vue";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { QUESTION_ANSWER_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import IQuestionAnswerMulti, {
  DefaultQuestionAnswerMulti
} from "src/models/IQuestionAnswerMulti";

@Module({ namespacedPath: "questionAnswerMultiStore/" })
export class QuestionAnswerMultiStore extends VuexModule {
  openModal: { index: boolean; create: boolean };
  questionAnswerMulti: IQuestionAnswerMulti;
  private _createVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.questionAnswerMulti = util.cloneObject(DefaultQuestionAnswerMulti);
    this.openModal = {
      index: false,
      create: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "جواب سوال";
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private RESET(vm: any) {
    util.mapObject(
      DefaultQuestionAnswerMulti,
      this.questionAnswerMulti,
      "QuestionId"
    );
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  OPEN_MODAL_CREATE(open: boolean) {
    this.openModal.create = open;
  }

  @mutation
  SET_CREATE_VUE(vm: Vue) {
    this._createVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.questionAnswerMulti.$touch();
      if (vm.$v.questionAnswerMulti.$error) {
        const context = getRawActionContext(this);
        context.dispatch("notifyInvalidForm", vm, { root: true });
        resolve(false);
      }
      resolve(true);
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
  async submitPreCreateMulti() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    var wordFile = vm.$refs.wordFile;
    var msg = "";
    if (wordFile["files"].length == 0) {
      msg = "فایل ورد انتخاب نشده است.<br/>";
      this.notify({
        vm: vm,
        data: {
          Message: msg,
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }

    var formData = new FormData();
    formData.append(wordFile["name"], wordFile["files"][0]);
    var params = util.toParam(this.questionAnswerMulti);

    return axios({
      method: "post",
      url: `${baseUrl}/PreCreateMulti?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
      let data = response.data;
      if (data.Message) {
        this.notify({ vm, data });
      }

      if (data.MessageType == MessageType.Success) {
        vm["selectedTab"] = "previewTab";
        vm["previewImages"] = data.Obj;
      }
    });
  }

  @action()
  async submitCreateMulti() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    var wordFile = vm.$refs.wordFile;
    var msg = "";
    if (wordFile["files"].length == 0) {
      msg = "فایل ورد انتخاب نشده است.<br/>";
      this.notify({
        vm: vm,
        data: {
          Message: msg,
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }

    var formData = new FormData();
    formData.append(wordFile["name"], wordFile["files"][0]);
    var params = util.toParam(this.questionAnswerMulti);

    return axios({
      method: "post",
      url: `${baseUrl}/CreateMulti?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
      let data = response.data;
      this.notify({ vm, data });
    });
  }

  @action()
  async resetCreate() {
    this.RESET(this._createVue);
    var wordFile = this._createVue.$refs.wordFile;
    wordFile["reset"]();
  }
  //#endregion
}

export const questionAnswerMultiStore = QuestionAnswerMultiStore.ExtractVuexModule(
  QuestionAnswerMultiStore
);
