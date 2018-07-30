<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <!--<my-input :model="$v.instanceObj.Name"
              class="col-md-6" />-->

  </my-modal-create>
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
    ...mapState('cityStore', {
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

