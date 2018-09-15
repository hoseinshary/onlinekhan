<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

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

  </my-modal-create>
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
    ...mapState('tagStore', {
      modelName: 'modelName',
      tagObj: 'tagObj',
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

