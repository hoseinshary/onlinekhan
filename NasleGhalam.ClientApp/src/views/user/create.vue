<template>
  <base-modal-create
    :title="userStore.modelName"
    :show="userStore.openModal.create"
    size="lg"
    @confirm="userStore.submitCreate"
    @reset="userStore.resetCreate"
    @open="open"
    @close="userStore.OPEN_MODAL_CREATE(false)"
  >
    <base-select
      :model="$v.user.RoleId"
      :options="roleStore.ddl"
      class="col-sm-6 col-md-4"
      filter
      ref="roleId"
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
      :options="cityStore.cityByProvinceIdDdl(user.ProvinceId)"
      class="col-sm-6 col-md-4"
      filter
    />
    <!-- <base-hr /> -->
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
    <base-input :model="$v.user.Password" type="password" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.user.IsActive">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model"/>
      </template>
    </base-field>
    <base-input :model="$v.user.Phone" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.user.Mobile" align="right" class="col-sm-6 col-md-4"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userCreateValidations } from "src/validations/user/UserCreateValidation";

@Component({
  validations: userCreateValidations
})
export default class UserCreateVue extends Vue {
  $v: any;

  //### data ###
  userStore = vxm.userStore;
  provinceStore = vxm.provinceStore;
  cityStore = vxm.cityStore;
  roleStore = vxm.roleStore;
  user = vxm.userStore.user;
  //--------------------------------------------------

  //### methods ###
  open() {
    this.provinceStore.fillList();
    this.cityStore.fillList();
    this.roleStore.fillList();
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.userStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}
</script>
