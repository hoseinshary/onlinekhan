<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.examObj.Name"
              class="col-md-6" />

    <my-input :model="$v.examObj.Date"
              class="col-md-6"
              @focus="show=!show" />
    <date-picker v-model="$v.examObj.Date.$model"
                 element="my-custom-editable-input"
                 :editable="true"
                 :show="show"
                 @close="show=false">
    </date-picker>

    <my-select :model="$v.examObj.EducationGroupId"
               :options="educationGroupDdl"
               class="col-md-6"
               clearable
               ref="educationGroupId" />
    <my-select :model="$v.examObj.EducationYearId"
               :options="educationYearDdl"
               class="col-md-6"
               clearable
               ref="educationYearId" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/examViewModel';
import { mapState, mapActions } from 'vuex';
import VuePersianDatetimePicker from 'vue-persian-datetime-picker';

export default {
  data() {
    return {
      show: false,
      educationYearDdl: [{ value: 2, label: '97' }]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('examStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions('educationGroupStore', {
      fillEducationGroupDdlStore: 'fillDdlStore'
    }),
    ...mapActions('educationGroupStore', {
      fillEducationGroupDdlStore: 'fillDdlStore'
    }),
    // ...mapActions('educationGroupStore', {
    //   fillYearDdlStore: 'fillDdlStore'
    // }),
    modalOpen() {
      this.fillEducationGroupDdlStore();
      // this.fillYearDdlStore();
    },
    submit() {
      this.examObj.EducationGroupName = this.$refs.educationGroupId.getSelectedLabel();
      this.examObj.EducationYearName = this.$refs.educationYearId.getSelectedLabel();
      this.submitEditStore();
    }
  },
  components: {
    datePicker: VuePersianDatetimePicker
  },
  /**
   * computed
   */
  computed: {
    // ...mapState('yearStore', {
    //   educationYearDdl: 'educationYearDdl'
    // }),
    ...mapState('educationGroupStore', {
      educationGroupDdl: 'educationGroupDdl'
    }),
    ...mapState('examStore', {
      modelName: 'modelName',
      examObj: 'examObj',
      isOpenModalEdit: 'isOpenModalEdit'
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

