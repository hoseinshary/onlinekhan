<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{educationTreeStore.modelName}}</span>
      <div slot="body">
        <!-- <div style="min-height: 67px;">
          <q-slide-transition>
            <div class="col-md-12" v-show="selectedNodeId !=null">
              <div class="col-12">
                <base-btn-create
                  v-if="pageAccess.canCreate"
                  @click="showModalCreate"
                  :disabled="educationTreeData.length == 0 ? false : selectedNodeId == null"
                />
                <base-btn-edit
                  v-if="pageAccess.canEdit"
                  :disabled="selectedNodeId == null"
                  @click="showModalEdit"
                />
                <base-btn-delete
                  v-if="pageAccess.canDelete"
                  :disabled="selectedNodeId == null"
                  @click="showModalDelete"
                />
              </div>
            </div>
          </q-slide-transition>
        </div>-->
        <q-slide-transition>
          <q-tree
            :nodes="educationTreeStore.treeData"
            class="col-md-12"
            color="blue"
            default-expand-all
            node-key="Id"
            ref="topicTree"
          />
        </q-slide-transition>
      </div>
    </base-panel>

    <!-- modals -->
    <!-- <modal-create @close="selectedNodeId = null" v-if="pageAccess.canCreate"></modal-create>
    <modal-edit @close="selectedNodeId = null" v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete @close="selectedNodeId = null" v-if="pageAccess.canDelete"></modal-delete>-->
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
export default class EducationTreeVue extends Vue {
  //### data ###
  educationTreeStore = vxm.educationTreeStore;
  pageAccess = util.getAccess(this.educationTreeStore.modelName);
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
  // showModalCreate() {
  //   this.educationTreeStore.resetCreate();
  //   this.educationTreeStore.OPEN_MODAL_CREATE(true);
  // }

  // showModalEdit(id) {
  //   this.educationTreeStore.resetEdit();
  //   this.educationTreeStore.getById(id).then(() => {
  //     this.educationTreeStore.OPEN_MODAL_EDIT(true);
  //   });
  // }

  // showModalDelete(id) {
  //   this.educationTreeStore.getById(id).then(() => {
  //     this.educationTreeStore.OPEN_MODAL_DELETE(true);
  //   });
  // }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.educationTreeStore.fillList();
  }
  //--------------------------------------------------
}
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
//     var pageAccess = this.$util.initAccess("/educationTree");
//     return {
//       pageAccess,
//       selectedNodeId: null
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("educationTreeStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillTreeStore",
//       "resetCreateStore",
//       "resetEditStore"
//     ]),
//     showModalCreate() {
//       // reset data on modal show
//       this.resetCreateStore();
//       // show modal
//       this.toggleModalCreateStore(true);
//       this.educationTreeObj.ParentEducationTreeId = this.selectedNodeId;
//     },
//     showModalEdit() {
//       // reset data on modal show
//       this.resetEditStore();
//       // get data by id
//       this.getByIdStore(this.selectedNodeId).then(() => {
//         // show modal
//         this.toggleModalEditStore(true);
//       });
//     },
//     showModalDelete() {
//       // get data by id
//       this.getByIdStore(this.selectedNodeId).then(() => {
//         // show modal
//         this.toggleModalDeleteStore(true);
//       });
//     },
//     setSelectedNodeId(isOpen) {
//       if (!isOpen) this.selectedNodeId = null;
//     }
//   },
//   computed: {
//     ...mapState("educationTreeStore", {
//       modelName: "modelName",
//       educationTreeData: "educationTreeData",
//       educationTreeObj: "educationTreeObj",
//       isOpenModalCreate: "isOpenModalCreate",
//       isOpenModalEdit: "isOpenModalEdit",
//       isOpenModalDelete: "isOpenModalDelete"
//     })
//     // ,
//     // treeLst: function() {
//     //   return this.listToTree(this.educationTreeData);
//     // }
//   },
//   created() {
//     this.fillTreeStore();
//   },
//   watch: {
//     isOpenModalCreate(newVal) {
//       this.setSelectedNodeId(newVal);
//     },
//     isOpenModalEdit(newVal) {
//       this.setSelectedNodeId(newVal);
//     },
//     isOpenModalDelete(newVal) {
//       this.setSelectedNodeId(newVal);
//     }
//   }
// };
</script>

