<template>
  <q-btn
    v-if="round"
    round
    size="sm"
    icon="clear"
    class="s-round-btn-delete"
    @click="$emit('click')"
    :disabled="disabled"
  >
    <q-tooltip>حذف</q-tooltip>
  </q-btn>
  <q-btn v-else class="s-btn-delete" @click="$emit('click')" :disabled="disabled">
    <q-icon name="delete" />حذف
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
.s-round-btn-delete {
  box-shadow: none;
  margin-right: 5px;
  background-color: #ef4130;
  color: white;
}

.s-round-btn-delete .q-icon {
  font-size: 30px;
}

.s-btn-delete {
  font-weight: bold;
  background-color: #b41e8e;
  color: white;
  margin-right: 6px;
}
.s-btn-delete .q-icon {
  font-size: 25px;
}
</style>

