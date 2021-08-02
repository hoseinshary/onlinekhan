<template>
  <base-modal-create
    :title="studentMajorListStore.modelName"
    :show="studentMajorListStore.openModal.create"
    @confirm="studentMajorListStore.submitCreate"
    @reset="studentMajorListStore.resetCreate"
    @close="studentMajorListStore.OPEN_MODAL_CREATE(false)"
  >

   <base-select
        :model="fieldFilter"
        :options="fieldDdl"
        class="col-md-3"
      />
    <base-input :model="$v.studentMajorList.Title" class="col-md-6"/>
    <base-input :model="$v.studentMajorList.Code" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

import { studentMajorListValidations } from "src/validations/StudentMajorListValidation";
import { Field } from "src/utilities/enumeration";

@Component({
  validations: studentMajorListValidations
})
export default class StudentMajorListCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  studentMajorList = vxm.studentMajorListStore.studentMajorList;

  fieldFilter :Field = Field["تجربی"];
  //#endregion

   get fieldDdl() {
    return util.enumToDdl(Field);
  }

  //#region ### hooks ###
  created() {
    this.studentMajorListStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

