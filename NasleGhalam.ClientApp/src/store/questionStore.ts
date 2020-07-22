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
  openModal: {
    create: boolean;
    edit: boolean;
    delete: boolean;
    questions: boolean;
  };
  question: IQuestion;
  private _questionList: Array<IQuestion>;
  private _listByQuestionGroupId: Array<IQuestion>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.question = util.cloneObject(DefaultQuestion);
    this._questionList = [];
    this._listByQuestionGroupId = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false,
      questions: false
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

  get gridDataByQuestionGroupId() {
    return this._listByQuestionGroupId;
  }

  get hasQuestionAnswer() {
    return this._listByQuestionGroupId.every(
      x => x.QuestionAnswers != undefined && x.QuestionAnswers.length > 0
    );
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
  private SET_LIST_BY_QUESTION_GROUP_ID(list: Array<IQuestion>) {
    this._listByQuestionGroupId = list;
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
  OPEN_MODAL_QUESTIONS(open: boolean) {
    this.openModal.questions = open;
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
        if (response.data.Topics) {
          this.question.TopicIds = response.data.Topics.map(x => x.Id);
        }
        if (response.data.Tags) {
          this.question.TagIds = response.data.Tags.map(x => x.Id);
        }
      });
  }

  @action()
  async fillList(payload: {
    lessonId: number;
    showWithoutTopic: boolean;
    showNoJudgement: boolean;
    showJudged: boolean;
    showActived: boolean;
    topicIds: Array<number>;
  }) {
    var url = "";

    if (payload.showJudged) {
      url = `${baseUrl}/GetAllJudgedByUserIdByLessonId/${payload.lessonId}`;
    } else {
      if (payload.showActived) {
        url = `${baseUrl}/GetAllActiveByLessonId/${payload.lessonId}`;
      } else {
        if (payload.showWithoutTopic) {
          url = `${baseUrl}/GetAllNoTopicByLessonId/${payload.lessonId}`;
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
      }
    }

    return axios.get(url).then((response: AxiosResponse<Array<IQuestion>>) => {
      this.SET_LIST(response.data);
    });
  }

  @action()
  async fillListByQuestionGroupId(questionGroupId: number) {
    return axios
      .get(`${baseUrl}/GetAllByQuestionGroupId/${questionGroupId}`)
      .then((response: AxiosResponse<Array<IQuestion>>) => {
        this.SET_LIST_BY_QUESTION_GROUP_ID(response.data);
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

    var newData = {
      Id: this.question.Id,
      QuestionNumber: this.question.QuestionNumber,
      QuestionPoint: this.question.QuestionPoint,
      UseEvaluation: this.question.UseEvaluation,
      IsStandard: this.question.IsStandard,
      WriterId: this.question.WriterId,
      SupervisorUserId: this.question.SupervisorUserId,
      ResponseSecond: this.question.ResponseSecond,
      Description: this.question.Description,
      AnswerNumber: this.question.AnswerNumber,
      LookupId_QuestionType: this.question.LookupId_QuestionType,
      LookupId_QuestionHardnessType: this.question
        .LookupId_QuestionHardnessType,
      LookupId_RepeatnessType: this.question.LookupId_RepeatnessType,
      LookupId_AuthorType: this.question.LookupId_AuthorType,
      LookupId_AreaType: this.question.LookupId_AreaType,
      TopicIds: this.question.TopicIds,
      TagIds: this.question.TagIds,
      LookupId_QuestionRank: this.question.LookupId_QuestionRank
    };
    var params = util.toParam(newData);

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
        this.OPEN_MODAL_CREATE(!closeModal);
        this.resetCreate();
      }
    });
  }

  @action()
  async resetCreate() {
    this.question.Id = 0;
    this.RESET(this._createVue);
    var wordFile = this._createVue.$refs.wordFile;
    wordFile["reset"]();
  }

  @action()
  async submitEdit(activeAccess: string) {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    var wordFile = vm.$refs.wordFile;
    //var msg = "";
    // if (this.question.TopicIds && this.question.TopicIds.length == 0) {
    //   msg += "مبحثی انتخاب نکرده اید.<br/>";
    // }
    // if (
    //   this.question.LookupId_QuestionType == 6 &&
    //   (this.question.AnswerNumber < 1 || this.question.AnswerNumber > 4)
    // ) {
    //   msg += "گزینه صحیح انتخاب نشده است.";
    // }

    // if (msg) {
    //   this.notify({
    //     vm: vm,
    //     data: {
    //       Message: msg,
    //       MessageType: MessageType.Error,
    //       Obj: null
    //     }
    //   });
    //   return;
    // }
    var base64File: any = "";
    if (wordFile && wordFile["files"][0]) {
      base64File = await util.convertFileToBase64(wordFile["files"][0]);
    }

    // var formData = new FormData();
    // if (wordFile) {
    //   formData.append(wordFile["name"], wordFile["files"][0]);
    // }

    var newData = {
      Id: this.question.Id,
      Base64File: base64File,
      QuestionNumber: this.question.QuestionNumber,
      QuestionPoint: this.question.QuestionPoint,
      UseEvaluation: this.question.UseEvaluation,
      IsStandard: this.question.IsStandard,
      IsDelete: this.question.IsDelete,
      WriterId: this.question.WriterId,
      ResponseSecond: this.question.ResponseSecond,
      Description: this.question.Description,
      AnswerNumber: this.question.AnswerNumber,
      LookupId_QuestionType: this.question.LookupId_QuestionType,
      LookupId_QuestionHardnessType: this.question
        .LookupId_QuestionHardnessType,
      LookupId_RepeatnessType: this.question.LookupId_RepeatnessType,
      LookupId_AuthorType: this.question.LookupId_AuthorType,
      LookupId_AreaType: this.question.LookupId_AreaType,
      LookupId_QuestionRank: this.question.LookupId_QuestionRank,
      TopicIds: this.question.TopicIds,
      TagIds: this.question.TagIds,
      TopicAnswer: this.question.TopicAnswer.join(",")
    };

    var url = "";
    if (activeAccess == "canEditAdminProp") {
      url = `${baseUrl}/Update`;
    } else if (activeAccess == "canEditImportProp") {
      url = `${baseUrl}/UpdateImport`;
    } else if (activeAccess == "canEditTopicProp") {
      url = `${baseUrl}/UpdateTopic`;
    }

    return axios
      .post(url, newData)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.UPDATE(data.Obj);
          this.OPEN_MODAL_EDIT(false);
          this.resetEdit();
        }
      });

    // return axios({
    //   method: "post",
    //   url: url,
    //   data: formData,
    //   headers: { "Content-Type": "multipart/form-data" }
    // }).then((response: AxiosResponse<IMessageResult>) => {
    //   let data = response.data;
    //   this.notify({ vm, data });

    //   if (data.MessageType == MessageType.Success) {
    //     this.UPDATE(data.Obj);
    //     this.OPEN_MODAL_EDIT(false);
    //     this.resetEdit();
    //   }
    // });
  }

  @action()
  async resetEdit() {
    if (!this._editVue) return;
    this.RESET(this._editVue);
    var wordFile = this._editVue.$refs.wordFile;
    if (wordFile) wordFile["reset"]();
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
