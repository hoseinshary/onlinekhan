<template>
  <my-modal-create
    :title="modelName"
    :show="isOpenModalCreate"
    @confirm="submit"
    @reset="resetCreateStore"
    @open="modalOpen"
    @close="toggleModalCreateStore(false)"
  >
    <my-select
      :model="$v.educationSubGroupObj.EducationTreeId"
      :options="educationGroupDdl"
      class="col-md-6"
      clearable
      ref="EducationTreeId"
    />
    <my-input :model="$v.educationSubGroupObj.Name" class="col-md-6"/>
  </my-modal-create>
</template>

<script>
  import viewModel from "viewModels/educationSubGroupViewModel";
  import { mapState, mapActions } from "vuex";

  export default {
    /**
     * methods
     */
    methods: {
      ...mapActions("educationSubGroupStore", [
        "toggleModalCreateStore",
        "createVueStore",
        "submitCreateStore",
        "resetCreateStore"
      ]),
      ...mapActions("educationTreeStore", {
        fillEducationGroupDdl: "fillEducationGroupDdl"
      }),
      submit(closeModal) {
        this.educationSubGroupObj.EducationTreeName = this.$refs.EducationTreeId.getSelectedLabel();
        this.submitCreateStore(closeModal);
      },
      modalOpen() {
        this.fillEducationGroupDdl();
      }
    },
    /**
     * computed
     */
    computed: {
      ...mapState("educationSubGroupStore", {
        modelName: "modelName",
        educationSubGroupObj: "educationSubGroupObj",
        isOpenModalCreate: "isOpenModalCreate"
      }),
      ...mapState("educationTreeStore", {
        educationGroupDdl: "educationGroupDdl"
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

