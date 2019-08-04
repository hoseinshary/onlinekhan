<template>
  <base-modal-edit
    :title="studentStore.modelName"
    :show="studentStore.openModal.edit"
    size="lg"
    @confirm="studentStore.submitEdit"
    @reset="studentStore.resetEdit"
    @open="open"
    @close="studentStore.OPEN_MODAL_EDIT(false)"
  >
    <base-select :model="$v.student.User.RoleId" :options="roleDdl" class="col-sm-6 col-md-4"/>
    <base-select
      :model="$v.student.User.ProvinceId"
      :options="provinceStore.ddl"
      class="col-sm-6 col-md-4"
      filter
      @change="student.User.CityId=0"
    />
    <base-select
      :model="$v.student.User.CityId"
      :options="cityStore.ddlByProvinceId(student.User.ProvinceId)"
      class="col-sm-6 col-md-4"
      clearable
      ref="cityId"
    />
    <base-input :model="$v.student.User.Name" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.student.User.Family" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.student.User.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="دختر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="پسر"/>
      </template>
    </base-field>
    <base-input :model="$v.student.User.NationalNo" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.student.User.Username" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.student.User.Password" type="password" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.student.User.IsActive">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model"/>
      </template>
    </base-field>
    <base-input :model="$v.student.User.Phone" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.student.User.Mobile" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.student.FatherName" class="col-md-6"/>
    <base-input :model="$v.student.Address" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentValidations } from "src/validations/student/studentEditValidation";
import { UserType } from "../../utilities/enumeration";

@Component({
  validations: studentValidations
})
export default class StudentEditVue extends Vue {
  $v: any;

  //#region ### data ###
  studentStore = vxm.studentStore;
  student = vxm.studentStore.student;
  roleStore = vxm.roleStore;
  cityStore = vxm.cityStore;
  provinceStore = vxm.provinceStore;
  //#endregion

  //#region ### computed ###
  get roleDdl() {
    return this.roleStore.ddlByUserType(UserType.Student);
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
    this.studentStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

