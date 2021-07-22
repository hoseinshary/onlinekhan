<template>
  <bs-modal
    title="انتخاب دانش آموز"
    :show="assayStore.openModal._0student"
    @close="assayStore.OPEN_MODAL_0STUDENT(false)"
  >


    <template slot="header">
      <q-toolbar slot="header" color="warning" text-color>
        <q-toolbar-title>انتخاب دانش آموز</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_0STUDENT(false)" />
      </q-toolbar>
    </template>

    <slot>
       <section class="col-12 q-px-md">
    <br />
    <br />

    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${studentStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <br />
    <br />

    <div class="col-md-6">
      <q-select
        v-model="student.id"
        :options="studentStore.ddl"
        float-label="انتخاب دانش آموز"
        filter
        clearable
      />
    </div>
    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>

    <br />
    <br />
    <div class="col-12">
      <!-- <q-btn color="primary" class="float-right" @click="goToLessonTab">
        انتخاب درس
        <q-icon name="arrow_back" />
      </q-btn> -->
    </div>
  </section>
    </slot>

    <template slot="footer">
      <base-btn-save-back @click="assayStore.OPEN_MODAL_0STUDENT(false)"></base-btn-save-back>
      <base-btn-back @click="assayStore.OPEN_MODAL_0STUDENT(false)"></base-btn-back>
    </template>
  </bs-modal>


</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("../../student/create.vue")

  }
})
export default class StudentVue extends Vue {
  //#region ### data ###
  studentStore = vxm.studentStore;
  student = vxm.studentStore.student;
  assayStore = vxm.assayStore;

  pageAccess = util.getAccess(this.studentStore.modelName);

  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
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

  goToLessonTab() {
    this.$emit("changeTab", "lessonTab");
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

