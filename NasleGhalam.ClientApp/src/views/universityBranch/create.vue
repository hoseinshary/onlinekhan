<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submit"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.universityBranchObj.EducationSubGroupId"
               :options="educationSubGroupDdl"
               class="col-md-6"
               clearable
               ref="EducationSubGroupId" />

    <my-input :model="$v.universityBranchObj.Name"
              class="col-md-6" />

    <my-input :model="$v.universityBranchObj.SiteAverage"
              class="col-md-6 offset-md-3" />
    <my-input :model="$v.universityBranchObj.Balance1Low"
              class="col-md-6" />
    <my-input :model="$v.universityBranchObj.Balance1High"
              class="col-md-6" />
    <my-input :model="$v.universityBranchObj.Balance2Low"
              class="col-md-6" />
    <my-input :model="$v.universityBranchObj.Balance2High"
              class="col-md-6" />

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
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
    submit(closeModal) {
      this.universityBranchObj.EducationSubGroupName = this.$refs.EducationSubGroupId.getSelectedLabel();
      this.submitCreateStore(closeModal);
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
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

