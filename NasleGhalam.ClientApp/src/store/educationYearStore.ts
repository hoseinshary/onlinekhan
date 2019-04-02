import Vue from "Vue";
import IEducationYear, {
  DefaultEducationYear
} from "src/models/IEducationYear";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { EDUCATION_YEAR_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "educationYearStore/" })
export class EducationYearStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  educationYear: IEducationYear;
  private _educationYearList: Array<IEducationYear>;
  private _selectedId: number;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.educationYear = util.cloneObject(DefaultEducationYear);
    this._educationYearList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "سال تحصیلی";
  }

  get recordName() {
    return this.educationYear.Name || "";
  }

  get ddl() {
    return this._educationYearList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._educationYearList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(educationYear: IEducationYear) {
    this._educationYearList.push(educationYear);
  }

  @mutation
  private UPDATE(educationYear: IEducationYear) {
    let index = this._educationYearList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    util.mapObject(educationYear, this._educationYearList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._educationYearList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    this._educationYearList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultEducationYear, this.educationYear);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IEducationYear>) {
    this._educationYearList = list;
  }

  @mutation
  private SET_SELECTED_ID(id: number) {
    this._selectedId = id;
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
      .then((response: AxiosResponse<IEducationYear>) => {
        util.mapObject(response.data, this.educationYear);
        this.SET_SELECTED_ID(this.educationYear.Id);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IEducationYear>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._educationYearList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.educationYear.$touch();
      if (vm.$v.educationYear.$error) {
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
      .post(`${baseUrl}/Create`, this.educationYear)
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
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    this.educationYear.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.educationYear)
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
    this.RESET(this._editVue);
  }

  @action()
  async submitDelete(vm: Vue) {
    return axios
      .post(`${baseUrl}/Delete/${this._selectedId}`)
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

export const educationYearStore = EducationYearStore.ExtractVuexModule(
  EducationYearStore
);
