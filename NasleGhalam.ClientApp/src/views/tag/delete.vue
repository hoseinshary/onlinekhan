<template>
  <base-modal-delete
    :title="tagStore.modelName"
    :recordName="tagStore.recordName"
    :show="tagStore.openModal.delete"
    @confirm="confirm"
    @close="tagStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class TagDeleteVue extends Vue {
  //#region ### data ###
  tagStore = vxm.tagStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.tagStore.submitDelete(this);
  }
  //#endregion
}
</script>