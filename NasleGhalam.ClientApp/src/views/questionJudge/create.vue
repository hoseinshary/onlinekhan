<template>
  <section class="row gutter-sm">
    <base-input :model="$v.questionJudge.ResponseSecond" class="col-md-6" suffix="ثانیه" />
    <div class="col-md-6"></div>
    <base-select
      :model="$v.questionJudge.LookupId_RepeatnessType"
      :options="lookupStore.repeatnessTypeDdl"
      class="col-md-6"
      clearable
    />
    <base-select
      :model="$v.questionJudge.LookupId_QuestionHardnessType"
      :options="lookupStore.questionHardnessTypeDdl"
      class="col-md-6"
      clearable
    />

    <q-field class="col-12">
      <div class="s-border q-pa-sm">
        <q-toggle
          v-model="$v.questionJudge.IsStandard.$model"
          
          :label="$v.questionJudge.IsStandard.$params.displayName.value"
          class="q-mx-md"
        />
        <q-toggle
          v-model="$v.questionJudge.IsLearning.$model"
          hidden
          :label="$v.questionJudge.IsLearning.$params.displayName.value"
          class="q-mx-md hidden"
        />
        <div class="col-12">
          <q-toggle
            v-model="$v.questionJudge.IsUpdate.$model"
            :label="$v.questionJudge.IsUpdate.$params.displayName.value"
            class="q-mx-md"
          />
          <q-toggle
            v-model="$v.questionJudge.IsDelete.$model"
            :label="$v.questionJudge.IsDelete.$params.displayName.value"
            class="q-mx-md"
          />
          <q-toggle
            v-model="$v.questionJudge.IsActiveQuestion.$model"
            :label="$v.questionJudge.IsActiveQuestion.$params.displayName.value"
            class="q-mx-md"
          />
        </div>
        <q-toggle
          v-model="$v.questionJudge.IsActiveQuestionAnswer.$model"
          :label="$v.questionJudge.IsActiveQuestionAnswer.$params.displayName.value"
          class="q-mx-md hidden"
        />
      </div>
    </q-field>

    <base-select
      :model="$v.questionJudge.LookupId_WhereProblem"
      :options="lookupStore.whereProblemDdl"
      class="col-md-6"
      clearable
    />
    <base-select
      :model="$v.questionJudge.LookupId_ReasonProblem"
      :options="lookupStore.reasonProblemDdl"
      class="col-md-6"
      clearable
    />
    <base-input :model="$v.questionJudge.Description" class="col-12" />

    <base-select
      :model="$v.questionJudge.LookupId_QuestionRank"
      :options="lookupStore.questionRankDdl"
      class="col-md-6"
      clearable
      filter
    />

    <base-select
      :model="$v.questionJudge.EducationGroup"
      :options="educationGroupDdl"
      class="col-md-6"
    ></base-select>


    <div class="col-12">
      <base-btn-create @click="questionJudgeStore.submitCreate()" :label="`ایجاد و پاک سازی`" />
      <base-btn-create @click="questionJudgeStore.submitCreateWithoutReset()" :label="`ایجاد`" />
    </div>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionJudgeValidations } from "src/validations/questionJudgeValidation";

@Component({
  validations: questionJudgeValidations
})
export default class QuestionJudgeCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  questionJudgeStore = vxm.questionJudgeStore;
  questionJudge = this.questionJudgeStore.questionJudge;
  lookupStore = vxm.lookupStore;
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionJudgeStore.SET_CREATE_VUE(this);
  }
  //#endregion

  //#region ### computed ###
  get educationGroupDdl() {
    return [
      { value: "ریاضی", label: "ریاضی" },
      { value: "تجربی", label: "تجربی" },
      { value: "انسانی", label: "انسانی" }
    ];
  }

  //#endregion

  //#region ### watch ###
  @Watch("questionJudge.IsDelete")
  questionJudgeIsDeleteChanged(newVal) {
    if (newVal) {
      this.questionJudge.IsUpdate = false;
      this.questionJudge.IsActiveQuestion = false;
    }
  }
  @Watch("questionJudge.IsUpdate")
  questionJudgeIsUpdateChanged(newVal) {
    if (newVal) {
      this.questionJudge.IsDelete = false;
      this.questionJudge.IsActiveQuestion = false;
    }
  }
  @Watch("questionJudge.IsActiveQuestion")
  questionJudgeIsActiveQuestionChanged(newVal) {
    if (newVal) {
      this.questionJudge.IsUpdate = false;
      this.questionJudge.IsDelete = false;
    }
  }
  //#endregion
}
</script>

