<template>
  <base-modal-delete
    :title="assayStore.modelName"
    :recordName="assayStore.recordName"
    :show="assayStore.openModal.delete"
    @confirm="confirm"
    @close="assayStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class AssayDeleteVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.assayStore.submitDelete(this);
  }
  //#endregion
}
</script>

