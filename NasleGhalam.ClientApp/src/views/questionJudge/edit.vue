<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsStandard">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsDelete">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsUpdate">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.questionJudgeObj.IsLearning">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="false" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="true" />
      </template>
    </my-field>

    <my-input :model="$v.questionJudgeObj.ResponseSecond"
              class="col-md-6" />

    <!-- <my-select :model="$v.questionJudgeObj.LookupId_RepeatnessType" :options="" class="col-md-6" clearable />
          
          <my-select :model="$v.questionJudgeObj.LookupId_QuestionHardnessType" :options="" class="col-md-6" clearable />
          
           -->

  </my-modal-edit>
</template>

<script>
import viewModel from "viewModels/questionJudgeViewModel";
import { mapState, mapActions } from "vuex";

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions("questionJudgeStore", [
      "toggleModalEditStore",
      "editVueStore",
      "submitEditStore",
      "resetEditStore"
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionJudgeStore", {
      modelName: "modelName",
      questionJudgeObj: "questionJudgeObj",
      isOpenModalEdit: "isOpenModalEdit"
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

