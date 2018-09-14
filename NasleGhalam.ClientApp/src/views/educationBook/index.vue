<template>
  <section class="col-md-11">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">
        <section class="row gutter-md">
          <my-select :model="$v.educationBookIndexObj.GradeId"
                     :options="gradeDdl"
                     class="col-md-3 col-sm-4"
                     @change="fillGradeLevelByGradeIdDdl($event)" />

          <my-select :model="$v.educationBookIndexObj.GradeLevelId"
                     :options="gradeLevelByGradeDdl"
                     class="col-md-3 col-sm-4"
                     @change="fillGridStore($event)" />

          <div class="col-md-3 col-sm-4">
            <my-btn-create v-if="pageAccess.canCreate"
                           :label="`ایجاد (${modelName}) جدید`"
                           @click="showModalCreate" />
          </div>

        </section>

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
    </my-panel>

    <!-- modals -->
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <!-- <modal-edit v-if="pageAccess.canEdit"></modal-edit> -->
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
          title: 'درس',
          data: 'LessonName'
        },
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
    ...mapActions({
      fillGradeDdl: 'gradeStore/fillDdlStore',
      fillGradeLevelByGradeIdDdl: 'gradeLevelStore/fillGradeLevelByGradeId'
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
    ...mapState({
      gradeDdl: s => s.gradeStore.gradeDdl,
      gradeLevelByGradeDdl: s => s.gradeLevelStore.gradeLevelByGradeDdl
    })
  },
  created() {
    this.fillGradeDdl();
  }
};
</script>

