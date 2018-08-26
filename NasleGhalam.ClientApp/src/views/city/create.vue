<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submit"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.cityObj.ProvinceId"
               :options="provinceDdl"
               class="col-md-6"
               clearable
               ref="provinceId" />

    <my-input :model="$v.cityObj.Name"
              class="col-md-6" />

  </my-modal-create>
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
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions('provinceStore', {
      fillProvinceDdlStore: 'fillDdlStore'
    }),
    submit() {
      this.cityObj.ProvinceName = this.$refs.provinceId.getSelectedLabel();
      this.submitCreateStore();
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
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

