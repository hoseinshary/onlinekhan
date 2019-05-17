<template>
  <base-modal-create
    :title="writerStore.modelName"
    :show="writerStore.openModal.create"
    @confirm="writerStore.submitCreate"
    @reset="writerStore.resetCreate"
    @close="writerStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.writer.Name" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { writerValidations } from "src/validations/WriterValidation";

@Component({
  validations: writerValidations
})
export default class WriterCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  writerStore = vxm.writerStore;
  writer = vxm.writerStore.writer;
  //#endregion

  //#region ### hooks ###
  created() {
    this.writerStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

