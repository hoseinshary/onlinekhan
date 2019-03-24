<template>
  <base-modal-create
    :title="provinceStore.modelName"
    :show="provinceStore.openModal.create"
    @confirm="submit"
    @reset="provinceStore.resetCreate"
    @close="provinceStore.TOGGLE_MODAL_CREATE(false)"
  >
    <base-input :model="$v.province.Name" class="col-md-6"/>
    <base-input :model="$v.province.Code" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { provinceValidations } from "src/validations/ProvinceValidation";

@Component({
  validations: provinceValidations
})
export default class ProvinceCreateVue extends Vue {
  $v: any;

  //### data ###
  provinceStore = vxm.provinceStore;
  province = vxm.provinceStore.province;
  //--------------------------------------------------

  //### methods ###
  modalOpen() {}
  submit(closeModal: boolean) {
    this.provinceStore.submitCreate(closeModal);
  }
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.provinceStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}
// export default {
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("provinceStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ])
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("provinceStore", {
//       modelName: "modelName",
//       provinceObj: "provinceObj",
//       isOpenModalCreate: "isOpenModalCreate"
//     })
//   },
//   /**
//    * validations
//    */
//   validations: viewModel,
//   /**
//    * created
//    */
//   created() {
//     this.createVueStore(this);
//   }
// };
</script>

