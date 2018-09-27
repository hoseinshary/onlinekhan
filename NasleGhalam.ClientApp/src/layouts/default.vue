<template>
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar :glossy="$q.theme === 'mat'"
                 text-color="white"
                 color="amber-10">
        <q-btn flat
               dense
               round
               @click="leftDrawerOpen = !leftDrawerOpen"
               aria-label="Menu">
          <q-icon name="menu" />
        </q-btn>

        <q-toolbar-title>
          نسل قلم
          <div slot="subtitle">سامانه جامع کنکور
          </div>
        </q-toolbar-title>
        <div class="gt-xs inline">
          <img src="/assets/img/User.jpg"
               class="img-circle"
               style="width: 20px;height: 20px;border-radius: 50%;"> {{FullName}}،
          خوش آمدید
          <a @click="logout"
             style="text-decoration: none;color: white;padding-right: 20px;cursor: pointer;">
            <q-icon name="assignment return" /> خروج </a>
        </div>
        <div class="lt-sm inline">
          <q-btn flat
                 dense
                 round
                 color="primary"
                 @click="showLogoutMenu = true"
                 style="color:white !important">
            <q-icon name="list" />
          </q-btn>
          <div>

            <q-popover v-model="showLogoutMenu"
                       style="border-radius: 5px;">
              <div style="padding:5px; background: rgb(32, 116, 119);border-radius: 5px;overflow: overlay;">
                <img src="/assets/img/User.jpg"
                     class="img-circle"
                     style="width: 20px;height: 20px;border-radius: 50%;">{{FullName}}،
                خوش آمدید
                <a @click="logout"
                   style="text-decoration: none;color: white;margin-right: 5px; cursor: pointer;">
                  <q-icon id="exitLtlMenu"
                          name="assignment return" /> خروج </a>
              </div>
            </q-popover>
          </div>
        </div>

      </q-toolbar>
    </q-layout-header>

    <q-layout-drawer v-model="leftDrawerOpen"
                     :overlay="true"
                     side="left"
                     :mini="miniState"
                     @click.capture="drawerClick"
                     bg-color = "amber-14"
                     >
                     
     
      <q-list no-border
              link
              inset-delimiter>

        <q-collapsible group="sideMenu"
                       v-for="menu in menuList"
                       :key="menu.ModuleId"
                       :label="menu.ModuleName">
          <router-link v-for="item in subMenuList.filter(x=>x.ModuleId == menu.ModuleId)"
                       :key="item.EnName"
                       :to="item.EnName">
            <q-item>
              <!-- <q-item-side icon='map' /> -->
              <!--"item.Icon" />-->
              <q-item-main :label="item.FaName"
                           sublabel="" />
            </q-item>
          </router-link>
        </q-collapsible>
      </q-list>
    </q-layout-drawer>

    <q-page-container>

      <br />
      <div class="row justify-center">
        <transition name="transitions"
                    enter-active-class="animated bounceInDown"
                    leave-active-class="animated bounceOutUp"
                    mode="out-in">
          <router-view />
        </transition>
      </div>

    </q-page-container>
  </q-layout>
</template>

<script>
import { openURL } from 'quasar';
import util from 'utilities/util';

export default {
  data() {
    return {
      leftDrawerOpen: true, //this.$q.platform.is.desktop,
      miniState: false,
      menuList: null,
      subMenuList: null,
      FullName: '',
      showLogoutMenu: false
    };
  },
  methods: {
    openURL,
    drawerClick(e) {
      // if in "mini" state and user
      // click on drawer, we switch it to "normal" mode
      if (this.miniState) {
        this.miniState = false;

        // notice we have registered an event with capture flag;
        // we need to stop further propagation as this click is
        // intended for switching drawer to "normal" mode only
        e.stopPropagation();
      }
    },
    logout() {
      util.logout();
    }
  },
  created: function() {
    this.FullName = this.$q.localStorage.get.item('FullName');
    this.menuList = this.$q.localStorage.get.item('menuList');
    this.subMenuList = this.$q.localStorage.get.item('subMenuList');
  }
};
</script>

<style>
.q-item.q-item-division.relative-position:hover {
  background: #719ee6;
}
aside a {
  text-decoration: none;
}
.q-collapsible-sub-item.relative-position {
  padding: 5px;
  overflow-x: hidden;
}
.q-layout-drawer.q-layout-drawer.q-layout-transition.q-layout-drawer-left.scroll.fixed {
  background: #f87352;
  /* background: #1f4037;  fallback for old browsers */
  /* background: -webkit-linear-gradient(to right, #99f2c8, #1f4037);  Chrome 10-25, Safari 5.1-6 */
  /* background: linear-gradient(
    to top,
    #136b6fad,
    #00585c
  ); W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */

  width: 220px;
  color: white;
}
.q-layout-drawer .q-item-side,
.q-layout-drawer .q-item-label {
  color: white;
}
</style>
