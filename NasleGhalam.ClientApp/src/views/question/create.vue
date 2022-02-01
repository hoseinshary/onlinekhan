<template>
  <base-modal-create
    :title="questionStore.modelName"
    :show="questionStore.openModal.create"
    size="xl"
    @confirm="questionStore.submitCreate"
    @reset="questionStore.resetCreate"
    @close="questionStore.OPEN_MODAL_CREATE(false)"
    @open="open"
  >
    <div class="col-sm-4">
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable />
        <q-tree
          :nodes="topicTreeDataProp"
          :filter="topicFilter"
          :ticked.sync="question.TopicIds"
          tick-strategy="leaf"
          color="primary"
          accordion
          ref="topicTree"
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

      <section v-if="question.LookupId_QuestionType==6" class="q-ma-sm q-pa-sm shadow-1">
        گزینه صحیح
        <base-select
          :model="$v.question.AnswerNumber"
          :options="answersDdl"
          class="col-md-4"
          filter
        />
      </section>

      <base-select :model="$v.question.TopicAnswer" :options="topicAnswerDdl" class="col-md-4"></base-select>
    </div>

    <div class="col-sm-8 row gutter-md">
      <div class="col-12">
        <p v-for="(elem, index) in concatTopicArray" :key="index">{{elem}}</p>
      </div>

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
      <base-input :model="$v.question.QuestionNumber" class="col-md-4" />
      <base-select
        :model="$v.question.LookupId_QuestionType"
        :options="lookupStore.questionTypeDdl"
        class="col-md-4"
        filter
      />
      <base-input :model="$v.question.QuestionPoint" class="col-md-4" />
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
          <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
          <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
        </template>
      </base-field>
      <base-field class="col-md-4" :model="$v.question.IsStandard">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
          <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
        </template>
      </base-field>
      <base-select
        :model="$v.question.LookupId_AuthorType"
        :options="lookupStore.authorTypeDdl"
        class="col-md-4"
        filter
      />
      <base-select
        :model="$v.question.WriterId"
        :options="writerStore.ddl"
        class="col-md-4"
        filter
      />
      <base-select
        :model="$v.question.SupervisorUserId"
        :options="userStore.ddl"
        class="col-md-4"
        filter
      />

      <base-select
        :model="$v.question.LookupId_AreaType"
        :options="lookupStore.areaTypeDdl"
        class="col-md-4"
        filter
      />
      <base-select
        :model="$v.question.LookupId_QuestionRank"
        :options="lookupStore.questionRankDdl"
        class="col-md-4"
        filter
      />
      <base-input :model="$v.question.ResponseSecond" class="col-md-4" />
      <base-field class="col-md-4" :model="$v.question.IsHybrid">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
          <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
        </template>
      </base-field>
      <base-input :model="$v.question.Description" class="col-12" />
    </div>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionValidations } from "src/validations/questionValidation";
import util from "src/utilities";

@Component({
  validations: questionValidations
})
export default class QuestionCreateVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  @Prop({ type: Array, required: true }) topicTickedIdsProp;
  //#endregion

  //#region ### data ###
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  tagStore = vxm.tagStore;
  topicStore = vxm.topicStore;
  question = vxm.questionStore.question;
  writerStore = vxm.writerStore;
  userStore = vxm.userStore;
  topicFilter = "";
  concatTopicArray: Array<string> = [];
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

  get topicAnswerDdl() {
    return this.concatTopicArray.map(x => ({ value: x, label: x }));
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.question.TopicIds = this.topicTickedIdsProp;
    this.tagStore.fillList();
    this.lookupStore.fillQuestionType();
    this.lookupStore.fillQuestionHardnessType();
    this.lookupStore.fillRepeatnessType();
    this.lookupStore.fillAuthorType();
    this.lookupStore.fillAreaType();
    this.lookupStore.fillQuestionRank();
    this.writerStore.fillList();
    this.userStore.fillListSupervisor();
  }
  //#endregion

  //#region ### watch ###
  @Watch("question.TopicIds")
  questionTopicIdsChanged(newVal) {
    var getNodeByKey = this.$refs["topicTree"]["getNodeByKey"];
    util.clearArray(this.concatTopicArray);
    var strArr: Array<string> = [];
    newVal.forEach(x => {
      strArr = [];
      var node = getNodeByKey(x);
      while (node) {
        strArr.unshift(node.label);
        node = node.parent;
      }
      this.concatTopicArray.push(strArr.join(" => "));
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

