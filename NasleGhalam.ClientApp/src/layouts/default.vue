<template>
  <q-layout view="lHh Lpr lFf">
    <loading></loading>
    <q-layout-header>
      <q-toolbar color="positive" :glossy="$q.theme === 'mat'" :inverted="$q.theme === 'ios'">
        <q-btn flat dense round @click="leftDrawerOpen = !leftDrawerOpen" aria-label="Menu">
          <q-icon name="menu" />
        </q-btn>

        <q-toolbar-title>
          آموزشگاه کنکور نسل قلم
          <div slot="subtitle">اتوماسیون</div>
        </q-toolbar-title>
        <div class="lt-sm inline">
          <q-btn flat dense round color="primary" @click="showLittleMenu = true" style="color:white !important">
            <q-icon name="list" />
          </q-btn>
          <div>

            <q-popover v-model="showLittleMenu">
              <div style="padding:5px; border-radius: 5px; overflow: overlay;">
                <img data-v-4ae020d8="" id="userImageProfile" src="/assets/img/User.png" alt="تصویر" width="46" height="46" class="img-circle" style=" width: 35px; height: 35px; border-radius: 50%;">
                 {{usersName}}، خوش آمدید
                <a @click="logout" style=" text-decoration: none;color: white;margin-right: 5px; cursor: pointer;">
                  <q-icon id="exitLtlMenu" name="assignment return" /> خروج </a>
              </div>
            </q-popover>
          </div>
        </div>
        <div class="gt-xs inline">
          <img data-v-4ae020d8="" id="userImageProfile" src="/assets/img/User.png" alt="تصویر" width="46" height="46" class="img-circle" style="width: 35px;height: 35px;border-radius: 50%;">
           {{usersName}}، خوش آمدید
          <a @click="logout" style="text-decoration: none;color: white;padding-right: 20px;cursor: pointer;">
            <q-icon name="assignment return" /> خروج </a>
        </div>
      </q-toolbar>

    </q-layout-header>

    <q-layout-drawer overlay v-model="leftDrawerOpen" :content-class="$q.theme === 'ios' ? 'bg-grey-2' : null">
      <q-list no-border link inset-delimiter>

        <q-collapsible group="sideMenu" icon="perm_identity" label="کاربری">
          <router-link to="/user">
            <q-item>
              <q-item-side icon="account circle" />
              <q-item-main label="کاربر" sublabel="" />
            </q-item>
          </router-link>
           <router-link to="/role">
            <q-item>
              <q-item-side icon="supervisor account" />
              <q-item-main label="نقش" sublabel="" />
            </q-item>
          </router-link>
        </q-collapsible>
        <q-collapsible group="sideMenu" icon="perm_identity" label="سوالات">
          <router-link to="/questionsGroup">
            <q-item>
              <q-item-side icon="receipt" />
              <q-item-main label="بارگذاری" sublabel="گروهی" />
            </q-item>
          </router-link>
          <router-link to="/questionsMono">
            <q-item>
              <q-item-side icon="reorder" />
              <q-item-main label="بارگذاری" sublabel="تکی" />
            </q-item>
          </router-link>
           <router-link to="/exams">
            <q-item>
              <q-item-side icon="question_answer" />
              <q-item-main label="آزمون" sublabel="" />
            </q-item>
          </router-link>
        </q-collapsible>
        <!-- <router-link v-for="menu in menuList" :key="menu.EnName" :to="menu.EnName">
          <q-item>
            <q-item-side :icon="menu.Icon" />
            <q-item-main :label="menu.FaName" sublabel="" />
          </q-item>
        </router-link> -->
      </q-list>
    </q-layout-drawer>

    <div @click="leftDrawerOpen = false">
      <q-page-container>

        <q-page padding>
          <div id="pagetitle" class="text-center">
          </div>
           <transition
              name="transitions"
              enter-active-class="animated bounceInDown"
              leave-active-class="animated bounceOutUp"
              mode="out-in"
      >
          <router-view/>
           </transition>
        </q-page>
      </q-page-container>
    </div>

  </q-layout>

</template>

<script>
import { openURL } from "quasar";
import loading from "../components/loading.vue";

export default {
  name: "LayoutDefault",
  data() {
    return {
      menuList: null,
      usersName: "مصطفی خرم نیا",
      showLittleMenu: false,
      leftDrawerOpen: this.$q.platform.is.desktop
    };
  },
  methods: {
    logout() {
      this.$q.localStorage.remove("authList");
      this.$q.localStorage.remove("menuList");
      this.$q.localStorage.remove("Token");
      this.$router.push("/user/login");
    },
    openURL,
    go() {
      this.leftDrawerOpen = false;
    }
  },
  components: { loading },
  created: function() {
    this.menuList = this.$q.localStorage.get.item("menuList");
    // window.$eventHub.on("logoutAxios", () => {
    //   this.$q.localStorage.remove("authList");
    //   this.$q.localStorage.remove("menuList");
    //   this.$q.localStorage.remove("Token");
    //   this.$router.push("/user/login");
    // });
  },
};
</script>

<style scoped>
#pagetitle {
  height: 35px;
  /* background: #8f8e8e38; */
  /* border-radius: 10px; */
  font-size: 16px;
  /* margin: 0px 12px; */
  border: 1px solid #45aacc;
  border-radius: 6px;
  margin-bottom: 10px;
}
.q-popover.scroll.animate-popup-down {
  border-radius: 5px;
}
#exitLtlMenu:hover {
  cursor: pointer;
}
aside a {
  text-decoration: none;
}
aside a div:hover {
  background-color: lightgray;
}
.layout-padding {
  padding: 1rem 1rem !important;
  max-width: 98%;
  box-shadow: rgb(136, 136, 136) 0px 0px 15px !important;
}
</style>
<style>
.q-layout-drawer.q-layout-drawer.q-layout-transition.q-layout-drawer-left.scroll.fixed {
  background: #294f70;
  /* background: #1f4037;  fallback for old browsers */
  /* background: -webkit-linear-gradient(to right, #99f2c8, #1f4037);  Chrome 10-25, Safari 5.1-6 */
  background: linear-gradient(
    to bottom,
    #31bf52,
    #5ad7a4
  ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */

  width: 220px;
  color: white;
}
.q-layout-drawer .q-item-side,
.q-layout-drawer .q-item-label {
  color: white;
}

.q-layout-page-container {
  margin-top: 10px;
}
tbody > tr:nth-child(even) {
  background: #f3f3f3;
}
tbody > tr:nth-child(odd) {
  background: #fafafa;
}
tbody > tr:hover {
  background: #d5e0f7 !important;
}
tbody > tr {
  font-size: larger !important;
}
thead > tr {
  background: #e6e6e6;
}

.q-toolbar.row.no-wrap.items-center.relative-position.q-toolbar-normal.glossy.bg-positive.text-white,
.q-popover.scroll.animate-popup-down{
    background-image: linear-gradient(to bottom, rgb(88, 214, 161), rgba(55, 194, 155, 0.49) 50%, rgba(102, 196, 157, 0.4) 51%, rgb(52, 191, 95)) !important;
}
</style>
