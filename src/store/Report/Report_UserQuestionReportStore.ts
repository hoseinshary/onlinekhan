import Vue from "Vue";
import IReport_UserQuestionReport, { DefaultReport_UserQuestionReport } from "src/models/Report/IReport_UserQuestionReport";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { REPORT_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";


@Module({ namespacedPath: "reportStore2/" })
export class ReportStore2 extends VuexModule {

  report2: IReport_UserQuestionReport;
  private _reportList: Array<IReport_UserQuestionReport>;
  private _modelChanged: boolean = true;
  

  /**
   * initialize data
   */
  constructor() {
    super();

    this.report2 = util.cloneObject(DefaultReport_UserQuestionReport);
    this._reportList = [];

  }

  //#region ### getters ###
  get modelName() {
    return "گزارش";
  }

  get recordName() {
    return this.report2.Name || "";
  }


  get gridData() {
    return this._reportList;
  }
  //#endregion

  //#region ### mutations ###
 

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultReport_UserQuestionReport  , this.report2, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IReport_UserQuestionReport>) {
    this._reportList = list;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this._modelChanged = changed;
  }
  //#endregion

  //#region ### actions ###
 

  @action()
  async fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAllUsersReport`)
        .then((response: AxiosResponse<Array<IReport_UserQuestionReport>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._reportList);
    }
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

export const reportStore2 = ReportStore2.ExtractVuexModule(ReportStore2);
