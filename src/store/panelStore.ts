import Vue from "Vue";
import IPanel, { DefaultPanel } from "src/models/IPanel";
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

@Module({ namespacedPath: "panelStore/" })
export class PanelStore extends VuexModule {
  
  panel: IPanel;
  
  /**
   * initialize data
   */
  constructor() {
    super();

    this.panel = util.cloneObject(DefaultPanel);
  
  }

  //#region ### getters ###
  get modelName() {
    return "پنل";
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
  async getDataExpert() {
    return axios
      .get(`${baseUrl}/GetDataExpert`)
      .then((response: AxiosResponse<IPanel>) => {
        util.mapObject(response.data, this.panel);
      });
  }

  @action()
  async getDataAdmin() {
    return axios
      .get(`${baseUrl}/GetDataAdmin`)
      .then((response: AxiosResponse<IPanel>) => {
        util.mapObject(response.data, this.panel);
      });
  }
  

  @action({ mode: "raw" })
  async validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.panel.$touch();
      if (vm.$v.panel.$error) {
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

export const panelStore = PanelStore.ExtractVuexModule(PanelStore);
