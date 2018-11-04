<template>
  <my-modal-delete :title="modelName"
                   :recordName="recordName"
                   :show="isOpenModalDelete"
                   @confirm="submitDelete"
                   @close="toggleModalDeleteStore(false);">
  </my-modal-delete>
</template>

<script>
import { mapState, mapActions, mapGetters } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('educationTreeStore', ['toggleModalDeleteStore', 'submitDeleteStore']),
    submitDelete() {
      this.submitDeleteStore(this);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('educationTreeStore', {
      modelName: 'modelName',
      isOpenModalDelete: 'isOpenModalDelete'
    }),
    ...mapGetters('educationTreeStore', ['recordName'])
  }
};
</script>

