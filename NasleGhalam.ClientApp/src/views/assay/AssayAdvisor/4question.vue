<template>
  <section class="row gutter-sm">
    <q-card class="col-12" v-for="lesson in assayStore.checkedLessons" :key="lesson.Id">
      <q-card-title>{{lesson.Name}}</q-card-title>
      <q-card-separator />
      <q-card-main>
        <ul>
          <li
            v-for="question in lesson.Questions"
            :key="question.Id"
            class="row shadow-1 q-ma-sm q-pa-sm"
          >
            <img :src="question.QuestionPicturePath" class="img-original-width" />
          </li>
        </ul>
      </q-card-main>
    </q-card>

    <div class="col-12">
      <q-btn color="primary" class="float-right" @click="goToTopicTab">
        ثبت سوال ها
        <q-icon name="arrow_back" />
      </q-btn>
    </div>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../../utilities/enumeration";
import AssayCreate, { AssayLesson } from "src/models/IAssay";
import ILesson from "src/models/ILesson";

@Component({})
export default class LessonTabVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  assayCreate = vxm.assayStore.assayCreate;
  //#endregion

  //#region ### computed ###
  //#endregion

  //#region ### methods ###
  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
  }
  //#endregion

  //#region ### hooks ###
  created() {}
  //#endregion
}
</script>