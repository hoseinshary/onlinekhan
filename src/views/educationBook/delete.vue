<template>
  <base-modal-delete
    :title="educationBookStore.modelName"
    :recordName="educationBookStore.recordName"
    :show="educationBookStore.openModal.delete"
    @confirm="confirm"
    @close="educationBookStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class EducationBookDeleteVue extends Vue {
  //#region ### data ###
  educationBookStore = vxm.educationBookStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.educationBookStore.submitDelete(this);
  }
  //#endregion
}
</script>