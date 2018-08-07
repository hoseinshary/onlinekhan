<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.gradeLevelObj.GradeId"
               :options="gradeDdl"
               class="col-md-6"
               clearable
               @change="setGradeName" />

    <my-input :model="$v.gradeLevelObj.Name"
              class="col-md-6" />
    <my-input :model="$v.gradeLevelObj.Priority"
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
    }),
    setGradeName(item) {
      this.gradeLevelObj.GradeName = item.label;
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
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

