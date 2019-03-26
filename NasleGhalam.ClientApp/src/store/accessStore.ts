import Vue from "Vue";
import IAccess, { DefaultAccess } from "src/models/IAccess";
import IAction from "src/models/IAction";
import IMenu from "src/models/IMenu";
import ISubMenu from "src/models/ISubMenu";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { ACCESS_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";

@Module({ namespacedPath: "accessStore/" })
export class AccessStore extends VuexModule {
  openModal: {
    access: boolean;
  };
  access: IAccess;
  actionList: Array<IAction>;
  menuList: Array<IMenu>;
  subMenuList: Array<ISubMenu>;
  private roleId: number = 0;
  private modelChanged: boolean = true;
  private accessVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.openModal = {
      access: false
    };
    this.access = util.cloneObject(DefaultAccess);
    this.menuList = [];
    this.subMenuList = [];
    this.actionList = [];
  }

  //#region ### getters ###
  get menuDdl() {
    return this.menuList.map(x => ({
      value: x.ModuleId,
      label: x.ModuleName
    }));
  }

  get subMenuDdl() {
    return this.subMenuList
      .filter(
        x => this.access.ModuleId == 0 || x.ModuleId == this.access.ModuleId
      )
      .map(x => ({
        value: x.ControllerId,
        label: x.FaName
      }));
  }

  get actionGridData() {
    return this.actionList
      .filter(
        x => this.access.ModuleId == 0 || x.ModuleId == this.access.ModuleId
      )
      .filter(
        x =>
          this.access.ControllerId == 0 ||
          x.ControllerId == this.access.ControllerId
      );
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  SET_ROLE_ID(id: number) {
    this.roleId = id;
  }

  @mutation
  private SET_ACTION_LIST(list: Array<IAction>) {
    this.actionList = list;
  }

  @mutation
  private SET_MENU_LIST(list: Array<IMenu>) {
    list.unshift({ ModuleId: 0, ModuleName: "همه" });
    this.menuList = list;
  }

  @mutation
  private SET_SUB_MENU_LIST(list: Array<ISubMenu>) {
    list.unshift({ ModuleId: 0, FaName: "همه", ControllerId: 0, EnName: "" });
    this.subMenuList = list;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this.modelChanged = changed;
  }

  @mutation
  OPEN_MODAL_ACCESS(open: boolean) {
    this.openModal.access = open;
  }

  @mutation
  SET_ACCESS_VUE(vm: Vue) {
    this.accessVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  fillMenuList() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetMenu`)
        .then((response: AxiosResponse<Array<IMenu>>) => {
          this.SET_MENU_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.menuList);
    }
  }

  @action()
  fillSubMenuList() {
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetSubMenu`)
        .then((response: AxiosResponse<Array<ISubMenu>>) => {
          this.SET_SUB_MENU_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.subMenuList);
    }
  }

  @action()
  fillActionList() {
    return axios
      .get(`${baseUrl}/GetAllActions?roleId=${this.roleId}`)
      .then((response: AxiosResponse<Array<IAction>>) => {
        this.SET_ACTION_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.role.$touch();
      if (vm.$v.role.$error) {
        const context = getRawActionContext(this);
        context.dispatch("notifyInvalidForm", vm, { root: true });
        resolve(false);
      }
      resolve(true);
    });
  }

  @action({ mode: "raw" })
  notify(data: IMessageResult) {
    const context = getRawActionContext(this);
    return context.dispatch(
      "notify",
      {
        body: data.Message,
        type: data.MessageType,
        vm: this.accessVue
      },
      { root: true }
    );
  }

  @action()
  changeAccess(payload) {
    return axios
      .post(`${baseUrl}/ChangeAccess`, {
        RoleId: this.roleId,
        ActionId: payload.actionId,
        IsChecked: payload.checked
      })
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;

        if (data.MessageType != MessageType.Success) {
          this.notify(data);
        }
      });
  }
  //#endregion
}

export const accessStore = AccessStore.ExtractVuexModule(AccessStore);
