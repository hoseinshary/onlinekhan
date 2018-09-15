<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.tagObj.Name"
              class="col-md-6" />

    <my-field class="col-md-6"
              :model="$v.tagObj.IsSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="بلی" />
      </template>
    </my-field>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/tagViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('tagStore', [
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
    ...mapState('tagStore', {
      modelName: 'modelName',
      tagObj: 'tagObj',
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

