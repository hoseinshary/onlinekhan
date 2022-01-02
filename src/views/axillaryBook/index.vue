<template>
  <section class="col-12 q-px-md">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${axillaryBookStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="axillaryBookStore.gridData" :columns="axillaryBookGridColumn" hasIndex>
      <template slot="Lookup_PrintType.Value" slot-scope="data">{{data.row.Lookup_PrintType.Value}}</template>
      <template slot="Lookup_BookType.Value" slot-scope="data">{{data.row.Lookup_BookType.Value}}</template>
      <template slot="Lookup_PaperType.Value" slot-scope="data">{{data.row.Lookup_PaperType.Value}}</template>
      <template slot="Publisher.Name" slot-scope="data">{{data.row.Publisher.Name}}</template>
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
export default class AxillaryBookVue extends Vue {
  //#region ### data ###
  axillaryBookStore = vxm.axillaryBookStore;
  pageAccess = util.getAccess(this.axillaryBookStore.modelName);
  axillaryBookGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "سال انتشار",
      data: "PublishYear"
    },
    {
      title: "نویسنده",
      data: "Author"
    },
    {
      title: "شابک",
      data: "Isbn"
    },
    {
      title: "قلم",
      data: "Font"
    },
    {
      title: "قیمت",
      data: "Price"
    },
    {
      title: "قیمت پشت جلد",
      data: "OriginalPrice"
    },
    {
      title: "انتشارات",
      data: "Publisher.Name"
    },
    {
      title: "نوع چاپ",
      data: "Lookup_PaperType.Value"
    },
    {
      title: "نوع قطع",
      data: "Lookup_BookType.Value"
    },
    {
      title: "نوع کاغذ",
      data: "Lookup_PaperType.Value"
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
    this.axillaryBookStore.resetCreate();
    this.axillaryBookStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.axillaryBookStore.resetEdit();
    this.axillaryBookStore.getById(id).then(() => {
      this.axillaryBookStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.axillaryBookStore.getById(id).then(() => {
      this.axillaryBookStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.axillaryBookStore.fillList();
  }
  //#endregion
}
</script>

