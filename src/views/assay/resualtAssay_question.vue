<template>
  <bs-modal
    title="نمایش سوال و جواب"
    :show="assayAnswerSheetStore.openModal.questionShow"
    size="lg"
    @close="assayAnswerSheetStore.OPEN_MODAL_QUESTION(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>نمایش سوال و جواب</q-toolbar-title>
        <q-btn dense icon="close" @click="assayAnswerSheetStore.OPEN_MODAL_QUESTION(false)" />
      </q-toolbar>
    </template>

        <img
        :src="question.QuestionPicturePath"
        class="img-original-width corner-around"
      />

      <div class="col-md-4 center ">
        <br/>
        <q-btn  @click="showQuestionAnswer()" rounded push color="secondary" icon="arrow_downward"/>
        
      </div>

        <div v-if="isShowAnswer" class="q-mt-sm"> 
        
            <!-- {{IsShowAnswer(question)}} -->
            <img 
          :src="question.QuestionAnswers[0].QuestionAnswerPicturePath"
          class="img-original-width corner-around"
        />
        </div>

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
export default class StudentVue extends Vue {
  //#region ### data ###
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  isShowAnswer = false;
  
  question = vxm.questionStore.question;
  //#endregion

  //#region ### computed ###


 


  //#endregion

  //#region ### methods ###

  showQuestionAnswer()
  {
          if(this.isShowAnswer)
          {
              this.isShowAnswer=false;
              
          }
          else{
              this.isShowAnswer=true;
          }
     
     
  }

  //#endregion

  //#region ### hooks ###
  created() {

      this.assayAnswerSheetStore.SET_QUESTION_VUE(this);  
  }
  //#endregion
}

</script>

