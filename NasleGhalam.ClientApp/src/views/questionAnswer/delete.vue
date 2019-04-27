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
        @click="questionAnswerStore.submitDelete()"
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
export default class QuestionAnswerDeleteVue extends Vue {
  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  //#endregion

  //#region ### props ###
  @Prop({ required: true }) recordIdProp;
  //#endregion

  //#region ### methods ###
  showPopoverDelete(id) {
    this.questionAnswerStore.questionAnswer.Id = id;
  }
  //#endregion
}
</script> 