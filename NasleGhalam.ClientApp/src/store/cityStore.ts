import Vue from "Vue";
import ICity, { DefaultCity } from "src/models/ICity";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { CITY_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "cityStore/" })
export class CityStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  city: ICity;
  private cityList: Array<ICity>;
  private selectedId: number = 0;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.city = util.cloneObject(DefaultCity);
    this.cityList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "شهر";
  }

  get recordName() {
    return this.city.Name || "";
  }

  get Ddl() {
    return this.cityList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this.cityList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(city: ICity) {
    this.cityList.push(city);
  }

  @mutation
  private UPDATE(city: ICity) {
    let index = this.cityList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    util.mapObject(city, this.cityList[index]);
  }

  @mutation
  private DELETE() {
    let index = this.cityList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    this.cityList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultCity, this.city);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<ICity>) {
    this.cityList = list;
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
      .then((response: AxiosResponse<ICity>) => {
        util.mapObject(response.data, this.city);
        this.SET_SELECTED_ID(this.city.Id);
      });
  }

  @action()
  fillList() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<ICity>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.cityList);
    }
  }

  @action({ mode: "raw" })
  validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.city.$touch();
      if (vm.$v.city.$error) {
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
      .post(`${baseUrl}/Create`, this.city)
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

    this.city.Id = this.selectedId;
    return axios
      .post(`${baseUrl}/Update/${this.selectedId}`, this.city)
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

export const cityStore = CityStore.ExtractVuexModule(CityStore);
