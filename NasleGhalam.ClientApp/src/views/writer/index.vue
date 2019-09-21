<template>
  <section class="col-md-8">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${writerStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="writerStore.gridData" :columns="writerGridColumn" hasIndex>
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
export default class WriterVue extends Vue {
  //#region ### data ###
  writerStore = vxm.writerStore;
  pageAccess = util.getAccess(this.writerStore.modelName);
  writerGridColumn = [
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
    this.writerStore.resetCreate();
    this.writerStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.writerStore.resetEdit();
    this.writerStore.getById(id).then(() => {
      this.writerStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.writerStore.getById(id).then(() => {
      this.writerStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.writerStore.fillList();
  }
  //#endregion
}
</script>