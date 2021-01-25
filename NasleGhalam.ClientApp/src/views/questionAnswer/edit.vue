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
    <base-select :model="$v.questionAnswer.WriterId" :options="writerStore.ddl" class="col-md-6"/>
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
       <base-select :model="$v.questionAnswer.QuestionAnswerType" :options="questionAnswerTypeDdl" class="col-md-6" />
       <div class="col-12">
          <q-toggle
            v-model="$v.questionAnswer.IsUpdate.$model"
            :label="$v.questionAnswer.IsUpdate.$params.displayName.value"
            class="q-mx-md"
          />
          <q-toggle
            v-model="$v.questionAnswer.IsDelete.$model"
            :label="$v.questionAnswer.IsDelete.$params.displayName.value"
            class="q-mx-md"
          />
          <q-toggle
            v-model="$v.questionAnswer.IsActive.$model"
            :label="$v.questionAnswer.IsActive.$params.displayName.value"
            class="q-mx-md"
          />
        </div>
     <base-select
      :model="$v.questionAnswer.LessonName"
      :options="lessonNameDdl"
      class="col-md-6"
    />
    <base-input :model="$v.questionAnswer.Description" class="col-12"/>
    <base-btn-edit @click="questionAnswerStore.submitEdit()"/>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionAnswerValidations } from "src/validations/questionAnswerValidation";
import util from "src/utilities";
import { QuestionAnswerType } from "src/utilities/enumeration";

@Component({
  validations: questionAnswerValidations
})
export default class QuestionAnswerEditVue extends Vue {
  $v: any;

  @Prop({ type: Number, required: true }) lessonIdProp;
  
  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionAnswer = this.questionAnswerStore.questionAnswer;
  lookupStore = vxm.lookupStore;
  writerStore = vxm.writerStore;
  lessonStore = vxm.lessonStore;
  //#endregion

 get lessonNameDdl() {
    return this.lessonStore.relatedLessons(this.lessonIdProp)
    .map(x=>({label:x.Name, value:x.Name}));
  }

    get questionAnswerTypeDdl() {
    return util.enumToDdl(QuestionAnswerType);
  }

  //#region ### watch ###
  @Watch("questionAnswer.IsDelete")
  questionJudgeIsDeleteChanged(newVal) {
    if (newVal) {
      this.questionAnswer.IsUpdate = false;
      this.questionAnswer.IsActive = false;
    }
  }
  @Watch("questionAnswer.IsUpdate")
  questionJudgeIsUpdateChanged(newVal) {
    if (newVal) {
      this.questionAnswer.IsDelete = false;
      this.questionAnswer.IsActive = false;
    }
  }
  @Watch("questionAnswer.IsActive")
  questionJudgeIsActiveQuestionChanged(newVal) {
    if (newVal) {
      this.questionAnswer.IsUpdate = false;
      this.questionAnswer.IsDelete = false;
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

