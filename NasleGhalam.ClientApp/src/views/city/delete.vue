<template>
  <base-modal-delete
    :title="cityStore.modelName"
    :recordName="cityStore.recordName"
    :show="cityStore.openModal.delete"
    @confirm="cityStore.submitDelete"
    @close="cityStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class CityDeleteVue extends Vue {
  //### data ###
  cityStore = vxm.cityStore;
  //--------------------------------------------------
}
</script>

