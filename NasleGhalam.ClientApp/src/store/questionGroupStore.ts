import Vue from "Vue";
import IQuestionGroup, {
  DefaultQuestionGroup
} from "src/models/IQuestionGroup";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { QUESTION_GROUP_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "questionGroupStore/" })
export class QuestionGroupStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  questionGroup: IQuestionGroup;
  private _questionGroupList: Array<IQuestionGroup>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.questionGroup = util.cloneObject(DefaultQuestionGroup);
    this._questionGroupList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "سوال گروهی";
  }

  get recordName() {
    return this.questionGroup.Title || "";
  }

  get gridData() {
    return this._questionGroupList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(questionGroup: IQuestionGroup) {
    this._questionGroupList.push(questionGroup);
  }

  @mutation
  private UPDATE(questionGroup: IQuestionGroup) {
    let index = this._questionGroupList.findIndex(
      x => x.Id == this.questionGroup.Id
    );
    if (index < 0) return;
    util.mapObject(questionGroup, this._questionGroupList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._questionGroupList.findIndex(
      x => x.Id == this.questionGroup.Id
    );
    if (index < 0) return;
    this._questionGroupList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultQuestionGroup, this.questionGroup, "Id", "LessonId");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IQuestionGroup>) {
    this._questionGroupList = list;
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
      .then((response: AxiosResponse<IQuestionGroup>) => {
        util.mapObject(response.data, this.questionGroup);
      });
  }

  @action()
  async fillListByLessonId(lessonId: number) {
    return axios
      .get(`${baseUrl}/GetAllByLessonId/${lessonId}`)
      .then((response: AxiosResponse<Array<IQuestionGroup>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.questionGroup.$touch();
      if (vm.$v.questionGroup.$error) {
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
  async submitPreCreate(closeModal: boolean) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    var formData = new FormData();
    var wordFileUpload = vm.$refs.wordFileUpload;
    if (wordFileUpload && wordFileUpload["files"]) {
      formData.append(wordFileUpload["name"], wordFileUpload["files"][0]);
    }

    var excelFileUpload = vm.$refs.excelFileUpload;
    if (excelFileUpload && excelFileUpload["files"]) {
      formData.append(excelFileUpload["name"], excelFileUpload["files"][0]);
    }
    var params = util.toParam(this.questionGroup);

    return axios({
      method: "post",
      url: `${baseUrl}/PreCreate?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
      let data = response.data;
      debugger;
      if (data.MessageType == MessageType.Success) {
        vm["selectedTab"] = "previewTab";
        vm["isPreCreate"] = false;
        vm["previewImages"] = data.Obj;
      }
    });
  }

  @action()
  async submitCreate(closeModal: boolean) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    var formData = new FormData();
    var wordFileUpload = vm.$refs.wordFileUpload;
    if (wordFileUpload && wordFileUpload["files"]) {
      formData.append(wordFileUpload["name"], wordFileUpload["files"][0]);
    }

    var excelFileUpload = vm.$refs.excelFileUpload;
    if (excelFileUpload && excelFileUpload["files"]) {
      formData.append(excelFileUpload["name"], excelFileUpload["files"][0]);
    }
    var params = util.toParam(this.questionGroup);

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
    this.questionGroup.Id = 0;
    this.RESET(this._createVue);

    this._createVue["selectedTab"] = "preCreateTab";
    this._createVue["previewImages"] = [];
    this._createVue["isPreCreate"] = true;

    var word = this._createVue.$refs.wordFileUpload;
    if (word) {
      word["reset"]();
    }

    var excel = this._createVue.$refs.excelFileUpload;
    if (excel) {
      excel["reset"]();
    }
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.questionGroup)
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
      .post(`${baseUrl}/Delete/${this.questionGroup.Id}`)
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

export const questionGroupStore = QuestionGroupStore.ExtractVuexModule(
  QuestionGroupStore
);
