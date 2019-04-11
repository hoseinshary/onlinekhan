<template>
  <base-modal-create
    :title="roleStore.modelName"
    :show="roleStore.openModal.create"
    @confirm="roleStore.submitCreate"
    @reset="roleStore.resetCreate"
    @close="roleStore.OPEN_MODAL_CREATE(false)"
  >
    <base-input :model="$v.role.Name" class="col-md-6"/>
    <base-input :model="$v.role.Level" class="col-md-6"/>
    <base-field class="col-12" :model="$v.role.UserType">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="0" label="سازمانی"/>
        <q-radio v-model="data.obj.$model" :val="1" label="دانش آموز"/>
        <q-radio v-model="data.obj.$model" :val="2" label="معلم"/>
      </template>
    </base-field>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { roleValidations } from "src/validations/roleValidation";

@Component({
  validations: roleValidations
})
export default class RoleCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  roleStore = vxm.roleStore;
  role = vxm.roleStore.role;
  //#endregion

  //#region ### hooks ###
  created() {
    this.roleStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>
