import Vue from "Vue";
import IEducationTree, {
  DefaultEducationTree
} from "src/models/IEducationTree";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { EDUCATION_TREE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "educationTreeStore/" })
export class EducationTreeStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  educationTree: IEducationTree;
  private educationTreeList: Array<IEducationTree>;
  private selectedId: number;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;
  treeExpandedData: Array<object> = [];
  _this = this;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.educationTree = util.cloneObject(DefaultEducationTree);
    this.educationTreeList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    debugger;
    return "درخت آموزش";
  }

  get recordName() {
    return this.educationTree.Name || "";
  }

  get ddl() {
    return this.educationTreeList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get treeData() {
    var list = this.educationTreeList.map(x => ({
      Id: x.Id,
      LookupId_EducationTreeState: x.LookupId_EducationTreeState,
      Lookup_EducationTreeState: x.Lookup_EducationTreeState,
      label: x.Name,
      ParentEducationTreeId: x.ParentEducationTreeId
    }));

    var tree = util.listToTree(list, "Id", "ParentEducationTreeId");
    debugger;
    if (tree && tree[0]) this.treeExpandedData = [tree[0].Id];
    return tree;
  }

  get expanded() {
    return this.treeExpandedData;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(educationTree: IEducationTree) {
    this.educationTreeList.push(educationTree);
  }

  @mutation
  private UPDATE(educationTree: IEducationTree) {
    let index = this.educationTreeList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    util.mapObject(educationTree, this.educationTreeList[index]);
  }

  @mutation
  private DELETE() {
    let index = this.educationTreeList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    this.educationTreeList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultEducationTree, this.educationTree);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IEducationTree>) {
    this.educationTreeList = list;
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
      .then((response: AxiosResponse<IEducationTree>) => {
        util.mapObject(response.data, this.educationTree);
        this.SET_SELECTED_ID(this.educationTree.Id);
      });
  }

  @action()
  async fillList() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IEducationTree>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.educationTreeList);
    }
  }

  @action({ mode: "raw" })
  validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.educationTree.$touch();
      if (vm.$v.educationTree.$error) {
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
      .post(`${baseUrl}/Create`, this.educationTree)
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

    this.educationTree.Id = this.selectedId;
    return axios
      .post(`${baseUrl}/Update/${this.selectedId}`, this.educationTree)
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
  async submitDelete(vm: Vue) {
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

export const educationTreeStore = EducationTreeStore.ExtractVuexModule(
  EducationTreeStore
);
