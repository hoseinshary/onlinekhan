<template>
  <base-modal-edit
    :title="tagStore.modelName"
    :show="tagStore.openModal.edit"
    @confirm="tagStore.submitEdit"
    @reset="tagStore.resetEdit"
    @close="tagStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.tag.Name" class="col-md-6"/>
    <base-field class="col-md-6" :model="$v.tag.IsSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { tagValidations } from "src/validations/TagValidation";

@Component({
  validations: tagValidations
})
export default class TagEditVue extends Vue {
  $v: any;

  //#region ### data ###
  tagStore = vxm.tagStore;
  tag = vxm.tagStore.tag;
  //#endregion

  //#region ### hooks ###
  created() {
    this.tagStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

