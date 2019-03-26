import Vue from "Vue";
import IProvince, { DefaultProvince } from "src/models/IProvince";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { PROVINCE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "provinceStore/" })
export class ProvinceStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  province: IProvince;
  private provinceList: Array<IProvince>;
  private selectedId: number;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.province = util.cloneObject(DefaultProvince);
    this.provinceList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "استان";
  }

  get recordName() {
    return this.province.Name || "";
  }

  get ddl() {
    return this.provinceList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this.provinceList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(province: IProvince) {
    this.provinceList.push(province);
  }

  @mutation
  private UPDATE(province: IProvince) {
    let index = this.provinceList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    util.mapObject(province, this.provinceList[index]);
  }

  @mutation
  private DELETE() {
    let index = this.provinceList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    this.provinceList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultProvince, this.province);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IProvince>) {
    this.provinceList = list;
  }

  @mutation
  private SET_SELECTED_ID(id: number) {
    this.selectedId = id;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this.modelChanged = changed;
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
    this.createVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this.editVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IProvince>) => {
        util.mapObject(response.data, this.province);
        this.SET_SELECTED_ID(this.province.Id);
      });
  }

  @action()
  fillList() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IProvince>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.provinceList);
    }
  }

  @action({ mode: "raw" })
  validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.province.$touch();
      if (vm.$v.province.$error) {
        const context = getRawActionContext(this);
        context.dispatch("notifyInvalidForm", vm, { root: true });
        resolve(false);
      }
      resolve(true);
    });
  }

  @action({ mode: "raw" })
  notify(payload: { vm: Vue; data: IMessageResult }) {
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
    let vm = this.createVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Create`, this.province)
      .then((response: AxiosResponse<IMessageResult>) => {
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
    this.RESET(this.createVue);
  }

  @action()
  async submitEdit() {
    let vm = this.createVue;
    if (!(await this.validateForm(vm))) return;

    this.province.Id = this.selectedId;
    return axios
      .post(`${baseUrl}/Update/${this.selectedId}`, this.province)
      .then((response: AxiosResponse<IMessageResult>) => {
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
    this.RESET(this.editVue);
  }

  @action()
  async submitDelete() {
    let vm = this.createVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Delete/${this.selectedId}`)
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

export const provinceStore = ProvinceStore.ExtractVuexModule(ProvinceStore);
