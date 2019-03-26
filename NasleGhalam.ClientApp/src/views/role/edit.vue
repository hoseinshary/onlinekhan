<template>
  <base-modal-edit
    :title="roleStore.modelName"
    :show="roleStore.openModal.edit"
    @confirm="roleStore.submitEdit"
    @reset="roleStore.resetEdit"
    @close="roleStore.OPEN_MODAL_EDIT(false)"
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
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { roleValidations } from "src/validations/role/roleValidation";

@Component({
  validations: roleValidations
})
export default class RoleEditVue extends Vue {
  $v: any;

  //### data ###
  roleStore = vxm.roleStore;
  role = vxm.roleStore.role;
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.roleStore.SET_EDIT_VUE(this);
  }
  //--------------------------------------------------
}
</script>