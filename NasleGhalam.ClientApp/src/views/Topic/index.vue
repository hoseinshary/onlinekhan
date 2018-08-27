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
        <my-select :model="$v.accessObj.ModuleId"
                   :options="moduleDdl"
                   class="col-md-6"
                   filter
                   @change="fillControllerDdl" />

        <my-select :model="$v.accessObj.ControllerId"
                   :options="controllerDdl"
                   class="col-md-6"
                   filter
                   @change="fillActionGrid" />
        <!-- <my-table :grid-data="topicGridData"
                  :columns="topicGridColumn"
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
        </my-table> -->
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
    var pageAccess = this.$util.initAccess('/topic');
    return {
      pageAccess
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('topicStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions('educationGroupStore', [
      (fillEduGrpDdlStore = 'fillDdlStore')
    ]),
    ...mapActions('lessonStore', [(fillLessonDdlStore = 'fillDdlStore')]),
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
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicGridData: 'topicGridData'
    }),
    ...mapState('educationGroupStore', {
      educationGroupDdl: 'educationGroupDdl'
    }),
    ...mapState('lessonStore', {
      lessonDdl: 'allObjDdl'
    })
  },
  created() {
    this.fillEduGrpDdlStore();
    this.fillLessonDdlStore();
  }
};
</script>

