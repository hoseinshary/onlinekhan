<template>
  <base-modal-delete
    :title="studentMajorListStore.modelName"
    :recordName="studentMajorListStore.recordName"
    :show="studentMajorListStore.openModal.delete"
    @confirm="confirm"
    @close="studentMajorListStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class StudentMajorListDeleteVue extends Vue {
  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.studentMajorListStore.submitDelete(this);
  }
  //#endregion
}
</script>