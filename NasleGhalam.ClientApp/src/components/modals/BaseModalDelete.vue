<template>
  <bs-modal :show="show" :center="center" @open="$emit('open')" @close="$emit('close')">
    <template slot="header">
      <q-toolbar slot="header" color text-color="black">
        <q-toolbar-title>
          <q-icon name="warning" color="amber" size="25px"></q-icon>حذف
          <span class="text-red">{{title}}</span>
        </q-toolbar-title>
        <q-btn dense icon="close" @click="$emit('close')"/>
      </q-toolbar>
    </template>

    <q-card-main class="text-dark">
      آیا از حذف این رکورد
      <span class="text-bold text-red">({{recordName}})</span>
      اطمینان دارید؟
    </q-card-main>

    <template slot="footer">
      <base-btn-delete @click="$emit('confirm')"></base-btn-delete>
      <base-btn-back @click="$emit('close')"></base-btn-back>
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
    recordName: String,
    center: {
      type: Boolean,
      default: false
    },
    show: {
      type: Boolean,
      required: true
    }
  }
};
</script>

<style>
</style>
