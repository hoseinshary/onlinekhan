<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.questionObj.Context" class="col-md-6" />
          
          <my-input :model="$v.questionObj.QuestionNumber" class="col-md-6" />
          
          <my-input :model="$v.questionObj.LookupId_QuestionType" class="col-md-6" />
          
          <my-input :model="$v.questionObj.QuestionPoint" class="col-md-6" />
          
          <my-input :model="$v.questionObj.LookupId_QuestionHardnessType" class="col-md-6" />
          
          <my-input :model="$v.questionObj.LookupId_RepeatnessType" class="col-md-6" />
          
          <my-field class="col-md-6" :model="$v.questionObj.UseEvaluation">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-field class="col-md-6" :model="$v.questionObj.IsStandard">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-input :model="$v.questionObj.LookupId_AuthorType" class="col-md-6" />
          
          <my-input :model="$v.questionObj.AuthorName" class="col-md-6" />
          
          <my-input :model="$v.questionObj.LookupId_AreaType" class="col-md-6" />
          
          <my-input :model="$v.questionObj.ResponseSecond" class="col-md-6" />
          
          <my-input :model="$v.questionObj.Description" class="col-md-6" />
          
          <my-input :model="$v.questionObj.FileName" class="col-md-6" />
          
          <my-input :model="$v.questionObj.InsertDateTime" class="col-md-6" />
          
          <my-input :model="$v.questionObj.UserId" class="col-md-6" />
          
          

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/questionViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('questionStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('questionStore', {
      modelName: 'modelName',
      questionObj: 'questionObj',
      isOpenModalEdit: 'isOpenModalEdit'
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

