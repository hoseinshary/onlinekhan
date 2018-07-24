<template>
  <my-modal-delete :title="modelName"
                   :recordName="recordName"
                   :openModal="isOpenModalDelete"
                   @confirm="submitDelete"
                   @toggle="toggleModalDeleteStore">
  </my-modal-delete>
</template>

<script>
import { mapState, mapActions, mapGetters } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('gradeStore', [
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
    ...mapState('gradeStore', {
      modelName: 'modelName',
      isOpenModalDelete: 'isOpenModalDelete'
    }),
    ...mapGetters('gradeStore', ['recordName'])
  }
};
</script>

