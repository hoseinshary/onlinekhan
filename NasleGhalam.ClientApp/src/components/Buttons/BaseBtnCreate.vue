<template>
  <q-btn outline
         :color="color"
         class="shadow-1 bg-white q-mr-sm"
         :icon="icon"
         :label="label"
         @click="$emit('click')"
         :disabled='disabled'>
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
    },
    disabled: {
      default: false
    }
  }
};
</script>

<style>
</style>
