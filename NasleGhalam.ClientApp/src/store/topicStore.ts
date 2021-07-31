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
  private _topicListByLessonID: Array<ITopic>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;
  qTreeData: {
    id: number;
    filter: string;
    selected: any;
    expanded: Array<object>;
    leafTicked: Array<number>;
    nodeTicked: Array<number>;
    firstLevel: any;
    setToFirstLevel: boolean;
  };

  /**
   * initialize data
   */
  constructor() {
    super();

    this.topic = util.cloneObject(DefaultTopic);
    this._topicListByLessonID = [];
    this._topicList = [];
    this.qTreeData = {
      id: 0,
      filter: "",
      selected: null,
      expanded: [],
      leafTicked: [],
      nodeTicked: [],
      firstLevel: null,
      setToFirstLevel: false
    };
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

  get detail() {
    return (id: number) => {
      return this._topicList.find(x => x.Id == id);
    };
  }

  get gridData() {
    return this._topicList;
  }

  get treeDataByLessonId() {
    return (lessonId: number) => {
    
      var list = this._topicList
        .filter(x => x.LessonId == lessonId)
        .map(x => ({
          Id: x.Id,
          label: x.Title,
          ParentTopicId: x.ParentTopicId,
          header: "custom",
          displayPriority: x.DisplayPriority
        }));
      var tree = util.listToTree(
        list,
        "Id",
        "ParentTopicId",
        "displayPriority"
      );
      // set expanded list to show first level of tree
      this.qTreeData.firstLevel = tree && tree[0] ? [tree[0].Id] : [];

      return tree;
    };
  }

  get treeDataByLessonId2() {
    var list = this._topicListByLessonID
      .map(x => ({
        Id: x.Id,
        label: x.Title,
        ParentTopicId: x.ParentTopicId,
        header: "custom",
        displayPriority: x.DisplayPriority
      }));
    var tree = util.listToTree(
      list,
      "Id",
      "ParentTopicId",
      "displayPriority"
    );
    // set expanded list to show first level of tree
    this.qTreeData.firstLevel = tree && tree[0] ? [tree[0].Id] : [];

    return tree;
  }

  get treeDataByLessonIds() {
    
    return (lessonIds: Array<number>) => {
      
      var list = this._topicList
        .filter(x => lessonIds.indexOf(x.LessonId) > -1)
        .map(x => ({
          Id: x.Id,
          label: x.Title,
          ParentTopicId: x.ParentTopicId,
          lessonId: x.LessonId,
          header: "custom",
          displayPriority: x.DisplayPriority
        }));
      var tree = util.listToTree(
        list,
        "Id",
        "ParentTopicId",
        "displayPriority"
      );
      return tree;
    };
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(topic: ITopic) {
    this._topicList.push(topic);
  }

  @mutation
  private UPDATE(topic: ITopic) {
    let index = this._topicList.findIndex(x => x.Id == this.topic.Id);
    if (index < 0) return;
    util.mapObject(topic, this._topicList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._topicList.findIndex(x => x.Id == this.topic.Id);
    if (index < 0) return;
    this._topicList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultTopic, this.topic, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<ITopic>) {
    this._topicList = list;
  }

  @mutation
  private SET_LIST_BY_LESSON_ID(list: Array<ITopic>) {
    this._topicListByLessonID = list;
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


  @action()
  async fillList3Level() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll3level`)
        .then((response: AxiosResponse<Array<ITopic>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._topicList);
    }
  }


  //گرفتن تمام مبحث های یک درس
  @action()
  async fillListByLessonId(payload: { id: number; showNumberForTopic: boolean; }) {

    if (payload.showNumberForTopic) {
      return axios
        .get(`${baseUrl}/GetAllByLessonIdWithCountQuestion/${payload.id}`)
        .then((response: AxiosResponse<Array<ITopic>>) => {
          this.SET_LIST_BY_LESSON_ID(response.data);
          this.MODEL_CHANGED(false);
        });
    }
    else {
      return axios
        .get(`${baseUrl}/GetAllByLessonId/${payload.id}`)
        .then((response: AxiosResponse<Array<ITopic>>) => {
          this.SET_LIST_BY_LESSON_ID(response.data);
          this.MODEL_CHANGED(false);
        });
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
          let topic = data.Obj;
          // make node selected
          this.qTreeData.selected = topic.Id;
          // show created node in tree
          let parentId: any = topic.ParentTopicId;
          if (parentId && this.qTreeData.expanded.indexOf(parentId)) {
            this.qTreeData.expanded.push(parentId);
          }

          this.CREATE(data.Obj);
          this.OPEN_MODAL_CREATE(!closeModal);
          this.MODEL_CHANGED(true);
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.topic.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.topic)
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
      .post(`${baseUrl}/Delete/${this.topic.Id}`)
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
