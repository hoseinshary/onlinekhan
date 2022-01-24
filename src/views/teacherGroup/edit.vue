<template>
  <base-modal-edit
    :title="teacherGroupStore.modelName"
    :show="teacherGroupStore.openModal.edit"
    @confirm="teacherGroupStore.submitEdit"
    @reset="teacherGroupStore.resetEdit"
    @close="teacherGroupStore.OPEN_MODAL_EDIT(false)"
  >
  <base-input :model="$v.teacherGroup.Name" class="col-md-6"/>

    <br />
    <div class="">
        <base-table :grid-data="teacherGroupStore.studentDataForEdit" :columns="studentGridColumn">
          <template slot="Checked" slot-scope="data">
            <q-checkbox v-model="data.row.Checked" />
          </template>
        </base-table>
    </div>

  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { teacherGroupValidations } from "src/validations/TeacherGroupValidation";

@Component({
  validations: teacherGroupValidations
})
export default class ProvinceEditVue extends Vue {
  $v: any;

  //#region ### data ###
  teacherGroupStore = vxm.teacherGroupStore;
  teacherGroup = vxm.teacherGroupStore.teacherGroup;


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
  //#endregion

  //#region ### hooks ###
  created() {
    this.teacherGroupStore.fillStudent();

    this.teacherGroupStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

