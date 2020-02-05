import Vue from "Vue";
import IReport, { DefaultPanel } from "src/models/IReport";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { PANEL_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";


@Module({ namespacedPath: "ReportStore/" })
export class ReportStore extends VuexModule {
  
  report: IReport;
  
  /**
   * initialize data
   */
  constructor() {
    super();

    this.report = util.cloneObject(DefaultPanel);
  
  }

  //#region ### getters ###
  get modelName() {
    return "گزارش";
  }

  

  

  //#endregion

  //#region ### mutations ###

  // @mutation
  // private CREATE(panel: IPanel) {
  //   this._panelList.push(panel);
  // }

  //#endregion

  //#region ### actions ###
  @action()
  async getAllQuestionOfEachLesson() {
    return axios
      .get(`${baseUrl}/GetAllQuestionOfEachLesson`)
      .then((response: AxiosResponse<IReport>) => {
        util.mapObject(response.data, this.report);
      });
  }

  

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.report.$touch();
      if (vm.$v.report.$error) {
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

  //#endregion
}

export const reportStore = ReportStore.ExtractVuexModule(ReportStore);
