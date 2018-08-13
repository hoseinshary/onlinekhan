<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.educationSubGroupObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-6"
               clearable
               @change="setEducationGroupName" /> />

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
      fillEducationSubGroupDdlStore: 'fillDdlStore'
    }),
    setEducationGroupName(item) {
      this.educationSubGroupObj.EducationGroupName = item.label;
    },
    modalOpen() {
      this.fillEducationSubGroupDdlStore();
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

