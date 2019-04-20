<template>
  <base-modal-delete
    :title="questionStore.modelName"
    :recordName="questionStore.recordName"
    :show="questionStore.openModal.delete"
    @confirm="confirm"
    @close="questionStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class QuestionDeleteVue extends Vue {
  //#region ### data ###
  questionStore = vxm.questionStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.questionStore.submitDelete(this);
  }
  //#endregion
}
</script>