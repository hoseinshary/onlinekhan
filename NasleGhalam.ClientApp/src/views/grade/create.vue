<template>
  <my-modal-create :title="modelName"
                   size="md"
                   height="240px"
                   :openModal="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @toggle="toggleModalCreateStore">

    <my-input :model="$v.instanceObj.Name"
              class="col-md-6" />
    <my-input :model="$v.instanceObj.Priority"
              class="col-md-6" />

  </my-modal-create>
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
    ...mapState('gradeStore', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
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

