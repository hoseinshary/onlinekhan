<template>
  <base-modal-edit
    :title="provinceStore.modelName"
    :show="provinceStore.openModal.edit"
    @confirm="provinceStore.submitEdit"
    @reset="provinceStore.resetEdit"
    @close="provinceStore.OPEN_MODAL_EDIT(false)"
  >

    <!--ATI  -->
    <br />
    <div class="">
        <base-table :grid-data="lesson_UserStore.userData" :columns="userGridColumn">
          <template slot="Checked" slot-scope="data">
            <q-checkbox v-model="data.row.Checked" />
          </template>
        </base-table>
      </div>
  <!-- ATI -->

    <base-input :model="$v.province.Name" class="col-md-6"/>
    <base-input :model="$v.province.Code" class="col-md-6"/>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { provinceValidations } from "src/validations/ProvinceValidation";

@Component({
  validations: provinceValidations
})
export default class ProvinceEditVue extends Vue {
  $v: any;

  //#region ### data ###
  provinceStore = vxm.provinceStore;
  province = vxm.provinceStore.province;

  // ATI
  lesson_UserStore = vxm.lesson_UserStore;
  userGridColumn = [
    {
      title: "انتخاب",
      data: "Checked"
    },
    {
      title: "نام",
      data: "FullName"
    },
    {
      title: "کد ملی",
      data: "NationalNo"
    }
  ];
  provinceGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "کد",
      data: "Code"
    }
  ];
  // ATI


  //#endregion

  //#region ### hooks ###
  created() {
    this.provinceStore.SET_EDIT_VUE(this);

    // ati
    this.lesson_UserStore.fillUserList();
    // ati

  }
  //#endregion
}
</script>

