<template>
  <base-modal-delete
    :title="questionJudgeStore.modelName"
    recordName="رکورد"
    :show="questionJudgeStore.openModal.delete"
    @confirm="confirm"
    @close="questionJudgeStore.openModal.delete=false"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class QuestionJudgeDeleteVue extends Vue {
  //#region ### data ###
  questionJudgeStore = vxm.questionJudgeStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.questionJudgeStore.submitDelete(this);
  }
  //#endregion
}
</script> 