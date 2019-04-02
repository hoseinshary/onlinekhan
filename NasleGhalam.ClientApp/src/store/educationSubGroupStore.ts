import Vue from "Vue";
import IEducationSubGroup, {
  DefaultEducationSubGroup
} from "src/models/IEducationSubGroup";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { EDUCATION_SUB_GROUP_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "educationSubGroupStore/" })
export class EducationSubGroupStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  educationSubGroup: IEducationSubGroup;
  private _educationSubGroupList: Array<IEducationSubGroup>;
  private _selectedId: number;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.educationSubGroup = util.cloneObject(DefaultEducationSubGroup);
    this._educationSubGroupList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "زیر گروه آموزشی";
  }

  get recordName() {
    return this.educationSubGroup.Name || "";
  }

  get ddl() {
    return this._educationSubGroupList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._educationSubGroupList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(educationSubGroup: IEducationSubGroup) {
    this._educationSubGroupList.push(educationSubGroup);
  }

  @mutation
  private UPDATE(educationSubGroup: IEducationSubGroup) {
    let index = this._educationSubGroupList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    util.mapObject(educationSubGroup, this._educationSubGroupList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._educationSubGroupList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    this._educationSubGroupList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultEducationSubGroup, this.educationSubGroup);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IEducationSubGroup>) {
    this._educationSubGroupList = list;
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
      .then((response: AxiosResponse<IEducationSubGroup>) => {
        util.mapObject(response.data, this.educationSubGroup);
        this.SET_SELECTED_ID(this.educationSubGroup.Id);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IEducationSubGroup>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._educationSubGroupList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.educationSubGroup.$touch();
      if (vm.$v.educationSubGroup.$error) {
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
      .post(`${baseUrl}/Create`, this.educationSubGroup)
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

    this.educationSubGroup.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.educationSubGroup)
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

export const educationSubGroupStore = EducationSubGroupStore.ExtractVuexModule(
  EducationSubGroupStore
);
