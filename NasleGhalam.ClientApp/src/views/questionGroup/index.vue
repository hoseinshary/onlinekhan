<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{questionGroupStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
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
          <div class="col-sm-7">
            <base-btn-create
              v-if="pageAccess.canCreate && questionGroupIndexObj.LessonId>0"
              :label="`ایجاد (${questionGroupStore.modelName}) جدید`"
              @click="showModalCreate"
            />
            <br>
            <base-table
              :grid-data="questionGroupStore.gridData"
              :columns="questionGroupGridColumn"
              hasIndex
            >
              <template slot="Id" slot-scope="data">
                <q-btn
                  outline
                  dense
                  color="primary"
                  class="shadow-1 bg-white q-mr-sm"
                  @click="showModalQuestion(data.row.Id)"
                >سوال ها</q-btn>

                <a :href="data.row.QuestionGroupWordPath" target="_blank" class="q-mr-sm">فایل ورد</a>
                <a :href="data.row.QuestionGroupExcelPath" target="_blank" class="q-mr-sm">فایل اکسل</a>
                <!-- <base-btn-delete v-if="pageAccess.canDelete"
                               round
                @click="showModalDelete(data.row.Id)" />-->
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </base-panel>

    <!-- modals -->
    <!-- <modal-create v-if="pageAccess.canCreate"></modal-create> -->
    <!-- <modal-question></modal-question> -->
    <!-- <modal-delete v-if="pageAccess.canDelete"></modal-delete> -->
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
export default class QuestionGroupVue extends Vue {
  //#region ### data ###
  questionGroupStore = vxm.questionGroupStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  pageAccess = util.getAccess(this.questionGroupStore.modelName);
  questionGroupGridColumn = [
    {
      title: "عنوان",
      data: "Title"
    },
    {
      title: "زمان ثبت",
      data: "PInsertTime"
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
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.questionGroupStore.resetCreate();
    this.questionGroupStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.questionGroupStore.resetEdit();
    this.questionGroupStore.getById(id).then(() => {
      this.questionGroupStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.questionGroupStore.getById(id).then(() => {
      this.questionGroupStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    this.lessonId = val;
    this.questionGroupStore.fillListByLessonId(val);
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
// import { mapState, mapActions, mapGetters } from "vuex";
// import viewModel from "viewModels/questionGroup/questionGroupIndexViewModel";

// export default {
//   components: {
//     "modal-create": () => import("./create"),
//     "modal-question": () => import("../question/questions"),
//     "modal-delete": () => import("./delete")
//   },
//   /**
//    * data
//    */
//   data() {
//     var pageAccess = this.$util.initAccess("/questionGroup");
//     return {
//       pageAccess,
//       educationTreeData: [],
//       questionGroupGridColumn: [
//         {
//           title: "عنوان",
//           data: "Title"
//         },
//         {
//           title: "زمان ثبت",
//           data: "PInsertTime"
//         },
//         {
//           title: "عملیات",
//           data: "Id",
//           searchable: false,
//           sortable: false
//           // visible: pageAccess.canViewQuestion //|| pageAccess.canDelete
//         }
//       ]
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("questionGroupStore", [
//       "toggleModalCreateStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridByLessonIdStore",
//       "resetCreateStore"
//     ]),
//     ...mapActions("questionStore", [
//       "getByQuestionGroupIdStore",
//       "toggleModalQuestionStore",
//       "resetQuestionStore"
//     ]),
//     ...mapActions("educationTreeStore", {
//       getAllGrade: "getAllGrade",
//       fillEducationTreeStore: "fillTreeStore",
//       fillEducationTreeByGradeIdStore: "fillTreeByGradeIdStore"
//     }),
//     ...mapActions("lessonStore", {
//       fillLessonDdlStore: "fillDdlStore"
//     }),
//     showModalCreate() {
//       // reset data on modal show
//       this.resetCreateStore();
//       // show modal
//       this.toggleModalCreateStore(true);
//     },
//     showModalQuestion(id) {
//       // reset data on modal show
//       this.resetQuestionStore();
//       // get data by id

//       this.getByQuestionGroupIdStore(id).then(() => {
//         // show modal
//         this.toggleModalQuestionStore(true);
//       });
//     },
//     showModalDelete(id) {
//       // get data by id
//       this.getByIdStore(id).then(() => {
//         // show modal
//         this.toggleModalDeleteStore(true);
//       });
//     },
//     gradeDdlChange(val) {
//       // filter lesson tree by gradeId
//       var self = this;
//       this.questionGroupIndexObj.LessonId = 0;
//       this.$util.clearArray(this.questionGroupIndexObj.EducationTreeIds);
//       this.fillEducationTreeByGradeIdStore(val).then(treeData => {
//         self.educationTreeData = [treeData];
//         setTimeout(() => {
//           self.$refs.educationTree.expandAll();
//         }, 300);
//       });
//     },
//     lessonDdlChange(val) {
//       this.questionGroupObj.LessonId = val;
//       this.fillGridByLessonIdStore(val);
//     }
//   },
//   computed: {
//     ...mapState("questionGroupStore", {
//       questionGroupStore.modelName: "questionGroupStore.modelName",
//       questionGroupObj: "questionGroupObj",
//       questionGroupIndexObj: "questionGroupIndexObj",
//       questionGroupGridData: "questionGroupGridData"
//     }),
//     ...mapState("educationTreeStore", {
//       educationTree_GradeDdl: "gradeDdl"
//     }),
//     ...mapState("lessonStore", {
//       lessonDdl: "allObjDdl"
//     })
//   },
//   watch: {
//     "questionGroupIndexObj.EducationTreeIds"(val) {
//       this.$util.clearArray(this.questionGroupGridData);
//       this.questionGroupIndexObj.LessonId = 0;
//       this.fillLessonDdlStore(val);
//     }
//   },
//   validations: viewModel,
//   created() {
//     this.getAllGrade();
//     this.fillEducationTreeStore();
//   }
// };
</script>

