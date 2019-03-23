<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{cityStore.modelName}}</span>
      <div slot="body">
        <base-btn-create :label="`ایجاد (${cityStore.modelName}) جدید`" @click="showModalCreate"/>
        <br>
        <!-- <base-table :grid-data="cityGridData" :columns="cityGridColumn" hasIndex>
          <template slot="Id" slot-scope="data">
            <base-btn-edit round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>-->
      </div>
    </base-panel>
    <!-- modals -->
    <modal-create></modal-create>
    <!-- <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>-->
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";

@Component({
  components: {
    ModalCreate: () => import("./create.vue")
  }
})
export default class CityVue extends Vue {
  //### datas ###
  cityStore = vxm.city;
  cityGridColumn: [
    {
      title: "استان";
      data: "ProvinceName";
    },
    {
      title: "نام شهر";
      data: "Name";
    },
    {
      title: "عملیات";
      data: "Id";
      searchable: false;
      sortable: false;
    }
  ];
  //--------------------------------------------------

  //### methods ###
  showModalCreate() {}
  //--------------------------------------------------

  //### hook functions ###
  created() {
    // this.cityStore.getAll();
  }
  //--------------------------------------------------
}
// export default {
//   // components: {
//   //   'modal-create': () => import('./create'),
//   //   'modal-edit': () => import('./edit'),
//   //   'modal-delete': () => import('./delete')
//   // },
//   /**
//    * data
//    */
//   data() {
//     var pageAccess = this.$util.initAccess('/city');
//     return {
//       pageAccess,
//       cityGridColumn: [
//         {
//           title: 'استان',
//           data: 'ProvinceName'
//         },
//         {
//           title: 'نام شهر',
//           data: 'Name'
//         },
//         {
//           title: 'عملیات',
//           data: 'Id',
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
//     ...mapActions('cityStore', [
//       'toggleModalCreateStore',
//       'toggleModalEditStore',
//       'toggleModalDeleteStore',
//       'getByIdStore',
//       'fillGridStore',
//       'resetCreateStore',
//       'resetEditStore'
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
//     ...mapState('cityStore', {
//       modelName: 'modelName',
//       cityGridData: 'cityGridData'
//     })
//   },
//   created() {
//     this.fillGridStore();
//   }
// };
</script>

