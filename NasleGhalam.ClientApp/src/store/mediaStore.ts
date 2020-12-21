import Vue from "Vue";
import IMedia, { DefaultMedia } from "src/models/IMedia";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { MEDIA_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "mediaStore/" })
export class MediaStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  media: IMedia;
  private _mediaList: Array<IMedia>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.media = util.cloneObject(DefaultMedia);
    this._mediaList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "رسانه";
  }

  get recordName() {
    return this.media.Title || "";
  }



  get gridData() {
    return this._mediaList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(media: IMedia) {
    this._mediaList.push(media);
  }

  @mutation
  private UPDATE(media: IMedia) {
    let index = this._mediaList.findIndex(x => x.Id == this.media.Id);
    if (index < 0) return;
    util.mapObject(media, this._mediaList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._mediaList.findIndex(x => x.Id == this.media.Id);
    if (index < 0) return;
    this._mediaList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultMedia, this.media, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IMedia>) {
    this._mediaList = list;
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
      .then((response: AxiosResponse<IMedia>) => {
        util.mapObject(response.data, this.media);
        if (response.data.Topics) {
          this.media.TopicIds = response.data.Topics.map(x => x.Id);
        }
      });
  }


  @action()
  async fillList(payload: {
    lessonId: number;
    topicIds: Array<number>;
  }) {

    var url = "";
    var params = util.toParam({
      Ids: payload.topicIds
    });

    url = `${baseUrl}/GetAllByTopicIds?${params}`;
    return axios.get(url).then((response: AxiosResponse<Array<IMedia>>) => {
      this.SET_LIST(response.data);
    });
  }

 

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.media.$touch();
      if (vm.$v.media.$error) {
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


    var file = vm.$refs.file;
    var CoverImage = vm.$refs.CoverImage;
    var msg = "";

    if (file["files"].length == 0) {
      msg = "فایل انتخاب نشده است.<br/>";
    }

    if (msg) {
      this.notify({
        vm: vm,
        data: {
          Message: msg,
          MessageType: MessageType.Error,
          Obj: null
        }
      });
      return;
    }
    
    var formData = new FormData();
    formData.append(file["name"], file["files"][0]);
    formData.append(CoverImage["name"],CoverImage["files"][0]);

    var newData = {
      Id: this.media.Id,
      Title: this.media.Title,
      WriterId: this.media.WriterId,
      Price: this.media.Price,
      Description: this.media.Description,
      LookupId_MediaType: this.media.LookupId_MediaType,
      TopicIds: this.media.TopicIds,
      Length:this.media.Length,
      YearOfBook : this.media.YearOfBook,
      PagesOfBook : this.media.PagesOfBook
    };
    var params = util.toParam(newData);


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
    this.media.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    var file = vm.$refs.file;

    var formData = new FormData();
    formData.append(file["name"], file["files"][0]);

    var newData = {
      Id: this.media.Id,
      Title: this.media.Title,
      WriterId: this.media.WriterId,
      Price: this.media.Price,
      Description: this.media.Description,
      LookupId_MediaType: this.media.LookupId_MediaType,
      TopicIds: this.media.TopicIds,
    };
    var params = util.toParam(newData);

    return axios({
      method: "post",
      url: `${baseUrl}/update?${params}`,
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
  }

  @action()
  async submitDelete(vm: Vue) {
    return axios
      .post(`${baseUrl}/Delete/${this.media.Id}`)
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

export const mediaStore = MediaStore.ExtractVuexModule(MediaStore);
