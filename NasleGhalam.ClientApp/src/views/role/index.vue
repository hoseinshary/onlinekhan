<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{roleStore.modelName}}</span>
      <div slot="body">
        <base-btn-create :label="`ایجاد (${roleStore.modelName}) جدید`" @click="showModalCreate"/>
        <br>
        <base-table :grid-data="roleStore.gridData" :columns="roleGridColumn" hasIndex>
          <q-btn
            slot="role"
            slot-scope="data"
            outline
            round
            icon="list"
            color="brown"
            size="sm"
            class="shadow-1 bg-white q-mr-sm"
            @click="showModalAccess(data.row.Id)"
          >
            <q-tooltip>انتساب نقش</q-tooltip>
          </q-btn>
          <template slot="Id" slot-scope="data">
            <base-btn-edit round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create></modal-create>
    <modal-edit></modal-edit>
    <modal-delete></modal-delete>
    <modal-access></modal-access>
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
    ModalDelete: () => import("./delete.vue"),
    ModalAccess: () => import("./access.vue")
  }
})
export default class RoleVue extends Vue {
  //### data ###
  roleStore = vxm.roleStore;
  pageAccess = util.getAccess(this.roleStore.modelName);
  roleGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "سطح نقش",
      data: "Level"
    },
    {
      title: "نوع کاربری",
      data: "UserTypeName"
    },
    {
      title: "انتساب نقش",
      data: "role",
      searchable: false,
      sortable: false,
      visible: this.canAccess
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

  get canAccess() {
    return this.pageAccess.indexOf("دسترسی") > -1;
  }
  //--------------------------------------------------

  //### methods ###
  showModalCreate() {
    this.roleStore.resetCreate();
    this.roleStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.roleStore.resetEdit();
    this.roleStore.getById(id).then(() => {
      this.roleStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.roleStore.getById(id).then(() => {
      this.roleStore.OPEN_MODAL_DELETE(true);
    });
  }

  showModalAccess(id, name) {
    this.roleStore.getById(id).then(() => {
      vxm.accessStore.OPEN_MODAL_ACCESS(true);
      vxm.accessStore.SET_ROLE_ID(id);
    });
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.roleStore.fillList();
  }
  //--------------------------------------------------
}
</script>