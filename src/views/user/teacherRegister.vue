<template>
<div class="row">
  <section class="col-12 q-px-md">
    <!-- <q-layout view="lHh Lpr lFf"> -->
      <q-page-container>
        <div class="row justify-center q-mt-lg">
          <div class="col-md-8">
            <section class="row gutter-sm">
              <q-field class="col-sm-6">
                <q-uploader
                  url="url"
                  float-label="عکس کاربر"
                  name="img"
                  hide-upload-button
                  auto-expand
                  ref="fileUpload"
                  extensions=".jpg"
                />
              </q-field>
              <div class="col-12"></div>
              <base-select
                :model="$v.user.RoleId"
                :options="roleDdl"
                class="col-sm-6 col-md-4"
                
              />
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
              <!-- <base-hr /> -->
              <base-input :model="$v.user.Name" class="col-sm-6 col-md-4" />
              <base-input :model="$v.user.Family" class="col-sm-6 col-md-4" />
              <base-field class="col-sm-6 col-md-4" :model="$v.user.Gender">
                <template slot-scope="data">
                  <q-radio v-model="data.obj.$model" :val="false" label="دختر" />
                  <q-radio v-model="data.obj.$model" :val="true" label="پسر" />
                </template>
              </base-field>
              <base-input :model="$v.user.NationalNo" align="right" class="col-sm-6 col-md-4" />
              <base-input :model="$v.user.Username" class="col-sm-6 col-md-4" />
              <base-input :model="$v.user.Password" type="password" class="col-sm-6 col-md-4" />

              <base-input :model="$v.user.Phone" align="right" class="col-sm-6 col-md-4" />
              <base-input :model="$v.user.Mobile" align="right" class="col-sm-6 col-md-4" />


            </section>
          </div>
        </div>
      </q-page-container>
     
    <!-- </q-layout> -->
    
  </section>

    <template>
      <base-btn-save-back @click="userStore.registerModal"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_REGISTER(false)"></base-btn-back>
    </template>

</div>

</template>
<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userCreateValidations } from "src/validations/user/userCreateValidation";
import { UserType } from "src/utilities/enumeration";

@Component({
  validations: userCreateValidations
})
export default class UserRegisterVue extends Vue {
  $v: any;

  //#region ### data ###
  userStore = vxm.userStore;
  provinceStore = vxm.provinceStore;
  cityStore = vxm.cityStore;
  roleStore = vxm.roleStore;
  user = vxm.userStore.user;

  //#endregion
  selectOptions= [
        {
          label: "معلم" , 
          value: -2
          }
        ]
  //#region ### computed ###
  get roleDdl() {
    return this.selectOptions;
  }
  //#endregion
 open() {
    this.provinceStore.fillList();
    this.cityStore.fillList();
    
  }

  created()
  {
    this.userStore.SET_REGISTER_TEACHER_VUE(this); 

  }
  //#region ### hooks ###
  //#endregion
}
</script>
