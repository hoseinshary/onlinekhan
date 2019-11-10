import Vue from "Vue";
import IPackage, { DefaultPackage } from "src/models/IPackage";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { PACKAGE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "packageStore/" })
export class PackageStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  thePackage: IPackage;
  private _packageList: Array<IPackage>;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.thePackage = util.cloneObject(DefaultPackage);
    this._packageList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "بسته";
  }

  get recordName() {
    return this.thePackage.Name || "";
  }

  get ddl() {
    return this._packageList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._packageList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(pkg: IPackage) {
    this._packageList.push(pkg);
  }

  @mutation
  private UPDATE(pkg: IPackage) {
    let index = this._packageList.findIndex(x => x.Id == this.thePackage.Id);
    if (index < 0) return;
    util.mapObject(pkg, this._packageList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._packageList.findIndex(x => x.Id == this.thePackage.Id);
    if (index < 0) return;
    this._packageList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultPackage, this.thePackage, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IPackage>) {
    this._packageList = list;
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
      .then((response: AxiosResponse<IPackage>) => {
        util.mapObject(response.data, this.thePackage);
        if (response.data.Lessons) {
          this.thePackage.LessonIds = response.data.Lessons.map(x => x.Id);
        }
      });
  }

  @action()
  async fillByEducationTreeIdList(ids: Array<number>) {
    var params = util.toParam({
      Ids: ids
    });
    return axios
      .get(`${baseUrl}/GetAllByEducationTreeId?${params}`)
      .then((response: AxiosResponse<Array<IPackage>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.thePackage.$touch();
      if (vm.$v.thePackage.$error) {
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

    if (this.thePackage.LessonIds && this.thePackage.LessonIds.length == 0) {
      this.notify({
        vm: vm,
        data: {
          Message: "درس انتخاب نشده است",
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }

    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.thePackage);

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
    this.thePackage.Id = 0;
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

    if (this.thePackage.LessonIds && this.thePackage.LessonIds.length == 0) {
      this.notify({
        vm: vm,
        data: {
          Message: "درس انتخاب نشده است",
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }
    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.thePackage);

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
      .post(`${baseUrl}/Delete/${this.thePackage.Id}`)
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

export const packageStore = PackageStore.ExtractVuexModule(PackageStore);
