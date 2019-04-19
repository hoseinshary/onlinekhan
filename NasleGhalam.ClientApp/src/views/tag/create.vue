<template>
  <base-modal-create
    :title="tagStore.modelName"
    :show="tagStore.openModal.create"
    @confirm="tagStore.submitCreate"
    @reset="tagStore.resetCreate"
    @close="tagStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.tag.Name" class="col-md-6"/>
    <base-field class="col-md-6" :model="$v.tag.IsSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { tagValidations } from "src/validations/TagValidation";

@Component({
  validations: tagValidations
})
export default class TagCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  tagStore = vxm.tagStore;
  tag = vxm.tagStore.tag;
  //#endregion

  //#region ### hooks ###
  created() {
    this.tagStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

