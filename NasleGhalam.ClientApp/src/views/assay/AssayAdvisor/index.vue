<template>
  <div class="col-md-10">
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
            <q-btn color="primary" class="float-right" @click="goToTopicTab">
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
                <div class="col-2">
                  <div class="panel">

                      <q-card class="bg-white corner-around q-mb-sm">
                        <q-card-title> سختی سوال: </q-card-title>
                        <!-- <q-card-separator /> -->
                        <q-card-main>
                          <q-checkbox
                            v-model="filterHardness"
                            :val="14"
                            label="بسیار سخت"
                          />
                          <br />
                          <q-checkbox
                            v-model="filterHardness"
                            :val="13"
                            label="سخت"
                          />
                          <br />
                          <q-checkbox
                            v-model="filterHardness"
                            :val="12"
                            label="متوسط"
                          />
                          <br />
                          <q-checkbox
                            v-model="filterHardness"
                            :val="11"
                            label="آسان"
                          />
                        </q-card-main>

                        <!-- <q-card-actions>
          <q-btn @click="filterHadrnessAction" color="primary" label="اعمال" />
        </q-card-actions> -->
                      </q-card>
                      <q-card class="bg-white corner-around q-mb-sm">
                        <q-card-title> تکرار سوال: </q-card-title>
                        <!-- <q-card-separator /> -->
                        <q-card-main>
                          <q-checkbox
                            v-model="filterRepeatness"
                            val="low"
                            label="کم"
                          />
                          <br />
                          <q-checkbox
                            v-model="filterRepeatness"
                            val="mid"
                            label="متوسط"
                          />
                          <br />
                          <q-checkbox
                            v-model="filterRepeatness"
                            val="high"
                            label="زیاد"
                          />
                        </q-card-main>

                        <!-- <q-card-actions>
          <q-btn color="primary" label="اعمال" />
        </q-card-actions> -->
                      </q-card>
                      <q-card class="bg-white corner-around q-mb-sm">
                        <q-card-title> حیطه: </q-card-title>
                        <q-card-main>
                          <q-select
                            v-model="filterAreaType"
                            :options="lookupStore.areaTypeDdl"
                            multiple
                            filter
                          />
                        </q-card-main>

                        <!-- <q-card-actions>
          <q-btn color="primary" label="اعمال" />
        </q-card-actions> -->
                      </q-card>
                      <q-card class="bg-white corner-around q-mb-sm">
                        <q-card-title> تگ ها : </q-card-title>
                        <q-card-main>
                          <q-select
                            v-model="filterAreaType"
                            :options="lookupStore.areaTypeDdl"
                            multiple
                            filter
                          />
                        </q-card-main>

                        <!-- <q-card-actions>
          <q-btn color="primary" label="اعمال" />
        </q-card-actions> -->
                      </q-card>
                    
                  </div>
                </div>
               
                <div class="col-10">
                  <div  v-if="lessonsCurrent.length != 0" class="panel">
                

                      <q-card v-for="lesson in lessonsCurrent" :key="lesson.Id" class="bg-white corner-around">
                      <div class="col-md-10">
                        <q-card-title
                          >{{ lesson.Name }} ( تعداد سوال :
                          {{ lesson.Questions.length }})</q-card-title
                        >
                        <q-card-separator />
                        <q-card-main>
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
                                  <q-card-separator />
                                  <div class="row col-md-10">
                                  <div class="col-md-4">
                                    <br/>
                                  <base-btn-create :label="`اضافه به آزمون`" />
                                  </div>
                                  <div class="col-md-4 center ">
                                    <br/>
                                    <q-btn  @click="showQuestionAnswer" rounded push color="secondary" icon="arrow_downward"/>
                                  </div>
                                  <div class="col-md-2 ">
                                    <br/>
                                    <div class="float-right">
                                    <q-icon  name="favorite_border" />180
                                    </div>
                                  </div>
                                  <div class="col-md-2">
                                  </div>
                                  </div>
                                </div>
                                <div class="col-md-2">
                                  <div class="center q-mb-sm">
                                  <router-link class=""
                                    :to="`/question/${question.Id}/${lesson.Id}`"
                                  >
                                    سوال ({{ question.Id }})</router-link
                                  >
                                  <br />
                                  <img
                                    :src="$q.localStorage.get.item('ProfilePic')"
                                    class="profile-image "
                                    alt="profile picture"
                                    width="60px"
                                    height="60px"
                                  />
                                  
                                  <br/>
                                  {{ question.Writer.Name }}
                                  </div>
                                  <div style="font-size: 11px;" class=" q-mb-sm row" >
                                    <div class="col-md-3">
                                      سختی: 
                                       <br />
                                      تکرار:
                                    </div>
                                    <div class="col-md-6">
                                      <q-rating
                                    disable
                                    size="16px"
                                    color="green"
                                    icon="stop"
                                    v-model="question.Lookup_QuestionHardnessType.State"
                                    :max="4"
                                  />
                                  <br />
                                   
                                  <q-rating
                                    disable
                                    size="16px"
                                    color="red"
                                    icon="stop"
                                    v-model="question.Lookup_RepeatnessType.State"
                                    :max="3"
                                  />
                                    </div>
                                    <div class="col-md-3">
                                      {{question.Lookup_QuestionHardnessType.Value}}
                                       <br />
                                      {{question.Lookup_RepeatnessType.Value}}
                                    </div>
                                  
                                  
                                  
                                  </div>
                                 
                                  <div class="center" >
                                  امتیاز:<q-rating
                                    disable
                                    size="22px"
                                    color="orange"
                                    v-model="question.Lookup_QuestionRank.State"
                                    :max="4"
                                  />
                                  </div>
                                </div>
                              </li>
                            </ul>
                          </div>
                        </q-card-main>
                      </div>
                    </q-card>

                  </div>  
                  <div v-else class="panel">
                    <img
                    class="corner-around center"
                      src="~assets/Asset 91.jpg"
                      style="
                        width: 80%;"
                    />
                  </div>
                </div>
              </div>
            
          </div>
        </base-panel>

        
      </section>
    </div>


        <modal-student></modal-student>
        <modal-lesson></modal-lesson>
        <modal-topic></modal-topic>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import BasePanel from "src/Components/BasePanel.vue";

@Component({
  components: {

    // studentTab: () => import("./0student.vue"),
    // lessonTab: () => import("./1lesson.vue"),
    // topicTab: () => import("./2topic.vue"),
    //  questionTab: () => import("./4question.vue"),
    // assayTab: () => import("./3assay.vue")


 
   ModalStudent: () => import("./0student.vue"),
    ModalLesson: () => import("./1lesson.vue"),
    ModalTopic: () => import("./2topic.vue")
 
  }
})
export default class AssayVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  selectedTab = "studentTab";
  assayCreate = vxm.assayStore.assayCreate;
  lookupStore = vxm.lookupStore;


  filterHardness: Array<number> = []; //['veryhard' ,'hard','mid' , 'easy'];
  filterRepeatness: Array<number> = []; //'high' ,'mid','low' ];
  filterAreaType: Array<number> = [];

  filterObject: any = [];

  show


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
    this.assayStore.checkedLessons.forEach((element, index) => {
      // this.lessonsCurrent[index].Questions = element.Questions.filter(x => this.filterHardness.includes(x.LookupId_QuestionHardnessType));
      console.log(this.filterHardness);
      console.log(this.filterHardness.includes(11));
    });
  }

  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
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

  showQuestionAnswer()
  {

  }

  showModal2topic() {
    //this.userStore.resetCreate();

    this.assayStore.OPEN_MODAL_2TOPIC(true);
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.assayStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>