<template>
  <section class="col-md-12 q-px-lg">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{lessonStore.modelName}}</span>
      <div slot="body" class="row">
        <div class="col-md-4 row">
          <!-- <q-select
            v-model="$v.instanceObj.GradeId.$model"
            class="col-md-6 offset-md-3"
            clearable
            :options="gradeDdl"
          />
          <q-tree
            :nodes="educationTreeData"
            class="col-md-12"
            color="blue"
            :ticked.sync="selectedNodeIds"
            tick-strategy="leaf"
            node-key="Id"
            ref="topicTree"
          />-->
        </div>
        <div class="col-md-8">
          <base-btn-create
            v-if="pageAccess.canCreate"
            :label="`ایجاد (${lessonStore.modelName}) جدید`"
            @click="showModalCreate"
          />
          <br>
          <base-table
            :grid-data="lessonStore.gridData"
            :columns="lessonGridColumn"
            hasIndex
            class="col-md-8"
          >
            <template slot="Id" slot-scope="data">
              <base-btn-edit round v-if="canEdit" @click="showModalEdit(data.row.Id)"/>
              <base-btn-delete round v-if="canDelete" @click="showModalDelete(data.row.Id)"/>
            </template>
          </base-table>
        </div>
      </div>
    </base-panel>
    <!-- modals -->
    <!-- <modal-create></modal-create>
    <modal-edit></modal-edit>
    <modal-delete></modal-delete> -->
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    // ModalCreate: () => import("./create.vue"),
    // ModalEdit: () => import("./edit.vue"),
    // ModalDelete: () => import("./delete.vue")
  }
})
export default class LessonVue extends Vue {
  //### data ###
  lessonStore = vxm.lessonStore;
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
  //--------------------------------------------------

  //### getters ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }
  //--------------------------------------------------

  //### methods ###
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
  //--------------------------------------------------

  //### hooks ###
  created() {
    // this.lessonStore.fillList();
  }
  //--------------------------------------------------
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

