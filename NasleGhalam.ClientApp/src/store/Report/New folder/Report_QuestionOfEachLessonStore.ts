import Vue from "Vue";
import IReport_QuestionOfEachLesson, { DefaultReport_QuestionOfEachLesson } from "src/models/Report/IReport_QuestionOfEachLesson";
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


@Module({ namespacedPath: "reportStore/" })
export class ReportStore extends VuexModule {

  report: IReport_QuestionOfEachLesson;
  private _reportList: Array<IReport_QuestionOfEachLesson>;
  private _modelChanged: boolean = true;
  

  /**
   * initialize data
   */
  constructor() {
    super();

    this.report = util.cloneObject(DefaultReport_QuestionOfEachLesson);
    this._reportList = [];

  }

  //#region ### getters ###
  get modelName() {
    return "گزارش";
  }

  get recordName() {
    return this.report.Name || "";
  }


  get gridData() {
    return this._reportList;
  }
  //#endregion

  //#region ### mutations ###
 

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultReport_QuestionOfEachLesson  , this.report, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IReport_QuestionOfEachLesson>) {
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
        .get(`${baseUrl}/GetAllQuestionOfEachLesson`)
        .then((response: AxiosResponse<Array<IReport_QuestionOfEachLesson>>) => {
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

export const reportStore = ReportStore.ExtractVuexModule(ReportStore);
