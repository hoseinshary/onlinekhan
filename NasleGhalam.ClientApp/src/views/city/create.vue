<template>
  <base-modal-create
    :title="cityStore.modelName"
    :show="cityStore.openModal.create"
    @confirm="cityStore.submitCreate"
    @reset="cityStore.resetCreate"
    @open="provinceStore.fillList"
    @close="cityStore.OPEN_MODAL_CREATE(false)"
  >
    <base-select :model="$v.city.ProvinceId" :options="provinceStore.ddl" filter class="col-md-6" />
    <base-input :model="$v.city.Name" class="col-md-6" />
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { cityValidations } from "src/validations/CityValidation";

@Component({
  validations: cityValidations
})
export default class CityCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  cityStore = vxm.cityStore;
  provinceStore = vxm.provinceStore;
  city = vxm.cityStore.city;
  //#endregion

  //#region ### hooks ###
  created() {
    this.cityStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

