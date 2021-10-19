<template>
  <section class="col-md-8">
    <!-- <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${assayStore.modelName}) جدید`"
      @click="showModalCreate"
    /> -->
    <br />
    <base-table :grid-data="assayStore.gridData" :columns="assayGridColumn" hasIndex>
      <template slot="Province.Name" slot-scope="data">{{data.row.Province.Name}}</template>
      <template slot="Id" slot-scope="data">
        <!-- <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" /> -->

        <q-btn class="q-ma-sm" size="sm" round color="purple" icon="print" @click="printAssay(data.row.Id)"/>

        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>

    <!-- modals -->
    <!-- <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit> -->
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { assayStore } from "src/store/assayStore";

@Component({
  components: {

      ModalDelete: () => import("./delete.vue")

  }
})
export default class CityVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  
  pageAccess = util.getAccess(this.assayStore.modelName);
  assayGridColumn = [
    {
      title: "نام آزمون",
      data: "Title"
    },
    {
      title: "تاریخ ساخت",
      data: "DateTimeCreate"
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
  printAssay(id){
    
     this.assayStore.getById(id);
    // router.push("/studentMajorList/printMajorList");
  
  }


  showModalDelete(id) {
    this.assayStore.getById(id).then(() => {
      this.assayStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.assayStore.fillList();
  }
  //#endregion
}
</script>