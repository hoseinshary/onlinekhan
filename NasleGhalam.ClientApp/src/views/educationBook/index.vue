<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row gutter-sm">

          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <my-select :model="$v.educationBookIndexObj.EducationTreeId_Grade"
                         :options="educationTree_GradeDdl"
                         @change="gradeDdlChange" />

              <q-tree :nodes="educationTreeData"
                      color="primary"
                      tick-strategy="leaf"
                      accordion
                      :ticked.sync="educationBookIndexObj.EducationTreeIds"
                      node-key="Id"
                      ref="educationTree" />

              <my-select :model="$v.educationBookIndexObj.LessonId"
                         :options="lessonDdl"
                         class="q-pt-lg"
                         @change="lessonDdlChange" />
            </section>
          </div>

          <div class="col-sm-7">
            <my-btn-create v-if="pageAccess.canCreate && educationBookIndexObj.LessonId > 0"
                           :label="`ایجاد (${modelName}) جدید`"
                           @click="showModalCreate" />
            <br>
            <my-table :grid-data="educationBookGridData"
                      :columns="educationBookGridColumn"
                      hasIndex>

              <template slot="IsActive"
                        slot-scope="data">
                <q-checkbox v-model="data.row.IsActive"
                            readonly />
              </template>

              <template slot="IsExamSource"
                        slot-scope="data">
                <q-checkbox v-model="data.row.IsExamSource"
                            readonly />
              </template>

              <template slot="IsChanged"
                        slot-scope="data">
                <q-checkbox v-model="data.row.IsChanged"
                            readonly />
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
        </section>
      </div>
    </my-panel>

    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"
                ref="modalEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import viewModel from 'viewModels/educationBook/educationBookIndexViewModel';

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
    var pageAccess = this.$util.initAccess('/educationBook');
    return {
      pageAccess,
      educationTreeData: [],
      educationBookGridColumn: [
        {
          title: 'نام کتاب',
          data: 'Name'
        },
        {
          title: 'سال انتشار',
          data: 'PublishYear'
        },
        {
          title: 'منبع کنکوری',
          data: 'IsExamSource'
        },
        {
          title: 'فعال',
          data: 'IsActive'
        },
        {
          title: 'تغییر نسبت به سال قبل',
          data: 'IsChanged'
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
    ...mapActions('educationBookStore', [
      'toggleModalCreateStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore'
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
      // show modal
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
      this.educationBookIndexObj.LessonId = 0;
      this.educationBookIndexObj.EducationTreeIds = [];
      this.fillEducationTreeByGradeIdStore(val).then(treeData => {
        self.educationTreeData = [treeData];
        setTimeout(() => {
          self.$refs.educationTree.expandAll();
        }, 300);
      });
    },
    lessonDdlChange(val) {
      this.fillGridStore(val);
      this.educationBookObj.LessonId = val;
    }
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * computed
   */
  computed: {
    ...mapState('educationBookStore', {
      modelName: 'modelName',
      educationBookObj: 'educationBookObj',
      educationBookIndexObj: 'educationBookIndexObj',
      educationBookGridData: 'educationBookGridData'
    }),
    ...mapState('educationTreeStore', {
      educationTree_GradeDdl: 'gradeDdl'
    }),
    ...mapState('lessonStore', {
      lessonDdl: 'allObjDdl'
    })
  },
  watch: {
    'educationBookIndexObj.EducationTreeIds'(val) {
      this.educationBookIndexObj.LessonId = 0;
      this.fillLessonDdlStore(val);
    }
  },
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

