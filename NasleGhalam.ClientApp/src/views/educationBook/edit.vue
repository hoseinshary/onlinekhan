<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 size="lg"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <section class="col-md-8">
      <div class="row gutter-sm q-ma-sm q-px-sm shadow-1">
        <my-input :model="$v.educationBookObj.Name"
                  class="col-md-6" />

        <my-input :model="$v.educationBookObj.PublishYear"
                  class="col-md-6" />

        <my-field class="col-md-4"
                  :model="$v.educationBookObj.IsExamSource">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model" />
          </template>
        </my-field>

        <my-field class="col-md-4"
                  :model="$v.educationBookObj.IsActive">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model" />
          </template>
        </my-field>

        <my-field class="col-md-4"
                  :model="$v.educationBookObj.IsChanged">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model" />
          </template>
        </my-field>
      </div>
    </section>
    <section class="col-md-4">
      <div class="row gutter-sm q-ma-sm q-px-sm shadow-1">
        <q-field class="col-12">
          <q-input v-model="topicFilter.value"
                   float-label="جستجوی مبحث"
                   clearable />
        </q-field>
        <q-tree :nodes="topicTreeData"
                color="primary"
                :ticked.sync="educationBookObj.TopicIds"
                tick-strategy="leaf"
                accordion
                node-key="Id"
                :filter="topicFilter.value"
                ref="topicTree" />
      </div>
    </section>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/educationBook/educationBookCrudViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationBookStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions({
      fillTopicTreeStore: 'topicStore/fillTreeStore'
    }),
    modalOpen() {
      this.fillTopicTreeStore(this.educationBookObj.LessonId).then(() => {
        // after tree loaded
        this.$refs.topicTree.expandAll();
      });
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationBookStore', {
      modelName: 'modelName',
      educationBookObj: 'educationBookObj',
      isOpenModalEdit: 'isOpenModalEdit',
      topicFilter: 'topicFilter'
    }),
    ...mapState({
      topicTreeData: s => s.topicStore.topicTreeData
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  }
};
</script>

