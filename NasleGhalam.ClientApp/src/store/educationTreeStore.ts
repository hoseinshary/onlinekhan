import Vue from "Vue";
import IEducationTree, {
  DefaultEducationTree
} from "src/models/IEducationTree";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType, EducationTreeState } from "src/utilities/enumeration";
import { EDUCATION_TREE_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getter,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "educationTreeStore/" })
export class EducationTreeStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  educationTree: IEducationTree;
  private _educationTreeList: Array<IEducationTree> = [];
  private _selectedId: number;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;
  private _expandedTreeData: Array<object> = [];

  /**
   * initialize data
   */
  constructor() {
    super();

    this.educationTree = util.cloneObject(DefaultEducationTree);
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "درخت آموزش";
  }

  get recordName() {
    return this.educationTree.Name || "";
  }

  get gridData() {
    return this._educationTreeList;
  }

  get treeData() {
    var list = this._educationTreeList.map(x => ({
      Id: x.Id,
      label:
        x.Name +
        (x.Lookup_EducationTreeState == undefined
          ? ""
          : ` (${x.Lookup_EducationTreeState.Value})`),
      ParentEducationTreeId: x.ParentEducationTreeId,
      header: "custom"
    }));
    var tree = util.listToTree(list, "Id", "ParentEducationTreeId");
    // set expanded list to show first level of tree
    this._expandedTreeData = tree && tree[0] ? [tree[0].Id] : [];
    return tree;
  }

  /**
   * فیلتر درخت با مقطع
   */
  get treeDataByEducationTreeId() {
    var list = this._educationTreeList.map(x => ({
      Id: x.Id,
      label:
        x.Name +
        (x.Lookup_EducationTreeState == undefined
          ? ""
          : ` (${x.Lookup_EducationTreeState.Value})`),
      ParentEducationTreeId: x.ParentEducationTreeId,
      header: "custom"
    }));
    var tree = util.listToTree(list, "Id", "ParentEducationTreeId");
    // set expanded list to show first level of tree
    this._expandedTreeData = tree && tree[0] ? [tree[0].Id] : [];

    return (educationTreeId: number | null) => {
      if (!educationTreeId) {
        return tree;
      } else {
        return tree[0].children.filter(x => x.Id == educationTreeId);
      }
    };
  }

  get expandedTreeData() {
    return this._expandedTreeData;
  }

  get byStateDdl() {
    return (state: EducationTreeState) => {
      return this._educationTreeList
        .filter(
          x =>
            x.Lookup_EducationTreeState != undefined &&
            x.Lookup_EducationTreeState.Name == "EducationTreeState" &&
            x.Lookup_EducationTreeState.State == state
        )
        .map(x => ({
          value: x.Id,
          label: x.Name
        }));
    };
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(educationTree: IEducationTree) {
    this._educationTreeList.push(educationTree);
  }

  @mutation
  private UPDATE(educationTree: IEducationTree) {
    let index = this._educationTreeList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    util.mapObject(educationTree, this._educationTreeList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._educationTreeList.findIndex(
      x => x.Id == this._selectedId
    );
    if (index < 0) return;
    this._educationTreeList.splice(index, 1);
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
    this._educationTreeList = list;
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
      .then((response: AxiosResponse<IEducationTree>) => {
        util.mapObject(response.data, this.educationTree);
        this.SET_SELECTED_ID(this.educationTree.Id);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IEducationTree>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._educationTreeList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
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
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    this.educationTree.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.educationTree)
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

export const educationTreeStore = EducationTreeStore.ExtractVuexModule(
  EducationTreeStore
);
