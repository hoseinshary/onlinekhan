<template>
  <base-modal-delete
    :title="provinceStore.modelName"
    :recordName="provinceStore.recordName"
    :show="provinceStore.openModal.delete"
    @confirm="confirm"
    @close="provinceStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class ProvinceDeleteVue extends Vue {
  //#region ### data ###
  provinceStore = vxm.provinceStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.provinceStore.submitDelete(this);
  }
  //#endregion
}
</script>