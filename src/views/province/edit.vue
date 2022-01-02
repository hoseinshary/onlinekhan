<template>
  <base-modal-edit
    :title="provinceStore.modelName"
    :show="provinceStore.openModal.edit"
    @confirm="provinceStore.submitEdit"
    @reset="provinceStore.resetEdit"
    @close="provinceStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.province.Name" class="col-md-6"/>
    <base-input :model="$v.province.Code" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { provinceValidations } from "src/validations/ProvinceValidation";

@Component({
  validations: provinceValidations
})
export default class ProvinceEditVue extends Vue {
  $v: any;

  //#region ### data ###
  provinceStore = vxm.provinceStore;
  province = vxm.provinceStore.province;
  //#endregion

  //#region ### hooks ###
  created() {
    this.provinceStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

