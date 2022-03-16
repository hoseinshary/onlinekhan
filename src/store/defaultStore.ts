import Vue from "Vue";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { CREDIT_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component"

@Module({ namespacedPath: "defaultStore/" })
export class DefaultStore extends VuexModule {
    credit:number;


    /**
   * initialize data
   */
  constructor() {
    super();

    this.credit = 0;
  }

  //#region ### getters ###
  get getCREDIT(){
      return this.credit;
  }

  //#endregion

  //#region ### mutations ###
  @mutation
  private SET_CREDIT(newcredit) {
    this.credit = newcredit;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async creditGetById() {
    return axios
      .get(`${baseUrl}/GetCreditByUserId`)
      .then((response)=>{
        // console.log('response',response.data);
        this.SET_CREDIT(response.data);
      });
     }
}

export const defaultStore = DefaultStore.ExtractVuexModule(DefaultStore);