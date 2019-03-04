<template>
  <section class="row gutter-sm shadow-1 q-pa-sm q-ma-sm">

    <my-input :model="$v.questionJudgeObj.ResponseSecond"
              class="col-md-6"
              helper="ثانیه" />

    <div class="row"></div>

    <my-select :model="$v.questionJudgeObj.LookupId_RepeatnessType"
               :options="lookupQuestionRepeatnessType"
               class="col-md-6"
               clearable />

    <my-select :model="$v.questionJudgeObj.LookupId_QuestionHardnessType"
               :options="lookupQuestionHardnessType"
               class="col-md-6"
               clearable />

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsStandard">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="بلی" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsLearning">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="بلی" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsUpdate">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="بلی" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsDelete">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="بلی" />
      </template>
    </my-field>
    <my-btn-create @click="submitCreateStore()" />
  </section>
</template>

<script>
import viewModel from "viewModels/questionJudgeViewModel";
import { mapState, mapActions } from "vuex";

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions("questionJudgeStore", [
      "toggleModalCreateStore",
      "createVueStore",
      "submitCreateStore",
      "resetCreateStore"
    ]),
    ...mapActions("lookupStore", [
      "getLookupQuestionHardnessType",
      "getLookupQuestionRepeatnessType"
    ]),
    fillAllDdls() {
      this.getLookupQuestionHardnessType();
      this.getLookupQuestionRepeatnessType();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionJudgeStore", {
      modelName: "modelName",
      questionJudgeObj: "questionJudgeObj",
      isOpenModalCreate: "isOpenModalCreate"
    }),
    ...mapState("lookupStore", [
      "lookupQuestionHardnessType",
      "lookupQuestionRepeatnessType"
    ])
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.createVueStore(this);
    this.fillAllDdls();
  }
};
</script>

