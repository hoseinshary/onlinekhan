import Vue from "Vue";
import ITopic, { DefaultTopic } from "src/models/ITopic";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { TOPIC_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "topicStore/" })
export class TopicStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  topic: ITopic;
  private _topicList: Array<ITopic>;
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

    this.topic = util.cloneObject(DefaultTopic);
    this._topicList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "مبحث";
  }

  get recordName() {
    return this.topic.Title || "";
  }

  get gridData() {
    return this._topicList;
  }

  get treeDataByLessonId() {
    var list = this._topicList
      .filter(x => x.LessonId == this.topic.LessonId)
      .map(x => ({
        Id: x.Id,
        label: x.Title,
        ParentTopicId: x.ParentTopicId,
        header: "custom"
      }));
    var tree = util.listToTree(list, "Id", "ParentTopicId");
    // set expanded list to show first level of tree
    this._expandedTreeData = tree && tree[0] ? [tree[0].Id] : [];
    return tree;
  }

  get expandedTreeData() {
    return this._expandedTreeData;
  }

  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(topic: ITopic) {
    this._topicList.push(topic);
  }

  @mutation
  private UPDATE(topic: ITopic) {
    let index = this._topicList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    util.mapObject(topic, this._topicList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._topicList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    this._topicList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultTopic, this.topic);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<ITopic>) {
    this._topicList = list;
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
      .then((response: AxiosResponse<ITopic>) => {
        util.mapObject(response.data, this.topic);
        this.SET_SELECTED_ID(this.topic.Id);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<ITopic>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._topicList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.topic.$touch();
      if (vm.$v.topic.$error) {
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
      .post(`${baseUrl}/Create`, this.topic)
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
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    this.topic.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.topic)
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

export const topicStore = TopicStore.ExtractVuexModule(TopicStore);
