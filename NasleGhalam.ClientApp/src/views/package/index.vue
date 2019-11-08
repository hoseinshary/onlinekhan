<template>
  <section class="col-md-11">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${packageStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="packageStore.gridData" :columns="packageGridColumn" hasIndex>
      <template slot="IsActive" slot-scope="data">
        <q-checkbox v-model="data.row.IsActive" readonly />
      </template>
      <template slot="Id" slot-scope="data">
        <!-- <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" /> -->
        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <!-- <modal-edit v-if="canEdit"></modal-edit> -->
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
    // ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class PackageVue extends Vue {
  //#region ### data ###
  packageStore = vxm.packageStore;
  pageAccess = util.getAccess(this.packageStore.modelName);
  packageGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "قیمت",
      data: "Price"
    },
    {
      title: "مدت به روز",
      data: "TimeDays"
    },
    {
      title: "تاریخ ایجاد",
      data: "PCreateDateTime"
    },
    {
      title: "فعال",
      data: "IsActive"
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
    this.packageStore.resetCreate();
    this.packageStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.packageStore.resetEdit();
    this.packageStore.getById(id).then(() => {
      this.packageStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.packageStore.getById(id).then(() => {
      this.packageStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.packageStore.fillList();
  }
  //#endregion
}
</script>