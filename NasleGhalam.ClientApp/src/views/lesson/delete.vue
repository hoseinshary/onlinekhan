<template>
  <my-modal-delete :title="modelName"
                   :recordName="recordName"
                   :show="isOpenModalDelete"
                   @confirm="submitDelete"
                   @close="toggleModalDeleteStore(false)">
  </my-modal-delete>
</template>

<script>
import { mapState, mapActions, mapGetters } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('lessonStore', [
      'toggleModalDeleteStore',
      'submitDeleteStore'
    ]),
    submitDelete() {
      this.submitDeleteStore(this);
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('lessonStore', {
      modelName: 'modelName',
      isOpenModalDelete: 'isOpenModalDelete'
    }),
    ...mapGetters('lessonStore', ['recordName'])
  }
};
</script>

