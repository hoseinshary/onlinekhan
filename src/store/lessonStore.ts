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
  private _modelChanged: boolean = true;
  private _selectedId: number;
  private _createVue: Vue;
  private _editVue: Vue;


  // ati
  private _imgPath:string;

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
    this._imgPath = ""
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

  get gridDataByEducationTreeIds() {
    return (ids: Array<number>) => {
      return this._lessonList.filter(
        lesson =>
          lesson.EducationTrees &&
          lesson.EducationTrees.some(
            educationTree => ids.indexOf(educationTree.Id) > -1
          )
      );
    };
  }

  get relatedLessonIds() {
    return (lessonId: number) => {
      var currentLesson = this._lessonList.find(
        lesson => lesson.Id == lessonId
      );
      var currentDepartmentIds: Array<number> =
        (currentLesson &&
          currentLesson.LessonDepartments &&
          currentLesson.LessonDepartments.map(x => x.Id)) ||
        [];

      return this._lessonList
        .filter(
          lesson =>
            currentLesson &&
            lesson.Id != currentLesson.Id &&
            lesson.LessonDepartments &&
            lesson.LessonDepartments.some(department =>
              currentDepartmentIds.some(
                currentDepartment => currentDepartment == department.Id
              )
            )
        )
        .map(x => x.Id);
    };
  }

  get relatedLessons() {
    return (lessonId: number) => {
      var currentLesson = this._lessonList.find(
        lesson => lesson.Id == lessonId
      );
      var currentDepartmentIds: Array<number> =
        (currentLesson &&
          currentLesson.LessonDepartments &&
          currentLesson.LessonDepartments.map(x => x.Id)) ||
        [];

      return this._lessonList.filter(
        lesson =>
          currentLesson &&
          lesson.Id != currentLesson.Id &&
          lesson.LessonDepartments &&
          lesson.LessonDepartments.some(department =>
            currentDepartmentIds.some(
              currentDepartment => currentDepartment == department.Id
            )
          )
      );
    };
  }

  get ddl() {
    return this._lessonList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get ddlByEducationTreeIds() {
    
    return (ids: Array<number>) => {
      
      var x  =this._lessonList
      .filter(
        lesson =>
          lesson.EducationTrees &&
          lesson.EducationTrees.some(
            educationTree => ids.indexOf(educationTree.Id) > -1
          )
      )
      .map(x => ({
        value: x.Id,
        label: x.Name
      }));
      //debugger;
      return x;
    };
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
            LessonId: this.lesson.Id
          });
        })
      );
  }

  @mutation
  private SET_LIST(list: Array<ILesson>) {
    this._lessonList = list;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this._modelChanged = changed;
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
        this.lesson.LessonDepartmentId =
          this.lesson.LessonDepartments != undefined &&
          this.lesson.LessonDepartments.length > 0
            ? this.lesson.LessonDepartments[0].Id
            : 0;
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<ILesson>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._lessonList);
    }
  }

  // @action()
  // async fillListByEducationTreeIds(ids: Array<number>) {
  //   let params = util.toParam({ ids });
  //   return axios
  //     .get(`${baseUrl}/GetAllByEducationTreeIds?${params}`)
  //     .then((response: AxiosResponse<Array<ILesson>>) => {
  //       this.SET_LIST(response.data);
  //     });
  // }

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
    if (!this.lesson.LessonDepartmentId) {
      this.lesson.LessonDepartmentId = 0;
    }
    return axios
      .post(`${baseUrl}/Create`, this.lesson)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.CREATE(data.Obj);
          this.OPEN_MODAL_CREATE(!payload.closeModal);
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
  async submitEdit(educationGroup: any) {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    // ati
    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.lesson);


    // this.SET_LESSON_RATIO(educationGroup);
    // this.lesson.Id = this._selectedId;
    // if (!this.lesson.LessonDepartmentId) {
    //   this.lesson.LessonDepartmentId = 0;
    // }

    // instead of 'this.lesson' , params
    return axios({
      method: "post",
      url: `${baseUrl}/Update/${this._selectedId}?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    })
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

export const lessonStore = LessonStore.ExtractVuexModule(LessonStore);
