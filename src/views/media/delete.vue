<template>
  <base-modal-delete
    :title="mediaStore.modelName"
    :recordName="mediaStore.recordName"
    :show="mediaStore.openModal.delete"
    @confirm="confirm"
    @close="mediaStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class MediaDeleteVue extends Vue {
  //#region ### data ###
  mediaStore = vxm.mediaStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.mediaStore.submitDelete(this);
  }
  //#endregion
}
</script>