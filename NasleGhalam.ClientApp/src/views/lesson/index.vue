<template>
  <section class="col-md-12 q-px-lg">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div class="row" slot="body">
        <div class="col-md-4">
          <my-select :model="$v.instanceObj.TreeId_Grade"
               :options="educationTreeDdl"
               class="col-md-12"
               clearable />
        </div>
        <div class="col-md-8">
          <my-btn-create v-if="pageAccess.canCreate"
                        :label="`ایجاد (${modelName}) جدید`"
                        @click="showModalCreate" />
          <br>
          <my-table :grid-data="allObj"
                    :columns="gridColumns"
                    hasIndex
                    class="col-md-8">
            <template slot="Id"
                      slot-scope="data">
              <my-btn-edit round
                          v-if="pageAccess.canEdit"
                          @click="showModalEdit(data.row.Id)" />
              <my-btn-delete round
                            v-if="pageAccess.canDelete"
                            @click="showModalDelete(data.row.Id)" />
                
            </template>
          </my-table>
        </div>
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
    var pageAccess = this.$util.initAccess('/lesson');
    return {
      pageAccess,
      gridColumns: [
        {
          title: 'نام',
          data: 'Name'
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
    ...mapActions('lessonStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions('educationTreeStore', [
      'getAllEducationTreeByState'
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
    }),
    ...mapState('educationTreeStore', {
      educationTreeDdl: 'educationTreeDdl'
    })
  },
  created() {
    this.getAllEducationTreeByState(0);
  }
};
</script>

