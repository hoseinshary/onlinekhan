<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{roleStore.modelName}}</span>
      <div slot="body">
        <base-btn-create :label="`ایجاد (${roleStore.modelName}) جدید`" @click="showModalCreate"/>
        <br>
        <base-table :grid-data="roleStore.gridData" :columns="roleGridColumn" hasIndex>
          <q-btn
            slot="role"
            slot-scope="data"
            outline
            round
            icon="list"
            color="brown"
            size="sm"
            class="shadow-1 bg-white q-mr-sm"
            @click="showModalAccess(data.row.Id)"
          >
            <q-tooltip>انتساب نقش</q-tooltip>
          </q-btn>
          <template slot="Id" slot-scope="data">
            <base-btn-edit round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create></modal-create>
    <modal-edit></modal-edit>
    <modal-delete></modal-delete>
    <modal-access></modal-access>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue"),
    ModalAccess: () => import("./access.vue")
  }
})
export default class RoleVue extends Vue {
  //### data ###
  roleStore = vxm.roleStore;
  roleGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "سطح نقش",
      data: "Level"
    },
    {
      title: "نوع کاربری",
      data: "UserTypeName"
    },
    {
      title: "انتساب نقش",
      data: "role",
      searchable: false,
      sortable: false
      // visible: pageAccess.canAccess
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false
      // visible: pageAccess.canEdit || pageAccess.canDelete
    }
  ];
  //--------------------------------------------------

  //### methods ###
  showModalCreate() {
    this.roleStore.resetCreate();
    this.roleStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.roleStore.resetEdit();
    this.roleStore.getById(id).then(() => {
      this.roleStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.roleStore.getById(id).then(() => {
      this.roleStore.OPEN_MODAL_DELETE(true);
    });
  }

  showModalAccess(id, name) {
    this.roleStore.getById(id).then(() => {
      vxm.accessStore.OPEN_MODAL_ACCESS(true);
      vxm.accessStore.SET_ROLE_ID(id);
    });
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.roleStore.fillList();
  }
  //--------------------------------------------------
}
// export default {
//   components: {
//     "modal-create": () => import("./create"),
//     "modal-edit": () => import("./edit"),
//     "modal-delete": () => import("./delete"),
//     "modal-access": () => import("./access")
//   },
//   /**
//    * data
//    */
//   data() {
//     var pageAccess = this.$util.initAccess("/role");
//     return {
//       pageAccess,
//       gridColumns: [
//         {
//           title: "نام",
//           data: "Name"
//         },
//         {
//           title: "سطح نقش",
//           data: "Level"
//         },
//         {
//           title: "سطح نقش",
//           data: "UserTypeName"
//         },
//         {
//           title: "انتساب نقش",
//           data: "role",
//           searchable: false,
//           sortable: false,
//           visible: pageAccess.canAccess
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
//     ...mapActions("roleStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore",
//       "toggleModalAccessStore",
//       "setRoleIdStore",
//       "setRoleNameStore"
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
//     },
//     showModalAccess(id, name) {
//       this.setRoleIdStore(id);
//       this.setRoleNameStore(name);
//       this.toggleModalAccessStore(true);
//     }
//   },
//   computed: {
//     ...mapState("roleStore", {
//       modelName: "modelName",
//       roleGridData: "roleGridData"
//     })
//   },
//   created() {
//     this.fillGridStore();
//   }
// };
</script>

