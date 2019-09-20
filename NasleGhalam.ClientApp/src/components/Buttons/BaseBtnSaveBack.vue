<template>
  <q-btn class="s-btn-save-back" @click="$emit('click')">
    <q-icon name="reply" />ثبت و بازگشت
  </q-btn>
</template>

<script>
export default {};
</script>

<style>
.s-btn-save-back {
  font-weight: bold;
  background-color: #faa61a;
  color: white;
  margin-right: 6px;
}
.s-btn-save-back .q-icon {
  font-size: 25px;
}
</style>

