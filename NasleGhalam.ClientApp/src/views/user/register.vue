<template>
<section class="col-12 q-px-md">
  <q-layout view="lHh Lpr lFf">
    <q-page-container>

      <div class="row justify-center q-mt-lg">
        <section class="col-md-8">
          <!-- panel -->
          <base-panel>
            <span slot="title">{{userStore.modelName}}</span>
            <div slot="body">
              <section class="row gutter-sm">
                <base-select
                  :model="$v.user.RoleId"
                  :options="roleDdl"
                  class="col-sm-6 col-md-4"
                  filter
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

                <div class="col-12">
                  <base-btn-save @click="userStore.submitCreate" />
                </div>
              </section>
            </div>
          </base-panel>
        </section>
      </div>
    </q-page-container>
  </q-layout>
</section>
</template>
<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userCreateValidations } from "src/validations/user/UserCreateValidation";
import { UserType } from "src/utilities/enumeration";

@Component({
  validations: userCreateValidations
})
export default class UserCreateVue extends Vue {
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

  //#region ### methods ###
  open() {
    this.provinceStore.fillList();
    this.cityStore.fillList();
    this.roleStore.fillList();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.userStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>
