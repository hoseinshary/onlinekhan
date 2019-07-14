<template>
  <section class="row">
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm">
      <q-checkbox label="سوال تصادفی" v-model="assayCreate.RandomQuestion" />
    </div>
    <div class="col-12 shadow-1 q-ma-sm q-pa-sm" v-for="lesson in checkedLessons" :key="lesson.Id">
      {{lesson.Name}} ({{lesson.CountOfQuestions}} سوال)
      <q-tree :nodes="lesson.Topics" class="q-pt-lg" color="primary" node-key="Id">
        <template slot="header-custom" slot-scope="prop">
          <div
            :class="assayCreate.RandomQuestion && prop.node.children.length == 0 && prop.node.Checked?'col-sm-4': 'col'"
          >
            <template v-if="prop.node.children.length==0">
              <q-checkbox :label="prop.node.Name" v-model="prop.node.Checked" />
            </template>
            <template v-else>{{prop.node.Name}}</template>
          </div>
          <div
            class="col-sm-8"
            v-show="!assayCreate.RandomQuestion && prop.node.children.length==0 && prop.node.Checked"
          >
            <section class="row gutter-xs">
              <div class="col">
                <q-input
                  v-model="prop.node.CountOfEasy"
                  float-label="آسان"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <q-input
                  v-model="prop.node.CountOfMedium"
                  float-label="متوسط"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <q-input
                  v-model="prop.node.CountOfHard"
                  float-label="سخت"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <!-- <input type="number" v-model="prop.node.CountOfQuestions" /> -->
                <q-input
                  v-model="prop.node.CountOfQuestions"
                  float-label="کل"
                  class="col"
                  align="center"
                  readonly
                />
                <!-- <label>کل سوال ها</label>
                <span>{{prop.node.CountOfQuestions}}</span>-->
              </div>
            </section>
          </div>
        </template>
      </q-tree>
    </div>
    <!-- <q-select
            v-model="educationTree.id"
            :options="educationTree_GradeDdl"
            float-label="فیلتر درخت آموزش با مقطع"
            clearable
          />
    -->
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

      // set assayTopics
      this.checkedLessons.forEach(lesson => {
        util.clearArray(lesson.Topics);
        var tree = topicTreeData.find(x => x.LessonId == lesson.Id);
        if (tree) {
          lesson.Topics.push(tree);
        }
      });
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonIdsChanged(this.lessonIds);
  }
  //#endregion
}
</script>