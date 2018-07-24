<template>
  <my-modal-edit :title="modelName"
                 size="sm"
                 height="240px"
                 :openModal="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @toggle="toggleModalEditStore">

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

