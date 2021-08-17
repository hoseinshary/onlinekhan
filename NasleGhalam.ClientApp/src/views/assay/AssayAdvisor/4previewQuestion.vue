<template>

<bs-modal
    title="پیش نمایش سوالات "
    :show="assayStore.openModal._4previewQuestion"
    size="lg"
    @close="assayStore.OPEN_MODAL_4PREVIEWQUESTION(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="primary" text-color>
        <q-toolbar-title>پیش نمایش سوالات</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_4PREVIEWQUESTION(false)" />
      </q-toolbar>
    </template>
           
    <slot>
     <section class="row col-md-12">
    <div class="col-md-12 shadow-1 q-ma-sm q-pa-sm">
 
                  <q-tabs class="bg-white   corner-around " inverted animated color="indigo-8"  >

                  
                  <q-tab default v-for="lesson in lessonsCurrent" :key="lesson.Id" :name="lesson.Name" slot="title" :label="lesson.Name" class="bg-yellow-2"  />
                  
                  <q-tab-pane v-for="lesson in lessonsCurrent" :key="lesson.Id" :name="lesson.Name"><div class="col-md-10">
                        <label
                          >{{ lesson.Name }} ( تعداد سوال :
                          {{ lesson.Questions.length }})</label
                        >
                          <div>
                            <ul>
                              <li
                                v-for="question in lesson.Questions"
                                :key="question.Id"
                                class="row shadow-1 q-ma-sm q-pa-sm bg-grey-2 corner-around"
                              >
                                
                                <div class="col-md-10">
                                  <!-- <div class="">
                                  <label class="bg-faded  text-white"> {{question.TopicAnswer}} </label>
                                  </div> -->
                                  <img
                                    :src="question.QuestionPicturePath"
                                    class="img-original-width corner-around"
                                  />
                                 
                                  
                                  
                                
                                </div>
                                 <div class="col-md-2">
                                  <div class="center q-mb-sm">
                                 <br/>
                                 <br/>
                                 <div class="row">
                                 <div class="q-mx-md">
                                  <q-btn  @click="DeleteQuestion(lesson.Id, question)" rounded push color="negative" icon="remove"/>
                                 </div>
                                 <div>
                                  <q-btn size="sm" round dense color="secondary" icon="expand_less" @click="upList(lesson,question)" class="q-mr-xs" />
                                  <q-btn size="sm" round dense color="tertiary" icon="expand_more" @click="downList(lesson,question)" class="q-mr-sm" />
                                 </div>
                                 </div>
                                  </div>
                                </div>
                              </li>
                            </ul>
                          </div>
                      </div></q-tab-pane>
</q-tabs>

    </div>

    
    
  </section>
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
  topicLeafTicked: Array<number> = [];
  topicTreeData = [];
  numberOfQuestionReport: AssayNumberOfQuestionReportForTopic = new AssayNumberOfQuestionReportForTopic();

  //#endregion



  //#region ### computed ###
get lessonsCurrent()
{
  return this.assayCreate.Lessons;     
}
  //#endregion

  //#region ### watch ###
 
  //#endregion

  //#region ### methods ###
 DeleteQuestion(lessonId :number , question )
  {
    var x = this.assayCreate.Lessons.find(x => x.Id === lessonId)
    if(x)
      if(x.Questions.find( y => y.Id === question.Id))
        x.Questions.splice(x.Questions.findIndex( y => y.Id === question.Id),1);

  }

   upList(lesson : AssayLesson ,question :any)
  {

    var currentLesson = this.assayCreate.Lessons.find(x => x.Id === lesson.Id);
    if(currentLesson)
    {
      var currentIndex = currentLesson.Questions.findIndex(x => x.Id === question.Id);

          if(currentIndex && currentIndex > 0)
          {
              currentLesson.Questions.splice(currentIndex-1, 0, currentLesson.Questions.splice(currentIndex, 1)[0]);
          }
    }

  }
downList(lesson : AssayLesson ,question:any)
  {
    var currentLesson = this.assayCreate.Lessons.find(x => x.Id === lesson.Id);
    if(currentLesson)
    {
      var currentIndex = currentLesson.Questions.findIndex(x => x.Id === question.Id);

      if((currentIndex || currentIndex ==0) && currentIndex < currentLesson.Questions.length-1)
      {
          currentLesson.Questions.splice(currentIndex+1, 0, currentLesson.Questions.splice(currentIndex, 1)[0]);
      }
    }
    
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.assayStore.SET_4OREVIEWQUESTION_VUE(this);
  }
  //#endregion
}
</script>