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
      <ul>
        <li
          v-for="lesson in lessonList"
          :key="lesson.LessonId"
          class="row shadow-1 q-ma-sm q-pa-sm"
        >
          <div class="col-md-5">
            <q-checkbox v-model="lesson.Checked" />
            {{lesson.LessonName}}
          </div>
          <div v-if="lesson.Checked" class="col-md-7">
            <section class="row">
              <q-input
                v-model="lesson.CountOfQuestion"
                float-label="تعداد سوال ها"
                @focus="$event.target.select()"
                class="col"
                align="center"
                type="number"
              />
              <q-input
                v-model="lesson.CountOfEasy"
                float-label="آسان"
                @focus="$event.target.select()"
                class="col"
                align="center"
                type="number"
              />
              <q-input
                v-model="lesson.CountOfMedium"
                float-label="متوسط"
                @focus="$event.target.select()"
                class="col"
                align="center"
                type="number"
              />
              <q-input
                v-model="lesson.CountOfHard"
                float-label="سخت"
                @focus="$event.target.select()"
                class="col"
                align="center"
                type="number"
              />
            </section>
          </div>
        </li>
      </ul>
    </div>
  </section>
</template>



<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({})
export default class LessonTabVue extends Vue {
  //#region ### data ###

  assayStore = vxm.assayStore;
  lessonStore = vxm.lessonStore;
  educationTreeStore = vxm.educationTreeStore;
  educationTree = this.educationTreeStore.qTreeData;
  lessonList: Array<any> = [];
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
  lessonGridDataChanged(value) {
    var list = value.map(x => ({
      LessonId: x.Id,
      LessonName: x.Name,
      Checked: false,
      CountOfQuestion: 0,
      CountOfEasy: 0,
      CountOfMedium: 0,
      CountOfHard: 0
    }));

    util.clearArray(this.lessonList);
    list.forEach(element => {
      this.lessonList.push(element);
    });
  }
  //#endregion

  //#region ### methods ###
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