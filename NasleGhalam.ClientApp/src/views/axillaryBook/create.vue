<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size="lg"
                   @confirm="submit"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <q-field class="col-sm-6">
      <q-uploader url="url"
                  float-label="تصویر"
                  name="img"
                  hide-upload-button
                  auto-expand
                  ref="fileUpload"
                  extensions=".jpg,.jpeg,.png" />
    </q-field>

    <div class="col-12"></div>

    <my-input :model="$v.axillaryBookObj.Name"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.PublishYear"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.Author"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.Isbn"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.Price"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.OriginalPrice"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.axillaryBookObj.Font"
              class="col-sm-6 col-md-4" />

    <my-select :model="$v.axillaryBookObj.LookupId_BookType"
               :options="lookupBookTypeDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="LookupId_BookType" />

    <my-select :model="$v.axillaryBookObj.LookupId_PaperType"
               :options="lookupPaperTypeDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="LookupId_PaperType" />

    <my-select :model="$v.axillaryBookObj.LookupId_PrintType"
               :options="lookupPrintTypeDdl"
               class="col-sm-6"
               clearable
               ref="LookupId_PrintType" />

    <my-select :model="$v.axillaryBookObj.PublisherId"
               :options="publisherDdl"
               class="col-sm-6"
               clearable
               ref="PublisherId" />

    <my-input :model="$v.axillaryBookObj.Description"
              type="textarea"
              class="col-12" />

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions({
      fillPrintTypeDdl: 'lookupStore/fillPrintTypeDdlStore',
      fillBookTypeDdl: 'lookupStore/fillBookTypeDdlStore',
      fillPaperTypeDdl: 'lookupStore/fillPaperTypeDdlStore',
      fillPublisherDdl: 'publisherStore/fillDdlStore'
    }),
    modalOpen() {
      this.fillPrintTypeDdl();
      this.fillBookTypeDdl();
      this.fillPaperTypeDdl();
      this.fillPublisherDdl();
    },
    submit(closeModal) {
      this.axillaryBookObj.PublisherName = this.$refs.PublisherId.getSelectedLabel();
      this.axillaryBookObj.BookTypeName = this.$refs.LookupId_BookType.getSelectedLabel();
      this.axillaryBookObj.PaperTypeName = this.$refs.LookupId_PaperType.getSelectedLabel();
      this.axillaryBookObj.PrintTypeName = this.$refs.LookupId_PrintType.getSelectedLabel();
      this.submitCreateStore(closeModal);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('axillaryBookStore', {
      modelName: 'modelName',
      axillaryBookObj: 'axillaryBookObj',
      isOpenModalCreate: 'isOpenModalCreate'
    }),
    ...mapState({
      lookupPrintTypeDdl: s => s.lookupStore.lookupPrintTypeDdl,
      lookupBookTypeDdl: s => s.lookupStore.lookupBookTypeDdl,
      lookupPaperTypeDdl: s => s.lookupStore.lookupPaperTypeDdl,
      publisherDdl: s => s.publisherStore.publisherDdl
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