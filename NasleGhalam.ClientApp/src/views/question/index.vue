<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-12">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-checkbox v-model="showNoJudgement"
                          label="نمایش سوال های بدون ارزیابی" />
              <br>
              <q-checkbox v-model="showWithoutTopic"
                          label="نمایش سوال های بدون مبحث" />
            </section>
          </div>
          <div class="col-sm-5 ">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-select :model="$v.questionIndexObj.EducationTreeId_Grade"
                         :options="educationTree_GradeDdl"
                         @change="gradeDdlChange" />

              <q-field class="col-12">
                <q-input v-model="educationTreeFilter"
                         float-label="جستجو در درخت آموزش"
                         clearable />
              </q-field>
              <q-tree :nodes="educationTreeData"
                      style="max-height: 200px; overflow-y:scroll;"
                      color="primary"
                      tick-strategy="leaf"
                      accordion
                      node-key="Id"
                      :ticked.sync="questionIndexObj.TickedEducationTreeIds"
                      :filter="educationTreeFilter"
                      ref="educationTree" />
              <my-select :model="$v.questionIndexObj.LessonId"
                         :options="lessonDdl"
                         class="q-pt-lg"
                         @change="lessonDdlChange" />
            </section>
          </div>
          <div class="col-sm-7 "
               v-if="!showWithoutTopic">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-field class="col-12">
                <q-input v-model="topicFilter"
                         float-label="جستجوی مبحث"
                         clearable />
              </q-field>
              <q-tree :nodes="topicTreeData"
                      style="max-height: 300px; overflow-y:scroll;"
                      tick-strategy="leaf"
                      class="q-pt-lg"
                      color="primary"
                      accordion
                      node-key="Id"
                      ref="topicTree"
                      :filter="topicFilter"
                      :ticked.sync="questionIndexObj.TickedTopicsIds" />
            </section>
          </div>
          <br>
        </section>
        <my-btn-create v-if="pageAccess.canCreate"
                       :label="`ایجاد (${modelName}) جدید`"
                       @click="showModalCreate" />
        <br>
        <my-table :grid-data="questionGridData"
                  :columns="questionGridColumn"
                  hasIndex>
          <template slot="Id"
                    slot-scope="data">
            <q-btn outline
                   round
                   icon="list"
                   color="brown"
                   size="sm"
                   class="shadow-1 bg-white q-mr-sm"
                   @click="showModalQuestionJudge(data.row.Id)">
              <q-tooltip>
                ارزیابی
              </q-tooltip>
            </q-btn>
            <my-btn-edit v-if="pageAccess.canEdit"
                         round
                         @click="showModalEdit(data.row.Id)" />
            <my-btn-delete v-if="pageAccess.canDelete"
                           round
                           @click="showModalDelete(data.row.Id)" />
          </template>
        </my-table>
      </div>
    </my-panel>
    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
    <modal-question-judge></modal-question-judge>
  </section>
</template>

<script>
import { mapState, mapActions } from "vuex";
import viewModel from "viewModels/question/questionIndexViewModel";

export default {
  components: {
    "modal-create": () => import("./create"),
    "modal-edit": () => import("./edit"),
    "modal-delete": () => import("./delete"),
    "modal-question-judge": () => import("../questionJudge/index")
  },
  /**
   * data
   */
  data() {
    var pageAccess = this.$util.initAccess("/question");
    return {
      pageAccess,
      educationTreeData: [],
      topicFilter: "",
      educationTreeFilter: "",
      showNoJudgement: false,
      showWithoutTopic: false,
      questionGridColumn: [
        {
          title: "متن سوال",
          data: "Context"
        },

        {
          title: "عملیات",
          data: "Id",
          searchable: false,
          sortable: false,
          visible: pageAccess.canEdit || pageAccess.canDelete
        }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions("questionStore", [
      "toggleModalCreateStore",
      "toggleModalEditStore",
      "toggleModalDeleteStore",
      "getByIdStore",
      "fillGridStore",
      "resetCreateStore",
      "resetEditStore"
    ]),
    ...mapActions("topicStore", {
      fillTopicTreeStore: "fillTreeStore"
    }),
    ...mapActions("educationTreeStore", {
      getAllGrade: "getAllGrade",
      fillEducationTreeStore: "fillTreeStore",
      fillEducationTreeByGradeIdStore: "fillTreeByGradeIdStore"
    }),
    ...mapActions("lessonStore", {
      fillLessonDdlStore: "fillDdlStore"
    }),
    ...mapActions("questionJudgeStore", {
      toggleModalQuestionJudgeStore: "toggleModalStore",
      fillGridQuestionJudgeByQuestionId: "fillGridStore"
    }),
    showModalCreate() {
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
    },
    showModalEdit(id) {
      // reset data on modal show
      this.resetEditStore();
      // get data by id
      this.getByIdStore(id).then(() => {
        // show modal
        this.toggleModalEditStore(true);
      });
    },
    showModalDelete(id) {
      // get data by id
      this.getByIdStore(id).then(() => {
        // show modal
        this.toggleModalDeleteStore(true);
      });
    },
    showModalQuestionJudge(id) {
      // get data by id
      this.fillGridQuestionJudgeByQuestionId(id).then(() => {
        // show modal
        this.toggleModalQuestionJudgeStore({ isOpen: true, questionId: id });
      });
    },
    gradeDdlChange(val) {
      // filter lesson tree by gradeId
      var self = this;
      this.$util.clearArray(this.educationTreeData);
      this.$util.clearArray(this.topicTreeData);
      this.$util.clearArray(this.questionIndexObj.TickedEducationTreeIds);
      this.$util.clearArray(this.questionIndexObj.TickedTopicsIds);
      this.questionIndexObj.LessonId = 0;
      this.fillEducationTreeByGradeIdStore(val).then(treeData => {
        self.educationTreeData = [treeData];
        setTimeout(() => {
          self.$refs.educationTree.expandAll();
        }, 300);
      });
    },
    lessonDdlChange(val) {
      this.questionIndexObj.TickedTopicsIds = [];
      if (this.showWithoutTopic) {
        this.fillGridStore({
          showWithoutTopic: this.showWithoutTopic,
          lessonId: val
        });
      } else {
        this.fillTopicTreeStore(val);
      }
    }
  },
  computed: {
    ...mapState("questionStore", {
      modelName: "modelName",
      questionGridData: "questionGridData",
      questionIndexObj: "questionIndexObj"
    }),
    ...mapState("educationTreeStore", {
      educationTree_GradeDdl: "gradeDdl"
    }),
    ...mapState("lessonStore", {
      lessonDdl: "allObjDdl"
    }),
    ...mapState("topicStore", {
      topicTreeData: "topicTreeData"
    })
  },
  validations: viewModel,
  watch: {
    "questionIndexObj.TickedEducationTreeIds"(val) {
      this.questionIndexObj.LessonId = 0;
      this.$util.clearArray(this.questionIndexObj.TickedTopicsIds);
      this.$util.clearArray(this.topicTreeData);
      this.fillLessonDdlStore(val);
    },
    "questionIndexObj.TickedTopicsIds"(newVal, oldVal) {
      if (this.questionIndexObj.TickedTopicsIds.length > 0) {
        this.fillGridStore({
          topicsIds: this.questionIndexObj.TickedTopicsIds,
          showNoJudgement: this.showNoJudgement
        });
      } else {
        this.$util.clearArray(this.questionGridData);
      }
    }
  },
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

