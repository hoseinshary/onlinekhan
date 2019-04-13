<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{topicStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-select
                v-model="educationTreeId"
                :options="educationTree_GradeDdl"
                float-label="فیلتر درخت آموزش با مقطع"
                clearable
              />
              <q-tree
                :nodes="educationTreeData"
                :expanded.sync="expandedEducationTreeIds"
                :ticked.sync="tickedEducationTreeIds"
                tick-strategy="leaf"
                color="blue"
                node-key="Id"
              />
              <q-select v-model="topic.LessonId" :options="lessonStore.ddl" class="q-pt-lg"/>
            </section>
          </div>
          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <base-btn-create
                v-if="pageAccess.canCreate && lessonDdl.length && !topicTreeData.length && topicIndexObj.LessonId != 0"
                :label="`ایجاد (${modelName}) جدید`"
                @click="showModalCreate"
              />
              <!-- :selected.sync="topicIndexObj.selectedTopicId" -->
              <q-input v-model="topicTreeFilter" float-label="جستجو در مبحث" clearable/>
              <q-tree
                :nodes="topicStore.treeDataByLessonId"
                :expanded.sync="expandedTopicTreeIds"
                :filter="topicTreeFilter"
                class="q-pt-lg"
                color="primary"
                accordion
                node-key="Id"
              >
                <div slot="header-custom" slot-scope="prop">
                  {{prop.node.label}}
                  <template>
                    <q-btn
                      v-if="canCreate"
                      outline
                      color="positive"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="save"
                      size="sm"
                      @click.stop="showModalCreate"
                    />
                    <q-btn
                      v-if="canEdit"
                      outline
                      color="purple"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="create"
                      size="sm"
                      @click.stop="showModalEdit(prop.node.Id)"
                    />
                    <q-btn
                      v-if="canDelete && !(prop.node.children && prop.node.children.length)"
                      outline
                      color="red"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="delete"
                      size="sm"
                      @click.stop="showModalDelete(prop.node.Id)"
                    />
                  </template>
                </div>
              </q-tree>
            </section>
          </div>
          <br>
        </section>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <!-- <modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>-->
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {
    ModalCreate: () => import("./create.vue")
    // ModalEdit: () => import("./edit.vue"),
    // ModalDelete: () => import("./delete.vue")
  }
})
export default class TopicVue extends Vue {
  //#region ### data ###
  topicStore = vxm.topicStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  topic = this.topicStore.topic;
  pageAccess = util.getAccess(this.topicStore.modelName);
  topicTreeFilter = "";
  educationTreeId = null;
  expandedEducationTreeIds: Array<Object> = [];
  tickedEducationTreeIds: Array<number> = [];
  expandedTopicTreeIds: Array<Object> = [];

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
    if (!this.educationTreeId) {
      return this.educationTreeStore.treeData;
    } else {
      return this.educationTreeStore.treeData[0].children.filter(
        x => x.Id == this.educationTreeId
      );
    }
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTreeId")
  educationTreeIdChanged(newVal, oldVal) {
    this.topic.LessonId = 0;
    this.tickedEducationTreeIds.splice(0, this.tickedEducationTreeIds.length);

    let index = this.expandedEducationTreeIds.indexOf(oldVal);
    if (index > -1) {
      this.expandedEducationTreeIds.splice(index, 1);
    }

    if (this.expandedEducationTreeIds.indexOf(newVal) == -1) {
      this.expandedEducationTreeIds.push(newVal);
    }
  }

  @Watch("tickedEducationTreeIds")
  tickedEducationTreeIdsChanged(newVal) {
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.topicStore.resetCreate();
    this.topicStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.topicStore.resetEdit();
    this.topicStore.getById(id).then(() => {
      this.topicStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.topicStore.getById(id).then(() => {
      this.topicStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.topicStore.fillList().then(res => {
      this.expandedTopicTreeIds = this.topicStore.expandedTreeData;
    });
    this.educationTreeStore.fillList().then(res => {
      this.expandedEducationTreeIds = this.educationTreeStore.expandedTreeData;
    });
  }
  //#endregion
}
// import { mapState, mapActions } from "vuex";
// import viewModel from "viewModels/topic/topicIndexViewModel";

// export default {
//   components: {
//     "modal-create": () => import("./create"),
//     "modal-edit": () => import("./edit"),
//     "modal-delete": () => import("./delete")
//   },
//   /**
//    * data
//    */
//   data() {
//     var pageAccess = this.$util.initAccess("/topic");
//     return {
//       pageAccess,
//       educationTreeData: []
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("topicStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillTreeStore",
//       "resetCreateStore",
//       "resetEditStore",
//       "clearTreeStore"
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
//     gradeDdlChange(val) {
//       // filter lesson tree by gradeId
//       var self = this;
//       this.topicIndexObj.LessonId = 0;
//       this.clearTreeStore();
//       this.topicIndexObj.EducationTreeIds = [];
//       this.fillEducationTreeByGradeIdStore(val).then(treeData => {
//         self.educationTreeData = [treeData];
//         setTimeout(() => {
//           self.$refs.educationTree.expandAll();
//         }, 300);
//       });
//     },
//     lessonDdlChange(val) {
//       this.fillTreeStore(val);
//       this.topicObj.LessonId = val;
//     }
//   },
//   computed: {
//     ...mapState("topicStore", {
//       modelName: "modelName",
//       topicTreeData: "topicTreeData",
//       topicIndexObj: "topicIndexObj",
//       topicObj: "topicObj"
//     }),
//     ...mapState("educationTreeStore", {
//       educationTree_GradeDdl: "gradeDdl"
//     }),
//     ...mapState("lessonStore", {
//       lessonDdl: "allObjDdl"
//     })
//   },
//   validations: viewModel,
//   watch: {
//     "topicIndexObj.EducationTreeIds"(val) {
//       this.clearTreeStore();
//       this.topicIndexObj.LessonId = 0;
//       this.fillLessonDdlStore(val);
//     },
//     "topicIndexObj.selectedTopicId"(newVal, oldVal) {
//       let node;
//       if (newVal && oldVal) {
//         node = this.$refs.topicTree.getNodeByKey(newVal);
//         if (node) node.visible = true;
//         node = this.$refs.topicTree.getNodeByKey(oldVal);
//         if (node) node.visible = false;
//       } else if (newVal) {
//         node = this.$refs.topicTree.getNodeByKey(newVal);
//         if (node) node.visible = true;
//       } else if (oldVal) {
//         node = this.$refs.topicTree.getNodeByKey(oldVal);
//         if (node) node.visible = false;
//       }
//       this.topicObj.ParentTopicId = newVal;
//     }
//   },
//   created() {
//     this.getAllGrade();
//     this.fillEducationTreeStore();
//   }
// };
</script>

