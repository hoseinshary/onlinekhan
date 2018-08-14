<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.educationSubGroupObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-6"
               clearable
               @change="setEducationGroupName" />

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

