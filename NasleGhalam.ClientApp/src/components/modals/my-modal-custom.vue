<template>
  <q-modal v-model="isOpen"
    @show="$emit('toggle', true)"
    @hide="$emit('toggle', false)"
    no-backdrop-dismiss :content-css="modalContentCss">
    <q-modal-layout>
      <q-toolbar slot="header" color="cyan-9"
        text-color="">
        <q-toolbar-title>
          <slot name="header"></slot>
        </q-toolbar-title>

        <q-btn dense v-close-overlay
          icon="close" />
      </q-toolbar>

      <div class="layout-padding">
        <div class="row gutter-sm">
          <slot></slot>
        </div>
      </div>

      <q-toolbar slot="footer" color="white">
        <q-toolbar-title>
          <div class="row justify-center ">
            <slot name="footer"></slot>
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
    let modalContentCss = {}
    if (this.contentCss) {
      modalContentCss = this.contentCss
    } else if (this.size == 'sm') {
      modalContentCss = {
        minWidth: '40vw',
        height: this.height
      }
    } else if (this.size == 'md') {
      modalContentCss = {
        minWidth: '60vw',
        height: this.height
      }
    } else if (this.size == 'lg') {
      modalContentCss = {
        minWidth: '80vw',
        height: this.height
      }
    } else if (this.size == 'xl') {
      modalContentCss = {
        minWidth: '95vw',
        height: this.height
      }
    }

    return {
      isOpen: false,
      modalContentCss
    }
  },
  /**
   * whatch
   */
  watch: {
    openModal(newVal) {
      this.isOpen = newVal
    }
  }
}
</script>

<style>
</style>
