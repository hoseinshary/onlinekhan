import ILookup from "src/models/ILookup";
import axios, { AxiosResponse } from "src/plugins/axios";
import { LOOKUP_URL as baseUrl } from "src/utilities/site-config";
import { VuexModule, mutation, action, Module } from "vuex-class-component";

@Module({ namespacedPath: "lookupStore/" })
export class LookupStore extends VuexModule {
  private _educationTreeState: Array<ILookup>;
  private _topicNezam: Array<ILookup>;
  private _topicHardnessType: Array<ILookup>;
  private _topicAreaType: Array<ILookup>;
  private _questionType: Array<ILookup>;
  private _questionHardnessType: Array<ILookup>;
  private _answerType: Array<ILookup>;
  private _paperType: Array<ILookup>;
  private _printType: Array<ILookup>;
  private _bookType: Array<ILookup>;
  private _areaType: Array<ILookup>;
  private _repeatnessType: Array<ILookup>;
  private _authorType: Array<ILookup>;
  private _assayImportance: Array<ILookup>;
  private _assayType: Array<ILookup>;
  private _whereProblem: Array<ILookup>;
  private _reasonProblem: Array<ILookup>;
  private _questionRank: Array<ILookup>;
  private _mediaType: Array<ILookup>;

  /**
   * initialize data
   */
  constructor() {
    super();
    this._educationTreeState = [];
    this._topicNezam = [];
    this._topicHardnessType = [];
    this._topicAreaType = [];
    this._questionType = [];
    this._questionHardnessType = [];
    this._answerType = [];
    this._paperType = [];
    this._printType = [];
    this._bookType = [];
    this._areaType = [];
    this._repeatnessType = [];
    this._authorType = [];
    this._assayImportance = [];
    this._assayType = [];
    this._whereProblem = [];
    this._reasonProblem = [];
    this._questionRank = [];
    this._mediaType = [];
  }

  //#region ### getters ###
  get educationTreeStateDdl() {
    return this._educationTreeState.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get topicNezamDdl() {
    return this._topicNezam.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get topicHardnessTypeDdl() {
    return this._topicHardnessType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get topicAreaTypeDdl() {
    return this._topicAreaType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get questionTypeDdl() {
    return this._questionType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get questionRankDdl() {
    return this._questionRank.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get questionHardnessTypeDdl() {
    return this._questionHardnessType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get answerTypeDdl() {
    return this._answerType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get paperTypeDdl() {
    return this._paperType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get printTypeDdl() {
    return this._printType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get bookTypeDdl() {
    return this._bookType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get areaTypeDdl() {
    return this._areaType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get repeatnessTypeDdl() {
    return this._repeatnessType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get authorTypeDdl() {
    return this._authorType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get assayImportanceDdl() {
    return this._assayImportance.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  get assayTypeDdl() {
    return this._assayType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  
  get whereProblemDdl() {
    return this._whereProblem.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }

  
  get reasonProblemDdl() {
    return this._reasonProblem.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }


  get mediaTypeDdl() {
    return this._mediaType.map(x => ({
      value: x.Id,
      label: x.Value
    }));
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private SET_EDUCATION_TREE_STATE_LIST(list: Array<ILookup>) {
    this._educationTreeState = list;
  }

  @mutation
  private SET_TOPIC_NEZAM_LIST(list: Array<ILookup>) {
    this._topicNezam = list;
  }

  @mutation
  private SET_TOPIC_HARDNESS_TYPE_LIST(list: Array<ILookup>) {
    this._topicHardnessType = list;
  }

  @mutation
  private SET_TOPIC_AREA_TYPE_LIST(list: Array<ILookup>) {
    this._topicAreaType = list;
  }

  @mutation
  private SET_QUESTION_TYPE_LIST(list: Array<ILookup>) {
    this._questionType = list;
  }

  @mutation
  private SET_QUESTION_RANK_LIST(list: Array<ILookup>) {
    this._questionRank = list;
  }

  @mutation
  private SET_QUESTION_HARDNESS_TYPE_LIST(list: Array<ILookup>) {
    this._questionHardnessType = list;
  }

  @mutation
  private SET_ANSWER_TYPE_LIST(list: Array<ILookup>) {
    this._answerType = list;
  }

  @mutation
  private SET_PAPER_TYPE_LIST(list: Array<ILookup>) {
    this._paperType = list;
  }

  @mutation
  private SET_PRINT_TYPE_LIST(list: Array<ILookup>) {
    this._printType = list;
  }

  @mutation
  private SET_BOOK_TYPE_LIST(list: Array<ILookup>) {
    this._bookType = list;
  }

  @mutation
  private SET_AREA_TYPE_LIST(list: Array<ILookup>) {
    this._areaType = list;
  }

  @mutation
  private SET_REPEATNESS_TYPE_LIST(list: Array<ILookup>) {
    this._repeatnessType = list;
  }

  @mutation
  private SET_AUTHOR_TYPE_LIST(list: Array<ILookup>) {
    this._authorType = list;
  }

  @mutation
  private SET_ASSAY_IMPORTANCE_LIST(list: Array<ILookup>) {
    this._assayImportance = list;
  }

  @mutation
  private SET_ASSAY_TYPE_LIST(list: Array<ILookup>) {
    this._assayType = list;
  }

  @mutation
  private SET_WHERE_PROBLEM_LIST(list: Array<ILookup>) {
    this._whereProblem = list;
  }

  @mutation
  private SET_REASON_PROBLEM_LIST(list: Array<ILookup>) {
    this._reasonProblem = list;
  }

  @mutation
  private SET_MEDIA_TYPE_LIST(list: Array<ILookup>) {
    this._mediaType = list;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async fillEducationTreeState() {
    if (!this._educationTreeState.length) {
      return axios
        .get(`${baseUrl}/GetAllEducationTreeState`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_EDUCATION_TREE_STATE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._educationTreeState);
    }
  }

  @action()
  async fillTopicNezam() {
    if (!this._topicNezam.length) {
      return axios
        .get(`${baseUrl}/GetAllNezam`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_TOPIC_NEZAM_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._topicNezam);
    }
  }

  @action()
  async fillTopicHardnessType() {
    if (!this._topicHardnessType.length) {
      return axios
        .get(`${baseUrl}/GetAllTopicHardnessType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_TOPIC_HARDNESS_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._topicHardnessType);
    }
  }

  @action()
  async fillTopicAreaType() {
    if (!this._topicAreaType.length) {
      return axios
        .get(`${baseUrl}/GetAllAreaType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_TOPIC_AREA_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._topicAreaType);
    }
  }

  @action()
  async fillQuestionType() {
    if (!this._questionType.length) {
      return axios
        .get(`${baseUrl}/GetAllQuestionType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_QUESTION_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._questionType);
    }
  }

  @action()
  async fillQuestionRank() {
    if (!this._questionRank.length) {
      return axios
        .get(`${baseUrl}/GetAllQuestionRank`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_QUESTION_RANK_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._questionRank);
    }
  }


  @action()
  async fillQuestionHardnessType() {
    if (!this._questionHardnessType.length) {
      return axios
        .get(`${baseUrl}/GetAllQuestionHardnessType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_QUESTION_HARDNESS_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._questionHardnessType);
    }
  }

  @action()
  async fillAnswerType() {
    if (!this._answerType.length) {
      return axios
        .get(`${baseUrl}/GetAllAnswerType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_ANSWER_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._answerType);
    }
  }

  @action()
  async fillPaperType() {
    if (!this._paperType.length) {
      return axios
        .get(`${baseUrl}/GetAllPaperType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_PAPER_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._paperType);
    }
  }

  @action()
  async fillPrintType() {
    if (!this._printType.length) {
      return axios
        .get(`${baseUrl}/GetAllPrintType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_PRINT_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._printType);
    }
  }

  @action()
  async fillBookType() {
    if (!this._bookType.length) {
      return axios
        .get(`${baseUrl}/GetAllBookType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_BOOK_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._bookType);
    }
  }

  @action()
  async fillAreaType() {
    if (!this._areaType.length) {
      return axios
        .get(`${baseUrl}/GetAllAreaType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_AREA_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._areaType);
    }
  }

  @action()
  async fillRepeatnessType() {
    if (!this._repeatnessType.length) {
      return axios
        .get(`${baseUrl}/GetAllRepeatnessType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_REPEATNESS_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._repeatnessType);
    }
  }

  @action()
  async fillAuthorType() {
    if (!this._authorType.length) {
      return axios
        .get(`${baseUrl}/GetAllAuthorType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_AUTHOR_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._authorType);
    }
  }

  @action()
  async fillAssayImportance() {
    if (!this._assayImportance.length) {
      return axios
        .get(`${baseUrl}/GetAllAssayImportance`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_ASSAY_IMPORTANCE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._assayImportance);
    }
  }

  @action()
  async fillAssayType() {
    if (!this._assayType.length) {
      return axios
        .get(`${baseUrl}/GetAllAssayType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_ASSAY_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._assayType);
    }
  }
  
  @action()
  async fillWhereProblem() {
    if (!this._whereProblem.length) {
      return axios
        .get(`${baseUrl}/GetAllWhereProblem`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_WHERE_PROBLEM_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._whereProblem);
    }
  }
  
  @action()
  async fillReasonProblem() {
    if (!this._reasonProblem.length) {
      return axios
        .get(`${baseUrl}/GetAllReasonProblem`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_REASON_PROBLEM_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._reasonProblem);
    }
  }

  @action()
  async fillMediaType() {
    if (!this._mediaType.length) {
      return axios
        .get(`${baseUrl}/GetAllMediaType`)
        .then((response: AxiosResponse<Array<ILookup>>) => {
          this.SET_MEDIA_TYPE_LIST(response.data);
        });
    } else {
      return Promise.resolve(this._mediaType);
    }
  }


  //#endregion
}

export const lookupStore = LookupStore.ExtractVuexModule(LookupStore);
