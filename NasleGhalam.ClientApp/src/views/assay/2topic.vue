<template>
  <section class="row gutter-sm">
    <div class="col-md-4">
      <!-- <q-tree :nodes="topicTreeData" tick-strategy="leaf" 
      color="blue" node-key="Id" />-->
      <!-- <q-tree
        :nodes="topicTreeData"
        tick-strategy="leaf"
        class="q-pt-lg"
        color="primary"
        node-key="Id"
      >
        <div slot="header-custom" slot-scope="prop">
          {{prop.node.label}}
          <template>
            <q-btn
              outline
              color="positive"
              class="shadow-1 bg-white q-mr-sm q-px-xs"
              icon="save"
              size="sm"
              @click.stop="showModalCreate(prop.node.Id, prop.node.label)"
            />
          </template>
        </div>
      </q-tree>-->
      <div
        class="col-12 shadow-1 q-ma-sm q-pa-sm"
        v-for="lesson in checkedLessons"
        :key="lesson.Id"
      >
        {{lesson.Name}} ({{lesson.CountOfQuestions}} سوال)
        <q-tree :nodes="lesson.Topics" tick-strategy="leaf" color="blue" node-key="Id" />
      </div>
      <!-- <q-select
            v-model="educationTree.id"
            :options="educationTree_GradeDdl"
            float-label="فیلتر درخت آموزش با مقطع"
            clearable
          />
      -->
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

      // set assayTopics
      this.checkedLessons.forEach(lesson => {
        debugger;
        util.clearArray(lesson.Topics);
        var tree = topicTreeData.find(x => x.lessonId == lesson.Id);
        if (tree) {
          lesson.Topics = [tree];
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