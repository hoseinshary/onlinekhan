<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <my-input :model="$v.educationYearObj.Name"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.educationYearObj.IsActiveYear">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model" />
      </template>
    </my-field>

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/educationYearViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationYearStore', [
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
    ...mapState('educationYearStore', {
      modelName: 'modelName',
      educationYearObj: 'educationYearObj',
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

