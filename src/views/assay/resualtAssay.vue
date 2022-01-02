<template>
  <bs-modal
    title="نتیجه آزمون "
    position="bottom"
    
    :show="assayAnswerSheetStore.openModal.resualt"
    @close="assayAnswerSheetStore.OPEN_MODAL_RESUALT(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>نتیجه آزمون </q-toolbar-title>
        <q-btn dense icon="close" @click="assayAnswerSheetStore.OPEN_MODAL_RESUALT(false)" />
      </q-toolbar>
    </template>

    <slot>
    <section class="col-md-10  " >
    <div class="col-md-12 row ">
     
     <div class="text-green">
       تعداد سوالات درست : {{2}}
     </div>
     <div class="text-red">
       تعداد سوالات غلط : {{3}}
     </div>
     <div class="text-orange">
       تعداد سوالات نزده : {{2}}
     </div>
     
    </div>
  </section>
    </slot>

    <template slot="footer">
      <base-btn-save-back @click="assayAnswerSheetStore.OPEN_MODAL_RESUALT(false)"></base-btn-save-back>
      <base-btn-back @click="assayAnswerSheetStore.OPEN_MODAL_RESUALT(false)"></base-btn-back>
    </template>
  </bs-modal>


</template>



<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";




@Component({
  components: {
      
      
  }
})
export default class AssayAnswerSheetVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assay = vxm.assayStore.assayCreate;
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  resualt = vxm.assayAnswerSheetStore.assayAnswerSheetResult;
  answerSheet = vxm.assayAnswerSheetStore.assayAnswerSheet;
   
  

  pageAccess = util.getAccess(this.assayStore.modelName);
  
  //tempChoice: Array< string> = ["0","1","2","3" ,"4"];
  //#endregion

  //#region ### computed ###
 

  //#endregion

  //#region ### methods ###




  
  //#endregion

  //#region ### hooks ###
  created() {
  
    this.assayAnswerSheetStore.SET_RESUALT_VUE(this);

  
  }
  //#endregion
}
</script>