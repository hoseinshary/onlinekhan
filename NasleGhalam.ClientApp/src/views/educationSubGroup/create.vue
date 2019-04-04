<template>
  <base-modal-create
    :title="educationSubGroupStore.modelName"
    :show="educationSubGroupStore.openModal.create"
    @confirm="educationSubGroupStore.submitCreate"
    @reset="educationSubGroupStore.resetCreate"
    @open="open"
    @close="educationSubGroupStore.OPEN_MODAL_CREATE(false)"
  >
    <base-select
      :model="$v.educationSubGroup.EducationTreeId"
      :options="educationTree_EducationGroupDdl"
      class="col-md-6"
      filter
    />
    <base-input :model="$v.educationSubGroup.Name" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationSubGroupValidations } from "src/validations/EducationSubGroupValidation";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  validations: educationSubGroupValidations
})
export default class EducationSubGroupCreateVue extends Vue {
  $v: any;

  //### data ###
  educationSubGroupStore = vxm.educationSubGroupStore;
  educationTreeStore = vxm.educationTreeStore;
  educationSubGroup = vxm.educationSubGroupStore.educationSubGroup;
  //--------------------------------------------------

  //### getters ###
  get educationTree_EducationGroupDdl() {
    return this.educationTreeStore.byState(EducationTreeState.EducationGroup);
  }
  //--------------------------------------------------

  //### methods ###
  open() {
    this.educationTreeStore.fillList();
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.educationSubGroupStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}
</script>

