<template>
  <base-modal-edit
    :title="programStore.modelName"
    :show="programStore.openModal.edit"
    @confirm="programStore.submitEdit"
    @reset="programStore.resetEdit"
    @close="programStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.program.Name" class="col-md-6"/>
    <base-input :model="$v.program.Code" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { programValidations } from "src/validations/ProgramValidation";

@Component({
  validations: programValidations
})
export default class ProgramEditVue extends Vue {
  $v: any;

  //#region ### data ###
  programStore = vxm.programStore;
  program = vxm.programStore.program;
  //#endregion

  //#region ### hooks ###
  created() {
    this.programStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

