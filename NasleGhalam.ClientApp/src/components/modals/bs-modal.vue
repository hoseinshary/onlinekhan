<template>
  <div>
    <transition name="bs-modal">
      <div v-show="show"
           class="bs-modal"
           tabindex="0"
           role="dialog"
           @click="close('click')">
        <div class="bs-modal-dialog"
             :class="modalSize"
             role="document"
             @click.stop>
          <div class="bs-modal-content">
            <section class="bs-modal-header">
              <slot name="header"></slot>
            </section>

            <section class="bs-modal-body">
              <div class="row gutter-sm">
                <slot></slot>
              </div>
            </section>

            <section class="bs-modal-footer">
              <slot name="footer"></slot>
            </section>
          </div>
        </div>
      </div>
    </transition>
    <section v-if="show"
             class="bs-modal-backdrop"></section>
  </div>
</template>

<script>
export default {
  /**
   * props
   */
  props: {
    size: String,
    show: {
      type: Boolean,
      required: true
    },
    center: Boolean,
    closeOnClick: Boolean,
    closeOnEsc: {
      type: Boolean,
      default: true
    }
  },

  /**
   * data
   */
  data() {
    return {
      modalSize: {
        'bs-modal-dialog-centered': this.center,
        'bs-modal-lg': this.size == 'lg',
        'bs-modal-sm': this.size == 'sm',
        'bs-modal-xl': this.size == 'xl'
      }
    };
  },

  /**
   * methods
   */
  methods: {
    close(type) {
      if (
        (type == 'click' && this.closeOnClick) ||
        (type == 'esc' && this.closeOnEsc)
      ) {
        // this.$emit('toggle', false);
        this.$emit('close');
      }
    }
  },

  /**
   * whatch
   */
  watch: {
    show(newVal) {
      if (newVal) {
        document.body.classList.add('bs-modal-open');
        // document.body.style.paddingRight = '17px';
      } else {
        document.body.classList.remove('bs-modal-open');
        // document.body.style.paddingRight = '0';
      }
    }
  },
  mounted() {
    // console.log('esc');
    // document.addEventListener('keydown', e => {
    //   console.log('esc');
    //   if (this.show && e.keyCode == 27) {
    //     this.close();
    //   }
    // });
  },
  destroyed() {
    // console.log('modal destroyed');
  }
};
</script>
<style>
/* transition  */
.bs-modal-enter,
.bs-modal-leave-to {
  transform: translate(0, -25%);
}

.bs-modal-enter-active,
.bs-modal-leave-active {
  transition: transform 0.5s;
}

.bs-modal-enter .bs-modal-dialog,
.bs-modal-leave-to .bs-modal-dialog {
  opacity: 0;
}

.bs-modal-enter-active .bs-modal-dialog,
.bs-modal-leave-active .bs-modal-dialog {
  transition: opacity 0.3s;
}
/* /transition */

/* bs-modal*/
.bs-modal-open {
  overflow: hidden;
}

.bs-modal {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1050;
  overflow: hidden;
  outline: 0;
  /* padding-right:0; */
}

.bs-modal-open .bs-modal {
  overflow-x: hidden;
  overflow-y: auto;
}

.bs-modal-dialog {
  position: relative;
  width: auto;
  margin: 0.5rem;
  pointer-events: none;
}

.bs-modal-dialog-centered {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  min-height: calc(100% - (0.5rem * 2));
}

.bs-modal-content {
  position: relative;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -ms-flex-direction: column;
  flex-direction: column;
  width: 100%;
  pointer-events: auto;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid rgba(0, 0, 0, 0.2);
  border-radius: 0.3rem;
  outline: 0;
}

.bs-modal-backdrop {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1040;
  background-color: #000;
  opacity: 0.5;
}

.bs-modal-header {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: start;
  -ms-flex-align: start;
  align-items: flex-start;
  -webkit-box-pack: justify;
  -ms-flex-pack: justify;
  justify-content: space-between;
  /* padding: 1rem; */
  padding: 0;
  border-bottom: 1px solid #e9ecef;
  border-top-left-radius: 0.3rem;
  border-top-right-radius: 0.3rem;
}

.bs-modal-body {
  position: relative;
  -webkit-box-flex: 1;
  -ms-flex: 1 1 auto;
  flex: 1 1 auto;
  padding: 2rem;
}

.bs-modal-footer {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: end;
  -ms-flex-pack: end;
  justify-content: center;
  padding: 1rem;
  border-top: 1px solid #e9ecef;
}

.bs-modal-footer > :not(:first-child) {
  margin-left: 0.25rem;
}

.bs-modal-footer > :not(:last-child) {
  margin-right: 0.25rem;
}

.bs-modal-scrollbar-measure {
  position: absolute;
  top: -9999px;
  width: 50px;
  height: 50px;
  overflow: scroll;
}

@media (min-width: 576px) {
  .bs-modal-dialog {
    max-width: 500px;
    margin: 1.75rem auto;
  }
  .bs-modal-dialog-centered {
    min-height: calc(100% - (1.75rem * 2));
  }
  .bs-modal-sm {
    max-width: 300px;
  }
}

@media (min-width: 992px) {
  .bs-modal-lg {
    max-width: 800px;
  }
}

.fixed-top {
  /* position: fixed;
  top: 0;
  right: 0;
  left: 0; */
  z-index: 1030;
}
/* /bs-modal*/
</style>
