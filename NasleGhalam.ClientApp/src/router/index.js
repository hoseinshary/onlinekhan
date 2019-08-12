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
  var path = (to.fullPath || "").toLowerCase();
  path = path.replace(/\//g, "/").replace(/\/\//g, "/");
  if (path.startsWith("/")) {
    path = path.substr(1);
  }
  if (path.endsWith("/")) {
    path = path.substr(0, path.length - 1);
  }

  var arr = path.split("/");
  var controller = arr[0] || "";
  var action = arr[1] || "";

  if (controller == "user" && action == "login") {
    next();
    document.title = "ورود";
    return;
  }
  if (controller == "resume" && action == "registration") {
    next();
    document.title = "رزومه";
    return;
  }
  if (controller == "topic" && action == "printTopic".toLowerCase()) {
    next();
    document.title = "چاپ مبحث";
    return;
  }

  var authList = LocalStorage.get.item("authList");
  var subMenuList = LocalStorage.get.item("subMenuList");

  if (
    !authList ||
    !authList.filter(
      x =>
        (x.startsWith("/") ? x.replace("/", "") : x).toLowerCase() == controller
    ).length
  ) {
    next("/user/login");
    util.logout();
    document.title = "ورود";
  } else {
    next();
    document.title = subMenuList.filter(x =>
      x.EnName.toLowerCase().endsWith(controller)
    )[0].FaName;
  }
});

export default Router;
