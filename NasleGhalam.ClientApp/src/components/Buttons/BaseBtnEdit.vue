<template>
  <q-btn
    v-if="round"
    round
    size="sm"
    icon="edit"
    class="s-round-btn-edit"
    @click="$emit('click')"
    :disabled="disabled"
  >
    <q-tooltip>ویرایش</q-tooltip>
  </q-btn>
  <q-btn v-else class="s-btn-edit" type="submit" @click="$emit('click')" :disabled="disabled">
    <q-icon name="create" />ذخیره
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
.s-round-btn-edit {
  box-shadow: none;
  margin-right: 5px;
  background-color: #0072bc;
  color: white;
}

.s-round-btn-edit .q-icon {
  font-size: 25px;
}

.s-btn-edit {
  font-weight: bold;
  background-color: #b41e8e;
  color: white;
  margin-right: 6px;
}
.s-btn-edit .q-icon {
  font-size: 25px;
}
</style>

