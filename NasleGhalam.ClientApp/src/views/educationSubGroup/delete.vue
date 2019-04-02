<template>
  <base-modal-delete
    :title="educationSubGroupStore.modelName"
    :recordName="educationSubGroupStore.recordName"
    :show="educationSubGroupStore.openModal.delete"
    @confirm="confirm"
    @close="educationSubGroupStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class EducationSubGroupDeleteVue extends Vue {
  //### data ###
  educationSubGroupStore = vxm.educationSubGroupStore;
  //--------------------------------------------------

  //### methods ###
  confirm() {
    this.educationSubGroupStore.submitDelete(this);
  }
  //--------------------------------------------------
}
</script>