<template>
  <base-modal-create
    :title="axillaryBookStore.modelName"
    :show="axillaryBookStore.openModal.create"
    size="lg"
    @confirm="axillaryBookStore.submitCreate"
    @reset="axillaryBookStore.resetCreate"
    @open="open"
    @close="axillaryBookStore.OPEN_MODAL_CREATE(false)"
  >
    <q-field class="col-sm-6">
      <q-uploader
        url="url"
        float-label="تصویر"
        name="img"
        hide-upload-button
        auto-expand
        ref="fileUpload"
        extensions=".jpg,.jpeg,.png"
      />
    </q-field>
    <div class="col-12"></div>
    <base-input :model="$v.axillaryBook.Name" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.PublishYear" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.Author" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.Isbn" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.Price" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.OriginalPrice" class="col-sm-6 col-md-4"/>
    <base-input :model="$v.axillaryBook.Font" class="col-sm-6 col-md-4"/>
    <base-select
      :model="$v.axillaryBook.LookupId_BookType"
      :options="lookupStore.bookTypeDdl"
      class="col-sm-6 col-md-4"
    />
    <base-select
      :model="$v.axillaryBook.LookupId_PaperType"
      :options="lookupStore.paperTypeDdl"
      class="col-sm-6 col-md-4"
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
    <base-input :model="$v.axillaryBook.Description" type="textarea" class="col-12"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { axillaryBookValidations } from "src/validations/axillaryBookValidation";

@Component({
  validations: axillaryBookValidations
})
export default class AxillaryBookCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  axillaryBookStore = vxm.axillaryBookStore;
  lookupStore = vxm.lookupStore;
  publisherStore = vxm.publisherStore;
  axillaryBook = vxm.axillaryBookStore.axillaryBook;
  //#endregion

  //#region ### methods ###
  open() {
    this.lookupStore.fillPrintType();
    this.lookupStore.fillBookType();
    this.lookupStore.fillPaperType();
    this.publisherStore.fillList();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.axillaryBookStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>