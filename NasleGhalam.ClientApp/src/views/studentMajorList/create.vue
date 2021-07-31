<template>
  <base-modal-create
    :title="studentMajorListStore.modelName"
    :show="studentMajorListStore.openModal.create"
    @confirm="studentMajorListStore.submitCreate"
    @reset="studentMajorListStore.resetCreate"
    @close="studentMajorListStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.studentMajorList.Name" class="col-md-6"/>
    <base-input :model="$v.studentMajorList.Code" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentMajorListValidations } from "src/validations/StudentMajorListValidation";

@Component({
  validations: studentMajorListValidations
})
export default class StudentMajorListCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  studentMajorList = vxm.studentMajorListStore.studentMajorList;
  //#endregion

  //#region ### hooks ###
  created() {
    this.studentMajorListStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

