<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.educationYearObj.Name"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.educationYearObj.IsActiveYear">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model" />
      </template>
    </my-field>

  </my-modal-edit>
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
    ...mapState('educationYearStore', {
      modelName: 'modelName',
      educationYearObj: 'educationYearObj',
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

