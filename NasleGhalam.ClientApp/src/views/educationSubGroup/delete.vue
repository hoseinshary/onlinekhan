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
  //#region ### data ###
  educationSubGroupStore = vxm.educationSubGroupStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.educationSubGroupStore.submitDelete(this);
  }
  //#endregion
}
</script>