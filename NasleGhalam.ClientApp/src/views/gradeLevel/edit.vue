<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.gradeLevelObj.GradeId"
               :options="gradeDdl"
               class="col-md-6"
               clearable
               ref="gradeLevelId" />

    <my-input :model="$v.gradeLevelObj.Name"
              class="col-md-6">
    </my-input>

    <my-input :model="$v.gradeLevelObj.Priority"
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
    }),
    submit() {
      this.gradeLevelObj.GradeName = this.$refs.gradeLevelId.getSelectedLabel();
      this.submitEditStore();
    },
    modalOpen() {
      this.fillGradeDdlStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('gradeLevelStore', {
      modelName: 'modelName',
      gradeLevelObj: 'gradeLevelObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState('gradeStore', {
      gradeDdl: 'gradeDdl'
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

