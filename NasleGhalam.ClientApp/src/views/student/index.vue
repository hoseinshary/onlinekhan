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
        <my-table :grid-data="studentGridData"
                  :columns="studentGridColumn"
                  hasIndex>
          <template slot="User.Name"
                    slot-scope="data">
            {{data.row.User.Name}}
          </template>
          <template slot="User.Family"
                    slot-scope="data">
            {{data.row.User.Family}}
          </template>
          <template slot="User.GenderName"
                    slot-scope="data">
            {{data.row.User.GenderName}}
          </template>
          <template slot="User.NationalNo"
                    slot-scope="data">
            {{data.row.User.NationalNo}}
          </template>
          <template slot="User.Username"
                    slot-scope="data">
            {{data.row.User.Username}}
          </template>
          <template slot="User.IsActive"
                    slot-scope="data">
            <q-checkbox v-model="data.row.User.IsActive"
                        readonly />
          </template>
          <template slot="User.Phone"
                    slot-scope="data">
            {{data.row.User.Phone}}
          </template>
          <template slot="User.Mobile"
                    slot-scope="data">
            {{data.row.User.Mobile}}
          </template>
          <template slot="User.RoleName"
                    slot-scope="data">
            {{data.row.User.RoleName}}
          </template>
          <template slot="User.CityName"
                    slot-scope="data">
            {{data.row.User.CityName}}
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
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';

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
    var pageAccess = this.$util.initAccess('/student');
    return {
      pageAccess,
      studentGridColumn: [
        {
          title: 'نام',
          data: 'User.Name'
        },
        {
          title: 'نام خانوادگی',
          data: 'User.Family'
        },
        {
          title: 'نام پدر',
          data: 'FatherName'
        },
        {
          title: 'جنسیت',
          data: 'User.GenderName'
          // render(data, type, row) {
          //   return data ? 'پسر' : 'دختر';
          // }
        },
        {
          title: 'کد ملی',
          data: 'User.NationalNo'
        },
        {
          title: 'نام کاربری',
          data: 'User.Username'
        },
        {
          title: 'فعال',
          data: 'User.IsActive'
        },
        {
          title: 'تلفن ثابت',
          data: 'User.Phone'
        },
        {
          title: 'موبایل',
          data: 'User.Mobile'
        },
        {
          title: 'نقش',
          data: 'User.RoleName'
        },
        {
          title: 'شهر',
          data: 'User.CityName'
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
    ...mapActions('studentStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions({
      fillCityByProvincIdDdl: 'cityStore/fillCityByProvinceIdDdlStore'
    }),
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
      this.getByIdStore(id).then(data => {
        // fill cityDdl by provinceId
        this.fillCityByProvincIdDdl(data.User.ProvinceId);
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
    ...mapState('studentStore', {
      modelName: 'modelName',
      studentGridData: 'studentGridData'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

