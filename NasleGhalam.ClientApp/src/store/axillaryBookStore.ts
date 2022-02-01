import Vue from "Vue";
import IAxillaryBook, { DefaultAxillaryBook } from "src/models/IAxillaryBook";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { AXILLARY_BOOK_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "axillaryBookStore/" })
export class AxillaryBookStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  axillaryBook: IAxillaryBook;
  private _axillaryBookList: Array<IAxillaryBook>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.axillaryBook = util.cloneObject(DefaultAxillaryBook);
    this._axillaryBookList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "کتاب کمک درسی";
  }

  get recordName() {
    return this.axillaryBook.Name || "";
  }

  get ddl() {
    return this._axillaryBookList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._axillaryBookList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(axillaryBook: IAxillaryBook) {
    this._axillaryBookList.push(axillaryBook);
  }

  @mutation
  private UPDATE(axillaryBook: IAxillaryBook) {
    let index = this._axillaryBookList.findIndex(
      x => x.Id == this.axillaryBook.Id
    );
    if (index < 0) return;
    util.mapObject(axillaryBook, this._axillaryBookList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._axillaryBookList.findIndex(
      x => x.Id == this.axillaryBook.Id
    );
    if (index < 0) return;
    this._axillaryBookList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultAxillaryBook, this.axillaryBook, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IAxillaryBook>) {
    this._axillaryBookList = list;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this._modelChanged = changed;
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
      .then((response: AxiosResponse<IAxillaryBook>) => {
        util.mapObject(response.data, this.axillaryBook);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IAxillaryBook>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._axillaryBookList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.axillaryBook.$touch();
      if (vm.$v.axillaryBook.$error) {
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

    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.axillaryBook);

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
        this.MODEL_CHANGED(true);
        this.resetCreate();
      }
    });
  }

  @action()
  async resetCreate() {
    this.axillaryBook.Id = 0;
    this.RESET(this._createVue);

    var fileUpload = this._createVue.$refs.fileUpload;
    if (fileUpload) {
      fileUpload["reset"]();
    }
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.axillaryBook);

    return axios({
      method: "post",
      url: `${baseUrl}/Update?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
      let data = response.data;
      this.notify({ vm, data });

      if (data.MessageType == MessageType.Success) {
        this.UPDATE(data.Obj);
        this.OPEN_MODAL_EDIT(false);
        this.MODEL_CHANGED(true);
        this.resetEdit();
      }
    });
  }

  @action()
  async resetEdit() {
    this.RESET(this._editVue);
    var fileUpload = this._editVue.$refs.fileUpload;
    if (fileUpload) {
      fileUpload["reset"]();
    }
  }

  @action()
  async submitDelete(vm: Vue) {
    return axios
      .post(`${baseUrl}/Delete/${this.axillaryBook.Id}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
          this.OPEN_MODAL_DELETE(false);
          this.MODEL_CHANGED(true);
        }
      });
  }
  //#endregion
}

export const axillaryBookStore = AxillaryBookStore.ExtractVuexModule(
  AxillaryBookStore
);
