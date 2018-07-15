<template>
  <my-modal-create :title="modelName"
                   size="sm"
                   height="55%"
                   :openModal="isOpenModalCreate"
                   @confirm="confirmCreate"
                   @reset="resetStore($v)"
                   @toggle="$event?openModalCreateStore($event):closeModalCreateStore($event)">

    <my-input :model="$v.instanceObj.Name"
              class="col-12">
    </my-input>

    <my-input :model="$v.instanceObj.Priority"
              class="col-12">
    </my-input>

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/gradeViewModel';
import { mapState, mapActions } from 'vuex';
export default {
  /**
   * data
   */
  data() {
    return {};
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('grade', [
      'openModalCreateStore',
      'closeModalCreateStore',
      'submitCreateStore',
      'resetStore'
    ]),
    confirmCreate(closeModal) {
      // submiting data
      this.submitCreateStore({
        closeModal,
        vm: this
      });
    }
  },
  validations: viewModel,
  computed: {
    ...mapState('grade', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
      isOpenModalCreate: 'isOpenModalCreate'
    })
  }
};
</script>

