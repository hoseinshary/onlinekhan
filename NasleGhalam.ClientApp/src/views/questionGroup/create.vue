<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <my-input :model="$v.questionGroupObj.Title"
              class="col-md-6" />

    <my-input :model="$v.questionGroupObj.LessonId"
              class="col-md-6" />

  </my-modal-create>
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
    ...mapState('questionGroupStore', {
      modelName: 'modelName',
      questionGroupObj: 'questionGroupObj',
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

