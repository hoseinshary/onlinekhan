<template>
  <base-modal-edit
    :title="studentMajorListStore.modelName"
    :show="studentMajorListStore.openModal.edit"
    @confirm="studentMajorListStore.submitEdit"
    @reset="studentMajorListStore.resetEdit"
    @close="studentMajorListStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.studentMajorList.Name" class="col-md-6"/>
    <base-input :model="$v.studentMajorList.Code" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentMajorListValidations } from "src/validations/StudentMajorListValidation";

@Component({
  validations: studentMajorListValidations
})
export default class StudentMajorListEditVue extends Vue {
  $v: any;

  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  studentMajorList = vxm.studentMajorListStore.studentMajorList;
  //#endregion

  //#region ### hooks ###
  created() {
    this.studentMajorListStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

