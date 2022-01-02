import Vue from "Vue";
import IEducationBook, {
  DefaultEducationBook
} from "src/models/IEducationBook";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { EDUCATION_BOOK_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "educationBookStore/" })
export class EducationBookStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  educationBook: IEducationBook;
  private _educationBookList: Array<IEducationBook>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.educationBook = util.cloneObject(DefaultEducationBook);
    this._educationBookList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "کتاب درسی";
  }

  get recordName() {
    return this.educationBook.Name || "";
  }

  get ddl() {
    return this._educationBookList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._educationBookList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(educationBook: IEducationBook) {
    this._educationBookList.push(educationBook);
  }

  @mutation
  private UPDATE(educationBook: IEducationBook) {
    let index = this._educationBookList.findIndex(
      x => x.Id == this.educationBook.Id
    );
    if (index < 0) return;
    util.mapObject(educationBook, this._educationBookList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._educationBookList.findIndex(
      x => x.Id == this.educationBook.Id
    );
    if (index < 0) return;
    this._educationBookList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultEducationBook, this.educationBook, "Id", "LessonId");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IEducationBook>) {
    this._educationBookList = list;
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
      .then((response: AxiosResponse<IEducationBook>) => {
        util.mapObject(response.data, this.educationBook);
        this.educationBook.TopicIds = response.data.Topics.map(x => x.Id);
      });
  }

  @action()
  async fillListByLessonId(lessonId: number) {
    return axios
      .get(`${baseUrl}/GetAllByLessonId/${lessonId}`)
      .then((response: AxiosResponse<Array<IEducationBook>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.educationBook.$touch();
      if (vm.$v.educationBook.$error) {
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
      .post(`${baseUrl}/Create`, this.educationBook)
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
    this.educationBook.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.educationBook)
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
      .post(`${baseUrl}/Delete/${this.educationBook.Id}`)
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

export const educationBookStore = EducationBookStore.ExtractVuexModule(
  EducationBookStore
);
