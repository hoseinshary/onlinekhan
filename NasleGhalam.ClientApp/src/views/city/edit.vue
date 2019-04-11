<template>
  <base-modal-edit
    :title="cityStore.modelName"
    :show="cityStore.openModal.edit"
    @confirm="cityStore.submitEdit"
    @reset="cityStore.resetEdit"
    @open="provinceStore.fillList"
    @close="cityStore.OPEN_MODAL_EDIT(false)"
  >
    <base-select :model="$v.city.ProvinceId" :options="provinceStore.ddl" filter class="col-md-6"/>
    <base-input :model="$v.city.Name" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { cityValidations } from "src/validations/CityValidation";

@Component({
  validations: cityValidations
})
export default class CityEditVue extends Vue {
  $v: any;

  //#region ### data ###
  cityStore = vxm.cityStore;
  provinceStore = vxm.provinceStore;
  city = vxm.cityStore.city;
  //#endregion

  //#region ### hooks ###
  created() {
    this.cityStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>