<template>
  <base-modal-delete
    :title="axillaryBookStore.modelName"
    :recordName="axillaryBookStore.recordName"
    :show="axillaryBookStore.openModal.delete"
    @confirm="confirm"
    @close="axillaryBookStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class AxillaryBookDeleteVue extends Vue {
  //#region ### data ###
  axillaryBookStore = vxm.axillaryBookStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.axillaryBookStore.submitDelete(this);
  }
  //#endregion
}
</script>