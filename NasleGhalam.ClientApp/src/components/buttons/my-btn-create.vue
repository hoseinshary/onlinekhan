<template>
  <q-btn outline
         :color="color"
         class="shadow-1 bg-white q-mr-sm"
         :icon="icon"
         :label="label"
         @click="$emit('click')">
  </q-btn>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      default: 'ایجاد'
    },
    color: {
      type: String,
      default: 'secondary'
    },
    icon: {
      type: String,
      default: 'library_add'
    }
  }
};
</script>

<style>
</style>
