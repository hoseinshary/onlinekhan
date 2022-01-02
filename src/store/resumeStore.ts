import Vue from "Vue";
import IResume, {
  DefaultResume,
  IPublication,
  ITeachingResume,
  IEducationCertificate,
  DefaultTeachingResume,
  DefaultEducationCertificate,
  DefaultPublication
} from "src/models/IResume";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { RESUME_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import utilities from "src/utilities";
import router from "src/router";

@Module({ namespacedPath: "resumeStore/" })
export class ResumeStore extends VuexModule {
  openModal: { detail: boolean };
  resume: IResume;
  publication: IPublication;
  educationCertificate: IEducationCertificate;
  teachingResume: ITeachingResume;
  private _resumeList: Array<IResume>;

  private _indexVue: Vue;
  private _publicationVue: Vue;
  private _educationCertificateVue: Vue;
  private _teachingResumeVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();
    this.resume = util.cloneObject(DefaultResume);
    this.publication = util.cloneObject(DefaultPublication);
    this.educationCertificate = util.cloneObject(DefaultEducationCertificate);
    this.teachingResume = util.cloneObject(DefaultTeachingResume);
    this._resumeList = [];
    this.openModal = {
      detail: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "رزومه";
  }

  get recordName() {
    return this.resume.Name || "";
  }

  get gridData() {
    return this._resumeList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  SET_INDEX_VUE(vm: Vue) {
    this._indexVue = vm;
  }

  @mutation
  SET_PUBLICATION_VUE(vm: Vue) {
    this._publicationVue = vm;
  }

  @mutation
  SET_TEACHING_RESUME_VUE(vm: Vue) {
    this._teachingResumeVue = vm;
  }

  @mutation
  SET_EDUCATION_CERTIFICATE_VUE(vm: Vue) {
    this._educationCertificateVue = vm;
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultResume, this.resume, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private RESET_PUBLICATION(vm: any) {
    util.mapObject(DefaultPublication, this.publication);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private RESET_EDUCATION_CERTIFICATE(vm: any) {
    util.mapObject(DefaultEducationCertificate, this.educationCertificate);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private RESET_TEACHING_RESUME(vm: any) {
    util.mapObject(DefaultTeachingResume, this.teachingResume);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IResume>) {
    this._resumeList = list;
  }

  @mutation
  OPEN_MODAL_DETAIL(open: boolean) {
    this.openModal.detail = open;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IResume>) => {
        util.mapObject(response.data, this.resume);
      });
  }

  @action()
  async fillList() {
    return axios
      .get(`${baseUrl}/GetAll`)
      .then((response: AxiosResponse<Array<IResume>>) => {
        this.SET_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(payload: { vm: any; model: any }) {
    return new Promise(resolve => {
      payload.model.$touch();
      if (payload.model.$error) {
        const context = getRawActionContext(this);
        context.dispatch("notifyInvalidForm", payload.vm, { root: true });
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
  async submitCreate() {
    let vm = this._indexVue;
    if (!(await this.validateForm({ vm, model: vm.$v.resume }))) return;

    return axios
      .post(`${baseUrl}/Create`, this.resume)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.resetCreate();
          router.push("/");
        }
      });
  }

  @action()
  async resetCreate() {
    this.resume.Id = 0;
    this.RESET(this._indexVue);
  }

  @action()
  async addPublication() {
    let vm = this._publicationVue;
    if (!(await this.validateForm({ vm, model: vm.$v.publication }))) return;
    var newPublication = utilities.cloneObject(this.publication);
    this.resume.Publications.push(newPublication);
    this.RESET_PUBLICATION(vm);
  }

  @action()
  async addEducationCertificate() {
    let vm = this._educationCertificateVue;
    if (!(await this.validateForm({ vm, model: vm.$v.educationCertificate })))
      return;
    var newEducationCertificate = utilities.cloneObject(
      this.educationCertificate
    );
    this.resume.EducationCertificates.push(newEducationCertificate);
    this.RESET_EDUCATION_CERTIFICATE(vm);
  }

  @action()
  async addTeachingResume() {
    let vm = this._teachingResumeVue;
    if (!(await this.validateForm({ vm, model: vm.$v.teachingResume }))) return;
    var newTeachingResume = utilities.cloneObject(this.teachingResume);
    this.resume.TeachingResumes.push(newTeachingResume);
    this.RESET_TEACHING_RESUME(vm);
  }
  //#endregion
}

export const resumeStore = ResumeStore.ExtractVuexModule(ResumeStore);
