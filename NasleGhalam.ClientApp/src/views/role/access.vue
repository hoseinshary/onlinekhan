<template>
  <bs-modal :show="isOpenModalAccess"
            size="lg"
            @open="openModal"
            @close="closeModal">

    <!-- header -->
    <template slot="header">
      <q-toolbar slot="header"
                 color="cyan-9"
                 text-color="">
        <q-toolbar-title>
          انتساب نقش به
          <span class="text-orange">{{roleName}}</span>
        </q-toolbar-title>
        <q-btn dense
               icon="close"
               @click="closeModal" />
      </q-toolbar>
    </template>

    <!-- body -->
    <my-select :model="$v.accessObj.ModuleId"
               :options="moduleDdl"
               class="col-md-6"
               filter
               @change="fillControllerDdl" />

    <my-select :model="$v.accessObj.ControllerId"
               :options="controllerDdl"
               class="col-md-6"
               filter
               @change="fillActionGrid" />

    <my-table :grid-data="actionGridData"
              :columns="gridColumns">
      <template slot="IsChecked"
                slot-scope="data">
        <q-checkbox v-model="data.row.IsChecked"
                    @input="actionChecked(data.row.Id, $event)" />
      </template>
    </my-table>

    <!-- footer -->

    <template slot="footer">
      <my-btn-back @click="closeModal"></my-btn-back>
    </template>
  </bs-modal>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import viewModel from 'viewModels/role/accessViewModel';

export default {
  /**
   * data
   */
  data() {
    return {
      moduleId: 0,
      gridColumns: [
        {
          title: 'انتخاب',
          data: 'IsChecked',
          searchable: false,
          sortable: false
        },
        {
          title: 'نام فرم',
          data: 'ControllerFaName'
        },
        {
          title: 'نام عملیات',
          data: 'ActionFaName'
        }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('roleStore', [
      'toggleModalAccessStore',
      'fillModuleDdlStore',
      'fillControllerByModuleIdDdlStore',
      'fillActionByControllerIdAndModuleIdGridStore',
      'changeAccessStore'
    ]),
    fillControllerDdl(moduleId) {
      this.moduleId = moduleId;
      this.fillControllerByModuleIdDdlStore(moduleId);
    },
    fillActionGrid(value) {
      this.fillActionByControllerIdAndModuleIdGridStore({
        controllerId: value,
        moduleId: this.moduleId
      });
    },
    openModal() {
      this.fillModuleDdlStore();
    },
    closeModal() {
      this.toggleModalAccessStore(false);
    },
    actionChecked(actionId, checked) {
      this.changeAccessStore({
        ActionId: actionId,
        IsChecked: checked
      });
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('roleStore', {
      roleName: 'roleName',
      isOpenModalAccess: 'isOpenModalAccess',
      accessObj: 'accessObj',
      moduleDdl: 'moduleDdl',
      controllerDdl: 'controllerDdl',
      actionGridData: 'actionGridData'
    })
  },
  /**
   * validations
   */
  validations: viewModel
};
</script>

