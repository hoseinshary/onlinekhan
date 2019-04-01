<template>
  <base-modal-create
    :title="educationTreeStore.modelName"
    :show="educationTreeStore.openModal.create"
    @confirm="educationTreeStore.submitCreate"
    @reset="educationTreeStore.resetCreate"
    @open="lookupStore.fillEducationTreeState"
    @close="educationTreeStore.OPEN_MODAL_CREATE(false)"
  >
    <div class="col-12">
      <label>ریشه:</label>
      <label class="text-bold text-red">
        {{educationTree.ParentEducationTree
        ? this.educationTree.ParentEducationTree.Name
        : ""}}
      </label>
    </div>
    <base-input :model="$v.educationTree.Name" class="col-md-6"/>
    <base-select
      :model="$v.educationTree.LookupId_EducationTreeState"
      :options="lookupStore.educationTreeStateDdl"
      class="col-md-6"
      clearable
    />
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationTreeValidations } from "src/validations/EducationTreeValidation";

@Component({
  validations: educationTreeValidations
})
export default class EducationTreeCreateVue extends Vue {
  $v: any;

  //### data ###
  educationTreeStore = vxm.educationTreeStore;
  lookupStore = vxm.lookupStore;
  educationTree = vxm.educationTreeStore.educationTree;
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.educationTreeStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}
</script>
