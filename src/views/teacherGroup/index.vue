<template>
  <section class="col-md-8">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${teacherGroupStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="teacherGroupStore.gridData" :columns="teacherGroupGridColumn" hasIndex>
       <template slot="Teacher.User.FullName" slot-scope="data">{{data.row.Teacher.User.FullName}}</template>
       <template slot="Students.User.FullName" slot-scope="data">{{data.row.Students.map(x => x.User.FullName)}}</template>
      <template slot="Id" slot-scope="data">
     


         <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
        <q-btn class="q-ma-sm" size="sm" round color="purple" icon="group_add" @click="showModalSubGroup(data.row.Id)">
          
          <q-tooltip>زیر گروه</q-tooltip>
        </q-btn>
    </template>
    </base-table>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-create-sub v-if="canCreate"></modal-create-sub>
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
    ModalCreateSub: () => import("./createSub.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class TeacherGroupVue extends Vue {
  //#region ### data ###
  teacherGroupStore = vxm.teacherGroupStore;
  pageAccess = util.getAccess(this.teacherGroupStore.modelName);
  teacherGroupGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "نام دبیر",
      data: "Teacher.User.FullName"
    },
    {
      title: "اعضا",
      data: "Students.User.FullName"
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
    this.teacherGroupStore.resetCreate();
    this.teacherGroupStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.teacherGroupStore.resetEdit();
    this.teacherGroupStore.getById(id).then(() => {
      this.teacherGroupStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.teacherGroupStore.getById(id).then(() => {
      this.teacherGroupStore.OPEN_MODAL_DELETE(true);
    });
  }

   showModalSubGroup(id) {
     this.teacherGroupStore.getById(id).then(() => {
        this.teacherGroupStore.resetCreateSub();
        this.teacherGroupStore.OPEN_MODAL_CREATE_SUB(true);

    });
     
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.teacherGroupStore.fillList();
   // console.log('teacherGroupStore.gridData.StudentsId',this.teacherGroupStore.gridData)
  }
  //#endregion
}
</script>