<template>
  <base-modal-edit
    :title="questionStore.modelName"
    :show="questionStore.openModal.edit"
    size="lg"
    @confirm="questionStore.submitEdit"
    @reset="questionStore.resetEdit"
    @close="questionStore.OPEN_MODAL_EDIT(false)"
    @open="open"
  >
    <q-card inline class="col-12" v-if="question.FileName">
      <q-card-media>
        <img :src="question.QuestionPicturePath" class="img-original-width">
      </q-card-media>
    </q-card>

    <div class="col-sm-4">
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable/>
        <q-tree
          :nodes="topicTreeDataProp"
          :filter="topicFilter"
          :ticked.sync="question.TopicIds"
          tick-strategy="leaf"
          color="primary"
          accordion
          node-key="Id"
        />
      </section>

      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-select
          filter
          chips
          multiple
          v-model="question.TagIds"
          :options="tagStore.ddl"
          float-label="تگ ها"
        />
      </section>

      <q-slide-transition>
        <section v-if="question.LookupId_QuestionType==6" class="q-ma-sm q-pa-sm shadow-1">
          گزینه صحیح
          <base-select
            :model="$v.question.AnswerNumber"
            :options="answersDdl"
            class="col-md-4"
            filter
          />
        </section>
      </q-slide-transition>
    </div>

    <div class="col-sm-8 row gutter-md">
      <q-field class="col-sm-4">
        <q-uploader
          url
          float-label="فایل سوال"
          name="word"
          hide-upload-button
          ref="wordFile"
          extensions=".doc,.docx"
        />
      </q-field>
      <base-input :model="$v.question.QuestionNumber" class="col-md-4"/>
      <base-select
        :model="$v.question.LookupId_QuestionType"
        :options="lookupStore.questionTypeDdl"
        class="col-md-4"
        filter
      />
      <base-input :model="$v.question.QuestionPoint" class="col-md-4"/>
      <base-select
        :model="$v.question.LookupId_QuestionHardnessType"
        :options="lookupStore.questionHardnessTypeDdl"
        class="col-md-4"
        filter
      />
      <base-select
        :model="$v.question.LookupId_RepeatnessType"
        :options="lookupStore.repeatnessTypeDdl"
        class="col-md-4"
        filter
      />
      <base-field class="col-md-4" :model="$v.question.UseEvaluation">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
          <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
        </template>
      </base-field>
      <base-field class="col-md-4" :model="$v.question.IsStandard">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
          <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
        </template>
      </base-field>
      <base-select
        :model="$v.question.LookupId_AuthorType"
        :options="lookupStore.authorTypeDdl"
        class="col-md-4"
        filter
      />
      <base-input :model="$v.question.AuthorName" class="col-md-4"/>
      <base-select
        :model="$v.question.LookupId_AreaType"
        :options="lookupStore.areaTypeDdl"
        class="col-md-4"
        filter
      />
      <base-input :model="$v.question.ResponseSecond" class="col-md-4"/>
      <base-input :model="$v.question.Description" class="col-12"/>
    </div>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionValidations } from "src/validations/questionValidation";

@Component({
  validations: questionValidations
})
export default class QuestionEditVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  //#endregion

  //#region ### data ###
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  tagStore = vxm.tagStore;
  topicStore = vxm.topicStore;
  question = vxm.questionStore.question;
  topicFilter = "";
  //#endregion

  //#region ### computed ###
  get answersDdl() {
    return [
      { value: 1, label: "1" },
      { value: 2, label: "2" },
      { value: 3, label: "3" },
      { value: 4, label: "4" }
    ];
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.tagStore.fillList();
    this.lookupStore.fillQuestionType();
    this.lookupStore.fillQuestionHardnessType();
    this.lookupStore.fillRepeatnessType();
    this.lookupStore.fillAuthorType();
    this.lookupStore.fillAreaType();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

