<template>
  <base-modal-edit
    :title="writerStore.modelName"
    :show="writerStore.openModal.edit"
    @confirm="writerStore.submitEdit"
    @reset="writerStore.resetEdit"
    @close="writerStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.writer.Name" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { writerValidations } from "src/validations/WriterValidation";

@Component({
  validations: writerValidations
})
export default class WriterEditVue extends Vue {
  $v: any;

  //#region ### data ###
  writerStore = vxm.writerStore;
  writer = vxm.writerStore.writer;
  //#endregion

  //#region ### hooks ###
  created() {
    this.writerStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

