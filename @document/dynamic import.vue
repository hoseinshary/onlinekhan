<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <my-btn-create @click="showModalCreate"></my-btn-create>
        <br>
        <my-table :grid-data="gradeLevelGridData"
                  :columns="gridColumns"
                  hasIndex>
          <template slot="Id"
                    slot-scope="data">
            <my-btn-edit round
                         @click="showModalEdit(data.row.Id)" />
            <my-btn-delete round
                           @click="showModalDelete(data.row.Id)" />
          </template>
        </my-table>
      </div>
    </my-panel>

    <!-- load modals component dynamicaly -->
    <keep-alive>
      <component :is="componentInstance"
                 @hook:mounted='componentMounted'>
      </component>
    </keep-alive>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * data
   */
  data() {
    return {
      // componentInstance: null,
      modalName: '',
      modalInstance: {
        create: null,
        edit: null,
        delete: null
      },
      gridColumns: [
        {
          title: 'مقطع تحصیلی',
          data: 'GradeName'
        },
        {
          title: 'نام',
          data: 'Name'
        },
        {
          title: 'اولویت',
          data: 'Priority'
        },
        {
          title: 'عملیات',
          data: 'Id'
        }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('gradeLevelStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions('gradeStore', {
      fillGradeDdlStore: 'fillDdlStore'
    }),
    componentMounted() {
      if (this.modalName == 'create') {
        // show modal
        this.toggleModalCreateStore(true);
      } else if (this.modalName == 'edit') {
        // show modal
        this.toggleModalEditStore(true);
      } else if (this.modalName == 'delete') {
        // show modal
        this.toggleModalDeleteStore(true);
      }
    },
    showModalCreate() {
      this.modalName = 'create';

      // check if modal loaded
      if (!this.modalInstance.create) {
        this.modalInstance.create = () => import(`./${this.modalName}`);
      } else {
        // reset data on modal show
        this.resetCreateStore();
        // show modal
        this.toggleModalCreateStore(true);
      }
    },
    async showModalEdit(id) {
      this.modalName = 'edit';
      // get data by id
      await this.getByIdStore(id);

      // check if modal loaded
      if (!this.modalInstance.edit) {
        this.modalInstance.edit = () => import(`./${this.modalName}`);
      } else {
        // reset data on modal show
        this.resetEditStore();
        // show modal
        this.toggleModalEditStore(true);
      }
    },
    async showModalDelete(id) {
      this.modalName = 'delete';
      // get data by id
      await this.getByIdStore(id);

      // check if modal loaded
      if (!this.modalInstance.delete) {
        this.modalInstance.delete = () => import(`./${this.modalName}`);
      } else {
        // show modal
        this.toggleModalDeleteStore(true);
      }
    }
  },
  computed: {
    ...mapState('gradeLevelStore', {
      modelName: 'modelName',
      gradeLevelGridData: 'gradeLevelGridData'
    }),
    componentInstance() {
      return this.modalInstance[this.modalName];
    }
  },
  created() {
    this.fillGridStore();
    this.fillGradeDdlStore();
  }
};
</script>

