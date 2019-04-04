<template>
  <base-modal-edit
    :title="educationSubGroupStore.modelName"
    :show="educationSubGroupStore.openModal.edit"
    @confirm="educationSubGroupStore.submitEdit"
    @reset="educationSubGroupStore.resetEdit"
    @open="open"
    @close="educationSubGroupStore.OPEN_MODAL_EDIT(false)"
  >
    <base-select
      :model="$v.educationSubGroup.EducationTreeId"
      :options="educationTree_EducationGroupDdl"
      class="col-md-6"
      filter
    />
    <base-input :model="$v.educationSubGroup.Name" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationSubGroupValidations } from "src/validations/EducationSubGroupValidation";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  validations: educationSubGroupValidations
})
export default class EducationSubGroupEditVue extends Vue {
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
    this.educationSubGroupStore.SET_EDIT_VUE(this);
  }
  //--------------------------------------------------
}
</script>

