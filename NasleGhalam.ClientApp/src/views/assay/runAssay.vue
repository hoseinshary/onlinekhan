<template>
  <section class="col-md-10" >
    <div class="col-md-12 row">
     
      <div class="col-md-3 ">
        
         <q-scroll-area style="width: 100%; height: 600px;" >
           
          <div class="panel center " >


              <div id = "intro" style = "text-align:center;">
                {{ timestamp }}
              </div>

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
                  <q-btn round dense color="blue" class="center q-mt-md" @click="scrollToElement(index)" >
                    {{index+1}}
                  </q-btn>
                  <div class="shadow-1 q-ma-sm gutter-xs  bg-grey-2 corner-around ">
                    

                  <q-radio v-model="answerSheet.Answers[index]" val="1" label="1" left-label @blur="addChoice(1,index)"/>
                  <q-radio v-model="answerSheet.Answers[index]" val="2" label="2" left-label @blur="addChoice(2,index)"/>
                  <q-radio v-model="answerSheet.Answers[index]" val="3" label="3" left-label @blur="addChoice(3,index)"/>
                  <q-radio v-model="answerSheet.Answers[index]" val="4" label="4" left-label @blur="addChoice(4,index)"/>

               
                </div>
                   <q-btn round class="center q-mt-md text-negative" @click="eraseAnswer(index)">

                      <q-icon  name="fas fa-eraser" />
                  </q-btn>
                
               
                </div>
              </li>
            </ul>

             <q-btn  dense color="positive" class="center q-mt-md" @click="submitAnswerSheet" >
                  ثبت نهایی آزمون
                </q-btn>
            
          </div>  
           </q-scroll-area>
      </div>
     
      <div class="col-md-9 ">
         
        <div class="panel">
          <q-scroll-area style="width: 100%; height: 600px;" >
                <div>
                  <ul>
                    <li
                      v-for="(question,index) in assay.QuestionsPath"
                      :key="`fruit-${index}`"
                      class="row shadow-1 q-ma-sm q-pa-sm bg-grey-2 corner-around"
                    >
                      <div class="row">
                        <div class="item-inverted-icon-size q-mt-lg  corner-around round">
                        <q-chip :id="index" color="yellow text-black">
                          {{index+1}}
                        </q-chip>
                        </div>
                      <div class="col-md-10">
                        
                        <img
                          :src="question"
                          class="img-original-width corner-around"
                        />
                      </div>
                      <div class="col-md-1 q-pt-sm">
                        <q-checkbox class="text-green" v-model="answerSheet.AfterList[index]" color="green" val="1" label="بعدا میزنم" />
                        
                        <q-checkbox class="text-orange" v-model="answerSheet.MaybeList[index]" color="orange" val="1" label="شاید بزنم"  />
                        <q-checkbox class="text-red" v-model="answerSheet.CantList[index]" color="red" val="1" label="نمی توانم "   />
                      </div>
                      
                      </div>
                    </li>
                  </ul>
                </div>
                  </q-scroll-area>
        </div>  
       
      </div>
  
   <modal-resualt></modal-resualt>


    
    </div>

   

  </section>
  
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { assayStore } from "src/store/assayStore";
import { scroll } from 'quasar'
import { off } from "process";
import * as moment from 'moment';
import { start } from "repl";
const { getScrollTarget, setScrollPosition } = scroll


@Component({
  components: {
   ModalResualt: () => import("./resualtAssay.vue")

      
  }
})
export default class AssayAnswerSheetVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assay = vxm.assayStore.assayCreate;
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  answerSheet = vxm.assayAnswerSheetStore.assayAnswerSheet;
   
  startTime : Date; 
  timestamp = "";

  pageAccess = util.getAccess(this.assayStore.modelName);
  
  //tempChoice: Array< string> = ["0","1","2","3" ,"4"];
  //#endregion

  //#region ### computed ###
 


  //#endregion

  //#region ### methods ###
  getNow() {
      const today = new Date();
      const date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
      const time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      const dateTime = date +' '+ time;
      
   
      var newDate =new Date( (today.getTime() - this.startTime.getTime()) + 621355968000000000 );
      console.log(today.toTimeString());
      console.log(this.startTime.toTimeString());
      this.timestamp = moment(newDate).format('HH:MM:SS');
      

  }

  scrollToElement (el) {
   
    var element  = document.getElementById(el);
    if(element != null)
    {
      let target = getScrollTarget(element)
      let offset = element.offsetTop
      let duration = 1000
      setScrollPosition(target, offset, duration)
    }
  }

  eraseAnswer(index)
  {
     
    this.answerSheet.Answers[index] = "0";
    this.$forceUpdate() ;
  }
  submitAnswerSheet()
  {
    
    this.assayAnswerSheetStore.submitCreate();
  }

  addChoice(val , index)
  {

    this.answerSheet.Answers[index] = val;
  }
  
  //#endregion

  //#region ### hooks ###
  created() {
    // if(this.assay)
      const today = new Date();
      const date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
      const time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      this.startTime = today;
      
    setInterval(this.getNow, 1000);
    for (let i = 0; i < this.assay.QuestionsPath.length; i++) {
      
     this.answerSheet.Answers[i] = "0";
     this.answerSheet.MaybeList[i] = false;
     this.answerSheet.AfterList[i] = false;
     this.answerSheet.CantList[i] = false;
    } 
    this.assayAnswerSheetStore.SET_CREATE_VUE(this);
    this.answerSheet.AssayId = this.assay.Id;
    this.answerSheet.AssayVarient = this.assay.NumberOfVarient;
  }
  //#endregion
}
</script>