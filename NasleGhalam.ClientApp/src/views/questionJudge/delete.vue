<template>
  <q-btn
    @click="showPopoverDelete(recordIdProp)"
    outline
    round
    icon="delete"
    color="red"
    size="sm"
    class="shadow-1 bg-white"
  >
    <q-tooltip>حذف</q-tooltip>
    <q-popover class="q-pa-sm">
      <p class="text-bold">این رکورد حذف شود؟</p>
      <q-btn
        @click="questionJudgeStore.submitDelete()"
        outline
        color="red"
        class="shadow-1 bg-white q-mr-sm"
      >حذف رکورد</q-btn>
    </q-popover>
  </q-btn>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class QuestionJudgeDeleteVue extends Vue {
  //#region ### data ###
  questionJudgeStore = vxm.questionJudgeStore;
  //#endregion

  //#region ### props ###
  @Prop({ required: true }) recordIdProp;
  //#endregion

  //#region ### methods ###
  showPopoverDelete(id) {
    this.questionJudgeStore.questionJudge.Id = id;
  }
  //#endregion
}
</script> 