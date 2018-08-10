<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <my-btn-create v-if="pageAccess.canCreate"
                       :label="`ایجاد (${modelName}) جدید`"
                       @click="showModalCreate" />
        <br>
        <my-table :grid-data="roleGridData"
                  :columns="gridColumns"
                  hasIndex>
          <q-btn slot="role"
                 slot-scope="data"
                 v-if="pageAccess.canAccess"
                 outline
                 round
                 icon="list"
                 color="brown"
                 size="sm"
                 class="shadow-1 bg-white q-mr-sm"
                 @click="showModalAccess(data.row.Id, data.row.Name)">
            <q-tooltip>
              انتساب نقش
            </q-tooltip>
          </q-btn>
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
    <modal-access v-if="pageAccess.canAccess"></modal-access>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  components: {
    'modal-create': () => import('./create'),
    'modal-edit': () => import('./edit'),
    'modal-delete': () => import('./delete'),
    'modal-access': () => import('./access')
  },
  /**
   * data
   */
  data() {
    var pageAccess = this.$util.initAccess('/role');
    return {
      pageAccess: pageAccess,
      gridColumns: [
        {
          title: 'نام',
          data: 'Name'
        },
        {
          title: 'سطح نقش',
          data: 'Level'
        },
        {
          title: 'انتساب نقش',
          data: 'role',
          searchable: false,
          sortable: false
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
    ...mapActions('roleStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore',
      'toggleModalAccessStore',
      'setRoleIdStore',
      'setRoleNameStore'
    ]),
    showModalCreate() {
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
    },
    showModalEdit(id) {
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
    },
    showModalAccess(id, name) {
      this.setRoleIdStore(id);
      this.setRoleNameStore(name);
      this.toggleModalAccessStore(true);
    }
  },
  computed: {
    ...mapState('roleStore', {
      modelName: 'modelName',
      roleGridData: 'roleGridData'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

