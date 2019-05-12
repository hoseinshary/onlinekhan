<template>
  <section class="col-xs-12 q-px-md">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{studentStore.modelName}}</span>
      <div slot="body">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${studentStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="studentStore.gridData" :columns="studentGridColumn" hasIndex>
          <template slot="User.Name" slot-scope="data">{{data.row.User.Name}}</template>
          <template slot="User.Family" slot-scope="data">{{data.row.User.Family}}</template>
          <template slot="User.GenderName" slot-scope="data">{{data.row.User.GenderName}}</template>
          <template slot="User.NationalNo" slot-scope="data">{{data.row.User.NationalNo}}</template>
          <template slot="User.Username" slot-scope="data">{{data.row.User.Username}}</template>
          <template slot="User.IsActive" slot-scope="data">
            <q-checkbox v-model="data.row.User.IsActive" readonly/>
          </template>
          <template slot="User.Phone" slot-scope="data">{{data.row.User.Phone}}</template>
          <template slot="User.Mobile" slot-scope="data">{{data.row.User.Mobile}}</template>
          <template slot="User.RoleName" slot-scope="data">{{data.row.User.RoleName}}</template>
          <template slot="User.CityName" slot-scope="data">{{data.row.User.CityName}}</template>
          <template slot="Id" slot-scope="data">
            <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class StudentVue extends Vue {
  //#region ### data ###
  studentStore = vxm.studentStore;
  pageAccess = util.getAccess(this.studentStore.modelName);
  studentGridColumn = [
    {
      title: "نام",
      data: "User.Name"
    },
    {
      title: "نام خانوادگی",
      data: "User.Family"
    },
    {
      title: "نام پدر",
      data: "FatherName"
    },
    {
      title: "جنسیت",
      data: "User.GenderName"
      // render(data, type, row) {
      //   return data ? 'پسر' : 'دختر';
      // }
    },
    {
      title: "کد ملی",
      data: "User.NationalNo"
    },
    {
      title: "نام کاربری",
      data: "User.Username"
    },
    {
      title: "فعال",
      data: "User.IsActive"
    },
    {
      title: "تلفن ثابت",
      data: "User.Phone"
    },
    {
      title: "موبایل",
      data: "User.Mobile"
    },
    {
      title: "نقش",
      data: "User.RoleName"
    },
    {
      title: "شهر",
      data: "User.CityName"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
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
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.studentStore.resetCreate();
    this.studentStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.studentStore.resetEdit();
    this.studentStore.getById(id).then(() => {
      this.studentStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.studentStore.getById(id).then(() => {
      this.studentStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.studentStore.fillList();
  }
  //#endregion
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
//     var pageAccess = this.$util.initAccess("/student");
//     return {
//       pageAccess,
//       studentGridColumn: [
//         {
//           title: "نام",
//           data: "User.Name"
//         },
//         {
//           title: "نام خانوادگی",
//           data: "User.Family"
//         },
//         {
//           title: "نام پدر",
//           data: "FatherName"
//         },
//         {
//           title: "جنسیت",
//           data: "User.GenderName"
//           // render(data, type, row) {
//           //   return data ? 'پسر' : 'دختر';
//           // }
//         },
//         {
//           title: "کد ملی",
//           data: "User.NationalNo"
//         },
//         {
//           title: "نام کاربری",
//           data: "User.Username"
//         },
//         {
//           title: "فعال",
//           data: "User.IsActive"
//         },
//         {
//           title: "تلفن ثابت",
//           data: "User.Phone"
//         },
//         {
//           title: "موبایل",
//           data: "User.Mobile"
//         },
//         {
//           title: "نقش",
//           data: "User.RoleName"
//         },
//         {
//           title: "شهر",
//           data: "User.CityName"
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
//     ...mapActions("studentStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore"
//     ]),
//     ...mapActions({
//       fillCityByProvincIdDdl: "cityStore/fillCityByStudentIdDdlStore"
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
//       this.getByIdStore(id).then(data => {
//         // fill cityDdl by studentId
//         this.fillCityByProvincIdDdl(data.User.StudentId);
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
//     ...mapState("studentStore", {
//       modelName: "modelName",
//       studentGridData: "studentGridData"
//     })
//   },
//   created() {
//     this.fillGridStore();
//   }
// };
</script>

