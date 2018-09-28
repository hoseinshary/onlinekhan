<template>
  <form @submit.prevent="submit">
    <bs-modal :show="show"
              :center="center"
              :size="size"
              @open="$emit('open')"
              @close="$emit('close')">
      <template slot="header">
        <q-toolbar slot="header"
                   color="cyan-9"
                   text-color="">
          <q-toolbar-title>
            ثبت
            <span class="text-orange">{{title}}</span>
            جدید
          </q-toolbar-title>
          <q-btn dense
                 icon="close"
                 @click="$emit('close')" />
        </q-toolbar>
      </template>

      <slot></slot>

      <template slot="footer">
        <my-btn-save></my-btn-save>
        <my-btn-save-back @click="$emit('confirm', true)"></my-btn-save-back>
        <my-btn-clear @click="$emit('reset')"></my-btn-clear>
        <my-btn-back @click="$emit('close')"></my-btn-back>
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
      this.$emit('confirm', false);
    }
  }
};
</script>
<style>
</style>
