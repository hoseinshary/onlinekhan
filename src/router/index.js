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
  var title = "";
 

  if (
    (controller == "" && action == "") ||
    (controller == "home" && action == "index")
  ) {
    next();
    title = document.title = "آنلاین خوان";
  }
  if (controller == "user" && action == "login") {
    next();
    title = document.title = "ورود";
  }
  if (controller == "user" && action == "register") {
    next();
    title = document.title = "ثبت نام";
  }
  if (controller == "resume" && action == "registration") {
    next();
    title = document.title = "رزومه";
  }
  if (controller == "topic" && action == "printTopic".toLowerCase()) {
    next();
    title = document.title = "چاپ مبحث";
  }
  if (controller == "studentmajorlist" && action == "printMajorList".toLowerCase()) {
    next();
    title = document.title = "چاپ لیست انتخاب رشته";
  }

  if (title) {
    LocalStorage.set("title", title);
    return;
  }

  var authList = LocalStorage.get.item("authList");
  var subMenuList = LocalStorage.get.item("subMenuList");


  var pathForEvaluate = "";
  var tempArray = path.split("/");
  tempArray.forEach(element => {
    if (isNaN(element)) {
      pathForEvaluate += element;
    }
  });
  if (
    !authList ||
    !authList.filter(
      x =>
        x.replace(/\//g, "").toLowerCase() == pathForEvaluate.replace(/\//g, "")
    ).length
  ) {
    next("/user/login");
    util.logout();
    title = document.title = "ورود";
  } else {
    next();
    
    var subMenu = subMenuList.find(x =>
      x.EnName.toLowerCase().includes(action) && x.EnName.toLowerCase().includes(controller)
    );

    if (!subMenu) {
      subMenu = subMenuList.find(x => x.EnName.toLowerCase().endsWith(controller));
    }

    if (subMenu) {
      title = document.title = subMenu.FaName;
      LocalStorage.set("title", title);
    }
  }
});

export default Router;
