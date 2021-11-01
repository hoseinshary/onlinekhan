<template>
  <bs-modal
    title="بارگذاری تصویر"
    :show="userStore.openModal.update_user_image"
    @close="userStore.OPEN_MODAL_UPDATE_USER_IMAGE_VUE(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>بارگذاری تصویر</q-toolbar-title>
        <q-btn dense icon="close" @click="userStore.OPEN_MODAL_UPDATE_USER_IMAGE_VUE(false)" />
      </q-toolbar>
    </template>

    <slot>
      <q-field class="col-sm-12">
        <q-uploader
          url="url"
          float-label="تصویر"
          name="img"
          auto-expand
          ref="fileUpload"
          extensions=".jpg"
        />
      </q-field>
    </slot>    

    <template slot="footer">
      <base-btn-save-back @click="userStore.submitUpdateUserImage()"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_UPDATE_USER_IMAGE_VUE(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class AssayPrinteVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  //#endregion

  //#region ### methods ###
 
  //#endregion
}
</script>

