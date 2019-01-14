<template>
  <my-modal-edit
    :title="modelName"
    :show="isOpenModalEdit"
    size='lg'
    @confirm="submitEditStore"
    @reset="resetEditStore"
    @close="toggleModalEditStore(false)"
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
          :ticked.sync="questionObj.TopicsId"
        />

      </section>
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-select
          filter
          chips
          multiple
          v-model="questionObj.TagsId"
          :options="tagDdl"
          float-label="تگ ها"
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
        @add="fileAdd($event,'questionObj','File',1,false)"
        @remove="fileRemove('questionObj','File',false)"
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
            :val="false"
            label="خیر"
          />
          <q-radio
            v-model="data.obj.$model"
            :val="true"
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
            :val="false"
            label="خیر"
          />
          <q-radio
            v-model="data.obj.$model"
            :val="true"
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
    <!--<my-input
      :model="$v.questionObj.Context"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.QuestionNumber"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.LookupId_QuestionType"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.QuestionPoint"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.LookupId_QuestionHardnessType"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.LookupId_RepeatnessType"
      class="col-md-6"
    />

    <my-field
      class="col-md-6"
      :model="$v.questionObj.UseEvaluation"
    >
      <template slot-scope="data">
        <q-radio
          v-model="data.obj.$model"
          val="false"
          label="false"
        />
        <q-radio
          v-model="data.obj.$model"
          val="true"
          label="true"
        />
      </template>
    </my-field>

    <my-field
      class="col-md-6"
      :model="$v.questionObj.IsStandard"
    >
      <template slot-scope="data">
        <q-radio
          v-model="data.obj.$model"
          val="false"
          label="false"
        />
        <q-radio
          v-model="data.obj.$model"
          val="true"
          label="true"
        />
      </template>
    </my-field>

    <my-input
      :model="$v.questionObj.LookupId_AuthorType"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.AuthorName"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.LookupId_AreaType"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.ResponseSecond"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.Description"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.FileName"
      class="col-md-6"
    />

    <my-input
      :model="$v.questionObj.InsertDateTime"
      class="col-md-6"
    />

     <my-input
      :model="$v.questionObj.UserId"
      class="col-md-6"
    /> -->

  </my-modal-edit>
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
      answersDdl: [
        { value: 1, label: "1" },
        { value: 2, label: "2" },
        { value: 3, label: "3" },
        { value: 4, label: "4" }
      ]
    };
  },
  methods: {
    ...mapActions("questionStore", [
      "toggleModalEditStore",
      "editVueStore",
      "submitEditStore",
      "resetEditStore",
      "fillTagsDdlStore"
    ]),
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
      // this.fillLessonDdl();
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
      isOpenModalEdit: "isOpenModalEdit",
      tagDdl: "tagDdl"
    }),
        ...mapState("lookupStore", [
      "lookupTopicAreaTypeDdl",
      "lookupQuestionType",
      "lookupQuestionHardnessType",
      "lookupQuestionRepeatnessType",
      "lookupQuestionAuthorType"
    ]),
    ...mapState("topicStore", {
      topicTreeData: "topicTreeData"
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  }
};
</script>

