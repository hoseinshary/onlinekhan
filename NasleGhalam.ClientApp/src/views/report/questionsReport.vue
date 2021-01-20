<template>

 

  <section >
  <div class="col-md-12 row" >
  <div class="col-md-4 ">
        
        <!-- v-if="showElementTree('educationTree_GradeDdl')" -->
        <q-select
          v-model="educationTree.id"
          :options="educationTree_GradeDdl"
          float-label="جستجو درخت آموزش با مقطع"
          clearable
          class="s-q-input-border s-spacing"
        />
        <!-- v-if="showElementTree('educationTreeData')" -->
        <q-tree
          :nodes="educationTreeData"
          :expanded.sync="educationTree.expanded"
          :ticked.sync="educationTree.leafTicked"
          tick-strategy="leaf"
          class="tree-max-height s-spacing s-border"
          color="blue"
          node-key="Id"
        />
        <q-select
          :value="lessonId"
          @change="lessonIdChanged"
          filter
          :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
          float-label="انتخاب درس"
          class="s-q-input-border s-spacing s-border"
        />
      </div>

  
    <div class="col-md-8 ">

         <base-btn-create
            
            :label="'خروجی اکسل'"
            @click="excelClick"
            class="s-spacing"
          />
    <base-table
      :grid-data="reportStore.gridDataQuestionsReport"
      :columns="reportGridColumns"
      hasIndex
    >
     <template slot="IsActive" slot-scope="data">{{data.row.IsActive? "بلی" : "خیر"}}</template>
      <template slot="IsUpdate" slot-scope="data">{{data.row.IsUpdate? "بلی" : "خیر"}}</template>
       <template slot="IsDelete" slot-scope="data">{{data.row.IsDelete? "بلی" : "خیر"}}</template>
    </base-table>
    </div>
  </div>
  </section>
</template>


<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import utilities from "src/utilities";




@Component({
  components: {}
})
export default class QuestionVue extends Vue {
  //#region ### data ###
  reportStore = vxm.reportStore;
    questionStore = vxm.questionStore;

  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;

   lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;

    pageAccess = util.getAccess(this.questionStore.modelName);


   treeAccessElement = {
    educationTreeData: {
      canEditAdminProp: true
    },
    educationTree_GradeDdl: {
      canEditAdminProp: true
    }
  };

  reportGridColumns = [
    {
      title: "کد سوال",
      data: "Id"
    },
    {
      title: "نام نویسنده",
      data: "WriterName"
    },
    {
      title: "نام ناظر",
      data: "SupervisorName"
    },
    // {
    //   title: "نوع نویسنده",
    //   data: "AuthorTypeName"
    // },

    {
      title: "فعال",
      data: "IsActive"
    },
    {
      title: "ویرایش",
      data: "IsUpdate"
    },
    {
      title: "حذف",
      data: "IsDelete"
    }
  ];

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

    get canShowTree() {
    return this.pageAccess.indexOf("مشاهده درخت پایه") > -1;
  }
  //#endregion

  //#region ### watch ###
@Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
    // clear educationTree leaf
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
  educationTreeTickedIdsChanged(newVal) {
    this.lessonId = 0;
  }

  //#endregion

  //#region ### methods ###
lessonIdChanged(val) {

  
    if (val != undefined) {
      this.lessonId = val;
    }
  this.reportStore.GetAllQuestionsReport({
      lessonId: this.lessonId});
  }

  excelClick(){
     this.reportStore.GetAllQuestionsReportExcel({
      lessonId: this.lessonId});
  
  }

  

  //#endregion

  //#region ### hooks ###

    mounted() {
    let route = this["$route"];
    if (route && route.params.id && route.params.lessonId) {
      // fill educationTree
      this.educationTreeStore
        .GetAllByLessonId(route.params.lessonId)
        .then(tickedEducationTreeIds => {
          utilities.clearArray(this.educationTree.leafTicked);
          tickedEducationTreeIds.forEach(x => {
            this.educationTree.leafTicked.push(x);
          });
        })
        .then(() => {
          this.lessonIdChanged(+route.params.lessonId);
        })
          }
  }
  created() {
      this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }

  //#endregion
}
</script>

