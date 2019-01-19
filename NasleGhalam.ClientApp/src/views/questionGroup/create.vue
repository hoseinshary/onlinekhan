<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)">

    <my-input :model="$v.questionGroupObj.Title"
              class="col-md-6" />

    <q-field class="col-12">
      <q-uploader url="wordUrl"
                  float-label="فایل Word"
                  name="word"
                  hide-upload-button
                  auto-expand
                  ref="wordFileUpload"
                  extensions=".docx" />
    </q-field>

    <q-field class="col-12">
      <q-uploader url="excelUrl"
                  float-label="فایل Excel"
                  name="excel"
                  hide-upload-button
                  auto-expand
                  ref="excelFileUpload"
                  extensions=".xlsx" />
    </q-field>
  </my-modal-create>
</template>

<script>
import viewModel from "viewModels/questionGroup/questionGroupViewModel";
import { mapState, mapActions } from "vuex";

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions("questionGroupStore", [
      "toggleModalCreateStore",
      "createVueStore",
      "submitCreateStore",
      "resetCreateStore"
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionGroupStore", {
      modelName: "modelName",
      questionGroupObj: "questionGroupObj",
      isOpenModalCreate: "isOpenModalCreate"
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

