<template>
  <section class="row gutter-sm shadow-1 q-pa-sm q-ma-sm">
    <q-field class="col-md-6">
      <q-uploader
        url
        float-label="فایل جواب"
        name="word"
        hide-upload-button
        ref="wordFile"
        extensions=".doc,.docx"
      />
    </q-field>
    <base-input :model="$v.questionAnswer.Title" class="col-md-6"/>
    <base-input :model="$v.questionAnswer.Author" class="col-md-6"/>
    <base-select
      :model="$v.questionAnswer.LookupId_AnswerType"
      :options="lookupStore.answerTypeDdl"
      class="col-md-6"
    />
    <base-field class="col-md-6" :model="$v.questionAnswer.IsMaster">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-input :model="$v.questionAnswer.Description" class="col-12"/>
    <base-btn-create @click="questionAnswerStore.submitCreate()"/>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionAnswerValidations } from "src/validations/questionAnswerValidation";

@Component({
  validations: questionAnswerValidations
})
export default class QuestionAnswerCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionAnswer = this.questionAnswerStore.questionAnswer;
  lookupStore = vxm.lookupStore;
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

