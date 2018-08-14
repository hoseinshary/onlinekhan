<template>
  <section class="col-xs-12 q-px-md">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <my-btn-create v-if="pageAccess.canCreate"
                       :label="`ایجاد (${modelName}) جدید`"
                       @click="showModalCreate" />
        <br>
        <my-table :grid-data="userGridData"
                  :columns="userGridColumn"
                  hasIndex>
          <template slot="IsActive"
                    slot-scope="data">
            <q-checkbox v-model="data.row.Gender"
                        readonly />
          </template>
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
    <!-- <modal-create v-if="pageAccess.canCreate"></modal-create> -->
    <!--<modal-edit v-if="pageAccess.canEdit"></modal-edit> -->
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  components: {
    // 'modal-create': () => import('./create'),
    // 'modal-edit': () => import('./edit'),
    'modal-delete': () => import('./delete')
  },
  /**
   * data
   */
  data() {
    var pageAccess = this.$util.initAccess('/user');
    return {
      pageAccess,
      userGridColumn: [
        {
          title: 'نام',
          data: 'Name'
        },
        {
          title: 'نام خانوادگی',
          data: 'Family'
        },
        {
          title: 'جنسیت',
          data: 'Gender'
          // render(data, type, row) {
          //   return data ? 'پسر' : 'دختر';
          // }
        },
        {
          title: 'کد ملی',
          data: 'NationalNo'
        },
        {
          title: 'نام کاربری',
          data: 'Username'
        },
        {
          title: 'فعال',
          data: 'IsActive'
        },
        {
          title: 'تلفن ثابت',
          data: 'Phone'
        },
        {
          title: 'موبایل',
          data: 'Mobile'
        },
        {
          title: 'نقش',
          data: 'RoleName'
        },
        {
          title: 'شهر',
          data: 'CityName'
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
    ...mapActions('userStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
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
    }
  },
  computed: {
    ...mapState('userStore', {
      modelName: 'modelName',
      userGridData: 'userGridData'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

