<template>
  <base-modal-edit
    :title="lessonDepartmentStore.modelName"
    :show="lessonDepartmentStore.openModal.edit"
    @confirm="lessonDepartmentStore.submitEdit"
    @reset="lessonDepartmentStore.resetEdit"
    @close="lessonDepartmentStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.lessonDepartment.Name" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { lessonDepartmentValidations } from "src/validations/LessonDepartmentValidation";

@Component({
  validations: lessonDepartmentValidations
})
export default class LessonDepartmentEditVue extends Vue {
  $v: any;

  //#region ### data ###
  lessonDepartmentStore = vxm.lessonDepartmentStore;
  lessonDepartment = vxm.lessonDepartmentStore.lessonDepartment;
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonDepartmentStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

