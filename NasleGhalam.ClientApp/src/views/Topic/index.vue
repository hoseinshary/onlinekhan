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
                         @change="gradeDdlChange" />

              <q-tree :nodes="educationTreeData"
                      color="primary"
                      tick-strategy="leaf"
                      accordion
                      node-key="Id"
                      :ticked.sync="topicIndexObj.EducationTreeIds"
                      ref="educationTree" />

              <my-select :model="$v.topicIndexObj.LessonId"
                         :options="lessonDdl"
                         class="q-pt-lg"
                         @change="lessonDdlChange" />
            </section>
          </div>
          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-btn-create v-if="pageAccess.canCreate && lessonDdl.length && !topicTreeData.length && topicIndexObj.LessonId != 0"
                             :label="`ایجاد (${modelName}) جدید`"
                             @click="showModalCreate" />
              <q-tree :nodes="topicTreeData"
                      class="q-pt-lg"
                      color="primary"
                      accordion
                      node-key="Id"
                      ref="topicTree"
                      :selected.sync="topicIndexObj.selectedTopicId">
                <div slot="header-custome"
                     slot-scope="prop">
                  {{prop.node.label}}
                  <template v-if="prop.node.visible && !(prop.node.children && prop.node.children.length)">
                    <q-btn outline
                           color="positive"
                           class="shadow-1 bg-white q-mr-sm q-px-xs"
                           icon="save"
                           size="sm"
                           @click.stop="showModalCreate" />
                    <q-btn outline
                           color="purple"
                           class="shadow-1 bg-white q-mr-sm q-px-xs"
                           icon="create"
                           size="sm"
                           @click.stop="showModalEdit(prop.node.Id)" />
                    <q-btn outline
                           color="red"
                           class="shadow-1 bg-white q-mr-sm q-px-xs"
                           icon="delete"
                           size="sm"
                           @click.stop="showModalDelete(prop.node.Id)" />
                  </template>
                  <template v-else-if="prop.node.visible">
                    <q-btn outline
                           color="positive"
                           class="shadow-1 bg-white q-mr-sm q-px-xs"
                           icon="save"
                           size="sm"
                           @click.stop="showModalCreate" />
                    <q-btn outline
                           color="purple"
                           class="shadow-1 bg-white q-mr-sm q-px-xs"
                           icon="create"
                           size="sm"
                           @click.stop="showModalEdit(prop.node.Id)" />
                  </template>
                </div>
              </q-tree>
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
      'resetEditStore',
      'clearTreeStore'
    ]),
    ...mapActions('educationTreeStore', {
      getAllGrade: 'getAllGrade',
      fillEducationTreeStore: 'fillTreeStore',
      fillEducationTreeByGradeIdStore: 'fillTreeByGradeIdStore'
    }),
    ...mapActions('lessonStore', {
      fillLessonDdlStore: 'fillDdlStore'
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
      this.topicIndexObj.LessonId = 0;
      this.clearTreeStore();
      this.topicIndexObj.EducationTreeIds = [];
      this.fillEducationTreeByGradeIdStore(val).then(treeData => {
        self.educationTreeData = [treeData];
        setTimeout(() => {
          self.$refs.educationTree.expandAll();
        }, 300);
      });
    },
    lessonDdlChange(val) {
      this.fillTreeStore(val);
      this.topicObj.LessonId = val;
    }
  },
  computed: {
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicTreeData: 'topicTreeData',
      topicIndexObj: 'topicIndexObj',
      topicObj: 'topicObj'
    }),
    ...mapState('educationTreeStore', {
      educationTree_GradeDdl: 'gradeDdl'
    }),
    ...mapState('lessonStore', {
      lessonDdl: 'allObjDdl'
    })
  },
  validations: viewModel,
  watch: {
    'topicIndexObj.EducationTreeIds'(val) {
      this.clearTreeStore();
      this.topicIndexObj.LessonId = 0;
      this.fillLessonDdlStore(val);
    },
    'topicIndexObj.selectedTopicId'(newVal, oldVal) {
      let node;
      if (newVal && oldVal) {
        node = this.$refs.topicTree.getNodeByKey(newVal);
        if (node) node.visible = true;
        node = this.$refs.topicTree.getNodeByKey(oldVal);
        if (node) node.visible = false;
      } else if (newVal) {
        node = this.$refs.topicTree.getNodeByKey(newVal);
        if (node) node.visible = true;
      } else if (oldVal) {
        node = this.$refs.topicTree.getNodeByKey(oldVal);
        if (node) node.visible = false;
      }
      this.topicObj.ParentTopicId = newVal;
    }
  },
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

