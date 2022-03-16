<template>
<bs-modal
    :title="userStore.modelName"
    :show="userStore.openModal.register"
    size="lg"
    @open="open"
  >

     <template slot="header">
      <q-toolbar slot="header" color="yellow-8">
        <q-toolbar-title>{{ select }}</q-toolbar-title>
        <q-btn
          dense
          icon="close"
          @click="userStore.OPEN_MODAL_REGISTER(false)"
        />

      </q-toolbar>
    </template>

    <template class="q-center">

      <div>
        <q-list>
          <q-item  label=" دانش آموز">
            <q-btn
            @click="openStudentRegister"
             flat>
              ثبت نام دانش آموز
            </q-btn>
            </q-item>
            <q-item  label=" معلم">
              <q-btn 
              @click="openTeacherRegister"
              flat>
                ثبت نام معلم
              </q-btn>
             </q-item>
          </q-list>
       </div>


        <student-register
        v-show="selected === 'student'"></student-register>

        <teacher-register
        v-show="selected === 'teacher'"></teacher-register>


    </template>



    </bs-modal>

</template>
<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { UserType } from "src/utilities/enumeration";
import { userLoginValidations } from "src/validations/user/userLoginValidation";
@Component({
  validations: userLoginValidations,

  components: {
    studentRegister: () => import("./studentRegister.vue"),
    teacherRegister:()=>import('./teacherRegister.vue')
  }
})

export default class UserRegisterVue extends Vue {
  $v: any;

  //#region ### data ###
  userStore = vxm.userStore;
  studentStore = vxm.studentStore;
  provinceStore = vxm.provinceStore;
  cityStore = vxm.cityStore;
  roleStore = vxm.roleStore;
  user = vxm.userStore.user;
  select='انتخاب نقش';
  selected='student'
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
  //#endregion
 open() {
   
  }

  openStudentRegister()
    {
      this.selected = 'student'
    }
  openTeacherRegister(){
    this.selected = 'teacher'
  }

  //#region ### hooks ###
  created() { 
    this.provinceStore.fillList();
    this.cityStore.fillList();
    this.userStore.SET_REGISTER_VUE(this);
  }
  //#endregion
}
</script>

<style scoped>
.profile-menu{
  position: fixed;
  z-index: 100;
  margin-top:4.7rem;
  top: 0;
  color:#0A3F7E ;
  background-color: #fcfaf9;
  border: 2px solid #48e5c2;
  border-radius: 4px;
}
.profile-menu .q-item{
  padding: 0;
}
</style>
