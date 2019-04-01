import Vue from "Vue";
import IUser, { DefaultUser } from "src/models/IUser";
import ILogin, { DefaultLogin } from "src/models/ILogin";
import IMessageResult from "src/models/IMessageResult";
import axios, { AxiosResponse } from "src/plugins/axios";
import { MessageType } from "src/utilities/enumeration";
import { USER_URL as baseUrl } from "src/utilities/site-config";
import util from "src/utilities";
import {
  VuexModule,
  mutation,
  action,
  Module,
  getRawActionContext
} from "vuex-class-component";
import { LocalStorage } from "quasar";
import router from "src/router";

@Module({ namespacedPath: "userStore/" })
export class UserStore extends VuexModule {
  openModal: { create: boolean; edit: boolean; delete: boolean };
  user: IUser;
  loginUser: ILogin;
  private _userList: Array<IUser>;
  private _selectedId: number = 0;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.user = util.cloneObject(DefaultUser);
    this.loginUser = util.cloneObject(DefaultLogin);
    this._userList = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false
    };
  }

  //#region ### getters ###
  get modelName() {
    return "کاربر";
  }

  get recordName() {
    return this.user.Name || "";
  }

  get ddl() {
    return this._userList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this._userList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(user: IUser) {
    this._userList.push(user);
  }

  @mutation
  private UPDATE(user: IUser) {
    let index = this._userList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    util.mapObject(user, this._userList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._userList.findIndex(x => x.Id == this._selectedId);
    if (index < 0) return;
    this._userList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultUser, this.user);
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IUser>) {
    this._userList = list;
  }

  @mutation
  private SET_SELECTED_ID(id: number) {
    this._selectedId = id;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this._modelChanged = changed;
  }

  @mutation
  OPEN_MODAL_CREATE(open: boolean) {
    this.openModal.create = open;
  }

  @mutation
  OPEN_MODAL_EDIT(open: boolean) {
    this.openModal.edit = open;
  }

  @mutation
  OPEN_MODAL_DELETE(open: boolean) {
    this.openModal.delete = open;
  }

  @mutation
  SET_CREATE_VUE(vm: Vue) {
    this._createVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this._editVue = vm;
  }
  //#endregion

  //#region ### actions ###
  @action()
  getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IUser>) => {
        util.mapObject(response.data, this.user);
        this.SET_SELECTED_ID(this.user.Id);
      });
  }

  @action()
  fillList() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IUser>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._userList);
    }
  }

  @action({ mode: "raw" })
  validateForm(vm: any) {
    return new Promise(resolve => {
      vm.$v.user.$touch();
      if (vm.$v.user.$error) {
        const context = getRawActionContext(this);
        context.dispatch("notifyInvalidForm", vm, { root: true });
        resolve(false);
      }
      resolve(true);
    });
  }

  @action({ mode: "raw" })
  notify(payload: { vm: Vue; data: IMessageResult }) {
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
  async submitCreate(closeModal: boolean) {
    let vm = this._createVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Create`, this.user)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.CREATE(data.Obj);
          this.OPEN_MODAL_CREATE(!closeModal);
          this.MODEL_CHANGED(true);
          this.resetCreate();
        }
      });
  }

  @action()
  async resetCreate() {
    this.RESET(this._createVue);
  }

  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    this.user.Id = this._selectedId;
    return axios
      .post(`${baseUrl}/Update/${this._selectedId}`, this.user)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.UPDATE(data.Obj);
          this.OPEN_MODAL_EDIT(false);
          this.MODEL_CHANGED(true);
          this.resetEdit();
        }
      });
  }

  @action()
  async resetEdit() {
    this.RESET(this._editVue);
  }

  @action()
  async submitDelete(vm: Vue) {
    return axios
      .post(`${baseUrl}/Delete/${this._selectedId}`)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });
        if (data.MessageType == MessageType.Success) {
          this.DELETE();
          this.OPEN_MODAL_DELETE(false);
          this.MODEL_CHANGED(true);
        }
      });
  }

  @action()
  login(vm: Vue) {
    var data = {
      Username: this.loginUser.Username1,
      Password: this.loginUser.Password1
    };
    return axios
      .post(`${baseUrl}/Login`, data)
      .then((response: AxiosResponse<any>) => {
        let data = response.data;

        if (data.MessageType == MessageType.Success) {
          axios.defaults.headers.common["Token"] = data.Token;
          LocalStorage.set("Token", data.Token);
          LocalStorage.set("FullName", data.FullName);
          LocalStorage.set(
            "authList",
            data.SubMenus.map(x => x.EnName.toLowerCase())
          );
          LocalStorage.set("menuList", data.Menus);
          LocalStorage.set("subMenuList", data.SubMenus);
          router.push(data.DefaultPage);
        } else {
          this.notify({ vm, data });
        }
      });
  }
  //#endregion
}

export const userStore = UserStore.ExtractVuexModule(UserStore);
