<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 size="lg"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <section class="col-md-8">
      <div class="row gutter-sm q-mx-sm">
        <my-input :model="$v.axillaryBookObj.Name"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.PublishYear"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.Author"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.Isbn"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.Price"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.OriginalPrice"
                  class="col-sm-6" />

        <my-input :model="$v.axillaryBookObj.Font"
                  class="col-sm-6" />

        <my-select :model="$v.axillaryBookObj.LookupId_BookType"
                   :options="lookupBookTypeDdl"
                   class="col-sm-6"
                   clearable
                   ref="LookupId_BookType" />

        <my-select :model="$v.axillaryBookObj.LookupId_PaperType"
                   :options="lookupPaperTypeDdl"
                   class="col-sm-6"
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
      </div>
    </section>

    <section class="col-md-4">
      <div class="row q-ml-sm">
        <q-field class="col-12 q-mb-lg">
          <q-uploader url="url"
                      float-label="تصویر"
                      name="img"
                      hide-upload-button
                      auto-expand
                      ref="fileUpload"
                      extensions=".jpg,.jpeg,.png"
                      @add="imageSelected"
                      @remove:cancel="imageRemoved" />
        </q-field>

        <div class="col-12 text-center"
             v-if="showPreview && axillaryBookObj.ImgPath.length != 0">
          <q-card inline
                  style="width:150px">
            <q-card-media>
              <img :src="axillaryBookObj.ImgPath" alt="preview">
        </q-card-media>
          </q-card>
        </div>
      </div>
    </section>

    <my-input :model="$v.axillaryBookObj.Description"
              type="textarea"
              class="col-12" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/axillaryBookViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  data() {
    return {
      showPreview: true
    };
  },
  /**
   * methods
   */
  methods: {
    imageSelected() {
      this.showPreview = false;
    },
    imageRemoved() {
      this.showPreview = true;
    },
    ...mapActions('axillaryBookStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions({
      fillPrintTypeDdl: 'lookupStore/fillPrintTypeDdlStore',
      fillBookTypeDdl: 'lookupStore/fillBookTypeDdlStore',
      fillPaperTypeDdl: 'lookupStore/fillPaperTypeDdlStore',
      fillPublisherDdl: 'publisherStore/fillDdlStore'
    }),
    modalOpen() {
      this.showPreview = true;
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
      this.submitEditStore(closeModal);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('axillaryBookStore', {
      modelName: 'modelName',
      axillaryBookObj: 'axillaryBookObj',
      isOpenModalEdit: 'isOpenModalEdit'
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
    this.editVueStore(this);
  }
};
</script>

