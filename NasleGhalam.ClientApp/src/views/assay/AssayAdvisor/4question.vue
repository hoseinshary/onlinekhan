<template>
  <section class="row gutter-sm">
    <div class="col-md-2">
      <q-card>
        <q-card-title> فیلتر ها </q-card-title>
        <q-card-separator />
        <q-card-main>
          سختی سوال
          <br />
          <q-checkbox v-model="selection" val="one" label="آسان" /> <br />
          <q-checkbox v-model="selection" val="two" label="متوسط" />
          <br />
          <q-checkbox v-model="selection" val="three" label="سخت" />

          <q-card-separator />
         <base-select
          :model="filterModel.Lookup_AreaTypeID"
          :options="lookupStore.areaTypeDdl"
          multiple
          class="col-md-4"
          filter
        />
        </q-card-main>
      </q-card>
    </div>
    <div class="col-md-10">
      <q-card
        class="col-md-12"
        v-for="lesson in assayStore.checkedLessons"
        :key="lesson.Id"
      >
        <q-card-title>{{ lesson.Name }}</q-card-title>
        <q-card-separator />
        <q-card-main>
          <ul>
            <li
              v-for="question in lesson.Questions"
              :key="question.Id"
              class="row shadow-1 q-ma-sm q-pa-sm"
            >
              <img
                :src="question.QuestionPicturePath"
                class="img-original-width"
              />
            </li>
          </ul>
        </q-card-main>
      </q-card>
    </div>
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
  lookupStore = vxm.lookupStore;
  assayCreate = vxm.assayStore.assayCreate;

  filterModel = {
    Lookup_AreaTypeID : 0
  };
  //#endregion

  //#region ### computed ###
  //#endregion

  //#region ### methods ###
  goToTopicTab() {
    this.$emit("changeTab", "topicTab");
  }
  //#endregion

  //#region ### hooks ###
  created() { }
  //#endregion
}
</script>