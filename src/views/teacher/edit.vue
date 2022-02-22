<template>
  <base-modal-edit
    :title="teacherStore.modelName"
    :show="teacherStore.openModal.edit"
    size="lg"
    @confirm="teacherStore.submitEdit"
    @reset="teacherStore.resetEdit"
    @open="open"
    @close="teacherStore.OPEN_MODAL_EDIT(false)"
  >
    <base-select :model="$v.teacher.User.RoleId" :options="roleDdl" class="col-sm-6 col-md-4"/>
    <base-select
      :model="$v.teacher.User.ProvinceId"
      :options="provinceStore.ddl"
      class="col-sm-6 col-md-4"
      filter
      @change="teacher.User.CityId=0"
    />
    <base-select
      :model="$v.teacher.User.CityId"
      :options="cityStore.ddlByProvinceId(teacher.User.ProvinceId)"
      class="col-sm-6 col-md-4"
      clearable
      ref="cityId"
    />
    <base-input :model="$v.teacher.User.Name" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.teacher.User.Family" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.teacher.User.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="دختر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="پسر"/>
      </template>
    </base-field>
    <base-input :model="$v.teacher.User.NationalNo" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.teacher.User.Username" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.teacher.User.Password" type="password" class="col-sm-6 col-md-4"/>
    <base-field class="col-sm-6 col-md-4" :model="$v.teacher.User.IsActive">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model"/>
      </template>
    </base-field>
    <base-input :model="$v.teacher.User.Phone" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.teacher.User.Mobile" align="right" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.teacher.FatherName" class="col-md-6"/>
    <base-input :model="$v.teacher.Address" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { teacherValidations } from "src/validations/teacher/teacherEditValidation";
import { UserType } from "../../utilities/enumeration";

@Component({
  validations: teacherValidations
})
export default class TeacherEditVue extends Vue {
  $v: any;

  //#region ### data ###
  teacherStore = vxm.teacherStore;
  teacher = vxm.teacherStore.teacher;
  roleStore = vxm.roleStore;
  cityStore = vxm.cityStore;
  provinceStore = vxm.provinceStore;
  //#endregion

  //#region ### computed ###
  get roleDdl() {
    return this.roleStore.ddlByUserType(UserType.Teacher);
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
    this.teacherStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

