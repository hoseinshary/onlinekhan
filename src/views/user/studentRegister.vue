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
                  float-label="عکس دانش آموز"
                  name="img"
                  hide-upload-button
                  auto-expand
                  ref="fileUpload"
                  extensions=".jpg"
                />
              </q-field>
              <div class="col-12"></div>
              <base-select
                :model="$v.student.User.RoleId"
                :options="roleDdl"
                class="col-sm-6 col-md-4"
                
              />
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
                filter
              />
              <!-- <base-hr /> -->
              <base-input :model="$v.student.User.Name" class="col-sm-6 col-md-4" />
              <base-input :model="$v.student.User.Family" class="col-sm-6 col-md-4" />
              <base-field class="col-sm-6 col-md-4" :model="$v.student.User.Gender">
                <template slot-scope="data">
                  <q-radio v-model="data.obj.$model" :val="false" label="دختر" />
                  <q-radio v-model="data.obj.$model" :val="true" label="پسر" />
                </template>
              </base-field>
              <base-input :model="$v.student.User.NationalNo" align="right" class="col-sm-6 col-md-4" />
              <base-input :model="$v.student.User.Username" class="col-sm-6 col-md-4" />
              <base-input :model="$v.student.User.Password" type="password" class="col-sm-6 col-md-4" />

              <base-input :model="$v.student.User.Phone" align="right" class="col-sm-6 col-md-4" />
              <base-input :model="$v.student.User.Mobile" align="right" class="col-sm-6 col-md-4" />

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



            </section>
          </div>
        </div>
      </q-page-container>
     
    <!-- </q-layout> -->
    
  </section>

  <template>
      <base-btn-save-back @click="studentStore.registerModal"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_REGISTER(false)"></base-btn-back>
  </template>

</div>

</template>
<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentValidations } from "src/validations/student/studentCreateValidation";
import { Field, Maghta, UserType } from "src/utilities/enumeration";
import  util  from '../../utilities/index';

@Component({
  validations: studentValidations
})
export default class StudentRegisterVue extends Vue {
  $v: any;

  //#region ### data ###
  studentStore = vxm.studentStore;
  provinceStore = vxm.provinceStore;
  cityStore = vxm.cityStore;
  roleStore = vxm.roleStore;
  user = vxm.userStore.user;
  userStore = vxm.userStore;

  student = vxm.studentStore.student;

  //#endregion
  selectOptions= [
        {
          label: "دانش آموز" , 
          value: -2
          }
        ]
  //#region ### computed ###
  get roleDdl() {
    return this.selectOptions;
  }
  get getField(){
    return util.enumToDdl(Field)
  }
  get getMaghta(){
    return util.enumToDdl(Maghta)
  }
  //#endregion
  


 open() {
    this.provinceStore.fillList();
    this.cityStore.fillList(); 
  }

  created()
  {
    this.studentStore.SET_REGISTER_STUDENT_VUE(this);

  }

  //#region ### hooks ###
  //#endregion
}
</script>
