<template>
  <section class="col-12 q-px-md">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${userStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="userStore.gridData" :columns="userGridColumn" hasIndex>
      <template slot="IsActive" slot-scope="data">
        <q-checkbox v-model="data.row.IsActive" readonly />
      </template>
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
export default class UserVue extends Vue {
  //#region ### data ###
  userStore = vxm.userStore;
  pageAccess = util.getAccess(this.userStore.modelName);
  userGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "نام خانوادگی",
      data: "Family"
    },
    {
      title: "جنسیت",
      data: "GenderName"
    },
    {
      title: "کد ملی",
      data: "NationalNo"
    },
    {
      title: "نام کاربری",
      data: "Username"
    },
    {
      title: "فعال",
      data: "IsActive"
    },
    {
      title: "تلفن ثابت",
      data: "Phone"
    },
    {
      title: "موبایل",
      data: "Mobile"
    },
    {
      title: "نقش",
      data: "RoleName"
    },
    {
      title: "شهر",
      data: "CityName"
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
    this.userStore.resetCreate();
    this.userStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.userStore.resetEdit();
    this.userStore.getById(id).then(() => {
      this.userStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.userStore.getById(id).then(() => {
      this.userStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.userStore.fillList();
  }
  //#endregion
}
</script>
