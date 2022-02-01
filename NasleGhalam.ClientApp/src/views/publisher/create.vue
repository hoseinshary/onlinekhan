<template>
  <base-modal-create
    :title="publisherStore.modelName"
    :show="publisherStore.openModal.create"
    @confirm="publisherStore.submitCreate"
    @reset="publisherStore.resetCreate"
    @close="publisherStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.publisher.Name" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { publisherValidations } from "src/validations/PublisherValidation";

@Component({
  validations: publisherValidations
})
export default class PublisherCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  publisherStore = vxm.publisherStore;
  publisher = vxm.publisherStore.publisher;
  //#endregion

  //#region ### hooks ###
  created() {
    this.publisherStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

