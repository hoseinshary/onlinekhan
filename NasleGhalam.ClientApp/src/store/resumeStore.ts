import Vue from "Vue";
import IResume, { DefaultResume } from "src/models/IResume";
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

@Module({ namespacedPath: "resumeStore/" })
export class ResumeStore extends VuexModule {
  resume: IResume;
  private _indexVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();
    this.resume = util.cloneObject(DefaultResume);
  }

  //#region ### getters ###
  get modelName() {
    return "رزومه";
  }

  get recordName() {
    return this.resume.Name || "";
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  SET_INDEX_VUE(vm: Vue) {
    this._indexVue = vm;
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultResume, this.resume, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }
  //#endregion

  //#region ### actions ###
  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.resume.$touch();
      if (vm.$v.resume.$error) {
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
  async submitCreate() {
    let vm = this._indexVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Create`, this.resume)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.resume.Id = 0;
    this.RESET(this._indexVue);
  }
  //#endregion
}

export const resumeStore = ResumeStore.ExtractVuexModule(ResumeStore);
