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

  /**
   * initialize data
   */
  constructor() {
    super();
    this._educationTreeState = [];
    this._topicNezam = [];
    this._topicHardnessType = [];
    this._topicAreaType = [];
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
  //#endregion
}

export const lookupStore = LookupStore.ExtractVuexModule(LookupStore);
