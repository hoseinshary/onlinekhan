<template>
  <base-modal-create
    :title="cityStore.modelName"
    :show="cityStore.openModal.create"
    @confirm="submit"
    @reset="cityStore.resetCreate"
    @open="modalOpen"
    @close="cityStore.TOGGLE_MODAL_CREATE(false)"
  >
    <!-- <base-select
      :model="$v.cityObj.ProvinceId"
      :options="provinceDdl"
      class="col-md-6"
      clearable
      ref="provinceId"
    />-->
    <base-input :model="$v.city.Name" class="col-md-6"/>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import {
  cityValidations,
  validationMixin
} from "src/validations/CityValidation";

@Component({
  mixins: [validationMixin],
  validations: cityValidations
})
export default class CityCreateVue extends Vue {
  $v: any;

  //### data ###
  cityStore = vxm.city;
  city = vxm.city.city;
  //--------------------------------------------------

  //### methods ###
  modalOpen() {}
  submit() {}
  //--------------------------------------------------

  //### hooks ###
  created() {
    this.cityStore.SET_CREATE_VUE(this);
  }
  //--------------------------------------------------
}

// export default {
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("cityStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ]),
//     ...mapActions("provinceStore", {
//       fillProvinceDdlStore: "fillDdlStore"
//     }),
//     submit() {
//       this.cityObj.ProvinceName = this.$refs.provinceId.getSelectedLabel();
//       this.submitCreateStore();
//     },
//     modalOpen() {
//       this.fillProvinceDdlStore();
//     }
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("cityStore", {
//       modelName: "modelName",
//       cityObj: "cityObj",
//       isOpenModalCreate: "isOpenModalCreate"
//     }),
//     ...mapState("provinceStore", {
//       provinceDdl: "provinceDdl"
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

