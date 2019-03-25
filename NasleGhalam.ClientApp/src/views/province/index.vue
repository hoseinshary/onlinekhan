<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{provinceStore.modelName}}</span>
      <div slot="body">
        <base-btn-create
          :label="`ایجاد (${provinceStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="provinceStore.gridData" :columns="provinceGridColumn" hasIndex>
          <!-- <template slot="Id"
                    slot-scope="data">
            <base-btn-edit round
                         v-if="pageAccess.canEdit"
                         @click="showModalEdit(data.row.Id)" />
            <base-btn-delete round
                           v-if="pageAccess.canDelete"
                           @click="showModalDelete(data.row.Id)" />
          </template>-->
        </base-table>
      </div>
    </base-panel>
    <!-- modals -->
    <modal-create></modal-create>
    <!-- <modal-edit></modal-edit>
    <modal-delete></modal-delete>-->
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";

@Component({
  components: {
    ModalCreate: () => import("./create.vue")
  }
})
export default class ProvinceVue extends Vue {
  //### data ###
  provinceStore = vxm.provinceStore;
  provinceGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "کد",
      data: "Code"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: false
    }
  ];
  //--------------------------------------------------

  //### computed ###
  // get gridData() {
  //   debugger;
  //   return this.provinceStore.gridData;
  // }
  //--------------------------------------------------

  //### methods ###
  showModalCreate() {
    this.provinceStore.resetCreate();
    this.provinceStore.TOGGLE_MODAL_CREATE(true);
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.provinceStore.getAll();
  }
  //--------------------------------------------------
}

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
//     var pageAccess = this.$util.initAccess("/province");
//     return {
//       pageAccess,
//       provinceGridColumn: [
//         {
//           title: "نام",
//           data: "Name"
//         },
//         {
//           title: "کد",
//           data: "Code"
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
//     ...mapActions("provinceStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore"
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
//     ...mapState("provinceStore", {
//       modelName: "modelName",
//       provinceGridData: "provinceGridData"
//     })
//   },
//   created() {
//     this.fillGridStore();
//   }
// };
</script>

