<template>
  <q-modal @escape-key="$emit('escape-key')" @hide="$emit('hide')" @show="$emit('show')" v-model="options.showModal" :content-css="{minWidth: options.width, minHeight: options.height}">
    <q-modal-layout class="relative-position">

      <q-toolbar slot="header" :color="options.color">
        <div class="absolute" style="right: 15px;">
          {{options.title}}
        </div>
        <q-btn flat round dense v-close-overlay icon="clear" class="absolute" style="left: 8px;" />
      </q-toolbar>

      <q-toolbar class="text-center" slot="footer" color="white">

        <q-toolbar-title>
          <!-- <q-btn
        color="positive"
        label="اجرا"
        @click="runClick()"
      >
      <q-icon name="done all" />
      </q-btn> -->
          <slot name="modal-btns">

          </slot>
          <q-btn :color="options.color" v-close-overlay label="بازگشت">

            <q-icon name="chevron left" />
          </q-btn>

        </q-toolbar-title>
      </q-toolbar>

      <div class="layout-padding">
        <slot name="modal-body">

        </slot>
      </div>

    </q-modal-layout>
  </q-modal>
</template>
<script>
export default {
  data() {
    return {
      obj: {
        x: 6,
        y: 7
      }
    };
  },
  props: ["options"]
};
</script>
