<template>
  <base-modal-delete
    :title="publisherStore.modelName"
    :recordName="publisherStore.recordName"
    :show="publisherStore.openModal.delete"
    @confirm="confirm"
    @close="publisherStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class PublisherDeleteVue extends Vue {
  //#region ### data ###
  publisherStore = vxm.publisherStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.publisherStore.submitDelete(this);
  }
  //#endregion
}
</script>