<template>
  <bs-modal
    title="شروع آزمون"
    :show="assayAnswerSheetStore.openModal.runAssay_Start"
    center
    @close="assayAnswerSheetStore.OPEN_MODAL_RUNASSAY_START(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>شروع آزمون</q-toolbar-title>
      </q-toolbar>
    </template>

    <slot>
       <section class="col-12 q-px-md">
          با آرزوی موفقیت در این آزمون برای شروع روی دکمه شروع آزمون کلیک کنید

  </section>
    </slot>

    <template slot="footer">
      <q-btn  dense color="primary" class="center q-mt-md" @click="closeModal" >
                  شروع آزمون
                </q-btn>
    </template>
  </bs-modal>


</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import asd from "src/views/assay/runAssay.vue"
@Component
export default class AssayDeleteVue extends Vue {
  //#region ### data ###
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  //#endregionss

  closeModal()
  {
    this.$root.$emit('component1')    ;
    this.assayAnswerSheetStore.SET_INDEX_VUE.call("startTimer");
    this.assayAnswerSheetStore.OPEN_MODAL_RUNASSAY_START(false);
  }
  //#region ### methods ###
  confirm() {

  }
  //#endregion
}
</script>

