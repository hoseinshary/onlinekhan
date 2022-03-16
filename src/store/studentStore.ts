import Vue from "Vue";
import IStudent, { DefaultStudent } from "src/models/IStudent";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { STUDENT_URL as baseUrl } from "src/utilities/site-config";
import { AssayNumberOfQuestionReport ,AssayNumberOfQuestionReportForTopic} from "src/models/IAssay";

import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

import router from "src/router";

@Module({ namespacedPath: "studentStore/" })
export class StudentStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  student: IStudent;
  private _studentList: Array<IStudent>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;
  private _lessonAssayVue : Vue;

  private _registerVue: Vue;


  private _registerStudentVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.student = util.cloneObject(DefaultStudent);
    this._studentList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "دانش آموز";
  }

  get recordName() {
    return this.student.User.FullName || "";
  }

  get ddl() {
    return this._studentList.map(x => ({
      value: x.Id,
      label: x.User.FullName
    }));
  }

  get gridData() {
    return this._studentList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(student: IStudent) {
    this._studentList.push(student);
  }

  @mutation
  private UPDATE(student: IStudent) {
    let index = this._studentList.findIndex(x => x.Id == this.student.Id);
    if (index < 0) return;
    util.mapObject(student, this._studentList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._studentList.findIndex(x => x.Id == this.student.Id);
    if (index < 0) return;
    this._studentList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultStudent, this.student, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IStudent>) {
    this._studentList = list;
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
  SET_LESSON_ASSAY_VUE(vm: Vue) {
    this._lessonAssayVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this._editVue = vm;
  }

  @mutation
  SET_REGISTER_STUDENT_VUE(vm:Vue){
    this._registerStudentVue = vm;
  }

  //#endregion

  //#region ### actions ###
  @action()
  async getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IStudent>) => {
        util.mapObject(response.data, this.student);
      });
  }

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IStudent>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._studentList);
    }
  }

  @action()
  async numberOfQuestionReport(payload:{lessonId :number , studentId : number}){
    let vm = this._lessonAssayVue;
    return axios
    .post(`${baseUrl}/GetQuestionAssayReportByLessonId`, payload)
    .then((response : AxiosResponse<AssayNumberOfQuestionReport>) => {
      let data = response.data;
      //debugger;
      // vm["$data"].numberOfQuestionReport.NumberOfNewQuestions = data.NumberOfNewQuestions;
      // vm["numberOfQuestionReport.NumberOfAssayQuestions"] = data.NumberOfAssayQuestions;
      // vm["numberOfQuestionReport.NumberOfHomeworkQuestions"] = data.NumberOfHomeworkQuestions;

      // console.log("store",vm["numberOfQuestionReport.NumberOfNewQuestions"]);
      // debugger;
      return data;
    });
  }

  @action()
  async numberOfQuestionReportForTopic(payload:{lessonIds :Array<number> , studentId : number}){
    let vm = this._lessonAssayVue;
    return axios
    .post(`${baseUrl}/GetQuestionAssayReportByLessonIds`, payload)
    .then((response : AxiosResponse<AssayNumberOfQuestionReportForTopic>) => {
      let data = response.data;
      //debugger;
      // vm["$data"].numberOfQuestionReport.NumberOfNewQuestions = data.NumberOfNewQuestions;
      // vm["numberOfQuestionReport.NumberOfAssayQuestions"] = data.NumberOfAssayQuestions;
      // vm["numberOfQuestionReport.NumberOfHomeworkQuestions"] = data.NumberOfHomeworkQuestions;

      // console.log("store",vm["numberOfQuestionReport.NumberOfNewQuestions"]);
      // debugger;
      return data;
    });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.student.$touch();
      if (vm.$v.student.$error) {
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
      .post(`${baseUrl}/Create`, this.student)
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
    this.student.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.student)
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
      .post(`${baseUrl}/Delete/${this.student.Id}`)
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


  // ati
  @action()
  async resetRegister() {
    this.student.Id = 0;
    this.RESET(this._registerVue);
  }


  @action()
  async registerModal() {
    let vm = this._registerStudentVue;
   if (!(await this.validateForm(vm))) return;

    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.student);

    return axios({
      method: "post",
      url: `${baseUrl}/create?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    })
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.resetRegister();
          router.push("/user/login");
        }
      });

  //#endregion
}
}

export const studentStore = StudentStore.ExtractVuexModule(StudentStore);
