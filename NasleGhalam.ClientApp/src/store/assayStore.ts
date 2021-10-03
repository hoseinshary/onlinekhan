import AssayCreate, { AssayLesson } from "src/models/IAssay";
import Vue from "Vue";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import IMessageResult from "src/models/IMessageResult";
//import IAssay ,{DefaultAssayCreate} from "src/models/IAssay";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { ASSAY_URL as baseUrl } from "src/utilities/site-config";
import utilities from "src/utilities";
import IQuestion from "src/models/IQuestion";
import IAssay from "src/models/IAssay";

@Module({ namespacedPath: "assayStore/" })
export class AssayStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean; _0student: boolean; _1lesson: boolean; _2topic: boolean ; _3assay : boolean ; _4previewQuestion : boolean };
  assayCreate: AssayCreate;
  private _assayList: Array<IAssay>;

  _lessonList: Array<AssayLesson>;
  IsDetailTopic : boolean;

  private _indexVue: Vue;
  private _assayVue: Vue;
  private _0studentVue: Vue;
  private _1lessonVue: Vue;
  private _2topicVue: Vue;
  private _3assayVue : Vue;
  private _4previewQuestionVue: Vue;





  /**
   * initialize data
   */
  constructor() {
    super();
    this.assayCreate = new AssayCreate();
    this._lessonList = [];
    this.IsDetailTopic = false;
    this.openModal = {
      create: false,
      edit: false,
      delete: false,
      _0student: false,
      _1lesson: false,
      _2topic: false,
      _3assay:false,
      _4previewQuestion : false
    }
  }
  //#region ### getters ###
  get modelName() {
    return "آزمون";
  }

  get checkedLessons() {
    return this._lessonList.filter(x => x.Checked);
  }


  get lessonIds() {
    return this._lessonList.filter(x => x.Checked).map(x => x.Id);
}

get IsShowAnswer()
{
  
  return(question :any) => {
    return question.IsShowAnswer;
  }
}

get lesssonChoose(){
  //console.log(this.assayCreate.Lessons);
  return this.assayCreate.Lessons.map((x) => ({
    Id: x.Id,
    Name: x.Name,
    Questions : x.Questions
       }));
}


get lessonChooseAllQuestioncount(){
  var countall = 0 ;
  this.assayCreate.Lessons.forEach(element => {
    countall += element.Questions.length
  });
  return countall;
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

  
  @mutation
  SET_0STUDENT_VUE(vm: Vue) {
    this._0studentVue = vm;
  }
  
  @mutation
  SET_1LESSON_VUE(vm: Vue) {
    this._1lessonVue = vm;
  }
  
  @mutation
  SET_2TOPIC_VUE(vm: Vue) {
    this._2topicVue = vm;
  }

  @mutation
  SET_3ASSAY_VUE(vm: Vue) {
    this._3assayVue = vm;
  }
  @mutation
  SET_4OREVIEWQUESTION_VUE(vm: Vue) {
    this._4previewQuestionVue = vm;
  }

  @mutation
  OPEN_MODAL_0STUDENT(open: boolean) {
    this.openModal._0student = open;
  }
  @mutation
  OPEN_MODAL_1LESSON(open: boolean) {
    
    this.openModal._1lesson = open;
  }
  @mutation
  OPEN_MODAL_2TOPIC(open: boolean) {
    this.openModal._2topic = open;
  }
  @mutation
  OPEN_MODAL_3ASSAY(open: boolean) {
    this.openModal._3assay = open;
  }

  @mutation
  OPEN_MODAL_4PREVIEWQUESTION(open: boolean) {
    this.openModal._4previewQuestion = open;
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
    //if (!(await this.validateForm(vm))) return;

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
      Lessons: this.assayCreate.Lessons,
      Page : this.assayCreate.Page
    };

    type TQuestionAssay = { LessonId: number; Questions: Array<IQuestion> };
    
    return axios
      .post(`${baseUrl}/GetAllQuestion`, data)
      .then((response: AxiosResponse<Array<TQuestionAssay>>) => {
        let data = response.data;
        this.checkedLessons.forEach(lesson => {
          var found = data.find(x => x.LessonId == lesson.Id);
          if (found) lesson.Questions = found.Questions;
        });
      });
  }


  @action()
  async submitCreate() {
    let vm = this._assayVue;
    //if (!(await this.validateForm(vm))) return;

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
      Lessons: this.assayCreate.Lessons,
      Page : this.assayCreate.Page
    };

    type TQuestionAssay = { LessonId: number; Questions: Array<IQuestion> };
    
    return axios
      .post(`${baseUrl}/Create`, data)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          
        }
      });
  }
  //#endregion
}

export const assayStore = AssayStore.ExtractVuexModule(AssayStore);
