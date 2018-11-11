<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-select :model="$v.topicIndexObj.EducationTreeId_Grade"
                         :options="educationTree_GradeDdl"
                         class="col-12"
                         @change="gradeDdlChange" />

              <q-tree :nodes="educationTreeData"
                      color="primary"
                      tick-strategy="leaf"
                      accordion
                      node-key="Id"
                      ref="educationTree" />
            </section>
          </div>

          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-btn-create v-if="pageAccess.canCreate"
                             :label="`ایجاد (${modelName}) جدید`"
                             @click="showModalCreate" />
            </section>

          </div>
          <br>
        </section>
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
import viewModel from 'viewModels/topic/topicIndexViewModel';

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
      pageAccess,
      educationTreeData: []
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
      'fillTreeStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    ...mapActions('educationTreeStore', {
      getAllGrade: 'getAllGrade',
      fillEducationTreeStore: 'fillTreeStore',
      fillEducationTreeByGradeIdStore: 'fillTreeByGradeIdStore'
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
    gradeDdlChange(val) {
      // filter lesson tree by gradeId
      var self = this;
      this.fillEducationTreeByGradeIdStore(val).then(treeData => {
        self.educationTreeData = [treeData];
        setTimeout(() => {
          self.$refs.educationTree.expandAll();
        }, 300);
      });
    }
  },
  computed: {
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicTreeData: 'topicTreeData',
      topicIndexObj: 'topicIndexObj'
    }),
    ...mapState('educationTreeStore', {
      educationTree_GradeDdl: 'gradeDdl'
    })
  },
  validations: viewModel,
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

