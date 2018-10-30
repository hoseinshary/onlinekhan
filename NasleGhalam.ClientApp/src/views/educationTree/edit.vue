<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                   @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.educationTreeObj.Name" class="col-md-6" />
          
           <my-select :model="$v.educationTreeObj.LookupId_EducationTreeState"
               :options="lookupEducationTreeStateDdl"
               class="col-md-6"
               clearable />
          
          

  </my-modal-edit>
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
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
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
      isOpenModalEdit: 'isOpenModalEdit'
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
    this.editVueStore(this);
  }
};
</script>

