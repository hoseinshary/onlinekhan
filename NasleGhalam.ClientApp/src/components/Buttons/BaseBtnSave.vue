<template>
  <q-btn class="s-btn-save" type="submit" @click="$emit('click')">
    <q-icon name="get_app" />ثبت
  </q-btn>
</template>

<script>
export default {};
</script>

<style>
.s-btn-save {
  font-weight: bold;
  background-color: #b41e8e;
  color: white;
  margin-right: 6px;
}
.s-btn-save .q-icon {
  font-size: 25px;
}
</style>

