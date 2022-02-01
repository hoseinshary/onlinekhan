<template>
  <base-modal-create
    :title="programStore.modelName"
    :show="programStore.openModal.create"
     size="lg"
    @confirm="programStore.submitCreate"
    @reset="programStore.resetCreate"
    @close="programStore.OPEN_MODAL_CREATE(false)"
     
  >
    <base-input :model="$v.program.Name" class="col-md-3"/>
    <base-input :model="$v.program.Description" class="col-md-4"/>


 <fieldset class="col-12" >
            <legend>شنبه</legend>
            <section class="row">
              
              <base-select
          :model="$v.lesson.LookupId_Nezam"
          :options="lookupStore.topicNezamDdl"
          class="col-md-6"
        />
            </section>
          </fieldset>

  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { programValidations } from "src/validations/ProgramValidation";

@Component({
  validations: programValidations
})
export default class ProgramCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  programStore = vxm.programStore;
  program = vxm.programStore.program;
  //#endregion

  //#region ### hooks ###
  created() {
    this.programStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

