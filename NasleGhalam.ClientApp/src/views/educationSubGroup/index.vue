<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{educationSubGroupStore.modelName}}</span>
      <div slot="body">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${educationSubGroupStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table
          :grid-data="educationSubGroupStore.gridData"
          :columns="educationSubGroupGridColumn"
          hasIndex
        >
          <template slot="EducationTree.Name" slot-scope="data">{{data.row.EducationTree.Name}}</template>
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
export default class EducationSubGroupVue extends Vue {
  //#region ### data ###
  educationSubGroupStore = vxm.educationSubGroupStore;
  pageAccess = util.getAccess(this.educationSubGroupStore.modelName);
  educationSubGroupGridColumn = [
    {
      title: "گروه آموزشی",
      data: "EducationTree.Name"
    },
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
    this.educationSubGroupStore.resetCreate();
    this.educationSubGroupStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.educationSubGroupStore.resetEdit();
    this.educationSubGroupStore.getById(id).then(() => {
      this.educationSubGroupStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.educationSubGroupStore.getById(id).then(() => {
      this.educationSubGroupStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationSubGroupStore.fillList();
  }
  //#endregion
}
</script>

