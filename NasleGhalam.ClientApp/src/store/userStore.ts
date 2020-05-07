import Vue from "Vue";
import IUser, { DefaultUser } from "src/models/IUser";
import IUserChangePassword, { DefaultUserChangePassword } from "src/models/IUserChangePassword";
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
  openModal: { create: boolean; edit: boolean; delete: boolean ; update_user_image : boolean ; update_user : boolean ; update_user_password : boolean};
  user: IUser;
  userChangePassword : IUserChangePassword;
  loginUser: ILogin;
  private _userList: Array<IUser>;
  private _userListSearch: Array<IUser>;
  private _modelChanged: boolean = true;
  private _createVue: Vue;
  private _registerVue: Vue;
  private _editVue: Vue;
  private _updateUserImageVue : Vue;
  private _updateUserPasswordVue : Vue;
  private _updateUserVue : Vue;
  

  /**
   * initialize data
   */
  constructor() {
    super();

    this.user = util.cloneObject(DefaultUser);
    this.userChangePassword = util.cloneObject(DefaultUserChangePassword);
    this.loginUser = util.cloneObject(DefaultLogin);
    this._userList = [];
    this._userListSearch = [];
    this.openModal = {
      create: false,
      edit: false,
      delete: false,
      update_user_image : false ,
      update_user : false ,
      update_user_password : false 
    };
  }

  //#region ### getters ###
  get modelName() {
    return "کاربر";
  }

  get recordName() {
    return this.user.FullName || "";
  }

  get ddl() {
    return this._userList.map(x => ({
      value: x.Id,
      label: x.FullName
    }));
  }

  get searchDdl() {
    return this._userListSearch.map(x => ({
      value: x.Id,
      label: `${x.FullName} ${x.NationalNo}`
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
    let index = this._userList.findIndex(x => x.Id == this.user.Id);
    if (index < 0) return;
    util.mapObject(user, this._userList[index]);
  }

  @mutation
  private DELETE() {
    let index = this._userList.findIndex(x => x.Id == this.user.Id);
    if (index < 0) return;
    this._userList.splice(index, 1);
  }

  @mutation
  private RESET(vm: any) {
    util.mapObject(DefaultUser, this.user, "Id");
    if (vm.$v) {
      vm.$v.$reset();
    }
  }

  @mutation
  private SET_LIST(list: Array<IUser>) {
    this._userList = list;
  }

  @mutation
  private SET_LIST_Search(list: Array<IUser>) {
    this._userListSearch = list;
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
  SET_REGISTER_VUE(vm: Vue) {
    this._registerVue = vm;
  }

  @mutation
  SET_EDIT_VUE(vm: Vue) {
    this._editVue = vm;
  }

  @mutation
  SET_UPDATE_USER_IMAGE_VUE(vm: Vue) {
    this._updateUserImageVue = vm;
  }

  @mutation
  SET_UPDATE_USER_PASSWORD_VUE(vm: Vue) {
    this._updateUserPasswordVue = vm;
  }

  @mutation
  SET_UPDATE_USER_VUE(vm: Vue) {
    this._updateUserVue= vm;
  }

  @mutation
  OPEN_MODAL_UPDATE_USER_IMAGE_VUE(open: boolean) {
    this.openModal.update_user_image = open;
  }

  @mutation
  OPEN_MODAL_UPDATE_USER_PASSWORD_VUE(open: boolean) {
    this.openModal.update_user_password = open;
  }

  @mutation
  OPEN_MODAL_UPDATE_USER_VUE(open: boolean) {
    this.openModal.update_user = open;
  }


  //#endregion

  //#region ### actions ###
  @action()
  async getById(id: number) {
    return axios
      .get(`${baseUrl}/GetById/${id}`)
      .then((response: AxiosResponse<IUser>) => {
        util.mapObject(response.data, this.user);
      });
  }

  @action()
  async getUserData() {
    return axios
      .get(`${baseUrl}/GetUserData`)
      .then((response: AxiosResponse<IUser>) => {
        util.mapObject(response.data, this.user);
      });
  }

  @action()
  async fillList() {
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

  @action()
  async fillListSupervisor() {
    if (this._modelChanged) {
      return axios
        .get(`${baseUrl}/GetAllSupervisors`)
        .then((response: AxiosResponse<Array<IUser>>) => {
          this.SET_LIST(response.data);
          this.MODEL_CHANGED(false);
        });
    } else {
      return Promise.resolve(this._userList);
    }
  }

  @action()
  async search(payload: { nationalNo: string; family: string; name: string }) {
    var data = util.toParam(payload);
    return axios
      .get(`${baseUrl}/Search?${data}`)
      .then((response: AxiosResponse<Array<IUser>>) => {
        this.SET_LIST_Search(response.data);
      });
  }

  @action({ mode: "raw" })
  async validateForm(vm: any) {
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
  async validateFormChangePassword(vm: any) {
    
    
    return new Promise(resolve => {
      vm.$v.userChangePassword.$touch();
      if (vm.$v.userChangePassword.$error) {
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
    this.user.Id = 0;
    this.RESET(this._createVue);
  }

  @action()
  async resetRegister() {
    this.user.Id = 0;
    this.RESET(this._registerVue);
  }


  @action()
  async resetUpdateUserImage() {
    this.user.Id = 0;
    var fileUpload = this._updateUserImageVue.$refs.fileUpload;
    if (fileUpload) {
      fileUpload["reset"]();
    }
  }

  @action()
  async resetUpdateUserPassword() {
    this.user.Id = 0;
    this.RESET(this._updateUserPasswordVue);
   
  }

  @action()
  async resetUpdateUser() {
    this.user.Id = 0;
    this.RESET(this._updateUserVue);
   
  }


  @action()
  async submitEdit() {
    let vm = this._editVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/Update`, this.user)
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
  async submitUpdateUser() {
    let vm = this._updateUserVue;
    if (!(await this.validateForm(vm))) return;

    return axios
      .post(`${baseUrl}/UpdateUser`, this.user)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.UPDATE(data.Obj);
          this.OPEN_MODAL_UPDATE_USER_VUE(false);
          this.MODEL_CHANGED(true);
          this.resetUpdateUser();
        }
      });
  }

  @action()
  async submitUpdateUserPassword() {
  
    let vm = this._updateUserPasswordVue;
   
    if (!(await this.validateFormChangePassword(vm))) return;

    return axios
      .post(`${baseUrl}/UpdateUserPassword`, this.userChangePassword)
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.OPEN_MODAL_UPDATE_USER_PASSWORD_VUE(false);
          this.resetUpdateUserPassword();
        }
      });
  }


  @action()
  async submitUpdateUserImage() {
    
    let vm = this._updateUserImageVue;
    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    else
    {
      await this.notify({vm , data : {Message : "فایل باید وارد شود." , MessageType : MessageType.Error ,Obj : null }})
      return ;
    }
    return axios({
      method: "post",
      url: `${baseUrl}/UpdateUserImage`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    }).then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
          this.OPEN_MODAL_UPDATE_USER_IMAGE_VUE(false);
          this.resetUpdateUserImage(); 

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
      .post(`${baseUrl}/Delete/${this.user.Id}`)
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
  async login(vm: Vue) {
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
          LocalStorage.set("ProfilePic", data.ProfilePic);
          
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

  @action()
  async register() {
    let vm = this._registerVue;
    if (!(await this.validateForm(vm))) return;

    var formData = new FormData();
    var fileUpload = vm.$refs.fileUpload;

    if (fileUpload && fileUpload["files"].length > 0) {
      formData.append(fileUpload["name"], fileUpload["files"][0]);
    }
    var params = util.toParam(this.user);


    return axios({
      method: "post",
      url: `${baseUrl}/Register?${params}`,
      data: formData,
      headers: { "Content-Type": "multipart/form-data" }
    })
      .then((response: AxiosResponse<IMessageResult>) => {
        let data = response.data;
        this.notify({ vm, data });

        if (data.MessageType == MessageType.Success) {
  
          this.resetRegister();
          router.push("/user/login");
        }
      });
  }

  //#endregion
}

export const userStore = UserStore.ExtractVuexModule(UserStore);
