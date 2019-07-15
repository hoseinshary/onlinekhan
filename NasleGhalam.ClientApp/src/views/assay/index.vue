<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{assayStore.modelName}}</span>
      <div slot="body">
        <q-tabs v-model="selectedTab" class="col-12" inverted color="primary">
          <q-tab slot="title" name="lessonTab" label="درس" />
          <q-tab slot="title" name="topicTab" label="مبحث" />
          <q-tab slot="title" name="assayTab" label="آزمون" />
          <q-tab-pane name="lessonTab" keep-alive>
            <lesson-tab @changeTab="changeTab"></lesson-tab>
          </q-tab-pane>
          <q-tab-pane name="topicTab" keep-alive>
            <topic-tab @changeTab="changeTab"></topic-tab>
          </q-tab-pane>
          <q-tab-pane name="assayTab" keep-alive>
            <assay-tab @changeTab="changeTab"></assay-tab>
          </q-tab-pane>
        </q-tabs>
      </div>
    </base-panel>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    lessonTab: () => import("./1lesson.vue"),
    topicTab: () => import("./2topic.vue"),
    assayTab: () => import("./3assay.vue")
  }
})
export default class AssayVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  selectedTab = "lessonTab";
  //#endregion

  //#region ### computed ###
  // get canCreate() {
  //   return this.pageAccess.indexOf("ایجاد") > -1;
  // }

  //#endregion

  //#region ### watch ###
  //#endregion

  //#region ### methods ###
  changeTab(tab) {
    this.selectedTab = tab;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.assayStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>