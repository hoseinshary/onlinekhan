import Vue from 'vue';
import VueRouter from 'vue-router';
import { LocalStorage } from 'quasar';
import routes from './routes';

Vue.use(VueRouter);

const Router = new VueRouter({
  /*
   * NOTE! Change Vue Router mode from quasar.conf.js -> build -> vueRouterMode
   *
   * When going with "history" mode, please also make sure "build.publicPath"
   * is set to something other than an empty string.
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
  // const nearestWithTitle = to.matched.slice().reverse().find(r => r.meta && r.meta.title);
  // if (nearestWithTitle) {
  // document.title = nearestWithTitle.meta.title;
  //   setTimeout(function () { if (document.getElementById('pagetitle')) document.getElementById('pagetitle').innerHTML = nearestWithTitle.meta.title; }, 500);
  // }
  // window.$eventHub.removeAllListeners('closeModal');

  var authList = LocalStorage.get.item('authList');
  var menuList = LocalStorage.get.item('menuList');
  if (to.fullPath == '/user/login') {
    next();
    document.title = 'ورود';
  } else if (!authList || !authList.includes(to.fullPath.toLowerCase())) {
    next('/user/login');
    document.title = 'ورود';
  } else {
    next();
    document.title = menuList.filter(
      x => x.EnName.toLowerCase() == to.fullPath.toLowerCase()
    )[0].FaName;
  }
});

export default Router;
