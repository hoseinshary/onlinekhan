<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <my-btn-create :label="`ایجاد (${modelName}) جدید`"
                       @click="showModalCreate" />
        <br>
        <my-table :grid-data="allObj"
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

    <!-- modals -->
    <modal-create></modal-create>
    <modal-edit></modal-edit>
    <modal-delete></modal-delete>
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
    return {
      gridColumns: [
        {
          title: 'نام',
          data: 'Name'
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
    ...mapActions('lessonStore', [
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
    ...mapState('lessonStore', {
      modelName: 'modelName',
      allObj: 'allObj'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

