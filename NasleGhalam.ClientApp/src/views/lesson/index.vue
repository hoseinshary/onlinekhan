<template>
  <section class="col-md-12 q-px-lg">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{lessonStore.modelName}}</span>
      <div slot="body" class="row gutter-sm">
        <div class="col-md-4">
          <q-select
            v-model="educationTreeId"
            :options="educationTree_GradeDdl"
            float-label="فیلتر درخت آموزش با مقطع"
            clearable
          />
          <q-tree
            :nodes="educationTreeData"
            :expanded.sync="expanded"
            :ticked.sync="tickedEducationTreeIds"
            tick-strategy="leaf"
            color="blue"
            node-key="Id"
          />
        </div>
        <div class="col-md-8">
          <base-btn-create
            v-if="canCreate"
            :label="`ایجاد (${lessonStore.modelName}) جدید`"
            @click="showModalCreate"
          />
          <br>
          <base-table :grid-data="lessonStore.gridData" :columns="lessonGridColumn" hasIndex>
            <template slot="Id" slot-scope="data">
              <base-btn-edit round v-if="canEdit" @click="showModalEdit(data.row.Id)"/>
              <base-btn-delete round v-if="canDelete" @click="showModalDelete(data.row.Id)"/>
            </template>
          </base-table>
        </div>
      </div>
    </base-panel>
    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :expandedTreeIdsProp="expanded"
      :leafTickedEducationTreeIdsProp="tickedEducationTreeIds"
    ></modal-create>
    <modal-edit v-if="canEdit" :expandedTreeIdsProp="expanded"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class LessonVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  educationTreeStore = vxm.educationTreeStore;
  pageAccess = util.getAccess(this.lessonStore.modelName);
  lessonGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
  educationTreeId = null;
  expanded: Array<Object> = [];
  tickedEducationTreeIds: Array<number> = [];
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
    this.tickedEducationTreeIds.splice(0, this.tickedEducationTreeIds.length);

    let index = this.expanded.indexOf(oldVal);
    if (index > -1) {
      this.expanded.splice(index, 1);
    }

    if (this.expanded.indexOf(newVal) == -1) {
      this.expanded.push(newVal);
    }
  }

  @Watch("tickedEducationTreeIds")
  tickedEducationTreeIdsChanged(newVal, oldVal) {
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.lessonStore.resetCreate();
    this.lessonStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.lessonStore.resetEdit();
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    var _this = this;
    this.educationTreeStore.fillList().then(function(res) {
      _this.expanded = _this.educationTreeStore.expandedTreeData;
    });
  }
  //#endregion
}
// import viewModel from "viewModels/lessonViewModel";
// import { mapState, mapActions } from "vuex";

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
//     var pageAccess = this.$util.initAccess("/lesson");
//     return {
//       pageAccess,
//       selectedNodeIds: null,
//       gridColumns: [
//         {
//           title: "نام",
//           data: "Name"
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
//     ...mapActions("lessonStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore"
//     ]),
//     ...mapActions("educationTreeStore", [
//       "getAllEducationTreeByState",
//       "fillTreeStore",
//       "getAllGrade",
//       "changeEducationTree"
//     ]),
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
//     }
//   },
//   computed: {
//     ...mapState("lessonStore", {
//       modelName: "modelName",
//       instanceObj: "instanceObj",
//       gradeDdl: "gradeDdl",
//       allObj: "allObj"
//     }),
//     ...mapState("educationTreeStore", {
//       educationTreeDdl: "educationTreeDdl",
//       gradeDdl: "gradeDdl",
//       educationTreeData: "educationTreeData"
//     })
//   },
//   validations: viewModel,
//   created() {
//     this.fillTreeStore();
//     // this.fillGridStore();
//     this.getAllGrade();
//   },
//   watch: {
//     "instanceObj.GradeId"(newVal) {
//       this.changeEducationTree(newVal);
//     },
//     selectedNodeIds(newVal) {
//       this.fillGridStore(newVal);
//     }
//   }
// };
</script>

