<template>
  <div class="col-md-8">
    <div class="col-md-12 panel bg-grey-3 q-mb-sm">
      <div class="row">
        <div class="row col-md-12 col-lg-12 background-white">
          <!-- <p class="col-12 text-primary text-weight-bold q-pa-sm">
          مشخصه های سوال
        </p> -->
          <div class="col-3 q-pa-sm" style="color: white">
            <q-btn
              class="bg-blue-5"
              icon="person"
              @click="showModal0student"
              label="دانش آموز"
            />
          </div>
          <div class="col-6 q-pa-sm center" style="color: black">
            <q-btn
              class="bg-orange-5"
              icon="import_contacts"
              @click="showModal1lesson"
              label="درس"
            />
            <!-- </div>
        <div class="col-3 q-pa-sm " style="color: black"> -->
            <q-btn
              class="bg-orange-3"
              icon="format_list_numbered"
              @click="showModal2topic"
              label="مبحث"
            />
          </div>
          <div class="col-3 q-pa-sm" style="color: red">
            <q-btn color="primary" class="float-right" @click="showModal3assay">
              ثبت سوال ها
              <q-icon name="arrow_back" />
            </q-btn>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-10">
      <section class="shadow-13">
        <!-- panel -->
        <base-panel>
          <!-- <span slot="title">{{assayStore.modelName}}</span> -->
          <div slot="body"> 
              <div class="row gutter-xs">
                
                  <base-table :grid-data="assayAnswerSheetStore.gridDataReport" :columns="assayReportGridColumn" hasIndex>
                    <!-- <template slot="Id" slot-scope="data">
                   
                    </template> -->
                  </base-table>

               
             
              </div>
            
          </div>
        </base-panel>

        
      </section>
    </div>


        <modal-student></modal-student>
        <modal-lesson></modal-lesson>
        <modal-topic></modal-topic>
        <modal-assay></modal-assay>
              

  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import BasePanel from "src/Components/BasePanel.vue";
import IQuestion from "src/models/IQuestion";
import BaseBtnDelete from "src/Components/Buttons/BaseBtnDelete.vue";
import IMessageResult from "src/models/IMessageResult";


@Component({
  components: {

    // studentTab: () => import("./0student.vue"),
    // lessonTab: () => import("./1lesson.vue"),
    // topicTab: () => import("./2topic.vue"),
    //  questionTab: () => import("./4question.vue"),
    // assayTab: () => import("./3assay.vue")


 
   ModalStudent: () => import("./AssayAdvisor/0student.vue"),
    ModalLesson: () => import("./AssayAdvisor/1lesson.vue"),
    ModalTopic: () => import("./AssayAdvisor/2topic.vue"),
    
    ModalAssay : () => import("./AssayAdvisor/3assay.vue")
 
  }
})
export default class AssayVue extends Vue {

  //#region ### data ###

  $v:any;
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;

  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  selectedTab = "studentTab";
  assayCreate = vxm.assayStore.assayCreate;
  questionAnswerStore = vxm.questionAnswerStore;
  lookupStore = vxm.lookupStore;
  rangeValues= {
        min: 30,
        max: 90
      };

  page = 1;

  filterHardness: Array<number> = []; //['veryhard' ,'hard','mid' , 'easy'];
  filterRepeatness: Array<number> = []; //'high' ,'mid','low' ];
  filterAreaType: Array<number> = [];

  filterObject: any = [];

  
  assayReportGridColumn = [
      {
        title: "عنوان آزمون",
        data: "Title"
      },
      {
        title: "نوع آزمون",
        data: "AssayVarient"
      },
      {
        title: "تاریخ",
        data: "DateTime"
      },
      {
        title: "درصد کنکوری ",
        data: "ExamScore"
      },
      {
        title: "درصد درست ",
        data: "CorrectScore"
      },
      {
        title: "درصد نادرست ",
        data: "WrongScore"
      },
      {
        title: "درصد نزده",
        data: "NonScore"
      }
      
    ];

  //#endregion

  //#region ### computed ###
  // get canCreate() {
  //   return this.pageAccess.indexOf("ایجاد") > -1;
  // }


 get lessonsCurrent() {

  return this.assayStore.checkedLessons.map((x) => ({
    Id: x.Id,
    Name: x.Name,
    Questions: x.Questions.filter(x => !this.filterHardness.length || this.filterHardness.includes(x.LookupId_QuestionHardnessType))
  }))
}

  //#endregion

  //#region ### methods ###

  filterHadrnessAction() {
    // this.assayStore.checkedLessons.forEach((element, index) => {
    //   // this.lessonsCurrent[index].Questions = element.Questions.filter(x => this.filterHardness.includes(x.LookupId_QuestionHardnessType));
    //   console.log(this.filterHardness);
    //   console.log(this.filterHardness.includes(11));
    // });
  }

  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
  }

  goToNextPage()
  {

    this.assayStore.submitPreCreate();
  }

  getTopicAnswer(question) {
    question.Topicanswer.substring(2,question.Topicanswer.length-2);
  }

  changeTab(tab) {
    this.selectedTab = tab;
  }

  showModal0student() {
    //this.userStore.resetCreate();
    this.assayStore.OPEN_MODAL_0STUDENT(true);
  }

  showModal1lesson() {
    //this.userStore.resetCreate();


    this.assayStore.OPEN_MODAL_1LESSON(true);
  }

   showModal3assay() {


    this.assayStore.OPEN_MODAL_3ASSAY(true);
  }

  showQuestionAnswer(question : any)
  {
      this.questionAnswerStore.getByQuestionId(question.Id).then(
      () => {
          if(question.IsShowAnswer)
          {
              question.IsShowAnswer=false;
              
          }
          else{
              question.IsShowAnswer=true;

            question.QuestionAnswerPath = this.questionAnswerStore.questionAnswer.QuestionAnswerPicturePath;
          }
      }

      );
     
  }


  get showGreen()
  {
    return (lessonId :number , questionId: number ): boolean =>{
           var x = this.assayCreate.Lessons.find(x => x.Id === lessonId) 
          if(x)
          {
            var y = x.Questions.find(z => z.Id === questionId)
            if(y)
              return false;
            
          }
          return true;
    }
    
  }
  AddQuestion(lessonId :number , question )
  {
    var x = this.assayCreate.Lessons.find(x => x.Id === lessonId)
    if(x)
      if(!x.Questions.find( y => y.Id === question.Id))
      {
        x.Questions.push(question);

         var data : IMessageResult = {
        MessageType : 1 ,
        Message : "سوال  "+ question.Id + "  اضافه شد "

      }
      this.assayStore.notify( {vm:this,data:data});
      }

  }

    DeleteQuestion(lessonId :number , question )
  {
    var x = this.assayCreate.Lessons.find(x => x.Id === lessonId)
    if(x)
      if(x.Questions.find( y => y.Id === question.Id))
      {
        x.Questions.splice(x.Questions.findIndex( y => y.Id === question.Id),1);

         var data : IMessageResult = {
        MessageType : 2 ,
        Message : "سوال  "+ question.Id + "  حذف شد "

      }
      this.assayStore.notify( {vm:this,data:data});
      }

  }

  showModal2topic() {
    //this.userStore.resetCreate();

    this.assayStore.OPEN_MODAL_2TOPIC(true);
  }

   showModal4preview() {
    //this.userStore.resetCreate();

    this.assayStore.OPEN_MODAL_4PREVIEWQUESTION(true);
  }

  startNew()
  {
    this.assayCreate.Lessons = [];
    this.assayStore._lessonList =[];

  }
  //#endregion

  @Watch("rangeValues")
  rangeValuesChanged(newVal) {
    if (newVal) {


      
    }
  }



  //#region ### hooks ###
  created() {
    this.assayAnswerSheetStore.fillListReport()
    this.assayStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>