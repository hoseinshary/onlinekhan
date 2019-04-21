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
    <base-btn-edit @click="questionJudgeStore.submitEdit()"/>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionJudgeValidations } from "src/validations/questionJudgeValidation";

@Component({
  validations: questionJudgeValidations
})
export default class QuestionJudgeEditVue extends Vue {
  $v: any;

  //#region ### data ###
  questionJudgeStore = vxm.questionJudgeStore;
  questionJudge = this.questionJudgeStore.questionJudge;
  lookupStore = vxm.lookupStore;
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionJudgeStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

