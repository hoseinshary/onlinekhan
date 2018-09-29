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
               class="col-md-6"
               filter />

    <my-select :model="$v.questionObj.EducationGroup_LessonId"
               :options="lessonFilteredDdl"
               class="col-md-6"
               filter
               @change="GetAllTreeStore($event);" />
    <my-uploader :model="$v.questionObj"
                 myRef="file1"
                 @add="fileAdd($event,'questionObj','File',1,true)"
                 @remove="fileRemove('questionObj','File',true)"
                 extensions='.doc,.docx'
                 label='فایل سوال' />

    <my-input :model="$v.questionObj.QuestionNumber"
              class="col-md-6" />

    <my-input :model="$v.questionObj.LookupId_QuestionType"
              class="col-md-6" />

    <my-input :model="$v.questionObj.QuestionPoint"
              class="col-md-6" />

    <my-input :model="$v.questionObj.LookupId_QuestionHardnessType"
              class="col-md-6" />

    <my-input :model="$v.questionObj.LookupId_RepeatnessType"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.questionObj.UseEvaluation">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionObj.IsStandard">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-input :model="$v.questionObj.LookupId_AuthorType"
              class="col-md-6" />

    <my-input :model="$v.questionObj.AuthorName"
              class="col-md-6" />

    <my-input :model="$v.questionObj.LookupId_AreaType"
              class="col-md-6" />

    <my-input :model="$v.questionObj.ResponseSecond"
              class="col-md-6" />

    <my-input :model="$v.questionObj.Description"
              class="col-md-6" />

    <my-input :model="$v.questionObj.FileName"
              class="col-md-6" />

    <my-input :model="$v.questionObj.InsertDateTime"
              class="col-md-6" />

    <my-input :model="$v.questionObj.UserId"
              class="col-md-6" />

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
      'getLookupTopicAreaType',
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
      this.getLookupTopicAreaType();
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
      'lookupTopicAreaType',
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

