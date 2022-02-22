<template>
  <base-modal-delete
    :title="lessonStore.modelName"
    :recordName="lessonStore.recordName"
    :show="lessonStore.openModal.delete"
    @confirm="confirm"
    @close="lessonStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class LessonDeleteVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.lessonStore.submitDelete(this);
  }
  //#endregion
}
</script>
