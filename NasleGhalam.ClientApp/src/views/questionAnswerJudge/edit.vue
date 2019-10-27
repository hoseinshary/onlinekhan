<template>
  <section class="row gutter-sm">
    <base-select
      :model="$v.questionAnswerJudge.LessonName"
      :options="lessonNameDdl"
      class="col-md-6"
    />

    <base-select
      :model="$v.questionAnswerJudge.LookupId_ReasonProblem"
      :options="lookupStore.reasonProblemDdl"
      class="col-md-6"
      clearable
    />

    <q-field class="col-md-6">
      <div class="s-border q-pa-sm">
        <q-toggle
          v-model="$v.questionAnswerJudge.IsActiveQuestionAnswer.$model"
          :label="$v.questionAnswerJudge.IsActiveQuestionAnswer.$params.displayName.value"
          class="q-mx-md"
        />
      </div>
    </q-field>

    <base-input :model="$v.questionAnswerJudge.Description" class="col-12" />
    <div class="col-12">
      <base-btn-edit @click="questionAnswerJudgeStore.submitEdit()" />
    </div>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionAnswerJudgeValidations } from "src/validations/questionAnswerJudgeValidation";

@Component({
  validations: questionAnswerJudgeValidations
})
export default class QuestionAnswerJudgeEditVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionAnswerJudgeStore = vxm.questionAnswerJudgeStore;
  questionAnswerJudge = this.questionAnswerJudgeStore.questionAnswerJudge;
  lookupStore = vxm.lookupStore;
  lessonStore = vxm.lessonStore;
  //#endregion

  //#region ### computed ###
  get lessonNameDdl() {
    return this.lessonStore
      .relatedLessons(this.lessonIdProp)
      .map(x => ({ label: x.Name, value: x.Name }));
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerJudgeStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

