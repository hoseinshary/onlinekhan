<template>
  <base-modal-delete
    :title="questionGroupStore.modelName"
    :recordName="questionGroupStore.recordName"
    :show="questionGroupStore.openModal.delete"
    @confirm="confirm"
    @close="questionGroupStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class QuestionGroupDeleteVue extends Vue {
  //#region ### data ###
  questionGroupStore = vxm.questionGroupStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.questionGroupStore.submitDelete(this);
  }
  //#endregion
}
</script>