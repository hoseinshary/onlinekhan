<template>
  <section class="col-md-8">
    <!-- <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${assayStore.modelName}) جدید`"
      @click="showModalCreate"
    /> -->
    <br />
    <base-table :grid-data="assayAnswerSheetStore.gridData" :columns="assayGridColumn" hasIndex>
      <template slot="Province.Name" slot-scope="data">{{data.row.Province.Name}}</template>
      <template slot="Id" slot-scope="data">
        <!-- <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" /> -->

        <q-btn class="q-ma-sm" size="sm" round color="blue" icon="directions_run" @click="runAssay(data.row.Id)">    
        <q-tooltip>اجرا</q-tooltip>
        </q-btn>
        <q-btn class="q-ma-sm" size="sm" round color="purple" icon="print" @click="printAssay(data.row.Id)">
          <q-tooltip>چاپ</q-tooltip>
        </q-btn>
        

        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>

    <!-- modals -->
    <!-- <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit> -->
    <modal-delete v-if="canDelete"></modal-delete>
        <modal-print ></modal-print>

  </section>
  
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import router from "src/router";

import { assayStore } from "src/store/assayStore";

@Component({
  components: {

      ModalDelete: () => import("./delete.vue"),
      ModalPrint: () => import("./printAssay.vue")
  }
})
export default class CityVue extends Vue {
  //#region ### data ###
  assayAnswerSheetStore = vxm.assayAnswerSheetStore;
  
  pageAccess = util.getAccess(this.assayAnswerSheetStore.modelName);
  assayGridColumn = [
    {
      title: "نام آزمون",
      data: "Assay.Title"
    },
    {
      title: "تاریخ آزمون",
      data: "DateTime"
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
    return this.pageAccess.indexOf("مشاهده") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("مشاهده") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("مشاهده") > -1;
  }


  //#endregion

  //#region ### methods ###



  runAssay(id){
    
    this.assayAnswerSheetStore.getById(id);
    router.push("/assay/runAssay");

  
  }


  showModalDelete(id) {
    this.assayAnswerSheetStore.getById(id).then(() => {
      this.assayAnswerSheetStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.assayAnswerSheetStore.fillList();
  }
  //#endregion
}
</script>