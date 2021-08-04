import Vue from "Vue";
import IStudentMajorList, { DefaultStudentMajorList, Major  } from "src/models/IStudentMajorList";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { STUDENTMAJORLIST_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "studentMajorListStore/" })
export class StudentMajorListStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  studentMajorList: IStudentMajorList;
  majorsList: Major;

  private _studentMajorListList: Array<IStudentMajorList>;
  private _majorList : Array<Major>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.studentMajorList = util.cloneObject(DefaultStudentMajorList);
    this._majorList = [];
    this._studentMajorListList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "انتخاب رشته";
  }

  get recordName() {
    return this.studentMajorList.Title || "";
  }

  get ddl() {
    return this._studentMajorListList.map(x => ({
      value: x.Id,
      label: x.Title
    }));
  }

  get gridData() {
    return this._studentMajorListList;
  }

  get gridDataMajor() {
    return this._majorList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(studentMajorList: IStudentMajorList) {
    this._studentMajorListList.push(studentMajorList);
  }

  @mutation
  private UPDATE(studentMajorList: IStudentMajorList) {
    let index = this._studentMajorListList.findIndex(x => x.Id == this.studentMajorList.Id);
    if (index < 0) return;
    util.mapObject(studentMajorList, this._studentMajorListList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._studentMajorListList.findIndex(x => x.Id == this.studentMajorList.Id);
    if (index < 0) return;
    this._studentMajorListList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultStudentMajorList, this.studentMajorList, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IStudentMajorList>) {
    this._studentMajorListList = list;
  }

  @mutation
  private SET_LIST_Major(list: Array<Major>) {
    //console.log(list);
    this._majorList = list;
    //console.log(this._majorList);
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
      .then((response: AxiosResponse<IStudentMajorList>) => {
        util.mapObject(response.data, this.studentMajorList);
      });
  }

  @action()
  async getMajorsBySearch(text : string) {
    
      return axios
        .get(`${baseUrl}/GetMajorsBySearch/?text=${text}`)
        .then((response: AxiosResponse<Array<Major>>) => {
          this.SET_LIST_Major(response.data);
        });
    
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetStudentById`)
        .then((response: AxiosResponse<Array<IStudentMajorList>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._studentMajorListList);
    }
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.studentMajorList.$touch();
      if (vm.$v.studentMajorList.$error) {
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

    var data = {
      Title: this.studentMajorList.Title,
      MajorsId: this.studentMajorList.Majors.map(x=>x.Id)
    };
    return axios
      .post(`${baseUrl}/Create`, data)
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
    this.studentMajorList.Id = 0;
    this.studentMajorList.Majors =[];
    this.SET_LIST_Major([]);
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;
    var data = {
      Title: this.studentMajorList.Title,
      MajorsId: this.studentMajorList.Majors.map(x=>x.Id),
      Id:this.studentMajorList.Id,
      StudentId:this.studentMajorList.StudentId
    };
    return axios
      .post(`${baseUrl}/Update`, data)
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
      .post(`${baseUrl}/Delete/${this.studentMajorList.Id}`)
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

export const studentMajorListStore = StudentMajorListStore.ExtractVuexModule(StudentMajorListStore);
