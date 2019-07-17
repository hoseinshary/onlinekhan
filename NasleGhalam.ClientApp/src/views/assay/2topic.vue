<template>
  <section class="row">
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-checkbox label="سوال تصادفی" v-model="assayCreate.RandomQuestion" />
    </div>
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm" v-for="lesson in checkedLessons" :key="lesson.Id">
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
      >
        <template slot="header-custom" slot-scope="prop">
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
    </div>
    <div class="col-12">
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        اطلاعات آزمون
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
@Component({
  components: {}
})
export default class TopicTabVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  topicStore = vxm.topicStore;
  assayCreate = vxm.assayStore.assayCreate;
  //#endregion

  //#region ### computed ###
  get checkedLessons() {
    return this.assayCreate.Lessons.filter(x => x.Checked == true);
  }
  get lessonIds() {
    return this.checkedLessons.map(x => x.Id);
  }
  //#endregion

  //#region ### watch ###
  @Watch("lessonIds")
  lessonIdsChanged(newVal) {
    this.topicStore.fillList().then(x => {
      var topicTreeData = this.topicStore.treeDataByLessonIds(newVal);

      // set assayTopicsTree
      this.checkedLessons.forEach(lesson => {
        util.clearArray(lesson.TopicsTree);
        var tree = topicTreeData.find(x => x.LessonId == lesson.Id);
        if (tree) {
          lesson.TopicsTree.push(tree);
        }
      });
    });
  }
  //#endregion

  //#region ### methods ###
  goToTopicTab() {
    this.$emit("changeTab", "assayTab");
  }

  expandTree(treeRef) {
    this.$refs[treeRef][0]["expandAll"]();
  }

  collapseTree(treeRef) {
    this.$refs[treeRef][0]["collapseAll"]();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonIdsChanged(this.lessonIds);
  }
  //#endregion
}
</script>