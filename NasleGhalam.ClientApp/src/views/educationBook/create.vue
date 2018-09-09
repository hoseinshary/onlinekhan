<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size="lg"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <section class="col-md-8">
      <div class="row gutter-sm q-ma-sm q-px-sm shadow-1">
        <my-select :model="$v.educationBookObj.EducationGroupId"
                   :options="educationGroupDdl"
                   class="col-md-6"
                   clearable
                   @change="educationGroupChange" />

        <my-select :model="$v.educationBookObj.EducationGroup_LessonId"
                   :options="educationGroup_LessonDdl"
                   class="col-md-6"
                   clearable />

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
          <q-input v-model="filter"
                   float-label="درس"
                   clearable/>
        </q-field>
        <q-tree :nodes="tickable"
                color="primary"
                default-expand-all
                :ticked.sync="ticked"
                :tick-strategy="tickStrategy"
                accordion
                node-key="label"
                :filter="filter" />
      </div>
    </section>

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/educationBookViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  data: () => ({
    filter: null,
    ticked: [],
    tickStrategy: 'leaf',
    tickable: [
      {
        label: 'Satisfied customers',
        children: [
          {
            label: 'Good food',
            icon: 'restaurant_menu',
            children: [
              { label: 'Quality ingredients' },
              { label: 'Good recipe' }
            ]
          },
          {
            label: 'Good service',
            icon: 'room_service',
            children: [
              { label: 'Prompt attention' },
              { label: 'Professional waiter' }
            ]
          },
          {
            label: 'Pleasant surroundings',
            icon: 'photo',
            children: [
              {
                label: 'Happy atmosphere (not tickable)',
                tickable: false
              },
              {
                label: 'Good table presentation (disabled node)',
                disabled: true
              },
              {
                label: 'Pleasing decor'
              }
            ]
          },
          {
            label: 'Extra information (has no tick)',
            noTick: true,
            icon: 'photo'
          },
          {
            label: 'Forced tick strategy (to "strict" in this case)',
            tickStrategy: 'strict',
            icon: 'school',
            children: [
              {
                label: 'Happy atmosphere'
              },
              {
                label: 'Good table presentation'
              },
              {
                label: 'Very pleasing decor'
              }
            ]
          }
        ]
      }
    ]
  }),
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
        'lessonStore/fillLessonByEducationGroupDdlStore'
    }),
    modalOpen() {
      this.fillEducationGroupDdlStore();
    },
    educationGroupChange(value) {
      this.fillLessonByEducationGroupDdlStore(value);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationBookStore', {
      modelName: 'modelName',
      educationBookObj: 'educationBookObj',
      isOpenModalCreate: 'isOpenModalCreate'
    }),
    ...mapState({
      educationGroupDdl: s => s.educationGroupStore.educationGroupDdl,
      educationGroup_LessonDdl: s => s.lessonStore.educationGroup_LessonDdl
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

