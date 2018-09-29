<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.universityBranchObj.EducationSubGroupId"
               :options="educationSubGroupDdl"
               class="col-md-6"
               clearable
               ref="EducationSubGroupId" />

    <my-input :model="$v.universityBranchObj.Name"
              class="col-md-6" />

    <my-input :model="$v.universityBranchObj.Balance"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/universityBranch/universityBranchCrudViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('universityBranchStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions('educationSubGroupStore', {
      fillEducationSubGroupByEducationGroupIdDdlStore:
        'fillEducationSubGroupByEducationGroupIdDdlStore'
    }),
    modalOpen() {
      this.fillEducationSubGroupByEducationGroupIdDdlStore(
        this.universityBranchIndexObj.EducationGroupId
      );
    },
    submit() {
      this.universityBranchObj.EducationSubGroupName = this.$refs.EducationSubGroupId.getSelectedLabel();
      this.submitEditStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('universityBranchStore', {
      modelName: 'modelName',
      universityBranchObj: 'universityBranchObj',
      universityBranchIndexObj: 'universityBranchIndexObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState('educationSubGroupStore', {
      educationSubGroupDdl: 'educationSubGroupDdl'
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

