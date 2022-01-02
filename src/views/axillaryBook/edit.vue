<template>
  <base-modal-edit
    :title="axillaryBookStore.modelName"
    :show="axillaryBookStore.openModal.edit"
    size="lg"
    @confirm="axillaryBookStore.submitEdit"
    @reset="axillaryBookStore.resetEdit"
    @open="open"
    @close="axillaryBookStore.OPEN_MODAL_EDIT(false)"
  >
    <section class="col-md-8">
      <div class="row gutter-sm q-mx-sm">
        <base-input :model="$v.axillaryBook.Name" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.PublishYear" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.Author" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.Isbn" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.Price" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.OriginalPrice" class="col-sm-6"/>
        <base-input :model="$v.axillaryBook.Font" class="col-sm-6"/>
        <base-select
          :model="$v.axillaryBook.LookupId_BookType"
          :options="lookupStore.bookTypeDdl"
          class="col-sm-6"
        />
        <base-select
          :model="$v.axillaryBook.LookupId_PaperType"
          :options="lookupStore.paperTypeDdl"
          class="col-sm-6"
        />
        <base-select
          :model="$v.axillaryBook.LookupId_PrintType"
          :options="lookupStore.printTypeDdl"
          class="col-sm-6"
        />
        <base-select
          :model="$v.axillaryBook.PublisherId"
          :options="publisherStore.ddl"
          class="col-sm-6"
        />
      </div>
    </section>

    <section class="col-md-4">
      <div class="row q-ml-sm">
        <q-field class="col-12 q-mb-lg">
          <q-uploader
            url="url"
            float-label="تصویر"
            name="img"
            hide-upload-button
            auto-expand
            ref="fileUpload"
            extensions=".jpg,.jpeg,.png"
            @add="imageSelected"
            @remove:cancel="imageRemoved"
          />
        </q-field>

        <div class="col-12 text-center" v-if="showPreview && axillaryBook.ImgPath.length != 0">
          <q-card inline style="width:150px">
            <q-card-media>
              <img :src="axillaryBook.ImgPath" alt="preview">
            </q-card-media>
          </q-card>
        </div>
      </div>
    </section>

    <base-input :model="$v.axillaryBook.Description" type="textarea" class="col-12"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { axillaryBookValidations } from "src/validations/axillaryBookValidation";

@Component({
  validations: axillaryBookValidations
})
export default class AxillaryBookEditVue extends Vue {
  $v: any;

  //#region ### data ###
  axillaryBookStore = vxm.axillaryBookStore;
  lookupStore = vxm.lookupStore;
  publisherStore = vxm.publisherStore;
  axillaryBook = vxm.axillaryBookStore.axillaryBook;
  showPreview = true;
  //#endregion

  //#region ### methods ###
  open() {
    this.lookupStore.fillPrintType();
    this.lookupStore.fillBookType();
    this.lookupStore.fillPaperType();
    this.publisherStore.fillList();
    this.showPreview = true;
  }

  imageSelected() {
    this.showPreview = false;
  }

  imageRemoved() {
    this.showPreview = true;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.axillaryBookStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

