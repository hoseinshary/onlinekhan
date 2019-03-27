import Vue from "Vue";
import IUser, { DefaultUser } from "src/models/IUser";
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
  loginUser: { Username: string; Password: string };
  private userList: Array<IUser>;
  private selectedId: number = 0;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;

  /**
   * initialize data
   */
  constructor() {
    super();

    this.user = util.cloneObject(DefaultUser);
    this.loginUser = { Password: "", Username: "" };
    this.userList = [];
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
    return this.userList.map(x => ({
      value: x.Id,
      label: x.Name
    }));
  }

  get gridData() {
    return this.userList;
  }
  //#endregion

  //#region ### mutations ###
  @mutation
  private CREATE(user: IUser) {
    this.userList.push(user);
  }

  @mutation
  private UPDATE(user: IUser) {
    let index = this.userList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    util.mapObject(user, this.userList[index]);
  }

  @mutation
  private DELETE() {
    let index = this.userList.findIndex(x => x.Id == this.selectedId);
    if (index < 0) return;
    this.userList.splice(index, 1);
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
    this.userList = list;
  }

  @mutation
  private SET_SELECTED_ID(id: number) {
    this.selectedId = id;
  }

  @mutation
  private MODEL_CHANGED(changed: boolean) {
    this.modelChanged = changed;
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
    this.createVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this.editVue = vm;
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
    if (this.modelChanged) {
      return axios
        .get(`${baseUrl}/GetAll`)
        .then((response: AxiosResponse<Array<IUser>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this.userList);
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
    let vm = this.createVue;
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
    this.RESET(this.createVue);
  }

  @action()
  async submitEdit() {
    let vm = this.createVue;
    if (!(await this.validateForm(vm))) return;

    this.user.Id = this.selectedId;
    return axios
      .post(`${baseUrl}/Update/${this.selectedId}`, this.user)
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
    this.RESET(this.editVue);
  }

  @action()
  async submitDelete() {
    let vm = this.createVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Delete/${this.selectedId}`)
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
    return axios
      .post(`${baseUrl}/Login`, this.loginUser)
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
