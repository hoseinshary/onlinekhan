<template>
  <q-btn outline
         color="secondary"
         class="shadow-1 bg-white q-mr-sm"
         icon="library_add"
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
    }
  }
};
</script>

<style>
</style>
