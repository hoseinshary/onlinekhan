<template>
  <section class="col-md-10" >
    <div class="col-md-12 row">
      <div class="col-md-2">
          <div class="panel center">
            پاسخ نامه 
            <br/>
         
            <ul style="list-style-type:none"  class="float-left">
              <li
                v-for="(question,index) in assay.QuestionsPath"
                :key="`fruit-${index}`"
                class="float-left"
              >

                <!-- <div v-if="index==0" class="row float-right">
                   <q-chip small  color="yellow text-black">1</q-chip>
                   <q-chip small  color="yellow text-black">2</q-chip>
                   <q-chip small color="yellow text-black">3</q-chip>
                   <q-chip  small color="yellow text-black">4</q-chip>
                </div> -->
                <div class="col-md-12 row">
                <q-btn round dense color="blue" class="center q-mt-md" >
                  {{index+1}}
                </q-btn>
                <div class="shadow-1 q-ma-sm  bg-grey-2 corner-around">
                <q-radio v-model="option" val="1" label="" float-label="1" />
                <q-radio v-model="option" val="2" label="" />
                <q-radio v-model="option" val="3" label="" />
                <q-radio v-model="option" val="4" label="" />
                </div>
                
                </div>
              </li>
            </ul>

             <q-btn  dense color="positive" class="center q-mt-md" >
                  ثبت نهایی آزمون
                </q-btn>
            
          </div>  
      </div>
      <div class="col-md-10">
        <div class="panel">
                <div>
                  <ul>
                    <li
                      v-for="(question,index) in assay.QuestionsPath"
                      :key="`fruit-${index}`"
                      class="row shadow-1 q-ma-sm q-pa-sm bg-grey-2 corner-around"
                    >
                      <div class="row">
                        <div class="item-inverted-icon-size q-mt-lg  corner-around round">
                        <q-chip  color="yellow text-black">
                          {{index+1}}
                        </q-chip>
                        </div>
                      <div class="col-md-11">
                        
                        <img
                          :src="question"
                          class="img-original-width corner-around"
                        />
                      </div>
                      </div>
                    </li>
                  </ul>
                </div>
        </div>  
      </div>
  

    
    </div>
  </section>
  
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { assayStore } from "src/store/assayStore";

@Component({
  components: {

      
  }
})
export default class CityVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assay = vxm.assayStore.assayCreate;

  

  pageAccess = util.getAccess(this.assayStore.modelName);
  
  //#endregion

  //#region ### computed ###
 


  //#endregion

  //#region ### methods ###
  


  
  //#endregion

  //#region ### hooks ###
  created() {
    
  }
  //#endregion
}
</script>