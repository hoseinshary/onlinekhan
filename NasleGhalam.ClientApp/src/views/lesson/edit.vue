<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.instanceObj.Name"
              class="col-md-12" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/lessonViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('lessonStore', [
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
    ...mapState('lessonStore', {
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

