import Vue from "vue";
import VueRouter from "vue-router";
import routes from "./routes";
import { LocalStorage } from "quasar";

import jQuery from "jquery";
window.$ = jQuery;

import "assets/css/my-style.css";

Vue.use(VueRouter);

import HTML from "vue-html";
Vue.use(HTML);

const Router = new VueRouter({
  /*
   * NOTE! Change Vue Router mode from quasar.conf.js -> build -> vueRouterMode
   *
   * If you decide to go with "history" mode, please also set "build.publicPath"
   * to something other than an empty string.
   * Example: '/' instead of ''
   */

  // Leave as is and change from quasar.conf.js instead!
  mode: process.env.VUE_ROUTER_MODE,
  base: process.env.VUE_ROUTER_BASE,
  scrollBehavior: () => ({ y: 0 }),
  routes
});
Router.beforeEach((to, from, next) => {
  //https://alligator.io/vuejs/vue-router-modify-head/
  const nearestWithTitle = to.matched.slice().reverse().find(r => r.meta && r.meta.title);
  if(nearestWithTitle) {document.title = nearestWithTitle.meta.title;
  setTimeout(function(){if(document.getElementById('pagetitle'))document.getElementById('pagetitle').innerHTML = nearestWithTitle.meta.title;},500);
  }


  var authList = LocalStorage.get.item("authList");
  if (to.fullPath == "/user/login") {
    next();
  } else if (!authList || !authList.includes(to.fullPath.toLowerCase())) {
    // next("/user/login");
    next();
  } else next();
});

export default Router;
