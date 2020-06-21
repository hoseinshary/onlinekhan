<template>
  <base-modal-delete
    :title="programStore.modelName"
    :recordName="programStore.recordName"
    :show="programStore.openModal.delete"
    @confirm="confirm"
    @close="programStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class ProgramDeleteVue extends Vue {
  //#region ### data ###
  programStore = vxm.programStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.programStore.submitDelete(this);
  }
  //#endregion
}
</script>