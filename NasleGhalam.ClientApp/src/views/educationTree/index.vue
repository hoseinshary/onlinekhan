<template>
<section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <div style="min-height: 67px;">
        <q-slide-transition>

            <div class="col-md-12"
                 v-show="selectedNodeId !=null">
                <div class="col-12"
                     >
                  <my-btn-create v-if="pageAccess.canCreate"
                                 @click="showModalCreate" 
                                 :disabled="educationTreeData.length == 0 ? false : selectedNodeId == null"/>
                  <my-btn-edit v-if="pageAccess.canEdit"
                                 :disabled="selectedNodeId == null"
                               @click="showModalEdit" />
                  <my-btn-delete v-if="pageAccess.canDelete"
                                 :disabled="selectedNodeId == null"
                                 @click="showModalDelete" />
                </div>
            </div>
          </q-slide-transition>
          </div>
          <q-slide-transition>
            <q-tree :nodes="educationTreeData"
                  class="col-md-12"
                  color="blue"
                  :selected.sync="selectedNodeId"
                  default-expand-all
                  node-key="Id"
                  ref="topicTree"  />
          </q-slide-transition>
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
    var pageAccess = this.$util.initAccess('/educationTree'); 
    return {
      pageAccess,
      selectedNodeId: null
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('educationTreeStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillTreeStore',
      'resetCreateStore',
      'resetEditStore'
    ]),
    showModalCreate() {
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
      this.educationTreeObj.ParentEducationTreeId = this.selectedNodeId;
    },
    showModalEdit() {
      // reset data on modal show
      this.resetEditStore();
      // get data by id
      this.getByIdStore(this.selectedNodeId).then(() => {
        // show modal
        this.toggleModalEditStore(true);
      });
    },
    showModalDelete() {
      // get data by id
      this.getByIdStore(this.selectedNodeId).then(() => {
        // show modal
        this.toggleModalDeleteStore(true);
      });
    },
    listToTree(lst){
      return [];
    }
  },
  computed: {
    ...mapState('educationTreeStore', {
      modelName: 'modelName',
      educationTreeData: 'educationTreeData',
      educationTreeObj:'educationTreeObj'
    })
    // ,
    // treeLst: function() {
    //   return this.listToTree(this.educationTreeData);
    // }
  },
  created() {
    this.fillTreeStore();
  }
};
</script>

