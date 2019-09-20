<template>
  <q-btn class="s-btn-clear" @click="$emit('click')">
    <q-icon name="stars" />جدید
  </q-btn>
</template>

<script>
export default {};
</script>

<style>
.s-btn-clear {
  font-weight: bold;
  background-color: #50b848;
  color: white;
  margin-right: 6px;
}
.s-btn-clear .q-icon {
  font-size: 25px;
}
</style>
