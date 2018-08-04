<template>
  <bs-modal :show="isOpenModalAccess"
            size="lg"
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
               clearable
               @change="fillControllerDdl" />

    <my-select :model="$v.accessObj.ControllerId"
               :options="controllerDdl"
               class="col-md-6"
               clearable
               @change="fillActionGrid" />

   

    <my-table :grid-data="actionGridData"
              :columns="gridColumns">
      <template slot="IsChecked"
                slot-scope="data">
        <q-checkbox v-model="data.row.IsChecked"
                    @input="actionChecked($event)" />
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
      'fillActionByControllerIdGridStore'
    ]),
    fillControllerDdl(item) {
      this.fillControllerByModuleIdDdlStore(item.value);
    },
    fillActionGrid(item) {
      this.fillActionByControllerIdGridStore(item.value);
    },
    closeModal() {
      this.toggleModalAccessStore(false);
    },
    actionChecked(e) {
      console.log(e);
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
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.fillModuleDdlStore();
  }
};
</script>

