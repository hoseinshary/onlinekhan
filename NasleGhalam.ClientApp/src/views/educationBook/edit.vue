<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.educationBookObj.Name"
              class="col-md-6" />

    <my-input :model="$v.educationBookObj.PublishYear"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.educationBookObj.IsExamSource">
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
              :model="$v.educationBookObj.IsActive">
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
              :model="$v.educationBookObj.IsChanged">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-input :model="$v.educationBookObj.GradeLevelId"
              class="col-md-6" />

    <!-- <my-select :model="$v.educationBookObj.EducationGroup_LessonId"
               :options=""
               class="col-md-6"
               clearable /> -->

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/educationBook/educationBookCrudViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationBookStore', [
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
    ...mapState('educationBookStore', {
      modelName: 'modelName',
      educationBookObj: 'educationBookObj',
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

