<template>
  <section class="row gutter-sm shadow-1 q-pa-sm q-ma-sm">
    <base-input :model="$v.questionJudge.ResponseSecond" class="col-md-6" helper="ثانیه"/>
    <div class="row"></div>
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
    <base-field class="col-md-6" :model="$v.questionJudge.IsStandard">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-field class="col-md-6" :model="$v.questionJudge.IsLearning">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-field class="col-md-6" :model="$v.questionJudge.IsUpdate">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-field class="col-md-6" :model="$v.questionJudge.IsDelete">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-btn-create @click="questionJudgeStore.submitCreate()"/>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
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
}

// import viewModel from "viewModels/questionJudgeViewModel";
// import { mapState, mapActions } from "vuex";

// export default {
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("questionJudgeStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ]),
//     ...mapActions("lookupStore", [
//       "getLookupQuestionJudgeHardnessType",
//       "getLookupQuestionJudgeRepeatnessType"
//     ]),
//     fillAllDdls() {
//       this.getLookupQuestionJudgeHardnessType();
//       this.getLookupQuestionJudgeRepeatnessType();
//     }
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("questionJudgeStore", {
//       modelName: "modelName",
//       questionJudge: "questionJudge",
//       isOpenModalCreate: "isOpenModalCreate"
//     }),
//     ...mapState("lookupStore", [
//       "lookupQuestionJudgeHardnessType",
//       "lookupQuestionJudgeRepeatnessType"
//     ])
//   },
//   /**
//    * validations
//    */
//   validations: viewModel,
//   /**
//    * created
//    */
//   created() {
//     this.createVueStore(this);
//     this.fillAllDdls();
//   }
// };
</script>

