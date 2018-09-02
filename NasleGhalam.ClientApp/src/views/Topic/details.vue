<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalDetails"
                   @confirm="submitDetailsStore"
                   @reset="resetDetailsStore"
                   @close="toggleModalDetailsStore(false)">

    <my-input :model="$v.topicObj.Title"
              readonly
              disabled
              class="col-md-6" />

    <my-input :model="$v.topicObj.ExamStock"
              readonly
              disabled
              class="col-md-6" />

    <my-input :model="$v.topicObj.ExamStockSystem"
              readonly
              disabled
              class="col-md-6" />

    <my-input :model="$v.topicObj.Importance"
              readonly
              disabled
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.topicObj.IsExamSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 readonly
                 disabled
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 readonly
                 disabled
                 label="بلی" />
      </template>
    </my-field>

    <my-select :model="$v.topicObj.LookupId_HardnessType"
               :options="lookupTopicHardnessType"
               readonly
               disabled
               class="col-md-6"
               clearable />

    <my-select :model="$v.topicObj.LookupId_AreaType"
               :options="lookupTopicAreaType"
               readonly
               disabled
               class="col-md-6"
               clearable />

    <my-field class="col-md-6"
              :model="$v.topicObj.IsActive">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 readonly
                 disabled
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 readonly
                 disabled
                 label="بلی" />
      </template>
    </my-field>

    <!-- <my-input :model="$v.topicObj.ParentTopicId"
              class="col-md-6" />

    <my-input :model="$v.topicObj.EducationGroup_LessonId"
              class="col-md-6" /> -->

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
    }),
    ...mapState('lookupStore', [
      'lookupTopicHardnessType',
      'lookupTopicAreaType'
    ])
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

