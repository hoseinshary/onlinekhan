<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">

        <section class="row gutter-md">
          <my-select :model="$v.universityBranchIndexObj.EducationGroupId"
                     :options="educationGroupDdl"
                     class="col-md-3 col-sm-4"
                     @change="fillGridStore($event)"
                     clearable />

          <div class="col-md-3 col-sm-4">
            <my-btn-create v-if="pageAccess.canCreate"
                           :label="`ایجاد (${modelName}) جدید`"
                           @click="showModalCreate" />
          </div>
        </section>

        <br>
        <my-table :grid-data="universityBranchGridData"
                  :columns="universityBranchGridColumn"
                  hasIndex>
          <template slot="Id"
                    slot-scope="data">
            <my-btn-edit v-if="pageAccess.canEdit"
                         round
                         @click="showModalEdit(data.row.Id)" />
            <my-btn-delete v-if="pageAccess.canDelete"
                           round
                           @click="showModalDelete(data.row.Id)" />
          </template>
        </my-table>
      </div>
    </my-panel>

    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import viewModel from 'viewModels/universityBranch/universityBranchIndexViewModel';

export default {
  components: {
    'modal-create': () => import('./create'),
    'modal-edit': () => import('./edit'),
    'modal-delete': () => import('./delete')
  },
  /**
   * data
   */
  data() {
    var pageAccess = this.$util.initAccess('/universityBranch');
    return {
      pageAccess,
      universityBranchGridColumn: [
        {
          title: 'زیر گروه آموزشی',
          data: 'EducationSubGroupName'
        },
        {
          title: 'نام رشته',
          data: 'Name'
        },
        {
          title: 'تراز',
          data: 'Balance'
        },
        {
          title: 'عملیات',
          data: 'Id',
          searchable: false,
          sortable: false,
          visible: pageAccess.canEdit || pageAccess.canDelete
        }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('universityBranchStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions('educationGroupStore', {
      fillEducationGroupDdlStore: 'fillDdlStore'
    }),
    showModalCreate() {
      this.$v.universityBranchIndexObj.$touch();
      if (this.$v.universityBranchIndexObj.$error) {
        return;
      }
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
    },
    showModalEdit(id) {
      this.$v.universityBranchIndexObj.$touch();
      if (this.$v.universityBranchIndexObj.$error) {
        return;
      }
      // reset data on modal show
      this.resetEditStore();
      // get data by id
      this.getByIdStore(id).then(() => {
        // show modal
        this.toggleModalEditStore(true);
      });
    },
    showModalDelete(id) {
      // get data by id
      this.getByIdStore(id).then(() => {
        // show modal
        this.toggleModalDeleteStore(true);
      });
    }
  },
  computed: {
    ...mapState('universityBranchStore', {
      modelName: 'modelName',
      universityBranchIndexObj: 'universityBranchIndexObj',
      universityBranchGridData: 'universityBranchGridData'
    }),
    ...mapState('educationGroupStore', {
      educationGroupDdl: 'educationGroupDdl'
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
    this.fillEducationGroupDdlStore();
  }
};
</script>

