<template>

  <q-btn v-if="round"
         outline
         round
         icon="delete"
         color="red"
         size="sm"
         class="shadow-1 bg-white"
         @click="$emit('click')"
         :disabled='disabled'>
    <q-tooltip>
      حذف
    </q-tooltip>
  </q-btn>
  <q-btn v-else
         outline
         color="red"
         class="shadow-1 bg-white q-mr-sm"
         @click="$emit('click')"
          :disabled='disabled'>
    <q-icon name="delete" /> حذف
  </q-btn>
</template>

<script>
export default {
  props: {
    round: Boolean,
    disabled: {
      default: false
    }
  }
};
</script>

<style>
</style>

