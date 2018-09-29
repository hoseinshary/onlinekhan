<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size='lg'
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)"
                   @open="getAllDdls()">
    <my-select :model="$v.questionObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-4"
               filter />

    <my-select :model="$v.questionObj.EducationGroup_LessonId"
               :options="lessonFilteredDdl"
               class="col-md-4"
               filter
               @change="GetAllTreeStore($event);" />
    <my-uploader :model="$v.questionObj"
    classes="col-sm-4"
                 myRef="file1"
                 @add="fileAdd($event,'questionObj','File',1,true)"
                 @remove="fileRemove('questionObj','File',true)"
                 extensions='.doc,.docx'
                 label='فایل سوال' />

    <my-input :model="$v.questionObj.QuestionNumber"
              class="col-md-4" />

 <my-select :model="$v.questionObj.LookupId_QuestionType"
               :options="lookupQuestionType"
               class="col-md-4"
               filter />


    <my-input :model="$v.questionObj.QuestionPoint"
              class="col-md-4" />

 <my-select :model="$v.questionObj.LookupId_QuestionHardnessType"
               :options="lookupQuestionHardnessType"
               class="col-md-4"
               filter />

 <my-select :model="$v.questionObj.LookupId_RepeatnessType"
               :options="lookupQuestionRepeatnessType"
               class="col-md-4"
               filter />


    <my-field class="col-md-4"
              :model="$v.questionObj.UseEvaluation">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="بلی" />
      </template>
    </my-field>

    <my-field class="col-md-4"
              :model="$v.questionObj.IsStandard">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="بلی" />
      </template>
    </my-field>

 <my-select :model="$v.questionObj.LookupId_AuthorType"
               :options="lookupQuestionAuthorType"
               class="col-md-4"
               filter />


    <my-input :model="$v.questionObj.AuthorName"
              class="col-md-4" />

 <my-select :model="$v.questionObj.LookupId_AreaType"
               :options="lookupTopicAreaTypeDdl"
               class="col-md-4"
               filter />
  

    <my-input :model="$v.questionObj.ResponseSecond"
              class="col-md-4" />

    <my-input :model="$v.questionObj.Description"
              class="col-md-4" />

    <!-- <my-input :model="$v.questionObj.FileName"
              class="col-md-4" />

    <my-input :model="$v.questionObj.InsertDateTime"
              class="col-md-4" />

    <my-input :model="$v.questionObj.UserId"
              class="col-md-4" /> -->

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/questionViewModel';
import { mapState, mapActions } from 'vuex';
import util from 'utilities/util';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('questionStore', [
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions('educationGroupStore', { fillEduGrpDdl: 'fillDdlStore' }),
    ...mapActions('lessonStore', { fillLessonDdl: 'fillDdlStore' }),
    ...mapActions('lookupStore', [
      'fillTopicAreaTypeDdlStore',
      'getLookupQuestionType',
      'getLookupQuestionHardnessType',
      'getLookupQuestionRepeatnessType',
      'getLookupQuestionAuthorType'
    ]),
    ...mapActions('topicStore', ['GetAllTreeStore']),
    getAllDdls() {
      this.fillEduGrpDdl();
      this.fillLessonDdl();
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
    ...mapState('questionStore', {
      modelName: 'modelName',
      questionObj: 'questionObj',
      isOpenModalCreate: 'isOpenModalCreate'
    }),
    ...mapState('educationGroupStore', ['educationGroupDdl']),
    ...mapState('lessonStore', { lessonDdl: 'allObjDdl' }),
    ...mapState('lookupStore', [
      'lookupTopicAreaTypeDdl',
      'lookupQuestionType',
      'lookupQuestionHardnessType',
      'lookupQuestionRepeatnessType',
      'lookupQuestionAuthorType'
    ]),
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

