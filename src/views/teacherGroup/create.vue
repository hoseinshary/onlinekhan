<template>
  <base-modal-create
    :title="teacherGroupStore.modelName"
    :show="teacherGroupStore.openModal.create"
    @confirm="teacherGroupStore.submitCreate"
    @reset="teacherGroupStore.resetCreate"
    @close="teacherGroupStore.OPEN_MODAL_CREATE(false)"
  >

     <!--ATI  -->
    <br />
    <div class="">
        <base-table :grid-data="lesson_UserStore.userData" :columns="userGridColumn">
          <template slot="Checked" slot-scope="data">
            <q-checkbox v-model="data.row.Checked" />
          </template>
        </base-table>
    </div>
    <!-- ATI -->

    <base-input :model="$v.teacherGroup.Name" class="col-md-6"/>
    <base-input :model="$v.teacherGroup.Id" class="col-md-6"/>
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
  lesson_UserStore = vxm.lesson_UserStore;
  userGridColumn = [
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
  provinceGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "کد",
      data: "Id"
    }
  ];
  //ati

  //#region ### hooks ###
  created() {
    this.teacherGroupStore.SET_CREATE_VUE(this);

    // ati
    this.lesson_UserStore.fillUserList();
    // ati

  }
  //#endregion
}
</script>

