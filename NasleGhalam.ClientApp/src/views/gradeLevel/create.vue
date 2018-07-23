<template>
  <my-modal-create :title="modelName"
                   size="md"
                   height="320px"
                   :openModal="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @toggle="toggleModalCreateStore">

    <my-select :model="$v.instanceObj.GradeId"
               :options="gradeDdl"
               class="col-md-6"
               clearable />

    <my-input :model="$v.instanceObj.Name"
              class="col-md-6" />
    <my-input :model="$v.instanceObj.Priority"
              class="col-md-6" />

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
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
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
    this.fillGradeDdlStore();
  }
};
</script>

