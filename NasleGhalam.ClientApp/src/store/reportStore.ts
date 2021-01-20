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
import IReport_UserQuestionReport, { DefaultReport_UserQuestionReport } from "src/models/Report/IReport_UserQuestionReport";
import IReport_QuestionsReport, { DefaultReport_QuestionsReport } from "src/models/Report/IReport_QuestionsReport";


@Module({ namespacedPath: "reportStore/" })
export class ReportStore extends VuexModule {
  
  reportQuestionOfEachLesson: IReport_QuestionOfEachLesson;
  reportUserQuestionReport: IReport_UserQuestionReport;
  reportQuestionsReport: IReport_QuestionsReport;
  private _reportListQuestionOfEachLesson: Array<IReport_QuestionOfEachLesson>;
  private _reportListUserQuestionReport: Array<IReport_UserQuestionReport>;

  private _reportListQuestionsReport: Array<IReport_QuestionsReport>;

  private _modelChanged: boolean = true;
  
  /**
   * initialize data
   */
  constructor() {
    super();

    this.reportQuestionOfEachLesson = util.cloneObject(DefaultReport_QuestionOfEachLesson);
    this._reportListQuestionOfEachLesson = [];

    this.reportUserQuestionReport = util.cloneObject(DefaultReport_UserQuestionReport);
    this._reportListUserQuestionReport = [];

    this.reportQuestionsReport = util.cloneObject(DefaultReport_QuestionsReport);
    this._reportListQuestionsReport = [];
  
  }

  //#region ### getters ###
  get modelName() {
    return "گزارش";
  }

  
  get recordNameUserQuestionReport() {
    return this.reportUserQuestionReport.Name || "";
  }

  get recordNameQuestionOfEachLesson() {
    return this.reportQuestionOfEachLesson.Name || "";
  }
  
  get recordNameQuestionsReport() {
    return this.reportQuestionsReport.Id || "";
  }
  get gridDataQuestionOfEachLesson() {
    return this._reportListQuestionOfEachLesson;
  }
  
  get gridDataUserQuestionReport() {
    return this._reportListUserQuestionReport;
  }
  
  get gridDataQuestionsReport() {
    return this._reportListQuestionsReport;
  }

  //#endregion

  //#region ### mutations ###

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultReport_UserQuestionReport  , this.reportUserQuestionReport, "Id");
    util.mapObject(DefaultReport_QuestionOfEachLesson  , this.reportQuestionOfEachLesson, "Id");
    util.mapObject(DefaultReport_QuestionsReport  , this.reportQuestionsReport, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LISTQuestionOfEachLesson(list: Array<IReport_QuestionOfEachLesson>) {
    this._reportListQuestionOfEachLesson = list;
  }

  @mutation
  private SET_LISTUserQuestionReport(list: Array<IReport_UserQuestionReport>) {
    this._reportListUserQuestionReport = list;
  }

  @mutation
  private SET_LISTQuestionsReport(list: Array<IReport_QuestionsReport>) {
    this._reportListQuestionsReport = list;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this._modelChanged = changed;
  }
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
    .then((response: AxiosResponse<Array<IReport_QuestionOfEachLesson>>) => {
      this.SET_LISTQuestionOfEachLesson(response.data);
      this.MODEL_CHANGED(false);
      this.RESET(this._reportListQuestionOfEachLesson);
    });
  }
  
  @action()
  async GetAllUsersReport() {
    return axios
    .get(`${baseUrl}/GetAllUsersReport`)
    .then((response: AxiosResponse<Array<IReport_UserQuestionReport>>) => {
      this.SET_LISTUserQuestionReport(response.data);
      this.MODEL_CHANGED(false);
      this.RESET(this._reportListUserQuestionReport);
    });
    
  }
  

  @action()
  async GetAllQuestionsReport(payload: {
    lessonId: number;
    
  }) {
    return axios
    .get(`${baseUrl}/GetAllQuestionsReport/${payload.lessonId}`)
    .then((response: AxiosResponse<Array<IReport_QuestionsReport>>) => {
      this.SET_LISTQuestionsReport(response.data);
      this.MODEL_CHANGED(false);
      this.RESET(this._reportListQuestionsReport);
    });
    
  }

  @action()
  async GetAllQuestionsReportExcel(payload: {
    lessonId: number;
    
  }) {
    return  axios({
      url: `${baseUrl}/GetAllQuestionsReportExcel/${payload.lessonId}`,
      method: 'GET',
      responseType: 'blob',
  }).then((response) => {
       var fileURL = window.URL.createObjectURL(new Blob([response.data]));
       var fileLink = document.createElement('a');
       fileLink.href = fileURL;
       fileLink.setAttribute('download', 'report.xlsx');
       document.body.appendChild(fileLink);
       fileLink.click();
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
