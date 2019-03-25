<template>
  <base-modal-create
    :title="provinceStore.modelName"
    :show="provinceStore.openModal.create"
    @confirm="provinceStore.submitCreate"
    @reset="provinceStore.resetCreate"
    @close="provinceStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.province.Name" class="col-md-6"/>
    <base-input :model="$v.province.Code" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { provinceValidations } from "src/validations/ProvinceValidation";

@Component({
  validations: provinceValidations
})
export default class ProvinceCreateVue extends Vue {
  $v: any;

  //### data ###
  provinceStore = vxm.provinceStore;
  province = vxm.provinceStore.province;
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.provinceStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}
</script>

