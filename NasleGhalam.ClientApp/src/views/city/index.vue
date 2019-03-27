<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{cityStore.modelName}}</span>
      <div slot="body">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${cityStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="cityStore.gridData" :columns="cityGridColumn" hasIndex>
          <template slot="Province.Name" slot-scope="data">{{data.row.Province.Name}}</template>
          <template slot="Id" slot-scope="data">
            <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)"/>
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
export default class CityVue extends Vue {
  //### data ###
  cityStore = vxm.cityStore;
  pageAccess = util.getAccess(this.cityStore.modelName);
  cityGridColumn = [
    {
      title: "استان",
      data: "Province.Name"
    },
    {
      title: "نام شهر",
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
  //--------------------------------------------------

  //### getters ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }
  //--------------------------------------------------

  //### methods ###
  showModalCreate() {
    this.cityStore.resetCreate();
    this.cityStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.cityStore.resetEdit();
    this.cityStore.getById(id).then(() => {
      this.cityStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.cityStore.getById(id).then(() => {
      this.cityStore.OPEN_MODAL_DELETE(true);
    });
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.cityStore.fillList();
  }
  //--------------------------------------------------
}
</script>