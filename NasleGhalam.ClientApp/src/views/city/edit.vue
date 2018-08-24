<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.cityObj.ProvinceId"
               :options="provinceDdl"
               class="col-md-6"
               clearable
               ref="provinceId" />

    <my-input :model="$v.cityObj.Name"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/cityViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('cityStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions('provinceStore', {
      fillProvinceDdlStore: 'fillDdlStore'
    }),
    submit() {
      this.cityObj.ProvinceName = this.$refs.provinceId.getSelectedLabel();
      this.submitEditStore();
    },
    modalOpen() {
      this.fillProvinceDdlStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('cityStore', {
      modelName: 'modelName',
      cityObj: 'cityObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState('provinceStore', {
      provinceDdl: 'provinceDdl'
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  }
};
</script>

