<template>
  <base-modal-delete
    :title="educationYearStore.modelName"
    :recordName="educationYearStore.recordName"
    :show="educationYearStore.openModal.delete"
    @confirm="confirm"
    @close="educationYearStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class EducationYearDeleteVue extends Vue {
  //### data ###
  educationYearStore = vxm.educationYearStore;
  //--------------------------------------------------

  //### methods ###
  confirm() {
    this.educationYearStore.submitDelete(this);
  }
  //--------------------------------------------------
}
</script>