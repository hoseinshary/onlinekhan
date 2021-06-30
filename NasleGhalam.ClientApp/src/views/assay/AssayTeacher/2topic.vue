<template>
  <section class="row">
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-checkbox label="سوال تصادفی" v-model="assayCreate.RandomQuestion" />
      <br />
      <q-checkbox
        label="نمایش جزئیات تعداد سوالات در هر مبحث"
        v-model="assayCreate.IsDetailTopic"
        @input="topicWithDetail()"
      />
      <!-- <div class="row">
      <q-checkbox v-if="assayCreate.RandomQuestion == true" v-model="checked" label="Checkbox Label" />
    </div> -->
    </div>

    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-tree
        :nodes="topicStore.treeDataByLessonIds(lessonIds)"
        ref="tree"
        class="q-pt-lg"
        color="primary"
        node-key="Id"
        tick-strategy="leaf"
        :ticked.sync="topicLeafTicked"
      >
        <div slot="header-custom" slot-scope="prop">
          <!-- <div class="col"> -->
            <template v-if="prop.node.ParentTopicId == null"
              >({{ getLesson(prop.node.lessonId).CountOfQuestions }}) سوال از
              درس ({{ getLesson(prop.node.lessonId).Name }})</template
            >
            <template v-else>{{ prop.node.label }}</template>
          <!-- </div> -->
          <!-- <div class="col"> -->
            <template>
              <div
                v-if="assayCreate.IsDetailTopic && numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id) "
                class="col-md-12 row"
                style="color: #a9a9a9; font-size: 12px"
              >
                <!-- <section class="row gutter-sm"> -->
                &nbsp;
                &nbsp;
                &nbsp;
                <div class="border border-primary">
                  <label>
                    تعداد سوال جدید:
                    {{
                      numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfNewQuestions
                    }}</label
                  >
                </div>
                &nbsp;
                &nbsp;
                &nbsp;
                <div class="">
                  <label> تعداد سوال تکلیف:{{   numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfHomeworkQuestions }} </label>
                </div>
                &nbsp;
                &nbsp;
                &nbsp;
                <div class="">
                  <label>
                    تعداد سوال آزمون قبلی:
                    {{   numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfAssayQuestions }}</label
                  >
                </div>
                &nbsp;
                &nbsp;
                &nbsp;
                <div class="">
                  <label> تعداد کل سوالات :{{  numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfNewQuestions+numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfHomeworkQuestions+numberOfQuestionReport.LessonReports.find(
                        (x) => x.Id === prop.node.lessonId
                      ).TopicReports.find(y=> y.ID === prop.node.Id).NumberOfAssayQuestions }} </label>
                </div>
                <!-- </section> -->
              </div>
            </template>
          <!-- </div> -->
        </div>
      </q-tree>
    </div>
    <div class="col-12">
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        سوالات آزمون
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
  </section>
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
  get checkedLessons() {
    return this.assayCreate.Lessons.filter(x => x.Checked == true);
  }
  get lessonIds() {
    return this.checkedLessons.map(x => x.Id);
  }
  get getLesson() {
    return lessonId => {
      return this.checkedLessons.find(x => x.Id == lessonId);
    };
  }
  get getTopic() {
    return (lessonId, topicId): AssayTopic | null => {
      var lesson = this.checkedLessons.find(x => x.Id == lessonId);
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
  //#endregion

  //#region ### watch ###
  @Watch("lessonIds")
  lessonIdsChanged(newVal) {
    this.topicStore.fillList();
  }

  @Watch("topicLeafTicked")
  topicLeafTickedChanged(newVal, oldVal) {
    var getNodeByKey = this.$refs["tree"]["getNodeByKey"];
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
  }

  expandTree(treeRef) {
    this.$refs[treeRef][0]["expandAll"]();
  }

  collapseTree(treeRef) {
    this.$refs[treeRef][0]["collapseAll"]();
  }

  topicWithDetail() {
    if (this.assayCreate.IsDetailTopic) {
      this.studentStore.numberOfQuestionReportForTopic({ lessonIds: this.lessonIds, studentId: 9 }).then(d => {
        this.numberOfQuestionReport = d;
      });
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonIdsChanged(this.lessonIds);
  }
  //#endregion
}
</script>