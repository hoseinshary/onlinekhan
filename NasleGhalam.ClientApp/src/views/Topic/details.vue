<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalDetails"
                   @confirm="submitDetailsStore"
                   @reset="resetDetailsStore"
                   @close="toggleModalDetailsStore(false)">

    <my-input :model="$v.topicObj.Title"
              readonly
              class="col-md-6" />

    <my-input :model="$v.topicObj.ExamStock"
              readonly
              class="col-md-6" />

    <my-input :model="$v.topicObj.ExamStockSystem"
              readonly
              class="col-md-6" />

    <my-input :model="$v.topicObj.Importance"
              readonly
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.topicObj.IsExamSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 readonly
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 readonly
                 label="true" />
      </template>
    </my-field>

    <my-input :model="$v.topicObj.LookupId_HardnessType"
              class="col-md-6" />

    <my-input :model="$v.topicObj.LookupId_AreaType"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.topicObj.IsActive">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-input :model="$v.topicObj.ParentTopicId"
              class="col-md-6" />

    <my-input :model="$v.topicObj.EducationGroup_LessonId"
              class="col-md-6" />

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
      'toggleModalDetailsStore',
      'createVueStore',
      'submitDetailsStore',
      'resetDetailsStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicObj: 'topicObj',
      isOpenModalDetails: 'isOpenModalDetails'
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

