<template>
  <my-modal-edit :title="modelName"
                 size="md"
                 height="240px"
                 :openModal="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @toggle="toggleModalEditStore">

    <!--<my-input :model="$v.instanceObj.Name"
              class="col-md-6"/>-->

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/cityViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('cityStore', [
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
    ...mapState('cityStore', {
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

