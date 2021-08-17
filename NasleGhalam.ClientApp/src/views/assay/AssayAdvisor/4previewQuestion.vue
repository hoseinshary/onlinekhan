<template>

<bs-modal
    title="پیش نمایش سوالات "
    :show="assayStore.openModal._4previewQuestionVue"
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
import { AssayTopic, AssayNumberOfQuestionReportForTopic } from "src/models/IAssay";

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
  console.log(123);
  return this.assayCreate.Lessons;     
}
  //#endregion

  //#region ### watch ###
 
  //#endregion

  //#region ### methods ###
 
  //#endregion

  //#region ### hooks ###
  created() {
    
  }
  //#endregion
}
</script>