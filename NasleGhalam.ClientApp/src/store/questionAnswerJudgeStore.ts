import Vue from "Vue";
import IQuestionAnswerJudge, {
  DefaultQuestionAnswerJudge
} from "src/models/IQuestionAnswerJudge";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { QUESTION_ANSWER_JUDGE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "questionAnswerJudgeStore/" })
export class QuestionAnswerJudgeStore extends VuexModule {
  questionAnswerJudge: IQuestionAnswerJudge;
  private _questionAnswerJudgeList: Array<IQuestionAnswerJudge>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.questionAnswerJudge = util.cloneObject(DefaultQuestionAnswerJudge);
    this._questionAnswerJudgeList = [];
  }

  //#region ### getters ###
  get modelName() {
    return "کارشناسی جواب";
  }

  get gridData() {
    return this._questionAnswerJudgeList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(questionAnswerJudge: IQuestionAnswerJudge) {
    this._questionAnswerJudgeList.push(questionAnswerJudge);
  }

  @mutation
  private UPDATE(questionAnswerJudge: IQuestionAnswerJudge) {
    let index = this._questionAnswerJudgeList.findIndex(
      x => x.Id == this.questionAnswerJudge.Id
    );
    if (index < 0) return;
    util.mapObject(questionAnswerJudge, this._questionAnswerJudgeList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._questionAnswerJudgeList.findIndex(
      x => x.Id == this.questionAnswerJudge.Id
    );
    if (index < 0) return;
    this._questionAnswerJudgeList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(
      DefaultQuestionAnswerJudge,
      this.questionAnswerJudge,
      "Id",
      "QuestionAnswerId"
    );
    if (vm && vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IQuestionAnswerJudge>) {
    this._questionAnswerJudgeList = list;
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
      .then((response: AxiosResponse<IQuestionAnswerJudge>) => {
        util.mapObject(response.data, this.questionAnswerJudge);
      });
  }

  @action()
  async fillListByQuestionAnswerId(questionAnswerId: number) {
    return axios
      .get(`${baseUrl}/GetAllByQuestionAnswerId/${questionAnswerId}`)
      .then((response: AxiosResponse<Array<IQuestionAnswerJudge>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.questionAnswerJudge.$touch();
      if (vm.$v.questionAnswerJudge.$error) {
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

    return axios
      .post(`${baseUrl}/Create`, this.questionAnswerJudge)
      .then((response: AxiosResponse<IMessageResult>) => {
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
    this.questionAnswerJudge.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.questionAnswerJudge)
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
      .post(`${baseUrl}/Delete/${this.questionAnswerJudge.Id}`)
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

export const questionAnswerJudgeStore = QuestionAnswerJudgeStore.ExtractVuexModule(
  QuestionAnswerJudgeStore
);
