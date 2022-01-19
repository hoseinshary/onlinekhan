<template>
  <base-modal-create
    :title="teacherGroupStore.modelName"
    :show="teacherGroupStore.openModal.create"
    @confirm="teacherGroupStore.submitCreate"
    @reset="teacherGroupStore.resetCreate"
    @close="teacherGroupStore.OPEN_MODAL_CREATE(false)"
  >

    <base-input :model="$v.teacherGroup.Name" class="col-md-6"/>

     <!--ATI  -->
    <br />
    <div class="">
        <base-table :grid-data="teacherGroupStore.studentData" :columns="studentGridColumn">
          <template slot="Checked" slot-scope="data">
            <q-checkbox v-model="data.row.Checked" />
          </template>
        </base-table>
    </div>
    <!-- ATI -->

    
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { teacherGroupValidations } from "src/validations/TeacherGroupValidation";

@Component({
  validations: teacherGroupValidations
})
export default class TeacherGroupVue extends Vue {
  $v: any;

  //#region ### data ###
  teacherGroupStore = vxm.teacherGroupStore;
  teacherGroup = vxm.teacherGroupStore.teacherGroup;
  //#endregion


  // ATI
  studentStore = vxm.studentStore;
  studentGridColumn = [
    {
      title: "انتخاب",
      data: "Checked"
    },
    {
      title: "نام",
      data: "FullName"
    },
    {
      title: "کد ملی",
      data: "NationalNo"
    }
  ];
  //ati



  //#region ### hooks ###
  created() {
    // ati
    this.teacherGroupStore.fillStudent();
    console.log( ' this.teacherGroupStore.studentList',this.teacherGroupStore.studentList);
    // ati
    
    this.teacherGroupStore.SET_CREATE_VUE(this);

    

  }
  //#endregion
}
</script>

