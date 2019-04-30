<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{publisherStore.modelName}}</span>
      <div slot="body">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${publisherStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="publisherStore.gridData" :columns="publisherGridColumn" hasIndex>
          <template slot="Id" slot-scope="data">
            <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete
              v-if="canDelete"
              round
              @click="showModalDelete(data.row.Id)"
            />
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
export default class PublisherVue extends Vue {
  //#region ### data ###
  publisherStore = vxm.publisherStore;
  pageAccess = util.getAccess(this.publisherStore.modelName);
  publisherGridColumn = [
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
    this.publisherStore.resetCreate();
    this.publisherStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.publisherStore.resetEdit();
    this.publisherStore.getById(id).then(() => {
      this.publisherStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.publisherStore.getById(id).then(() => {
      this.publisherStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.publisherStore.fillList();
  }
  //#endregion
}
</script>