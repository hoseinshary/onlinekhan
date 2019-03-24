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
  province: IProvince;
  provinceList: Array<IProvince>;
  openModal: { create: boolean; edit: boolean; delete: boolean } = {
    create: false,
    edit: false,
    delete: false
  };
  private selectedId: number = 0;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.provinceList = [];
    this.province = DefaultProvince;
  }

  //#region ### internal functions ###
  private getIndexById(id: number) {
    return this.provinceList.findIndex(x => x.Id == id);
  }

  private findById(id: number) {
    return this.provinceList.find(x => x.Id == id);
  }
  //#endregion

  //#region ### getters ###
  readonly modelName = "استان";
  readonly recordName = (this.province && this.province.Name) || "";

  get provinceDdl() {
    return this.provinceList.map(x => ({
      value: x.Id,
      name: x.Name
    }));
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(province: IProvince) {
    this.provinceList.push(province);
  }

  @mutation
  private UPDATE(province: IProvince) {
    let index = this.getIndexById(province.Id);
    if (index < 0) return;

    this.provinceList[index].Id = province.Id;
    this.provinceList[index].Name = province.Name;
  }

  @mutation
  private DELETE(id: number) {
    let index = this.getIndexById(id);
    if (index < 0) return;

    this.provinceList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    this.province = DefaultProvince;
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_PROVINCE_LIST(list: Array<IProvince>) {
    this.provinceList = list;
  }

  @mutation
  private TOGGLE_MODEL_CHANGED(b: boolean) {
    this.modelChanged = b;
  }

  @mutation
  TOGGLE_MODAL_CREATE(open: boolean) {
    this.openModal.create = open;
  }

  @mutation
  TOGGLE_MODAL_EDIT(open: boolean) {
    this.openModal.edit = open;
  }

  @mutation
  TOGGLE_MODAL_DELETE(open: boolean) {
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
  getById() {
    return axios
      .get(`${baseUrl}/GetById`)
      .then((response: AxiosResponse<IProvince>) => {
        this.province.Id = response.data.Id;
        this.province.Name = response.data.Name;
      });
  }

  @action()
  getAll() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IProvince>>) => {
          this.SET_PROVINCE_LIST(response.data);
          this.TOGGLE_MODEL_CHANGED(false);
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
          this.CREATE(this.province);
          this.TOGGLE_MODAL_CREATE(closeModal);
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.RESET(this.createVue);
  }
  //#endregion
}

export const provinceStore = ProvinceStore.ExtractVuexModule(ProvinceStore);
