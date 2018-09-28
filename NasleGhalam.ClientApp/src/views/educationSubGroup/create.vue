<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submit"
                   @reset="resetCreateStor"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.educationSubGroupObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-6"
               clearable
               ref="educationGroupId" />

    <my-input :model="$v.educationSubGroupObj.Name"
              class="col-md-6" />

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions('educationGroupStore', {
      fillEducationGroupDdlStore: 'fillDdlStore'
    }),
    submit(closeModal) {
      this.educationSubGroupObj.EducationGroupName = this.$refs.educationSubGroupId.getSelectedLabel();
      this.submitCreateStore(closeModal);
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
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

