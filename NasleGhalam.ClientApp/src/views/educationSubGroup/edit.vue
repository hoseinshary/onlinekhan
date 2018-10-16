<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.educationSubGroupObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-6"
               clearable
               ref="educationGroupId" />

    <my-input :model="$v.educationSubGroupObj.Name"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/educationSubGroupViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationSubGroupStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions('educationGroupStore', {
      fillEducationGroupDdlStore: 'fillDdlStore'
    }),
    submit() {
      this.educationSubGroupObj.EducationGroupName = this.$refs.educationGroupId.getSelectedLabel();
      this.submitEditStore();
    },
    modalOpen() {
      this.fillEducationGroupDdlStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationSubGroupStore', {
      modelName: 'modelName',
      educationSubGroupObj: 'educationSubGroupObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState('educationGroupStore', {
      educationGroupDdl: 'educationGroupDdl'
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

