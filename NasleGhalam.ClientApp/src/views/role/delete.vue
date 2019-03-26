<template>
  <base-modal-delete
    :title="roleStore.modelName"
    :recordName="roleStore.recordName"
    :show="roleStore.openModal.delete"
    @confirm="roleStore.submitDelete"
    @close="roleStore.OPEN_MODAL_DELETE(false)"
  ></base-modal-delete>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component
export default class RoleDeleteVue extends Vue {
  //### data ###
  roleStore = vxm.roleStore;
  //--------------------------------------------------
}
</script>


