<template>

  <my-panel class="col-md-8">
    <span slot="title">{{modelName}}</span>
    <div slot="body">
      <my-btn-create @click="showModalCreate"></my-btn-create>
      <br>
      <my-table :grid-data="allObj"
                :columns="gridColumns"
                :hasIndex="true">
        <template slot="Id"
                  slot-scope="data">
          <my-btn-edit round
                       @click="showModalEdit(data.row.Id)" />
          <my-btn-delete round
                         @click="showModalDelete(data.row.Id)" />
        </template>
      </my-table>

      <modal-create></modal-create>
      <modal-edit></modal-edit>
      <modal-delete></modal-delete>
    </div>
  </my-panel>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import modalCreate from './create';
import modalEdit from './edit';
import modalDelete from './delete';

export default {
  components: {
    'modal-create': modalCreate,
    'modal-edit': modalEdit,
    'modal-delete': modalDelete
  },
  /**
   * data
   */
  data() {
    return {
      gridColumns: [
        {
          title: 'نام',
          data: 'Name',
          searchable: true,
          sortable: true
        },
        {
          title: 'اولویت',
          data: 'Priority',
          searchable: true,
          sortable: true
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
    ...mapActions('grade', [
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
    ...mapState('grade', {
      modelName: 'modelName',
      allObj: 'allObj'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

