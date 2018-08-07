<template>
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
          ویرایش
          <span class="text-orange">{{title}}</span>
        </q-toolbar-title>
        <q-btn dense
               icon="close"
               @click="$emit('close')" />
      </q-toolbar>
    </template>

    <slot></slot>

    <template slot="footer">
      <my-btn-edit @click="$emit('confirm')"></my-btn-edit>
      <my-btn-clear @click="$emit('reset')"></my-btn-clear>
      <my-btn-back @click="$emit('close')"></my-btn-back>
    </template>
  </bs-modal>
</template>

<script>
export default {
  /**
   * props
   */
  props: {
    title: String,
    size: String,
    center: Boolean,
    show: {
      type: Boolean,
      required: true
    }
  }
};
</script>

<style>
</style>

