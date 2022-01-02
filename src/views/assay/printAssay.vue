<template>
  <bs-modal
    title="چاپ آزمون"
    :show="assayStore.openModal.print"
    @close="assayStore.OPEN_MODAL_PRINT(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>چاپ آزمون</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_PRINT(false)" />
      </q-toolbar>
    </template>

    <slot>
     <q-list highlight>
        <q-list-header>{{assayCreate.Title}}</q-list-header>
      
        <q-item v-if="assayCreate.File1 != '' && assayCreate.File1 != null ">
          <q-item-main label="فایل 1" />
          <q-item-side right>
              <a
                  :href="assayCreate.AssayPdfPath1"
                  target="_blank"
                  class="q-mr-sm text-red"
                  
              >لینک دانلود فایل PDF </a>
          </q-item-side>
           <q-item-side right>
              <a
                  :href="assayCreate.AssayWordPath1"
                  target="_blank"
                  class="q-mr-sm"
              >لینک دانلود فایل WORD </a>
          </q-item-side>
        </q-item>
         <q-item v-if="assayCreate.File2 != ''&& assayCreate.File2 != null ">
          <q-item-main label="فایل 2" />
          <q-item-side right>
              <a
                  :href="assayCreate.AssayPdfPath2"
                  target="_blank"
                  class="q-mr-sm text-red"
                  
              >لینک دانلود فایل PDF </a>
          </q-item-side>
           <q-item-side right>
              <a
                  :href="assayCreate.AssayWordPath2"
                  target="_blank"
                  class="q-mr-sm"
              >لینک دانلود فایل WORD </a>
          </q-item-side>
        </q-item>
         <q-item v-if="assayCreate.File3 != ''&& assayCreate.File3 != null ">
          <q-item-main label="فایل 3" />
          <q-item-side right>
              <a
                  :href="assayCreate.AssayPdfPath3"
                  target="_blank"
                  class="q-mr-sm text-red"
                  
              >لینک دانلود فایل PDF </a>
          </q-item-side>
           <q-item-side right>
              <a
                  :href="assayCreate.AssayWordPath3"
                  target="_blank"
                  class="q-mr-sm"
              >لینک دانلود فایل WORD </a>
          </q-item-side>
        </q-item>
         <q-item v-if="assayCreate.File4 != ''&& assayCreate.File4 != null  ">
          <q-item-main label="فایل 4" />
          <q-item-side right>
              <a
                  :href="assayCreate.AssayPdfPath4"
                  target="_blank"
                  class="q-mr-sm text-red"
                  
              >لینک دانلود فایل PDF </a>
          </q-item-side>
           <q-item-side right>
              <a
                  :href="assayCreate.AssayWordPath4"
                  target="_blank"
                  class="q-mr-sm"
              >لینک دانلود فایل WORD </a>
          </q-item-side>
        </q-item>
      
    </q-list>
    </slot>

    <template slot="footer">
      
      <base-btn-back @click="assayStore.OPEN_MODAL_PRINT(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { assayStore } from "src/store/assayStore";
@Component
export default class AssayPrintVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assayCreate= vxm.assayStore.assayCreate;
  //#endregionss

  //#region ### methods ###
  
  //#endregion
}
</script>

