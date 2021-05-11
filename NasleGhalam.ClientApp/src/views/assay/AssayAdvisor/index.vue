<template>
<div class="col-md-8">
 <q-card >
      <section class="row s-border s-spacing col-md-12 col-lg-12" >
        <!-- <p class="col-12 text-primary text-weight-bold q-pa-sm">
          مشخصه های سوال
        </p> -->
        <div class="col-3 q-pa-sm" style="color: red">
          دانش آموز:
          <span class="text-red"></span>
        </div>
        <div class="col-3 q-pa-sm" style="color: red">
          درس:
          <span class="text-black"></span>
        </div>
        <div class="col-3 q-pa-sm " style="color: red">
          مبحث:
          <span class="text-black"></span>
        </div>
        <div class="col-3 q-pa-sm" style="color: red">
          تعداد سوالات :
        </div>
      </section>
 </q-card >
  <section >
    <!-- panel -->
    <base-panel>
      <!-- <span slot="title">{{assayStore.modelName}}</span> -->
      <div slot="body">
        <q-tabs v-model="selectedTab" class="col-12" inverted color="primary">
          <q-tab slot="title" name="studentTab" label="دانش آموز" />
          <q-tab slot="title" name="lessonTab" label="درس" />
          <q-tab slot="title" name="topicTab" label="مبحث" />
          <q-tab slot="title" name="questionTab" label="سوال" />
          <q-tab slot="title" name="assayTab" label="آزمون" />
          
           <q-tab-pane name="studentTab" keep-alive>
            <student-tab @changeTab="changeTab"></student-tab>
          </q-tab-pane>
          <q-tab-pane name="lessonTab" keep-alive>
            <lesson-tab @changeTab="changeTab"></lesson-tab>
          </q-tab-pane>
          <q-tab-pane name="topicTab" keep-alive>
            <topic-tab @changeTab="changeTab"></topic-tab>
          </q-tab-pane>
          <q-tab-pane name="assayTab" keep-alive>
            <assay-tab @changeTab="changeTab"></assay-tab>
          </q-tab-pane>
          <q-tab-pane name="questionTab" keep-alive>
            <question-tab @changeTab="changeTab"></question-tab>
          </q-tab-pane>
        </q-tabs>
      </div>
    </base-panel>
  </section>
</div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {

    studentTab: () => import("./0student.vue"),
    lessonTab: () => import("./1lesson.vue"),
    topicTab: () => import("./2topic.vue"),
    
    assayTab: () => import("./3assay.vue"),
     questionTab: () => import("./4question.vue")
   
  }
})
export default class AssayVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  selectedTab = "studentTab";
  //#endregion

  //#region ### computed ###
  // get canCreate() {
  //   return this.pageAccess.indexOf("ایجاد") > -1;
  // }
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