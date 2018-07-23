<template>
  <my-modal-edit :title="modelName"
                 size="md"
                 height="320px"
                 :openModal="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @toggle="toggleModalEditStore">

    <my-select :model="$v.instanceObj.GradeId"
               :options="gradeDdl"
               class="col-md-6"
               clearable />

    <my-input :model="$v.instanceObj.Name"
              class="col-md-6">
    </my-input>

    <my-input :model="$v.instanceObj.Priority"
              class="col-md-6">
    </my-input>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/gradeLevelViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('gradeLevelStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions('gradeStore', {
      fillGradeDdlStore: 'fillDdlStore'
    })
  },
  /**
   * computed
   */
  computed: {
    ...mapState('gradeLevelStore', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState('gradeStore', {
      gradeDdl: 'allObjDdl'
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
    this.fillGradeDdlStore();
  }
};
</script>

