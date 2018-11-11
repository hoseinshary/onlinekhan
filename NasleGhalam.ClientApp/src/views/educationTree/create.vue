<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false);">

    <my-input :model="$v.educationTreeObj.Name" class="col-md-6" />
        <my-select :model="$v.educationTreeObj.LookupId_EducationTreeState"
               :options="lookupEducationTreeStateDdl"
               class="col-md-6"
               clearable />
          <!-- <my-input :model="$v.educationTreeObj.LookupId_EducationTreeState" class="col-md-6" /> -->
          
  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/educationTreeViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationTreeStore', [
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions('lookupStore', [
      'fillEducationTreeStateDdlStore'
    ]),
    modalOpen:function(){
       this.fillEducationTreeStateDdlStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationTreeStore', {
      modelName: 'modelName',
      educationTreeObj: 'educationTreeObj',
      isOpenModalCreate: 'isOpenModalCreate'
    }),
    ...mapState('lookupStore', [
      'lookupEducationTreeStateDdl'
    ])
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

