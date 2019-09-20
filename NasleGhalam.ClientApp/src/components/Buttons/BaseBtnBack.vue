<template>
  <q-btn class="s-btn-back" @click="$emit('click')">
    <q-icon name="reply_all" />بازگشت
  </q-btn>
</template>

<script>
export default {};
</script>

<style>
.s-btn-back {
  font-weight: bold;
  background-color: #ed1c24;
  color: white;
  margin-right: 6px;
}
.s-btn-back .q-icon {
  font-size: 25px;
}
</style>
