<template>
<bs-modal
    title="انتخاب درس"
    :show="assayStore.openModal._1lesson"
    size="lg"
    @close="assayStore.OPEN_MODAL_1LESSON(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>انتخاب درس</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_1LESSON(false)" />
      </q-toolbar>
    </template>

    <slot>
      <section class="row gutter-lg col-md-12">
    <div class="col-md-3">
      <q-select
        v-model="educationTree.id"
        :options="educationTree_GradeDdl"
        float-label="جستجو درخت آموزش با مقطع"
        clearable
      />
      <q-tree
        :nodes="educationTreeData"
        :expanded.sync="educationTree.expanded"
        :ticked.sync="educationTree.leafTicked"
        tick-strategy="leaf"
        color="blue"
        node-key="Id"
      />
    </div>
    <div class="col-md-9">
      <ul style="max-height: 500px; overflow-y: auto">
        <li
          v-for="lesson in assayCreate.Lessons"
          :key="'lesson' + lesson.Id"
          class="row shadow-1 q-ma-sm q-pa-sm"
        >
          <div class="col-md-5">
            <q-checkbox
              v-model="lesson.Checked"
              @input="getQuestionNumberReport(lesson.Id)"
            />

            {{ lesson.Name }}
          </div>
          <div v-if="lesson.Checked" class="col-md-12">
            <section class="row gutter-sm">
              <div class="col">
                <label>
                  تعداد سوال جدید:
                  {{ numberOfQuestionReport.NumberOfNewQuestions }}</label
                >
                <q-input
                  v-model="lesson.CountOfEasy"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <label>
                  تعداد سوال تکلیف:
                  {{numberOfQuestionReport.NumberOfHomeworkQuestions}}
                </label>

                <q-input
                  v-model="lesson.CountOfMedium"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <label>
                  تعداد سوال آزمون قبلی:
                  {{ numberOfQuestionReport.NumberOfAssayQuestions }}</label
                >

                <q-input
                  v-model="lesson.CountOfHard"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <label>
                  تعداد کل سوالات   :{{numberOfQuestionReport.NumberOfNewQuestions + numberOfQuestionReport.NumberOfAssayQuestions +numberOfQuestionReport.NumberOfHomeworkQuestions}}
                </label>
                <q-input
                  v-model="lesson.CountOfQuestions"
                  class="col"
                  align="center"
                  readonly
                />
              </div>
            </section>
          </div>
        </li>
      </ul>
    </div>
    <div class="col-12">
      <!-- <q-btn color="primary" class="float-right" @click="goToTopicTab">
        انتخاب مبحث
        <q-icon name="arrow_back" />
      </q-btn> -->
    </div>
  </section>
    </slot>

    <template slot="footer">
      <base-btn-save-back @click="assayStore.OPEN_MODAL_1LESSON(false)"></base-btn-save-back>
      <base-btn-back @click="assayStore.OPEN_MODAL_1LESSON(false)"></base-btn-back>
    </template>
  </bs-modal>





  
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../../utilities/enumeration";
import AssayCreate, { AssayLesson, AssayNumberOfQuestionReport } from "src/models/IAssay";
import ILesson from "src/models/ILesson";
import { DefaultUser } from "src/models/IUser";

@Component({})
export default class LessonTabVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  studentStore = vxm.studentStore;
  assayStore = vxm.assayStore;
  educationTreeStore = vxm.educationTreeStore;
  educationTree = this.educationTreeStore.qTreeData;
  assayCreate = vxm.assayStore.assayCreate;
  numberOfQuestionReport: AssayNumberOfQuestionReport = new AssayNumberOfQuestionReport();

  //#endregion

  //#region ### computed ###
  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }


  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.educationTree.leafTicked.splice(
      0,
      this.educationTree.leafTicked.length
    );
    let index = this.educationTree.expanded.indexOf(oldVal);
    if (index > -1) {
      this.educationTree.expanded.splice(index, 1);
    }

    if (this.educationTree.expanded.indexOf(newVal) == -1) {
      this.educationTree.expanded.push(newVal);
    }
  }



  @Watch("educationTree.leafTicked")
  tickedEducationTreeIdsChanged(newVal) {
    util.clearArray(this.assayCreate.Lessons);
    this.lessonStore.gridDataByEducationTreeIds(newVal).forEach(x => {
      this.assayCreate.Lessons.push(new AssayLesson(x.Id, x.Name));
    });
  }
  //#endregion

  //#region ### methods ###
  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
  }

  getQuestionNumberReport(LessonId: number) {
    this.studentStore.numberOfQuestionReport({ lessonId: LessonId, studentId: 9 }).then(d => {
      this.numberOfQuestionReport = d;
    });
    //debugger;
    //  this.numberOfQuestionReport.NumberOfNewQuestions = this["numberOfQuestionReport.NumberOfNewQuestions"];
    //  this.numberOfQuestionReport.NumberOfAssayQuestions = this["numberOfQuestionReport.NumberOfAssayQuestions"];
    //  this.numberOfQuestionReport.NumberOfHomeworkQuestions = this["numberOfQuestionReport.NumberOfHomeworkQuestions"];


    // console.log("vue :", this["numberOfQuestionReport.NumberOfNewQuestions"]);//255
    // console.log("vue :", this.numberOfQuestionReport.NumberOfNewQuestions);//0


  }

  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.fillList();
    this.studentStore.SET_LESSON_ASSAY_VUE(this);

    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>