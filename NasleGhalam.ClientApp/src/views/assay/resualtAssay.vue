<template>
  <section class="col-md-10 row">
    <div class="col-md-2 ">
     
     <q-card>
      <q-card-title>
        نتیجه آزمون 
      </q-card-title>
      <q-card-separator />
       <q-card-main>
         <div>
         تعداد کل سوالات : {{getCountAll}}
         </div>
                  <div class=" ">
         درصد کنکوری : {{((((3 * getCorrect) - getWrong) / (getCountAll * 3)).toFixed(2) * 100).toFixed(0)}}
         </div>
      </q-card-main>
      <q-card-main>
     <div class="text-green">
       تعداد سوالات درست : {{getCorrect}}
     </div>
        <div class="text-green">
       درصد : {{(getCorrect /getCountAll).toFixed(2) * 100}}
     </div>
      </q-card-main>
       <q-card-main>
   <div class="text-red">
       تعداد سوالات غلط : {{getWrong}}
     </div>
      <div class="text-red">
        درصد : {{(getWrong /getCountAll).toFixed(2) * 100}}
     </div>
      </q-card-main>
         <q-card-main>
      <div class="text-orange">
       تعداد سوالات نزده : {{getNon}}
     </div>
       <div class="text-orange">
       درصد : {{getNon /getCountAll * 100}}
     </div>
      </q-card-main>
    </q-card>
     
     
 
     
    </div>

    <div class="col-md-6" >
        <div class="col-md-12 ">
        
         
           
          <div class="panel center " >

        
         

            پاسخ نامه 
            <br/>
         
            <ul style="list-style-type:none"  class="float-left">
              <li
                v-for="(question,index) in answerSheet.Answers"
                :key="`fruit-${index}`"
                class="float-left"
              >

                <!-- <div v-if="index==0" class="row float-right">
                   <q-chip small  color="yellow text-black">1</q-chip>
                   <q-chip small  color="yellow text-black">2</q-chip>
                   <q-chip small color="yellow text-black">3</q-chip>
                   <q-chip  small color="yellow text-black">4</q-chip>
                </div> -->
                <div class="row" >
                <div v-if="answerSheet.AnswerSheetCorectExams[index].Tashih == 0" class="row " >
                  <q-btn round dense color="green" class="center q-mt-md"  >
                    {{index+1}}
                  </q-btn>
                  <div class="shadow-1 q-ma-sm gutter-xs  bg-grey-2 corner-around ">
                    
                  
                  <q-radio color="green" readonly v-model="answerSheet.Answers[index]" val="1" label="1" left-label />
                  <q-radio color="green" readonly v-model="answerSheet.Answers[index]" val="2" label="2" left-label />
                  <q-radio color="green" readonly v-model="answerSheet.Answers[index]" val="3" label="3" left-label />
                  <q-radio color="green" readonly v-model="answerSheet.Answers[index]" val="4" label="4" left-label />
            
               
                </div>
           
                </div>
                <div v-if="answerSheet.AnswerSheetCorectExams[index].Tashih == 1" class="row" >
                  <q-btn round dense color="red" class="center q-mt-md"  >
                    {{index+1}}
                  </q-btn>
                  <div class="shadow-1 q-ma-sm gutter-xs  bg-grey-2 corner-around row">
                    
                  <!-- <q-checkbox readonly toggle-indeterminate indeterminate-value="1" checked-icon="cancel_presentation" unchecked-icon="radio_button_unchecked" indeterminate-icon="check_box" v-model="answerSheet.Answers[index]" true-value="1" val="1" label="1" left-label/>
                  <q-checkbox readonly toggle-indeterminate indeterminate-value="2" checked-icon="cancel_presentation" unchecked-icon="radio_button_unchecked" indeterminate-icon="check_box" v-model="answerSheet.Answers[index]" true-value="2" val="2" label="2" left-label/>
                  <q-checkbox readonly toggle-indeterminate indeterminate-value="3" checked-icon="cancel_presentation" unchecked-icon="radio_button_unchecked" indeterminate-icon="check_box" v-model="answerSheet.Answers[index]" true-value="3" val="3" label="3" left-label/>
                  <q-checkbox readonly toggle-indeterminate indeterminate-value="4" checked-icon="cancel_presentation" unchecked-icon="radio_button_unchecked" indeterminate-icon="check_box" v-model="answerSheet.Answers[index]" true-value="4" val="4" label="4" left-label/> -->
  

                  <div v-for="index2 in 4" :key="index2">
                   
                    <q-radio v-if="answerSheet.Answers[index] == index2"  checked-icon="cancel_presentation" color="red" readonly v-model="answerSheet.Answers[index]" :val="index2.toString()" :label="index2.toString()" left-label />      
                    <q-radio v-else-if="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer == index2"  checked-icon="check_box" color="blue" readonly v-model="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer" :val="index2.toString()" :label="index2.toString()" left-label />      
                    <q-radio v-else readonly v-model="answerSheet.Answers[index]" :val="index" :label="index2.toString()" left-label />      
                  </div>

                  <!-- <q-radio  checked-icon="cancel_presentation" color="red" readonly v-model="answerSheet.Answers[index]" val="1" label="1" left-label />
                  <q-radio  checked-icon="cancel_presentation" color="red" readonly v-model="answerSheet.Answers[index]" val="2" label="2" left-label />
                  <q-radio  checked-icon="cancel_presentation" color="red" readonly v-model="answerSheet.Answers[index]" val="3" label="3" left-label />
                  <q-radio  checked-icon="cancel_presentation" color="red" readonly v-model="answerSheet.Answers[index]" val="4" label="4" left-label /> -->
                   
                </div>
                
                </div>
                <div v-if="answerSheet.AnswerSheetCorectExams[index].Tashih == 2" class="row" >
                  <q-btn round dense color="orange" class="center q-mt-md"  >
                    {{index+1}}
                  </q-btn>
                  <div class="shadow-1 q-ma-sm gutter-xs  bg-grey-2 corner-around ">
                    
                  
                  <q-radio color="orange" checked-icon="check_box" readonly v-model="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer" val="1" label="1" left-label />
                  <q-radio color="orange" checked-icon="check_box" readonly v-model="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer" val="2" label="2" left-label />
                  <q-radio color="orange" checked-icon="check_box" readonly v-model="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer" val="3" label="3" left-label />
                  <q-radio color="orange" checked-icon="check_box" readonly v-model="answerSheet.AnswerSheetCorectExams[index].CorrectAnswer" val="4" label="4" left-label />
                  
               
                </div>
                
     
                </div>
                
                  <q-btn round class="center q-mt-md " @click="showModalQuestion(answerSheet.QuestionIds[index])">

                  <q-icon  name="description" />
                  </q-btn>
                   <div class="q-pa-lg gutter-xs center" >
                     زمان پاسخ : {{0}}
                  </div>

                     <div class="q-pa-lg gutter-xs center text-grey" >
                     مبحث : 
                  </div>
                </div>
              </li>
            </ul>

            
          </div>  
       
      </div>
     
    </div>

      <modal-question ></modal-question>

  </section>
</template>






<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";




@Component({
  components: {
      ModalQuestion: () => import("./resualtAssay_question.vue")
      
  }
})
export default class AssayAnswerSheetVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assay = vxm.assayStore.assayCreate;
  question = vxm.questionStore.question;
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  questionStore = vxm.questionStore;
  resualt = vxm.assayAnswerSheetStore.assayAnswerSheetResult;
  answerSheet = vxm.assayAnswerSheetStore.assayAnswerSheet;
   
  

  pageAccess = util.getAccess(this.assayStore.modelName);
  
  //tempChoice: Array< string> = ["0","1","2","3" ,"4"];
  //#endregion

  //#region ### computed ###
  get getCorrect()
  {
    return this.answerSheet.AnswerSheetCorectExams.filter(x => x.Tashih ==0 ).length;
  }
get getNon()
  {
    return this.answerSheet.AnswerSheetCorectExams.filter(x => x.Tashih == 2).length;
  }
  get getWrong()
  {
    return this.answerSheet.AnswerSheetCorectExams.filter(x => x.Tashih ==1 ).length;
  }

  get getCountAll()
  {
    return this.answerSheet.AnswerSheetCorectExams.length;
  }

  //#endregion

  //#region ### methods ###



showModalQuestion(index : number ) {
    
    this.questionStore.getById(index);

    this.assayAnswerSheetStore.OPEN_MODAL_QUESTION(true);
  }
  
  //#endregion

  //#region ### hooks ###
  created() {
  
    


    if(this.answerSheet.Id != 0)
      this.assayAnswerSheetStore.getById(this.answerSheet.Id);
  }
  //#endregion
}
</script>