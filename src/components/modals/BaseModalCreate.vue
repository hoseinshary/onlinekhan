<template>
  <form @submit.prevent="submit">
    <bs-modal
      :show="show"
      :center="center"
      :size="size"
      @open="$emit('open')"
      @close="$emit('close')"
    >
      <template slot="header">
        <q-toolbar slot="header" color="blue-8" text-color>
          <q-toolbar-title>
            ثبت
            <span class="text-orange" >{{title}}</span>
            جدید
          </q-toolbar-title>
          <q-btn dense icon="close" @click="$emit('close')"/>
        </q-toolbar>
      </template>

      <slot></slot>

      <template slot="footer">
        <base-btn-save></base-btn-save>
        <base-btn-save-back @click="$emit('confirm', true)"></base-btn-save-back>
        <base-btn-clear @click="$emit('reset')"></base-btn-clear>
        <base-btn-back @click="$emit('close')"></base-btn-back>
      </template>
    </bs-modal>
  </form>
</template>

<script>
export default {
  /**
   * props
   */
  props: {
    title: String,
    size: String,
    center: {
      type: Boolean,
      default: false
    },
    show: {
      type: Boolean,
      required: true
    }
  },
  /**
   * methods
   */
  methods: {
    submit() {
      this.$emit("confirm", false);
    }
  }
};
</script>
<style>
</style>
