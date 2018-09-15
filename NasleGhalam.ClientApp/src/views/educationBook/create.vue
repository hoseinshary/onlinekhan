<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size="lg"
                   @confirm="submit"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <section class="col-md-8">
      <div class="row gutter-sm q-ma-sm q-px-sm shadow-1">
        <my-select :model="$v.educationBookObj.EducationGroupId"
                   :options="educationGroupDdl"
                   class="col-md-6"
                   @change="educationGroupChange" />

        <my-select :model="$v.educationBookObj.EducationGroup_LessonId"
                   :options="educationGroup_LessonDdl"
                   class="col-md-6"
                   @change="fillTopicTree"
                   ref="EducationGroup_LessonId" />

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
                   clearable/>
        </q-field>
        <q-tree :nodes="topicTree"
                color="primary"
                :ticked.sync="educationBookObj.TopicIds"
                tick-strategy="leaf"
                accordion
                node-key="Id"
                :filter="topicFilter.value"
                ref="topicTree" />
      </div>
    </section>

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions({
      fillEducationGroupDdlStore: 'educationGroupStore/fillDdlStore',
      fillLessonByEducationGroupDdlStore:
        'lessonStore/fillLessonByEducationGroupDdlStore',
      fillTopicTreeStore: 'topicStore/GetAllTreeStore'
    }),
    modalOpen() {
      this.fillEducationGroupDdlStore();
    },
    educationGroupChange(value) {
      this.fillLessonByEducationGroupDdlStore(value);
    },
    fillTopicTree(value) {
      this.fillTopicTreeStore(value).then(() => {
        // after tree loaded
        this.$refs.topicTree.expandAll();
      });
    },
    submit(closeModal) {
      this.educationBookObj.LessonName = this.$refs.EducationGroup_LessonId.getSelectedLabel();
      this.submitCreateStore(closeModal);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationBookStore', {
      modelName: 'modelName',
      educationBookObj: 'educationBookObj',
      isOpenModalCreate: 'isOpenModalCreate',
      topicFilter: 'topicFilter'
    }),
    ...mapState({
      educationGroupDdl: s => s.educationGroupStore.educationGroupDdl,
      educationGroup_LessonDdl: s => s.lessonStore.educationGroup_LessonDdl,
      topicTree: s => s.topicStore.treeLst
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
    this.createVueStore(this);
  }
};
</script>

