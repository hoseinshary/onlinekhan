<template>
  <base-modal-delete
    :title="packageStore.modelName"
    :recordName="packageStore.recordName"
    :show="packageStore.openModal.delete"
    @confirm="confirm"
    @close="packageStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class PackageDeleteVue extends Vue {
  //#region ### data ###
  packageStore = vxm.packageStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.packageStore.submitDelete(this);
  }
  //#endregion
}
</script>