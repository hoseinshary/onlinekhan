<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.roleObj.Name"
              class="col-md-6" />

    <my-input :model="$v.roleObj.Level"
              class="col-md-6" />

    <my-field class="col-12"
              :model="$v.roleObj.UserType">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="0"
                 label="سازمانی" />
        <q-radio v-model="data.obj.$model"
                 :val="1"
                 label="دانش آموز" />
        <q-radio v-model="data.obj.$model"
                 :val="2"
                 label="معلم" />
      </template>
    </my-field>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/role/roleViewModel';
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
      roleObj: 'roleObj',
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

