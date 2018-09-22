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
      title:'Name',
      data:'Name'
    },{
      title:'PublishYear',
      data:'PublishYear'
    },{
      title:'Author',
      data:'Author'
    },{
      title:'Isbn',
      data:'Isbn'
    },{
      title:'Font',
      data:'Font'
    },{
      title:'LookupId_PrintType',
      data:'LookupId_PrintType'
    },{
      title:'Price',
      data:'Price'
    },{
      title:'OriginalPrice',
      data:'OriginalPrice'
    },{
      title:'LookupId_BookType',
      data:'LookupId_BookType'
    },{
      title:'LookupId_PaperType',
      data:'LookupId_PaperType'
    },{
      title:'Description',
      data:'Description'
    },{
      title:'PublisherId',
      data:'PublisherId'
    }
        ,{
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

