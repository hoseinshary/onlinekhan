<template>
  <base-modal-edit
    :title="packageStore.modelName"
    :show="packageStore.openModal.edit"
    @confirm="packageStore.submitEdit"
    @reset="packageStore.resetEdit"
    @close="packageStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.thePackage.Name" class="col-md-6" />
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { packageValidations } from "src/validations/packageValidation";

@Component({
  validations: packageValidations
})
export default class PackageEditVue extends Vue {
  $v: any;

  //#region ### data ###
  packageStore = vxm.packageStore;
  thePackage = vxm.packageStore.thePackage;
  //#endregion

  //#region ### hooks ###
  created() {
    this.packageStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

