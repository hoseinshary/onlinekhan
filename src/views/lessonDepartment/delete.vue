<template>
  <base-modal-delete
    :title="lessonDepartmentStore.modelName"
    :recordName="lessonDepartmentStore.recordName"
    :show="lessonDepartmentStore.openModal.delete"
    @confirm="confirm"
    @close="lessonDepartmentStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class LessonDepartmentDeleteVue extends Vue {
  //#region ### data ###
  lessonDepartmentStore = vxm.lessonDepartmentStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.lessonDepartmentStore.submitDelete(this);
  }
  //#endregion
}
</script>