<template >
<div class="background" >
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar class="toolbar-header1">
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          aria-label="Menu"
          color="black"
        >
          <q-icon name="menu" />
        </q-btn>
        <router-link to ="/panel/adminpanel" class="logo" > </router-link>
      </q-toolbar>

      <q-toolbar class="toolbar-header2">
        <router-link to ="/panel/adminpanel" ><img src="/assets/img/header/default.png" class="header-image" alt="header image" /></router-link>
        <q-toolbar-title>{{$q.localStorage.get.item("title")}}</q-toolbar-title>

        <img
          :src="$q.localStorage.get.item('ProfilePic')"
          class="profile-image"
          alt="profile picture"
        />
        <q-btn flat dense class="q-mr-sm">
          <!-- <q-icon name="account_circle" /> -->
          {{$q.localStorage.get.item("FullName")}}
        </q-btn>
        <q-btn @click="logout" flat dense>
          <q-icon name="exit_to_app" />خروج
        </q-btn>
      </q-toolbar>
    </q-layout-header>

    <q-layout-drawer
      v-model="leftDrawerOpen"
      :overlay="true"
      side="left"
      :mini="false"
      behavior="mobile"
      class="layout-drawer"
      @click.capture="drawerClick"
    >
      <q-list no-border link inset-delimiter>
        <q-list-header>{{siteName}}</q-list-header>
        <q-collapsible
          group="sideMenu"
          v-for="menu in menuList"
          :key="menu.ModuleId"
          :label="menu.ModuleName"
        >
          <router-link
            v-for="item in subMenuList.filter(x=>x.ModuleId == menu.ModuleId)"
            :key="item.EnName"
            :to="item.EnName"
          >
            <q-item>
              <!-- <q-item-side icon='map' /> -->
              <!--"item.Icon" />-->
              <q-item-main :label="item.FaName" sublabel color="white" />
            </q-item>
          </router-link>
        </q-collapsible>
      </q-list>
    </q-layout-drawer>

    <q-page-container class>
      <br />
      <div class="row justify-center q-mt-lg">
        <transition
          name="transitions"
          enter-active-class="animated bounceInDown"
          leave-active-class="animated bounceOutUp"
          mode="out-in"
        >
          <router-view />
        </transition>
      </div>
    </q-page-container>
  </q-layout>
</div>
</template>

<script>
import util from "src/utilities";

export default {
  data() {
    return {
      siteName: "آنلاین خوان",
      leftDrawerOpen: true,
      miniState: false,
      menuList: null,
      subMenuList: null
    };
  },
  methods: {
    drawerClick(e) {
      if (this.miniState) {
        this.miniState = false;
        e.stopPropagation();
      }
    },
    logout() {
      util.logout();
    }
  },
  created: function() {
    this.menuList = this.$q.localStorage.get.item("menuList");
    this.subMenuList = this.$q.localStorage.get.item("subMenuList");
  }
};
</script>

<style>
.q-layout-header {
  border-top: 14px solid #f36f21;
}

/* toolbar-header1 */
.toolbar-header1 {
  background-color: white !important;
  height: 115px;
}

.toolbar-header1 .logo {
  background-image: url("../assets/img/logo1-old.png");
  background-repeat: no-repeat;
  background-size: contain;
  height: 90px;
  width: 240px;
  max-width: 100%;
  margin: 0 auto;
}
/* --- */

/* toolbar-header2 */
.toolbar-header2 {
  background-color: #0072bc;
  height: 60px;
  color: white;
  position: relative;
}

.toolbar-header2 .q-toolbar-title {
  margin-left: 210px;
}

.toolbar-header2 img.header-image {
  position: fixed;
  left: 140px;
  top: 88px;
  width: 90px;
}

.toolbar-header2 img.profile-image {
  width: 55px;
  height: 55px;
  border-radius: 50px;
}

.toolbar-header2 .q-btn,
.toolbar-header2 .q-toolbar-title {
  font-size: 23px;
  font-weight: bold;
}

.toolbar-header2 .q-btn .q-icon {
  color: #f36f21;
}

.toolbar-header2 .q-btn:last-child {
  margin-right: 100px;
}

/* --- */

/* sidebar */
.layout-drawer aside {
  width: 220px;
  background-color: #3d566e;
  color: white;
}

.layout-drawer aside .q-list-header {
  font-weight: bold;
  color: white;
  text-align: center;
  border-bottom: 1px solid gray;
}

.layout-drawer aside .q-item {
  border-bottom: 1px solid gray;
}

.layout-drawer aside .q-item:hover {
  border-bottom: 2px solid white;
  background-color: #263544;
  padding-left: 25px;
  font-weight: bold;
  -webkit-transition: all 500ms; /* Safari */
  transition: all 500ms;
}

.layout-drawer aside .q-collapsible-sub-item {
  padding: 0;
}

.layout-drawer aside .q-collapsible-sub-item a {
  color: white;
  text-decoration: none;
}

.layout-drawer aside .q-collapsible-sub-item .q-item-label {
  color: white;
  padding-left: 15px;
}

.layout-drawer aside .q-collapsible i.q-icon {
  color: white;
}
/* --- */
</style>