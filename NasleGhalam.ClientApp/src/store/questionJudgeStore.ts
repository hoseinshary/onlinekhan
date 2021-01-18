import Vue from "Vue";
import IQuestionJudge, {
  DefaultQuestionJudge
} from "src/models/IQuestionJudge";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { QUESTION_JUDGE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "questionJudgeStore/" })
export class QuestionJudgeStore extends VuexModule {
  openModal: { index: boolean };
  questionJudge: IQuestionJudge;
  private _questionJudgeList: Array<IQuestionJudge>;
  private _indexVue: Vue;
  private _createVue: Vue;
  private _editVue: Vue;
  private _detailVue: Vue;


  /**
   * initialize data
   */
  constructor() {
    super();

    this.questionJudge = util.cloneObject(DefaultQuestionJudge);
    this._questionJudgeList = [];
    this.openModal = {
      index: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "ارزیابی سوال";
  }

  get gridData() {
    return this._questionJudgeList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(questionJudge: IQuestionJudge) {
    this._questionJudgeList.push(questionJudge);
  }

  @mutation
  private UPDATE(questionJudge: IQuestionJudge) {
    let index = this._questionJudgeList.findIndex(
      x => x.Id == this.questionJudge.Id
    );
    if (index < 0) return;
    util.mapObject(questionJudge, this._questionJudgeList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._questionJudgeList.findIndex(
      x => x.Id == this.questionJudge.Id
    );
    if (index < 0) return;
    this._questionJudgeList.splice(index, 1);
  }

  @mutation
  private RESET(vm?: any) {
    util.mapObject(
      DefaultQuestionJudge,
      this.questionJudge,
      "Id",
      "QuestionId"
    );
    if (vm && vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IQuestionJudge>) {
    this._questionJudgeList = list;
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

  @mutation
  SET_DEATIL_VUE(vm: Vue) {
    this._detailVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IQuestionJudge>) => {
        util.mapObject(response.data, this.questionJudge);
      });
  }

  @action()
  async fillListByQuestionId(questionId: number) {
    return axios
      .get(`${baseUrl}/GetAllByQuestionId/${questionId}`)
      .then((response: AxiosResponse<Array<IQuestionJudge>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.questionJudge.$touch();
      if (vm.$v.questionJudge.$error) {
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
    if(!this.questionJudge.IsDelete && !this.questionJudge.IsUpdate && !this.questionJudge.IsActiveQuestion){
    this.notify({
      vm: vm,
      data: {
        Message: "وضعیت سوال مشخص نشده است. باید تایید یا عدم تایید شود.",
        MessageType: MessageType.Error,
        Obj: null
      }
    });
    return;
  }
    return axios
      .post(`${baseUrl}/Create`, this.questionJudge)
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
  async submitCreateWithoutReset() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    if(!this.questionJudge.IsDelete && !this.questionJudge.IsUpdate && !this.questionJudge.IsActiveQuestion){
      this.notify({
        vm: vm,
        data: {
          Message: "وضعیت سوال مشخص نشده است. باید تایید یا عدم تایید شود.",
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }

    return axios
      .post(`${baseUrl}/Create`, this.questionJudge)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.CREATE(data.Obj);
        }
      });
  }

  @action()
  async resetCreate() {
    this.questionJudge.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    if(!this.questionJudge.IsDelete && !this.questionJudge.IsUpdate && !this.questionJudge.IsActiveQuestion){
      this.notify({
        vm: vm,
        data: {
          Message: "وضعیت سوال مشخص نشده است. باید تایید یا عدم تایید شود.",
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }

    return axios
      .post(`${baseUrl}/Update`, this.questionJudge)
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
  async submitDelete() {
    var vm = this._indexVue;
    return axios
      .post(`${baseUrl}/Delete/${this.questionJudge.Id}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
          vm["selectedTab"] = "tab-create";
          this.RESET();
        }
      });
  }
  //#endregion
}

export const questionJudgeStore = QuestionJudgeStore.ExtractVuexModule(
  QuestionJudgeStore
);
