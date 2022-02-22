<template>
  <base-modal-edit
    title="اطلاعات کاربری"
    :show="userStore.openModal.update_user"
    size="lg"
    @confirm="userStore.submitUpdateUser"
    @reset="userStore.resetUpdateUser"
    @open="open"
    @close="userStore.OPEN_MODAL_UPDATE_USER_VUE(false)"
  >
    
    <base-select
      :model="$v.user.ProvinceId"
      :options="provinceStore.ddl"
      class="col-sm-6 col-md-4"
      filter
      @change="user.CityId=0"
    />
    <base-select
      :model="$v.user.CityId"
      :options="cityStore.ddlByProvinceId(user.ProvinceId)"
      class="col-sm-6 col-md-4"
      filter
    />
    <div class="col-sm-6 col-md-4" ></div>

    <!-- <base-hr/> -->
    <base-input :model="$v.user.Name" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.user.Family" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.user.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="دختر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="پسر"/>
      </template>
    </base-field>
    <base-input :model="$v.user.NationalNo" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.user.Username" class="col-sm-6 col-md-4"/>
    
   
    <base-input :model="$v.user.Phone" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.user.Mobile" align="right" class="col-sm-6 col-md-4"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userEditValidations } from "src/validations/user/UserEditValidation";
import { UserType } from "src/utilities/enumeration";

@Component({
  validations: userEditValidations
})
export default class UserUpdateVue extends Vue {
  $v: any;

  //#region ### data ###
  userStore = vxm.userStore;
  provinceStore = vxm.provinceStore;
  cityStore = vxm.cityStore;
  roleStore = vxm.roleStore;
  user = vxm.userStore.user;
  //#endregion

  //#region ### computed ###
  get roleDdl() {
    return this.roleStore.ddlByUserType(UserType.Organ);
  }
  //#endregion

  //### methods ###
  open() {
     this.provinceStore.fillList();
     this.cityStore.fillList();
    
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.userStore.SET_UPDATE_USER_VUE(this);
  }
  //#endregion
}
</script>

