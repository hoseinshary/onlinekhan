<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <my-input :model="$v.topicObj.Title" class="col-md-6" />
          
          <my-input :model="$v.topicObj.ExamStock" class="col-md-6" />
          
          <my-input :model="$v.topicObj.ExamStockSystem" class="col-md-6" />
          
          <my-input :model="$v.topicObj.Importance" class="col-md-6" />
          
          <my-field class="col-md-6" :model="$v.topicObj.IsExamSource">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-input :model="$v.topicObj.LookupId_HardnessType" class="col-md-6" />
          
          <my-input :model="$v.topicObj.LookupId_AreaType" class="col-md-6" />
          
          <my-field class="col-md-6" :model="$v.topicObj.IsActive">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-input :model="$v.topicObj.ParentTopicId" class="col-md-6" />
          
          <my-input :model="$v.topicObj.EducationGroup_LessonId" class="col-md-6" />
          
          

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/topicViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('topicStore', [
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicObj: 'topicObj',
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

