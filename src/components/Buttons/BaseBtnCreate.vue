<template>
  <q-btn
    :color="color"
    class="s-btn-create q-ml-sm"
    :icon="icon"
    :label="label"
    @click="$emit('click')"
    :disabled="disabled"
  ></q-btn>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      default: "ایجاد"
    },
    color: {
      type: String,
      default: "positive"
    },
    icon: {
      type: String,
      default: "add_circle"
    },
    disabled: {
      default: false
    }
  }
};
</script>

<style>
.s-btn-create {
  border-radius: 50px;
  padding: 0px 3px;
  font-weight: bold;
}
</style>
