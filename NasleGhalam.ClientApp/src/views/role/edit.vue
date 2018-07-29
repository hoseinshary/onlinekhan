<template>
  <my-modal-edit :title="modelName"
                 size="md"
                 height="240px"
                 :openModal="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @toggle="toggleModalEditStore">

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

  </my-modal-edit>
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
    ...mapState('roleStore', {
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

