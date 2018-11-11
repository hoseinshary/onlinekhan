<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.topicObj.Title"
              class="col-md-6" />

    <my-input :model="$v.topicObj.ExamStock"
              class="col-md-6" />

    <my-input :model="$v.topicObj.Importance"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.topicObj.IsExamSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <!-- <my-select :model="$v.topicObj.LookupId_HardnessType"
               :options=""
               class="col-md-6"
               clearable />

    <my-select :model="$v.topicObj.LookupId_AreaType"
               :options=""
               class="col-md-6"
               clearable /> -->

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

    <!-- <my-select :model="$v.topicObj.LessonId"
               :options=""
               class="col-md-6"
               clearable /> -->

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/topic/topicViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('topicStore', [
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
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicObj: 'topicObj',
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

