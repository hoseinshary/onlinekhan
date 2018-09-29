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
        <my-table :grid-data="questionGridData"
                  :columns="questionGridColumn"
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
        </my-table>
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
    var pageAccess = this.$util.initAccess('/question');
    return {
      pageAccess,
      questionGridColumn: [
        // {
        //   title: 'Context',
        //   data: 'Context'
        // },
        {
          title: 'شماره سوال',
          data: 'QuestionNumber'
        },
        // {
        //   title: 'LookupId_QuestionType',
        //   data: 'LookupId_QuestionType'
        // },
        // {
        //   title: 'QuestionPoint',
        //   data: 'QuestionPoint'
        // },
        // {
        //   title: 'LookupId_QuestionHardnessType',
        //   data: 'LookupId_QuestionHardnessType'
        // },
        // {
        //   title: 'LookupId_RepeatnessType',
        //   data: 'LookupId_RepeatnessType'
        // },
        // {
        //   title: 'UseEvaluation',
        //   data: 'UseEvaluation'
        // },
        // {
        //   title: 'IsStandard',
        //   data: 'IsStandard'
        // },
        // {
        //   title: 'LookupId_AuthorType',
        //   data: 'LookupId_AuthorType'
        // },
        // {
        //   title: 'AuthorName',
        //   data: 'AuthorName'
        // },
        // {
        //   title: 'LookupId_AreaType',
        //   data: 'LookupId_AreaType'
        // },
        // {
        //   title: 'ResponseSecond',
        //   data: 'ResponseSecond'
        // },
        // {
        //   title: 'Description',
        //   data: 'Description'
        // },
        // {
        //   title: 'FileName',
        //   data: 'FileName'
        // },
        // {
        //   title: 'InsertDateTime',
        //   data: 'InsertDateTime'
        // },
        // {
        //   title: 'UserId',
        //   data: 'UserId'
        // },
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
    ...mapActions('questionStore', [
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
    ...mapState('questionStore', {
      modelName: 'modelName',
      questionGridData: 'questionGridData'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

