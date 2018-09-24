<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.axillaryBookObj.Name"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.PublishYear"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.Author"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.Isbn"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.Font"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.Price"
              class="col-md-6" />

    <my-input :model="$v.axillaryBookObj.OriginalPrice"
              class="col-md-6" />
    <my-input :model="$v.axillaryBookObj.Description"
              class="col-md-6" />

    <!-- <my-select :model="$v.axillaryBookObj.LookupId_BookType" :options="" class="col-md-6" clearable />
          
          <my-select :model="$v.axillaryBookObj.LookupId_PaperType" :options="" class="col-md-6" clearable />
          <my-select :model="$v.axillaryBookObj.LookupId_PrintType" :options="" class="col-md-6" clearable />
          
          
          <my-select :model="$v.axillaryBookObj.PublisherId" :options="" class="col-md-6" clearable /> -->

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/axillaryBookViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('axillaryBookStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('axillaryBookStore', {
      modelName: 'modelName',
      axillaryBookObj: 'axillaryBookObj',
      isOpenModalEdit: 'isOpenModalEdit'
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

