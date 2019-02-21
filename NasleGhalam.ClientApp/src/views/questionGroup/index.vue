<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-select :model="$v.questionGroupIndexObj.EducationTreeId_Grade"
                         :options="educationTree_GradeDdl"
                         @change="gradeDdlChange" />

              <q-tree :nodes="educationTreeData"
                      style="max-height: 200px; overflow-y:scroll;"
                      color="primary"
                      tick-strategy="leaf"
                      accordion
                      node-key="Id"
                      :ticked.sync="questionGroupIndexObj.EducationTreeIds"
                      ref="educationTree" />

              <my-select :model="$v.questionGroupIndexObj.LessonId"
                         :options="lessonDdl"
                         class="q-pt-lg"
                         @change="lessonDdlChange" />
            </section>
          </div>
          <div class="col-sm-7">
            <my-btn-create v-if="pageAccess.canCreate && questionGroupIndexObj.LessonId>0"
                           :label="`ایجاد (${modelName}) جدید`"
                           @click="showModalCreate" />
            <br>
            <my-table :grid-data="questionGroupGridData"
                      :columns="questionGroupGridColumn"
                      hasIndex>
              <template slot="Id"
                        slot-scope="data">
                <q-btn outline
                       color="primary"
                       class="shadow-1 bg-white q-mr-sm"
                       @click="showModalQuestion(data.row.Id)">
                  سوال ها
                </q-btn>
                <!-- <my-btn-delete v-if="pageAccess.canDelete"
                               round
                               @click="showModalDelete(data.row.Id)" /> -->
              </template>
            </my-table>
          </div>
        </section>
      </div>
    </my-panel>

    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-question></modal-question>
    <!-- <modal-delete v-if="pageAccess.canDelete"></modal-delete> -->
  </section>
</template>

<script>
import { mapState, mapActions } from "vuex";
import viewModel from "viewModels/questionGroup/questionGroupIndexViewModel";

export default {
  components: {
    "modal-create": () => import("./create"),
    "modal-question": () => import("../question/questions"),
    "modal-delete": () => import("./delete")
  },
  /**
   * data
   */
  data() {
    var pageAccess = this.$util.initAccess("/questionGroup");
    return {
      pageAccess,
      educationTreeData: [],
      questionGroupGridColumn: [
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
          sortable: false
          // visible: pageAccess.canViewQuestion //|| pageAccess.canDelete
        }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions("questionGroupStore", [
      "toggleModalCreateStore",
      "toggleModalDeleteStore",
      "getByIdStore",
      "fillGridByLessonIdStore",
      "resetCreateStore"
    ]),
    ...mapActions("questionStore", [
      "getByQuestionGroupIdStore",
      "toggleModalQuestionStore",
      "resetQuestionStore"
    ]),
    ...mapActions("educationTreeStore", {
      getAllGrade: "getAllGrade",
      fillEducationTreeStore: "fillTreeStore",
      fillEducationTreeByGradeIdStore: "fillTreeByGradeIdStore"
    }),
    ...mapActions("lessonStore", {
      fillLessonDdlStore: "fillDdlStore"
    }),
    showModalCreate() {
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
    },
    showModalQuestion(id) {
      // reset data on modal show
      this.resetQuestionStore();
      // get data by id

      this.getByQuestionGroupIdStore(id).then(() => {
        // show modal
        this.toggleModalQuestionStore(true);
      });
    },
    showModalDelete(id) {
      // get data by id
      this.getByIdStore(id).then(() => {
        // show modal
        this.toggleModalDeleteStore(true);
      });
    },
    gradeDdlChange(val) {
      // filter lesson tree by gradeId
      var self = this;
      this.questionGroupIndexObj.LessonId = 0;
      this.questionGroupIndexObj.EducationTreeIds = [];
      this.fillEducationTreeByGradeIdStore(val).then(treeData => {
        self.educationTreeData = [treeData];
        setTimeout(() => {
          self.$refs.educationTree.expandAll();
        }, 300);
      });
    },
    lessonDdlChange(val) {
      this.questionGroupObj.LessonId = val;
      this.fillGridByLessonIdStore(val);
    }
  },
  computed: {
    ...mapState("questionGroupStore", {
      modelName: "modelName",
      questionGroupObj: "questionGroupObj",
      questionGroupIndexObj: "questionGroupIndexObj",
      questionGroupGridData: "questionGroupGridData"
    }),
    ...mapState("educationTreeStore", {
      educationTree_GradeDdl: "gradeDdl"
    }),
    ...mapState("lessonStore", {
      lessonDdl: "allObjDdl"
    })
  },
  watch: {
    "questionGroupIndexObj.EducationTreeIds"(val) {
      this.questionGroupIndexObj.LessonId = 0;
      this.fillLessonDdlStore(val);
    },
    "questionGroupIndexObj.selectedquestionGroupId"(newVal, oldVal) {
      let node;
      if (newVal && oldVal) {
        node = this.$refs.questionGroupTree.getNodeByKey(newVal);
        if (node) node.visible = true;
        node = this.$refs.questionGroupTree.getNodeByKey(oldVal);
        if (node) node.visible = false;
      } else if (newVal) {
        node = this.$refs.questionGroupTree.getNodeByKey(newVal);
        if (node) node.visible = true;
      } else if (oldVal) {
        node = this.$refs.questionGroupTree.getNodeByKey(oldVal);
        if (node) node.visible = false;
      }
      this.questionGroupObj.ParentquestionGroupId = newVal;
    }
  },
  validations: viewModel,
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

