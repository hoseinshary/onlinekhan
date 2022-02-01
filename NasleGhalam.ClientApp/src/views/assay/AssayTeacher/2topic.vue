<template>

<bs-modal
    title="انتخاب مبحث"
    :show="assayStore.openModal._2topic"
    size="lg"
    @close="assayStore.OPEN_MODAL_2TOPIC(false)"
    @open="bindLessonsCurrent"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>انتخاب مبحث</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_2TOPIC(false)" />
      </q-toolbar>
    </template>

    <slot>
    <div class="col-12 ">
     
      
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        سوالات آزمون
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
       <q-tabs v-model="selectedTab" class="bg-white corner-around col-md-12 " inverted animated color="indigo-8"  >

                  
        <q-tab default v-for="lesson in this.assayCreate.Lessons" :key="lesson.Id" :name="lesson.Name" slot="title" :label="lesson.Name" class="bg-yellow-2"  />
                  
        <q-tab-pane v-for="lesson in this.assayCreate.Lessons" :key="lesson.Id" :name="lesson.Name">
          <section class="row col-md-12">
      <div class="col-md-12 shadow-1 q-ma-sm q-pa-sm">
         جابجایی ترتیب دروس 
         <q-btn size="md" round dense color="secondary" icon="arrow_forward_ios" @click="upList(lesson)" class="q-mr-xs" />
         <q-btn size="md" round dense color="tertiary" icon="arrow_back_ios" @click="downList(lesson)" class="q-mr-sm" />
         <br/>
         <br/>
      <!-- <q-checkbox label="سوال تصادفی" v-model="assayCreate.RandomQuestion" />
      <br /> -->
      <q-checkbox
        label="نمایش جزئیات تعداد سوالات در هر مبحث"
        v-model="assayStore.IsDetailTopic"
        @input="topicWithDetail()"
      />
      <!-- <div class="row">
      <q-checkbox v-if="assayCreate.RandomQuestion == true" v-model="checked" label="Checkbox Label" />
    </div> -->
    </div>
      
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-tree
        :nodes="topicStore.treeDataByLessonId(lesson.Id)"
        class="q-pt-lg"
        color="primary"
        node-key="Id"
        tick-strategy="leaf"
        :ticked.sync="lesson.TopicIds"
      >
        <div slot="header-custom" slot-scope="prop">
          <!-- <div class="col"> -->
          
            <template v-if="prop.node.ParentTopicId == null"
              >({{ getLesson(lesson.Id).CountOfQuestions }}) سوال از
              درس ({{ getLesson(lesson.Id).Name }})</template
            >
            <template v-else>{{ prop.node.label }}</template>
          <!-- </div> -->
          <!-- <div class="col"> -->
            <template>
              
              <div
                v-if="assayStore.IsDetailTopic  "
                class="col-md-12 row"
                style="color: #ada8a7 ; font-size: 13px"
              >
                <!-- <section class="row col-md-12"> -->
                &nbsp;
                &nbsp;
                &nbsp;
                
                <div class="">
                  <label> تعداد سوال جدید:</label>
                    {{getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfNewQuestions}}
                </div>
                &nbsp;
                &nbsp;
                &nbsp;
                
                <div class="">
                  <label>تعداد سوال تکلیف:</label>
                    {{getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfHomeworkQuestions}} 

                </div>
                &nbsp;
                &nbsp;
                &nbsp;
               
                <div class="">
                  <label>تعداد سوال آزمون قبلی:</label>
                  {{getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfAssayQuestions}}
                </div>
                &nbsp;
                &nbsp;
                &nbsp;
                
                <div class="">
                  <label> تعداد کل سوالات : </label>
                  {{getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfNewQuestions+getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfHomeworkQuestions+getTopicDetailForTree(lesson.Id , prop.node.Id).NumberOfAssayQuestions }}
                </div>
                <!-- </section> -->
              </div>
            </template>
          <!-- </div> -->
        </div>
      </q-tree>

    </div>
    
  </section>
        </q-tab-pane>
       </q-tabs>

     
    </slot>

    <template slot="footer">
      <!-- <base-btn-save-back @click="userStore.submitUpdateUserImage()"></base-btn-save-back>
      <base-btn-back @click="userStore.OPEN_MODAL_UPDATE_USER_IMAGE_VUE(false)"></base-btn-back> -->
    </template>
  </bs-modal>




  
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { AssayTopic, AssayNumberOfQuestionReportForTopic, AssayLesson } from "src/models/IAssay";

@Component({
  components: {}
})
export default class TopicTabVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  
  topicStore = vxm.topicStore;
  studentStore = vxm.studentStore;
  assayCreate = vxm.assayStore.assayCreate;


  lessonsCurrent : Array<any> = []; 
  allTopicLeafTicked : Array<number> = [];
  topicLeafTicked: Array<number> = [];
  topicTreeData = [];
  numberOfQuestionReport: AssayNumberOfQuestionReportForTopic = new AssayNumberOfQuestionReportForTopic();

  selectedTab="";
  //#endregion

  //#region ### computed ###


  get getLesson() {
    return lessonId => {
      return this.assayStore.checkedLessons.find(x => x.Id == lessonId);
    };
  }
  get getTopic() {
    return (lessonId, topicId): AssayTopic | null => {
      var lesson = this.assayStore.checkedLessons.find(x => x.Id == lessonId);
      if (!lesson) return null;
      var assayTopic = lesson.Topics.find(x => x.Id == topicId);
      if (assayTopic) return assayTopic;
      else {
        var topic = this.topicStore.detail(topicId);
        if (topic) {
          assayTopic = new AssayTopic(
            topic.Id,
            topic.Title,
            topic.LessonId,
            topic.ParentTopicId
          );
          lesson.Topics.push(assayTopic);
          return assayTopic;
        }
        return null;
      }
    };
  }

bindLessonsCurrent()
{
this.lessonsCurrent = this.assayCreate.Lessons.map((x) => ({
    Id: x.Id,
    Name: x.Name,
    TopicIds: [],
    Topics : []
    
  }))

}

  upList(lesson : AssayLesson )
  {
    var currentLesson = this.assayCreate.Lessons.findIndex(x => x.Id === lesson.Id);
    if(currentLesson)
    {
      this.assayCreate.Lessons.splice(currentLesson-1,0,this.assayCreate.Lessons.splice(currentLesson,1)[0]);
    }

    var currentLesson2 = this.assayStore.checkedLessons.findIndex(x => x.Id === lesson.Id)
    if(currentLesson2)
    {
      this.assayStore.checkedLessons.splice(currentLesson2-1,0,this.assayStore.checkedLessons.splice(currentLesson2,1)[0]);
    }
  }

downList(lesson : AssayLesson )
  {

   var currentLesson = this.assayCreate.Lessons.findIndex(x => x.Id === lesson.Id);
    if(currentLesson >= 0)
    {
      this.assayCreate.Lessons.splice(currentLesson+1,0,this.assayCreate.Lessons.splice(currentLesson,1)[0]);
    }

       var currentLesson2 = this.assayStore.checkedLessons.findIndex(x => x.Id === lesson.Id)
    if(currentLesson2 >= 0)
    {
      this.assayStore.checkedLessons.splice(currentLesson2+1,0,this.assayStore.checkedLessons.splice(currentLesson2,1)[0]);
    }
    
  }
  //#endregion

  //#region ### watch ###
  @Watch("lessonIds")
  lessonIdsChanged(newVal) {
    console.log("lesson ids change");
    this.topicStore.fillList();  
  }

  @Watch("topicLeafTicked")
  topicLeafTickedChanged(newVal, oldVal) {
    var getNodeByKey = this.$refs["tree"]["getNodeByKey"];
    debugger;
    newVal.forEach(topicId => {
      var node = getNodeByKey(topicId);
      var topic = this.getTopic(node.lessonId, node.Id);
      if (topic && !topic.Checked) {
        topic.Checked = true;
      }
    });

    oldVal.forEach(topicId => {
      if (this.topicLeafTicked.indexOf(topicId) > -1) return;
      var node = getNodeByKey(topicId);
      var topic = this.getTopic(node.lessonId, node.Id);
      if (topic && topic.Checked) {
        topic.Checked = false;
      }
    });
  }
  //#endregion

  //#region ### methods ###
  goToTopicTab() {

    this.assayStore.submitPreCreate().then(() => {
      this.$emit("changeTab", "questionTab");
    })
    this.assayStore.OPEN_MODAL_2TOPIC(false);
  }

  expandTree(treeRef) {
    this.$refs[treeRef][0]["expandAll"]();
  }

  collapseTree(treeRef) {
    this.$refs[treeRef][0]["collapseAll"]();
  }

  topicWithDetail() {
    if (this.assayStore.IsDetailTopic) {
      this.studentStore.numberOfQuestionReportForTopic({ lessonIds: this.assayStore.lessonIds, studentId: 9 }).then(d => {
        this.numberOfQuestionReport = d;
      });
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    
        this.topicStore.fillList();

  }
  //#endregion
}
</script>