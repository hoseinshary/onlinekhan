<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

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

  </my-modal-create>
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
      roleObj: 'roleObj',
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

