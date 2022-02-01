<template>
  <base-modal-create
    :title="packageStore.modelName"
    :show="packageStore.openModal.create"
    size="xl"
    @confirm="packageStore.submitCreate"
    @reset="packageStore.resetCreate"
    @close="packageStore.OPEN_MODAL_CREATE(false)"
    @open="open"
  >
    <base-select
      :model="$v.thePackage.LessonIds"
      :options="lessonStore.ddlByEducationTreeIds(educationTreeLeafTickedProp)"
      class="col-md-4"
      filter
      multiple
    />
    <base-input :model="$v.thePackage.Name" class="col-md-4" />
    <q-field class="col-md-4">
      <div class="s-border q-pa-sm">
        <q-toggle
          v-model="$v.thePackage.IsActive.$model"
          :label="$v.thePackage.IsActive.$params.displayName.value"
          class="q-mx-md"
        />
      </div>
    </q-field>
    <base-input :model="$v.thePackage.Price" class="col-md-4" @focus="$event.target.select()" />
    <base-input :model="$v.thePackage.TimeDays" class="col-md-4" @focus="$event.target.select()" />
    <q-field class="col-md-4">
      <q-uploader
        url="url"
        float-label="تصویر"
        name="img"
        hide-upload-button
        auto-expand
        ref="fileUpload"
        extensions=".jpg,.jpeg,.png"
      />
    </q-field>
    <base-input :model="$v.thePackage.Description" class="col-md-12" />
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { packageValidations } from "src/validations/packageValidation";
import utilities from "../../utilities";

@Component({
  validations: packageValidations
})
export default class PackageCreateVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) educationTreeLeafTickedProp;
  //#endregion

  //#region ### data ###
  packageStore = vxm.packageStore;
  thePackage = vxm.packageStore.thePackage;
  lessonStore = vxm.lessonStore;
  //#endregion

  //#region ### methods ###
  open() {
    this.lessonStore.fillList();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.packageStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

