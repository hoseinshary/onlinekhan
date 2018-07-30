<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.instanceObj.GradeId"
               :options="gradeDdl"
               class="col-md-6"
               clearable
               @change="setGradeName" />

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
    setGradeName(item) {
      this.instanceObj.GradeName = item.label;
    }
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
  }
};
</script>

