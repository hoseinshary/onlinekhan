import ILookup from "src/models/ILookup";
import axios, { AxiosResponse } from "src/plugins/axios";
import { LOOKUP_URL as baseUrl } from "src/utilities/site-config";
import { VuexModule, mutation, action, Module } from "vuex-class-component";

@Module({ namespacedPath: "lookupStore/" })
export class LookupStore extends VuexModule {
  private _educationTreeState: Array<ILookup>;

  /**
   * initialize data
   */
  constructor() {
    super();
    this._educationTreeState = [];
  }

  //#region ### getters ###
  get educationTreeStateDdl() {
    return this._educationTreeState.map(x => ({
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
  //#endregion
}

export const lookupStore = LookupStore.ExtractVuexModule(LookupStore);
