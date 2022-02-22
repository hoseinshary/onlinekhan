<template>
  <base-modal-edit
    :title="educationYearStore.modelName"
    :show="educationYearStore.openModal.edit"
    @confirm="educationYearStore.submitEdit"
    @reset="educationYearStore.resetEdit"
    @close="educationYearStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.educationYear.Name" class="col-md-6"/>
    <base-field class="col-md-6" :model="$v.educationYear.IsActiveYear">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model"/>
      </template>
    </base-field>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationYearValidations } from "src/validations/EducationYearValidation";

@Component({
  validations: educationYearValidations
})
export default class EducationYearEditVue extends Vue {
  $v: any;

  //#region ### data ###
  educationYearStore = vxm.educationYearStore;
  educationYear = vxm.educationYearStore.educationYear;
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationYearStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>