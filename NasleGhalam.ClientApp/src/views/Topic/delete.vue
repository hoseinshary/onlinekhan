<template>
  <base-modal-delete
    :title="topicStore.modelName"
    :recordName="topicStore.recordName"
    :show="topicStore.openModal.delete"
    @confirm="confirm"
    @close="topicStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class TopicDeleteVue extends Vue {
  //#region ### data ###
  topicStore = vxm.topicStore;
  //#endregion

  //#region ### methods ###
  confirm() {
    this.topicStore.submitDelete(this);
  }
  //#endregion
}
</script>