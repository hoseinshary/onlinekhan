<template>
  <q-modal v-model="isOpen"
    minimized @show="$emit('toggle', true)"
    @hide="$emit('toggle', false)"
    no-backdrop-dismiss>
    <q-card>
      <q-card-title>
        <q-icon name="warning" color="amber"
          size="30px"></q-icon>
        حذف
        <span class="text-red">{{title}}</span>
      </q-card-title>
      <q-card-separator />
      <q-card-main class="text-dark">
        آیا از حذف این رکورد
        <span class="text-bold text-red">
          ({{recordName}}) </span>
        اطمینان دارید؟
      </q-card-main>
      <q-card-separator />
      <q-card-main>
        <div class="row justify-center">
          <my-btn-delete @click="$emit('confirm')"></my-btn-delete>
          <my-btn-back @click="isOpen=false"></my-btn-back>
        </div>
      </q-card-main>
    </q-card>
  </q-modal>
</template>

<script>
export default {
  /**
   * data
   */
  data() {
    return {
      isOpen: false
    }
  },
  /**
   * props
   */
  props: {
    recordName: {
      type: String,
      default: ''
    },
    title: {
      type: String,
      default: ''
    },
    openModal: {
      type: Boolean,
      default: false
    }
  },
  /**
   * whatch
   */
  watch: {
    openModal(newVal) {
      this.isOpen = newVal
    }
  }
}
</script>

<style>
</style>
