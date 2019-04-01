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
  getter,
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
  private _actionList: Array<IAction>;
  private _menuList: Array<IMenu>;
  private _subMenuList: Array<ISubMenu>;
  private _roleId: number = 0;
  private _menuChanged: boolean = true;
  private _subMenuChanged: boolean = true;
  private _accessVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.openModal = {
      access: false
    };
    this.access = util.cloneObject(DefaultAccess);
    this._menuList = [];
    this._subMenuList = [];
    this._actionList = [];
  }

  //#region ### getters ###
  get menuDdl() {
    var _menuList = this._menuList.map(x => ({
      value: x.ModuleId,
      label: x.ModuleName
    }));
    return [{ value: 0, label: "همه" }].concat(_menuList);
  }

  get subMenuDdl() {
    var _subMenuList = this._subMenuList
      .filter(
        x => this.access.ModuleId == 0 || x.ModuleId == this.access.ModuleId
      )
      .map(x => ({
        value: x.ControllerId,
        label: x.FaName
      }));
    return [{ value: 0, label: "همه" }].concat(_subMenuList);
  }

  get actionGridData() {
    return this._actionList
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
    this._roleId = id;
  }

  @mutation
  private SET_ACTION_LIST(list: Array<IAction>) {
    this._actionList = list;
  }

  @mutation
  private SET_MENU_LIST(list: Array<IMenu>) {
    this._menuList = list;
  }

  @mutation
  private SET_SUB_MENU_LIST(list: Array<ISubMenu>) {
    this._subMenuList = list;
  }

  @mutation
  private MENU_CHANGED(changed: boolean) {
    this._menuChanged = changed;
  }

  @mutation
  private SUB_MENU_CHANGED(changed: boolean) {
    this._subMenuChanged = changed;
  }

  @mutation
  OPEN_MODAL_ACCESS(open: boolean) {
    this.openModal.access = open;
  }

  @mutation
  SET_ACCESS_VUE(vm: Vue) {
    this._accessVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  async fillMenuList() {
    if (this._menuChanged) {
      return axios
        .get(`${baseUrl}/GetMenu`)
        .then((response: AxiosResponse<Array<IMenu>>) => {
          this.SET_MENU_LIST(response.data);
          this.MENU_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._menuList);
    }
  }

  @action()
  async fillSubMenuList() {
    if (this._subMenuChanged) {
      return axios
        .get(`${baseUrl}/GetSubMenu`)
        .then((response: AxiosResponse<Array<ISubMenu>>) => {
          this.SET_SUB_MENU_LIST(response.data);
          this.SUB_MENU_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._subMenuList);
    }
  }

  @action()
  async fillActionList() {
    return axios
      .get(`${baseUrl}/GetAllActions?roleId=${this._roleId}`)
      .then((response: AxiosResponse<Array<IAction>>) => {
        this.SET_ACTION_LIST(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
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
  async notify(data: IMessageResult) {
    const context = getRawActionContext(this);
    return context.dispatch(
      "notify",
      {
        body: data.Message,
        type: data.MessageType,
        vm: this._accessVue
      },
      { root: true }
    );
  }

  @action()
  async changeAccess(payload) {
    return axios
      .post(`${baseUrl}/ChangeAccess`, {
        RoleId: this._roleId,
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
