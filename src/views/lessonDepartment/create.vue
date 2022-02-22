<template>
  <base-modal-create
    :title="lessonDepartmentStore.modelName"
    :show="lessonDepartmentStore.openModal.create"
    @confirm="lessonDepartmentStore.submitCreate"
    @reset="lessonDepartmentStore.resetCreate"
    @close="lessonDepartmentStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.lessonDepartment.Name" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { lessonDepartmentValidations } from "src/validations/LessonDepartmentValidation";

@Component({
  validations: lessonDepartmentValidations
})
export default class LessonDepartmentCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  lessonDepartmentStore = vxm.lessonDepartmentStore;
  lessonDepartment = vxm.lessonDepartmentStore.lessonDepartment;
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonDepartmentStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

