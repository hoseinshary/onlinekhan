<template>
  <section class="row">
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-checkbox label="سوال تصادفی" v-model="assayCreate.RandomQuestion" />
<br/>
      <q-checkbox label="نمایش جزئیات تعداد سوالات در هر مبحث" v-model="assayCreate.IsDetailTopic" @input="topicWithDetail()"
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
        <template slot="header-custom" slot-scope="prop">
          <template v-if="prop.node.ParentTopicId == null"
            >({{ getLesson(prop.node.lessonId).CountOfQuestions }}) سوال از درس
            ({{ getLesson(prop.node.lessonId).Name }})</template
          >
          <template v-else>{{ prop.node.label }}</template>

          <!-- <q-checkbox v-model="checked" label="Checkbox Label" />
          <q-input
            v-if="
              prop.node.children.length == 0 &&
              !assayCreate.RandomQuestion &&
              getTopic(prop.node.lessonId, prop.node.Id).Checked
            "
            v-model="getTopic(prop.node.lessonId, prop.node.Id).CountOfEasy"
            float-label="آسان"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && getTopic(prop.node.lessonId, prop.node.Id).Checked"
            v-model="getTopic(prop.node.lessonId, prop.node.Id).CountOfMedium"
            float-label="متوسط"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && getTopic(prop.node.lessonId, prop.node.Id).Checked"
            v-model="getTopic(prop.node.lessonId, prop.node.Id).CountOfHard"
            float-label="سخت"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && getTopic(prop.node.lessonId, prop.node.Id).Checked"
            v-model="getTopic(prop.node.lessonId, prop.node.Id).CountOfQuestions"
            float-label="کل"
            align="center"
            readonly
          /> -->
        </template>
      </q-tree>
    </div>
    <!-- <div class="col-12 shadow-1 q-ma-sm q-pa-sm" v-for="lesson in checkedLessons" :key="lesson.Id">
      {{lesson.Name}} ({{lesson.CountOfQuestions}} سوال)
      <q-btn color="primary" @click="expandTree('tree'+lesson.Id)" icon="unfold_more">
        <q-tooltip>باز کردن درخت</q-tooltip>
      </q-btn>
      <q-btn color="primary" @click="collapseTree('tree'+lesson.Id)" icon="unfold_less">
        <q-tooltip>بستن کردن درخت</q-tooltip>
      </q-btn>

      <q-tree
        :nodes="lesson.TopicsTree"
        :ref="'tree'+lesson.Id"
        class="q-pt-lg"
        color="primary"
        node-key="Id"
        tick-strategy="leaf"
        :ticked.sync="topicLeafTicked[lesson.Id]"
      > -->
        <!-- <template slot="header-custom" slot-scope="prop">
          <template v-if="prop.node.children.length==0">
            <q-checkbox :label="prop.node.Name" v-model="prop.node.Checked" />
          </template>
          <template v-else>{{prop.node.Name}}</template>
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && prop.node.Checked"
            v-model="prop.node.CountOfEasy"
            float-label="آسان"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && prop.node.Checked"
            v-model="prop.node.CountOfMedium"
            float-label="متوسط"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && prop.node.Checked"
            v-model="prop.node.CountOfHard"
            float-label="سخت"
            @focus="$event.target.select()"
            align="center"
            type="number"
            class="q-mx-sm"
          />
          <q-input
            v-if="prop.node.children.length==0 && !assayCreate.RandomQuestion && prop.node.Checked"
            v-model="prop.node.CountOfQuestions"
            float-label="کل"
            align="center"
            readonly
          />
        </template>
      </q-tree>
    </div> -->
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
import { AssayTopic } from "src/models/IAssay";

@Component({
  components: {}
})
export default class TopicTabVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  topicStore = vxm.topicStore;
  assayCreate = vxm.assayStore.assayCreate;
  topicLeafTicked: Array<number> = [];
  topicTreeData = [];
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
    this.topicStore.fillList3Level();
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
      this.$emit("changeTab", "questionTab");})
  }

  expandTree(treeRef) {
    this.$refs[treeRef][0]["expandAll"]();
  }

  collapseTree(treeRef) {
    this.$refs[treeRef][0]["collapseAll"]();
  }

  topicWithDetail()
  {

  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonIdsChanged(this.lessonIds);
  }
  //#endregion
}
</script>