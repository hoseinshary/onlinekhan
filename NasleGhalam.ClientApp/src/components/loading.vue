<template>
  <transition name="loader-fade">
    <section class="loader-modal" v-if="show">
      <div class="loader"></div>
    </section>
  </transition>
</template>

<script>
export default {
  data() {
    return {
      show: false,
      showLoading: true
    };
  },
  created: function() {
    window.$eventHub.on("enableLoader", () => {
      // debugger;
      if (this.showLoading) this.show = true;
    });
    window.$eventHub.on("disableLoader", () => {
      // debugger;
      if (this.showLoading)
        setTimeout(() => {
          this.show = false;
        }, 500);
    });
    window.$eventHub.on("showLoading", data => {
      this.showLoading = data;
    });

    // this.$bus.$on("enableLoader", () => {
    //   if (this.showLoading) this.show = true;
    // });
    // this.$bus.$on("disableLoader", () => {
    //   if (this.showLoading) this.show = false;
    // });

    // this.$bus.$on("showLoading", data => {
    //   this.showLoading = data;
    // });
  }
};
</script>

<style>
.loader-fade-enter-active,
.loader-fade-leave-active {
  transition: all 0.5s;
}

.loader-fade-enter,
.loader-fade-leave-to {
  opacity: 0;
}

.loader-modal {
  position: fixed;
  z-index: 9990;
  height: 100%;
  width: 100%;
  top: 0;
  background-color: #000;
  filter: alpha(opacity=60);
  opacity: 0.6;
  -moz-opacity: 0.8;
}
.loader,
.loader:before,
.loader:after {
  border-radius: 50%;
  width: 2.5em;
  height: 2.5em;
  -webkit-animation-fill-mode: both;
  animation-fill-mode: both;
  -webkit-animation: load7 1.8s infinite ease-in-out;
  animation: load7 1.8s infinite ease-in-out;
}
.loader {
  color: #ffffff;
  font-size: 10px;
  margin: 80px auto;
  position: fixed;
  z-index: 9999;
  right: 50%;
  top: 20%;
  text-indent: -9999em;
  -webkit-transform: translateZ(0);
  -ms-transform: translateZ(0);
  transform: translateZ(0);
  -webkit-animation-delay: -0.16s;
  animation-delay: -0.16s;
}
.loader:before,
.loader:after {
  content: "";
  position: absolute;
  top: 0;
}
.loader:before {
  left: -3.5em;
  -webkit-animation-delay: -0.32s;
  animation-delay: -0.32s;
}
.loader:after {
  left: 3.5em;
}
@-webkit-keyframes load7 {
  0%,
  80%,
  100% {
    box-shadow: 0 2.5em 0 -1.3em;
  }
  40% {
    box-shadow: 0 2.5em 0 0;
  }
}
@keyframes load7 {
  0%,
  80%,
  100% {
    box-shadow: 0 2.5em 0 -1.3em;
  }
  40% {
    box-shadow: 0 2.5em 0 0;
  }
}
</style>

