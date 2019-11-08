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
    <div class="col-md-4">
      <q-select
        v-model="educationTree.id"
        :options="educationTree_GradeDdl"
        float-label="فیلتر درخت آموزش با مقطع"
        clearable
        class="s-q-input-border s-spacing"
      />

      <q-tree
        :nodes="educationTreeData"
        :expanded.sync="educationTree.expanded"
        :ticked.sync="educationTree.leafTicked"
        tick-strategy="leaf"
        class="tree-max-height s-spacing s-border"
        color="blue"
        node-key="Id"
      />
      <q-select
        v-model="thePackage.LessonIds"
        multiple
        filter
        :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
        float-label="انتخاب درس"
        class="s-q-input-border s-spacing s-border"
      />
    </div>

    <div class="col-md-8">
      <section class="row gutter-sm">
        <base-input :model="$v.thePackage.Name" class="col-md-6" />
        <q-field class="col-md-6">
          <div class="s-border q-pa-sm">
            <q-toggle
              v-model="$v.thePackage.IsActive.$model"
              :label="$v.thePackage.IsActive.$params.displayName.value"
              class="q-mx-md"
            />
          </div>
        </q-field>
        <base-input :model="$v.thePackage.Price" class="col-md-6" />
        <base-input :model="$v.thePackage.TimeDays" class="col-md-6" />
        <base-input :model="$v.thePackage.Description" class="col-12" />
        <q-field class="col-sm-6">
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
      </section>
    </div>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { packageValidations } from "src/validations/packageValidation";
import { EducationTreeState } from "../../utilities/enumeration";
import utilities from "../../utilities";

@Component({
  validations: packageValidations
})
export default class PackageCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  packageStore = vxm.packageStore;
  thePackage = vxm.packageStore.thePackage;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  educationTree = this.educationTreeStore.qTreeData;
  //#endregion

  //#region ### computed ###
  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    utilities.clearArray(this.thePackage.LessonIds);
    // clear educationTree leaf
    this.educationTree.leafTicked.splice(
      0,
      this.educationTree.leafTicked.length
    );

    let index = this.educationTree.expanded.indexOf(oldVal);
    if (index > -1) {
      this.educationTree.expanded.splice(index, 1);
    }

    if (this.educationTree.expanded.indexOf(newVal) == -1) {
      this.educationTree.expanded.push(newVal);
    }
  }

  @Watch("educationTree.leafTicked")
  educationTreeTickedIdsChanged(newVal) {
    utilities.clearArray(this.thePackage.LessonIds);
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.lessonStore.fillList();
    this.educationTreeStore.fillList();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.packageStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

