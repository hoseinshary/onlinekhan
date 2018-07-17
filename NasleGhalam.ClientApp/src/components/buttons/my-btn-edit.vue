<template>
  <q-btn v-if="round"
         outline
         round
         icon="create"
         color="purple"
         size="sm"
         class="shadow-5 bg-white"
         @click="$emit('click')">
    <q-tooltip>
      ویرایش
    </q-tooltip>
  </q-btn>
  <q-btn v-else
         outline
         color="purple"
         class="shadow-5 bg-white q-mr-md"
         @click="$emit('click')">
    <q-icon name="create" /> ویرایش
  </q-btn>
</template>

<script>
export default {
  props: {
    round: Boolean
  }
};
</script>

<style>
</style>

