<template>

  <q-card :square="true"
          class="my-panel">
    <q-card-title class="text-center text-bold bg-white header">
      <slot name="title"></slot>
    </q-card-title>
    <q-card-separator />
    <q-card-main class="bg-white">
      <slot name="body"></slot>
    </q-card-main>
  </q-card>

</template>

<script>
export default {};
</script>

<style>
/* .my-panel {
  margin: 0 auto;
} */

.my-panel .header {
  border-bottom: 2px solid orange;
}
</style>

