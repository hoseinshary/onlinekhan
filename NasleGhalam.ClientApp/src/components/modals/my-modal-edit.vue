<template>
  <q-modal v-model="isOpen"
           @show="$emit('toggle', true)"
           @hide="hide"
           no-backdrop-dismiss
           :content-css="modalContentCss">
    <q-modal-layout>
      <q-toolbar slot="header"
                 color="cyan-9"
                 text-color="">
        <q-toolbar-title>
          ویرایش
          <span class="text-orange">{{title}}</span>
        </q-toolbar-title>

        <q-btn dense
               v-close-overlay
               icon="close" />
      </q-toolbar>

      <div class="layout-padding">
        <div class="row gutter-sm">
          <slot></slot>
        </div>
      </div>

      <q-toolbar slot="footer"
                 color="white">
        <q-toolbar-title>
          <div class="row justify-center ">
            <my-btn-edit @click="$emit('confirm')"></my-btn-edit>
            <my-btn-clear @click="$emit('reset')"></my-btn-clear>
            <my-btn-back @click="isOpen=false"></my-btn-back>
          </div>
        </q-toolbar-title>
      </q-toolbar>
    </q-modal-layout>
  </q-modal>
</template>

<script>
export default {
  /**
   * props
   */
  props: {
    title: {
      type: String,
      default: ''
    },
    size: {
      type: String,
      default: 'md'
    },
    height: {
      type: String,
      default: '100%'
    },
    contentCss: Object,
    openModal: {
      type: Boolean,
      default: false
    }
  },
  /**
   * data
   */
  data() {
    let modalContentCss = {};
    if (this.contentCss) {
      modalContentCss = this.contentCss;
    } else if (this.size == 'sm') {
      modalContentCss = { minWidth: '40vw', height: this.height };
    } else if (this.size == 'md') {
      modalContentCss = { minWidth: '60vw', height: this.height };
    } else if (this.size == 'lg') {
      modalContentCss = { minWidth: '80vw', height: this.height };
    } else if (this.size == 'xl') {
      modalContentCss = { minWidth: '95vw', height: this.height };
    }

    return {
      isOpen: false,
      modalContentCss
    };
  },
  /**
   * methods
   */
  methods: {
    hide(isOpen) {
      this.isOpen = false;
      this.$emit('toggle', false);
    }
  },
  /**
   * whatch
   */
  watch: {
    openModal(newVal) {
      this.isOpen = newVal;
    }
  }
};
</script>

<style>
</style>

