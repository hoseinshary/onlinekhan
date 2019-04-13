import Vue from "Vue";
import ILesson, { DefaultLesson } from "src/models/ILesson";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { LESSON_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "lessonStore/" })
export class LessonStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  lesson: ILesson;
  private _lessonList: Array<ILesson>;
  private _selectedId: number;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.lesson = util.cloneObject(DefaultLesson);
    this._lessonList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "درس";
  }

  get recordName() {
    return this.lesson.Name || "";
  }

  get gridData() {
    return this._lessonList;
  }

  get ddl() {
    return this._lessonList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(lesson: ILesson) {
    this._lessonList.push(lesson);
  }

  @mutation
  private UPDATE(lesson: ILesson) {
    let index = this._lessonList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    util.mapObject(lesson, this._lessonList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._lessonList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    this._lessonList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultLesson, this.lesson);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_OBJECT(lesson: ILesson) {
    util.mapObject(lesson, this.lesson);
    if (lesson.EducationTrees)
      this.lesson.EducationTreeIds = lesson.EducationTrees.map(x => x.Id);
  }

  @mutation
  private SET_LESSON_RATIO(educationGroup: any) {
    util.clearArray(this.lesson.Ratios);
    educationGroup
      .filter(x => x.IsChecked)
      .map(x =>
        x.EducationSubGroup.filter(
          y => y.Rate != undefined && y.Rate != 0
        ).forEach(item => {
          this.lesson.Ratios.push({
            EducationSubGroupId: item.Id,
            Rate: item.Rate,
            EducationTreeId: 0,
            Id: 0,
            LessonId: 0
          });
        })
      );
  }

  @mutation
  private SET_LIST(list: Array<ILesson>) {
    this._lessonList = list;
  }

  @mutation
  private SET_SELECTED_ID(id: number) {
    this._selectedId = id;
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
      .then((response: AxiosResponse<ILesson>) => {
        this.SET_OBJECT(response.data);
        this.SET_SELECTED_ID(this.lesson.Id);
      });
  }

  @action()
  async fillListByEducationTreeIds(ids: Array<number>) {
    let params = util.toParam({ ids });
    return axios
      .get(`${baseUrl}/GetAllByEducationTreeIds?${params}`)
      .then((response: AxiosResponse<Array<ILesson>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.lesson.$touch();
      if (vm.$v.lesson.$error) {
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
  async submitCreate(payload: any) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    this.SET_LESSON_RATIO(payload.educationGroup);
    return axios
      .post(`${baseUrl}/Create`, this.lesson)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.CREATE(data.Obj);
          this.OPEN_MODAL_CREATE(!payload.closeModal);
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit(educationGroup) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    this.SET_LESSON_RATIO(educationGroup);
    this.lesson.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.lesson)
      .then((response: AxiosResponse<IMessageResult>) => {
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
      .post(`${baseUrl}/Delete/${this._selectedId}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
          this.OPEN_MODAL_DELETE(false);
        }
      });
  }
  //#endregion
}

export const lessonStore = LessonStore.ExtractVuexModule(LessonStore);
