<template>
  <section class="col-md-8">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${studentMajorListStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table v-if="studentMajorListStore.gridData.length>0" :grid-data="studentMajorListStore.gridData" :columns="studentMajorListGridColumn" hasIndex>

    <template slot="Student.User.Family" slot-scope="data">{{data.row.Student.User.Name + " " +data.row.Student.User.Family }}</template>
      <template slot="Id" slot-scope="data">
        <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>

    <div v-else class="">
                    <img class="corner-around center"
                      src="~assets/newData.png"
                      style="width: 80%;"/>
                  </div>

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
export default class StudentMajorListVue extends Vue {
  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  pageAccess = util.getAccess(this.studentMajorListStore.modelName);
  studentMajorListGridColumn = [
    {
      title: "نام لیست انتخاب رشته",
      data: "Title"
    },
    {
      title: "نام و نام خانوادگی",
      data: "Student.User.Family"
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
    this.studentMajorListStore.resetCreate();
    this.studentMajorListStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.studentMajorListStore.resetEdit();
    this.studentMajorListStore.getById(id).then(() => {
      this.studentMajorListStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    console.log(id);
    this.studentMajorListStore.getById(id).then(() => {
      this.studentMajorListStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.studentMajorListStore.fillList();
  }
  //#endregion
}
</script>