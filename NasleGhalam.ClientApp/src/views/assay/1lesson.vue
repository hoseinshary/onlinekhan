<template>
  <section class="row gutter-sm">
    <div class="col-md-4">
      <q-select
        v-model="educationTree.id"
        :options="educationTree_GradeDdl"
        float-label="فیلتر درخت آموزش با مقطع"
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
    <div class="col-md-8">
      <ul style="max-height:500px; overflow-y:auto">
        <li
          v-for="lesson in assayCreate.Lessons"
          :key="lesson.Id"
          class="row shadow-1 q-ma-sm q-pa-sm"
        >
          <div class="col-md-5">
            <q-checkbox v-model="lesson.Checked" />
            {{lesson.Name}}
          </div>
          <div v-if="lesson.Checked" class="col-md-7">
            <section class="row gutter-sm">
              <div class="col">
                <q-input
                  v-model="lesson.CountOfEasy"
                  float-label="آسان"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <q-input
                  v-model="lesson.CountOfMedium"
                  float-label="متوسط"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <q-input
                  v-model="lesson.CountOfHard"
                  float-label="سخت"
                  @focus="$event.target.select()"
                  class="col"
                  align="center"
                  type="number"
                />
              </div>
              <div class="col">
                <q-input
                  v-model="lesson.CountOfQuestions"
                  float-label="کل"
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
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        انتخاب مبحث
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import AssayCreate, { AssayLesson } from "src/models/IAssay";
import ILesson from "src/models/ILesson";

@Component({})
export default class LessonTabVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  educationTreeStore = vxm.educationTreeStore;
  educationTree = this.educationTreeStore.qTreeData;
  assayCreate = vxm.assayStore.assayCreate;
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
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }

  @Watch("lessonStore.gridData")
  lessonGridDataChanged(value: Array<ILesson>) {
    util.clearArray(this.assayCreate.Lessons);
    var list = value.forEach(x => {
      this.assayCreate.Lessons.push(new AssayLesson(x.Id, x.Name));
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
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>