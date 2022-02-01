<template>
  <section class="col-12 q-px-md">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${teacherStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="teacherStore.gridData" :columns="teacherGridColumn" hasIndex>
      <template slot="User.Name" slot-scope="data">{{data.row.User.Name}}</template>
      <template slot="User.Family" slot-scope="data">{{data.row.User.Family}}</template>
      <template slot="User.GenderName" slot-scope="data">{{data.row.User.GenderName}}</template>
      <template slot="User.NationalNo" slot-scope="data">{{data.row.User.NationalNo}}</template>
      <template slot="User.Username" slot-scope="data">{{data.row.User.Username}}</template>
      <template slot="User.IsActive" slot-scope="data">
        <q-checkbox v-model="data.row.User.IsActive" readonly />
      </template>
      <template slot="User.Phone" slot-scope="data">{{data.row.User.Phone}}</template>
      <template slot="User.Mobile" slot-scope="data">{{data.row.User.Mobile}}</template>
      <template slot="User.RoleName" slot-scope="data">{{data.row.User.RoleName}}</template>
      <template slot="User.CityName" slot-scope="data">{{data.row.User.CityName}}</template>
      <template slot="Id" slot-scope="data">
        <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>

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
export default class TeacherVue extends Vue {
  //#region ### data ###
  teacherStore = vxm.teacherStore;
  pageAccess = util.getAccess(this.teacherStore.modelName);
  teacherGridColumn = [
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
    this.teacherStore.resetCreate();
    this.teacherStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.teacherStore.resetEdit();
    this.teacherStore.getById(id).then(() => {
      this.teacherStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.teacherStore.getById(id).then(() => {
      this.teacherStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.teacherStore.fillList();
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
//     var pageAccess = this.$util.initAccess("/teacher");
//     return {
//       pageAccess,
//       teacherGridColumn: [
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
//     ...mapActions("teacherStore", [
//       "toggleModalCreateStore",
//       "toggleModalEditStore",
//       "toggleModalDeleteStore",
//       "getByIdStore",
//       "fillGridStore",
//       "resetCreateStore",
//       "resetEditStore"
//     ]),
//     ...mapActions({
//       fillCityByProvincIdDdl: "cityStore/fillCityByTeacherIdDdlStore"
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
//         // fill cityDdl by teacherId
//         this.fillCityByProvincIdDdl(data.User.TeacherId);
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
//     ...mapState("teacherStore", {
//       modelName: "modelName",
//       teacherGridData: "teacherGridData"
//     })
//   },
//   created() {
//     this.fillGridStore();
//   }
// };
</script>

