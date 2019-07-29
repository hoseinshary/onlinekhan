import Vue from "vue";
import VueRouter from "vue-router";
import { LocalStorage } from "quasar";
import routes from "./routes";
import util from "src/utilities";

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
  if (to.fullPath == "/resume") {
    next();
    document.title = "رزومه";
    return;
  }

  var authList = LocalStorage.get.item("authList");
  var subMenuList = LocalStorage.get.item("subMenuList");

  if (to.fullPath == "/user/login") {
    next();
    document.title = "ورود";
  } else if (!authList || !authList.includes(to.fullPath.toLowerCase())) {
    next("/user/login");
    util.logout();
    document.title = "ورود";
  } else {
    next();
    document.title = subMenuList.filter(
      x => x.EnName.toLowerCase() == to.fullPath.toLowerCase()
    )[0].FaName;
  }
});

export default Router;
