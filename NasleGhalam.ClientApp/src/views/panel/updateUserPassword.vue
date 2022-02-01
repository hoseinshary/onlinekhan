<template>
  <bs-modal
    title="تغییر رمز عبور "
    :show="userStore.openModal.update_user_password"
    @close="userStore.OPEN_MODAL_UPDATE_USER_PASSWORD_VUE(false)"
    
  >


    <template slot="header">
      <q-toolbar slot="header" color="secondary" text-color>
        <q-toolbar-title>تغییر رمز عبور</q-toolbar-title>
        <q-btn dense icon="close" @click="userStore.OPEN_MODAL_UPDATE_USER_PASSWORD_VUE(false)" />
      </q-toolbar>
    </template>

<slot>
  <div class="caption">رمز عبور قبلی</div>
  <base-input :model="$v.userChangePassword.OldPassword" type="password" class="col-sm-12 col-md-12"/>
  <div class="caption">رمز عبور جدید</div>
   <base-input :model="$v.userChangePassword.NewPassword" type="password" class="col-sm-12 col-md-12"/>
   <div class="caption">تکرار رمز عبور جدید</div>
    <base-input :model="$v.userChangePassword.ReNewPassword" type="password" class="col-sm-12 col-md-12"/>
</slot>

    <template slot="footer">
      <base-btn-save-back @click="userStore.submitUpdateUserPassword()"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_UPDATE_USER_PASSWORD_VUE(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userChangePasswordValidations } from "src/validations/user/UserChangePasswordValidation";

@Component({
  validations: userChangePasswordValidations
})
export default class UpdateUserPasswordVue extends Vue {
  $v: any;


  //#region ### data ###
  userStore = vxm.userStore;
  userChangePassword = vxm.userStore.userChangePassword;
  //#endregion

  //#region ### hooks ###
  created() {
    this.userStore.SET_UPDATE_USER_PASSWORD_VUE(this);
  }
  //#endregion
}
</script>

