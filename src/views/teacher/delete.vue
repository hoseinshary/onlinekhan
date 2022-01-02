<template>
  <base-modal-delete
    :title="teacherStore.modelName"
    :recordName="teacherStore.recordName"
    :show="teacherStore.openModal.delete"
    @confirm="confirm"
    @close="teacherStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class TeacherDeleteVue extends Vue {
  //#region ### data ###
  teacherStore = vxm.teacherStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.teacherStore.submitDelete(this);
  }
  //#endregion
}
</script>