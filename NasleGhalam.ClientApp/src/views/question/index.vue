<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{questionStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-12">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-checkbox
                v-model="showNoJudgement"
                label="نمایش سوال های بدون ارزیابی"
                @input="fillGrid()"
              />
              <br>
              <q-checkbox
                v-model="showWithoutTopic"
                label="نمایش سوال های بدون مبحث"
                @input="fillGrid()"
              />
            </section>
          </div>
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-select
                v-model="educationTree.id"
                :options="educationTree_GradeDdl"
                float-label="فیلتر درخت آموزش با مقطع"
                clearable
              />
              <!-- <q-field class="col-12">
                <q-input v-model="educationTreeFilter" float-label="جستجو در درخت آموزش" clearable/>
              </q-field>-->
              <q-tree
                :nodes="educationTreeData"
                :expanded.sync="educationTree.expanded"
                :ticked.sync="educationTree.leafTicked"
                tick-strategy="leaf"
                class="tree-max-height"
                color="blue"
                node-key="Id"
              />
              <q-select
                :value="lessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddl"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>
          <div class="col-sm-7" v-if="!showWithoutTopic">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-field class="col-12">
                <q-input v-model="topicTree.filter" float-label="جستجوی مبحث" clearable/>
              </q-field>
              <q-tree
                :nodes="topicTreeData"
                :expanded.sync="topicTree.expanded"
                :selected.sync="topicTree.selected"
                :ticked.sync="topicTree.leafTicked"
                :filter="topicTree.filter"
                tick-strategy="leaf"
                class="q-pt-lg"
                color="primary"
                accordion
                node-key="Id"
              />
            </section>
          </div>
          <br>
        </section>
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${questionStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="questionStore.gridData" :columns="questionGridColumn" hasIndex>
          <template slot="Context" slot-scope="data">
            <div v-if="data.row.Context && data.row.Context.length> 100">
              {{(`${data.row.Context.substring(0,100)} ...`)}}
              <q-tooltip>{{data.row.Context}}</q-tooltip>
            </div>
            <div v-else>{{data.row.Context}}</div>
          </template>
          <template slot="Id" slot-scope="data">
            <!-- <q-btn
              outline
              round
              icon="list"
              color="brown"
              size="sm"
              class="shadow-1 bg-white q-mr-sm"
              @click="showModalQuestionJudge(data.row.Id)"
            >
              <q-tooltip>ارزیابی</q-tooltip>
            </q-btn>-->
            <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>
      </div>
    </base-panel>
    <!-- modals -->
    <!-- <modal-create v-if="canCreate"></modal-create> -->
    <!--<modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
    <modal-question-judge></modal-question-judge>-->
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {
    // ModalCreate: () => import("./create.vue"),
    // ModalEdit: () => import("./edit.vue"),
    // ModalDelete: () => import("./delete.vue")
  }
})
export default class QuestionVue extends Vue {
  //#region ### data ###
  questionStore = vxm.questionStore;
  topicStore = vxm.topicStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionGridColumn = [
    {
      title: "متن سوال",
      data: "Context"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
  lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;
  topicTree = this.topicStore.qTreeData;
  showNoJudgement = false;
  showWithoutTopic = false;
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }

  get topicTreeData() {
    debugger;
    var treeData = this.topicStore.treeDataByLessonId(this.lessonId);
    if (this.topicTree.setToFirstLevel) {
      this.topicTree.expanded = this.topicTree.firstLevel;
      this.topicTree.setToFirstLevel = false;
    }
    return treeData;
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
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }

  @Watch("topicTree.leafTicked")
  topicTreeTickedIdsChanged(newVal) {
    this.fillGrid();
  }
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.questionStore.resetCreate();
    this.questionStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.questionStore.resetEdit();
    this.questionStore.getById(id).then(() => {
      this.questionStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.questionStore.getById(id).then(() => {
      this.questionStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    this.lessonId = val;
    this.topicTree.setToFirstLevel = true;
    // clear topicTree leaf
    this.topicTree.leafTicked.splice(0, this.topicTree.leafTicked.length);
  }

  fillGrid() {
    this.questionStore.fillList({
      lessonId: this.lessonId,
      topicIds: this.topicTree.leafTicked,
      showNoJudgement: this.showNoJudgement,
      showWithoutTopic: this.showWithoutTopic
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationTreeStore.fillList().then(res => {
      this.topicStore.fillList();
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
// import { mapState, mapActions } from "vuex";
// import viewModel from "viewModels/question/questionIndexViewModel";

// export default {
//   components: {
//     "modal-create": () => import("./create"),
//     "modal-edit": () => import("./edit"),
//     "modal-delete": () => import("./delete"),
//     "modal-question-judge": () => import("../questionJudge/index")
//   },
//   /**
//    * data
//    */
//   data() {
//     var pageAccess = this.$util.initAccess("/question");
//     return {
//       pageAccess,
//       educationTreeData: [],
//       topicFilter: "",
//       educationTreeFilter: "",
//       showNoJudgement: false,
//       showWithoutTopic: false,
//       questionGridColumn: [
//         {
//           title: "متن سوال",
//           data: "Context"
//         },

//         {
//           title: "عملیات",
//           data: "Id",
//           searchable: false,
//           sortable: false,
//           visible: pageAccess.canEdit || pageAccess.canDelete
//         }
//       ]
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("questionStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore"
//     ]),
//     ...mapActions("topicStore", {
//       fillTopicTreeStore: "fillTreeStore"
//     }),
//     ...mapActions("educationTreeStore", {
//       getAllGrade: "getAllGrade",
//       fillEducationTreeStore: "fillTreeStore",
//       fillEducationTreeByGradeIdStore: "fillTreeByGradeIdStore"
//     }),
//     ...mapActions("lessonStore", {
//       fillLessonDdlStore: "fillDdlStore"
//     }),
//     ...mapActions("questionJudgeStore", {
//       toggleModalQuestionJudgeStore: "toggleModalStore",
//       fillGridQuestionJudgeByQuestionId: "fillGridStore"
//     }),
//     showModalCreate() {
//       // reset data on modal show
//       this.resetCreateStore();
//       // show modal
//       this.toggleModalCreateStore(true);
//     },
//     showModalEdit(id) {
//       // reset data on modal show
//       this.resetEditStore();
//       // get data by id
//       this.getByIdStore(id).then(() => {
//         // show modal
//         this.toggleModalEditStore(true);
//       });
//     },
//     showModalDelete(id) {
//       // get data by id
//       this.getByIdStore(id).then(() => {
//         // show modal
//         this.toggleModalDeleteStore(true);
//       });
//     },
//     showModalQuestionJudge(id) {
//       this.getByIdStore(id).then(() => {
//         // show modal
//         this.fillGridQuestionJudgeByQuestionId(id).then(() => {
//           // show modal
//           this.toggleModalQuestionJudgeStore({ isOpen: true, questionId: id });
//         });
//       });
//       // get data by id
//     },
//     gradeDdlChange(val) {
//       // filter lesson tree by gradeId
//       var self = this;
//       this.$util.clearArray(this.educationTreeData);
//       this.$util.clearArray(this.topicTreeData);
//       this.$util.clearArray(this.questionIndexObj.TickedEducationTreeIds);
//       this.$util.clearArray(this.questionIndexObj.TickedTopicsIds);
//       this.questionIndexObj.LessonId = 0;

//       this.fillEducationTreeByGradeIdStore(val).then(treeData => {
//         self.educationTreeData = [treeData];
//         setTimeout(() => {
//           self.$refs.educationTree.expandAll();
//         }, 300);
//       });
//     },
//     lessonDdlChange(val) {
//       this.questionIndexObj.LessonId = val;
//       this.questionIndexObj.TickedTopicsIds = [];
//       this.fillTopicTreeStore(val);
//       if (this.showWithoutTopic) {
//         this.fillGrid();
//       }
//     },
//     fillGrid() {
//       this.fillGridStore({
//         lessonId: this.questionIndexObj.LessonId,
//         topicsIds: this.questionIndexObj.TickedTopicsIds,
//         showNoJudgement: this.showNoJudgement,
//         showWithoutTopic: this.showWithoutTopic
//       });
//     }
//   },
//   computed: {
//     ...mapState("questionStore", {
//       modelName: "modelName",
//       questionGridData: "questionGridData",
//       questionIndexObj: "questionIndexObj"
//     }),
//     ...mapState("educationTreeStore", {
//       educationTree_GradeDdl: "gradeDdl"
//     }),
//     ...mapState("lessonStore", {
//       lessonDdl: "allObjDdl"
//     }),
//     ...mapState("topicStore", {
//       topicTreeData: "topicTreeData"
//     })
//   },
//   validations: viewModel,
//   watch: {
//     "questionIndexObj.TickedEducationTreeIds"(val) {
//       this.questionIndexObj.LessonId = 0;
//       this.$util.clearArray(this.questionIndexObj.TickedTopicsIds);
//       this.$util.clearArray(this.topicTreeData);
//       this.fillLessonDdlStore(val);
//     },
//     "questionIndexObj.TickedTopicsIds"(newVal, oldVal) {
//       if (this.questionIndexObj.TickedTopicsIds.length > 0) {
//         this.fillGrid();
//       } else {
//         this.$util.clearArray(this.questionGridData);
//       }
//     }
//   },
//   created() {
//     this.getAllGrade();
//     this.fillEducationTreeStore();
//   }
// };
</script>

