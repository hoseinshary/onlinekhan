<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.questionGroupObj.Title"
              class="col-md-6" />

    <my-input :model="$v.questionGroupObj.LessonId"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/questionGroup/questionGroupViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('questionGroupStore', [
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
    ...mapState('questionGroupStore', {
      modelName: 'modelName',
      questionGroupObj: 'questionGroupObj',
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

