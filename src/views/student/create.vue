<template>
  <base-modal-create
    :title="studentStore.modelName"
    :show="studentStore.openModal.create"
    size="lg"
    @confirm="studentStore.submitCreate"
    @reset="studentStore.resetCreate"
    @open="open"
    @close="studentStore.OPEN_MODAL_CREATE(false)"
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

    
              <base-select
                :model="$v.student.EducationGroupEnum"
                :options="getField"
                class="col-sm-6 col-md-4"
                filter
              />

              <base-select
                :model="$v.student.GradeEnum"
                :options="getMaghta"
                class="col-sm-6 col-md-4"
                filter
              />

              <base-input :model="$v.student.SchoolName" align="right" class="col-sm-6 col-md-4" />


              <div class="col-sm-6 col-md-4"></div>

              <base-input :model="$v.student.IntroducedCode" align="right" class="col-sm-6 col-md-4" />


  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentValidations } from "src/validations/student/studentCreateValidation";
import { UserType } from "../../utilities/enumeration";
import  util  from '../../utilities/index';
import { Field, Maghta } from "src/utilities/enumeration";



@Component({
  validations: studentValidations
})
export default class StudentCreateVue extends Vue {
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
   get getField(){
    return util.enumToDdl(Field)
  }
  get getMaghta(){
    return util.enumToDdl(Maghta)
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
    this.studentStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

