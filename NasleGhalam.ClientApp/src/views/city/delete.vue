<template>
  <base-modal-delete
    :title="cityStore.modelName"
    :recordName="cityStore.recordName"
    :show="cityStore.openModal.delete"
    @confirm="confirm"
    @close="cityStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class CityDeleteVue extends Vue {
  //#region ### data ###
  cityStore = vxm.cityStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.cityStore.submitDelete(this);
  }
  //#endregion
}
</script>

