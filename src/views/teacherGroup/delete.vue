<template>
  <base-modal-delete
    :title="teacherGroupStore.modelName"
    :recordName="teacherGroupStore.recordName"
    :show="teacherGroupStore.openModal.delete"
    @confirm="confirm"
    @close="teacherGroupStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class ProvinceDeleteVue extends Vue {
  //#region ### data ###
  teacherGroupStore = vxm.teacherGroupStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.teacherGroupStore.submitDelete(this);
  }
  //#endregion
}
</script>