import Vue from "Vue";
import IQuestion, { DefaultQuestion } from "src/models/IQuestion";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { QUESTION_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "questionStore/" })
export class QuestionStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  question: IQuestion;
  private _questionList: Array<IQuestion>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.question = util.cloneObject(DefaultQuestion);
    this._questionList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "سوال";
  }

  get recordName() {
    return this.question.Context || "";
  }

  get gridData() {
    return this._questionList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(question: IQuestion) {
    this._questionList.push(question);
  }

  @mutation
  private UPDATE(question: IQuestion) {
    let index = this._questionList.findIndex(x => x.Id == this.question.Id);
    if (index < 0) return;
    util.mapObject(question, this._questionList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._questionList.findIndex(x => x.Id == this.question.Id);
    if (index < 0) return;
    this._questionList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultQuestion, this.question, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IQuestion>) {
    this._questionList = list;
  }

  @mutation
  OPEN_MODAL_CREATE(open: boolean) {
    this.openModal.create = open;
  }

  @mutation
  OPEN_MODAL_EDIT(open: boolean) {
    this.openModal.edit = open;
  }

  @mutation
  OPEN_MODAL_DELETE(open: boolean) {
    this.openModal.delete = open;
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
      .then((response: AxiosResponse<IQuestion>) => {
        util.mapObject(response.data, this.question);
      });
  }

  @action()
  async fillList(payload: {
    lessonId: number;
    showWithoutTopic: boolean;
    showNoJudgement: boolean;
    topicIds: Array<number>;
  }) {
    var url = "";
    if (payload.showWithoutTopic) {
      url = `${baseUrl}/GetAllByLessonId/${payload.lessonId}`;
    } else {
      var params = util.toParam({
        Ids: payload.topicIds
      });
      if (payload.showNoJudgement) {
        url = `${baseUrl}/GetAllByTopicIdsNoJudge?${params}`;
      } else {
        url = `${baseUrl}/GetAllByTopicIds?${params}`;
      }
    }

    return axios.get(url).then((response: AxiosResponse<Array<IQuestion>>) => {
      this.SET_LIST(response.data);
    });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.question.$touch();
      if (vm.$v.question.$error) {
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
  async submitCreate(closeModal: boolean) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Create`, this.question)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.CREATE(data.Obj);
          this.OPEN_MODAL_CREATE(!closeModal);
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.question.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.question)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.UPDATE(data.Obj);
          this.OPEN_MODAL_EDIT(false);
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
      .post(`${baseUrl}/Delete/${this.question.Id}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
          this.OPEN_MODAL_DELETE(false);
        }
      });
  }
  //#endregion
}

export const questionStore = QuestionStore.ExtractVuexModule(QuestionStore);
