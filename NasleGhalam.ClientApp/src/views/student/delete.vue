<template>
  <base-modal-delete
    :title="studentStore.modelName"
    :recordName="studentStore.recordName"
    :show="studentStore.openModal.delete"
    @confirm="confirm"
    @close="studentStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class StudentDeleteVue extends Vue {
  //#region ### data ###
  studentStore = vxm.studentStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.studentStore.submitDelete(this);
  }
  //#endregion
}
</script>