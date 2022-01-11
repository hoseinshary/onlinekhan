<template>
  <bs-modal
    title="شروع آزمون"
    :show="assayAnswerSheetStore.openModal.runAssay_Start"
    @close="assayAnswerSheetStore.OPEN_MODAL_RUNASSAY_START(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>شروع آزمون</q-toolbar-title>
        
      </q-toolbar>
    </template>

    <slot>
       <section class="col-12 q-px-md">
  
  </section>
    </slot>

    <template slot="footer">
      <base-btn-save-back @click="assayAnswerSheetStore.OPEN_MODAL_RUNASSAY_START(false)"></base-btn-save-back>
      <base-btn-back @click="assayAnswerSheetStore.OPEN_MODAL_RUNASSAY_START(false)"></base-btn-back>
    </template>
  </bs-modal>


</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class AssayDeleteVue extends Vue {
  //#region ### data ###
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  //#endregionss

  //#region ### methods ###
  confirm() {
    this.assayAnswerSheetStore.submitDelete(this);
  }
  //#endregion
}
</script>

