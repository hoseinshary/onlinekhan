<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <my-btn-create v-if="rolePageAccess.canCreate"
                       :label="`ایجاد (${modelName}) جدید`"
                       @click="showModalCreate" />
        <br>
        <my-table :grid-data="roleGridData"
                  :columns="gridColumns"
                  hasIndex>
          <template slot="Id"
                    slot-scope="data">
            <q-btn v-if="rolePageAccess.canAccess"
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

            <my-btn-edit v-if="rolePageAccess.canEdit"
                         round
                         @click="showModalEdit(data.row.Id)" />
            <my-btn-delete v-if="rolePageAccess.canDelete"
                           round
                           @click="showModalDelete(data.row.Id)" />
          </template>
        </my-table>
      </div>
    </my-panel>

    <component :is="componentId"
               @hook:created="showModals"></component>
    <!-- modals -->
    <!-- <modal-create v-if="roleModalShown.modalCreate"></modal-create>
    <modal-edit v-if="roleModalShown.modalEdit"></modal-edit>
    <modal-delete v-if="roleModalShown.modalDelete"></modal-delete>
    <modal-access v-if="roleModalShown.modalAccess"></modal-access> -->
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  // components: {
  //   'modal-create': () => import('./create'),
  //   'modal-edit': () => import('./edit'),
  //   'modal-delete': () => import('./delete'),
  //   'modal-access': () => import('./access')
  // },
  /**
   * data
   */
  data() {
    return {
      componentId: '',
      currentObjId: 0,
      rolePageAccess: {
        canCreate: false,
        canEdit: false,
        canDelete: false,
        canAccess: false
      },
      roleModalShown: {
        modalCreate: false,
        modalEdit: false,
        modalDelete: false,
        modalAccess: false
      },
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
          title: 'عملیات',
          data: 'Id',
          searchable: false,
          sortable: false
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
      if (this.componentId == 'modal-create') this.showModals();
      else this.componentId = 'modal-create';
      // debugger;
      // // modal Create v-if = true => Created Trigger
      // this.roleModalShown.modalCreate = true;

      // // reset data on modal show
      // this.resetCreateStore();
      // // show modal
      // this.toggleModalCreateStore(true);
    },
    showModals() {
      debugger;
      switch (this.componentId) {
        case 'modal-create':
          this.resetCreateStore();
          this.toggleModalCreateStore(true);
          break;
        case 'modal-edit':
          this.resetEditStore();
          this.getByIdStore(this.currentObjId).then(() => {
            // show modal
            this.toggleModalEditStore(true);
          });
          break;
      }
    },
    showModalEdit(id) {
      this.currentObjId = id;
      if (this.componentId == 'modal-edit') this.showModals();
      else this.componentId = 'modal-edit';
      // // reset data on modal show
      // this.resetEditStore();
      // // modal Edit v-if = true => Created Trigger
      // this.roleModalShown.modalEdit = true;
      // // get data by id
      // this.getByIdStore(id).then(() => {
      //   // show modal
      //   this.toggleModalEditStore(true);
      // });
    },
    showModalDelete(id) {
      // get data by id
      this.getByIdStore(id).then(() => {
        // modal Delete v-if = true => Created Trigger
        this.roleModalShown.modalDelete = true;
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
    var accessControl = this.$q.localStorage.get
      .item('subMenuList')
      .filter(x => x.EnName.toLowerCase() == '/role')
      .map(x => x.UserAccess);
    this.rolePageAccess.canCreate = accessControl[0].includes('ایجاد');
    this.rolePageAccess.canEdit = accessControl[0].includes('ویرایش');
    this.rolePageAccess.canDelete = accessControl[0].includes('حذف');
    this.rolePageAccess.canDelete = accessControl[0].includes('دسترسی');

    this.fillGridStore();
  },
  watch: {
    componentId(newVal, oldVal) {
      console.log(newVal);
      console.log(oldVal);
    }
  }
};
</script>

