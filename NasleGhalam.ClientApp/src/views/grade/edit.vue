<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.instanceObj.Name"
              class="col-md-6" />

    <my-input :model="$v.instanceObj.Priority"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/gradeViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('gradeStore', [
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
    ...mapState('gradeStore', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
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

