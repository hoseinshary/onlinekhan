<template>
  <bs-modal :show="accessStore.openModal.access" size="lg" @open="open" @close="closeModal">
    <!-- header -->
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9" text-color>
        <q-toolbar-title>
          انتساب نقش به
          <span class="text-orange">{{roleStore.recordName}}</span>
        </q-toolbar-title>
        <q-btn dense icon="close" @click="closeModal"/>
      </q-toolbar>
    </template>

    <!-- body -->
    <base-select
      :model="$v.access.ModuleId"
      :options="accessStore.menuDdl"
      class="col-md-6"
      filter
    />

    <base-select
      :model="$v.access.ControllerId"
      :options="accessStore.subMenuDdl"
      class="col-md-6"
      filter
    />

    <base-table :grid-data="accessStore.actionGridData" :columns="gridColumns">
      <template slot="IsChecked" slot-scope="data">
        <q-checkbox v-model="data.row.IsChecked" @input="actionChecked(data.row.Id, $event)"/>
      </template>
    </base-table>

    <!-- footer -->
    <template slot="footer">
      <base-btn-back @click="closeModal"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { accessValidations } from "src/validations/accessValidation";

@Component({
  validations: accessValidations
})
export default class RoleAccessVue extends Vue {
  $v: any;

  //#region ### data ###
  roleStore = vxm.roleStore;
  accessStore = vxm.accessStore;
  access = vxm.accessStore.access;
  gridColumns = [
    {
      title: "انتخاب",
      data: "IsChecked",
      searchable: false,
      sortable: false
    },
    {
      title: "نام فرم",
      data: "ControllerFaName"
    },
    {
      title: "نام عملیات",
      data: "ActionFaName"
    }
  ];
  //#endregion

  //#region ### methods ###
  open() {
    this.accessStore.fillMenuList();
    this.accessStore.fillSubMenuList();
    this.accessStore.fillActionList();
  }

  closeModal() {
    this.accessStore.OPEN_MODAL_ACCESS(false);
  }

  actionChecked(actionId, checked) {
    this.accessStore.changeAccess({ actionId, checked });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.accessStore.SET_ACCESS_VUE(this);
  }
  //#endregion
}
</script>
