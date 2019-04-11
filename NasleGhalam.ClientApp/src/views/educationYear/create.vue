<template>
  <base-modal-create
    :title="educationYearStore.modelName"
    :show="educationYearStore.openModal.create"
    @confirm="educationYearStore.submitCreate"
    @reset="educationYearStore.resetCreate"
    @close="educationYearStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.educationYear.Name" class="col-md-6"/>
    <base-field class="col-md-6" :model="$v.educationYear.IsActiveYear">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model"/>
      </template>
    </base-field>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationYearValidations } from "src/validations/EducationYearValidation";

@Component({
  validations: educationYearValidations
})
export default class EducationYearCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  educationYearStore = vxm.educationYearStore;
  educationYear = vxm.educationYearStore.educationYear;
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationYearStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>