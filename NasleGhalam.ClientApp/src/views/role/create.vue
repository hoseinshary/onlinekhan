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

    <my-field class="col-md-6"
              :model="$v.instance.Priority">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-select :model="$v.instanceObj.GradeId"
               :options=""
               class="col-md-6"
               clearable />

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/roleViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('roleStore', [
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
    ...mapState('roleStore', {
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

