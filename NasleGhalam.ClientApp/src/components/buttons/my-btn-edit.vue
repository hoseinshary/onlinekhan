<template>
  <q-btn v-if="round"
         outline
         round
         icon="create"
         color="purple"
         size="sm"
         class="shadow-1 bg-white q-mr-sm"
         @click="$emit('click')"
         :disabled='disabled'>
    <q-tooltip>
      ویرایش
    </q-tooltip>
  </q-btn>
  <q-btn v-else
         outline
         color="purple"
         class="shadow-1 bg-white q-mr-sm"
         type="submit"
         @click="$emit('click')"
          :disabled='disabled'>
    <q-icon name="create" /> ویرایش
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

