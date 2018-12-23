
<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-select
                :model="$v.questionObj.EducationTreeId_Grade"
                :options="educationTree_GradeDdl"
                @change="gradeDdlChange"
              />
              <!-- <q-select
                multiple
                chips
                filter
                v-model="tmpObj"
                :options="educationTree_GradeDdl"
                float-label="Some label"
              /> -->
              <q-tree
                :nodes="educationTreeData"
                color="primary"
                tick-strategy="leaf"
                accordion
                node-key="Id"
                :ticked.sync="questionObj.EducationTreeIds"
                ref="educationTree"
              />
              <my-select
                :model="$v.questionObj.LessonId"
                :options="lessonDdl"
                class="q-pt-lg"
                @change="lessonDdlChange"
              />
            </section>
          </div>
          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <!-- <my-btn-create
                v-if="pageAccess.canCreate && lessonDdl.length && !topicTreeData.length"
                :label="`ایجاد (${modelName}) جدید`"
                @click="showModalCreate"
              />-->

              <q-tree
                :nodes="topicTreeData"
                tick-strategy="leaf"
                class="q-pt-lg"
                color="primary"
                accordion
                node-key="Id"
                ref="topicTree"
                :ticked.sync="questionObj.IndexTopicsId"
              />
            </section>
          </div>
          <br>
        </section>
        <my-btn-create
          v-if="pageAccess.canCreate"
          :label="`ایجاد (${modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <my-table :grid-data="questionGridData" :columns="questionGridColumn" hasIndex>
          <template slot="Id" slot-scope="data">
            <my-btn-edit v-if="pageAccess.canEdit" round @click="showModalEdit(data.row.Id)"/>
            <my-btn-delete v-if="pageAccess.canDelete" round @click="showModalDelete(data.row.Id)"/>
          </template>
        </my-table>
      </div>
    </my-panel>
    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
  import { mapState, mapActions } from "vuex";
  import viewModel from "viewModels/questionViewModel";

  export default {
    components: {
      "modal-create": () => import("./create"),
      "modal-edit": () => import("./edit"),
      "modal-delete": () => import("./delete")
    },
    /**
     * data
     */
    data() {
      var pageAccess = this.$util.initAccess("/question");
      return {
        pageAccess,
        educationTreeData: [],
        tmpObj:[],
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
      ...mapActions("topicStore", ["fillTreeStore"]),
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
      gradeDdlChange(val) {
        // filter lesson tree by gradeId
        var self = this;
        this.questionObj.EducationTreeIds = [];
        this.fillEducationTreeByGradeIdStore(val).then(treeData => {
          self.educationTreeData = [treeData];
          setTimeout(() => {
            self.$refs.educationTree.expandAll();
          }, 300);
        });
      },
      lessonDdlChange(val) {
        this.fillTreeStore(val);
        this.questionObj.IndexTopicsId = [];
      }
    },
    computed: {
      ...mapState("questionStore", {
        modelName: "modelName",
        questionGridData: "questionGridData",
        questionObj: "questionObj"
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
      "questionObj.EducationTreeIds"(val) {
        this.fillLessonDdlStore(val);
        this.questionObj.IndexTopicsId = [];
      },
      "questionObj.IndexTopicsId"(newVal, oldVal) {
        if(this.questionObj.IndexTopicsId.length > 0)
        this.fillGridStore();
      }
    },
    created() {
      // this.fillGridStore();
      this.getAllGrade();
      this.fillEducationTreeStore();
      this.questionObj.IndexTopicsId=[3351,3352]
    }
  };
</script>

