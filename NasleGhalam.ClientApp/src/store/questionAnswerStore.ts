import Vue from "Vue";
import IQuestionAnswer, {
  DefaultQuestionAnswer
} from "src/models/IQuestionAnswer";
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

@Module({ namespacedPath: "questionAnswerStore/" })
export class QuestionAnswerStore extends VuexModule {
  openModal: { index: boolean };
  questionAnswer: IQuestionAnswer;
  private _questionAnswerList: Array<IQuestionAnswer>;
  private _indexVue: Vue;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.questionAnswer = util.cloneObject(DefaultQuestionAnswer);
    this._questionAnswerList = [];
    this.openModal = {
      index: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "جواب سوال";
  }

  get recordName() {
    return this.questionAnswer.Title || "";
  }

  get gridData() {
    return this._questionAnswerList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(questionAnswer: IQuestionAnswer) {
    this._questionAnswerList.push(questionAnswer);
  }

  @mutation
  private UPDATE(questionAnswer: IQuestionAnswer) {
    let index = this._questionAnswerList.findIndex(
      x => x.Id == this.questionAnswer.Id
    );
    if (index < 0) return;
    util.mapObject(questionAnswer, this._questionAnswerList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._questionAnswerList.findIndex(
      x => x.Id == this.questionAnswer.Id
    );
    if (index < 0) return;
    this._questionAnswerList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(
      DefaultQuestionAnswer,
      this.questionAnswer,
      "Id",
      "QuestionId"
    );
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IQuestionAnswer>) {
    this._questionAnswerList = list;
  }

  @mutation
  OPEN_MODAL_INDEX(open: boolean) {
    this.openModal.index = open;
  }

  @mutation
  SET_INDEX_VUE(vm: Vue) {
    this._indexVue = vm;
  }

  @mutation
  SET_CREATE_VUE(vm: Vue) {
    this._createVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this._editVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IQuestionAnswer>) => {
        util.mapObject(response.data, this.questionAnswer);
      });
  }

  @action()
  async fillListByQuestionId(questionId: number) {
    return axios
      .get(`${baseUrl}/GetAllByQuestionId/${questionId}`)
      .then((response: AxiosResponse<Array<IQuestionAnswer>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.questionAnswer.$touch();
      if (vm.$v.questionAnswer.$error) {
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
  async submitCreate() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    var wordFile = vm.$refs.wordFile;
    var msg = "";

    if (wordFile["files"].length == 0) {
      msg = "فایل ورد انتخاب نشده است.<br/>";
    }
    // if (this.question.TopicIds && this.question.TopicIds.length == 0) {
    //   msg += "مبحثی انتخاب نکرده اید.<br/>";
    // }
    // if (
    //   this.question.LookupId_QuestionType == 6 &&
    //   (this.question.AnswerNumber < 1 || this.question.AnswerNumber > 4)
    // ) {
    //   msg += "گزینه صحیح انتخاب نشده است.";
    // }

    if (msg) {
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
    var params = util.toParam(this.questionAnswer);

    return axios({
      method: "post",
      url: `${baseUrl}/Create?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
      let data = response.data;
      this.notify({ vm, data });

      if (data.MessageType == MessageType.Success) {
        this.CREATE(data.Obj);
        this.resetCreate();
      }
    });
  }

  @action()
  async resetCreate() {
    this.questionAnswer.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.questionAnswer)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.UPDATE(data.Obj);
          this.resetEdit();
        }
      });
  }

  @action()
  async resetEdit() {
    this.RESET(this._editVue);
  }

  @action()
  async submitDelete(vm: Vue) {
    return axios
      .post(`${baseUrl}/Delete/${this.questionAnswer.Id}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
        }
      });
  }
  //#endregion
}

export const questionAnswerStore = QuestionAnswerStore.ExtractVuexModule(
  QuestionAnswerStore
);
