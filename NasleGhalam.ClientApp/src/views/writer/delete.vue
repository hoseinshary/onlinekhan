<template>
  <base-modal-delete
    :title="writerStore.modelName"
    :recordName="writerStore.recordName"
    :show="writerStore.openModal.delete"
    @confirm="confirm"
    @close="writerStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class WriterDeleteVue extends Vue {
  //#region ### data ###
  writerStore = vxm.writerStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.writerStore.submitDelete(this);
  }
  //#endregion
}
</script>