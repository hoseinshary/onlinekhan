<template>
  <base-modal-delete
    :title="educationTreeStore.modelName"
    :recordName="educationTreeStore.recordName"
    :show="educationTreeStore.openModal.delete"
    @confirm="confirm"
    @close="educationTreeStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class EducationTreeDeleteVue extends Vue {
  //### data ###
  educationTreeStore = vxm.educationTreeStore;
  //--------------------------------------------------

  //### methods ###
  confirm() {
    this.educationTreeStore.submitDelete(this);
  }
  //--------------------------------------------------
}
</script>