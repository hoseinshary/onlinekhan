<template>
  <div class="row gutter-sm">
    <div class="col-12">
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        ثبت سوال ها
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
    <div class="col-12 row">
      <div class="col-2">
        <q-card>
          <q-card-title> سختی سوال: </q-card-title>
          <!-- <q-card-separator /> -->
          <ul>
            <li v-for="x in filterHardness" :key="x">
              {{ x }}
            </li>
          </ul>
          <q-card-main>
            <q-checkbox v-model="filterHardness" :val="14" label="بسیار سخت" />
            <br />
            <q-checkbox v-model="filterHardness" :val="13" label="سخت" />
            <br />
            <q-checkbox v-model="filterHardness" :val="12" label="متوسط" />
            <br />
            <q-checkbox v-model="filterHardness" :val="11" label="آسان" />
          </q-card-main>

          <!-- <q-card-actions>
          <q-btn @click="filterHadrnessAction" color="primary" label="اعمال" />
        </q-card-actions> -->
        </q-card>

        <q-card>
          <q-card-title> تکرار سوال: </q-card-title>
          <!-- <q-card-separator /> -->
          <q-card-main>
            <q-checkbox v-model="filterRepeatness" val="low" label="کم" />
            <br />
            <q-checkbox v-model="filterRepeatness" val="mid" label="متوسط" />
            <br />
            <q-checkbox v-model="filterRepeatness" val="high" label="زیاد" />
          </q-card-main>

          <!-- <q-card-actions>
          <q-btn color="primary" label="اعمال" />
        </q-card-actions> -->
        </q-card>

        <q-card>
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
        <q-card>
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
      <div class="col-10">
        <q-card v-for="lesson in lessonsCurrent" :key="lesson.Id">
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
                    class="row shadow-1 q-ma-sm q-pa-sm"
                  >
                    <div class="col-md-10">
                      <img
                        :src="question.QuestionPicturePath"
                        class="img-original-width"
                      />
                      <base-btn-create
                        :label="`اضافه به آزمون`"
                        
                      />
                    </div>

                    <div class="col-md-2">
                      <router-link
                        :to="`/question/${question.Id}/${lesson.Id}`"
                      >
                        سوال ({{ question.Id }})</router-link
                      >
                      <br />
                      <img
                        :src="$q.localStorage.get.item('ProfilePic')"
                        class="profile-image"
                        alt="profile picture"
                        width="60px"
                        height="60px"
                      />
                      <br />
                      {{ question.Writer.Name }}<br />
                      سختی:<q-rating
                        disable
                        size="22px"
                         color="green"
                         icon="gavel"
                        v-model="question.Lookup_QuestionHardnessType.State"
                        :max="4"
                      />
                      <br />
                      تکرار:<q-rating
                        disable
                        size="22px"
                         color="red"
                         icon="repeat"
                        v-model="question.Lookup_RepeatnessType.State"
                        :max="3"
                      /><br />
                      امتیاز:<q-rating
                        disable
                        size="22px"
                         color="orange"
                        v-model="question.Lookup_QuestionRank.State"
                        :max="4"
                      />
                    </div>
                  </li>
                </ul>
              </div>
            </q-card-main>
          </div>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../../utilities/enumeration";
import AssayCreate, { AssayLesson } from "src/models/IAssay";
import ILesson from "src/models/ILesson";
import { assayStore } from "src/store/assayStore";
import { lookupStore } from "src/store/lookupStore";

import { debug } from "util";

@Component({})
export default class LessonTabVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assayCreate = vxm.assayStore.assayCreate;
  lookupStore = vxm.lookupStore;


  //lessonsCurrent: Array<AssayLesson> = [];

  filterHardness: Array<number> = []; //['veryhard' ,'hard','mid' , 'easy'];
  filterRepeatness: Array<number> = []; //'high' ,'mid','low' ];
  filterAreaType: Array<number> = [];

  filterObject: any = [];



  //#endregion

  //#region ### computed ###
  get lessonsCurrent() {

    return this.assayStore.checkedLessons.map((x) => ({
      Id: x.Id,
      Name: x.Name,
      Questions: x.Questions.filter(x => !this.filterHardness.length || this.filterHardness.includes(x.LookupId_QuestionHardnessType))
    }))
  }

  filterHadrnessAction() {



    this.assayStore.checkedLessons.forEach((element, index) => {


      // this.lessonsCurrent[index].Questions = element.Questions.filter(x => this.filterHardness.includes(x.LookupId_QuestionHardnessType));
      console.log(this.filterHardness);
      console.log(this.filterHardness.includes(11));



    });
  }
  //#endregion

  //#region ### methods ###
  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
  }
  //#endregion

  //#region ### hooks ###
  created() {

    //  this.lessonsCurrent = util.cloneObject( this.assayStore.checkedLessons);
  }
  //#endregion
}
</script>