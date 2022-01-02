<template>
  <base-modal-edit :title="publisherStore.modelName"
    :show="publisherStore.openModal.edit"
    @confirm="publisherStore.submitEdit"
    @reset="publisherStore.resetEdit"
    @close="publisherStore.OPEN_MODAL_EDIT(false)">
    <base-input :model="$v.publisher.Name" class="col-md-6" />
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { publisherValidations } from "src/validations/PublisherValidation";

@Component({
  validations: publisherValidations
})
export default class PublisherEditVue extends Vue {
  $v: any;

  //#region ### data ###
  publisherStore = vxm.publisherStore;
  publisher = vxm.publisherStore.publisher;
  //#endregion

  //#region ### hooks ###
  created() {
    this.publisherStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

