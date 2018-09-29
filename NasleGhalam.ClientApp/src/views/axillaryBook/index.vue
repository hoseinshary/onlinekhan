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
        <my-table :grid-data="axillaryBookGridData"
                  :columns="axillaryBookGridColumn"
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
    var pageAccess = this.$util.initAccess('/axillaryBook');
    return {
      pageAccess,
      axillaryBookGridColumn: [
        {
          title: 'نام',
          data: 'Name'
        },
        {
          title: 'سال انتشار',
          data: 'PublishYear'
        },
        {
          title: 'نویسنده',
          data: 'Author'
        },
        {
          title: 'شابک',
          data: 'Isbn'
        },
        {
          title: 'قلم',
          data: 'Font'
        },
        {
          title: 'قیمت',
          data: 'Price'
        },
        {
          title: 'قیمت پشت جلد',
          data: 'OriginalPrice'
        },
        {
          title: 'انتشارات',
          data: 'PublisherName'
        },
        {
          title: 'نوع چاپ',
          data: 'PrintTypeName'
        },
        {
          title: 'نوع قطع',
          data: 'BookTypeName'
        },
        {
          title: 'نوع کاغذ',
          data: 'PaperTypeName'
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
    ...mapActions('axillaryBookStore', [
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
    ...mapState('axillaryBookStore', {
      modelName: 'modelName',
      axillaryBookGridData: 'axillaryBookGridData'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

