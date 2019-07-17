import AssayCreate from "src/models/IAssay";
import Vue from "Vue";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { ASSAY_URL as baseUrl } from "src/utilities/site-config";
import utilities from "src/utilities";

@Module({ namespacedPath: "assayStore/" })
export class AssayStore extends VuexModule {
  assayCreate: AssayCreate;
  private _indexVue: Vue;
  private _assayVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();
    this.assayCreate = new AssayCreate();
  }

  //#region ### getters ###
  get modelName() {
    return "آزمون";
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  SET_INDEX_VUE(vm: Vue) {
    this._indexVue = vm;
  }

  @mutation
  SET_ASSAY_VUE(vm: Vue) {
    this._assayVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.assayCreate.$touch();
      if (vm.$v.assayCreate.$error) {
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
  async submitPreCreate() {
    let vm = this._assayVue;
    if (!(await this.validateForm(vm))) return;

    var data = {
      Title: this.assayCreate.Title,
      Time: this.assayCreate.Time,
      LookupId_Importance: this.assayCreate.LookupId_Importance,
      LookupId_Type: this.assayCreate.LookupId_Type,
      LookupId_QuestionType: this.assayCreate.LookupId_QuestionType,
      IsPublic: this.assayCreate.IsPublic,
      IsOnline: this.assayCreate.IsOnline,
      RandomOptions: this.assayCreate.RandomOptions,
      RandomQuestion: this.assayCreate.RandomQuestion,
      Lessons: this.assayCreate.Lessons.filter(x => x.Checked)
    };
    data.Lessons.forEach(x => {
      x.Topics = x.Topics.filter(x => x.Checked);
    });
    return axios
      .post(`${baseUrl}/GetAllQuestion`, data)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          debugger;
        }
      });
  }

  //#endregion
}

export const assayStore = AssayStore.ExtractVuexModule(AssayStore);
