<template>
  <my-modal-create
    :title="modelName"
    :show="isOpenModalCreate"
    size='lg'
    @confirm="submitCreateStore"
    @reset="resetCreateStore"
    @close="toggleModalCreateStore(false)"
    @open="getAllDdls()"
  >

    <div class="col-sm-4">

      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-tree
          :nodes="topicTreeData"
          tick-strategy="leaf"
          class="q-pt-lg"
          color="primary"
          accordion
          node-key="Id"
          ref="topicTree"
          :ticked.sync="questionObj.TopicIds"
        />

      </section>
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-select
          filter
          chips
          multiple
          v-model="questionObj.TagsId"
          :options="tagDdl"
        />

      </section>
      <q-slide-transition>
        <section
          v-if="questionObj.LookupId_QuestionType==6"
          class="q-ma-sm q-pa-sm shadow-1"
        >گزینه صحیح
          <my-select
            :model="$v.questionObj.AnswerNumber"
            :options="answersDdl"
            class="col-md-4"
            filter
          />

        </section>
      </q-slide-transition>
    </div>
    <div class="col-sm-8 row gutter-md">

      <!-- <my-select :model="$v.questionObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-4"
               filter /> -->

      <!-- <my-select :model="$v.questionObj.EducationGroup_LessonId"
               :options="lessonFilteredDdl"
               class="col-md-4"
               filter
               @change="GetAllTreeStore($event);" /> -->
      <my-uploader
        :model="$v.questionObj"
        classes="col-sm-4"
        myRef="file1"
        @add="fileAdd($event,'questionObj','File',1,true)"
        @remove="fileRemove('questionObj','File',true)"
        extensions='.doc,.docx'
        label='فایل سوال'
      />

      <my-input
        :model="$v.questionObj.QuestionNumber"
        class="col-md-4"
      />

      <my-select
        :model="$v.questionObj.LookupId_QuestionType"
        :options="lookupQuestionType"
        class="col-md-4"
        filter
      />

      <my-input
        :model="$v.questionObj.QuestionPoint"
        class="col-md-4"
      />

      <my-select
        :model="$v.questionObj.LookupId_QuestionHardnessType"
        :options="lookupQuestionHardnessType"
        class="col-md-4"
        filter
      />

      <my-select
        :model="$v.questionObj.LookupId_RepeatnessType"
        :options="lookupQuestionRepeatnessType"
        class="col-md-4"
        filter
      />

      <my-field
        class="col-md-4"
        :model="$v.questionObj.UseEvaluation"
      >
        <template slot-scope="data">
          <q-radio
            v-model="data.obj.$model"
            val="false"
            label="خیر"
          />
          <q-radio
            v-model="data.obj.$model"
            val="true"
            label="بلی"
          />
        </template>
      </my-field>

      <my-field
        class="col-md-4"
        :model="$v.questionObj.IsStandard"
      >
        <template slot-scope="data">
          <q-radio
            v-model="data.obj.$model"
            val="false"
            label="خیر"
          />
          <q-radio
            v-model="data.obj.$model"
            val="true"
            label="بلی"
          />
        </template>
      </my-field>

      <my-select
        :model="$v.questionObj.LookupId_AuthorType"
        :options="lookupQuestionAuthorType"
        class="col-md-4"
        filter
      />

      <my-input
        :model="$v.questionObj.AuthorName"
        class="col-md-4"
      />

      <my-select
        :model="$v.questionObj.LookupId_AreaType"
        :options="lookupTopicAreaTypeDdl"
        class="col-md-4"
        filter
      />

      <my-input
        :model="$v.questionObj.ResponseSecond"
        class="col-md-4"
      />

      <my-input
        :model="$v.questionObj.Description"
        class="col-md-4"
      />

      <!-- <my-input :model="$v.questionObj.FileName"
              class="col-md-4" />

    <my-input :model="$v.questionObj.InsertDateTime"
              class="col-md-4" />

    <my-input :model="$v.questionObj.UserId"
              class="col-md-4" /> -->
    </div>

  </my-modal-create>
</template>

<script>
import viewModel from "viewModels/questionViewModel";
import { mapState, mapActions } from "vuex";
import util from "utilities/util";

export default {
  /**
   * methods
   */
  data() {
    return {
      answersDdl:[{value:1,label:"1"},{value:2,label:"2"}
      ,{value:3,label:"3"},{value:4,label:"4"}]
    };
  },
  methods: {
    ...mapActions("questionStore", [
      "toggleModalCreateStore",
      "createVueStore",
      "submitCreateStore",
      "resetCreateStore",
      "fillTagsDdlStore"
    ]),
    ...mapActions("educationGroupStore", { fillEduGrpDdl: "fillDdlStore" }),
    ...mapActions("lessonStore", { fillLessonDdl: "fillDdlStore" }),
    ...mapActions("lookupStore", [
      "fillTopicAreaTypeDdlStore",
      "getLookupQuestionType",
      "getLookupQuestionHardnessType",
      "getLookupQuestionRepeatnessType",
      "getLookupQuestionAuthorType"
    ]),
    ...mapActions("topicStore", ["GetAllTreeStore"]),
    getAllDdls() {
      // this.fillEduGrpDdl();
      this.fillLessonDdl();
      this.fillTagsDdlStore();
      this.getLookupQuestionType();
      this.getLookupQuestionHardnessType();
      this.getLookupQuestionRepeatnessType();
      this.getLookupQuestionAuthorType();
      this.fillTopicAreaTypeDdlStore();
    },
    fileAdd(chObj, model, prop, number, isRequired) {
      util.fileAdd(model, prop, number, isRequired, chObj, this);
    },
    fileRemove(model, prop, isRequired) {
      util.fileRemove(model, prop, isRequired, this);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionStore", {
      modelName: "modelName",
      questionObj: "questionObj",
      isOpenModalCreate: "isOpenModalCreate",
      tagDdl: "tagDdl"
    }),
    ...mapState("educationGroupStore", ["educationGroupDdl"]),
    ...mapState("lessonStore", { lessonDdl: "allObjDdl" }),
    ...mapState("lookupStore", [
      "lookupTopicAreaTypeDdl",
      "lookupQuestionType",
      "lookupQuestionHardnessType",
      "lookupQuestionRepeatnessType",
      "lookupQuestionAuthorType"
    ]),
    ...mapState("topicStore", {
      topicTreeData: "topicTreeData"
    }),
    lessonFilteredDdl: function() {
      return this.questionObj.EducationGroupId == 0
        ? []
        : this.lessonDdl.filter(
            x => x.educationGroupId == this.questionObj.EducationGroupId
          );
    }
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
  }
};
</script>

