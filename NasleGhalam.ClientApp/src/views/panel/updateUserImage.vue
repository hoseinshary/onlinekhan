<template>
  <bs-modal
    title="بارگذاری تصویر"
    :show="userStore.openModal.update_user_image"
    @close="userStore.OPEN_MODAL_UPDATE_USER_IMAGE(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="blue-8" text-color>
        <q-toolbar-title>بارگذاری تصویر</q-toolbar-title>
        <q-btn dense icon="close" @click="userStore.OPEN_MODAL_UPDATE_USER_IMAGE(false)" />
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
      <base-btn-save-back @click="$emit('confirm', true)"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_UPDATE_USER_IMAGE(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";

@Component({})
export default class UpdateUserImageVue extends Vue {
  $v: any;

  //#region ### data ###
  userStore = vxm.userStore;
  //#endregion

  //#region ### hooks ###
  created() {
    this.userStore.SET_UPDATE_USER_IMAGE_VUE(this);
  }
  //#endregion
}
</script>

