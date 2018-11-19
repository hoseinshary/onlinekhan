<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row gutter-sm">

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
            <my-btn-create v-if="pageAccess.canCreate"
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
      educationBookIndexObj: {
        GradeId: undefined,
        GradeLevelId: undefined
      },
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
      this.$v.educationBookIndexObj.$touch();
      if (this.$v.educationBookIndexObj.$error) {
        return;
      }
      // reset data on modal show
      this.resetCreateStore();
      //set GradeLevelId
      this.educationBookObj.GradeLevelId = this.educationBookIndexObj.GradeLevelId;
      // show modal
      this.toggleModalCreateStore(true);
    },
    showModalEdit(id) {
      this.$v.educationBookIndexObj.$touch();
      if (this.$v.educationBookIndexObj.$error) {
        return;
      }
      // reset data on modal show
      this.resetEditStore();
      var vm = this;
      // get data by id
      this.getByIdStore(id).then(data => {
        this.fillLessonByEducationGroupDdlStore(data.EducationGroupId);
        this.fillTopicTreeStore(data.EducationGroup_LessonId).then(() => {
          // after tree loaded
          vm.$refs.modalEdit.$refs.topicTree.expandAll();
        });
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
      educationBookGridData: 'educationBookGridData'
    }),
    ...mapState('educationTreeStore', {
      educationTree_GradeDdl: 'gradeDdl'
    }),
    ...mapState('lessonStore', {
      lessonDdl: 'allObjDdl'
    })
  },
  created() {
    this.getAllGrade();
    this.fillEducationTreeStore();
  }
};
</script>

