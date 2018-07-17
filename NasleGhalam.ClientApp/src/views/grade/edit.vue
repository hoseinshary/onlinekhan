<template>
  <my-modal-edit :title="modelName"
                 size="sm"
                 height="55%"
                 :openModal="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @toggle="toggleModalEditStore">

    <my-input :model="$v.instanceObj.Name"
              class="col-12">
    </my-input>

    <my-input :model="$v.instanceObj.Priority"
              class="col-12">
    </my-input>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/gradeViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * data
   */
  data() {
    return {};
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('grade', [
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
    ...mapState('grade', {
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

